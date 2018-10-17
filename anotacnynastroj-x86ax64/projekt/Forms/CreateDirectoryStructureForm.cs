using System;
using System.Windows.Forms;
using Projekt.DrawObjects.Extensions;
using Projekt.Enums;

namespace Projekt.Forms
{
    /// <summary>
    /// trieda reprezentujuca UI pre zadefinovanie adresarovej struktury pre ulozenie
    /// vygenerovaných dát
    /// </summary>
    public partial class CreateDirectoryStructureForm : Form
    {
        /// <summary>
        /// Nastavenia pre adresarovu strukturu
        /// </summary>
        public SettingForFolders SettingForFolds { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="settingForFolders">nastavenia ktore sa maju zobrazit uzivatelovi</param>
        public CreateDirectoryStructureForm(SettingForFolders settingForFolders)
        {
            InitializeComponent();
            SettingForFolds = settingForFolders;
            cmbTypeSortDir.Items.Clear();
            foreach (Enum value in Enum.GetValues(typeof(ETypeOfSortDir)))
            {
                cmbTypeSortDir.Items.Add(value.GetDescription());
            }

            SelectSetting();
        }

        private void SelectSetting()
        {
            cmbNumAttrs.SelectedIndex = SettingForFolds.MaxCountAttrs;
            cmbTypeSortDir.SelectedIndex = (int) SettingForFolds.TypeOfSortDir;
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void TypeSortDirChanged(object sender, EventArgs e)
        {
            SettingForFolds.TypeOfSortDir = (ETypeOfSortDir)cmbTypeSortDir.SelectedIndex;
        }

        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("V pripade zaskrtnutia moznosti 'Real/Artificial Folder As Last'\n" +
                            "tak vysledna struktura bude obsahovat najskor zlozky vytvorene\n" +
                            "z atributov tried a az potom budu zlozky Real resp. Artificial.\n" +
                            "V pripade umelych dat(Artificial) bude este tato zlozka obsahovat\n" +
                            "zlozky pre jednotlive typy transformacii.\n" +
                            "Pri nezaskrtnutej volbe budu najskor zlozky Real resp. Artificial\n" +
                            "az v nich struktura vytovrena podla atributov tried. V pripade\n" +
                            "umelych dat(Artificial) bude tato zlozka obsahovat najskor zlozky\n" +
                            "pre jednotlive transformacie a az potom struktura vytovrena podla\n" +
                            "atributov tried.\n" +
                            "V oboch pripadoch su ako prve zlozky TRENING/VALIDATE/TESTING.\n\n" +
                            "Dalsie nastavenia sluzia na zadefinovanie poctu atributov, ktore sa\n" +
                            "maju pouzit pri vytvarani adresarovych struktur. Atributy triedy sa\n" +
                            "vyberaju podla poradia v akom su definovane. Cize ak zadame pocet 3,\n" +
                            "tak sa bude vytvarat adresarova struktura podla prvych 3 atributov.\n", 
                "Help - Pre viac informacii pozri Tutorial", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NumAttrsForStructChanged(object sender, EventArgs e)
        {
            SettingForFolds.MaxCountAttrs = cmbNumAttrs.SelectedIndex < cmbNumAttrs.Items.Count - 1 ? cmbNumAttrs.SelectedIndex : int.MaxValue;
        }
    }

    [Serializable]
    public class SettingForFolders
    {
        /// <summary>
        /// pocet atributov z ktorych sa ma tvorit adresarova struktura
        /// </summary>
        public int MaxCountAttrs { get; set; }

        /// <summary>
        /// Ako za ma vytvorit adresarova struktura - poradie jednotlivych zloziek
        /// </summary>
        public ETypeOfSortDir TypeOfSortDir { get; set; }

        public SettingForFolders()
        {
            MaxCountAttrs = 0;
            TypeOfSortDir = 0;
        }

        public override string ToString()
        {
            return string.Format("Type of Sort: {0}, Numbers Of Attribute For Structure: {1}", TypeOfSortDir.GetDescription(), MaxCountAttrs);
        }
    }
}
