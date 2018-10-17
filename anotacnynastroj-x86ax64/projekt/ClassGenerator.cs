using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projekt.Forms;
using Projekt.UserControls;


namespace Projekt
{
    /// <summary>
    /// Trieda pre pristup a pracu s jednotlivymi triedami a ich atributmi v nastroji
    /// </summary>
    [Serializable]
    public class ClassGenerator
    {
        [NonSerialized]
        private ComboBox _cmbClass;

        /// <summary>
        ///  panel na ktory sa maju pridat controly
        /// </summary>
        [NonSerialized]
        private Panel _pnlProperty;

        /// <summary>
        /// Nazvy atributov pre aktualne zvoleny typ triedy
        /// </summary>
        public string[] PropsName { get; set; }

        /// <summary>
        /// Triedy definovane uzivatelom
        /// </summary>
        private List<string> _class = new List<string>();

        /// <summary>
        /// Adresou obrazku triedy definovanej uzivatelom
        /// </summary>
        private List<string> _imageOfClass = new List<string>();

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="cmbClass">combobox s nazvami tried</param>
        /// <param name="pnlProperty">Panel kde sa nachadzaju atributy triedy</param>
        public ClassGenerator(ComboBox cmbClass, Panel pnlProperty)
        {
            _cmbClass = cmbClass;
            _pnlProperty = pnlProperty;
            PropsName = new string[0];
        }

