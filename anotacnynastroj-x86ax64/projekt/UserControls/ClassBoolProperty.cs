using System.Windows.Forms;

namespace Projekt.UserControls
{
    /// <summary>
    /// User control ktory sluzi na zobrazenie konkretnej vlastnosti danej triedy v checkboxe.
    /// </summary>
    public partial class ClassBoolProperty : UserControl
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ClassBoolProperty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Stav checkboxu daneho atributu
        /// </summary>
        public bool Checked
        {
            get { return checkBox.Checked; }
            set { checkBox.Checked = value; }
        }

        /// <summary>
        /// Meno daneho atributu zobrazene uzivatelovi
        /// </summary>
        public string AtrName
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        /// <summary>
        /// stav checkboxu pre vyhladavanie
        /// </summary>
        public bool Finding 
        {
            get { return cbFinding.Checked; }
            set { cbFinding.Checked = value; }
        }

        /// <summary>
        /// NAstavenie viditelnosti chceskboxu pre vyhladavanie
        /// </summary>
        public void SetCanFinding()
        {
           cbFinding.Visible = true; 
        }
    }
}
