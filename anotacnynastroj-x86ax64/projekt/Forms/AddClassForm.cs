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
    /// Formulár pre pridane novej triedy objektov spolu s vlastnosťami tejto triedy.
    /// </summary>
    public partial class AddClassForm : Form
    {
        /// <summary>
        /// Povodny nazov triedy pred editovanim.
        /// </summary>
        private string _className;

        /// <summary>
        /// Zoznam property(atribútov) pre vytváranú triedu.
        /// </summary>
        private List<string> _props = new List<string>();

        /// <summary>
        /// zoznam tried
        /// </summary>
        private List<string[]> _classes = null;

        /// <summary>
        /// Priznakova premenna - ak TRUE -> editujem, ak FALSE -> vytvaram novy typ triedy
        /// </summary>
        private bool _edit = false;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="edit">Priznakova premenna - ak TRUE -> editujem, ak FALSE -> vytvaram novy typ triedy</param>
        public AddClassForm(bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (!edit) AddAtributeToPanel(new AddMultiAttribute());
        }

        /// <summary>
        /// Obslúženie akcie kliknutia na tlačítko Browse, pri výbere obrázku pre vytváranú triedu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyBrowseClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbImagePath.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// Obsluženie akcie kliknutia na tlačítko Add - pridanie viachodnotovych atribútov pre vytváranú triedu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMultivalueAttributeClick(object sender, EventArgs e)
        {
            AddAtributeToPanel(new AddMultiAttribute());
        }

        /// <summary>
        /// Pridanie UserControl na panel s vlastnostami pre atribut.
        /// </summary>
        /// <param name="control"></param>
        private void AddAtributeToPanel(UserControl control)
        {
            pnlAttribute.VerticalScroll.Value = 0;
            control.Location = new Point(3, (3 + 26) * pnlAttribute.Controls.Count);
            pnlAttribute.Controls.Add(control);
        }

        /// <summary>
        /// Obsluženie akcie kliknutia na tlačítko Add - pridanie dvojhodnotovych atribútov pre vytváranú triedu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddBooleanAttributeClick(object sender, EventArgs e)
        {
            AddAtributeToPanel(new AddBoolAttribute());
        }

        /// <summary>
        /// Obslúženie akcie kliknutia na tlačítko Save. Uloženie triedy spolu s atribútmi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MySaveClick(object sender, EventArgs e)
        {
            _props.Clear();
            if (string.IsNullOrWhiteSpace(tbClassName.Text))
            {
                MessageBox.Show("Nie je vyplneny nazov triedy", "Chybne vyplnenie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (Control control in pnlAttribute.Controls)
            {
                if (control.GetType() == typeof (AddMultiAttribute))
                {
                    string values = ((AddMultiAttribute)control).GetPropertyString();
                    if (values != null)
                        _props.Add(values);
                    else
                    {
                        MessageBox.Show("Niektori z viachodnotovych atributov nema zadefinovane meno alebo hodnoty","Zle vyplnene hodnotu.", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (control.GetType() == typeof (AddBoolAttribute))
                {
                    string value = ((AddBoolAttribute) control).GetPropertyString();
                    if (value != null) _props.Add("01;" + value);
                    else
                    {
                        MessageBox.Show("Niektori z boolenovskych atributov nema zadefinovane meno","Zle vyplnene hodnotu.", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                    
                }
                
            }
            if(_edit) SaveAfterEdit();
            SaveToFile();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Uloženie novej triedy spolu s atribútmi do súboru.
        /// </summary>
        private void SaveToFile()
        {
            TextWriter sw = null;
            try
            {
                string imageName = "";
                if (!string.IsNullOrWhiteSpace(tbImagePath.Text))
                {
                    imageName = tbImagePath.Text.Split(new char[] { '/', '\\' }).Last();
                    if (!tbImagePath.Text.Equals(imageName)) File.Copy(tbImagePath.Text, "Data\\" + imageName, true);
                }

                sw = new StreamWriter(@"Data\Class.dat", true);

                if(!_edit) sw.WriteLine();
                sw.Write(tbClassName.Text + ";" + imageName);
                sw.Close();

                //ukladanie atributov triedy
                sw =  new StreamWriter(@"Data\"+tbClassName.Text+".dat");

                foreach (string prop in _props)
                {
                    sw.WriteLine(prop);   
                }
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri ukladani novej triedy spolu s atribútmi do súboru:\n" + exc.Message, "Error - AddClassForm.SaveToFile()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (sw != null) sw.Close();
            }
        }

        /// <summary>
        /// Prepisanie subora s triedami po editovani triedy.
        /// </summary>
        public void SaveAfterEdit()
        {
            TextWriter sw = null;
            try
            {
                sw = new StreamWriter(@"Data\Class.dat", false);
                foreach (string[] array in _classes)
                {
                    if (array[0] != _className)
                    {
                        sw.WriteLine(array[0] + ";" + array[1]);
                    }
                    else
                    {
                        File.Delete(_className + ".dat");
                    }
                }               
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri ukladani triedy spolu s atribútmi do súboru:\n" + exc.Message, "Error - AddClassForm.SaveAfterEdit()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (sw != null) sw.Close();
            }
            
        }

        /// <summary>
        /// Zobrazenie povodnych atributov triedy pred editovanim.
        /// </summary>
        /// <param name="pClass">nazov tredy ktorej atributy chceme zobrazit</param>
        public void MyInitialize(string pClass)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (!File.Exists(@"Data\Class.dat")) return;

                _className = pClass;
                _classes = new List<string[]>();

                fs = new FileStream(@"Data\Class.dat", FileMode.Open);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    string[] param = sr.ReadLine().Split(';');
                    _classes.Add(param);
                    if (pClass.Equals(param[0]))
                    {
                        tbClassName.Text = param[0];
                        if (param.Length > 1) tbImagePath.Text = param[1];
                    }
                }
                fs.Close();
                sr.Close();

                if (!File.Exists(@"Data\" + pClass + ".dat")) return;

                fs = new FileStream(@"Data\" + pClass + ".dat", FileMode.Open);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    string[] value = sr.ReadLine().Split(';');
                    if (value[0] == "01")
                    {
                        var control = new AddBoolAttribute();
                        control.SetNamePropertyString(value[1]);
                        AddAtributeToPanel(control);
                    }
                    else
                    {
                        var control = new AddMultiAttribute();
                        control.SetNameAndPropertyString(value);
                        AddAtributeToPanel(control);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri nacitavani tried a ich atributov zo súboru:\n" + exc.Message, "Error - AddClassForm.MyInitialize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Obsluzenie kliknutia na tlacidlo help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Hodnoty pre viachodnotové atribúty zadávajte oddelené čiarkami alebo bodkočiarkami, " +
                            "pričom za poslednú hodnotou sa čiarka resp. bodkočiarka nedáva.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PnlAttributeControlRemoved(object sender, ControlEventArgs e)
        {
            try
            {
                var controls = (sender as Panel).Controls;
                if (controls.Count == 0) return;
                for (int i = 0; i < controls.Count; i++)
                {
                    controls[i].Location = new Point(3, i * 29);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error-CreateImageFromDrawObj.SettingRemovedFromControl", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

    }
}
