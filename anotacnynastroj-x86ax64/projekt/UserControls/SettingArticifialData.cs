using System;
using System.Windows.Forms;
using Projekt.Enums;
using Projekt.Forms;
using Projekt.Interfaces;
using Projekt.SettingOfTransformation;

namespace Projekt.UserControls
{
    /// <summary>
    /// Nastavenie konkrétneho typu generovania umelych dat (rotácia, zmena veľkosti)
    /// a nastavenie rozsahu, kroku a početnosti pre danu voľbu generovania
    /// </summary>
    public partial class SettingArticifialData : UserControl, IBaseUseControlArtifSeting
    {
        private readonly string _name;

        /// <summary>
        /// Urcuje o aky typ transformacie umelych dat sa jedna vid ETransformType
        /// </summary>
        public ETransformType TypeTransformation { get; private set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="type">urcuje o aky typ transformacie umelych dat sa jedna vid ETransformType</param>
        public SettingArticifialData(ETransformType type)
        {
            InitializeComponent();
            TypeTransformation = type;
            switch (TypeTransformation)
            {
                case ETransformType.Move:
                    _name = "Move";
                    lblName.Text = string.Format("{0} [vect]", _name);
                    break;
                case ETransformType.Rotate:
                    _name = "Rotate";
                    lblName.Text = string.Format("{0} [°]", _name);
                    break;
                case ETransformType.Scale:
                    _name = "Scale";
                    lblName.Text = string.Format("{0} [%]", _name);
                    break;
                case ETransformType.AdditiveNoise:
                    _name = "Additive";
                    lblName.Text = string.Format("{0} [%]", _name);
                    break;
                case ETransformType.ImpulseNoise:
                    _name = "Impulse";
                    lblName.Text = string.Format("{0} [%]", _name);
                    break;
                case ETransformType.Blurring:
                    _name = "Blurring";
                    lblName.Text = string.Format("{0}", _name);
                    break;
                default:
                    this.Dispose();
                    break;
            }
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
        /// Vratenie parametrov danej volby. Parametre su vratene ako pole [od, do, krok, pocetnost]
        /// Ak parametre zle vyplnene vrati null
        /// </summary>
        /// <returns></returns>
        public SettingArtifTransfBase GetSetting()
        {
            int from;
            int to;
            int step;
            int count;
            if (!Int32.TryParse(tbFrom.Text, out from) || !Int32.TryParse(tbTo.Text, out to) ||
                !Int32.TryParse(tbStep.Text, out step) || !Int32.TryParse(tbCount.Text, out count)) return null;
            
            switch (TypeTransformation)
            {
                case ETransformType.Rotate:
                case ETransformType.Move:
                case ETransformType.Scale:
                    return new SettingArtifTransfRoMoSc(TypeTransformation, from, to, step, count);
                case ETransformType.AdditiveNoise:
                case ETransformType.ImpulseNoise:
                    return new SettingArtifTransfNoise(TypeTransformation, from, to, step, count);
                case ETransformType.Blurring:
                    return new SettingArtifTransfBlur(TypeTransformation, from, to, step, count);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Kontrola ci uzivatelom zadane nastavenia su spravne
        /// vrati true ak je rozsah, krok a pocetnost zadana spravne
        /// </summary>
        /// <returns></returns>
        public bool CheckUserDefinedSetting()
        {
            SettingArtifTransfBase setting = GetSetting();
            
            //ak uzivatel nevyplnil udaje pre dane nastavenie
            if (setting == null)
            {
                MessageBox.Show("Zle vyplnené nastavenia pre generovanie umelých dát.", "Error: generovanie umelých dát - voľba " + _name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return setting.CheckValue();
        }

        /// <summary>
        /// len integer hodnota.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedSettingValue(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back || e.KeyChar == (char)46;
        }

        /// <summary>
        ///  Rozsah zalezi od typu (posun - kladne cislo, rotacia - neobmedzena, velkost - kladne cisla).
        /// Aditive impuls noise v % a rozsah 1-100
        ///  znamienko minus(char 45)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSettingValueFromTo(object sender, KeyPressEventArgs e)
        {
            switch (TypeTransformation)
            {
                case ETransformType.Move:
                case ETransformType.Scale:
                case ETransformType.AdditiveNoise:
                case ETransformType.ImpulseNoise:
                case ETransformType.Blurring:
                    e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back || e.KeyChar == (char)46;
                    break;
                case ETransformType.Rotate:
                    e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)45 || e.KeyChar == (char)46;
                    break;    
            }
        }

        /// <summary>
        /// Uzivatel klikol na preview chce vidiet nahlad pre dane nastavenia generovania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPreviewClick(object sender, EventArgs e)
        {
            if (!CheckUserDefinedSetting())
                return;

            try
            {
                SettingArtifTransfBase setting = GetSetting();
                var createImageFromDrawObj = this.ParentForm as CreateImageFromDrawObj;
                if (createImageFromDrawObj != null)
                {
                    var preview = new PreviewForImageDravObj(createImageFromDrawObj.OpenImages, createImageFromDrawObj.DrawObjType, setting, createImageFromDrawObj.GetSizeForPolylinePreview());

                    preview.ShowDialog(this.ParentForm);
                }
                else throw new Exception("Parent form is unknown.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyva pri vytvarani nahladu." + ex.Message, "Error - preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nastavenie nastaveni pre dany tym transformacie
        /// </summary>
        /// <param name="setting">nastavenia ktore sa maju nastavit</param>
        /// <returns>true ak OK inak false</returns>
        public bool SetSetting(SettingArtifTransfBase setting)
        {
            try
            {
                var sett = setting as SettingArtifTransf4Params;
                if (sett == null) return false;
                tbFrom.Text = sett.From + "";
                tbTo.Text = sett.To + "";
                tbStep.Text = sett.Step + "";
                tbCount.Text = sett.Count + "";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
