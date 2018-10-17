using System;
using System.Windows.Forms;
using Projekt.Enums;
using Projekt.Forms;
using Projekt.Interfaces;
using Projekt.SettingOfTransformation;

namespace Projekt.UserControls
{
    /// <summary>
    /// Nastavenie konkrétneho typu generovania umelych dat (rotacia)
    /// a nastavenie typu rotacie (horizontalny alebo vertikalny smer)
    /// </summary>
    public partial class SettingArticifialDataReflection : UserControl, IBaseUseControlArtifSeting
    {
        /// <summary>
        /// True ak uzivatel vybral horizontalne zrkadlenie inak false
        /// </summary>
        public bool Horizontal { get; private set; }

        /// <summary>
        /// true ak uzivatel vybral vertikalne zrkadlenie inak false
        /// </summary>
        public bool Vertical { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SettingArticifialDataReflection(ETransformType typeTransformation)
        {
            TypeTransformation = typeTransformation;
            InitializeComponent();
            Horizontal = chbHorizont.Checked;
            Vertical = chbVertic.Checked;
        }

        /// <summary>
        /// Typ transformacie, vid. ETransformType
        /// </summary>
        public ETransformType TypeTransformation { get; private set; }

        /// <summary>
        /// Nastavenie nastaveni pre zrkadlenie
        /// </summary>
        /// <param name="setting">nastavenia ktore sa maju nastavit</param>
        /// <returns>true ak OK inak false</returns>
        public bool SetSetting(SettingArtifTransfBase setting)
        {
            try
            {
                var sett = setting as SettingArtifTransfRefl;
                if (sett == null) return false;
                chbHorizont.Checked = sett.Horizontal;
                chbVertic.Checked = sett.Vertical;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  Vratenie parametrov danej volby.
        /// Ak uzivatel zle vyplnil tak vrati null;
        /// </summary>
        /// <returns>nastavenia</returns>
        public SettingArtifTransfBase GetSetting()
        {
            //ak uzivatel nevyplnil tuto volbu tak vrati null
            if (!Horizontal && !Vertical) return null;

            return new SettingArtifTransfRefl(ETransformType.Reflection, Horizontal, Vertical);
        }

        /// <summary>
        /// Kontrola zadefinovanych nastaveni
        /// </summary>
        /// <returns>true ak OK inak false</returns>
        public bool CheckUserDefinedSetting()
        {
            SettingArtifTransfBase setting = GetSetting();

            //ak uzivatel nevyplnil udaje pre dane nastavenie
            if (setting == null)
            {
                MessageBox.Show("Zle vyplnené nastavenia pre generovanie umelých dát.", "Error: generovanie umelých dát - voľba zrkadlenie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return setting.CheckValue();
        }

        /// <summary>
        /// Po kliknuti na toto tlacidlo sa odstrani z parenta a disposne usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemoveSettingClick(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }

        /// <summary>
        /// Uzivatel klikol na volbu horizontal(ak oznacil treba volbu vertikal zrusit ak je oznacena)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckChangeHorizont(object sender, EventArgs e)
        {
            Horizontal = chbHorizont.Checked;
            if (!Horizontal && !Vertical) chbVertic.Checked = true;
        }

        /// <summary>
        /// Uzivatel klikol na volbu vertical(ak oznacil treba volbu horizontal zrusit ak je oznacena)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckChangeVertical(object sender, EventArgs e)
        {
            Vertical = chbVertic.Checked;
            if (!Vertical && !Horizontal) chbHorizont.Checked = true;
        }

        private void BtnPreviewClick(object sender, EventArgs e)
        {
            SettingArtifTransfBase setting;
            if ((setting = GetSetting()) == null)
            {
                MessageBox.Show("Lutujem no musite zadat korektné hodnoty pre generovanie.", "Error - Preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var createImageFromDrawObj = this.ParentForm as CreateImageFromDrawObj;
                if (createImageFromDrawObj != null)
                {
                    var preview = new PreviewForImageDravObj(createImageFromDrawObj.OpenImages, createImageFromDrawObj.DrawObjType, setting, createImageFromDrawObj.GetSizeForPolylinePreview());

                    preview.ShowDialog();
                }
                else throw new Exception("Parent form is unknown.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyva pri vytvarani nahladu."+ex.Message, "Error - preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
    }
}
