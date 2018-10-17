using System;
using System.Linq;
using System.Windows.Forms;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca uzivatelske rozhranie pre nastanie atributov pre generovanie
    /// negativnych treningovych dat
    /// </summary>
    public partial class CreateNegativeTrainingData : Form
    {
        /// <summary>
        /// Uzivatelom definovana sirka.
        /// </summary>
        public int ImageWidth { get; private set; }

        /// <summary>
        /// Uzivatelom nastavena vyska.
        /// </summary>
        public int ImageHeight { get; private set; }

        /// <summary>
        /// Adresar kde budu ulozene obrazky.
        /// </summary>
        public string FolderToSave { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="folderToSave">Adresar kde maju byt ulozene vytvorene obrazky</param>
        public CreateNegativeTrainingData(string folderToSave)
        {
            InitializeComponent();
            FolderToSave = folderToSave + "\\TrainingData\\";
            tbFolder.Text = FolderToSave;
            ImageWidth = 0;
            ImageHeight = 0;
        }

        /// <summary>
        /// Nastavenie vysky a sirky pre obrazky
        /// </summary>
        /// <returns></returns>
        private bool SetHeightAndWidth()
        {
            //nacitame uzivatelom zadanu hodnotu pre vysku a sirku
            if (!tbHeight.Text.Any() || !tbWidth.Text.Any()) return false;

            try
            {
                ImageWidth = Convert.ToInt32(tbWidth.Text);
                ImageHeight = Convert.ToInt32(tbHeight.Text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Obsluha kliknutia na tlacidlo Create.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (!SetHeightAndWidth())
            {
                MessageBox.Show(this,"Boli zadané zlé vstupné hodnoty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Obsluha kliknutia na tlacidlo Change Folder.
        /// Ak chce uzivatel zmenit adresar kde sa uložia obrazky.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeFolderClick(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                FolderToSave = fbd.SelectedPath + "\\";
                tbFolder.Text = FolderToSave;
            }
        }

                /// <summary>
        /// Kontrola zadania pripustnych hodnot pre velkosti.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckValue(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)58 || e.KeyChar == (char)46; //46 je bodka; 58 je dvojbodka
        }
    }
}
