using System.Collections.Generic;
using System.Windows.Forms;

namespace Projekt.UserControls
{
    /// <summary>
    /// Usercontrol pre atributy obrazka
    /// </summary>
    public partial class PictureAttribute : UserControl
    {
        /// <summary>
        /// Nazov zobrazovaneho atributu.
        /// </summary>
        public string AttributeName {
            get {return lblName.Text; }
            set { lblName.Text = value; }
        }

        public bool IsBoolean { get; private set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="attributeName">Nazov vlastnosti.</param>
        /// <param name="attributeValues">Hodnoty ktore moze nadobudat vlastnost.</param>
        public PictureAttribute(string attributeName, List<string> attributeValues)
        {
            InitializeComponent();
            if (attributeValues != null) IsBoolean = attributeValues.Count > 0 && attributeValues[0] == "01";
            AttributeName = attributeName;
            SetAttribute(attributeValues);
        }

        /// <summary>
        /// Nastavenie poloziek komboboxu.
        /// </summary>
        /// <param name="attributeValues">Hodnoty ktore moze nadobudat vlastnost.</param>
        private void SetAttribute(List<string> attributeValues)
        {
            if (IsBoolean)
            {
                cmbAtribute.Visible = false;
                cbAtribute.Visible = true;
                cbAtribute.Checked = attributeValues.Count > 1 && attributeValues[1] == "true";
            }
            else
            {
                foreach (string atribute in attributeValues)
                {
                    cmbAtribute.Items.Add(atribute);
                }
            }
        }

        /// <summary>
        /// Vratenie hodnoty ktora je vybrana v komboboxe.
        /// </summary>
        /// <returns>hodnoty vybrana v komboboxe</returns>
        public string GetSelectedValue()
        {
            if (IsBoolean)
            {
                return cbAtribute.Checked.ToString().ToLower();
            }

            string newValue = cmbAtribute.Text;

            //kontrola ci uzivatel rucne nevpisal hodnotu do komboboxu
            if (!newValue.Equals("") && cmbAtribute.Items.IndexOf(newValue) == -1)
                //ak ano musime ju pridat co komboboxu
            {
                cmbAtribute.Items.Add(newValue);
                cmbAtribute.SelectedItem = newValue;
            }
            return ((cmbAtribute.SelectedItem) ?? "").ToString();
        }

        /// <summary>
        /// Nastavenie hodnoty v komboboxe.
        /// </summary>
        /// <param name="value">hodnota pre nastavenie do comboboxu</param>
        public void SetSelectedValue(string value)
        {
            if (IsBoolean)
                cbAtribute.Checked = value == "true";
            else 
                cmbAtribute.SelectedIndex = cmbAtribute.Items.IndexOf(value);
        }
    }
}
