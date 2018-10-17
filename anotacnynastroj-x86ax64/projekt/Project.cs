using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Projekt.DrawObjects;
using Projekt.Enums;
using Projekt.ExportImport;
using Projekt.Figure;
using Projekt.Forms;
using Projekt.Interfaces;

namespace Projekt
{
    /// <summary>
    /// Delegat pre vyhladavanie objektov
    /// </summary>
    /// <param name="propsToFind"></param>
    public delegate void FindObjectEventHandler(ObjectProperties propsToFind);

    /// <summary>
    /// Tirda reprezentujuca konktetny projekt s ktorym sa pracuje
    /// </summary>
    public class Project : IExternalUseProject
    {
        /// <summary>
        /// Referencia na hlavne okno aplikacie.
        /// </summary>
        private readonly MainWindowApplication _mainForm = null;

        private Thread _thread;        
        private string _type;
        private string _fileName;
         
        /// <summary>
        /// Pristup k triedam nastroja a ich atributom
        /// </summary>
        public ClassGenerator MyClasses { get; set; }

        /// <summary>
        /// Zlozka, v ktorej su ulozene data projektu.
        /// </summary>
        public string ProjectFolder { get; set; }

        /// <summary>
        /// Zoznam vsetkych nacitanych snimok.
        /// </summary>
        public List<BaseFigure> OpenImages { get; set; }

        /// <summary>
        /// Zvoleny snimok, s ktorym sa aktualne pracuje.
        /// </summary>
        public BaseFigure CurrentImage { get; set; }

        /// <summary>
        /// Vlasnosti podla ktorych sa ma vyhladavat, ak null tak sa nehlada
        /// </summary>
        public ObjectProperties FindingObjProps { get; set; }

        /// <summary>
        /// Vyhladane objekty
        /// </summary>
        public GraphicsPath GpFinding{ get; set; }

        /// <summary>
        /// Nastavenie pre dany projekt, bud defaultne alebo nacitane zo suboru
        /// </summary>
        public ProjectOptions ProjOptions { get; set; }

        /// <summary>
        /// Uzivatelom preddefinovane nastavenie hodnot atributov tried. Ci a ako sa ma vyuzit cesta k obrazku na automaticke doplnenie
        /// atributov tried, alebo maju byt atributy vyplnene defaultne bez ohladu na cestu k obrazku.
        /// Posledny prvok v zozname je nazov triedy ktorej sa maju automaticky doplnat atributy.
        /// </summary>
        private Dictionary<string,List<string>> _defaltValuesForClasses;

        private string _defaultClass;

        private PictureProperty _pictureProp;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="mainForm">hlavne okno aplikacie</param>
        /// <param name="projectFolder">adresar projektu</param>
        public Project(MainWindowApplication mainForm, string projectFolder)
        {
            _mainForm = mainForm;
            ProjectFolder = projectFolder;
            OpenImages = new List<BaseFigure>();
            _defaltValuesForClasses = new Dictionary<string, List<string>>();
            _defaultClass = null;
            FindingObjProps = null;
            GpFinding = new GraphicsPath();            
            _type = null;
            _fileName = null;
        }

        public void PrecedingFigure()
        {
            var indexOfFigure = OpenImages.IndexOf(CurrentImage) - 1;
            if (indexOfFigure >= 0)
            {
                for (int i = 0; i < OpenImages[indexOfFigure].BoundBoxes.Count; i++)
                {
                    CurrentImage.AddBoundingBox((BoundingBox)OpenImages[indexOfFigure].BoundBoxes[i].Clone());    
                }
            }           
        }

        /// <summary>
        /// Vytvorenie vseobecnych atributov o obrazku a pridanie na panel s tymito atributmi.
        /// </summary>
        /// <param name="pnlAttr">panel na ktory maju but pridane atributy</param>
        public void CreatePictureProperty(Panel pnlAttr)
        {
            string filename = @"Data\Setting\PictureProps.dat";
            if (!File.Exists(filename)) return;
            FileStream fs = null;
            StreamReader sr = null;
            var attributes = new List<string[]>();
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    attributes.Add(sr.ReadLine().Split(';'));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error-Project.CreatePictureProperty()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }

            _pictureProp = new PictureProperty(pnlAttr, attributes);
        }

        /// <summary>
        /// Zmena obrazka po kliknuti na miniaturu toho obrazku.
        /// </summary>
        /// <param name="image">obrazok ktory sa ma nastavit ako aktualny</param>
        public void ChangePicture(BaseFigure image)
        {
            if(CurrentImage != null)SavePictureProps();
            CurrentImage = image;
            SetPictureProperty();
            FindObjectOnImage();
        }

