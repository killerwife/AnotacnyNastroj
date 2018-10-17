using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Projekt.Enums;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca uzivatelske rozhranie pre umoznenie ulozenia projektu
    /// cize vyber a nastavenie vystupneho formátu.
    /// </summary>
    public partial class SetNameOutputFile : Form
    {
        /// <summary>
        /// Format zvolil uzivatel - XML alebo pascal, caltech (CSV)
        /// </summary>
        public EImportExportFormat SaveFormat {get ; private set;}

        private string _projectFolder;

        private ClassGenerator _myClasses;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="projectFolder">cesta k zlozke projektu</param>
        /// <param name="myClasses">zabezpecenie pristupu k triedam projektu</param>
        public SetNameOutputFile(string projectFolder, ClassGenerator myClasses)
        {
            InitializeComponent();

            _myClasses = myClasses;
            _projectFolder = projectFolder;
            cmbFormat.DataSource = Enum.GetValues(typeof(EImportExportFormat));

            if(_myClasses != null)FillCmbClasses();
        }

        /// <summary>
        /// Nampnenie comboboxu s nazvami tried
        /// </summary>
        private void FillCmbClasses()
        {
            cmbClassToSave.Items.Clear();
            cmbClassToSave.Items.Add("All Classes");
            foreach (string cls in _myClasses.GetAllClasses())
            {
                cmbClassToSave.Items.Add(cls);
            }
            cmbClassToSave.SelectedIndex = 0;
        }

        /// <summary>
        /// Trieda ktora sa ma ulozit pri volbe CSV ak sa maju ukladat vsetky tiedy vrati -1
        /// ak nie je volba CSV vrati null
        /// </summary>
        /// <returns>Trieda ktora sa ma ulozit pri volbe CSV ak sa maju ukladat vsetky tiedy vrati -1
        /// ak nie je volba CSV vrati null</returns>
        public string GetClassToSave()
        {
            return (SaveFormat != EImportExportFormat.Xml && SaveFormat != EImportExportFormat.Xml2) ? (cmbClassToSave.SelectedIndex == 0 ? "-1" : cmbClassToSave.Text) : null;
        }

        /// <summary>
        /// ID pre triedy ktore sa budu ukladat pri volbe CSV
        /// ak nie je volba CSV vrati null
        /// nazov atributu ktoreho hodnota bude reprezentovat danu triedu. Ak je vybrana konkretne trieda (GetClassTuSave != -1) pre ukladanie tak classid bude 
        /// predstavovat nazov atributu ktoreho hodnota bude reprezentovat danu triedu
        /// </summary>
        /// <returns>ID pre triedy ktore sa budu ukladat pri volbe CSV
        /// ak nie je volba CSV vrati null</returns>
        public string GetClassID()
        {
            return (SaveFormat != EImportExportFormat.Xml && SaveFormat != EImportExportFormat.Xml2) ? cmbClassID.Text : null;
        }

        /// <summary>
        /// Metoda ktora vracia nazov suboru ktory zadal uzivatel aj s formatom(xml/csv).
        /// </summary>
        /// <returns>nazov suboru ktory zadal uzivatel aj s formatom(xml/csv)</returns>
        public string GetOutputName()
        {
            return cbName.Text + ((SaveFormat == EImportExportFormat.Xml || SaveFormat == EImportExportFormat.Xml2) ? ".xml" : ".csv");
        }

        /// <summary>
        /// Pridanie predvolenych nazvov pre zvoleny typ subor z umiestnenia kde sa nachadza zlozka projektu.(xml/csv)
        /// </summary>
        /// <param name="enumType"></param>
        private void AddFileNameForSelect(EImportExportFormat enumType)
        {
            string type = ((enumType == EImportExportFormat.Xml || enumType == EImportExportFormat.Xml2) ? "xml" : "csv");
            cbName.Items.Clear();
            cbName.Text = "";
            string last = _projectFolder.Split(new[] {'/', '\\'}).Last();
            string[] xmlFiles = Directory.GetFiles(_projectFolder.Substring(0, _projectFolder.Count() - last.Count()), "*." + type);
            if (xmlFiles.Any())
            {
                foreach (string fileName in xmlFiles.Select(value => value.Split(new[] {'/', '\\'}).Last()))
                {
                    cbName.Items.Add(fileName.Substring(0, fileName.Count() - (fileName.Split(new[] { '.' }).Last().Count() + 1)));
                }
            }          
       }

        /// <summary>
        /// Obsluzenie akcie kliknutia na tlacidko save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbName.Text))
            {
                MessageBox.Show("Nebol zadaný názov súboru pre ulozenie. Zadajte ho prosim.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((SaveFormat != EImportExportFormat.Xml && SaveFormat != EImportExportFormat.Xml2) && (cmbClassToSave.SelectedIndex == -1 || cmbClassID.SelectedIndex == -1))
            {
                MessageBox.Show("Nebola zvolena trieda pre ulozenie alebo ID triedy. Doplnte udaje prosim.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Kontrola ci uzivatel nezadal neprisustny znak ako nazov suboru.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckValue(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)46 || e.KeyChar == (char)58 || e.KeyChar == (char)47 || e.KeyChar == (char)92; //46 je bodka; 47 lomka/; 58 bodkociarka;92 lomka\
        }

        /// <summary>
        /// uzivatel zmenil moznost ulozenia do XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedFormatChanged(object sender, EventArgs e)
        {
            SaveFormat = (EImportExportFormat)cmbFormat.SelectedItem;
            AddFileNameForSelect(SaveFormat);
            gbCsvOption.Enabled = (SaveFormat != EImportExportFormat.Xml && SaveFormat != EImportExportFormat.Xml2);
        }

        /// <summary>
        /// doslo k zmene triedy pre ulozenie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedClassToSaveChanged(object sender, EventArgs e)
        {
            cmbClassID.Items.Clear();
            if(cmbClassToSave.SelectedIndex == -1)
                return;
            if (cmbClassToSave.SelectedIndex == 0)
            {
                cmbClassID.Items.Add("Class Name");
                cmbClassID.SelectedIndex = 0;
            }
            else
            {
                foreach (string prop in _myClasses.GetPropsForClass(cmbClassToSave.Text))
                {
                    cmbClassID.Items.Add(prop);
                }
            }
        }

        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Na vyber su 3 vystupne formaty XML, Csv2Point(CSV), CsvPointSize(CSV).\n" +
                            "Struktura Csv2Point:\n" +
                            "image_path;left_top_x;left_top_y; right_bottom_x;right_bottom_y;classid\n\n" +
                            "Struktura CsvPointSize:\n" +
                            "image_path;left_top_x;left_top_y;width;height;classid\n\n" +
                            "Pre viac info pozri HELP->Tutorial sekcia Ulozenie projektu\n\n" +
                            "Po vybere formatu XML\n staci vybrat subor(ci uz existujuci,\n" +
                            "alebo zadat nazov noveho suboru) a potvrdit nastavenia, nasledne\n" +
                            "sa spusti ukladanie projektu.Ak sa vyberie niektori z CSV formatov\n" +
                            "tak treba vybrat subor rovnako ako pri formate XML. No pri tejto\n" +
                            "volbe je potrebne zadefinovat, ktore triedy boundingboxov sa maju\n" +
                            "ukladat (volba: Class To Save). Ak sa vyberie moznost All Classes\n" +
                            "automaticky sa ulozia boundingboxy vsetkych tried,pricom hodnota\n" +
                            "atributu ClassId bude nazov triedy konkretneho boundingboxu.Ak sa\n" +
                            "vyberie len konkretna trieda boundingboxou, ktore sa maju ulozit,\n" +
                            "je potrebne zvolit, hodnoty ktoreho atribut tejto triedy sa ulozia\n" +
                            "ako ClassID (volba: ClassID).\n" +
                            "Formaty CSV sluzia len na ukladanie boundingboxov.\n" +
                            "Format XML sluzi na ukladanie vsetkych objektov (Boundingboxy, Paintingy, Polyline)", 
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
