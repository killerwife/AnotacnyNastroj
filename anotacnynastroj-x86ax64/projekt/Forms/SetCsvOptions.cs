using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Projekt.Enums;

namespace Projekt.Forms
{
    /// <summary>
    /// trieda reprezentujuca uzivatelske rozhranie pre volbu csv
    /// ide o nastavenie triedy a atributov pre boundingboxy v tomto subore
    /// </summary>
    public partial class SetCsvOptions : Form
    {
        private readonly ClassGenerator _myClasses;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="myClasses">pre pristup k triedam v nastroji</param>
        public SetCsvOptions(ClassGenerator myClasses)
        {
            InitializeComponent();
            var items =  new ArrayList(Enum.GetValues(typeof(EImportExportFormat)));
            items.Remove(EImportExportFormat.Xml);
            cmbFormatType.DataSource = items;
            _myClasses = myClasses;
            SetClasses();
        }

        /// <summary>
        /// konkretny format csv suboru
        /// </summary>
        public EImportExportFormat CsvFormat { get; set; }

        /// <summary>
        /// Vytvorenie prednastavenej volby. Class Id bude predstavovat triedu BB.
        /// </summary>
        private void InitClasses()
        {
            cmbClassId.Items.Clear();
            cmbClassId.Items.Add("Class");
            cmbClassId.SelectedIndex = 0;
            EnableClassId(false);
        }

        /// <summary>
        /// V pripade ze uzivatel zadal konkretnu triedu aka sa ma priradit nacitavanim BB,
        /// tak potom hodnota classId v subore predstavuje hodnotu konkretneho atributu, ktory
        /// vybral uzivatel a nazov tohoto atributu vrati tato metoda
        /// </summary>
        /// <returns>nazov atributu ktoremu sa ma priradit hodnota ClassID</returns>
        public string AttributeForClassID()
        {
            return cmbClassId.Text;
        }

        /// <summary>
        /// Vrati nazov triedy, ktora sa ma priradit nacitanim BB, ak vrati -1 tak 
        /// nazov triedy nacitame z csv suboru, ktoru prestavuje hodnota classId v csv suboru.
        /// </summary>
        /// <returns>nazov triedy, ktora sa ma priradit nacitanim BB, ak vrati -1 tak 
        /// nazov triedy nacitame z csv suboru, ktoru prestavuje hodnota classId v csv suboru</returns>
        public string GetClassOfLoadBB()
        {
            if (cmbClassesBB.SelectedIndex == 0)
                return "-1";//nazov triedy nacitany zo suboru
            return cmbClassesBB.Text;

        }


        /// <summary>
        /// Nastavi uzivatelovi na vyber triedy pre BB, z ktorych ma moznost si vybrat
        /// ak vyberie moznost ClassId znamena to ze sa trieda BB priradi podla hodnoty
        /// ClassId v subore csv. Ak vyberie inu moznost tak nasledne musi nastavit
        /// akemu atributu danej triedy sa priradi hodnota ClassId zo suboru csv.
        /// </summary>
        private void SetClasses()
        {
            cmbClassesBB.Items.Clear();
            cmbClassesBB.Items.Add("Class ID");
            foreach (string cls in _myClasses.GetAllClasses())
            {
                cmbClassesBB.Items.Add(cls);
            }
            cmbClassesBB.SelectedIndex = 0;
        }

        /// <summary>
        /// Obsluzenie akcie kliknutia na tlacidko save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (cmbClassesBB.SelectedIndex == -1 || (cmbClassesBB.SelectedIndex != 0 && cmbClassId.SelectedIndex == -1))
            {
                MessageBox.Show("Neboli korektne zvolene nastavenia. Doplnte udaje prosim.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Spristupnenie/zakazanie nastavovania atributu triedy ktory sa naplni hodnotou podla ClassID
        /// ak je zakazany tak hodnotou classID sa nastavi trieda BB.
        /// </summary>
        /// <param name="enable"></param>
        private void EnableClassId(bool enable)
        {
            lblClassId.Enabled = enable;
            cmbClassId.Enabled = enable;
        }

        /// <summary>
        /// Uzivatel zmenil triedu pre nacitavane BBs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedClassBBsChanged(object sender, EventArgs e)
        {
            if (cmbClassesBB.SelectedIndex == -1) return;
            
            if (cmbClassesBB.SelectedIndex == 0)
            {
                InitClasses();
                return;
            }

            EnableClassId(true);
            cmbClassId.Items.Clear();
            foreach (string prop in _myClasses.GetPropsForClass(cmbClassesBB.Text))
            {
                cmbClassId.Items.Add(prop);
            }
            if (cmbClassId.Items.Count>0) cmbClassId.SelectedIndex = 0;
        }

        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Je potrebné vybrať konkrétny formá csv: Csv2Point(CSV), CsvPointSize(CSV).\n" +
                            "Struktura Csv2Point:\n" +
                            "image_path;left_top_x;left_top_y; right_bottom_x;right_bottom_y;classid\n\n" +
                            "Struktura CsvPointSize:\n" +
                            "image_path;left_top_x;left_top_y;width;height;classid\n\n" +
                            "Pre viac info týchto formátoch pozri HELP->Tutorial sekcia Ulozenie projektu\n\n" + 
                            "Pre viac info o nacitani pozri HELP->Tutorial sekcia Nacitanie projektu\n\n"+
                            "Ďalej je potrebné zadefinovať triedu boundingboxom, ktoré sa budú načítavať " +
                            "z csv formátu(volba: Class Of Boundingboxes), ak si vyberie moznost ClassId\n" +
                            "znamena to, ze sa trieda boundingBoxu priradi podla hodnoty ClassId v subore csv.\n" +
                            "Ak sa vyberie iná hodnotu pre triedu boundingboxu, následné je potrebné nastaviť \n" +
                            "akemu atributu danej triedy sa priradi hodnota ClassId zo suboru csv (volba: ClassID).",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// definovanie typu csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbFormatTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            CsvFormat = (EImportExportFormat) cmbFormatType.SelectedItem;
        }

    }
}