        /// <summary>
        /// Ulozenie atributov na urovni obrazka
        /// </summary>
        public void SavePictureProps()
        {
            if(_pictureProp != null)_pictureProp.SavePictureProps(CurrentImage);
        }

        /// <summary>
        /// Vyplnenie vseobecnych atributov obrazka na panely s tymito atributmi.
        /// Ak dany obrazok este nema definovane tieto vlastnosti tak sa mu nastavia 
        /// preddefinovane. Inak sa nastavia podla jeho vlastnosti.
        /// </summary>
        private void SetPictureProperty()
        {
            if (CurrentImage == null) return;
            if (CurrentImage.Properties == null) //nema def, nastavime preddefinovane
            {
                _pictureProp.SetUserDefine();
            }
            else
            {
                _pictureProp.SetPictureProps(CurrentImage.Properties);
            }
        }

        /// <summary>
        /// Import noveho obrazka pre anotaciu.
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public bool ImportImage()
        {
            string[] files = LoadImageName();
            if (files != null)
            {
                //potrebujeme zistit ci su obrazky zo zlozky projektu alebo nie, staci zistit u jedneho, ostatne budu na tom tak isto.
                var other = !files[0].StartsWith(ProjectFolder);
                Thread thread = new Thread(() => OpenImageThread(files, other));
                thread.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Nacitanie obrazku, ktory uz je ulozeny v zlozke projektu.
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public bool OpenImageFromProject()
        {
            string[] files = LoadImageName(ProjectFolder);
            if (files != null)
            {
                //_mainForm.SetProgressBar(0);
                Thread thread = new Thread(() => OpenImageThread(files, false));
                thread.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Nacitanie adries obrazkov ktore sa maju otvorit. Ak Je zadany loadFolder tak sa zadefinuje InitialDirectory
        /// pretoze sa idu otvarat obrazky z aktualneho adresara projektu a nejdu sa importovat.
        /// </summary>
        /// <param name="loadFolder">folder ktory sa ma zadefinovat ako initialDirectory</param>
        /// <returns></returns>
        private string[] LoadImageName(string loadFolder = null)
        {
            var ofd = new OpenFileDialog();
            if(loadFolder != null)ofd.InitialDirectory = loadFolder;
            ofd.Multiselect = true;
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF;*.TIF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF;*.TIF";
            ofd.CheckFileExists = false;

            //nacitanie adries vsetkych obrazkov
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileNames;
            }
            return null;
        }

        /// <summary>
        /// Nacitanie zadanich obrazkov do projektu. Ak je import true tak sa aj fyzicky kopiruju do projektu, ak false
        /// tak sa fyzicky nekopiruju lebo uz su v zlozke projektu.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="import"></param>
        private void OpenImageThread(string[] files, bool import)
        {
            int i = 1 + OpenImages.Count, max = files.Count() + OpenImages.Count;
            foreach (string file in files)
            {
                string imgRelAdr = import ? "\\" + file.Split(new[] { '/', '\\' }).Last() : file.Replace(ProjectFolder, "");

                try
                {
                    if (import && !file.Equals(ProjectFolder + imgRelAdr))
                        File.Copy(file, ProjectFolder + imgRelAdr, true);

                    BaseFigure image = new ImageFigure(ProjectFolder + imgRelAdr, ProjectFolder.Split(new[] { '/', '\\' }).ToList().Last() + imgRelAdr);
                    SetProgressBar((int)Math.Round(((double)i / max) * 100));
                    OpenImages.Add(image);
                    AddStatus(file, i, max, image);
                }
                catch (ArgumentException) { }
                i++;
            }
            if (OpenImages.Count > 0) SetSettingAfterLoaded("Images Loaded");

            if (_mainForm.InvokeRequired)
                _mainForm.Invoke((Action<int>)_mainForm.CreateActuallThumbs, CurrentImage == null ? 0 : OpenImages.IndexOf(CurrentImage));
            else _mainForm.CreateActuallThumbs(CurrentImage == null ? 0 : OpenImages.IndexOf(CurrentImage));
        }

        /// <summary>
        /// Import snimiek videa, (frame videa sa nakopiruju do zlozky projektu a umiestnia sa do zlozky s nazvom videa)
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public bool ImportVideo()
        {
            string file = LoadVideoName();
            if (file != null)
            {
                string folderForVideo = "\\" + Path.GetFileNameWithoutExtension(file);
                if (Directory.Exists(ProjectFolder + folderForVideo))//kontrola ci uz nie su data pre dane video v projekte vygenerovane
                {
                    MessageBox.Show(
                        "Lutujem no data pre video: " + file + ", uz existuju v projekte - zlozka: " + folderForVideo +
                        ".\n Video musi mat unikatny nazov.", "Error - Video nema unikatny nazov.", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                else Directory.CreateDirectory(ProjectFolder + folderForVideo);
                
                string path = string.Format("{0}\\{1}", folderForVideo, file.Split(new[] { '/', '\\' }).Last());//ziskame relativnu adresu videa vzhladom na zlozku projektu 
                File.Copy(file, ProjectFolder + path, true);//nakopirujeme video do zlozky s videom

                var progress = new ProgressForm();
                progress.SetName("Taking Frame From Video");

                Thread thread = new Thread(() => LoadFrameFromVideoThread(progress, path, ProjectFolder + folderForVideo));
                thread.Start();
                progress.Show(_mainForm);
                return true;
            }
            return false;
        }

        private string LoadVideoName(string loadFolder = null)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video Files(*.AVI;*.MPG;*.3GP;*.WMA;*.MP4)|*.AVI;*.MPG;*.3GP;*.WMA;*.MP4";
            if (loadFolder != null) ofd.InitialDirectory = loadFolder;

            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
        }

        private void LoadFrameFromVideoThread(ProgressForm progress, string relVideoAdr, string folderToSave)
        {
            try
            {
                _mainForm.ResetGraphicsPath();
                using (var capture = new Capture(ProjectFolder + relVideoAdr))
                {
                    var fps = (int)capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_FPS);
                    var frameCount = (int) capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                    int actualNumberFigureInProject = 1 + OpenImages.Count, max = frameCount + OpenImages.Count;
                    //capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_FRAMES, ++i); // pristup ku konkretnemu frame
                    var number = 1;

                    progress.Count = frameCount;
                    progress.Actuall = 0;
                    while(capture.Grab())
                    {
                        using (Image<Bgr, Byte> frame = capture.QueryFrame())
                        {
                            if(frame == null)continue;
                            string imageOfFrame = string.Format("{0}\\{1}.png", folderToSave, number);
                            frame.Save(imageOfFrame);
                            BaseFigure image = new FrameFigure(imageOfFrame, ProjectFolder.Split(new[] { '/', '\\' }).ToList().Last() + relVideoAdr, number++);
                            image.Properties = new List<string[]>();
                            OpenImages.Add(image);

                            if (progress.IsCanceled())
                            {
                                progress.MyClosed();
                                break;
                            }
                            progress.Actuall++;
                        }
                    }
                    progress.Count = progress.Actuall--;
                    progress.Actuall++;
                    CurrentImage = OpenImages.Last();
                    SetSettingAfterLoaded("Images Loaded");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error-Project.LoadFrameFromVideoThread()", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (_mainForm.InvokeRequired)
                _mainForm.Invoke((Action<int>)_mainForm.CreateActuallThumbs, CurrentImage == null ? 0 : OpenImages.IndexOf(CurrentImage));
            else _mainForm.CreateActuallThumbs(CurrentImage == null ? 0 : OpenImages.IndexOf(CurrentImage));
        }

        /// <summary>
        /// Nastavenie progress baru z vlakna
        /// </summary>
        /// <param name="i"></param>
        private void SetProgressBar(int i)
        {
            try
            {
                if (_mainForm.InvokeRequired)
                    _mainForm.Invoke((Action<int>)SetProgressBar, i);
                else
                {
                    _mainForm.SetProgressBar(i);
                }
            }
            catch (ObjectDisposedException) { }
        }

        /// <summary>
        /// Po skonceni nacitavania sa z vlakna zavola tato metoda a nastavi status a aktualny obrazok.
        /// </summary>
        /// <param name="status"></param>
        private void SetSettingAfterLoaded(string status)
        {
            if (_mainForm.InvokeRequired)
                _mainForm.Invoke((Action<string>)SetSettingAfterLoaded, status);
            else
            {
                _mainForm.SetImage2ImageBox(OpenImages.Last());
                _mainForm.SetStatusBar(OpenImages.Count, OpenImages.Count);
                _mainForm.SetStatusLoading(status);
                SetPictureProperty();
            }
        }

        /// <summary>
        /// Pri nacitavani obrazkov sa z vlakna vola tato metoda pre vykreslovanie thumbs a statusov.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="count"></param>
        /// <param name="image"></param>
        /// <param name="actuall"></param>
        private void AddStatus(string file, int actuall, int count, BaseFigure image)
        {
            try
            {
                if (_mainForm.InvokeRequired)
                    _mainForm.Invoke((Action<string, int, int, BaseFigure>)AddStatus, file, actuall, count, image);
                else
                {
                    CurrentImage = image;
                    _mainForm.SetStatusBar(actuall, count);
                    _mainForm.SetStatusLoading("Loading Images ...");
                }
            }
            catch (ArgumentException ex)
            {
                OpenImages.Remove(image);
                MessageBox.Show("Obrázok nenajdený: " + file, ex.Message);
            }
            catch (ObjectDisposedException)
            {
                if(_thread.IsAlive)_thread.Abort();
            }
        }

        /// <summary>
        /// Ulozenie projektu do xml/csv suboru
        /// </summary>
        public void SaveProjectAs()
        {
            var snof = new SetNameOutputFile(ProjectFolder, MyClasses);
            if (snof.ShowDialog() == DialogResult.OK)
            {
                ImportExport export = null;
                switch (snof.SaveFormat)
                {
                    case EImportExportFormat.Xml:
                        export = new ImportExportXML(OpenImages, ProjectFolder, false);
                        //GenerateXML(snof.GetOutputName());
                        _type = "xml";
                        _fileName = snof.GetOutputName();
                        break;
                    case EImportExportFormat.Csv2Point:
                        export = new ImportExportCSV(EImportExportFormat.Csv2Point, OpenImages, ProjectFolder, false);
                        //GenerateCSV(snof.GetOutputName(), snof.GetClassToSave(), snof.GetClassID());
                        break;
                    case EImportExportFormat.CsvPointSize:
                        export = new ImportExportCSV(EImportExportFormat.CsvPointSize, OpenImages, ProjectFolder, false);
                        break;
                    case EImportExportFormat.Xml2:
                        export = new ImportExportXML(OpenImages, ProjectFolder, true);
                        _type = "xml2";
                        _fileName = snof.GetOutputName();
                        break;
                }
                if (export != null)
                {
                    if (!export.ExportProject(snof))
                        MessageBox.Show("Ukladanie zlyhalo", "Error", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("Neznamy format suboru", "Error - Project.SaveProject()", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            } 
        }

        public void SaveImmediately()
        {
            if (_type != null)
            {
                ImportExportXML export = null;
                if (String.Compare(_type, "xml", false) == 0)
                {
                    export = new ImportExportXML(OpenImages, ProjectFolder, false);
                }
                else
                {
                    export = new ImportExportXML(OpenImages, ProjectFolder, true);    
                }
                if (!export.ExportProjectImmediately(_fileName))
                    MessageBox.Show("Ukladanie zlyhalo", "Error", MessageBoxButtons.OK);
            }
            else
            {
                SaveProjectAs();
            }
        }

        /// <summary>
        /// Otvorenie uz existujuceho suboru (Nacitanie z XML suboru, alebo z textoveho suboru/csv)
        /// </summary>
        /// <returns>true ak OK inak false</returns>
        public bool OpenProject()
        {
            string selectedPath = ProjectFolder; //cesta k xml/csv
            string fileType = ProjectFolder.Split(new[] { '/', '\\', '.' }).Last();
            
            var imagesSrc = new List<string>();
            var imagesAttrs = new List<List<string[]>>();
            var imagesDrawObjects = new List<List<DrawObject>>();

            var videosSrc = new List<string>();
            var framesAttrs = new List<List<List<string[]>>>();
            var framesDrawObjects = new List<List<List<DrawObject>>>();
            ImportExport import = null;
            //treba zistit ci je to xml alebo csv
            if (fileType.ToLower().Equals("xml")) //xml
            {
                import = new ImportExportXML(ProjectFolder);
            }
            else //csv
            {
                var cso = new SetCsvOptions(MyClasses);
                if (cso.ShowDialog() == DialogResult.OK)
                {
                    import = new ImportExportCSV(ProjectFolder, cso.CsvFormat, cso.GetClassOfLoadBB(), cso.AttributeForClassID());
                }
                else
                {
                    MessageBox.Show("Nacitavanie zrusene uzivatelom.", "Zrusenie nacitavania", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!import.ImportProject(ref imagesSrc, ref imagesAttrs, ref imagesDrawObjects, ref videosSrc,
                                         ref framesAttrs, ref framesDrawObjects))
                return false;

            if (import is ImportExportXML)
            {
                _fileName = ProjectFolder.Split(new[] { '/', '\\' }).Last();
                if (import.GetImpType() == 1)
                {
                    _type = "xml";
                }
                else
                {
                    _type = "xml2";
                }
            }
            //kontola ci nie je subor prazdny
            if (!imagesSrc.Any() && !videosSrc.Any()) return true;

            //teraz je potrebne zmenit cestu pre ProjectFolder, pretoze teraz je v nom cesta ku .xml/.csv
            //cesta k projektu je rovnaka ako cesta k xml/csv az na to ze nazov xml/csv z tejto cesty treba zamenit
            //s nazvom projektu a ten si vieme ziskat z .xml/csv suboru(prva zlozka v ceste k obrazkom v xml/csv).
            string nameProject = imagesSrc.Any() ? imagesSrc[0].Split(new[] { '/', '\\' }).First() : videosSrc[0].Split(new[] { '/', '\\' }).First();

            string last = selectedPath.Split(new[] { '/', '\\' }).Last();
            string projFolder = selectedPath.Substring(0, selectedPath.Count() - last.Count());
            ProjectFolder = projFolder + nameProject;

            CheckDefaultValueFileExist(); //ak su tak nacitame preddef hodnoty triedam

            //load project options
            ProjOptions = ProjectOptions.LoadProjectOtions(ProjectFolder) ?? new ProjectOptions();

            var thread = new Thread(() => OpenImageFromFileThread(fileType, imagesSrc, imagesAttrs, imagesDrawObjects, videosSrc, framesAttrs, framesDrawObjects));

            thread.Start();
            return true;
        }

        /// <summary>
        /// metoda ktora z nacitanych dat z XML/CSV suboru vytvori obrazky
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="imagesSrc"></param>
        /// <param name="imagesAttrs"></param>
        /// <param name="imagesDrawObjects"></param>
        /// <param name="videosSrc"></param>
        /// <param name="framesAttrs"></param>
        /// <param name="framesDrawObject"></param>
        private void OpenImageFromFileThread(string fileType, List<string> imagesSrc, List<List<string[]>> imagesAttrs, List<List<DrawObject>> imagesDrawObjects, List<string> videosSrc, List<List<List<string[]>>> framesAttrs, List<List<List<DrawObject>>> framesDrawObject)
        {
            //potrebujem vytvorit cestu k projektu bez posledneho adresara(lebo budem spajat s cestou v xml/csv a tam uz je tento posledny adresar lebo xml/csv sa nachadza o adresar vyssie)
            string last = ProjectFolder.Split(new[] { '/', '\\' }).Last();
            string projFolder = ProjectFolder.Substring(0,ProjectFolder.Count()-last.Count());

            int imgCount = imagesSrc.Count;
            int frmCount = framesAttrs.Sum(x => x.Count());

            var sb = new StringBuilder();
            for (int i = 0; i < imgCount; i++)
            {
                string src = imagesSrc[i];
                string path = projFolder + src;

                if (!File.Exists(path))
                {
                    sb.AppendLine(path);
                    continue;
                }
                
                BaseFigure image = new ImageFigure(path, src);
                image.BoundBoxes = imagesDrawObjects[i].Where(imgDrawObj => imgDrawObj.GetType() == typeof(BoundingBox)).Cast<BoundingBox>().ToList();

                if (fileType.ToLower().Equals("xml"))//ak je to xml tak nacitaj aj PA,PL a atributy obrazkov
                {
                    image.Properties = imagesAttrs[i].Count == 0 ? null : imagesAttrs[i];
                    image.Paintings = imagesDrawObjects[i].Where(imgDrawObj => imgDrawObj.GetType() == typeof(Painting)).Cast<Painting>().ToList();
                    image.Polylines = imagesDrawObjects[i].Where(imgDrawObj => imgDrawObj.GetType() == typeof(Polyline)).Cast<Polyline>().ToList();
                }

                SetProgressBar((int)Math.Round(((double)(i + 1) / (imgCount + frmCount)) * 100));
                OpenImages.Add(image);
                AddStatus(src, i + 1, imgCount + frmCount, image);
            }

            for (int i = 0; i < videosSrc.Count; i++)
            {
                for (int j = 0; j < framesAttrs[i].Count; j++)
                {
                    var frames = framesAttrs[i][j];
                    int frameNum;
                    if (!Int32.TryParse(frames[0][1], out frameNum)) continue;
                    string src = videosSrc[i];
                    string newPath = src.Remove(src.Length - src.Split(new[] {'/', '\\'}).Last().Length);
                    string path = string.Format("{0}{1}.png", projFolder + newPath, frameNum);

                    if (!File.Exists(path))
                    {
                        sb.AppendLine(path);
                        continue;
                    }

                    BaseFigure image = new FrameFigure(path, src, frameNum);
                    image.BoundBoxes = framesDrawObject[i][j].Where(imgDrawObj => imgDrawObj.GetType() == typeof (BoundingBox)).Cast<BoundingBox>().ToList();

                    if (fileType.ToLower().Equals("xml")) //ak je to xml tak nacitaj aj PA,PL a atributy obrazkov
                    {
                        image.Properties = imagesAttrs[i].Count == 0 ? null : imagesAttrs[i];
                        image.Paintings = framesDrawObject[i][j].Where(imgDrawObj => imgDrawObj.GetType() == typeof (Painting)).Cast<Painting>().ToList();
                        image.Polylines = framesDrawObject[i][j].Where(imgDrawObj => imgDrawObj.GetType() == typeof (Polyline)).Cast<Polyline>().ToList();
                    }

                    SetProgressBar((int) Math.Round(((double) (imgCount + j + 1)/(imgCount + frmCount))*100));
                    OpenImages.Add(image);
                    AddStatus(src, imgCount + j + 1, imgCount + frmCount, image);
                }
            }

            if (sb.Length > 0)
                MessageBox.Show("Nasledujuce obrazky sa nepodarilo nacitat:\n" + sb, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (OpenImages.Count > 0) SetSettingAfterLoaded("Images Loaded");
            if (_mainForm.InvokeRequired)
                _mainForm.Invoke((Action<int>)_mainForm.CreateActuallThumbs, 0);
            else _mainForm.CreateActuallThumbs(0);
        }

        /// <summary>
        /// Nastavenie nasledujuceho obrazku na image box.
        /// </summary>
        /// <returns>nastaveny obrazok</returns>
        public BaseFigure SelectNextPicture()
        {
            int index = OpenImages.IndexOf(CurrentImage);
            if (index < OpenImages.Count-1) return OpenImages[index + 1];
            return null;
        }

        /// <summary>
        /// Nastavenie predchadzajuceho obrazku na image box.
        /// </summary>
        /// <returns>nastaveny obrazok</returns>
        public BaseFigure SelectPreviousPicture()
        {
            int index = OpenImages.IndexOf(CurrentImage);
            if (index > 0) return OpenImages[index - 1];
            return null;
        }

        /// <summary>
        /// Nastavenie obrazku na image box podla poradoveho cisla.
        /// </summary>
        /// <param name="index">poradove cislo obrazku, ktory sa ma nastavit</param>
        /// <returns>nastaveny obrazok</returns>
        public BaseFigure GoOnPicture(int index)
        {
            if (index > 0 && index<OpenImages.Count) return OpenImages[index - 1];
            return null;
        }

        /// <summary>
        /// Vyber uzivatelom preddefinovanej triedy a nastavenie preddefinovanych atributov pre tuto triedu.
        /// </summary>
        public void SelectDefaultClass()
        {
            if(CurrentImage == null) return;
            if (_defaultClass != null) // ak mam preddefinovanu triedu, zistim ci nema aj preddefinovane hodnoty
            {
                if (!SelectUserDefClass(_defaultClass))
                    SetDefaultAttrsForClass(new KeyValuePair<string, List<string>>(_defaultClass, _defaltValuesForClasses[_defaultClass]));
            }
            else if (_defaltValuesForClasses.Count > 0) //ak nemam preddefinovanu triedu nastavim prvu triedu ktora ma preddefinovane atributy
                  SetDefaultAttrsForClass(_defaltValuesForClasses.First());            
        }

        /// <summary>
        /// Vyber uzivatelom preddefinovanych hodnot pre zadanu triedu ak tato ma preddef hodnoty.
        /// </summary>
        /// <param name="nameClass">trieda pre ktoru sa maju nastavit preddefinovane hodnoty</param>
        /// <returns>Vrati true ak ma dana trieda nastavene preddef vlastnosti</returns>
        public bool SelectUserDefClass(string nameClass)
        {
            if (_defaltValuesForClasses.Count == 0 || CurrentImage == null) return false;
            foreach (KeyValuePair<string, List<string>> defaltValuesForClass in _defaltValuesForClasses)
            {
                if (defaltValuesForClass.Key == nameClass)
                {
                    SetDefaultAttrsForClass(defaltValuesForClass);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// nastavenie atributov podla preddef hodnot
        /// </summary>
        /// <param name="defaultValueForClass">Preddef hodnoty</param>
        private void SetDefaultAttrsForClass(KeyValuePair<string, List<string>> defaultValueForClass)
        {
            string[] path = CurrentImage.Source.Split(new[] { '/', '\\' }); //teraz mam cestu rozdelenu na hodnoty atributov, avsak posledna hodnota je nazov obrazku.
            List<string> attrsValue = new List<string>();
            //naplnime zoznam preddefinovanymi hodnotami ktore maju mat atributy
            for (int i = 0; i < defaultValueForClass.Value.Count; i++)
            {
                string v = defaultValueForClass.Value[i].Replace(" ", "");
                if (v == "") attrsValue.Add("");  //ak nie je ziadna hodnota vo 'v' znamena to ze ju uzivatel nevyplnil
                else if (v.Substring(0, 1) == "=") //uzivatel definoval pevnu hodnotu bez ohladu na cestu
                {
                    attrsValue.Add(v.Replace("=", ""));
                }
                else if (v == "false" || v == "true") //hodnota pre checkbox
                {
                    attrsValue.Add(v);
                }
                else //hodnota pre kombobox z cesty
                {
                    int index;
                    try
                    {
                        index = Convert.ToInt32(v);
                    }
                    catch (FormatException fx)
                    {
                        MessageBox.Show("Lutujem, no hodnotu \"" + v + "\" nie je mozne skonvertovat na int32.\nZle nastavené preddefinvané hodnoty atribútov.", fx.Message,
                                        MessageBoxButtons.OK);
                        attrsValue.Add("");
                        continue;
                    }
                    attrsValue.Add(index >= path.Count() ? "" : path[index - 1]);
                }
            }
            //potrebujeme nastavit tieto hodnoty do controlov
            MyClasses.SelectDefineValue(defaultValueForClass.Key, attrsValue);
        }

        /// <summary>
        /// Nastavenie hodnot atributov uzivatelom.
        /// </summary>
        public void DefineDefaultPropValue()
        {
            var setProp = new SetObjProperties(CurrentImage != null ? CurrentImage.Source : "");
            if (setProp.ShowDialog() == DialogResult.OK)
            {
                if (setProp.GetAllUserDefAttr() != null) _defaltValuesForClasses = setProp.GetAllUserDefAttr();
                if (setProp.GetDefaultClass() != null) _defaultClass = setProp.GetDefaultClass();
            }
            SelectDefaultClass();
        }

        /// <summary>
        /// Prezretie ci nie je v subore preddef nastavenie triedy. 
        /// </summary>
        public void CheckDefaultValueFileExist()
        {
            FileStream fs = null;
            StreamReader sr = null;
            string defaultClassFile = @"Data\Setting\UserDefautClass.dat";
            if (File.Exists(defaultClassFile))
            {
                try
                {
                    fs = new FileStream(defaultClassFile, FileMode.Open);
                    sr = new StreamReader(fs);
                    _defaultClass = sr.ReadLine();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri nacitavani uzivatelom preddefinovanej triedy:\n " + ex, "Error - Project.CheckDefaultValueFileExist()",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fs != null) fs.Close();
                    if (sr != null) sr.Close();
                }
            }

            string fileName = @"Data\Setting\UserDefValue.dat";
            if (!File.Exists(fileName))
                return;
      
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                sr = new StreamReader(fs);
                _defaltValuesForClasses.Clear();
                while (!sr.EndOfStream) //jeden riadok pre jednu triedu
                {
                    var param = sr.ReadLine().Split(';').ToList();
                    var className = param.Last();
                    param.RemoveAt(param.Count - 1);
                    _defaltValuesForClasses.Add(className, param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri nacitavani uzivatelom preddefinovanych hodnot atributov tried:\n " + ex, "Error-Project.CheckDefaultValueFileExist()",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Metoda na ulozenie BB ako samostatne obrazky, cize
        /// vystrihnutie z obrazka to cast ktoru tvori BB.
        /// </summary>
        public void SaveBBsAsImages()
        {
            var crImgFrmBB = new CreateImageFromDrawObj(OpenImages, ProjectFolder, 0);

            if (crImgFrmBB.ShowDialog() == DialogResult.OK)
            {
                ProgressForm progress = new ProgressForm();
                progress.SetName("Save BoundingBoxes To Images");

                GenerateData generator = new GenerateData(OpenImages);
                Thread thread = new Thread(() => generator.SaveBBAsAsImages(progress, crImgFrmBB));
                thread.Start();
                progress.Show(_mainForm);
            }
        }

        /// <summary>
        /// vytvorenie treningovych dat pre BB
        /// </summary>
        public void GenerateNegativeTrainingData()
        {
            var trainingForm = new CreateNegativeTrainingData(ProjectFolder);
            if (trainingForm.ShowDialog() == DialogResult.OK)
            {
                GenerateData generator = new GenerateData(OpenImages);
                ProgressForm progress = new ProgressForm();
                progress.SetName("Save Training Data");
                Thread thread =
                    new Thread(
                        () =>
                        generator.GenerateNegativeTrainingData(progress, trainingForm.ImageWidth,
                                                               trainingForm.ImageHeight, trainingForm.FolderToSave));
                progress.Show(_mainForm);
                thread.Start();
                trainingForm.Close();
            }
        }

        /// <summary>
        /// generovanie dat pre PL.
        /// </summary>
        public void GenerateDataForPL()
        {
            CreateImageFromDrawObj crImgFrmPL = new CreateImageFromDrawObj(OpenImages, ProjectFolder, 1);

            if (crImgFrmPL.ShowDialog() == DialogResult.OK)
            {
                ProgressForm progress = new ProgressForm();
                progress.SetName("Save Polylines To Images");

                GenerateData generator = new GenerateData(OpenImages);
                Thread thread = new Thread(() => generator.GenerateDataForPolylines(progress, crImgFrmPL /*, gr*/));
                thread.Start();
                progress.Show(_mainForm);
            }
        }

        /// <summary>
        /// Vyhladanie objektov podla uzivatelom zadefinovanych hodnot
        /// </summary>
        /// <param name="eventHandler">event handler</param>
        public void FindObjsInProject(EventHandler eventHandler)
        {
            FindObjectEvent = OnFindObjectEvent;// rovna sa je preto lebo chcem tam zavesit len jedinu metodu keby+=tak by mi ich tam hadzalo s kazdym novym vyhladavanim
            FindObjectsInProject foip = FindObjectsInProject.Instance ?? new FindObjectsInProject(MyClasses, ref FindObjectEvent);
            foip.Show(_mainForm);
            
            foip.Opacity = 0.9;
            foip.TopMost = true;
            foip.Closed += eventHandler;
        }

        private void OnFindObjectEvent(ObjectProperties propsToFind)
        {           
            //nastavime vlasnosti vyhladavania - ake objekty sa maju hladat
            FindingObjProps = propsToFind; 
            //vyhladame objekty na danom obrazku
            FindObjectOnImage();        
        }

        /// <summary>
        /// Udalost ktora vyvola vyhladanie objektov na obrazku
        /// </summary>
        public event FindObjectEventHandler FindObjectEvent;

        /// <summary>
        /// Prehladanie aktualneho obrazku a najdenie objektov ktore maju
        /// zhodne vlasnoti ako uzivatelom zadefinovane hodnoty
        /// </summary>
        public void FindObjectOnImage()
        {
            if (FindingObjProps == null)
            {
                GpFinding = null;
                _mainForm.Refresh();
                return;
            }
               
            GpFinding = new GraphicsPath();
            bool marked = false;
            foreach (var bb in CurrentImage.BoundBoxes)
            {
                if(bb.ComparePropertyWith(FindingObjProps))
                {
                    GpFinding.AddRectangle(bb.GetRectangle());
                    if (!marked)
                    {
                        marked = true;
                        _mainForm.SetFindingObj(bb);
                    }
                }
            }

            foreach (var pa in CurrentImage.Paintings)
            {
                if (pa.ComparePropertyWith(FindingObjProps))
                { 
                    GpFinding.AddPolygon(pa.Points.ToArray()); 
                    GpFinding.AddRectangle(pa.BoundingBox.GetRectangle());
                }
                if (!marked)
                {
                    marked = true;
                    _mainForm.SetFindingObj(pa);
                }
            }

            GraphicsPath gp = new GraphicsPath();
            foreach (var pl in CurrentImage.Polylines)
            {
                if (pl.ComparePropertyWith(FindingObjProps))
                {
                    gp.AddLines(pl.Points.ToArray());
                    GpFinding.AddPath(gp, false);
                    gp.Reset();
                }
                if (!marked)
                {
                    marked = true;
                    _mainForm.SetFindingObj(pl);
                }
            }

            _mainForm.Refresh();

        }

        /// <summary>
        /// Moznost zmenit defaultne nastavenia projektu / hrubka a farba nastrojov ...
        /// </summary>
        /// <returns>Vrati true ak doslo k zmene</returns>
        public bool SetProjectOptions()
        {
            var po = new ProjectOptionsForm(ProjOptions);
            if (po.ShowDialog() == DialogResult.OK)
            {
                ProjOptions = po.ProjOptions;
                ProjOptions.SaveProjectOptions(ProjectFolder);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Vrati zoznam vsetkych obrazkov projektu
        /// </summary>
        /// <returns>zoznam vsetkych obrazkov projektu</returns>
        public List<string> GetAllProjectImages()
        {
            List<string> paths = new List<string>();
            string lastFold = ProjectFolder.Split(new[] { '/', '\\' }).ToList().Last();
            string pathOfImg = ProjectFolder.Remove(ProjectFolder.Length-lastFold.Length);
            foreach (var figure in OpenImages)
            {
                paths.Add(pathOfImg + figure.Source);
            }

            return paths;
        }

        /// <summary>
        /// Pridanie boundingboxov na obrazku projektu
        /// </summary>
        /// <param name="rectanglesOfImages">boundingboxy pre pridanie na obrazky projektu</param>
        public void AddBoundingBoxesToImages(List<List<BoundingBoxStructure>> rectanglesOfImages)
        {
            for (int i = 0; i < rectanglesOfImages.Count; i++)
            {
                var listRectsOfImage = rectanglesOfImages[i];
                if (listRectsOfImage == null || !listRectsOfImage.Any())
                    continue;

                foreach (var rectangle in listRectsOfImage)
                {
                    OpenImages[i].BoundBoxes.Add(new BoundingBox(OpenImages[i].BoundBoxes.Count,rectangle.Rectangle,rectangle.ClassName));
                }
            }
        }
    }
    
}
