using System.Windows.Forms;

namespace Projekt.UserControls
{
    /// <summary>
    /// UserControl pre atribúty a ich hodnoty.
    /// </summary>
    public partial class AddMultiAttribute : UserControl
    {
        /// <summary>
        /// Kontruktor
        /// </summary>
        public AddMultiAttribute()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Získanie názvu atribútu spolu s hodnotami atribútu vo forme stringu.
        /// </summary>
        /// <returns>názvu atribútu spolu s hodnotami atribútu oddelene bodkociarkov</returns>
        public string GetPropertyString()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbValues.Text))
                return null;
            string name = tbName.Text;
            string values = tbValues.Text.Replace(',', ';').ToLower();
            return (name == "" || values == "") ? "" : name + ";" + values;
        }

        /// <summary>
        /// Vratenie len nadefinovanych hodnot atributu bez nazvu atributu.
        /// </summary>
        /// <returns>vrati len hodnoty atributu oddelene bodkociarkov bez nazvu atributu</returns>
        public string GetValues()
        {
            return tbValues.Text.Replace(',', ';').ToLower();   
        }

        /// <summary>
        /// Nastavenie názvu atribútu spolu s hodnotou atribútu.
        /// </summary>
        /// <param name="array">pelo nazov a hodnoty atributu</param>
        public void SetNameAndPropertyString(string[] array)
        {
            string values = "";
            for(int i = 1; i< array.Length; i++)
            {
                values += array[i] + ((i == array.Length-1)? "" : ",");
            }

            tbName.Text = array[0];
            tbValues.Text = values.ToLower();
        }

        /// <summary>
        /// Nastavenie stavu viachodnotoveho atributu, ci je ReadOnly alebo je ho mozne aj menit.
        /// </summary>
        /// <param name="value">ak true tak atribut ReadOnly</param>
        public void SetNameReadOnly(bool value)
        {
            tbName.ReadOnly = value;
        }

        private void BtnRemoveSettingClick(object sender, System.EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
