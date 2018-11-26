using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Projekt.DrawObjects;
using Projekt.Enums;
using Projekt.ExportImport;
using Projekt.Figure;
using Projekt.UserControls;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca hlavne okno aplikacie
    /// </summary>
    public partial class MainWindowApplication : Form
    {
        /// <summary>
        ///  vrstva na ktoru vykreslujeme objekty
        /// </summary>
        private Bitmap _btm = new Bitmap(1,1);

        /// <summary>
        /// graphics pomocou ktorej vykreslujeme objekty
        /// </summary>
        Graphics _graphics = null;

        /// <summary>
        /// Boundingboxy
        /// </summary>
        private GraphicsPath _gpBB;

        /// <summary>
        /// Paintingy
        /// </summary>
        private GraphicsPath _gpPA;

        /// <summary>
        /// boundingBoxy paintingov
        /// </summary>
        private GraphicsPath _gpPABB;

        /// <summary>
        /// boundingBoxy polyline
        /// </summary>
        private GraphicsPath _gpPL;

        /// <summary>
        /// oznaceny objekt
        /// </summary>
        private GraphicsPath _gpMark;

        private GraphicsPath _gpGuideLine;
        private bool _redrawOnlyGuideLine = false;
        /// <summary>
        /// BB ktory sa prave vytvara
        /// </summary>
        private Rectangle _crBB;

        /// <summary>
        /// PA ktory sa prave vytvara
        /// </summary>
        private List<Point> _crPA;

        /// <summary>
        /// PL ktory sa prave vytvara
        /// </summary>
        private List<Point> _crPL; 

        /// <summary>
        /// true ak sa edituje BB, inak false
        /// </summary>
        private bool _editingBB = false;

        /// <summary>
        /// true ak sa vytvara polyline, inak false
        /// </summary>
        private bool _creatingPL = false;

        /// <summary>
        /// True ak sa kresli nejaky objekt(PA,BB), inak false
        /// </summary>
        private bool _mouseDown = false;

        private Image<Bgr, Byte> _imgWelcome;
        
        /// <summary>
        /// Toto je bod na ktory bol vybraty(bude zmeneni) pri editovani BB.
        /// </summary>
        private Point _selectPoint;

        /// <summary>
        /// DrawObject ktory bol vybraty pre editovanie.
        /// </summary>
        private DrawObject _selectedObj = null;

        private bool _scrollButtonPressed;
        private bool _copyBB;
        private bool _movingBB;
        private int _hlpHScroll, _hlpVScroll;
        private double _hlpZoom;
        private int _selectedClass;

        /// <summary>
        /// Podla toho ktory prvok true, tak taky nastroj je zvoleny
        /// 0 - _paint, 
        /// 1 - _drawBB, 
        /// 2 - _cursor,
        /// 3 - _polyline,
        /// 4 - _find
        /// </summary>
        //private bool[] _tools = new bool[5];
        private EToolItem _activeTool = EToolItem.None;
        private Project _project;
        private TrackEditorForm _trackWindow;

        private List<string> _copyPropertiesList;
        private List<string> _copyNamesList;
        private string _copyClassName;
        private ToolTip _toolTip1;

        private TestEditorForm _testWindow;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="args">parametre pri volani z prikazoveho riadku</param>
        public MainWindowApplication(string[] args)
        {  
            InitializeComponent();
            imgBoxSelectedObj.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            pnlProperty.BorderStyle = BorderStyle.Fixed3D;
            _gpBB = new GraphicsPath();
            _gpPA = new GraphicsPath();
            _gpPA.FillMode = FillMode.Winding;
            _gpPL = new GraphicsPath();
            _gpPABB = new GraphicsPath();
            _gpMark = new GraphicsPath();
            _gpGuideLine = new GraphicsPath();
            _crBB = new Rectangle();
            _crPA = new List<Point>();
            _crPL = new List<Point>();
            imageBox.Image = (_imgWelcome = File.Exists("welcome.png") ? new Image<Bgr, Byte>("welcome.png") : null); 
            imageBox.HorizontalScrollBar.Scroll += ScrollBarOnScroll;
            imageBox.VerticalScrollBar.Scroll += ScrollBarOnScroll;
            imageBox.OnZoomScaleChange += ImageBoxOnOnZoomScaleChange;
            imageBox.MouseWheel += ImgeBoxOnMouseWheel;
            txbZoom.Text = 100 + "%";
            EnableTools(false);
            EnableMenu(false);
            FileFromCmd = null;
 //           gbProperty.MouseLeave += GbPropertyOnMouseLeave;
            if (args.Any())//potrebne pre implementaciu tohto nastroja do filtracneho nastroja
            {
                OpenProject(FileFromCmd);
                newToolStripMenuItem.Enabled = openToolStripMenuItem.Enabled = false;
            }
            _scrollButtonPressed = false;
            _copyBB = true;
            _movingBB = false;
            _hlpVScroll = 0; _hlpHScroll = 0;
            _hlpZoom = 0;
            _trackWindow = null;
            _selectedClass = 0;
            _testWindow = null;

            _copyClassName = null;
            pnlCopyInfo.AccessibleDescription = "No data selected";
        }

/*        private void GbPropertyOnMouseLeave(object sender, EventArgs eventArgs)
        {
            var a = gbProperty.PointToClient(MousePosition);
            var b = gbProperty.PointToScreen(MousePosition);
            //(new Rectangle(this.PointToScreen(gbProperty.Location), gbProperty.Size)).Contains(MousePosition)
            if (gbProperty.GetChildAtPoint(gbProperty.PointToClient(MousePosition)) != null)//if (gbProperty.ClientRectangle.Contains(gbProperty.PointToClient(MousePosition)))
            {
                MessageBox.Show("IN");
                // the mouse is inside the control bounds
            }
            else
            {
                MessageBox.Show("OUT");
                // the mouse is outside the control bounds
            }
        }*/

        /// <summary>
        /// V pripade spustenia nastroja cez prikazovy riadok
        /// je tu atribut predstavujuci subor na otvorenie
        /// </summary>
        public string FileFromCmd { get; private set; }

        /// <summary>
        /// Vytorenie noveho projektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewProject(object sender, EventArgs e)
        {
            chbRememberZoom.Checked = false;
            _hlpVScroll = 0; _hlpHScroll = 0;
            _hlpZoom = 0;
            var fbd1 = new FolderBrowserDialog();
            if (fbd1.ShowDialog() == DialogResult.OK)
            {
                SetStatusBar(0,0);
                SetProgressBar(100);
                _project = new Project(this, fbd1.SelectedPath)
                {
                    MyClasses = new ClassGenerator(cmbClass, pnlProperty),
                    ProjOptions = new ProjectOptions() //defaultne nastavenia, kedze vytvarame novy projekt niet ake ine nastavenia nacitat
                };
                _project.CreatePictureProperty(pnlPictAttribute);
                _project.CheckDefaultValueFileExist();
                RestoreDefault();
                EnableMenu(true);
                ResetClassControls();
                //nacitanie tried zo suboru
                _project.MyClasses.GenerateClass(cmbClass);
                
            }
        }

        /// <summary>
        /// Otvorenie ulozeneho projektu. Nacita sa xml a podla neho sa 
        /// hlada zlozka projektu ako aj obrazky v tomto projekte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProject(object sender, EventArgs e)
        {            
            chbRememberZoom.Checked = false;
            _hlpVScroll = 0; _hlpHScroll = 0;
            _hlpZoom = 0;
            var ofd = new OpenFileDialog();
            ofd.Filter = "XML Files(*.XML)|*.XML|TEXT FILE(*.TXT, *.CSV)|*.TXT; *.CSV";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenProject(ofd.FileName);
                if (_trackWindow != null)
                {
                    _trackWindow.Close();
                    _trackWindow = null;
                }
                if (_testWindow != null)
                {
                    _testWindow.Close();
                    _testWindow = null;
                }
            }
        }

        private void OpenProject(string file)
        {            
            try
            {
                _project = new Project(this, file)//tato cesta je cesta k XML nie k projektu (lisia sa len v poslednej casti)
                {
                    MyClasses = new ClassGenerator(cmbClass, pnlProperty)
                };
                _project.CreatePictureProperty(pnlPictAttribute);
                RestoreDefault();
                EnableMenu(true);
                EnableTools(true);
                ResetClassControls();
                //nacitanie tried zo suboru
                _project.MyClasses.GenerateClass(cmbClass);
                if (!_project.OpenProject())
                {
                    RestoreDefault();
                    return;
                }
                
                if (_project.CurrentImage != null)
                {
                    RefreshComboBox();
                    _project.SelectDefaultClass();
                    txbZoom.Text = _project.CurrentImage.Zooom * 100 + "%";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "MainWindowApplication-OpenProject()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        /// <summary>
        /// Import obrazka s ktorym chce uzivatel pracovat. 
        /// Mozu byt v zlozke projektu, cize obrázky nekopirujeme,
        /// ale možu byt uložene v roznej adresarovej štrukture, ktoru chceme zachovat.
        /// Alebo mozu byt v inej zlozke ako je zlozka projektu, vtedy nakopirujeme tieto obrazky do zlozky projeku.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportImage(object sender, EventArgs e)
        {
            if (_project.ImportImage())
            {
                ResetClassControls();
                ResetGraphicsPath();
                EnableTools(true);
            }
        }

        /// <summary>
        /// Import videa s ktorym chce uzivatel pracovat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportVideo(object sender, EventArgs e)
        {
            if (_project.ImportVideo())
            {
                ResetClassControls();
                ResetGraphicsPath();
                EnableTools(true);
                SetZoomAndScroll();
                cmbAllObject.Items.Clear();
            }
        }

        /// <summary>
        /// Vytvorenie nahladu pre dany obrazok.
        /// </summary>
        /// <param name="image">obrazok pre ktory sa ma vytvorit nahlad</param>
        public void DrawThumb(BaseFigure image)
        {
            //  Vytvorenie panelu nahladu                                
            var thumbPanel = new ThumbPanel(image);
            thumbPanel.SetBounds(5 + 115*(pnlThumbs.Controls.Count), 5, 110, 85);//  Nastavenie umiestnenia na liste miniatur
            thumbPanel.BackgroundImageLayout = ImageLayout.Zoom;
            thumbPanel.BackgroundImage = image.GetImage().ToBitmap(100, 70);
            thumbPanel.MouseClick += ThumbPanelOnMouseClick;
            pnlThumbs.Controls.Add(thumbPanel);
        }

        /// <summary>
        /// Nastavenie statusu, aktualna / pocet vsekych
        /// </summary>
        /// <param name="actual">poradove cislo aktualnej snimky</param>
        /// <param name="all">pocet vsetkych snimok</param>
        public void SetStatusBar(int actual, int all)
        {
            if (this.InvokeRequired)
                this.Invoke((Action<int, int>)SetStatusBar, actual, all); 
            else statusLabelCurrent.Text = "Current image: " + (all == 0 ? 0: actual) + "/" + all;
        }

        /// <summary>
        /// Obsluzenie akcie kliknutia na miniaturu obrazka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void ThumbPanelOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_creatingPL) // ak sa vytvara PL a ukoncenie vytvarania PL
            {
                EndCreatePL();
            }
            _selectedClass = cmbClass.SelectedIndex;
            _hlpHScroll = imageBox.HorizontalScrollBar.Value; 
            _hlpVScroll = imageBox.VerticalScrollBar.Value;
            _hlpZoom = _project.CurrentImage.Zooom;
            _project.CurrentImage.VScroll = imageBox.VerticalScrollBar.Value;
            _project.CurrentImage.HScroll = imageBox.HorizontalScrollBar.Value;
            UnselectObject(); 
            _project.ChangePicture(((ThumbPanel)sender).Image);
            txbZoom.Text = _project.CurrentImage.Zooom * 100 + "%";
            ResetClassControls(); 
            SetImage2ImageBox(_project.CurrentImage);
            SetStatusBar(_project.OpenImages.IndexOf(_project.CurrentImage)+1, _project.OpenImages.Count);

            if (cmbClass.Items.Count > _selectedClass)
                cmbClass.SelectedIndex = _selectedClass;

            //skontroluje sa ci to nie je prvy alebo posledny nahlad ak hej tak sa vytvoria nove
            //bud predchazajuce alebo nasledovne
            if (pnlThumbs.Controls.Count == 1)//prvy a posledny zaroven
            {
                int index = _project.OpenImages.IndexOf(_project.CurrentImage);
                if (index == 0) return;
                CreateActuallThumbs(index - 50 < 0 ? 0 : index - 50);
            }
            else if (((ThumbPanel)sender) == pnlThumbs.Controls[pnlThumbs.Controls.Count - 1])//posledny na panely thumbs
            {
                if (_project.CurrentImage == _project.OpenImages.Last()) return;//ak je posledny aj v zozname vsetkych obrazkov, netreba nic prekreslovat
                CreateActuallThumbs(_project.OpenImages.IndexOf(_project.CurrentImage));
            }
            else if (((ThumbPanel)sender) == pnlThumbs.Controls[0]) //prvy
            {
                int index = _project.OpenImages.IndexOf(_project.CurrentImage);
                if (index == 0) return;
                CreateActuallThumbs(index - 50 < 0 ? 0 : index - 50);
            }            
        }

        private void SavePropertyFor(DrawObject drObj)
        {
            if (drObj == null || (cmbClass.SelectedItem == null)) return;

            string[] propsValue = new string[_project.MyClasses.PropsName.Length];

            int i = 0;
            foreach (Control control in pnlProperty.Controls) //nacitanie vlastnosti z komboboxou s atributami z panelu property
            {
                if (typeof(ClassMultiProperty) == control.GetType())
                    propsValue[i++] = (control as ClassMultiProperty).GetSelectedValue();
                else if (typeof(ClassBoolProperty) == control.GetType())
                    propsValue[i++] = (control as ClassBoolProperty).Checked ? "true" : "false";
            }

            drObj.Properties = new ObjectProperties(_project.MyClasses.PropsName, propsValue, cmbClass.SelectedItem.ToString());
            RefreshComboBox();
        }

        /// <summary>
        /// Ulozenie vlasnosti daneho objektu.
        /// </summary>
        private void SaveProperties()
        {
            try
            {
                if ((cmbAllObject.SelectedItem == null) || (cmbClass.SelectedItem == null)) return;
                Object obj = (cmbAllObject.SelectedItem as ComboBoxItem).Value;
                if (obj == null) return;

                string[] propsValue = new string[_project.MyClasses.PropsName.Length];

                int i = 0;
                foreach (Control control in pnlProperty.Controls) //nacitanie vlastnosti z komboboxou s atributami z panelu property
                {
                    if (typeof(ClassMultiProperty) == control.GetType())
                        propsValue[i++] = (control as ClassMultiProperty).GetSelectedValue();
                    else if (typeof(ClassBoolProperty) == control.GetType())
                        propsValue[i++] = (control as ClassBoolProperty).Checked ? "true" : "false";
                }

                //var op = new ObjectProperties(_project.MyClasses.PropsName, propsValue, cmbClass.SelectedItem.ToString());

                var drawObject = obj as DrawObject;
                if (drawObject != null)
                    drawObject.Properties = new ObjectProperties(_project.MyClasses.PropsName, propsValue, cmbClass.SelectedItem.ToString());
                //_project.SaveObjectProperty(op, obj);
                RefreshComboBox();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        /// <summary>
        /// Refresh poloziek v comboboxe po pridani dalsieho.
        /// </summary>
        private void RefreshComboBox()
        {
            cmbAllObject.Items.Clear();
            cmbAllObject.ValueMember = "Value";
            cmbAllObject.DisplayMember = "Text";

            ChangeObjectIds();
            cmbAllObject.Items.Add(new ComboBoxItem { Selectable = false, Text = "Bounding Box", Value = 0 });
            foreach (BoundingBox bb in _project.CurrentImage.BoundBoxes)
            {   //  Pre vsetky BB aktualne vybraneho obrazku sa vytvoria polozky v komboboxe
                cmbAllObject.Items.Add(new ComboBoxItem { Selectable = true, Text = "    " + bb.Name + " " + (bb.Properties != null ? bb.Properties.Class : "" ), Value = bb });
            }

            cmbAllObject.Items.Add(new ComboBoxItem { Selectable = false, Text = "Painting", Value = 0 });
            foreach (Painting paint in _project.CurrentImage.Paintings)
            {   //  Pre vsetky PA aktualne vybraneho obrazku sa vytvoria polozky v komboboxe
                cmbAllObject.Items.Add(new ComboBoxItem { Selectable = true, Text = "    " + paint.Name + " " + (paint.Properties != null ? paint.Properties.Class : ""), Value = paint });
            }

            cmbAllObject.Items.Add(new ComboBoxItem { Selectable = false, Text = "Polyline", Value = 0 });
            foreach (Polyline polyline in _project.CurrentImage.Polylines)
            {   //  Pre vsetky PL aktualne vybraneho obrazku sa vytvoria polozky v komboboxe
                cmbAllObject.Items.Add(new ComboBoxItem { Selectable = true, Text = "    " + polyline.Name + " " + (polyline.Properties != null ? polyline.Properties.Class : ""), Value = polyline });
            }

            //  Po kliktuti na polozku v komboboxe, sa kontroluje ci je vybratelna, ak nieje, tak sa nevyberie
            cmbAllObject.SelectedIndexChanged += (cbSender, cbe) =>
            {
                var cb = cbSender as ComboBox;

                if (cb.SelectedItem is ComboBoxItem && ((ComboBoxItem)cb.SelectedItem).Selectable == false)
                {
                    cb.SelectedIndex = -1; // deselect item
                }
            };
            cmbAllObject.Text = "";
            if (_selectedObj == null) return;
            if (_project.CurrentImage.BoundBoxes.Any() && typeof(BoundingBox) == _selectedObj.GetType())
                cmbAllObject.SelectedItem = FindCbbItem(/*_project.CurrentImage.BoundBoxes.Last()*/_selectedObj);
            else if (_project.CurrentImage.Paintings.Any() && typeof(Painting) == _selectedObj.GetType())
                cmbAllObject.SelectedItem = FindCbbItem(/*_project.CurrentImage.Paintings.Last()*/_selectedObj);
            else if (_project.CurrentImage.Polylines.Any() && typeof(Polyline) == _selectedObj.GetType())
                cmbAllObject.SelectedItem = FindCbbItem(/*_project.CurrentImage.Polylines.Last()*/_selectedObj);
        }

        private void ChangeObjectIds()
        {
            for (int i = 0; i < _project.CurrentImage.BoundBoxes.Count; i++)
            { 
                _project.CurrentImage.BoundBoxes[i].ID = i;
            }

            for (int i = 0; i < _project.CurrentImage.Paintings.Count; i++)
            {
                _project.CurrentImage.Paintings[i].ID = i;
            }

            for (int i = 0; i < _project.CurrentImage.Polylines.Count; i++)
            {
                _project.CurrentImage.Polylines[i].ID = i;
            }
        }

        /// <summary>
        /// Vrati polozku komboboxu podla parametru.
        /// Sluzi na nastavenie zobrazenej polozky v CBB na poslednu pridanu.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private ComboBoxItem FindCbbItem(Object obj)
        {
            return cmbAllObject.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Value == obj);
        }

        /// <summary>
        /// Vnorena trieda. Prvok komboboxu.
        /// </summary>
        public class ComboBoxItem
        {
            /// <summary>
            /// polozka komboboxu
            /// </summary>
            public Object Value { get; set; }

            /// <summary>
            /// text polozky komboboxu
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// urcuje ci je mozne vybrat tuto polozku
            /// </summary>
            public bool Selectable { get; set; }

            /// <summary>
            /// ToString metoda
            /// </summary>
            /// <returns>text polozky komboboxu</returns>
            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// Rozklikol sa kombobox so vsetkymi objektami je treba ulozit
        /// vlastnosti doteraz vybranemu objektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbAllObjectDropDown(object sender, EventArgs e)
        {
            //cmbAllObject.SelectedIndex = cmbAllObject.Items.Count / 2;
            //cmbAllObject.AutoScrollOffset.Offset.
            if (_creatingPL) // ak sa vytvara PL a ukoncenie vytvarania PL
            {
                EndCreatePL();
            }

            var obj = (ComboBoxItem)cmbAllObject.SelectedItem;
            if (obj == null || !obj.Selectable) return;

            var drObj = (DrawObject)obj.Value;
            SelectObject(drObj);
           // SaveProperties();
//            SavePropertyFor(_selectedObj);
        /*    if (_mouseDown || _creatingPL) return;

            ComboBoxItem obj = (ComboBoxItem)cmbAllObject.SelectedItem;
            if (obj == null || !obj.Selectable) return;

            DrawObject drObj = (DrawObject)obj.Value;
            if (drObj.GetType() == _selectedObj.GetType() && drObj.ComparePropertyWith(_selectedObj.Properties))
                return;
            SelectObject(drObj);*/
        }

        /// <summary>
        /// Vypis vlastnosti o vybranom objekte (BB,PA)
        /// po vybrati z comboboxu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select(object sender, EventArgs e)
        {
            if (_mouseDown || _creatingPL) return;

            ComboBoxItem obj = (ComboBoxItem)cmbAllObject.SelectedItem;
            if (obj == null || !obj.Selectable) return;

            DrawObject drObj = (DrawObject)obj.Value;

            //  Vyber nazov triedy v komboboxe
            if (drObj.Properties.Class == null || cmbClass.SelectedItem == null || !drObj.Properties.Class.Equals(cmbClass.SelectedItem.ToString())) cmbClass.SelectedItem = null;

            foreach (ComboBoxItem item in cmbClass.Items)
            {
                if (item.Text == drObj.Properties.Class)
                {
                    cmbClass.SelectedItem = item;
                    break;
                }
            }

            //vyplnenie cbb podla property daneho bb.
            SetCbbProperty(drObj);   
            _selectedObj = drObj;
            MarkSelected(drObj);
            if (_trackWindow != null && _selectedObj.GetType() == typeof(BoundingBox))
            {
                _trackWindow.SelectObject(_selectedObj);
            }
            else if (_trackWindow != null && _selectedObj.GetType() != typeof(BoundingBox))
            {
                _trackWindow.SelectObject(null);    
            }
            imageBox.Refresh();
        }

        /// <summary>
        /// Vyplnenie cbb na panely property podla hodot atributov daneho bb.
        /// </summary>
        /// <param name="drObj"></param>
        private void SetCbbProperty(DrawObject drObj)
        {            
            if (drObj.Properties.Class == null) return;
                        
            foreach (Control control in pnlProperty.Controls)
            {
                if (typeof (ClassMultiProperty) == control.GetType())
                {
                    ClassMultiProperty con = (ClassMultiProperty) control;
                    con.SetSelectedValue("");//vymazeme
                    for (int j = 0; j < drObj.Properties.AtributesName.Length; j++)
                    {
                        if (con.GetName() == drObj.Properties.AtributesName[j])
                        {
                            con.SetSelectedValue(drObj.Properties.AtributesValue[j]);
                            break;
                        }
                    }
                }
                else if (typeof (ClassBoolProperty) == control.GetType())
                {
                    ClassBoolProperty con = (ClassBoolProperty)control;
                    for (int j = 0; j < drObj.Properties.AtributesName.Length; j++)
                    {
                        if (con.AtrName == drObj.Properties.AtributesName[j])
                        {
                            con.Checked = (drObj.Properties.AtributesValue[j] == "true");
                            break;
                        }
                    }
                }      
            }            
        }

        private void SaveToFileUndefined(string className, List<string> props)
        {
            TextWriter sw = null;
            try
            {
                string imageName = "";                

                sw = new StreamWriter(@"Data\Class.dat", true);

                sw.WriteLine();
                sw.Write(className + ";" + imageName);
                sw.Close();

                //ukladanie atributov triedy
                sw = new StreamWriter(@"Data\" + className + ".dat");

                foreach (string prop in props)
                {
                    sw.WriteLine(prop);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Chyba pri ukladani novej triedy spolu s atribútmi do súboru:\n" + exc.Message, "Error - AddClassForm.SaveToFile()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sw != null) sw.Close();
                _project.MyClasses.GenerateClass(cmbClass);
            }
        }

        /// <summary>
        /// Ulozenie projektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveProjectAsClick(object sender, EventArgs e)
        {
            try
                {
                    if (FileFromCmd == null)
                        _project.SaveProjectAs();
                    else
                    {
                            var export = new ImportExportXML(_project.OpenImages, _project.ProjectFolder, true);
                            if (export.ExportProjectExecuteCmd(FileFromCmd))
                                MessageBox.Show("Ukladanie úspešne ukončené", "Upozornenie", MessageBoxButtons.OK);
                            else MessageBox.Show("Ukladanie zlyhalo", "Error", MessageBoxButtons.OK);
                    }
                }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error - MainWindowApplication.SaveProjectAsClick()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obsluha kliknutia na platno obrazka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxClick(object sender, EventArgs e)
        {
            if (_creatingPL) return;

            if (_project == null || _project.CurrentImage == null || (_activeTool != EToolItem.Cursor && _activeTool != EToolItem.Find) || _editingBB) return;

            //_gpMark.Reset(); //vymazanie GraphicsPath v ktorom udrzujeme oznaceny objekt, pretoze sa oznacuje novy objekt

            var mouse = (e as MouseEventArgs);
            var pointClick = UnzoomAndUnscrollPoint(mouse.Location); 
            UnselectObject();
            //_selectedObj = null;
            imgBoxSelectedObj.Image = null;
            pnlSelObjOptions.Enabled = false;

            var selObj = SearchConstraintsInBBs(pointClick);
            var selObjSec = SearchConstraintsInPAs(pointClick, selObj);
            if (selObjSec != null)
                selObj = selObjSec;
            selObjSec = SearchConstraintsInPLs(pointClick);
            if (selObjSec != null)
                selObj = selObjSec;

            SelectObject(selObj);

            if ((_selectedObj == null || _selectedObj.GetType() != typeof(BoundingBox)) && _trackWindow != null)
            {
                _trackWindow.SelectObject(null);
            }

            //ak bolo nieco vybrate(kliklo sa na nejaky objekt BB alebo PA)
            if (_selectedObj != null)
            {
                //kontrola ci sa nekliklo znova na ten isty objekt
                if (cmbAllObject.SelectedItem == null || ((ComboBoxItem)cmbAllObject.SelectedItem).Value != _selectedObj) //ak nie
                    cmbAllObject.SelectedItem = FindCbbItem(_selectedObj); //spristupnime objektu pre upravu.
                else //ak ano tak len prepiseme hodnoty v komboboxoch urcujuce vlasnosti daneho objektu, lebo tie mohli byt zmenene
                    SetCbbProperty(_selectedObj);
//                MarkSelected(_selectedObj);

                if (_trackWindow != null && _selectedObj.GetType() == typeof(BoundingBox))
                {
                    _trackWindow.SelectObject(_selectedObj);    
                }
            }
            else if (_selectedObj == null)            
                cmbAllObject.SelectedIndex = -1;
            imageBox.Refresh();
        }

        private void ImgBoxMouseEnter(object sender, EventArgs e)
        {
            if (!imageBox.Focused)
                imageBox.Focus();
        }

        private void ImgBoxMouseLeave(object sender, EventArgs e)
        {
            if (imageBox.Focused)
                imageBox.Parent.Focus();

            if (_project != null && _gpGuideLine != null)
            {
                _gpGuideLine.Reset();
                _redrawOnlyGuideLine = true;
                imageBox.Refresh();
            }          
        }

        /// <summary>
        /// Prehladanie BBs a hladanie ci sa bod nachadza v niektorom z nich
        /// </summary>
        /// <param name="pointClick"></param>
        private DrawObject SearchConstraintsInBBs(Point pointClick)
        {
            DrawObject selObj = null;
            foreach (BoundingBox bb in _project.CurrentImage.BoundBoxes) //prehladavame BB ci sa nekliklo na niektory z nich
            {
                Rectangle rec = new Rectangle(bb.PointA, bb.Size);
                if (!rec.Contains(pointClick)) continue; //kontrola ci sa kliklo do BB
                //treba skontrolovat ci neprebieha vyhladananie a ci sa nekliklo na niektory z BB ktory nie je zobrazeny, vtedy ignorujeme kliknutie 
                if (_project.FindingObjProps != null && !bb.ComparePropertyWith(_project.FindingObjProps)) continue;
                if (selObj == null) selObj = bb;
                else
                {   //ak su dva BB v sebe a klikneme do oblasti mensieho tak vratime tento mensi
                    if (((BoundingBox)selObj).Size.Width > bb.Size.Width && ((BoundingBox)selObj).Size.Height > bb.Size.Height) selObj = bb;
                }
            }

            return selObj;
        }

        /// <summary>
        /// Prehladanie PAs a hladanie ci sa bod nachadza v niektorom z nich
        /// </summary>
        /// <param name="pointClick"></param>
        private DrawObject SearchConstraintsInPAs(Point pointClick, DrawObject drObj)
        {
            DrawObject selObj = drObj;
            foreach (Painting pa in _project.CurrentImage.Paintings)//kontrola ci sa nakliko na PA
            {
                Rectangle rec = new Rectangle(pa.BoundingBox.PointA, pa.BoundingBox.Size);
                if (!rec.Contains(pointClick)) continue;
                if (_project.FindingObjProps != null && !pa.ComparePropertyWith(_project.FindingObjProps)) continue;

                if (selObj == null) selObj = pa;
                else
                {   //ak su dva PA(or PA a BB) v sebe a klikneme do oblasti mensieho tak vratime tento mensi
                    if ((selObj.GetType() == typeof(Painting) ? ((Painting)selObj).BoundingBox : (BoundingBox)selObj).Size.Width > pa.BoundingBox.Size.Width
                        && (selObj.GetType() == typeof(Painting) ? ((Painting)selObj).BoundingBox : (BoundingBox)selObj).Size.Height > pa.BoundingBox.Size.Height) selObj = pa;
                }
            }
            return selObj;
        }

        /// <summary>
        /// Prehladanie PLs a hladanie ci sa bod nachadza v blizkosti niektorej z nich
        /// </summary>
        /// <param name="pointClick"></param>
        private DrawObject SearchConstraintsInPLs(Point pointClick)
        {
            DrawObject selObj = _selectedObj;
            double distance = double.MaxValue;
            foreach (Polyline pl in _project.CurrentImage.Polylines)//kontrola ci sa nakliko na PL
            {                                                       //http://www.topcoder.com/tc?d1=tutorials&d2=geometry1&module=Static
                for (int i = 0; i < pl.Points.Count - 1; i++)
                {
                    double aktDist;
                    if (((pl.Points[i + 1].X - pl.Points[i].X) * (pointClick.X - pl.Points[i + 1].X)) +
                         ((pl.Points[i + 1].Y - pl.Points[i].Y) * (pointClick.Y - pl.Points[i + 1].Y)) > 0)
                        aktDist = Math.Sqrt(Math.Pow(pl.Points[i + 1].X - pointClick.X, 2) + Math.Pow(pl.Points[i + 1].Y - pointClick.Y, 2)); //vzdialenost BC
                    else if (((pl.Points[i].X - pl.Points[i + 1].X) * (pointClick.X - pl.Points[i].X)) +
                             ((pl.Points[i].Y - pl.Points[i + 1].Y) * (pointClick.Y - pl.Points[i].Y)) > 0)
                        aktDist =
                            Math.Sqrt(Math.Pow(pl.Points[i].X - pointClick.X, 2) +
                                      Math.Pow(pl.Points[i].Y - pointClick.Y, 2)); //vzdialenost AC
                    else
                        aktDist = Math.Abs(((pl.Points[i + 1].X - pl.Points[i].X) * (pointClick.Y - pl.Points[i].Y) -
                                            (pl.Points[i + 1].Y - pl.Points[i].Y) * (pointClick.X - pl.Points[i].X)) /
                                            (Math.Sqrt(Math.Pow(pl.Points[i].X - pl.Points[i + 1].X, 2) +
                                             Math.Pow(pl.Points[i].Y - pl.Points[i + 1].Y, 2))));
                    if (!(aktDist < distance)) continue;
                    if (_project.FindingObjProps != null && !pl.ComparePropertyWith(_project.FindingObjProps)) continue;
                    distance = aktDist;
                    if (distance < 5) //ci oznacime ciaru alebo nie cize ci je vzdialenost mensia ako definovana min vzdialenost
                    {
                        selObj = pl;
                        break;
                    }
                }
            }
            return selObj;
        }

        /// <summary>
        /// Odznacenie vybraneho objektu.
        /// Oznaceny je zvyrazneny.
        /// </summary>
        private void UnselectObject()
        {
            SavePropertyFor(_selectedObj);
            _selectedObj = null;
            _gpMark.Reset();
        }

        /// <summary>
        /// Oznacenie zadaneho objektu
        /// </summary>
        /// <param name="drObj"></param>
        private void SelectObject(DrawObject drObj)
        {
            if (drObj == null)
            {
                UnselectObject();
            }
            else
            {
                
                //RefreshComboBox();
                SaveProperties();
                //SavePropertyFor(_selectedObj);
                _selectedObj = drObj;
                MarkSelected(_selectedObj);
            }
        }

        /// <summary>
        /// Obsluženie akcie kliknutia na nástroj vymazania objektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, EventArgs e)
        {
            if (_project.CurrentImage == null || _selectedObj == null) return;
            imgBoxSelectedObj.Image = null;
            if (typeof(BoundingBox) == _selectedObj.GetType())
                DeleteBB();
            else if (typeof(Painting) == _selectedObj.GetType())
                DeletePA();
            else if (typeof(Polyline) == _selectedObj.GetType())
                DeletePL();
            if(_activeTool == EToolItem.Find)_project.FindObjectOnImage();
            RefreshComboBox();
        }



        /// <summary>
        /// Odstránenie vybraného BB.
        /// </summary>
        private void DeleteBB()
        {
            if (_project.CurrentImage.BoundBoxes.Remove(((BoundingBox)_selectedObj)))
            {
                _gpMark = new GraphicsPath();
                if (_project.CurrentImage.BoundBoxes.Any() && _activeTool != EToolItem.Find) SelectObject(_project.CurrentImage.BoundBoxes.Last());
                else
                {
                    if (_trackWindow != null)
                    {
                        _trackWindow.SelectObject(null);
                    }
                }
                if (_trackWindow != null)
                {
                    _trackWindow.ResetCheckboxesItems();
                }
                RedrawBB();
                imageBox.Refresh();
            }
        }

        /// <summary>
        /// Odstránenie vybraného PA.
        /// </summary>
        private void DeletePA()
        {
            if (_project.CurrentImage.Paintings.Remove(((Painting)_selectedObj)))
            {
                _gpMark = new GraphicsPath();
                if (_project.CurrentImage.Paintings.Any() && _activeTool != EToolItem.Find) SelectObject(_project.CurrentImage.Paintings.Last());
                RedrawPA();
                imageBox.Refresh();
            }
        }

        /// <summary>
        /// Odstránenie vybraného PA.
        /// </summary>
        private void DeletePL()
        {
            if (_project.CurrentImage.Polylines.Remove(((Polyline)_selectedObj)))
            {
                _gpMark = new GraphicsPath();
                if (_project.CurrentImage.Polylines.Any() && _activeTool != EToolItem.Find) SelectObject(_project.CurrentImage.Polylines.Last());
                RedrawPL();
                imageBox.Refresh();
            }
        }

        /// <summary>
        /// Zmena panela s vlastnostami pri zvoleni triedy z komboboxu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePropertyForClass(object sender, EventArgs e)
        {
            _project.MyClasses.CreatePropertyForClass((ComboBoxItem) cmbClass.SelectedItem);
            _project.SelectUserDefClass(cmbClass.Text); 
        }        

        /// <summary>
        /// Vytvorenie vlastnej triedy objektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClassClick(object sender, EventArgs e)
        {
            if(new AddClassForm(false).ShowDialog() == DialogResult.OK)
                _project.MyClasses.GenerateClass(cmbClass);
        }

        /// <summary>
        /// Uprava zvolenej triedy. (nazov, ikonka, vlastnosti)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClassClick(object sender, EventArgs e)
        {
            if (cmbClass.SelectedItem != null)
            {
                var ada = new AddClassForm(true);
                string selectedClass = cmbClass.Text;
                ada.MyInitialize(selectedClass);

                if (ada.ShowDialog() == DialogResult.OK)
                {
                    _project.MyClasses.GenerateClass(cmbClass);

                    foreach (ComboBoxItem item in cmbClass.Items.Cast<ComboBoxItem>().Where(item => item.Text == selectedClass))
                    {
                        cmbClass.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie je vybrana ziadna trieda pre upravu.", "Ziaden vyber", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Vykreslovanie BB, PA, PL na platno s obrazkom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if(_project == null || imageBox.Image == null)return;

                if (_btm == null || _btm.Width != imageBox.Image.Bitmap.Width || _btm.Height != imageBox.Image.Bitmap.Height)
                {
                    _btm = new Bitmap(imageBox.Image.Bitmap.Width, imageBox.Image.Bitmap.Height);
                    _graphics = Graphics.FromImage(_btm);
                }

                if (_project.CurrentImage == null) return;

                if(_project.ProjOptions.ShowGuideLine)
                {
                    e.Graphics.DrawPath(_project.ProjOptions.GetGuideLinePen(_project.CurrentImage.Zooom), _gpGuideLine);//kreslenie vodiacich ciar
                    if(_redrawOnlyGuideLine && !_creatingPL)//refresh sa volal len pre prekreslenie vodiacich ciar
                    {
                        e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime obrazok
                        _redrawOnlyGuideLine = false;
                        return;
                    }   
                }

                if (_crPA.Count > 0)//kreslenie PA ktory sa prave vytvara
                {
                    e.Graphics.FillPolygon(_project.ProjOptions.CreatingPAColor, _crPA.ToArray(), FillMode.Winding);
                    e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime vrstvu nad obrazok so vsetkými BB,PA,PL
                    return;
                }

                if (_crPL.Count > 1)//kreslenie PL ktory sa prave vytvara
                {
                    e.Graphics.DrawLines(_project.ProjOptions.GetCreatingPLPen(_project.CurrentImage.Zooom), _crPL.ToArray());
                    e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime vrstvu nad obrazok so vsetkými BB,PA,PL
                    return;
                }

                if (!_crBB.IsEmpty)//kreslenie BB ktory sa prave vytvara
                {
                    e.Graphics.FillRectangle(_project.ProjOptions.CreatingBBColor, _crBB);
                    e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime vrstvu nad obrazok so vsetkými BB,PA,PL
                    return;     
                }

                if (_project.FindingObjProps != null && _project.GpFinding != null)
                {
                    _graphics = Graphics.FromImage(_btm);
                    _graphics.Clear(Color.FromArgb(0, 255, 255, 255)); //vytvorime transparentnu vrstvu nad obrazkom
                    _graphics.DrawPath(_project.ProjOptions.GetFoundObjPen(_project.CurrentImage.Zooom), _project.GpFinding); //nakreslenie vyhladanych objektov
                    _graphics.DrawPath(_project.ProjOptions.GetSelectedObjectPen(_project.CurrentImage.Zooom), _gpMark); //nakreslenie oznaceneho objektu(BB,PA,PL)
                    e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime vrstvu nad obrazok so vsetkými BB,PA,PL
                    _graphics.Dispose();
                    return;
                }

                _graphics = Graphics.FromImage(_btm); //vytvorime graphics z vrstvy _btm
                _graphics.Clear(Color.FromArgb(0, 255, 255, 255)); //vycistime vrstvu _btm a vytvorime z nej transparentnu vrstvu nad obrazkom
                _graphics.DrawPath(_project.ProjOptions.GetCreatedBBPen(_project.CurrentImage.Zooom), _gpBB); //nakreslenie BBs na trans. vrstvu _btm
                _graphics.DrawPath(_project.ProjOptions.GetCreatedPLPen(_project.CurrentImage.Zooom), _gpPL); //nakreslenie PLs na trans. vrstvu _btm
                _graphics.FillRegion(_project.ProjOptions.GetCreatedPAPen(1).Brush, new Region(_gpPA)); //nakreslenie PAs na trans. vrstvu _btm
                _graphics.DrawPath(_project.ProjOptions.GetCreatedPAPen(_project.CurrentImage.Zooom), _gpPABB); //nakreslenie boundingboxov paintingov na trans. vrstvu _btm
                _graphics.DrawPath(_project.ProjOptions.GetSelectedObjectPen(_project.CurrentImage.Zooom), _gpMark); //nakreslenie oznaceneho objektu(BB,PA,PL)  na trans. vrstvu _btm
                //DrawClassIds();
                e.Graphics.DrawImageUnscaled(_btm, 0, 0);//vykreslime vrstvu nad obrazok so vsetkými BB,PA,PL
                _graphics.Dispose();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message,"MainWindowApplication.ImageBoxPaint()",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }

        /// <summary>
        /// Zobrazenie Id tried pre BB
        /// </summary>        
        private void DrawClassIds()
        {
            /*if (chbIds.Checked)
            {
                var bbs = _project.CurrentImage.BoundBoxes;
                float x = 0.0F;
                float y = 0.0F;
                string cName = "";
                float rectPlus;
                if (_project.ProjOptions.ClassFont.PenWidth > 19)
                {
                    rectPlus = 6;
                }
                else
                {
                    rectPlus = 4;
                }

                for (int i = 0; i < bbs.Count; i++)
                {
                    x = bbs[i].PointA.X+2;
                    y = bbs[i].PointA.Y+2;
                    cName = ""+bbs[i].Properties.Class;                    
                    //RectangleF rectF1 = new RectangleF(x, y, bbs[i].PointB.X-x, 14);
                    //_graphics.FillRectangle(new SolidBrush(Color.Red), rectF1);
                    //_graphics.DrawString(cName, new Font("Arial", 10), new SolidBrush(Color.Black), rectF1);
                    //_graphics.DrawRectangle(Pens.Red, Rectangle.Round(rectF1));

                    RectangleF rectF1 = new RectangleF(x, y, bbs[i].PointB.X-x, _project.ProjOptions.ClassFont.PenWidth+rectPlus);                    
                    _graphics.FillRectangle(new SolidBrush(_project.ProjOptions.RectColor), rectF1);                    
                    _graphics.DrawString(cName, new Font("Arial", _project.ProjOptions.ClassFont.PenWidth), new SolidBrush(_project.ProjOptions.ClassFont.PenColor), rectF1);
                    //_graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));
                    Pen drawPen = new Pen(_project.ProjOptions.RectColor);
                    _graphics.DrawRectangle(drawPen, Rectangle.Round(rectF1));
                }
            }*/
        }

        private void ChbIds_CheckedChanged(Object sender, EventArgs e)
        {
            RedrawBB();
            imageBox.Refresh();
        }

        /// <summary>
        /// Skopirovanie vsetkych BB z predosleho snimku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyBbsFrom(object sender, EventArgs e)
        {
            if (_copyBB)
            {
                _copyBB = false;
                _project.CurrentImage.VScroll = imageBox.VerticalScrollBar.Value;
                _project.CurrentImage.HScroll = imageBox.HorizontalScrollBar.Value;
                _project.PrecedingFigure();
                UnselectObject();
                ResetClassControls();
                SetImage2ImageBox(_project.CurrentImage);
            }
        }

        /// <summary>
        /// Vymazanie vsetkych BB v snimku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAllBbsFrom(object sender, EventArgs e)
        {
            _project.CurrentImage.VScroll = imageBox.VerticalScrollBar.Value;
            _project.CurrentImage.HScroll = imageBox.HorizontalScrollBar.Value;
            _project.CurrentImage.DeleteAllBbs();
            UnselectObject();
            ResetClassControls();
            SetImage2ImageBox(_project.CurrentImage);
            if (_trackWindow != null)
            {
                _trackWindow.SelectObject(null);
                _trackWindow.ResetCheckboxesItems();
            }
        }
        
        /// <summary>
        /// Ukoncenie prace s obrazkom a zavretie aktualneho obrazka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCurrentToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!_project.OpenImages.Any()) return; 

            int indexOfdelete = _project.OpenImages.IndexOf(_project.CurrentImage);
            //pnlThumbs.Controls.RemoveAt(indexOfdelete);
            if (_project.OpenImages.Count == 1)//ak je len jeden obrazok tak ho vymazeme.
            {
                _project.CurrentImage = null;
                imageBox.Image = null;
                cmbAllObject.Items.Clear();
                EnableTools(false);
            }
            else
            {
                if (_project.CurrentImage != _project.OpenImages.Last())    //  Ak vymazavany nieje posledy, nahrad ho nasledovnikom --pomale lebo treba presunut vsetky thumby
                {                                                            
                    //pnlThumbs.Controls[pnlThumbs.Controls.Count-1].SetBounds(5 + 115 * (indexOfdelete), 5, 110, 85);
                    _project.ChangePicture(_project.OpenImages[indexOfdelete + 1]);     //  Nahrada vymazaneho nasledovnikom
                }
                else _project.ChangePicture(_project.OpenImages[indexOfdelete - 1]);    //  Nahrada vymazaneho predchodcom

                RefreshComboBox();
            }

            _project.OpenImages.RemoveAt(indexOfdelete);
            SetImage2ImageBox(_project.CurrentImage);
            CreateActuallThumbs(indexOfdelete);
            if (_project.CurrentImage != null)
            {
                //SetZoomAndScroll();
                SetStatusBar(_project.OpenImages.IndexOf(_project.CurrentImage)+1, _project.OpenImages.Count);
            }
            else SetStatusBar(0, _project.OpenImages.Count);
        }

        /// <summary>
        /// Prekreslenie platna pri zmene velkosti okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1Resize(object sender, EventArgs e)
        {
            if (_project == null || _project.CurrentImage == null) return;
            SetMaxOfScrollBar();
            imageBox.Refresh();
        }
        
        /// <summary>
        /// Stlacenie tlacidla mysky.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (_project == null || _project.CurrentImage == null) return;
            if (e.Button != MouseButtons.Middle)
            {
                if (_activeTool == EToolItem.Painting) StartCreatePA(e);
                else if (_activeTool == EToolItem.BoundingBox) StartCreateBB(e);
                else if ((_activeTool == EToolItem.Cursor || _activeTool == EToolItem.Find) && imageBox.Cursor == Cursors.SizeNWSE)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        _movingBB = true;
                    }
                    StartEditingBB(e);
                }
                else if (_activeTool == EToolItem.Polyline) // je vybraty nastroj PL
                {
                    if (_creatingPL)//uz sa vytvara nejaky PL to znamena ze pridame bod k nemu
                        CreatingPL(e);
                    else//inak sa zacina vytvarat novy
                        StartCreatePL(e);
                }
            }
            else 
            {
                Cursor.Current = Cursors.SizeAll;
                _scrollButtonPressed = true;                
            }            
        }

        /// <summary>
        /// Uvolnenie tlacidla mysky.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (_project == null || _project.CurrentImage == null) return;
            if (e.Button != MouseButtons.Middle)
            {
                if (_activeTool == EToolItem.Painting)
                {
                    EndCreatePA();
                    //SaveProperties();
                }
                else if (_activeTool == EToolItem.BoundingBox)
                {
                    /*if(*/
                    EndCreateBB(e); //)
                    //SaveProperties();
                }
                else if ((_activeTool == EToolItem.Cursor || _activeTool == EToolItem.Find) && _editingBB)
                {
                    EndEditingBB(e);
                    _movingBB = false;
                    if (_activeTool == EToolItem.Find) _project.FindObjectOnImage();
                    imageBox.Cursor = Cursors.Default;
                }
            }
            else
            {
                _scrollButtonPressed = false;
                SetZoom(Math.Round(_project.CurrentImage.Zooom+0.1, 1));
                SetZoom(Math.Round(_project.CurrentImage.Zooom-0.1, 1));                
            }
            
        }

        /// <summary>
        /// Pohyb mysky.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (_project == null || _project.CurrentImage == null) return;
            //vytvorenie vodiacich ciar, potrebne nakreslit v pripade ze sa kresli BB alebo PA tak sa kresli
            //lebo sa vola refresh ale ckceme aj ked nie je kliknute vidiet vodiace ciary
            //CreateGuideLines(e);
              
            
            if (_scrollButtonPressed)
            {
                MoveScrollBar(e);
                
            }
            else
            {
                CreateGuideLines(e);

                if (_activeTool == EToolItem.BoundingBox && _mouseDown)    //  Zvoleny nastroj pre vytvatanie BB a stlacene tlacidlo mysi - kresli sa objekt
                    CreatingBB(e);              //  vykreslovanie bb
                else if (_activeTool == EToolItem.Painting && _mouseDown)   //  Zvoleny nastroj pre vytvatanie PA a stlacene tlacidlo mysi - kresli sa objekt
                    CreatingPA(e);                  //  vykreslovanie paintingu
                else if ((_activeTool == EToolItem.Cursor || _activeTool == EToolItem.Find) && _selectedObj != null) //  Zvoleny nastroj Kurzor a bol vybrany nejaky objekt
                {
                    if ((!_editingBB) && (typeof(BoundingBox) == _selectedObj.GetType()))
                    {
                        //  Ak sa needituje velkost BB - nebolo kliknute na jeden z rohov BB
                        //  Vytvorenie pravouholnikov (lavy horny a pravy dolny roh vybraneho BB)
                        var bb = (BoundingBox)_selectedObj;
                        var a = ZoomAndScrollPoint(bb.PointA);
                        var b = ZoomAndScrollPoint(bb.PointB);
                        var r1 = new Rectangle(a, new Size(5, 5));
                        var r2 = new Rectangle(b.X - 5, b.Y - 5, 5, 5);

                        //  Kontrola, ci sa kurzor nachadza nad niektorym z tychto rohov, ak ano - Kurzor nad rohom (zmena typu kurzora), Inak defaultny kurzor 
                        imageBox.Cursor = r1.Contains(e.Location) || r2.Contains(e.Location) ? Cursors.SizeNWSE : Cursors.Default;
                    }
                    else if (_editingBB)
                    {
                        CreatingBB(e); //Ak editujeme BB - prekreslovanie BB
                        return;
                    }
                    _redrawOnlyGuideLine = true;
                    imageBox.Refresh();
                }
                else //chceme vykreslit vodiace ciary lebo nebude refresnute platno
                {
                    _redrawOnlyGuideLine = true;
                    imageBox.Refresh();
                }
            } 
            
        }

        private void CreateGuideLines(MouseEventArgs e)
        {
            _gpGuideLine.Reset();
            using (var gp = new GraphicsPath())
            {
                var eNorm = UnzoomAndUnscrollPoint(e.Location);
                var newPointNorm = UnzoomAndUnscrollPoint(new Point(imageBox.ClientSize.Width, imageBox.ClientSize.Height));
                gp.AddLine(_project.CurrentImage.HorizontScroll, eNorm.Y, newPointNorm.X, eNorm.Y);
                _gpGuideLine.AddPath(gp, false);
                gp.Reset();
                gp.AddLine(eNorm.X, _project.CurrentImage.VerticalScroll, eNorm.X, newPointNorm.Y);
                _gpGuideLine.AddPath(gp, false);
            }
        }

        private void MoveScrollBar(MouseEventArgs e)
        {            
            if (imageBox.HorizontalScrollBar.Maximum >= e.X && imageBox.HorizontalScrollBar.Minimum <= e.X)
                imageBox.HorizontalScrollBar.Value = e.X;
            if (imageBox.VerticalScrollBar.Maximum >= e.Y && imageBox.VerticalScrollBar.Minimum <= e.Y)
                imageBox.VerticalScrollBar.Value = e.Y;
            imageBox.Refresh();
        }

        /// <summary>
        /// Obsluha dvojkliknutia - ukoncenie kreslenia PL ak sa kresli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBoxDoubleClick(object sender, EventArgs e)
        {
            if (_activeTool == EToolItem.Polyline && _creatingPL) // ak sa vytvara PL a pride dvojklik to znamena ukoncenie vytvarania PL
            {
                EndCreatePL();
            }
        }

        /// <summary>
        /// Vytvorenie noveho paintingu.
        /// </summary>
        private void StartCreatePA(MouseEventArgs e)
        {
            _mouseDown = true;
            UnselectObject();
            imageBox.Refresh();
            _project.CurrentImage.AddPainting(new Point(e.X, e.Y), imageBox.Image.Size.Width, imageBox.Image.Size.Height);
        }

        /// <summary>
        /// Vytvaranie PA.
        /// </summary>
        /// <param name="e"></param>
        private void CreatingPA(MouseEventArgs e)
        {
            var actuall = UnzoomAndUnscrollPoint(e.Location);

            if (actuall.X < 0) actuall.X = 0;
            if (actuall.Y < 0) actuall.Y = 0;
            if (actuall.X > imageBox.Image.Size.Width) actuall.X = imageBox.Image.Size.Width;
            if (actuall.Y > imageBox.Image.Size.Height) actuall.Y = imageBox.Image.Size.Height;

           _project.CurrentImage.Paintings.Last().AddPoints(actuall);
           _crPA.Add(actuall);
            imageBox.Refresh();
        }

        /// <summary>
        /// Odzoomovanie a odscrollovanie daneho bodu. Cize suradnice bodu vydelime zoomom a pricitame dany scroll.
        /// Zmena suradnic bodu podla zoomu a scrollu.
        /// </summary>
        /// <returns></returns>
        private Point UnzoomAndUnscrollPoint(Point p)
        {            
            return new Point((int) Math.Floor(p.X / _project.CurrentImage.Zooom + _project.CurrentImage.HorizontScroll),
                             (int) Math.Floor(p.Y / _project.CurrentImage.Zooom + _project.CurrentImage.VerticalScroll));
        }

        /// <summary>
        /// Zoomovanie a scrollovanie daneho bodu. Cize od suradnic bodu odcitame scroll a vynasobime zoomom.
        /// Zmena suradnic bodu podla zoomu a scrollu.
        /// </summary>
        /// <returns></returns>
        private Point ZoomAndScrollPoint(Point p)
        {
            return new Point((int)((p.X - _project.CurrentImage.HorizontScroll) * _project.CurrentImage.Zooom),
                             (int)((p.Y - _project.CurrentImage.VerticalScroll) * _project.CurrentImage.Zooom));
        }

        /// <summary>
        /// Skoncenie malovania PA.
        /// </summary>
        private void EndCreatePA()
        {
            if(!_mouseDown)return;
            if (_project.CurrentImage.Paintings.Last().Points.Count < 3)
            {
                _project.CurrentImage.Paintings.RemoveAt(_project.CurrentImage.Paintings.Count - 1);
                _mouseDown = false;
                _crPA = new List<Point>();
                return;
            }
            var pa = _project.CurrentImage.Paintings.Last();
            SavePropertyFor(pa);
            //SelectObject(_project.CurrentImage.Polylines.Last());
            _selectedObj = pa;
            RefreshComboBox();
            MarkSelected(_selectedObj);
            //RefreshComboBox();
            //SelectObject(_project.CurrentImage.Paintings.Last());
 //           MarkSelected(_selectedObj);
            //RefreshComboBox();
            _mouseDown = false;
            _crPA = new List<Point>();
            RedrawPA();
            imageBox.Refresh();
        }

        /// <summary>
        /// Nakreslenie vsetkych Paintingov obrazku.
        /// </summary>
        private void RedrawPA()
        {
            if (_project.CurrentImage == null) return;
            _gpPA.Reset();
            _gpPA.FillMode = FillMode.Winding;
            _gpPABB.Reset();
            foreach (Painting paint in _project.CurrentImage.Paintings)
            {
                if (!paint.Points.Any()) continue;
                _gpPA.AddPolygon(paint.Points.ToArray());
                _gpPABB.AddRectangle(paint.BoundingBox.GetRectangle());
            }
        }

        /// <summary>
        /// Vytvorenie novej polyline.
        /// </summary>
        /// <param name="e"></param>
        private void StartCreatePL(MouseEventArgs e)
        {
            _creatingPL = true;
            imageBox.Cursor = Cursors.Cross;
            UnselectObject();
            imageBox.Refresh();
            _project.CurrentImage.AddPolyLine(new Point(e.X, e.Y), imageBox.Image.Size.Width, imageBox.Image.Size.Height);
            _crPL.Add(_project.CurrentImage.Polylines.Last().Points.First());
        }

        /// <summary>
        /// Vytvaranie PL.
        /// </summary>
        /// <param name="e"></param>
        private void CreatingPL(MouseEventArgs e)
        {
            Point actuall = UnzoomAndUnscrollPoint(e.Location);

            if (actuall.X < 0) actuall.X = 0;
            if (actuall.Y < 0) actuall.Y = 0;
            if (actuall.X > imageBox.Image.Size.Width) actuall.X = imageBox.Image.Size.Width;
            if (actuall.Y > imageBox.Image.Size.Height) actuall.Y = imageBox.Image.Size.Height;

            _project.CurrentImage.Polylines.Last().Points.Add(actuall);
            _crPL.Add(actuall);
            imageBox.Refresh();
        }

        /// <summary>
        /// Skoncenie malovania PL.
        /// </summary>
        private void EndCreatePL()
        {
            try
            {
                imageBox.Cursor = Cursors.Arrow;
                if (!_creatingPL) return;
                if (_project.CurrentImage.Polylines.Last().Points.Count < 2)
                {
                    _project.CurrentImage.Polylines.RemoveAt(_project.CurrentImage.Polylines.Count - 1);
                    _creatingPL = false;
                    _crPL = new List<Point>();
                    imageBox.Refresh();
                    return;
                }
                var pl = _project.CurrentImage.Polylines.Last();
                SavePropertyFor(pl);
                //SelectObject(_project.CurrentImage.Polylines.Last());
                _selectedObj = pl;
                RefreshComboBox();
                MarkSelected(_selectedObj);
                
                //_creatingPL = false;
                _crPL = new List<Point>();
                RedrawPL();
                imageBox.Refresh();
                _creatingPL = false;
  //              SaveProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: MainWindowApplication.EndCreatePL()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// Nakreslenie vsetkych Polylines obrazku.
        /// </summary>
        private void RedrawPL()
        {
            if (_project.CurrentImage == null) return;
            _gpPL.Reset();
            GraphicsPath gp = new GraphicsPath();
            foreach (Polyline polyline in _project.CurrentImage.Polylines)
            {
                if (!polyline.Points.Any()) continue;
                gp.AddLines(polyline.Points.ToArray());
                _gpPL.AddPath(gp, false);
                gp.Reset();
            }
            gp.Dispose();
        }

        /// <summary>
        /// Ziskanie prveho bodu, ktory bude tvorit boundingbox.
        /// </summary>
        /// <param name="e"></param>
        private void StartCreateBB(MouseEventArgs e)
        {
            _mouseDown = true;
            UnselectObject();
            imageBox.Refresh();
            _project.CurrentImage.AddBoundingBox(new Point(e.X, e.Y));   
        }

        /// <summary>
        /// Prepocitavanie druheho bodu pri tahani mysou pri vytvarani / editovani BB.
        /// Volanie metody prekreslovania - "animacia".
        /// </summary>
        /// <param name="e"></param>
        private void CreatingBB(MouseEventArgs e)
        {
            Point eNorm = UnzoomAndUnscrollPoint(e.Location);
            Point A;

            if (_mouseDown)
                A = _project.CurrentImage.BoundBoxes[_project.CurrentImage.BoundBoxes.Count - 1].PointA;
            else
            {
                A = ((BoundingBox)_selectedObj).PointA == _selectPoint ? ((BoundingBox)_selectedObj).PointB : ((BoundingBox)_selectedObj).PointA;
            }

            Point start = A;

            int a, b = 0;
            if (!_movingBB)
            {
                a = Math.Abs(A.X - eNorm.X);
                b = Math.Abs(A.Y - eNorm.Y);                

                if (eNorm.X < A.X) start.X = start.X - a;
                if (eNorm.Y < A.Y) start.Y = start.Y - b;
            }
            else
            {
                a = ((BoundingBox)_selectedObj).Size.Width;
                b = ((BoundingBox)_selectedObj).Size.Height;

                if (((BoundingBox)_selectedObj).PointA == _selectPoint)
                {
                    start.X = eNorm.X;
                    start.Y = eNorm.Y;
                }
                else
                {
                    start.X = eNorm.X - a;
                    start.Y = eNorm.Y - b;
                }
            }          

            _crBB = new Rectangle(start.X, start.Y, a, b);
            imageBox.Refresh();
        }

        /// <summary>
        /// kontrola ci uzivatel nevytvoril BB ktory zasahuje mimo obrazok, 
        /// ak ano tak prestavenie jeho bodou na hranicne body obrazku.
        /// </summary>
        /// <param name="bb"></param>
        private void CheckBBLocation(ref BoundingBox bb)
        {
            if (bb.PointB.X < 0) bb.PointB = new Point(0, bb.PointB.Y);
            if (bb.PointB.Y < 0) bb.PointB = new Point(bb.PointB.X, 0);
            if (bb.PointA.X < 0) bb.PointA = new Point(0, bb.PointA.Y);
            if (bb.PointA.Y < 0) bb.PointA = new Point(bb.PointA.X, 0);
            Point tmp = new Point(imageBox.Image.Size.Width, imageBox.Image.Size.Height);
            if (bb.PointA.X > tmp.X) bb.PointA = new Point(tmp.X, bb.PointA.Y);
            if (bb.PointA.Y > tmp.Y) bb.PointA = new Point(bb.PointA.X, tmp.Y);
            if (bb.PointB.X > tmp.X) bb.PointB = new Point(tmp.X, bb.PointB.Y);
            if (bb.PointB.Y > tmp.Y) bb.PointB = new Point(bb.PointB.X, tmp.Y);
        }

        /// <summary>
        /// kontrola ci len uzivatel neklikol cize oba rohove body BB sa zhoduju(klikol, nepotiahol myskou a pustil)
        /// alebo ci sa X-ove alebo Y-ove body BB nezhoduju(cize je to vodorovna alebo zvisla ciara)
        /// ak ano odstranime tento BB
        /// </summary>
        /// <param name="bb"></param>
        /// <returns>true ak ma BB pripustnu velkost, inak false</returns>
        private bool CheckBBSize(BoundingBox bb)
        {
            if (bb.PointA.X == bb.PointB.X || bb.PointA.Y == bb.PointB.Y)
            {
                _project.CurrentImage.BoundBoxes.RemoveAt(_project.CurrentImage.BoundBoxes.Count - 1);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Ziskanie a zapisanie druheho bodu, ktory bude tvorit boundingbox.
        /// </summary>
        /// <param name="e"></param>
        private bool EndCreateBB(MouseEventArgs e)
        {
            if(!_mouseDown)return false;
            if (_project.CurrentImage.BoundBoxes.Any())
            {
                BoundingBox bb = _project.CurrentImage.BoundBoxes.Last();//[_project.CurrentImage.BoundBoxes.Count - 1];

                bb.PointB = UnzoomAndUnscrollPoint(e.Location);

                CheckBBLocation(ref bb);
                if (!CheckBBSize(bb))
                {
                    _mouseDown = false;
                    _crBB = new Rectangle();
                    
                    imageBox.Refresh();
                    return false;
                }
                SavePropertyFor(bb);
                //SelectObject(_project.CurrentImage.Polylines.Last());
                _selectedObj = bb;
                RefreshComboBox();
                MarkSelected(_selectedObj);
                //RefreshComboBox();
                //SelectObject(bb);
//                MarkSelected(_selectedObj);
                //RefreshComboBox();
                _mouseDown = false;
                _crBB = new Rectangle();
                RedrawBB();
                imageBox.Refresh();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Editovanie existujuceho BB.
        /// </summary>
        /// <param name="e"></param>
        private void StartEditingBB(MouseEventArgs e)
        {
            _editingBB = true;

            var bb = (BoundingBox) _selectedObj;    //  Pretypovanie DrawObjectu na BB

            if (Math.Abs(ZoomAndScrollPoint(bb.PointA).Y - e.Y) < Math.Abs(ZoomAndScrollPoint(bb.PointB).Y - e.Y))
            {   //  kliklo sa na A (lavy horny)
                _selectPoint = bb.PointA;
            }
            else // kliklo sa na B (pravy dolny)
                _selectPoint = bb.PointB;
        }

        /// <summary>
        /// Ulozenie zmeny rozmerov modifikujuceho BB
        /// </summary>
        /// <param name="e"></param>
        private void EndEditingBB(MouseEventArgs e)
        {
            var bb = (BoundingBox)_selectedObj;    //  Pretypovanie DrawObjectu na BB

            var endPoint = UnzoomAndUnscrollPoint(e.Location);

            if (!_movingBB)
            {
                if (bb.PointA == _selectPoint)
                {
                    bb.PointA = endPoint;
                    bb.PointB = bb.PointB;  //  Kvoli vykonani pripadnej vymeny bodov A (lavy horny) a B (pravy dolny),
                }                           //  ak je bod B lavsi a/alebo vyssi ako A. 
                else bb.PointB = endPoint;
            }
            else
            {

                if (bb.PointA == _selectPoint)
                {
                    bb.PointB = new Point(endPoint.X + ((BoundingBox)_selectedObj).Size.Width, endPoint.Y + ((BoundingBox)_selectedObj).Size.Height); 
                    bb.PointA = endPoint;                    
                }                           
                else
                {
                    bb.PointA = new Point(endPoint.X - ((BoundingBox)_selectedObj).Size.Width, endPoint.Y - ((BoundingBox)_selectedObj).Size.Height); 
                    bb.PointB = endPoint;
                }
            }

            CheckBBLocation(ref bb);

            _editingBB = false;
            _crBB = new Rectangle();
            MarkSelected(bb);
            RedrawBB();
            imageBox.Refresh();
        }

        /// <summary>
        /// Naplnenie graphics path vsetkymi boundingboxami
        /// </summary>
        private void RedrawBB()
        {
            if (_project.CurrentImage == null) return;

            _gpBB.Reset();

            if (!chbIds.Checked)
            {
                foreach (BoundingBox bb in _project.CurrentImage.BoundBoxes)    //  odstranit cyklus !!!
                {
                    //if ((_tools[1] && _mouseDown) && bb == _project.CurrentImage.BoundBoxes.Last()) break;
                    if (_editingBB && bb == _selectedObj) continue;                    
                    _gpBB.AddRectangle(bb.GetRectangle());
                }
            }
            else
            {
                foreach (BoundingBox bb in _project.CurrentImage.BoundBoxes)
                {                    
                    if (_editingBB && bb == _selectedObj) continue;
                    if (CheckTrackOfBB(bb))//pridane
                    {
                        _gpBB.AddRectangle(bb.GetRectangle());
                    }
                    else
                    {
                        if (bb.Equals(_selectedObj))
                        {
                            SavePropertyFor(_selectedObj);
                            _selectedObj = null;
                            _gpMark.Reset();
                        }

                    }
                }
            }
        }

        private bool CheckTrackOfBB(BoundingBox bb)
        {
            var trackIdIndex = Array.IndexOf(bb.Properties.AtributesName, "track_id");
            if (trackIdIndex < 0)
                return true;            
            if (bb.Properties.AtributesValue[trackIdIndex] == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Vyznacenie Draw objectu ktorý vybral uzivatel.
        /// </summary>
        /// <param name="markObj"></param>
        private void MarkSelected(DrawObject markObj)
        {
            _gpMark.Reset();
            imgBoxSelectedObj.Image = null;
            imgBoxSelectedObj.Enabled = false;

            if (typeof (BoundingBox) == markObj.GetType())
            {
                var markBB = (BoundingBox) markObj;
                var r1 = new Rectangle(markBB.PointA.X, markBB.PointA.Y, 5, 5);
                var r2 = new Rectangle(markBB.PointB.X - 5, markBB.PointB.Y - 5, 5, 5);
                _gpMark.AddRectangles(new[] {r1, r2});
                SetSelectedDrawObjToPanel(markBB);
            }
            else if (typeof (Painting) == markObj.GetType())
            {
                _gpMark.AddPolygon((markObj as Painting).Points.ToArray());
                _gpMark.AddRectangle((markObj as Painting).BoundingBox.GetRectangle());
            }
            else if (typeof(Polyline) == markObj.GetType())
            {
                _gpMark.AddLines(((markObj as Polyline).Points.ToArray()));
            }
        }

        /// <summary>
        /// Vykreslenie objektu na panel detailu
        /// </summary>
        /// <param name="markBB"></param>
        private void SetSelectedDrawObjToPanel(BoundingBox markBB)
        {
            try
            {
                if(cbShowSelObj.Checked)
                    using (var img = _project.CurrentImage.GetImage())
                    {
                        pnlSelObjOptions.Enabled = true;
                        var norSize = new Size();
                        var vysledok = (double)imgBoxSelectedObj.ClientSize.Width / markBB.Size.Width;
                        if (vysledok * markBB.Size.Height > imgBoxSelectedObj.ClientSize.Height)
                            vysledok = (double)imgBoxSelectedObj.ClientSize.Height / markBB.Size.Height;

                        norSize.Width = (int)Math.Ceiling(markBB.Size.Width * vysledok);
                        norSize.Height = (int)Math.Ceiling(markBB.Size.Height * vysledok);
                        imgBoxSelectedObj.Image = new Image<Bgr, byte>(
                            img.Copy(new Rectangle(markBB.PointA, markBB.Size))
                                    .ToBitmap(norSize.Width, norSize.Height));
                        GC.Collect();
                    }

                tbSelObjX.Text = markBB.PointA.X + "";
                tbSelObjY.Text = markBB.PointA.Y + "";
                tbSelObjWidth.Text = markBB.Size.Width + "";
                tbSelObjHeight.Text = markBB.Size.Height + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri MainWindowApplication.SetSelectedDrawObjToPanel() " + ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void ChShowSelObjChanged(object sender, EventArgs e)
        {
            if (!cbShowSelObj.Checked)
                imgBoxSelectedObj.Image = null;
            else if(_selectedObj != null && _selectedObj.GetType() == typeof(BoundingBox))
                SetSelectedDrawObjToPanel(_selectedObj as BoundingBox);
        }

        private void TbSelObjCheckValue(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back || e.KeyChar == (char)46; //46 je bodka
        }

        private void BtnSaveChangeSelOjClick(object sender, EventArgs e)
        {
            if(_selectedObj == null || !pnlSelObjOptions.Enabled)
                return;

            int x, y, width, height;
            if (!Int32.TryParse(tbSelObjX.Text, out x) || x < 0 || x > imageBox.Image.Size.Width ||
                !Int32.TryParse(tbSelObjY.Text, out y) || y < 0 || y > imageBox.Image.Size.Height ||
                !Int32.TryParse(tbSelObjWidth.Text, out width) || width <= 0 || x + width > imageBox.Image.Size.Width ||
                !Int32.TryParse(tbSelObjHeight.Text, out height) || height <= 0 || y + height > imageBox.Image.Size.Height)
            {
                MessageBox.Show("Nepodarilo sa ulozit zmeny. Zle vstupne hodnoty.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            try
            {
                var bb = _selectedObj as BoundingBox;
                if (bb == null) return;
                bb.PointA = new Point(x, y);
                bb.PointB = new Point(bb.PointA.X + width, bb.PointA.Y + height);

                UnselectObject();
                RedrawBB();
                SelectObject(bb);
 //               MarkSelected(bb);
                imageBox.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Form.BtnSaveChangeSelOjClick()-" + ex.Message, "Error", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }


        //////////////////////////////////////////////////////////////////////////////
        ////////////////       Z       O      O     O       M     ////////////////////
        //////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Zoomovanie (+).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn(object sender, EventArgs e)
        {
            SetZoom(Math.Round(_project.CurrentImage.Zooom + 0.2, 1));
        }

        /// <summary>
        /// Zoomovanie (-).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut(object sender, EventArgs e)
        {
            if (_project.CurrentImage.Zooom <= 0.2) return;
            SetZoom(Math.Round(_project.CurrentImage.Zooom - 0.2, 1));
            //SetZoom(Math.Round(1.0, 1));
        }

        /// <summary>
        /// Manualne nastavenie ZOOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                var strZoom = ((TextBox) sender).Text.Replace("%", "");
                var zoom = Convert.ToInt32(strZoom)/100.0;
                SetZoom(zoom);
            }

            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back;
        }

        private void SetZoom(double zoom, Point p = default(Point))
        {
            imageBox.SetZoomScale(zoom, p);
        }

        /// <summary>
        /// Zoomovanie kolieskom mysky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgeBoxOnMouseWheel(object sender, MouseEventArgs e)
        {
            if (_project == null || _project.CurrentImage == null) return;

            double scale;
            if (e.Delta > 0)
            {
                scale = 2.0;
            }
            else if (e.Delta < 0)
            {
                scale = 0.5;
            }
            else
                return;

            SetZoom(_project.CurrentImage.Zooom * scale, e.Location);
            CreateGuideLines(e);
        }

        private void ImageBoxOnOnZoomScaleChange(object sender, EventArgs eventArgs)
        {
            txbZoom.Text = imageBox.ZoomScale * 100 + "%";
            _project.CurrentImage.Zooom = imageBox.ZoomScale;
            SetMaxOfScrollBar();
            _project.CurrentImage.VerticalScroll = imageBox.VerticalScrollBar.Value;
            _project.CurrentImage.HorizontScroll = imageBox.HorizontalScrollBar.Value;
            imageBox.Refresh();
        }

        /// <summary>
        /// Automaticke nastavenie obrazka na velkost imageboxu
        /// </summary>
        /// <param name="imgHeihgt"></param>
        /// <param name="imgWidth"></param>
        /// <param name="boxHeight"></param>
        /// <param name="boxWidth"></param>
        private void AutoZoom(double imgHeihgt, double imgWidth, double boxHeight, double boxWidth)
        {            
            if (chbRescale.Checked)
            {
                double zoomNum, hNum, wNum = 0;
                
                if (imgHeihgt > boxHeight && imgWidth > boxWidth)
                {
                    hNum = boxHeight / imgHeihgt;
                    wNum = boxWidth / imgWidth;
                    if (hNum < wNum) 
                    {
                        zoomNum = hNum;
                    }
                    else
                    {
                        zoomNum = wNum;
                    }
                }
                else if (imgHeihgt > boxHeight)
                {
                    zoomNum = boxHeight / imgHeihgt;    
                }
                else if (imgWidth > boxWidth)
                {
                    zoomNum = boxWidth / imgWidth;
                }
                else 
                {
                    hNum = boxHeight / imgHeihgt;
                    wNum = boxWidth / imgWidth;
                    if (hNum < wNum)
                    {
                        zoomNum = hNum;
                    }
                    else
                    {
                        zoomNum = wNum;
                    }
                }                                               
                _project.CurrentImage.Zooom = Math.Floor(zoomNum * 100) / 100;
            }
        }

        public void FitZoom(object sender, EventArgs e)
        {            
            double imgHeihgt = (double)imageBox.Image.Size.Height;
            double imgWidth = (double)imageBox.Image.Size.Width;
            double boxHeight = (double)imageBox.Height;
            double boxWidth = (double)imageBox.Width;
            if (chbRescale.Checked)
            {
                double zoomNum, hNum, wNum = 0;

                if (imgHeihgt > boxHeight && imgWidth > boxWidth)
                {
                    hNum = boxHeight / imgHeihgt;
                    wNum = boxWidth / imgWidth;
                    if (hNum < wNum)
                    {
                        zoomNum = hNum;
                    }
                    else
                    {
                        zoomNum = wNum;
                    }
                }
                else if (imgHeihgt > boxHeight)
                {
                    zoomNum = boxHeight / imgHeihgt;
                }
                else if (imgWidth > boxWidth)
                {
                    zoomNum = boxWidth / imgWidth;
                }
                else
                {
                    hNum = boxHeight / imgHeihgt;
                    wNum = boxWidth / imgWidth;
                    if (hNum < wNum)
                    {
                        zoomNum = hNum;
                    }
                    else
                    {
                        zoomNum = wNum;
                    }
                }                                
                SetZoom(Math.Floor(zoomNum * 100) / 100);
            }
        }

        private void RememberZoomFromPrevious()
        {
            if (chbRememberZoom.Checked)
            {
                _project.CurrentImage.Zooom = _hlpZoom;
                _project.CurrentImage.VScroll = _hlpVScroll;
                _project.CurrentImage.HScroll = _hlpHScroll;
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        ////////////////          T  O  O  L       B  A  R        ////////////////////
        //////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Zvyraznenie pri prechode cez ikonu nastroja.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighlightIcon(object sender, EventArgs e)
        {
            if (((Panel) sender).BackColor == Color.SteelBlue) return;
            var toolPanel = ((Panel)sender);
            toolPanel.BackColor = Color.Violet;
            var toolTip1 = new ToolTip();
            toolTip1.SetToolTip(toolPanel, toolPanel.AccessibleDescription);
        }

        /// <summary>
        /// Reset farby po vystupe z ikony nastroja.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetIconBack(object sender, EventArgs e)
        {
            if (((Panel)sender).BackColor != Color.SteelBlue) ((Panel)sender).ResetBackColor();
            if (((Panel)sender) == pnlCopyBB)
            {
                _copyBB = true;
            }
        }

        /// <summary>
        /// Reset podfarbenia vsetkych ikon v panely nastrojov.
        /// </summary>
        private void UnselectToolsIcon()
        {
            foreach (var p in from object p in pnlTools.Controls where p.GetType() == typeof(Panel) select p)
            {
                ((Panel)p).ResetBackColor();
            }
        }

        /// <summary>
        /// Zvyraznenie ikony po zvoleni nastroja.
        /// </summary>
        /// <param name="panel"></param>
        private void SelectIcon(Panel panel)
        {
            UnselectToolsIcon();
            panel.BackColor = Color.SteelBlue;
        }

        /// <summary>
        /// Aktivovanie - Nastroj malovanie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivePaint(object sender, EventArgs e)
        {
            ActiveTool((Panel)sender, EToolItem.Painting);
        }

        /// <summary>
        /// Aktivovanie nastroja BoundingBoxing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveBB(object sender, EventArgs e)
        {
            ActiveTool((Panel)sender, EToolItem.BoundingBox);
        }

        /// <summary>
        /// Aktivovanie - Nastroj kurzor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveCursor(object sender, EventArgs e)
        {
            ActiveTool((Panel) sender, EToolItem.Cursor);
        }

        /// <summary>
        /// Aktivovanie - Nastroj polyline.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivePolyline(object sender, EventArgs e)
        {
            ActiveTool((Panel)sender, EToolItem.Polyline);
        }

        /// <summary>
        /// Aktivovanie - Nastroj vyhladavanie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveFind(object sender, EventArgs e)
        {
            if (_activeTool == EToolItem.Find) return;

            cmbAllObject.Enabled = false;
            ActiveTool((Panel)sender, EToolItem.Find);
            _project.FindObjsInProject(new EventHandler(OnFindEndEvent));
        }

        /// <summary>
        /// Udalost ktora sa vyvola pri zatvoreni formu pre vyhladanie objektov na obrazku
        /// </summary>
        private void OnFindEndEvent(object sender, EventArgs e)
        {
           UnsetTools();
           UnselectToolsIcon();
           cmbAllObject.Enabled = true;
        }
        
        /// <summary>
        /// Aktivovanie nastroja.
        /// </summary>
        /// <param name="panel">panel predstavuje nastroj na paneli nastrojov</param>
        /// <param name="tool">nastroj ktory ma byt aktivovany</param>
        private void ActiveTool(Panel panel, EToolItem tool)
        {
            //ak je aktivny nastroj find tak sa neda kreslit nic nove, funguje iba kurzor a delete
            if (_activeTool == EToolItem.Find)
                return;

            SelectIcon(panel);
            //ak sa akurat kresli nejaka polyline tak ju ulozime zrusime kreslenie polyline a az potom aktivujeme dany nastroj
            if (_creatingPL)
            { 
                EndCreatePL();
            }

            _activeTool = tool;
            if (_activeTool == EToolItem.Find) //ak find tak kurzor tiez aktivny
                pnlCoursor.BackColor = Color.SteelBlue;
        }

        /// <summary>
        /// Deaktivovanie vsetkych nastrojov.
        /// </summary>
        private void UnsetTools()
        {
            _activeTool = EToolItem.None;
        }

        //////////////////////////////////////////////////////////////////////////////
        ////////////////         O T H E R S   M E T H O D S      ////////////////////
        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Vytvorenie nahladov na paneli s nahladmi
        /// </summary>
        /// <param name="startPic">cislo obrazku ktoreho nahlad ma byt prvy (od ktoreho sa zacne)</param>
        public void CreateActuallThumbs(int startPic)
        {
            //ak je nieco na paneli tak ho vymazeme
            if (pnlThumbs.Controls.Count > 0) pnlThumbs.Controls.Clear();

            //kontrola ci su obrazky dake a cislo start obrazku
            if (!_project.OpenImages.Any() || startPic < 0) return;

            //ak je obrazkov menej ako 50 tak vykresli vsetky
            if (_project.OpenImages.Count < 50) startPic = 0;

            //vykreslit 50 nahladov     
            for (int i = 0; i < 50; i++)
            {
                if (_project.OpenImages.Count <= startPic + i) return;
                if (this.InvokeRequired)
                    this.Invoke((Action<BaseFigure>)DrawThumb, _project.OpenImages[startPic + i]);
                else
                    DrawThumb(_project.OpenImages[startPic + i]);
            }

        }

        /// <summary>
        /// Nastavenie obrazka pre imagebox.
        /// </summary>
        /// <param name="image">obrazok ktory sa ma nastavit na imagebox</param>
        /// <returns>true ak prebehlo OK inak false</returns>
        public bool SetImage2ImageBox(BaseFigure image)
        {            
            //for (int i = 0; i < _project.CurrentImage.BoundBoxes.Count; i++)
            //{
            //    if (_project.CurrentImage.BoundBoxes[i].Properties.AtributesName != null)
            //    {
            //        if (!_project.MyClasses.GetAllClasses().Contains(_project.CurrentImage.BoundBoxes[i].Properties.Class))
            //        {
            //            List<string> props = new List<string>();
            //            for (int j = 0; j < _project.CurrentImage.BoundBoxes[i].Properties.AtributesName.Length; j++)
            //            {
            //                if (_project.CurrentImage.BoundBoxes[i].Properties.AtributesValue[j] == "true" || _project.CurrentImage.BoundBoxes[i].Properties.AtributesValue[j] == "false")
            //                {
            //                    props.Add("01;" + _project.CurrentImage.BoundBoxes[i].Properties.AtributesName[j]);
            //                }
            //                else
            //                {
            //                    props.Add(_project.CurrentImage.BoundBoxes[i].Properties.AtributesName[j] + ";" + _project.CurrentImage.BoundBoxes[i].Properties.AtributesValue[j]);
            //                }
            //            }
            //            SaveToFileUndefined(_project.CurrentImage.BoundBoxes[i].Properties.Class, props);
            //        }
            //    }
            //}

            try
            {
                AutoZoom((double)image.GetImage().Height, (double)image.GetImage().Width, (double)imageBox.Height, (double)imageBox.Width);
                if (!chbRescale.Checked)
                {
                    RememberZoomFromPrevious();    
                }
                imgBoxSelectedObj.Image = null;
                _redrawOnlyGuideLine = false;
                ResetGraphicsPath();
                RedrawBB();
                RedrawPA();
                RedrawPL();
                
                if (imageBox.Image != null) imageBox.Image.Dispose();
                imageBox.Image = image == null ? null : image.GetImage();

                if (imageBox.Image != null)//nastavime velkost pre horizontal a vertikal scroll bar
                {
                    SetZoomAndScroll();
                    if (_project.CurrentImage != null) txbZoom.Text = _project.CurrentImage.Zooom * 100 + "%";
                }
                imageBox.Refresh();
                if(_project.CurrentImage == null)cmbAllObject.Items.Clear();
                else RefreshComboBox();
                _project.SelectDefaultClass();
                //AutoZoom((double)image.GetImage().Height, (double)image.GetImage().Width, (double)imageBox.Height, (double)imageBox.Width);
                if (!chbRescale.Checked)
                {
                    if (imageBox.HorizontalScrollBar.Maximum > _project.CurrentImage.HScroll && imageBox.HorizontalScrollBar.Minimum < _project.CurrentImage.HScroll)
                        imageBox.HorizontalScrollBar.Value = _project.CurrentImage.HScroll;
                    if (imageBox.VerticalScrollBar.Maximum > _project.CurrentImage.VScroll && imageBox.VerticalScrollBar.Minimum < _project.CurrentImage.VScroll)
                        imageBox.VerticalScrollBar.Value = _project.CurrentImage.VScroll;
                }
                if (imageBox.HorizontalScrollBar.Value != 0 || imageBox.VerticalScrollBar.Value != 0)
                {
                    SetZoom(Math.Round(_project.CurrentImage.Zooom + 0.1, 1));
                    SetZoom(Math.Round(_project.CurrentImage.Zooom - 0.1, 1));    
                }

                if (_trackWindow != null)
                {
                    _trackWindow.SetImageToBox(_project.CurrentImage);
                    DrawObject ob = FindFromTrack();
                    if (ob != null)
                    {
                        SelectObject(ob);
                        cmbAllObject.SelectedItem = FindCbbItem(_selectedObj);
                        imageBox.Refresh();
                    }
                }

                return true;
                }
            catch(ArgumentException ex)
            {
                MessageBox.Show("Neplatný obrázok: " + (image == null ? "" : image.Source) +",\nbude zatvorený.\nChyba "+ ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                CloseCurrentToolStripMenuItemClick(null, null);
                return false;
            }
        }

        /// <summary>
        /// nastavenie horizontal a vertical scroll barov
        /// </summary>
        private void SetMaxOfScrollBar()
        {
            int horizMax =
                (int)Math.Ceiling(imageBox.Image.Size.Width - ((imageBox.ClientSize.Width - imageBox.VerticalScrollBar.Width) / _project.CurrentImage.Zooom));

            int vertMax =
                (int)Math.Ceiling(imageBox.Image.Size.Height - ((imageBox.ClientSize.Height - imageBox.HorizontalScrollBar.Height) / _project.CurrentImage.Zooom));
            
            if (horizMax > 0)
            {
                imageBox.HorizontalScrollBar.Maximum = horizMax;
                imageBox.HorizontalScrollBar.SmallChange = horizMax / 20;
                imageBox.HorizontalScrollBar.LargeChange = horizMax / 10;
                imageBox.HorizontalScrollBar.Maximum += imageBox.HorizontalScrollBar.LargeChange;
                imageBox.HorizontalScrollBar.Show();
            }
            else imageBox.HorizontalScrollBar.Hide();
            
            if (vertMax > 0)
            {
                imageBox.VerticalScrollBar.Maximum = vertMax;
                imageBox.VerticalScrollBar.SmallChange = vertMax / 20;
                imageBox.VerticalScrollBar.LargeChange = vertMax / 10;
                imageBox.VerticalScrollBar.Maximum += imageBox.VerticalScrollBar.LargeChange;
                imageBox.VerticalScrollBar.Show();
            }
            else imageBox.VerticalScrollBar.Hide();
        }

        /// <summary>
        /// Nastavenie zoom a scroll pre konkretny zobrazeny obrazok.
        /// </summary>
        private void SetZoomAndScroll()
        {
            if(_project == null || _project.CurrentImage == null) return;
            imageBox.SetZoomScale(_project.CurrentImage.Zooom, new Point(0, 0));
            SetMaxOfScrollBar();

            if (_project.CurrentImage.HorizontScroll > imageBox.HorizontalScrollBar.Maximum) _project.CurrentImage.HorizontScroll = 0;
            imageBox.HorizontalScrollBar.Value = _project.CurrentImage.HorizontScroll;
            if (_project.CurrentImage.VerticalScroll > imageBox.VerticalScrollBar.Maximum) _project.CurrentImage.VerticalScroll = 0;
            imageBox.VerticalScrollBar.Value = _project.CurrentImage.VerticalScroll;
        }

        /// <summary>
        /// Aktualizacia premennych pre nastavenie scroll-u konkretneho obrazka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="scrollEventArgs"></param>
        private void ScrollBarOnScroll(object sender, ScrollEventArgs scrollEventArgs)
        {
            if (_project.CurrentImage == null)return;
            _project.CurrentImage.VerticalScroll = imageBox.VerticalScrollBar.Value;
            _project.CurrentImage.HorizontScroll = imageBox.HorizontalScrollBar.Value;
        }

        /// <summary>
        /// Resetovanie graphicsPath pre vykreslenie.
        /// </summary>
        public void ResetGraphicsPath()
        {
            UnselectObject();//_selectedObj = null;
            _gpBB.Reset();
            _gpPA.Reset();
            _gpPA.FillMode = FillMode.Winding;
            _gpPL.Reset();
            _gpPABB.Reset();
            _gpMark.Reset();
            _gpGuideLine.Reset();
        }

        /// <summary>
        /// Resetovanie comboboxu so zoznamom tried, reset panelu pre vlastnosti triedy.
        /// </summary>
        private void ResetClassControls()
        {
            cmbClass.SelectedIndex = -1;
            _project.MyClasses.PropsName = new string[0];
            pnlProperty.Controls.Clear();
        }

        /// <summary>
        /// Povolenie / zakazanie aktivovania nastrojov.
        /// </summary>
        /// <param name="enable"></param>
        private void EnableTools(bool enable)
        {
            saveAsToolStripMenuItem.Enabled = enable;           //  file - save as
            closeCurrentToolStripMenuItem.Enabled = enable;     //  project - close current
            cmbClass.Enabled = enable;                          //  kombobox so zoznamom tried
            cmbAllObject.Enabled = enable;                      //  kombobox so vsetkymi objektami na obrazku (BB, PA)
            pnlAddClass.Enabled = enable;                       //  tlacitko '+' pre pridanie novej triedy
            pnlEditClass.Enabled = enable;                      //  tlacitko '/' pre editovanie triedy
            pnlProperty.Enabled = enable;                       //  panel s vlastnostami danej triedy
            pnlTools.Enabled = enable;                          //  panel nastrojov
            pnlPictAttribute.Enabled = enable;                  //  panel s vlastnostami daneho obrazku
            pnlPictAttribute.Enabled = enable;
            pnlSelectObj.Enabled = enable;
            joinBbClassToolStripMenuItem.Enabled = enable;
            tracksEditorToolStripMenuItem.Enabled = enable;
            trackCompareToolToolStripMenuItem.Enabled = enable;
        }

        /// <summary>
        /// Povolenie / zakazanie kliknutia na polozky menu.
        /// </summary>
        /// <param name="enable"></param>
        private void EnableMenu(bool enable)
        {
            projectToolStripMenuItem.Enabled = enable;          //  lista menu - TOOL
            toolsToolStripMenuItem.Enabled = enable;            //  lista menu - PROJECT
            optionsToolStripMenuItem.Enabled = enable;          //  Nastavenia projektu - FILE -> OPTIONS
        }

        /// <summary>
        /// Nastavenie defaultnych parametrov pri vytvarani noveho projektu.
        /// </summary>
        private void RestoreDefault()
        {
            _btm = null; // vymazanie vrstvy nad obrazkom
            pnlThumbs.Controls.Clear();          
            EnableTools(false);
            EnableMenu(false);
            _editingBB = false;
            _creatingPL = false;
            _mouseDown = false;
            UnsetTools();
            imageBox.Image = null;
            imgBoxSelectedObj.Image = null;
            UnselectToolsIcon();
            ResetGraphicsPath();
            imageBox.Refresh();
        }

        /// <summary>
        /// Obsluha kliknutia na menu - Tutorial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TutotialToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Help.ShowHelp(this, @"Help\help.html");
            }
            catch (Exception exc)
            {
                MessageBox.Show("CreateImageFromDrawObj.BtnHelpClick()", "Error: " + exc, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            
        }

        /// <summary>
        /// Nastavenie preddefinovanych hodnot atributov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetObjectPropertiesMenuClick(object sender, EventArgs e)
        {
            _project.DefineDefaultPropValue();
        }

        /// <summary>
        /// Obsluha stlacenia tlacidla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (_project == null) return;
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Right) //prepnutie nasledujucej snimky
            {
                Object thumbPanel = new ThumbPanel(_project.SelectNextPicture());
                if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
                imageBox.Cursor = Cursors.Default; 
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Left)//prepnutie predchadzajucej snimky
            {
                Object thumbPanel = new ThumbPanel(_project.SelectPreviousPicture());
                if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
                imageBox.Cursor = Cursors.Default; 
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Delete)//vymazanie aktualne oznaceneho objektu(BB, PA)
            {
                DeleteClick(null,null);
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Add) //zoom in
            {
                if (_project.CurrentImage != null)
                {
                    ZoomIn(null, e);
                    _gpGuideLine.Reset();
                }
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Subtract)//zoom out
            {
                if (_project.CurrentImage != null)
                {
                    ZoomOut(null, e);
                    _gpGuideLine.Reset();
                }
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.S)//save object properties
            {
                if (MessageBox.Show("Naozaj chcete uložiť zmeny?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _project.SaveImmediately();
                }
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.F)//find objects
            {
                ActiveFind(pnlFind, null);
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.O)//save object properties
            {
                SaveProperties();
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Q)//copy object properties
            {
                SetCopyParameters();
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.W)//set object properties
            {
                CopyParametersToPanel();
            }

        }
        
        /// <summary>
        /// Nastavenie najdeneho objektu pre uzivatela
        /// </summary>
        /// <param name="drObj">najdeny objekt</param>
        public void SetFindingObj(DrawObject drObj)
        {
            SelectObject(drObj);
            //kontrola ci sa nenasiel objekt ktory ma aktualne zobrazene vlasnosti
            if (cmbAllObject.SelectedItem == null || ((ComboBoxItem)cmbAllObject.SelectedItem).Value != _selectedObj) //ak nie
                cmbAllObject.SelectedItem = FindCbbItem(_selectedObj); //spristupnime objektu pre upravu.
            else //ak ano tak len prepiseme hodnoty v komboboxoch urcujuce vlasnosti daneho objektu, lebo tie mohli byt zmenene
                SetCbbProperty(_selectedObj);
//            MarkSelected(drObj);
        }

        /// <summary>
        /// Obsluha prechodu na cislo obrazku ktore zadal uzivatel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoOnKeyPress(object sender, KeyPressEventArgs e)
        {
            _hlpHScroll = imageBox.HorizontalScrollBar.Value;
            _hlpVScroll = imageBox.VerticalScrollBar.Value;
            _hlpZoom = _project.CurrentImage.Zooom;
            _selectedClass = cmbClass.SelectedIndex;
            if (e.KeyChar == (char)Keys.Enter)
            {
                string goOn = ((TextBox)sender).Text;
                Object thumbPanel = new ThumbPanel(_project.GoOnPicture(Convert.ToInt32(goOn)));
                if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
            }

            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back || e.KeyChar == (char)46; //46 je bodka;

            if (cmbClass.Items.Count > _selectedClass)
                cmbClass.SelectedIndex = _selectedClass;
        }
        
        /// <summary>
        /// Nastavenie statusu na status bare
        /// </summary>
        /// <param name="status">tect statusu</param>
        public void SetStatusLoading(string status)
        {
            statusLabelLoading.Text = status;
        }

        /// <summary>
        /// Nastavenie progres baru na status bare
        /// </summary>
        /// <param name="value">hodnota progressbaru</param>
        public void SetProgressBar(int value)
        {
            StatusProgressBar.Value = value;
        }

        /// <summary>
        /// Obsluha kliknutia na volbu generovanie treningových dat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateTraToolStripMenuItemClick(object sender, EventArgs e)
        {
            _project.GenerateNegativeTrainingData();
        }

        /// <summary>
        /// Obsluha kliknutia na volbu generovania dat z BB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateImagesFromBBsMenuClick(object sender, EventArgs e)
        {
            _project.SaveBBsAsImages();
        }

        /// <summary>
        /// Generovanie dat pre PL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateRealDataFromPolylineToolClick(object sender, EventArgs e)
        {
            _project.GenerateDataForPL();
        }

        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(_project == null) return;
            if(_project.SetProjectOptions())
                imageBox.Refresh();
        }

        private void JoinBbsToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_project == null) return;
            var po = new JoinBbsForm(_project.OpenImages);
            po.ShowDialog();
        }

        private void BtnNextThumbsClick(object sender, EventArgs e)
        {
            _hlpHScroll = imageBox.HorizontalScrollBar.Value;
            _hlpVScroll = imageBox.VerticalScrollBar.Value;
            _hlpZoom = _project.CurrentImage.Zooom;
            if(pnlThumbs.Controls.Count == 0)return;
            Object thumbPanel = pnlThumbs.Controls[pnlThumbs.Controls.Count - 1];
            if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
            imageBox.Cursor = Cursors.Default; 
        }

        private void BtnPreviousThumbsClick(object sender, EventArgs e)
        {
            _hlpHScroll = imageBox.HorizontalScrollBar.Value;
            _hlpVScroll = imageBox.VerticalScrollBar.Value;
            _hlpZoom = _project.CurrentImage.Zooom;
            if (pnlThumbs.Controls.Count == 0) return;
            Object thumbPanel = pnlThumbs.Controls[0];
            if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
            imageBox.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Odchytenie stlacenia tlacidla 
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="key">stlacene tlacidlo</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            if (_project != null)
            {
                Keys a = key;
                switch (key)
                {
                    case Keys.F1:
                    case Keys.F2:
                    case Keys.F3:
                    case Keys.F4:
                    case Keys.F5:
                    case Keys.F6:
                    case Keys.F7:
                    case Keys.F8:
                        break;
                    case Keys.R:
                        //MessageBox.Show(a.ToString());
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, key);
        }

        /// <summary>
        /// obsluha kliknutia na polozku menu Use External Method For Recognize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseExternMethodForRecognizeMenuItemClick(object sender, EventArgs e)
        {
            if (_project == null) return;
            
            ExternalMethodForRecognition.ProcessExternalMethod(_project);
            RedrawBB();
            RedrawPA();
            RedrawPL();
            imageBox.Refresh();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("Annotation tool to support solving computer vision tasks", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// odznacenie objektu po kliknuti na menu-pre ulozenie vlastnosti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuStrip1ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (_project == null) return;
            _project.SavePictureProps();

            foreach (var ownedForm in this.OwnedForms)
            {
                if (ownedForm.GetType() == typeof (FindObjectsInProject))
                {
                    ownedForm.Close();
                    break;
                }
            }

            if (_creatingPL) // ak sa vytvara PL a ukoncenie vytvarania PL
            {
                EndCreatePL();
            }
            else
            {
                UnselectObject();
                imageBox.Refresh();
            }
        }

        private void DefinePicturePropertyToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(_project == null) return;
            var dpp = new DefinePicturePropertyForm();
            if(dpp.ShowDialog() == DialogResult.OK)
                _project.CreatePictureProperty(pnlPictAttribute);
        }

        private void FitZoom()
        {

        }

        //////////////////////////////////////////////////////////////////////////////
        ////////////////           T R A C K  E D I T O R         ////////////////////
        //////////////////////////////////////////////////////////////////////////////

        private void tracksEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_testWindow == null)
            {
                if (_trackWindow != null)
                {
                    _trackWindow.Close();
                }
                _trackWindow = new TrackEditorForm(this);
                _trackWindow.AllImages = _project.OpenImages;
                _trackWindow.SetImageToBox(_project.CurrentImage);
                _trackWindow.AddCheckBoxesItems();
                _trackWindow.Show();
            }
            else
            {
                MessageBox.Show("Je potrebné ukončiť porovnávanie trackov!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public TrackEditorForm TrackWin
        {
            get
            {
                return _trackWindow;
            }

            set
            {
                _trackWindow = value;
            }
        }

        public DrawObject SelectedObject
        {
            get
            {
                return _selectedObj;
            }

            set
            {
                _selectedObj = value;
            }
        }

        public BoundingBox FindFromTrack()
        {
            var trId = _trackWindow.SelectedObj;
            if (trId != null)
            {
                BoundingBox drOb = null;
                foreach(var bb in _project.CurrentImage.BoundBoxes)
                {
                    var ind = Array.IndexOf(bb.Properties.AtributesName, "track_id");
                    if (ind >= 0)
                    {
                        if (String.Compare(bb.Properties.AtributesValue[ind], trId, false) == 0)
                        {
                            drOb = bb;
                            break;
                        }
                    }
                }
                return drOb;
            }
            else
            {
                return null;
            }
        }

        public void ChangeFrameManually(bool nextFrame)
        {
            if (_project == null) return;
            if (nextFrame)
            {
                Object thumbPanel = new ThumbPanel(_project.SelectNextPicture());
                if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
            }
            else
            {
                Object thumbPanel = new ThumbPanel(_project.SelectPreviousPicture());
                if (((ThumbPanel)thumbPanel).Image != null) ThumbPanelOnMouseClick(thumbPanel, null);
            }
            imageBox.Cursor = Cursors.Default;            
        }        

        private void SetCopyParameters()
        {
            if (_selectedObj == null)
            {
                MessageBox.Show("Je potrebné zvoliť objekt!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (_project == null)
            {
                return;
            }
            else if (cmbClass.SelectedItem == null)
            {
                MessageBox.Show("Nebola zvolená trieda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                _copyClassName = cmbClass.SelectedItem.ToString();
                _copyPropertiesList = new List<string>();
                _copyNamesList = new List<string>();
                foreach (Control c in pnlProperty.Controls)
                {                    
                    if (c is ClassMultiProperty)
                    {
                        _copyNamesList.Add(((ClassMultiProperty)c).GetName());
                        _copyPropertiesList.Add(((ClassMultiProperty)c).GetSelectedValue());
                    }
                    else
                    {
                        _copyNamesList.Add(((ClassBoolProperty)c).AtrName);
                        _copyPropertiesList.Add(((ClassBoolProperty)c).Checked.ToString());
                    }
                }
                SetAccessibleDescription();
            }
        }

        private void SetAccessibleDescription()
        {
            var vals = "";
            for (int i = 0; i < _copyPropertiesList.Count; i++)
            {
                vals += _copyNamesList[i] + ": " + _copyPropertiesList[i] + " | ";
            }
            //pnlCopyInfo.AccessibleDescription.Remove();
            pnlCopyInfo.AccessibleDescription = "Class: " + _copyClassName + " | Parameter values: " + vals; 
        }

        private void CopyParametersToPanel()
        {
            if (_selectedObj == null)
            {
                MessageBox.Show("Je potrebné zvoliť objekt!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (_project == null)
            {
                return;
            }
            else if (_copyClassName == null)
            {
                MessageBox.Show("Neboli zvolené žiadne dáta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {                
                cmbClass.SelectedIndex = cmbClass.FindStringExact(_copyClassName);
                int loopIndex = 0;
                foreach (Control c in pnlProperty.Controls)
                {
                    if (c is ClassMultiProperty)
                    {                        
                        ((ClassMultiProperty)c).SetSelectedValue(_copyPropertiesList[loopIndex]);
                    }
                    else
                    {                        
                        if(String.Compare(_copyPropertiesList[loopIndex], "True") == 0)
                        {
                            ((ClassBoolProperty)c).Checked = true;
                        }
                        else
                        {
                            ((ClassBoolProperty)c).Checked = false;
                        }
                    }
                    loopIndex++;
                }                
            }
        }

        private void HighlightIcon4Tooltip(object sender, EventArgs e)
        {            
            var toolPanel = ((Panel)sender);
            toolPanel.BackColor = Color.Violet;
           _toolTip1 = new ToolTip();
            _toolTip1.SetToolTip(toolPanel, toolPanel.AccessibleDescription);
        }

        private void ResetIconBack4Tooltip(object sender, EventArgs e)
        {
            ((Panel)sender).ResetBackColor();
            _toolTip1.Dispose();
        }        

        private void PropertyMouseLeave(object sender, EventArgs e)
        {

        }

        //////////////////////////////////////////////////////////////////////////////
        ////////////////              T E S T  T O O L            ////////////////////
        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Obsluha kliknutia na polozku menu - vytvorenie noveho okna pre porovnanie trackov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackCompareToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_trackWindow == null)
            {
                if (_testWindow != null)
                {
                    _testWindow.Close();
                }
                BaseFigure firstImage = null;
                if (_project.OpenImages.Count > 0)
                    firstImage = _project.OpenImages[0];
                _testWindow = new TestEditorForm(this);
                _testWindow.AllImages = _project.OpenImages;
                _testWindow.InitImageBoxes();
                _testWindow.Show();
            }
            else
            {
                MessageBox.Show("Je potrebné ukončiť editáciu trackov!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// property test win
        /// </summary>
        public TestEditorForm TestkWin
        {
            get
            {
                return _testWindow;
            }

            set
            {
                _testWindow = value;
            }
        }        
    }
}