using System.Windows.Forms;

namespace Projekt.UserControls
{
    /// <summary>
    /// Usercontrol reprezentujuci booleanovsky atribut.
    /// </summary>
    public partial class AddBoolAttribute : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public AddBoolAttribute()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Získanie názvu atribútu.
        /// </summary>
        /// <returns></returns>
        public string GetPropertyString()
        {
            string name = tbName.Text;
            return string.IsNullOrWhiteSpace(name) ? null : name;
        }

        /// <summary>
        /// Nastavenie názvu atribútu.
        /// </summary>
        /// <returns>nazov atributu</returns>
        public void SetNamePropertyString(string name)
        {
            tbName.Text = name;
        }

        /// <summary>
        /// Vracia a nastavuje stav checkboxu(stav daneho bool usercontrolu).
        /// </summary>
        public bool Checked
        {
            get { return checkBox.Checked; }
            set { checkBox.Checked = value; }
        }

        /// <summary>
        /// Nastavenie stavu booleanovskeho atributu, ci je ReadOnly alebo je ho mozne aj menit.
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
