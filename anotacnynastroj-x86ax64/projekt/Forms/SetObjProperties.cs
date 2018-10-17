using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Projekt.UserControls;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca uzivatelske rozhranie pre umoznenie zadefinovania
    /// preddefinovanych hodnot jednotlivým atributom tried
    /// </summary>
    public partial class SetObjProperties : Form
    {
        private List<string> _nameClasses;
        private List<string> _attributeName;
        private KeyValuePair<string, List<string>> _attributeValue; //kluc je nazov triedy
        private Dictionary<string, List<string>> _classesWithParams;

        private string _defaultClass;
        private string pathUserDefClass = @"Data\Setting\UserDefautClass.dat";
        private const string pathUserDefValue = @"Data\Setting\UserDefValue.dat";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">cesta kt obazku, v pripade ze by chcel uzivatel zadefinovat hodnoty atributom pre tieto cesty</param>
        public SetObjProperties(string path)
        {
            InitializeComponent();
            _nameClasses = new List<string>();
            _attributeName = new List<string>();
            _attributeValue = new KeyValuePair<string, List<string>>();
            _classesWithParams = null;
            _defaultClass = null;
            tbPath.Text = path;
            CreateClass();

            LoadSettingForClassFromFile();
        }

        /// <summary>
        /// Nacitanie a zobrazenie vsetkych tried, ktore su vytvorene.
        /// </summary>
        private void CreateClass()
        {
            cbClass.Items.Clear();
            cmbDefaultClass.Items.Clear();
            pnlAttributes.Controls.Clear();
            _nameClasses.Clear();

            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (!File.Exists(@"Data\Class.dat")) return;

                fs = new FileStream(@"Data\Class.dat", FileMode.Open);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    string[] param = sr.ReadLine().Split(';');
                    _nameClasses.Add(param[0]);
                    cbClass.Items.Add(param[0]);
                    cmbDefaultClass.Items.Add(param[0]);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba" + exc.Message, "Error-SetObjProperties.CreateClass()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Po vybere triedy je potrebne zobrazit vsetky nazvy atributov,
        /// ktora tato trieda ma spolu s preddefinovanymi hodnotami ak nejake su.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateAttributeForClass(object sender, EventArgs e)
        {
            string selectClass = cbClass.Text;
            pnlAttributes.Controls.Clear();
            _attributeName.Clear();

            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (!File.Exists(@"Data\" + selectClass + ".dat"))
                {
                    MessageBox.Show("Lutujem, no dana trieda nebola najdena", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                fs = new FileStream(@"Data\" + selectClass + ".dat", FileMode.Open);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    string[] param = sr.ReadLine().Split(';');
                    if (param[0] == "01")
                    {
                        _attributeName.Add(param[0] + param[1]);
                    }
                    else _attributeName.Add(param[0]);
                }

                CreateAtributes(_classesWithParams.ContainsKey(selectClass) ? _classesWithParams[selectClass] : null);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba" + exc.Message, "Error-SetObjProperties.CreateAttributeForClass()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Zobrazenie vsetkych atributov uzivatelovi a prednastavenie hodnot ak nejake su
        /// </summary>
        /// <param name="deffValue">preddef hodnoty pre danu triedu</param>
        private void CreateAtributes(List<string> deffValue)
        {
            int i = 0;
            foreach (string attribute in _attributeName)
            {
                Control atr;
                if (attribute.Substring(0, 2) == "01")
                {
                    atr = new AddBoolAttribute();
                    ((AddBoolAttribute)atr).SetNamePropertyString(attribute.Replace("01",""));
                    ((AddBoolAttribute)atr).SetNameReadOnly(true);
                    ((AddBoolAttribute)atr).Checked = (deffValue != null && deffValue[i] == "true");

                }
                else
                {
                    atr = new AddMultiAttribute();
                    ((AddMultiAttribute)atr).SetNameAndPropertyString(deffValue == null ? new[] { attribute} :new[] { attribute,deffValue[i]});
                    ((AddMultiAttribute)atr).SetNameReadOnly(true);
                }
                
                atr.Location = new Point(0, 26 * i);
                
                pnlAttributes.Controls.Add(atr);
                i++;
            }
        }

        /// <summary>
        /// Potvrdenie nastaveni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            if(!AddDefineValueForClass()) return;
 
            SaveToFile();
            
            _defaultClass = cmbDefaultClass.SelectedIndex != -1 ? cmbDefaultClass.Text : null;
            if (_defaultClass != null)
                SaveDefaultClass();


            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Ulozenie defaultnej triedy - vytvori sa subor kde bude nazov tejto defaultnej triedy.
        /// </summary>
        private void SaveDefaultClass()
        {
            TextWriter sw = null;
            try
            {        
                sw = new StreamWriter(pathUserDefClass, false);
                sw.WriteLine(_defaultClass);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri ukladani defaultnej triedy" + exc.Message, "Error - SetObjProperties.SaveDefaultClass()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sw != null)sw.Close();
            }
        }

        /// <summary>
        /// Ulozenie nastaveni do suboru.
        /// </summary>
        private void SaveToFile()
        {
            AddDefineValueForClass();

            //ulozime nove nastavenia do suboru
            TextWriter sw = null;
            try
            {
                sw = new StreamWriter(pathUserDefValue, false);
                foreach (KeyValuePair<string, List<string>> classWithParams in _classesWithParams)
                {
                    string line = "";
                    foreach (string value in classWithParams.Value)
                    {
                        line += value + ";";
                    }
                    if (line != "")
                    {
                        line += classWithParams.Key;
                        sw.WriteLine(line);
                    }
                }
                sw.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri ukladani preddefinovanych nastaveni tried" + exc.Message, "Error- SetObjProperties.SaveToFile()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if(sw != null)sw.Close();
            }
        }

        /// <summary>
        /// Pridame uzivatelom zadefinovane hodnoty do zoznamu
        /// </summary>
        private bool AddDefineValueForClass()
        {
            if (!ReadValuesFromForm())
            {
                _attributeValue = new KeyValuePair<string, List<string>>();
                return false;
            }
            if (_attributeValue.Key == null) return true;
            if (_classesWithParams.Count == 0)//ziadne nastavenia
                _classesWithParams.Add(_attributeValue.Key, _attributeValue.Value); 
            else
            {
                //v zozname je viac tried s preddefinovanymi hodnotami, treba skontrolovat ci uz nie je zaznam pre pridavanu
                if (_classesWithParams.ContainsKey(_attributeValue.Key))
                    _classesWithParams[_attributeValue.Key] = _attributeValue.Value;
                else
                    _classesWithParams.Add(_attributeValue.Key, _attributeValue.Value); 
            }
            _attributeValue = new KeyValuePair<string, List<string>>();
            return true;
        }

        /// <summary>
        /// Nacitanie zadefinovanych nastaveni z formularu
        /// </summary>
        private bool ReadValuesFromForm()
        {
            if (cbClass.SelectedIndex == -1)
            {
                _attributeValue = new KeyValuePair<string, List<string>>();
                return true;
            }

            _attributeValue = new KeyValuePair<string, List<string>>(cbClass.Text, new List<string>());
            foreach (Control control in pnlAttributes.Controls)
            {
                if (control.GetType() == typeof (AddMultiAttribute))
                {
                    string s = ((AddMultiAttribute) control).GetValues().Replace(" ", "");
                    //tu treba skontrolovat ci sa zadal znak '=' ktory urcuje defautnu hodnotu,
                    //ak nie je zadany znak '=' tak ide o preddefinovanu hodnotu z cesty a tym padom musi byt hodnota typu int
                    if (s == "" || s.Substring(0, 1) == "=") //atribut nie je vyplneny alebo je to defaultny atribut
                        _attributeValue.Value.Add(s);
                    else //inak je to atribut pre cestu a musi byt int
                    {
                        int pom;
                        if (int.TryParse(s, out pom))
                            _attributeValue.Value.Add(s);
                        else
                        {
                            _attributeValue.Value.Clear();
                            MessageBox.Show("Zadali ste zlú hodnotu: '" + s + "' pre atribút." +
                                            " Ak chcete zadať defaultnú hodnotu bez ohľadu na cestu k obrázku " +
                                            "musíte zadať symbol '=',ak chcete preddefinovať hodnotu atribútu " +
                                            "podľa cesty musí byť táto hodnota typu INT. " +
                                            "Pre viac informácii pozrite HELP.", "Error", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return false;
                        }
                    }

                }
                else if (control.GetType() == typeof (AddBoolAttribute))
                {
                    _attributeValue.Value.Add(((AddBoolAttribute) control).Checked ? "true" : "false");
                }
            }
            return true;
        }

        /// <summary>
        /// potrebujem nacitat subor s nastaveniami, ci už v nom nie su definovane atributy
        /// pre triedy.
        /// </summary>
        private void LoadSettingForClassFromFile()
        {
            _classesWithParams = new Dictionary<string, List<string>>();
            
            if (File.Exists(pathUserDefValue))
            {
                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(pathUserDefValue, FileMode.OpenOrCreate);
                    sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        var param = sr.ReadLine().Split(';').ToList();
                        var className = param.Last();
                        param.RemoveAt(param.Count-1);
                        //if (param[param.Count() - 1] != cbClass.Text)
                            //ak je v subore definovany riadok pre upravovanu triedu, tak ho neulozime
                        _classesWithParams.Add(className, param);
                        // pridame riadok zo suboru ten predstavuje nastavenia pre jednu triedu, nazov triedy je ako posledny v zozname
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Chyba" + exc.Message, "Error - SetObjProperties.LoadSettingForClassFromFile()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fs != null) fs.Close();
                    if (sr != null) sr.Close();
                }
            }
        }

        /// <summary>
        /// Metoda ktora sluzi na vratenie uzivatelom zadefinovanych nastaveni,
        /// ktore urcuju ako nastavit atributy tried. Ci uz z cesty ku obrazku alebo defaultnov hodnotou.
        /// Posedny provok v zozname ktory vracia tato metoda urcuje nazov
        /// triedy objektov ktoremu sa maju naplnat atributy.
        /// </summary>
        /// <returns>Zoznam ktoreho jednotlive polozky urcuju ktora preddefinovana hodnota
        /// sa ma pouzit na naplnenie atributu(to ktoreho atributu urcuje poradie v zozname: 
        /// prvy zaznam = prvy atribut danej triedy), az na posledny prvok zoznamu, ten urcuje
        /// nazov triedy pre ktoru su preddefinovane tieto hodnoty atributov</returns>
        public Dictionary<string, List<string>> GetAllUserDefAttr()
        {
            return _classesWithParams;
        }

        /// <summary>
        /// Vrati uyivatelom yadefinovanu defaultnu triedu
        /// </summary>
        /// <returns></returns>
        public string GetDefaultClass()
        {
            return _defaultClass;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Po výbere triedy z komboboxu CLASS sa zobrazia všetky atribúty danej " +
                            "triedy, ktorým je možné následne zadefinovať preddefinované hodnoty " +
                            "a to nasledovne:\n" +
                            "Ak chceme aby sa hodnota atribútu generovala podla cesty k obrazku, " +
                            "zvolíme pre tento atribút hodnotu {1,2,3,...} podľa toho, ktorá zložka " +
                            "(zanorenie v ceste) predstavuje hodnotu atribútu.\n" +
                            "Ak chceme preddefinovať hodnotu niektorého z atribútov pevne (bez ohľadu " +
                            "na cestu) zapíšeme tomuto atribútu požadovanú hodnotu a napíšeme pred ňu " +
                            "symbol {=}. (Napr. pre atribut farba chceme pevne zadefinovať predvolenú " +
                            "hodnotu na modrú. Spravíme to tak že do textboxu pre atribút farba zapíšeme " +
                            "=modra)." +
                            "Ak chceme preddefinovať hodnotu určenú checkboxom(dvojhodnotovú), tak ju " +
                            "nastavíme podľa toho ako chceme aby bola defaultne nastavená(označíme alebo " +
                            "neoznačíme.)", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// uzivatel rozklikol combobox s triedami, treba upravit defaultne nastavenia
        /// ktore zadefinoval 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbClassDropDown(object sender, EventArgs e)
        {
            AddDefineValueForClass();
        }
    }
}
