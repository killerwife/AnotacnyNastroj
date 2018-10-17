using System;
using System.Windows.Forms;
using Projekt.Enums;
using Projekt.Forms;
using Projekt.Interfaces;
using Projekt.SettingOfTransformation;

namespace Projekt.UserControls
{
    /// <summary>
    /// Usercontrol pretransformacie bez moznosti zadefionovania nastaveni
    /// </summary>
    public partial class SettingArticifialDataEmpty : UserControl, IBaseUseControlArtifSeting
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="typeTransformation">Typ transformacie, vid. ETransformType</param>
        public SettingArticifialDataEmpty(ETransformType typeTransformation)
        {
            TypeTransformation = typeTransformation;
            InitializeComponent();
        }

        private void BtnRemoveSettingClick(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }

        private void BtnPreviewClick(object sender, EventArgs e)
        {
            try
            {
                var createImageFromDrawObj = this.ParentForm as CreateImageFromDrawObj;
                if (createImageFromDrawObj != null)
                {
                    var preview = new PreviewForImageDravObj(createImageFromDrawObj.OpenImages,
                                                             createImageFromDrawObj.DrawObjType, GetSetting(), createImageFromDrawObj.GetSizeForPolylinePreview());

                    preview.ShowDialog();
                }
                else throw new Exception("Parent form is unknown.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyva pri vytvarani nahladu."+ex.Message, "Error - preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Typ transformacie, vid. ETransformType
        /// </summary>
        public ETransformType TypeTransformation { get; private set; }

        /// <summary>
        /// Nastavenie nastaveni pre dany tym transformacie
        /// </summary>
        /// <param name="setting">nastavenia ktore sa maju nastavit</param>
        /// <returns>true ak OK inak false</returns>
        public bool SetSetting(SettingArtifTransfBase setting)
        {
            return true;
        }

        /// <summary>
        ///  Vratenie parametrov danej volby.
        /// Ak uzivatel zle vyplnil tak vrati null;
        /// </summary>
        /// <returns>nastavenia</returns>
        public SettingArtifTransfBase GetSetting()
        {
            return new SettingArtifTransfSharpen(ETransformType.Sharpen);
        }

        /// <summary>
        /// Kontrola nastaveni
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public bool CheckUserDefinedSetting()
        {
            return true;
        }
    }
}
