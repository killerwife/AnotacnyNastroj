using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Projekt.UserControls
{
    /// <summary>
    /// User control ktory sluzi na zobrazenie konkretnej vlastnosti danej triedy v komboboxe.
    /// </summary>
    public partial class ClassMultiProperty : UserControl
    {
        /// <summary>
        /// Nazov triedy ku ktorej patri dany atribut.
        /// </summary>
        private string ClassName { get; set; }

        /// <summary>
        /// Nazov zobrazovaneho atributu.
        /// </summary>
        private string AttributeName { get; set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="className">Nazov triedy ku ktorej patri dany atribut.</param>
        /// <param name="attributeName">Nazov vlastnosti.</param>
        /// <param name="attributeValues">Hodnoty ktore moze nadobudat vlastnost.</param>
        public ClassMultiProperty(string className, string attributeName, List<string> attributeValues)
        {
            InitializeComponent();
            ClassName = className;
            SetName(attributeName);
            SetAttribute(attributeValues);
        }

        /// <summary>
        /// Nastavenie nazvu atributu.
        /// </summary>
        /// <param name="atrName">Nastavenie nazvu atributu</param>
        private void SetName(string atrName)
        {
            AttributeName = atrName;
            lblName.Text = AttributeName;
        }

        /// <summary>
        /// Metoda ktora vracia nazov atributu.
        /// </summary>
        /// <returns>nazov atributu</returns>
        public string GetName()
        {
            return lblName.Text;
        }

        /// <summary>
        /// Nastavenie poloziek komboboxu.
        /// </summary>
        /// <param name="attributeValues">Hodnoty ktore moze nadobudat vlastnost.</param>
        private void SetAttribute(List<string> attributeValues)
        {
            foreach (string atribute in attributeValues)
            {
                cmbAtribute.Items.Add(atribute.ToLower());
            }
        }

        /// <summary>
        /// Vratenie hodnoty ktora je vybrana v komboboxe.
        /// </summary>
        /// <returns>hodnota vybrana v comboboxe</returns>
        public string GetSelectedValue()
        {
            string newValue = cmbAtribute.Text.ToLower();

            //kontrola ci uzivatel rucne nevpisal hodnotu do komboboxu
            if (!newValue.Equals("") && cmbAtribute.Items.IndexOf(newValue) == -1) //ak ano musime ju pridat co komboboxu a aj do suboru pre dany atribut
            {
                cmbAtribute.Items.Add(newValue);
                cmbAtribute.SelectedItem = newValue;
                if(!ClassName.Equals(""))SaveNewValueOfAtribute(newValue);
            }
            return ((cmbAtribute.SelectedItem) ?? "").ToString();
        }

        //Ulozenie novej hodnoty atributu do suboru s hodnotami tohto atributu
        private void SaveNewValueOfAtribute(string newValue)
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(@"Data\" + ClassName + ".dat", FileMode.Open);
                sr = new StreamReader(fs);

                bool find = false;
                List<string> text = new List<string>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] param = line.Split(';');
                    if (AttributeName.Equals(param[0])) //hladame riadok na ktorom je ulozeny dany atribut
                    {
                        line += (param.Count() == 1 ? newValue : ";" + newValue);
                            //ak je to prva hodnota atributu tak bodkociarku nepiseme
                        find = true;
                    }
                    text.Add(line);
                }

                sr.Close();

                if (find)
                {
                    fs = new FileStream(@"Data\" + ClassName + ".dat", FileMode.Create);
                    var sw = new StreamWriter(fs);
                    foreach (string s in text)
                    {
                        sw.WriteLine(s);
                    }

                    sw.Close();
                }

                fs.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error - ClassMultiProperty.SaveNewValueOfAtribute()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Nastavenie hodnoty v komboboxe.
        /// </summary>
        /// <param name="value">hodnota pre combobox</param>
        public void SetSelectedValue(string value)
        {
            //cmbAtribute.SelectedIndex = cmbAtribute.Items.IndexOf(value.ToLower());
            if (value == "") cmbAtribute.SelectedIndex = -1;
            else cmbAtribute.Text = value.ToLower();
        }

        /// <summary>
        /// stav checkboxu Finding
        /// </summary>
        public bool Finding
        {
            get { return cbFinding.Checked; }
            set { cbFinding.Checked = value; }
        }

        /// <summary>
        /// Nastavenie viditelnosti chceckboxu Finding
        /// </summary>
        public void SetCanFinding()
        {
            cbFinding.Visible = true;
        }
    }
}
