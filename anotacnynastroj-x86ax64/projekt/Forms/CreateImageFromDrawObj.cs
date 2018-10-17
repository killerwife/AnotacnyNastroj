using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Projekt.DrawObjects.Extensions;
using Projekt.Enums;
using Projekt.Figure;
using Projekt.Interfaces;
using Projekt.SettingOfTransformation;
using Projekt.UserControls;

namespace Projekt.Forms
{
    /// <summary>
    /// Form pre uzivatelske rozhranie generovania dat.
    /// </summary>
    public partial class CreateImageFromDrawObj : Form
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
        /// Velkost kroku pri generovani dat z PA
        /// </summary>
        public int StepLength
        {
            get
            {
                try
                {
                    return tbStepLength.Text.Any() ? Convert.ToInt32(tbStepLength.Text) : 0;
                }
                catch (Exception)
                {

                    return 0;
                }
            }
        }

        /// <summary>
        /// Urcuje pre aky typ draw objektu sa idu generovat data BB - 0,PL - 1
        /// </summary>
        public int DrawObjType { get; private set; }

        /// <summary>
        /// Adresar kde budu ulozene obrazky.
        /// </summary>
        public string FolderToSave { get; set; }

        /// <summary>
        /// Domovsky adresar projektu.
        /// </summary>
        public string HomeFolder { get; set; }

        /// <summary>
        /// Percentualne rozdelenie generovanych dat do 3 skupin
        /// trenovacia, validacna, testovacia.
        /// </summary>
        public SortedList<int, MyDistrType> PercentDistribution { get; set; }

        /// <summary>
        /// Uzivatelom definovany pomer. (vo forme sirka : vyska)
        /// </summary>
        private List<int> _scale;
     
        /// <summary>
        /// Potrebujem vediet ci sa naposledy menila vyska,pomer alebo sirka, aky som vedel co sa ma automaticky doplnat.
        /// Potrebujem si pamatat dve zmeny, poslednu zmenu a zmenu pred nou ktora bola odlina.[0]-posledna zmena; [1]-predposledna zmena
        /// hodnota 1 ak sa menil pomer.
        /// hodnota 2 ak sa menila sirka.
        /// hodnota 3 ak sa menila vyska.
        /// </summary>
        private int[] _lastChanges;

        private SettingForFolders _settingForFolds;

        /// <summary>
        /// True ak chceme generovat realne data.
        /// </summary>
        public bool GenRealData { get; private set; }

        /// <summary>
        /// True ak chceme generovat umele data.
        /// </summary>
        public bool GenArtificialData { get; private set; }

        /// <summary>
        /// Nastavenia pre adresarovu strukturu
        /// </summary>
        public SettingForFolders SettingForFolds
        {
            get { return _settingForFolds; }
            set 
            { 
                _settingForFolds = value;
                if (_settingForFolds != null) tbDirStruct.Text = _settingForFolds.ToString();
            }
        }

        /// <summary>
        /// Urcuje format v akom sa maju ulozit vystupne obrazky
        /// </summary>
        public EImageFormat OutputImageFormat { get; set; }

        /// <summary>
        /// true ak chceme generovat umele testovacie data, inak false
        /// </summary>
        public bool ArtificTesting { get; private set; }

        /// <summary>
        /// true ak chceme generovat umele validacne data, inak false
        /// </summary>
        public bool ArtificValidation { get; private set; }

        /// <summary>
        /// true ak chceme generovat umele treningove data, inak false
        /// </summary>
        public bool ArtificTraining { get; private set; }

        /// <summary>
        /// Zoznam obrazkov v aktualnom projekte
        /// </summary>
        public List<BaseFigure> OpenImages { get; private set; }

        /// <summary>
        /// Zoznam nastaveni pre generovanie umelych dat
        /// </summary>
        public List<SettingArtifTransfBase> SettingArtifTransf { get; private set; }