        /// <summary>
        /// Nacitanie preddefinovanych tried objektov zo suboru. A ich vypis na CCB spolu s obrazkami.
        /// A taktiez zadefinovanie farieb pre jednotlive BB.
        /// </summary>
        public void GenerateClass(ComboBox cmbClass)
        {
            cmbClass.Items.Clear();
            _class.Clear();
            _imageOfClass.Clear();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (!File.Exists(@"Data\Class.dat")) return;

                fs = new FileStream(@"Data\Class.dat", FileMode.Open);
                sr = new StreamReader(fs);

                cmbClass.DrawMode = DrawMode.OwnerDrawVariable;

                while (!sr.EndOfStream)
                {
                    string[] param = sr.ReadLine().Split(';');
                    _class.Add(param[0]);
                    if (param.Length > 1) _imageOfClass.Add(param[1]);
                    else _imageOfClass.Add("");
                }

                for (int i = 0; i < _class.Count; i++)
                {
                    MainWindowApplication.ComboBoxItem cbbi = new MainWindowApplication.ComboBoxItem();
                    if (_imageOfClass[i] != "") cbbi.Value = Image.FromFile(@"Data\" + _imageOfClass[i]);
                    cbbi.Text = _class[i];
                    cbbi.Selectable = true;
                    cmbClass.Items.Add(cbbi);
                    cmbClass.DrawItem +=
                        delegate(object sender, DrawItemEventArgs e)
                            //delegat pre vytvorenie položiek v comboboxe spolu s obrazkami a textom.
                            {
                                e.DrawBackground();
                                if (_imageOfClass[e.Index] != "")
                                {
                                    e.Graphics.DrawImage(
                                        (Image) (cmbClass.Items[e.Index] as MainWindowApplication.ComboBoxItem).Value,
                                        e.Bounds.X, e.Bounds.Y, 15, 15);
                                }
                                e.Graphics.DrawString(
                                    (cmbClass.Items[e.Index] as MainWindowApplication.ComboBoxItem).Text,
                                    cmbClass.Font, Brushes.Black,
                                    new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                                e.DrawFocusRectangle();
                            };
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri nacitavani tried zo suboru:\n"+exc.Message, "Error- ClassGenerator.GenerateClass()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Vytvorenie atributov na paneli property pre aktualne vybranu triedu v comboboxe tried
        /// </summary>
        /// <param name="selectedItem">vybrana polozka v komboboxe</param>
        public void CreatePropertyForClass(MainWindowApplication.ComboBoxItem selectedItem)
        {
            _pnlProperty.Controls.Clear();
            if (selectedItem == null) return;
            string className = selectedItem.Text;
            CreatePropsControls(className);
        }

        /// <summary>
        /// Vrati zoznam vsetkych tried
        /// </summary>
        /// <returns>zoznam vsetkych tried</returns>
        public List<string> GetAllClasses()
        {
            return _class;
        }

        /// <summary>
        /// Vrati zoznam atributov pre zadanu triedu.
        /// </summary>
        /// <param name="className">nazov triedy ktorej zoznam atributov chceme</param>
        /// <returns>zoznam atributov pre zadanu triedu</returns>
        public List<string> GetPropsForClass(string className)
        {
            FileStream fs = null;
            StreamReader sr = null;
            List<string> propsName = null;
            try
            {
                if (className == "" || !File.Exists(@"Data\" + className + ".dat")) return null;
                fs = new FileStream(@"Data\" + className + ".dat", FileMode.Open);
                sr = new StreamReader(fs);

                propsName = new List<string>();

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');
                    if (line[0].Equals("01")) //01 indikuje ze ide o checkbox
                    {
                        propsName.Add(line[1]);
                    }
                    else //inak je to kombobox
                    {
                        propsName.Add(line[0]);
                    }
                }             
            }
            catch (IOException exc)
            {
                MessageBox.Show("Doslo k chybe." + exc.Message, "Error - ClassGenerator.GetPropsForClass()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
            return propsName;
        }

        /// <summary>
        /// Vytvorenie komboboxov a checkboxov na pozadovanom panely s vlastnostami triedy.
        /// </summary>
        /// <param name="className">nazov triedy</param>
        /// <param name="pnlProps">panel na ktorom chceme vytvorit komboboxy/chceckboxy danej triedy</param>
        public void CreatePropertiesControls(string className, Panel pnlProps)
        {
            CreatePropsControls(className, pnlProps);
        }

        /// <summary>
        /// Vytvorenie komboboxov a checkboxov na pozadovanom panely s vlastnostami triedy.
        /// </summary>
        /// <param name="className">nazov triedy</param>
        /// <param name="pnlProps">ak null tak sa vytvara na pnlProperty</param>
        private void CreatePropsControls(string className, Panel pnlProps = null)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                PropsName = new string[0];

                if (className == "" || !File.Exists(@"Data\" + className + ".dat")) return;
                fs = new FileStream(@"Data\" + className + ".dat", FileMode.Open);
                sr = new StreamReader(fs);

                List<string> propsName = new List<string>();

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(';');
                    if (line[0].Equals("01")) //01 indikuje ze chceme vytvorit checkbox
                    {
                        propsName.Add(line[1]);
                        AddCheckBoxToPanel(line[1], pnlProps);
                    }
                    else //inak chceme kombobox
                    {
                        propsName.Add(line[0]);
                        AddCbbToPanel(className, line, pnlProps);
                    }
                }
                PropsName = propsName.ToArray();
                
            }
            catch (IOException exc)
            {
                MessageBox.Show("Doslo k chybe." + exc.Message, "Error - ClassGenerator.CreatePropsControls()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Pridanie checkboxu na panel property.
        /// </summary>
        /// <param name="name">nazov checkboxu</param>
        /// <param name="pnlProps">nepoovinny atribut, ak nul tak pridame na pnlProperty</param>
        private void AddCheckBoxToPanel(string name, Panel pnlProps = null)
        {
            var control = new ClassBoolProperty();
            control.AtrName = name;
            control.Location = new Point(0, 30 * (pnlProps == null ? _pnlProperty.Controls.Count : pnlProps.Controls.Count));
            if(pnlProps == null)_pnlProperty.Controls.Add(control);
            else pnlProps.Controls.Add(control);
        }

        /// <summary>
        /// Pridanie comboboxu pre konkretnu vlastnost danej triedy na panel.
        /// </summary>
        /// <param name="className">nazoc triedy</param>
        /// <param name="line">zoznam hodnot atributov</param>
        /// <param name="pnlProps">nepoovinny atribut,ak nul tak pridame na pnlProperty</param>
        private void AddCbbToPanel(string className, string[] line, Panel pnlProps = null)
        {
            List<string> items = line.ToList();
            items.RemoveAt(0);
            var control = new ClassMultiProperty(className, line[0], items);
            control.Location = new Point(0, 30 * (pnlProps == null ? _pnlProperty.Controls.Count : pnlProps.Controls.Count));
            if (pnlProps == null) _pnlProperty.Controls.Add(control);
            else pnlProps.Controls.Add(control);
        }

        /// <summary>
        /// Mame nadefinovane hodnoty uzivatelom pre danu triedu a tie je potrebne nastavit ako defaultné.
        /// </summary>
        /// <param name="nameClass">nazov triedy</param>
        /// <param name="attrsValue">hodnoty atributov</param>
        public void SelectDefineValue(string nameClass, List<string> attrsValue)
        {
            int index = -1;
            for (int i = 0; i < _cmbClass.Items.Count; i++)
            {
                if (((MainWindowApplication.ComboBoxItem) _cmbClass.Items[i]).Text == nameClass)
                {
                    index = i;
                    break;
                }
            }
            _cmbClass.SelectedIndex = index; //tym ze zmenime polozku v komboboxe s nazvom triedy vytvoria sa aj komboboxy pre atributy

            //teraz potrebujeme naplnit komboboxy s atributami preddefinovanymi hodnotami
            for (int i = 0; i < _pnlProperty.Controls.Count; i++)
            {
                var control = _pnlProperty.Controls[i];
                if (control.GetType() == typeof (ClassMultiProperty))
                {
                    ((ClassMultiProperty)control).SetSelectedValue(i < attrsValue.Count ? attrsValue[i] : "");
                }
                else if (control.GetType() == typeof (ClassBoolProperty))
                {
                    ((ClassBoolProperty) control).Checked = i < attrsValue.Count && attrsValue[i] == "true";
                }
            }
        }

        /// <summary>
        /// Vytvorenie kopie 
        /// </summary>
        /// <returns>kopia</returns>
        public object Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                var obj = (ClassGenerator)formatter.Deserialize(stream);
                obj._cmbClass = _cmbClass;
                obj._pnlProperty = _pnlProperty;
                return obj;
            }
        }
    }
}