        /// <summary>
        /// Konstruktor, typeDrawObj urcuje pre ktory objekt sa idu generovat data: 0-BB, 1-PL
        /// </summary>
        /// <param name="openImages">zoznam obrazkov v projekte</param>
        /// <param name="folderToSave">projekt folder</param>
        /// <param name="drawObjType"> typ objektu pre ktory sa idu generovat data: 0-BB, 1-PL</param>
        public CreateImageFromDrawObj(List<BaseFigure> openImages, string folderToSave, int drawObjType)
        {
            OpenImages = openImages;
            FolderToSave = HomeFolder = folderToSave;
            DrawObjType = drawObjType;

            InitializeComponent();

            cmbOutputFormat.DataSource = Enum.GetValues(typeof(EImageFormat));
            _lastChanges = new int[]{-1, -1};

            tbScale.LostFocus += delegate { 
                if (tbScale.Modified)
                {
                    if (_lastChanges[0] != 1) _lastChanges[1] = _lastChanges[0];
                    _lastChanges[0] = 1;
                }
                tbScale.Modified = false;
            };

            tbWidth.LostFocus += delegate { 
                if (tbWidth.Modified)
                {
                    if (_lastChanges[0] != 2) _lastChanges[1] = _lastChanges[0];
                    _lastChanges[0] = 2;
                }
                tbWidth.Modified = false;
            };

            tbHeight.LostFocus += delegate { 
                if (tbHeight.Modified)
                {
                    if (_lastChanges[0] != 3) _lastChanges[1] = _lastChanges[0];
                    _lastChanges[0] = 3;
                }
                tbHeight.Modified = false;
            };

            tbFolderToSave.Text = FolderToSave;
            ImageWidth = 0;
            ImageHeight = 0;
            _scale = new List<int>();
            PercentDistribution = new SortedList<int, MyDistrType>(new ComparePercent());

            SettingArtifTransf = new List<SettingArtifTransfBase>();
            SettingForFolds = null;
            GenArtificialData = chbGenArtificialData.Checked;
            gbGenArtificialData.Enabled = GenArtificialData;
            GenRealData = chbGenRealData.Checked;
            ArtificTraining = chbTraining.Checked;
            ArtificValidation = chbValidation.Checked;
            ArtificTesting = chbTesting.Checked;
        }

        /// <summary>
        /// Vytvorilo sa okno potrebujem ho nastavit podla typu draw objectu pre ktory sa idu generovat data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadForm(object sender, EventArgs e)
        {
            switch (DrawObjType)
            {
                case 0://BB
                    this.Text += " BoundingBoxes";
                    FolderToSave += "\\BoundingBoxes\\";
                    break;
                case 1://PL
                    this.Text += " Polylines";
                    chbUnnormalized.Visible = false;
                    gpStepLength.Visible = true;
                    gbNormalization.Text = "Size Of Rectangle (Window)";
                    FolderToSave += "\\Polylines\\";
                    break;
            }
        }

        /// <summary>
        /// Obsluha tlacitka create.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateClick(object sender, EventArgs e)
        {
            if (!CheckDefineValueForGen())
                return;
            SaveGenSettingToFile(FolderToSave + String.Format("{0:ddMMHHmmssss}.gsf", DateTime.Now));
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Kontrola vsetkych nastaveni ktore zadefinoval uzivatel pre generovanie umelych dat
        /// </summary>
        /// <returns></returns>
        private bool CheckDefineValueForGen()
        {
            if (SettingForFolds == null)
            {
                MessageBox.Show("Musia byť zadefinované nastavenia pre vytvorenie adresárovej štruktúry.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //kontrola ci uzivatel vybral aspon jednu moznost bud generovanie umelych alebo realnych dat alebo oboje
            //ak nevybral ziadnu tak chyba
            if (!GenArtificialData && !GenRealData)
            {
                MessageBox.Show("Musí byť vybraná aspoň jedna voľba generovania dát - UMELÉ, SKUTOČNÉ", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //kontrola v pripade ze uzivatel vybral generovanie umelych dat, tak ci zvolil pre ktoru skupinu sa maju tieto data generovat
            if (GenArtificialData && !ArtificTesting && !ArtificTraining && !ArtificValidation)
            {
                MessageBox.Show("Musí byť vybraná aspoň jedna skupina pre generovanie umelých dát - TRAINING, VALIDATION, TESTING", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //ak generujeme data pre PL musi byt zadana hodnota kroku >0
            if (DrawObjType == 1)
            {
                if (StepLength <= 0)
                {
                    MessageBox.Show("Musí byť zadaná veľkosť kroku > 0", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //teraz potrebujeme zistit ci sa budu data normalizovat
            //ak bez normalizacie tak netreba nacitavat nastavenia o normalizacii ale len data o percentualnom rozdeleni
            if (!Unnormalized())
            {
                bool[] values = new[] { SetScale(), SetWidth(), SetHeight() };
                if (values.Count(value => !value) >= 2)
                //ak dve a viac hodnot nebolo zadanych spravne(pomer alebo sirka alebo vyska)
                {
                    if (DrawObjType == 0) MessageBox.Show("Zle zadané hodnoty pre normalizáciu. Opakujte voľbu prosím.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (DrawObjType == 1) MessageBox.Show("Zle zadané hodnoty pre rozmery okna (rectangle). Opakujte voľbu prosím.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!values[0]) SetScaleFromWandH();  //nie je zadany pomer, tak ju vypocitame z vysky a sirky
                if (!values[1]) SetWidthFromHandS();  //nie je zadana sirka, tak ju vypocitame z pomeru a vysky
                if (!values[2]) SetHeightFromWandS(); //nie je zadana vyska, tak ju vypocitame z pomeru a sirky       
            }

            if (!GetPercentuageDistributon())
            {
                MessageBox.Show("Zle zadané hodnoty pre percentuálne rozdelenie. Opakujte voľbu prosím.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (PercentDistribution.Sum(keyValuePair => keyValuePair.Key) != 100)
            {
                MessageBox.Show("Zle zadané hodnoty pre percentuálne rozdelenie\n(Suma zadaných hodnôt pre trenovacie, validacne, testovacie sa musí rovnať 100).\nOpakujte voľbu prosím.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //potrebujeme zistit ake data sa maju generovat, ci umele, ci realne alebo oboje
            //ak sa maju generovat aj umele je potrebne ziskat nastavenia podla ktorych sa maju generovat
            if (GenArtificialData)
            {
                //nacitanie nastaveni pre generovanie umelých dát, vrati true ak data nacitane spravne inak false. 
                if (!SetSettingForGenArtifData())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Nacitanie uzivatelom zadefinovaných nastavení pre generovanie umelých dát.
        /// Vrati true ak prebehlo nacitanie spravne inak false.
        /// </summary>
        /// <returns>Vrati true ak prebehlo nacitanie spravne inak false.</returns>
        private bool SetSettingForGenArtifData()
        {
            //Treba vymazat nastavenia lebo ak boli uz nastavenia nacitavane a doslo k chybe
            //uzivatel bude tieto nastavenia opravovat a pri dalsom nacitani by sa znovu nacitali cize
            //by boli nacitane tolko krat kolko krat by uzivatel menil nastavenia
            ClearSettingForArtifData();
            foreach (var control in pnlSettingGenArtificialData.Controls)
            {
                if (control is IBaseUseControlArtifSeting)
                {
                    if (!(control as IBaseUseControlArtifSeting).CheckUserDefinedSetting())
                        return false;
                    SettingArtifTransf.Add((control as IBaseUseControlArtifSeting).GetSetting());
                }
            }
            //chceme generovat umele data no nebolo nic vyplnene
            if (!SettingArtifTransf.Any())
            {
                MessageBox.Show("Neboli vybrané žiadne nastavenia pre generovanie umelých dát.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// vymazanie nastaveni pre generovanie umelych dat
        /// </summary>
        private void ClearSettingForArtifData()
        {
            SettingArtifTransf.Clear();
        }

        /// <summary>
        /// Ziskanie uzivatelom zadefinovaneho percentualneho rozdelenia vygenerovanych dat na trenovacie, validacne, testovacie.
        /// </summary>
        /// <returns></returns>
        private bool GetPercentuageDistributon()
        {
            PercentDistribution.Clear();
            try
            {
                PercentDistribution.Add(tbTraining.Text == "" ? 0 : Convert.ToInt32(tbTraining.Text), new MyDistrType(0, "Training", ArtificTraining));
                PercentDistribution.Add(tbValidation.Text == "" ? 0 : Convert.ToInt32(tbValidation.Text), new MyDistrType(1, "Validation", ArtificValidation));
                PercentDistribution.Add(tbTesting.Text == "" ? 0 : Convert.ToInt32(tbTesting.Text), new MyDistrType(2, "Testing", ArtificTesting));
            }
            catch (Exception)
            {
                PercentDistribution.Clear();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Nastavenie uzivatelom zadefinovanej sirky.
        /// </summary>
        /// <returns></returns>
        private bool SetWidth()
        {
            try{
                ImageWidth = Convert.ToInt32(tbWidth.Text);
                if (ImageWidth <= 0) return false;
             }catch (Exception)
            {
                ImageWidth = 0;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Nastavenie uzivatelom zadefinovanej vysky.
        /// </summary>
        /// <returns></returns>
        private bool SetHeight()
        {
            try{
                ImageHeight = Convert.ToInt32(tbHeight.Text);
                if (ImageHeight <= 0) return false;
            }catch (Exception)
            {
                ImageHeight = 0;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Nastavenie uzivatelom zadaneho pomeru.
        /// </summary>
        /// <returns></returns>
        private bool SetScale()
        {
            _scale.Clear();
            string[] pomer = tbScale.Text.Split(':');
            if (pomer.Count() != 2) return false;
            try
            {
                _scale.Add(Convert.ToInt32(pomer[0]));
                _scale.Add(Convert.ToInt32(pomer[1]));

                foreach (int pom in _scale)
                {
                    if (pom <= 0) return false;
                }
            }
            catch (Exception)
            {
                _scale.Clear();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Nacita velkost ak je zadefinovana
        /// </summary>
        /// <returns>velkost ktora sa ma pouzit pre nahlady polyline</returns>
        public Size GetSizeForPolylinePreview()
        {
            try
            {
                return new Size(Convert.ToInt32(tbWidth.Text), Convert.ToInt32(tbHeight.Text)) == default(Size) ? new Size(10, 10) : new Size(Convert.ToInt32(tbWidth.Text), Convert.ToInt32(tbHeight.Text));
            }
            catch (Exception)
            {
                return new Size(10,10);
            }
            
        }

        /// <summary>
        /// Vrati velkost akej maju byt BoundingBoxy (obrazky ktore vzniknu vyrezanim z boundingBoxov) po normaizacii.
        /// </summary>
        /// <returns>Vrati velkost akej maju byt BoundingBoxy (obrazky ktore vzniknu vyrezanim z boundingBoxov) po normaizacii</returns>
        public Size GetSize()
        {
            return new Size(ImageWidth, ImageHeight);
        }

        /// <summary>
        /// Ak nemaju byt velkosti BB pred ulozenim znormalizovane tak vrati true inak false.
        /// </summary>
        /// <returns>Ak nemaju byt velkosti BB pred ulozenim znormalizovane tak vrati true inak false</returns>
        public bool Unnormalized()
        {
            return chbUnnormalized.Checked;
        }

        /// <summary>
        /// Vrati pomer v akom maju byt obrazky ulozene.
        /// </summary>
        /// <returns>zoznam s dvoma prvkami: prvy je width(sirka), druhy je height(vyska)</returns>
        public List<int> GetScale()
        {
            return _scale;
        } 

        /// <summary>
        /// Nastavenie pripustnych hodnot ktore mozu byt zadane do textboxu pre sirku a vysku a taktiez pre textboxy kde
        /// sa zadavaja percentualne rozdelenie dat a aj pre velkost kroku.
        /// Pripustne su integer hodnoty. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckValueIntPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back || e.KeyChar == (char)46; //46 je bodka;
        }

        /// <summary>
        /// Nastavenie pripustnych hodnot ktore mozu byt zadane do textboxu pre pomer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckValueScalePress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)58 || e.KeyChar == (char)46; //46 je bodka; 58 je dvojbodka
            
        }

        /// <summary>
        /// Uzivatel zadefinoval vysku a pomer(sirka : vyske), je potrebne nastavit sirku.
        /// Z pomeru vieme zavislost sirky na vyske.
        /// </summary>
        private void SetWidthFromHandS()
        {
            ImageWidth = (int)Math.Round((ImageHeight * _scale[0]) / (double)_scale[1]);
            tbWidth.Text = ImageWidth + "";
        }

        /// <summary>
        /// Uzivatel zadefinoval sirku a pomer(sirka : vyske), je potrebne nastavit vysku.
        /// Z pomeru vieme zavislost vysky na sirke.
        /// </summary>
        private void SetHeightFromWandS()
        {
            ImageHeight = (int)Math.Round((ImageWidth * _scale[1]) / (double)_scale[0]);
            tbHeight.Text = ImageHeight + "";
        }

        /// <summary>
        /// Uzivatel zadefinoval sirku a vysku, je potrebne nastaviť pomer.
        /// Pomer(sirka : vyske).
        /// </summary>
        private void SetScaleFromWandH()
        {
            //najdem najmensieho spolocneho delitela
            int pom, w = ImageWidth, h = ImageHeight;

            while (h != 0)
            {
                pom = w % h;
                w = h;
                h = pom;
            }
            _scale.Clear();
            _scale.Add(ImageWidth / w);
            _scale.Add(ImageHeight / w);
            tbScale.Text = _scale[0] + ":" + _scale[1];
        }

        /// <summary>
        /// Zrusenie okna po praci s nim.
        /// </summary>
        public void MyClosed()
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(MyClosed));
                else
                {
                    this.Close();
                }
            }
            catch (ObjectDisposedException) { }
        }

        /// <summary>
        /// Po oznaceni chceme aby boli BB ulozene bez normalizacie preto nastavime
        /// nastavenia normalizacie tak aby sa nedali zadavat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChbUnnormalizedCheckedChanged(object sender, EventArgs e)
        {
            gbNormalization.Enabled = !Unnormalized();
        }

        /// <summary>
        /// Po oznaceni chceme aby sa generovali aj umele data preto povolíme nastavavenia pre generovanie
        /// umelych dat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckChangeArticifialGen(object sender, EventArgs e)
        {
            GenArtificialData = chbGenArtificialData.Checked;
            gbGenArtificialData.Enabled = GenArtificialData;
            if (!GenArtificialData) chbGenRealData.Checked = true;
        }

        /// <summary>
        /// Po oznaceni chceme aby sa generovali aj realne data preto povolime generovanie
        /// umelych dat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckChangeRealGen(object sender, EventArgs e)
        {
            GenRealData = chbGenRealData.Checked;
            if (!GenRealData) chbGenArtificialData.Checked = true;
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
                tbFolderToSave.Text = FolderToSave;
            }
        }

        /// <summary>
        /// Vrati rectangle podla uzivatelom zadefinovanej velkosti
        /// </summary>
        /// <returns>rectangle podla uzivatelom zadefinovanej velkosti</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(0, 0, ImageWidth, ImageHeight);
        }

        /// <summary>
        /// Nastavovanie hodnot pomeru podla zadavania uzivatela a nastavenie ostatnych parametrov.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetScaleFromUser(object sender, KeyEventArgs e)
        {
            if(!SetScale())return;
            SetOtherFromScale(_lastChanges[0] == 1 ? _lastChanges[1] : _lastChanges[0]);
        }

        /// <summary>
        /// Nastavenie ostatnych parametrov podla pomeru a posledneho meneneho parametra.
        /// </summary>
        /// <param name="change"></param>
        private void SetOtherFromScale(int change)
        {
            if (tbWidth.Text.Any() && change == 2)
                SetHeightFromWandS();
            else if (tbHeight.Text.Any() && change == 3)
                SetWidthFromHandS();
        }

        /// <summary>
        /// Nastavovanie hodnoty sirky podla zadavania uzivatela a nastavenie ostatnych parametrov.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetWidthFromUser(object sender, KeyEventArgs e)
        {
            if(!SetWidth())return;
            SetOtherFromWidth(_lastChanges[0] == 2 ? _lastChanges[1] : _lastChanges[0]);
        }

        /// <summary>
        /// Nastavenie ostatnych parametrov podla sirky a posledneho meneneho parametra.
        /// </summary>
        /// <param name="change"></param>
        private void SetOtherFromWidth(int change)
        {
            if (_scale.Any() && change == 1)
                SetHeightFromWandS();
            else if (tbHeight.Text.Any() && change == 3)
                SetScaleFromWandH();
        }

        /// <summary>
        /// Nastavovanie hodnoty vysky podla zadavania uzivatela a nastavenie ostatnych parametrov.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetHeightFromUser(object sender, KeyEventArgs e)
        {
            if (!SetHeight()) return;
            SetOtherFromHeight(_lastChanges[0] == 3 ? _lastChanges[1] : _lastChanges[0]);
        }

        /// <summary>
        /// Nastavenie ostatnych parametrov podla vysky a posledneho meneneho parametra.
        /// </summary>
        /// <param name="change"></param>
        private void SetOtherFromHeight(int change)
        {
            if (_scale.Any() && change == 1)
                SetWidthFromHandS();
            else if (tbWidth.Text.Any() && change == 2)
                SetScaleFromWandH();
        }

        /// <summary>
        /// Trieda vdaka ktorej viem utriedit front udalosti podla 
        /// casu kedy sa maju vykonat.
        /// </summary>
        private class ComparePercent : IComparer<int>
        {
            public int Compare(int time1, int time2)
            {
                if (time1 < time2)
                    return -1;
                return 1;
            }
        }

        private void CheckChangedTraining(object sender, EventArgs e)
        {
            ArtificTraining = chbTraining.Checked;
        }

        private void CheckChangedValidation(object sender, EventArgs e)
        {
            ArtificValidation = chbValidation.Checked;
        }

        private void CheckChangedTesting(object sender, EventArgs e)
        {
            ArtificTesting = chbTesting.Checked;
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - POSUN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddMoveSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.Move);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - ROTACIA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddRotateSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.Rotate);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - ZRKADLENIE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddReflectionSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialDataReflection(ETransformType.Reflection);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - ZMENA VELKOSTI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddScaleSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.Scale);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - ADITIVNY SUM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddAdditiveNoiseSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.AdditiveNoise);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - INVERZNY SUM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddImpulseNoiseSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.ImpulseNoise);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie nastavenia generovania umelých dát - ROZMAZANIE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddBlurringSettingClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialData(ETransformType.Blurring);
            AddSetingOnPanel(component);
        }

        private void BtnAddSharpenClick(object sender, EventArgs e)
        {
            var component = new SettingArticifialDataEmpty(ETransformType.Sharpen);
            AddSetingOnPanel(component);
        }

        /// <summary>
        /// Pridanie konkternych nastaveny na panel
        /// </summary>
        /// <param name="component"></param>
        private void AddSetingOnPanel(UserControl component)
        {
            pnlSettingGenArtificialData.VerticalScroll.Value = 0;
            component.Location = new Point(0, pnlSettingGenArtificialData.Controls.Count * 35);
            pnlSettingGenArtificialData.Controls.Add(component);
        }

        /// <summary>
        /// Pomoc pre pridavanie transformacii
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelpClick(object sender, EventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help\help.html");
            }
            catch (Exception exc)
            {
                MessageBox.Show("CreateImageFromDrawObj.BtnHelpClick()","Error: "+exc, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
                /*
            MessageBox.Show("Hodnota pre rozsah (From-To) zalezi od typu transformacie\n" +
                            "(posun -  moze byt len kladne cislo, rotacia - neobmedzena\n" +
                            "hodnota, velkost - v percentach cize -99 az 100), pricom\n" +
                            "From musi byt vzdy mensie ako To. Pri posune(move) sa z\n" +
                            "daneho rozsahu generuju nahodne velkosti vektorov podla,\n" +
                            "ktorych sa nasledne posuva obrazok v BB. Pri rotacii(rotate)\n" +
                            "sa z daneho rozsahu generuju nahodne hodnoty stupnou, podla\n" +
                            "ktorych sa natoci obrazok v BB. Pri zmene velkosti(scale)\n sa" +
                            "z daneho rozsahu generuju hodnoty(scale faktor) podla ktorych sa\n" +
                            "ma obrazok v BB zvacsit(hodnoty>100)/zmensit(0<hodnoty<100)\n" +
                            "Hodnota kroku (Step) urcuje ako sa maju menit hodnoty z\n" +
                            "rozsahu (From-To).\n" +
                            "Hodnota pocet (Count) urcuje kolko obrazkov sa ma vygenerovat\n" +
                            "z daneho rozsahu a pri danom kroku.", 
                            "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zaskrtnutie pre ktoru skupinu sa maju generovat umele data.\n" +
                            "Avsak to ci budu umele data pre jednotlive skupiny vygenerovane\n" +
                            "urcuje aj percentualne rozdelenie.\n" +
                            "Priklad:\n " +
                            "Cize ak je percentualne rozdelenie(100%/0%/0%)(trening-validacia-test)\n" +
                            "tak umele data mozu byt generovane len pre treningovu cast.Aj po\n" +
                            "zaskrtnuti inych casti sa pre ne nevygeneruju umele data, lebo\n" +
                            "percentuálne rozdelenie hovorí, že pre tieto casti nemajú byt vytvorené ziadne data.\n", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveGenSettingToFile(string fileToSave)
        {
            try
            {
               /* if (File.Exists(fileToSave))
                { 
                    File.Delete(fileToSave);
                }
                else // ak existoval subor netreba kontrolovat ci existje zlozka
                {*/
                    var folder = fileToSave.Substring(0, fileToSave.Length - fileToSave.Split(new[] { '\\', '/' }).Last().Length);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                //}
                var formater = new BinaryFormatter();
                using (var stream = new FileStream(fileToSave, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    formater.Serialize(stream, new SerializeSetting(this));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Problem pri ukladani nastaveni: " + exc.Message, "Error - CreateImageFromDrawObj - SaveGenSettingToFile()", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
            }
        }

        private void BtnSaveSettingClick(object sender, EventArgs e)
        {
            if (!CheckDefineValueForGen())
                return;

            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = FolderToSave;
            sfd.Filter ="Files with generate settings(*.gsf)|*.gsf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveGenSettingToFile(sfd.FileName);
            }
        }

        private void SettingRemovedFromPanel(object sender, ControlEventArgs e)
        {
            try
            {
                var controls = (sender as Panel).Controls;
                if (controls.Count == 0) return;
                for (int i = 0; i < controls.Count; i++)
                {
                    controls[i].Location = new Point(0, i * 35); ;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error-CreateImageFromDrawObj.SettingRemovedFromControl", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void OutputFormatChanged(object sender, EventArgs e)
        {
            OutputImageFormat = (EImageFormat)cmbOutputFormat.SelectedItem;
        }

        private void BtnLoadSettingClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            
            ofd.Filter = "Generate Setting File(*.GSF)|*GSF";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                try
                {
                    var formater = new BinaryFormatter();
                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        var setting = (SerializeSetting)formater.Deserialize(stream);
                        tbFolderToSave.Text = FolderToSave = Directory.Exists(setting.FolderToSave) ? setting.FolderToSave : FolderToSave;
                        SettingForFolds = setting.SettingForFolds;
                        chbGenRealData.Checked = setting.GenRealData;
                        chbGenArtificialData.Checked = setting.GenArtificialData;

                        tbTraining.Text = setting.TbTraining;
                        tbValidation.Text = setting.TbValidation;
                        tbTesting.Text = setting.TbTesting;

                        tbScale.Text = setting.Scale;
                        tbWidth.Text = setting.Width;
                        tbHeight.Text = setting.Height;

                        chbUnnormalized.Checked = (DrawObjType != 1) && setting.Unnormalized;
                        tbStepLength.Text = setting.StepLength + "";

                        chbTraining.Checked = setting.ArtificTraining;
                        chbTesting.Checked = setting.ArtificTesting;
                        chbValidation.Checked = setting.ArtificValidation;
                        cmbOutputFormat.SelectedItem = setting.OutputImageFormat;

                        pnlSettingGenArtificialData.Controls.Clear();
                        pnlSettingGenArtificialData.VerticalScroll.Value = 0;
                        foreach (var settingTransf in setting.SettingArtifTransf)
                        {
                            UserControl component = null;  
                            switch (settingTransf.TransformType)
                            {
                                case ETransformType.Rotate:
                                case ETransformType.Move:
                                case ETransformType.Scale:
                                case ETransformType.AdditiveNoise:
                                case ETransformType.ImpulseNoise:
                                case ETransformType.Blurring:
                                    component = new SettingArticifialData(settingTransf.TransformType);
                                    break;
                                case ETransformType.Reflection:
                                    component = new SettingArticifialDataReflection(ETransformType.Reflection);
                                    break;
                                case ETransformType.Sharpen:
                                    component = new SettingArticifialDataEmpty(ETransformType.Sharpen);
                                    break;
                            }
                            if (component == null) continue;
                            (component as IBaseUseControlArtifSeting).SetSetting(settingTransf);
                            component.Location = new Point(0, pnlSettingGenArtificialData.Controls.Count * 35);
                            pnlSettingGenArtificialData.Controls.Add(component);
                        }
                    }  
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Problem pri nacitavani nastaveni: " + exc.Message, "Error - CreateImageFromDrawObj - BtnLoadSettingClick()", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDefDirStruct(object sender, EventArgs e)
        {
            var bds = new CreateDirectoryStructureForm(SettingForFolds ?? new SettingForFolders());
            if (bds.ShowDialog() == DialogResult.OK)
            {
                SettingForFolds = bds.SettingForFolds;

            }
            bds.Dispose();
        }
     
        /// <summary>
        /// Ulozenie nastaveni
        /// </summary>
        [Serializable]
        internal class SerializeSetting
        {
            public SerializeSetting(CreateImageFromDrawObj crImgFrmDrObj)
            {
                FolderToSave = crImgFrmDrObj.FolderToSave;
                GenRealData = crImgFrmDrObj.GenRealData;
                GenArtificialData = crImgFrmDrObj.GenArtificialData;
                SettingForFolds = crImgFrmDrObj.SettingForFolds;
                OutputImageFormat = crImgFrmDrObj.OutputImageFormat;
                foreach (var distr in crImgFrmDrObj.PercentDistribution)
                {
                    switch (distr.Value.Type)
                    {
                        case 0:
                            TbTraining = distr.Key + "";
                            break;
                        case 1:
                            TbValidation = distr.Key + "";
                            break;
                        case 2:
                            TbTesting = distr.Key + "";
                            break;
                    }
                }

                Unnormalized = crImgFrmDrObj.Unnormalized();
                StepLength = crImgFrmDrObj.StepLength + "";

                Width = crImgFrmDrObj.ImageWidth + "";
                Height = crImgFrmDrObj.ImageHeight + "";
                if (crImgFrmDrObj.GetScale().Any())
                    Scale = crImgFrmDrObj.GetScale()[0] + ":" + crImgFrmDrObj.GetScale()[1];
                ArtificTraining = crImgFrmDrObj.ArtificTraining;
                ArtificValidation = crImgFrmDrObj.ArtificValidation;
                ArtificTesting = crImgFrmDrObj.ArtificTesting;

                SettingArtifTransf = crImgFrmDrObj.SettingArtifTransf;
            }
            public List<SettingArtifTransfBase> SettingArtifTransf { get; set; }

            public string FolderToSave { get; private set; }
            public bool GenRealData { get; private set; }
            public bool GenArtificialData { get; private set; }
            public SettingForFolders SettingForFolds { get; private set; }

            public string TbTesting { get; private set; }
            public string TbValidation { get; private set; }
            public string TbTraining { get; private set; }

            public string Scale { get; private set; }
            public string Width { get; private set; }
            public string Height { get; private set; }

            public bool Unnormalized { get; private set; }
            public string StepLength { get; private set; }

            public bool ArtificTraining { get; private set; }
            public bool ArtificTesting { get; private set; }
            public bool ArtificValidation { get; private set; }
            public EImageFormat OutputImageFormat { get; private set; }
        }
    }

    /// <summary>
    /// vytvorenie typu ktory bude sluzit na uchovanie skupiny pre ktoru sa generuju data,
    /// a taktiez uchovanie nastavenia ci sa maju pre tuto skupinu generovat data umele.
    /// </summary>
    public class MyDistrType
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="type">trenovacia - 0, validacna - 1 , testovacia - 2.</param>
        /// <param name="fName">nazov zlozky</param>
        /// <param name="generate">ak sa ma generovat true inak false</param>
        public MyDistrType(int type, string fName, bool generate)
        {
            Type = type;
            FolderName = fName;
            Generate = generate;
        }

        /// <summary>
        /// trenovacia - 0, validacna - 1 , testovacia - 2.
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        /// nazov zlozky
        /// </summary>
        public string FolderName { get; private set; }

        /// <summary>
        /// ak sa ma generovat true inak false
        /// </summary>
        public bool Generate { get; private set; }
    }
}
