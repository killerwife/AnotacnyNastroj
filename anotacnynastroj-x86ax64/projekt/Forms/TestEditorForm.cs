using Projekt.DrawObjects;
using Projekt.ExportImport;
using Projekt.Figure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt.Forms
{
    public partial class TestEditorForm : Form
    {
        private MainWindowApplication _mainWin;
        private BaseFigure _currentImg;
        private int _currentImgNum;
        private bool _needScrollHandler;

        private List<List<BoundingBox>> _gtObjects;
        private List<List<BoundingBox>> _testObjects;

        private Bitmap _btm = new Bitmap(1, 1);
        private Graphics _graphics = null;

        private Bitmap _btm1 = new Bitmap(1, 1);
        private Graphics _graphics1 = null;

        private double _imgBox1Zoom;
        private double _imgBox2Zoom;        

        public List<BaseFigure> AllImages { get; set; }        
        private bool GtFromProject { get; set; }
        private bool TestFromProject { get; set; }
        private bool ProjectIsSet { get; set; }
        private List<BoundingBox> SelectedTrack { get; set; }
        private string SelectedTrackName { get; set; }
        private List<BoundingBox> TestedTracks { get; set; }
        private List<BoundingBox> UncategorizedPoints { get; set; }

        //pamatanie zoomu pri vybere oblasti
        private double _leftClickZoom;
        private int _leftClickVScroll1;
        private int _leftClickHScroll1;
        private int _leftClickVScroll2;
        private int _leftClickHScroll2;

        //statistiky
        private int _truePositives;
        private int _falsePositives;
        private int _falseNegatives;
        Dictionary<string, int[]> _statsDictionary;
        Dictionary<string, int> _statsTestDictionary;

        public TestEditorForm(MainWindowApplication paMain)
        {
            InitializeComponent();
            _mainWin = paMain;
            imageBox.OnZoomScaleChange += ImageBox1OnOnZoomScaleChange;
            imageBox.HorizontalScrollBar.Scroll += HorizontalScrollImgBox1Change;
            imageBox.VerticalScrollBar.Scroll += VerticalScrollImgBox1Change;
            imageBox1.OnZoomScaleChange += ImageBox2OnOnZoomScaleChange;
            imageBox1.HorizontalScrollBar.Scroll += HorizontalScrollImgBox2Change;
            imageBox1.VerticalScrollBar.Scroll += VerticalScrollImgBox2Change; 
             _needScrollHandler = true;
            ProjectIsSet = false;
            cmbGtTracks.Enabled = false;
            chListTracks.Enabled = false;
            chbUncategorized.Enabled = false;            
            SelectedTrack = new List<BoundingBox>();
            TestedTracks = new List<BoundingBox>();
            UncategorizedPoints = new List<BoundingBox>();
            chListTracks.Sorted = true;
            //retain zoom
            _imgBox1Zoom = 1.0;
            _imgBox2Zoom = 1.0;
            _leftClickZoom = 1.0;
            _leftClickHScroll1 = 0;
            _leftClickVScroll1 = 0;
            _leftClickHScroll2 = 0;
            _leftClickVScroll2 = 0;

            _truePositives = 0;
            _falsePositives = 0;
            _falseNegatives = 0;
            _statsDictionary = new Dictionary<string, int[]>();
            //index 0 = prue positives, index 2 = false negatives
            _statsTestDictionary = new Dictionary<string, int>();
            //false positives pre testovacie
            nmTruePositives.Value = _mainWin.TruePositiveWeight;
            nmFalsePositives.Value = _mainWin.FalsePositiveWeight;
            nmFalseNegatives.Value = _mainWin.FalseNegativeWeight;
        }

        private void EndWork(object sender, FormClosingEventArgs e)
        {
            _mainWin.TestkWin = null;
        }

        /// <summary>
        /// Inicializacia imageboxov
        /// </summary>
        public void InitImageBoxes()
        {
            if (AllImages.Count > 0)
            {
                _currentImgNum = 1;
                SetImage2ImageBox(AllImages[0]);
            }
            else
            {
                _currentImgNum = 0;
            }

            labImageCount.Text = "Current image: " + _currentImgNum + "/" + AllImages.Count;
        }

        /// <summary>
        /// Nastavenie obrazkov do img boxov
        /// </summary>
        /// <param name="image"></param>
        private void SetImage2ImageBox(BaseFigure image)
        {
            _currentImg = image;
            if (imageBox.Image != null) imageBox.Image.Dispose();
            if (imageBox1.Image != null) imageBox1.Image.Dispose();

            imageBox.Image = image == null ? null : image.GetImage();
            imageBox1.Image = image == null ? null : image.GetImage();

            if (_currentImg != null)
                SetZoom();
        }

        /// <summary>
        /// Prisposobenie velkosti obrazka imageboxu
        /// </summary>
        public void SetZoom()
        {
            if (chbRememberZoom.Checked)
            {
                imageBox.SetZoomScale(_imgBox1Zoom, new Point(0, 0));
                imageBox1.SetZoomScale(_imgBox2Zoom, new Point(0, 0));
            }
            else
            {
                double imgHeihgt = (double)_currentImg.GetImage().Height;
                double imgWidth = (double)_currentImg.GetImage().Width;
                double boxHeight = (double)imageBox.Height;
                double boxWidth = (double)imageBox.Width;
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
                imageBox.SetZoomScale(Math.Floor(zoomNum * 100) / 100, new Point(0, 0));
                imageBox1.SetZoomScale(Math.Floor(zoomNum * 100) / 100, new Point(0, 0));
            }
        }

        private void TestEditorForm_Resize(object sender, EventArgs e)
        {
            SetZoom();
        }
        
        private void HighlightIcon(object sender, EventArgs e)
        {
            if (((Panel)sender).BackColor == Color.SteelBlue) return;
            var toolPanel = ((Panel)sender);
            toolPanel.BackColor = Color.Violet;
            var toolTip1 = new ToolTip();
            toolTip1.SetToolTip(toolPanel, toolPanel.AccessibleDescription);
        }

        private void ResetIconBack(object sender, EventArgs e)
        {
            if (((Panel)sender).BackColor != Color.SteelBlue) ((Panel)sender).ResetBackColor();
        }

        /// <summary>
        /// Zmena obrazka
        /// </summary>
        /// <param name="imageNum"></param>
        private void ChangeImage(int imageNum)
        {
            var imgNum = imageNum - 1;
            if (imgNum >= 0 && imgNum < AllImages.Count)
            {
                //retain zoom
                _imgBox1Zoom = imageBox.ZoomScale;                
                _imgBox2Zoom = imageBox1.ZoomScale;                
                //zmena obrazka
                SetImage2ImageBox(AllImages[imgNum]);
                _currentImgNum = imageNum;
                labImageCount.Text = "Current image: " + _currentImgNum + "/" + AllImages.Count;
            }

        }

        /// <summary>
        /// Nastavenie predosleho snimku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPreviousFrame(object sender, MouseEventArgs e)
        {
            ChangeImage(_currentImgNum - 1);
        }

        /// <summary>
        /// Nastavenie nasledujuceho snimku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetNextFrame(object sender, MouseEventArgs e)
        {
            ChangeImage(_currentImgNum + 1);
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Right) //ctrl + ->
            {
                ChangeImage(_currentImgNum + 1);
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Left)//ctrl + <-
            {
                ChangeImage(_currentImgNum - 1);
            }
        }

        /// <summary>
        /// Zmena snimku pri zadani cisla snimku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoOnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    int goOn = Convert.ToInt32(((TextBox)sender).Text);
                    ChangeImage(goOn);
                }
                catch (FormatException)
                { }
            }
        }

        /// <summary>
        /// Otvorenie okna pre nacitanie suborov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFilesToCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var success = true;
            var fil = new AddFilesToCompareForm();
            if (fil.ShowDialog() == DialogResult.OK)
            {
                ProjectIsSet = true;
                if (!fil.Project4Gt)
                {
                    GtFromProject = false;
                    ImportExportXML import = new ImportExportXML(fil.File4Gt);
                    var imagesSrc = new List<string>();
                    var imagesAttrs = new List<List<string[]>>();
                    var imagesDrawObjects = new List<List<DrawObject>>();
                    _gtObjects = new List<List<BoundingBox>>();

                    var videosSrc = new List<string>();
                    var framesAttrs = new List<List<List<string[]>>>();
                    var framesDrawObjects = new List<List<List<DrawObject>>>();
                    if (!import.ImportProject(ref imagesSrc, ref imagesAttrs, ref imagesDrawObjects, ref videosSrc, ref framesAttrs, ref framesDrawObjects))
                    {
                        success = false;
                    }
                    else
                    {
                        for (int i = 0; i < imagesDrawObjects.Count; i++)
                        {                            
                            var bboxes = imagesDrawObjects[i];
                            var nbbs = new List<BoundingBox>();
                            for (int j = 0; j < bboxes.Count; j++)
                            {
                                if (bboxes[j].GetType() == typeof(BoundingBox))
                                {
                                    nbbs.Add((BoundingBox)bboxes[j]);
                                }
                            }
                            _gtObjects.Add(nbbs);
                        }
                    }
                    CheckBbArrays(_gtObjects);
                }
                else
                {
                    GtFromProject = true;
                }

                if (!success)
                {
                    ProjectIsSet = false;
                    cmbGtTracks.Enabled = false;
                    chListTracks.Enabled = false;
                    chbUncategorized.Enabled = false;                    
                    imageBox.Refresh();
                    imageBox1.Refresh();
                    return;
                }

                if (!fil.Project4Test)
                {
                    TestFromProject = false;
                    ImportExportXML import = new ImportExportXML(fil.File4Test);
                    var imagesSrc = new List<string>();
                    var imagesAttrs = new List<List<string[]>>();
                    var imagesDrawObjects = new List<List<DrawObject>>();
                    _testObjects = new List<List<BoundingBox>>();

                    var videosSrc = new List<string>();
                    var framesAttrs = new List<List<List<string[]>>>();
                    var framesDrawObjects = new List<List<List<DrawObject>>>();
                    if (!import.ImportProject(ref imagesSrc, ref imagesAttrs, ref imagesDrawObjects, ref videosSrc, ref framesAttrs, ref framesDrawObjects))
                    {
                        success = false;
                    }
                    else
                    {
                        for (int i = 0; i < imagesDrawObjects.Count; i++)
                        {
                            var bboxes = imagesDrawObjects[i];
                            var nbbs = new List<BoundingBox>();
                            for (int j = 0; j < bboxes.Count; j++)
                            {
                                if (bboxes[j].GetType() == typeof(BoundingBox))
                                {
                                    nbbs.Add((BoundingBox)bboxes[j]);
                                }
                            }
                            _testObjects.Add(nbbs);
                        }
                    }
                    CheckBbArrays(_testObjects);
                }
                else
                {
                    TestFromProject = true;
                }

                if (!success)
                {
                    ProjectIsSet = false;
                    cmbGtTracks.Enabled = false;
                    chListTracks.Enabled = false;
                    chbUncategorized.Enabled = false;                    
                    imageBox.Refresh();
                    imageBox1.Refresh();
                    return;
                }

                SetComboboxItems();
                
            }            
        }

        /// <summary>
        /// Vytvori sa zoznam trackov gt
        /// </summary>
        public void SetComboboxItems()
        {
            cmbGtTracks.Enabled = true;
            chListTracks.Enabled = true;
            chbUncategorized.Enabled = true;            
            cmbGtTracks.Items.Clear();
            cmbGtTracks.ResetText();
            SelectedTrack.Clear();
            TestedTracks.Clear();
            UncategorizedPoints.Clear();
            chListTracks.Items.Clear();

            List<string> trackNumbers = new List<string>();
            if (GtFromProject)
            {
                for (int i = 0; i < AllImages.Count; i++)
                {
                    for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                    {
                        var trIndex = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                        if (trIndex >= 0)
                        {
                            var trId = AllImages[i].BoundBoxes[j].Properties.AtributesValue[trIndex];
                            if (!trackNumbers.Contains(trId) && String.Compare(trId, "", false) != 0)                            
                            {
                                trackNumbers.Add(trId);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _gtObjects.Count; i++)
                {
                    var frameBbs = _gtObjects[i];
                    for (int j = 0; j < frameBbs.Count; j++)
                    {
                        var trIndex = Array.IndexOf(frameBbs[j].Properties.AtributesName, "track_id");
                        if (trIndex >= 0)
                        {
                            var trId = frameBbs[j].Properties.AtributesValue[trIndex];
                            if (!trackNumbers.Contains(trId) && String.Compare(trId, "", false) != 0)                            
                            {
                                trackNumbers.Add(trId);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < trackNumbers.Count; i++)
            {                
                cmbGtTracks.Items.Add("Track " + trackNumbers[i]);
            }
            imageBox.Refresh();
            imageBox1.Refresh();

            CalcStats4FullSequence();

            //tsProgressBar.Visible = true;
            //tsStatusLabel.Visible = true;
            //sem pojde thread
            
        }

        /// <summary>
        /// Zoom handler pre ground truth okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ImageBox1OnOnZoomScaleChange(object sender, EventArgs eventArgs)
        {
            if (_currentImg == null) return;
            if (!chbSync.Checked) return;
            if (_needScrollHandler)
            {
                _needScrollHandler = false;
                var nZoom = imageBox.ZoomScale;
                imageBox1.SetZoomScale(nZoom, default(Point));

                if (imageBox.HorizontalScrollBar.Value <= imageBox1.HorizontalScrollBar.Maximum)
                    imageBox1.HorizontalScrollBar.Value = imageBox.HorizontalScrollBar.Value;

                if (imageBox.VerticalScrollBar.Value <= imageBox1.VerticalScrollBar.Maximum)
                    imageBox1.VerticalScrollBar.Value = imageBox.VerticalScrollBar.Value;
                _needScrollHandler = true;
            }
        }

        /// <summary>
        /// Zoom handler pre testovacie okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ImageBox2OnOnZoomScaleChange(object sender, EventArgs eventArgs)
        {
            if (_currentImg == null) return;
            if (!chbSync.Checked) return;
            if (_needScrollHandler)
            {
                _needScrollHandler = false;
                var nZoom = imageBox1.ZoomScale;
                imageBox.SetZoomScale(nZoom, default(Point));

                if (imageBox1.HorizontalScrollBar.Value <= imageBox.HorizontalScrollBar.Maximum)
                    imageBox.HorizontalScrollBar.Value = imageBox1.HorizontalScrollBar.Value;

                if (imageBox1.VerticalScrollBar.Value <= imageBox.VerticalScrollBar.Maximum)
                    imageBox.VerticalScrollBar.Value = imageBox1.VerticalScrollBar.Value;
                _needScrollHandler = true;
            }
        }

        /// <summary>
        /// Img box gt horizontal scroll event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void HorizontalScrollImgBox1Change(object sender, EventArgs eventArgs)
        {
            if (!chbSync.Checked) return;
            if (imageBox.HorizontalScrollBar.Value <= imageBox1.HorizontalScrollBar.Maximum)
                imageBox1.HorizontalScrollBar.Value = imageBox.HorizontalScrollBar.Value;
            imageBox1.Refresh();            
        }

        /// <summary>
        /// Img box gt vertical scroll event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VerticalScrollImgBox1Change(object sender, EventArgs eventArgs)
        {
            if (!chbSync.Checked) return;
            if (imageBox.VerticalScrollBar.Value <= imageBox1.VerticalScrollBar.Maximum)
                imageBox1.VerticalScrollBar.Value = imageBox.VerticalScrollBar.Value;
            imageBox1.Refresh();
        }

        /// <summary>
        /// Img box test horizontal scroll event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void HorizontalScrollImgBox2Change(object sender, EventArgs eventArgs)
        {
            if (!chbSync.Checked) return;
            if (imageBox1.HorizontalScrollBar.Value <= imageBox.HorizontalScrollBar.Maximum)
                imageBox.HorizontalScrollBar.Value = imageBox1.HorizontalScrollBar.Value;
            imageBox.Refresh();
        }

        /// <summary>
        /// Img box test vertical scroll event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VerticalScrollImgBox2Change(object sender, EventArgs eventArgs)
        {
            if (!chbSync.Checked) return;
            if (imageBox1.VerticalScrollBar.Value <= imageBox.VerticalScrollBar.Maximum)
                imageBox.VerticalScrollBar.Value = imageBox1.VerticalScrollBar.Value;
            imageBox.Refresh();
        }        

        /// <summary>
        /// Nastavenie scroll barov pre test box pri stlaceni middle mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imageBox.SetZoomScale(_leftClickZoom, default(Point));
                imageBox.HorizontalScrollBar.Value = _leftClickHScroll1;
                imageBox.VerticalScrollBar.Value = _leftClickVScroll1;
                imageBox1.HorizontalScrollBar.Value = _leftClickHScroll2;
                imageBox1.VerticalScrollBar.Value = _leftClickVScroll2;
            }
            else
            {
                if (!chbSync.Checked) return;
                var refrsh = false;
                if (imageBox.HorizontalScrollBar.Value != imageBox1.HorizontalScrollBar.Value)
                {
                    refrsh = true;
                    if (imageBox.HorizontalScrollBar.Value <= imageBox1.HorizontalScrollBar.Maximum)
                        imageBox1.HorizontalScrollBar.Value = imageBox.HorizontalScrollBar.Value;
                }
                if (imageBox.VerticalScrollBar.Value != imageBox1.VerticalScrollBar.Value)
                {
                    refrsh = true;
                    if (imageBox.VerticalScrollBar.Value <= imageBox1.VerticalScrollBar.Maximum)
                        imageBox1.VerticalScrollBar.Value = imageBox.VerticalScrollBar.Value;
                }
                if (refrsh)
                    imageBox1.Refresh();
            }
        }

        /// <summary>
        /// Nastavenie scroll barov pre gt box pri stlaceni middle mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imageBox1.SetZoomScale(_leftClickZoom, default(Point));
                imageBox.HorizontalScrollBar.Value = _leftClickHScroll1;
                imageBox.VerticalScrollBar.Value = _leftClickVScroll1;
                imageBox1.HorizontalScrollBar.Value = _leftClickHScroll2;
                imageBox1.VerticalScrollBar.Value = _leftClickVScroll2;
            }
            else
            {
                if (!chbSync.Checked) return;
                var refrsh = false;
                if (imageBox.HorizontalScrollBar.Value != imageBox1.HorizontalScrollBar.Value)
                {
                    refrsh = true;
                    if (imageBox1.HorizontalScrollBar.Value <= imageBox.HorizontalScrollBar.Maximum)
                        imageBox.HorizontalScrollBar.Value = imageBox1.HorizontalScrollBar.Value;
                }
                if (imageBox.VerticalScrollBar.Value != imageBox1.VerticalScrollBar.Value)
                {
                    refrsh = true;
                    if (imageBox1.VerticalScrollBar.Value <= imageBox.VerticalScrollBar.Maximum)
                        imageBox.VerticalScrollBar.Value = imageBox1.VerticalScrollBar.Value;
                }
                if (refrsh)
                    imageBox.Refresh();
            }
        }

        /// <summary>
        /// Zmena stavu checkboxu pre synchronizaciu img boxov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbSync_CheckedChanged(object sender, EventArgs e)
        {
            if (_currentImg == null) return;
            if (!chbSync.Checked) return;
            _needScrollHandler = false;
            var nZoom = imageBox.ZoomScale;
            imageBox1.SetZoomScale(nZoom, default(Point));

            if (imageBox.HorizontalScrollBar.Value <= imageBox1.HorizontalScrollBar.Maximum)
                imageBox1.HorizontalScrollBar.Value = imageBox.HorizontalScrollBar.Value;

            if (imageBox.VerticalScrollBar.Value <= imageBox1.VerticalScrollBar.Maximum)
                imageBox1.VerticalScrollBar.Value = imageBox.VerticalScrollBar.Value;
            _needScrollHandler = true;
        }

        private void DrawToImageBox1(object sender, PaintEventArgs e)
        {
            if (imageBox.Image == null) return;
            if (!ProjectIsSet) return;
            if (SelectedTrack.Count == 0) return;

            _btm = new Bitmap(imageBox.Image.Bitmap.Width, imageBox.Image.Bitmap.Height);
            _graphics = Graphics.FromImage(_btm);
            _graphics.Clear(Color.FromArgb(0, 255, 255, 255));            

            var lineHeight = 2;
            var midPosition = 3;
            var pointSize = 6;
            if (chbGtModeEnabled.Checked)
            {
                lineHeight = 8;
                midPosition = 8;
                pointSize = 16;
            }

            for (int i = 0; i < SelectedTrack.Count-1; i++)
            {                
                var xCor1 = SelectedTrack[i].PointA.X + ((SelectedTrack[i].PointB.X - SelectedTrack[i].PointA.X) / 2);
                var yCor1 = SelectedTrack[i].PointA.Y + ((SelectedTrack[i].PointB.Y - SelectedTrack[i].PointA.Y) / 2);

                var xCor2 = SelectedTrack[i + 1].PointA.X + ((SelectedTrack[i + 1].PointB.X - SelectedTrack[i + 1].PointA.X) / 2);
                var yCor2 = SelectedTrack[i + 1].PointA.Y + ((SelectedTrack[i + 1].PointB.Y - SelectedTrack[i + 1].PointA.Y) / 2);

                _graphics.DrawLine(new Pen(Color.Chartreuse, lineHeight), new Point(xCor1, yCor1), new Point(xCor2, yCor2));                
            }

            List<BoundingBox> frame = null;
            if (GtFromProject)
            {
                frame = _currentImg.BoundBoxes;
            }
            else
            {
                frame = _gtObjects[_currentImgNum-1];
            }
            
            for (int i = 0; i < frame.Count; i++)
            {
                var trIndex = Array.IndexOf(frame[i].Properties.AtributesName, "track_id");
                if (trIndex >= 0)
                {
                    var trId = frame[i].Properties.AtributesValue[trIndex];
                    if (String.Compare(trId, SelectedTrackName, false) == 0)
                    {
                        var xCor = frame[i].PointA.X + ((frame[i].PointB.X - frame[i].PointA.X) / 2) - midPosition;
                        var yCor = frame[i].PointA.Y + ((frame[i].PointB.Y - frame[i].PointA.Y) / 2) - midPosition;

                        _graphics.DrawRectangle(new Pen(Color.Chartreuse), xCor, yCor, pointSize, pointSize);
                        _graphics.FillRectangle(new SolidBrush(Color.Chartreuse), xCor, yCor, pointSize, pointSize);
                        break;
                    }
                }
            }

            if (chbGtModeEnabled.Checked)
            {
                for (int i = 0; i < TestedTracks.Count - 1; i++)
                {
                    var point1Index = Array.IndexOf(TestedTracks[i].Properties.AtributesName, "track_id");
                    var point2Index = Array.IndexOf(TestedTracks[i + 1].Properties.AtributesName, "track_id");
                    var point1Id = TestedTracks[i].Properties.AtributesValue[point1Index];
                    var point2Id = TestedTracks[i + 1].Properties.AtributesValue[point2Index];

                    if ((String.Compare(point1Id, point2Id, false) == 0) && (chListTracks.CheckedItems.Contains(point1Id)))
                    {
                        var color = SelectColor(point1Id);
                        var xCor1 = TestedTracks[i].PointA.X + ((TestedTracks[i].PointB.X - TestedTracks[i].PointA.X) / 2);
                        var yCor1 = TestedTracks[i].PointA.Y + ((TestedTracks[i].PointB.Y - TestedTracks[i].PointA.Y) / 2);

                        var xCor2 = TestedTracks[i + 1].PointA.X + ((TestedTracks[i + 1].PointB.X - TestedTracks[i + 1].PointA.X) / 2);
                        var yCor2 = TestedTracks[i + 1].PointA.Y + ((TestedTracks[i + 1].PointB.Y - TestedTracks[i + 1].PointA.Y) / 2);
                        
                        _graphics.DrawLine(new Pen(color, 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));                        
                    }
                }

                List<BoundingBox> testCurrent = null;
                if (TestFromProject)
                {
                    testCurrent = _currentImg.BoundBoxes;
                }
                else
                {
                    testCurrent = _testObjects[_currentImgNum-1];
                }
                for (int i = 0; i < testCurrent.Count; i++)
                {
                    var trIndex = Array.IndexOf(testCurrent[i].Properties.AtributesName, "track_id");
                    if (trIndex >= 0)
                    {
                        var trId = testCurrent[i].Properties.AtributesValue[trIndex];
                        if (chListTracks.CheckedItems.Contains(trId))
                        {
                            var color = SelectColor(trId);
                            var xCor = testCurrent[i].PointA.X + ((testCurrent[i].PointB.X - testCurrent[i].PointA.X) / 2) - 3;
                            var yCor = testCurrent[i].PointA.Y + ((testCurrent[i].PointB.Y - testCurrent[i].PointA.Y) / 2) - 3;

                            _graphics.DrawRectangle(new Pen(color), xCor, yCor, 6, 6);
                            _graphics.FillRectangle(new SolidBrush(color), xCor, yCor, 6, 6);
                        }
                    }
                }
            }

            if (chbUncategorized.Checked && chbGtModeEnabled.Checked)
            {
                for (int i = 0; i < UncategorizedPoints.Count; i++)
                {
                    var xCor = UncategorizedPoints[i].PointA.X + ((UncategorizedPoints[i].PointB.X - UncategorizedPoints[i].PointA.X) / 2) - 3;
                    var yCor = UncategorizedPoints[i].PointA.Y + ((UncategorizedPoints[i].PointB.Y - UncategorizedPoints[i].PointA.Y) / 2) - 3;
                    _graphics.DrawRectangle(new Pen(Color.MintCream), xCor, yCor, 6, 6);
                    _graphics.FillRectangle(new SolidBrush(Color.MintCream), xCor, yCor, 6, 6);
                }
            }

            e.Graphics.DrawImageUnscaled(_btm, 0, 0);            
            _graphics.Dispose();            
        }

        private void cmbGtTracks_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmbIndex = cmbGtTracks.SelectedIndex;
            string trackName = cmbGtTracks.Items[cmbIndex].ToString().Substring(6);
            SelectedTrackName = trackName;

            if (_statsDictionary.ContainsKey(trackName))
            {
                tbTPosSel.Text = _statsDictionary[trackName][0] + "";                
                tbFNegSel.Text = _statsDictionary[trackName][1] + "";
            }
            else
            {
                tbTPosSel.Text = "0";                
                tbFNegSel.Text = "0";
            }

            SelectedTrack.Clear();
            TestedTracks.Clear();
            UncategorizedPoints.Clear();
            chListTracks.Items.Clear();
            List<int> imageNums = new List<int>(AllImages.Count);

            if (GtFromProject)
            {
                for (int i = 0; i < AllImages.Count; i++)
                {
                    for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                    {
                        var trIndex = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                        if (trIndex >= 0)
                        {
                            var trId = AllImages[i].BoundBoxes[j].Properties.AtributesValue[trIndex];
                            if (String.Compare(trId, trackName, false) == 0)
                            {
                                SelectedTrack.Add(AllImages[i].BoundBoxes[j]);
                                imageNums.Add(i);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _gtObjects.Count; i++)
                {
                    var frameBbs = _gtObjects[i];
                    for (int j = 0; j < frameBbs.Count; j++)
                    {
                        var trIndex = Array.IndexOf(frameBbs[j].Properties.AtributesName, "track_id");
                        if (trIndex >= 0)
                        {
                            var trId = frameBbs[j].Properties.AtributesValue[trIndex];
                            if (String.Compare(trId, trackName, false) == 0)
                            {
                                SelectedTrack.Add(frameBbs[j]);
                                imageNums.Add(i);
                                break;
                            }
                        }
                    }
                }
            }

            SetTestTracks(imageNums);

            imageBox.Refresh();
            imageBox1.Refresh();
        }

        private void SetTestTracks(List<int> imgNums)
        {
            try
            {
                List<string> selectedIds = new List<string>();
                for (int i = 0; i < imgNums.Count; i++)
                {
                    List<BoundingBox> imgBBs = null;
                    if (TestFromProject)
                    {
                        imgBBs = AllImages[imgNums[i]].BoundBoxes;
                    }
                    else
                    {
                        imgBBs = _testObjects[imgNums[i]];
                    }
                    for (int j = 0; j < imgBBs.Count; j++)
                    {
                        if (SelectedTrack[i].PointA.X == imgBBs[j].PointA.X && SelectedTrack[i].PointA.Y == imgBBs[j].PointA.Y
                            && SelectedTrack[i].Size.Width == imgBBs[j].Size.Width && SelectedTrack[i].Size.Height == imgBBs[j].Size.Height)
                        {
                            var trIndex = Array.IndexOf(imgBBs[j].Properties.AtributesName, "track_id");
                            if (trIndex >= 0)
                            {
                                var trId = imgBBs[j].Properties.AtributesValue[trIndex];
                                if (String.Compare(trId, "", false) == 0)
                                {
                                    UncategorizedPoints.Add(imgBBs[j]);
                                }
                                else if (!selectedIds.Contains(trId))
                                {
                                    selectedIds.Add(trId);
                                }
                            }
                            break;
                        }
                    }
                }
                chbUncategorized.Text = "Show uncategorized points(" + UncategorizedPoints.Count + ")";
                int falsePositives = 0;
                foreach (string tId in selectedIds)
                {
                    chListTracks.Items.Add(tId);

                    if (_statsTestDictionary.ContainsKey(tId))
                    {
                        falsePositives += _statsTestDictionary[tId];
                    }

                    for (int i = 0; i < AllImages.Count; i++)
                    {
                        List<BoundingBox> imgBBs = null;
                        if (TestFromProject)
                        {
                            imgBBs = AllImages[i].BoundBoxes;
                        }
                        else
                        {
                            imgBBs = _testObjects[i];
                        }
                        for (int j = 0; j < imgBBs.Count; j++)
                        {
                            var trIndex = Array.IndexOf(imgBBs[j].Properties.AtributesName, "track_id");
                            if (trIndex >= 0)
                            {
                                var trackIdNum = imgBBs[j].Properties.AtributesValue[trIndex];
                                if (String.Compare(trackIdNum, tId, false) == 0)
                                {
                                    TestedTracks.Add(imgBBs[j]);
                                    break;
                                }
                            }
                        }
                    }
                }

                tbFPosSel.Text = falsePositives + "";
            }
            catch(ArgumentOutOfRangeException e)
            {
                DisableProject();
                MessageBox.Show("Inconsistent data. ", "Data inconsistency error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
                
        private void chbGtModeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            imageBox.Refresh();
        }

        private void chListTracks_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
            this.BeginInvoke((MethodInvoker)(() => imageBox.Refresh()));
            this.BeginInvoke((MethodInvoker)(() => imageBox1.Refresh()));
        }

        private void DrawToImageBox2(object sender, PaintEventArgs e)
        {
            if (imageBox1.Image == null) return;
            if (!ProjectIsSet) return;
            if (SelectedTrack.Count == 0) return;

            _btm1 = new Bitmap(imageBox1.Image.Bitmap.Width, imageBox1.Image.Bitmap.Height);
            _graphics1 = Graphics.FromImage(_btm1);
            _graphics1.Clear(Color.FromArgb(0, 255, 255, 255));

            for (int i = 0; i < TestedTracks.Count - 1; i++)
            {
                var point1Index = Array.IndexOf(TestedTracks[i].Properties.AtributesName, "track_id");
                var point2Index = Array.IndexOf(TestedTracks[i + 1].Properties.AtributesName, "track_id");
                var point1Id = TestedTracks[i].Properties.AtributesValue[point1Index];
                var point2Id = TestedTracks[i + 1].Properties.AtributesValue[point2Index];

                if ((String.Compare(point1Id, point2Id, false) == 0) && (chListTracks.CheckedItems.Contains(point1Id)))
                {
                    var color = SelectColor(point1Id);
                    var xCor1 = TestedTracks[i].PointA.X + ((TestedTracks[i].PointB.X - TestedTracks[i].PointA.X) / 2);
                    var yCor1 = TestedTracks[i].PointA.Y + ((TestedTracks[i].PointB.Y - TestedTracks[i].PointA.Y) / 2);

                    var xCor2 = TestedTracks[i + 1].PointA.X + ((TestedTracks[i + 1].PointB.X - TestedTracks[i + 1].PointA.X) / 2);
                    var yCor2 = TestedTracks[i + 1].PointA.Y + ((TestedTracks[i + 1].PointB.Y - TestedTracks[i + 1].PointA.Y) / 2);                    

                    _graphics1.DrawLine(new Pen(color, 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));
                }
            }

            if (chbUncategorized.Checked)
            {
                for (int i = 0; i < UncategorizedPoints.Count; i++)
                {
                    var xCor = UncategorizedPoints[i].PointA.X + ((UncategorizedPoints[i].PointB.X - UncategorizedPoints[i].PointA.X) / 2) - 3;
                    var yCor = UncategorizedPoints[i].PointA.Y + ((UncategorizedPoints[i].PointB.Y - UncategorizedPoints[i].PointA.Y) / 2) - 3;
                    _graphics1.DrawRectangle(new Pen(Color.MintCream), xCor, yCor, 6, 6);
                    _graphics1.FillRectangle(new SolidBrush(Color.MintCream), xCor, yCor, 6, 6);
                }
            }

            List<BoundingBox> testCurrent = null;
            if (TestFromProject)
            {
                testCurrent = _currentImg.BoundBoxes;
            }
            else
            {
                testCurrent = _testObjects[_currentImgNum-1];
            }
            for (int i = 0; i < testCurrent.Count; i++)
            {
                var trIndex = Array.IndexOf(testCurrent[i].Properties.AtributesName, "track_id");
                if (trIndex >= 0)
                {
                    var trId = testCurrent[i].Properties.AtributesValue[trIndex];
                    if (chListTracks.CheckedItems.Contains(trId))
                    {
                        var color = SelectColor(trId);
                        var xCor = testCurrent[i].PointA.X + ((testCurrent[i].PointB.X - testCurrent[i].PointA.X) / 2) - 3;
                        var yCor = testCurrent[i].PointA.Y + ((testCurrent[i].PointB.Y - testCurrent[i].PointA.Y) / 2) - 3;

                        _graphics1.DrawRectangle(new Pen(color), xCor, yCor, 6, 6);
                        _graphics1.FillRectangle(new SolidBrush(color), xCor, yCor, 6, 6);
                    }
                }
            }

            e.Graphics.DrawImageUnscaled(_btm1, 0, 0);
            _graphics1.Dispose();
        }

        private Color SelectColor(string trackId)
        {
            try
            {
                var trackNum = Int32.Parse(trackId);
                var colorNum = trackNum % 21;

                switch (colorNum)
                {
                    case 0:
                        return Color.Blue;
                    case 1:
                        return Color.BlueViolet;
                    case 2:
                        return Color.Brown;
                    case 3:
                        return Color.Orange;
                    case 4:
                        return Color.Gold;
                    case 5:
                        return Color.Black;
                    case 6:
                        return Color.BurlyWood;
                    case 7:
                        return Color.Chocolate;
                    case 8:
                        return Color.Coral;
                    case 9:
                        return Color.CornflowerBlue;
                    case 10:
                        return Color.DarkBlue;
                    case 11:
                        return Color.DarkCyan;
                    case 12:
                        return Color.DarkGoldenrod;
                    case 13:
                        return Color.DarkOrange;
                    case 14:
                        return Color.DarkOrchid;
                    case 15:
                        return Color.DarkSalmon;
                    case 16:
                        return Color.Fuchsia;
                    case 17:
                        return Color.HotPink;
                    case 18:
                        return Color.Indigo;
                    case 19:
                        return Color.OrangeRed;
                    default:
                        return Color.Red;
                }
            }
            catch (FormatException)
            {
                return Color.Red;
            }
        }

        private void chbUncategorized_CheckedChanged(object sender, EventArgs e)
        {
            imageBox.Refresh();
            imageBox1.Refresh();
        }

        /// <summary>
        /// Prida polia dlzky 0
        /// </summary>
        private void CheckBbArrays(List<List<BoundingBox>> paImages)
        {
            for (int i = 0; i < paImages.Count; i++)
            {
                var aImage = paImages[i];
                for (int j = 0; j < aImage.Count; j++)
                {
                    if (aImage[j].Properties.AtributesName == null)
                    {
                        aImage[j].Properties.AtributesName = new string[0];
                        aImage[j].Properties.AtributesValue = new string[0];
                    }
                }
            }
        }

        /// <summary>
        /// Obsluha stlacenia tlacidla mysky pre imgbox gt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftClickZoom = imageBox.ZoomScale;
                _leftClickHScroll1 = imageBox.HorizontalScrollBar.Value;
                _leftClickVScroll1 = imageBox.VerticalScrollBar.Value;
                _leftClickHScroll2 = imageBox1.HorizontalScrollBar.Value;
                _leftClickVScroll2 = imageBox1.VerticalScrollBar.Value;
            }
        }

        /// <summary>
        /// Obsluha stlacenia tlacidla mysky pre imgbox test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftClickZoom = imageBox1.ZoomScale;
                _leftClickHScroll1 = imageBox.HorizontalScrollBar.Value;
                _leftClickVScroll1 = imageBox.VerticalScrollBar.Value;
                _leftClickHScroll2 = imageBox1.HorizontalScrollBar.Value;
                _leftClickVScroll2 = imageBox1.VerticalScrollBar.Value;
            }
        }

        /// <summary>
        /// Pocitanie tp, fp, fn
        /// </summary>
        private void CalcStats4FullSequence()
        {
            try
            { 
                _truePositives = 0;
                _falsePositives = 0;
                _falseNegatives = 0;
                _statsDictionary.Clear();
                _statsTestDictionary.Clear();

                List<BoundingBox> groundTruth;
                List<BoundingBox> testObjects;
                var gtTrack = "";
                var dictionaryId = "";

                for (int i = 0; i < AllImages.Count - 1; i++)
                {
                    if (GtFromProject)
                    {
                        groundTruth = AllImages[i].BoundBoxes;
                    }
                    else
                    {
                        groundTruth = _gtObjects[i];
                    }

                    if (TestFromProject)
                    {
                        testObjects = AllImages[i].BoundBoxes;
                    }
                    else
                    {
                        testObjects = _testObjects[i];
                    }

                    for (int j = 0; j < groundTruth.Count; j++)
                    {
                        var gtIndex = Array.IndexOf(groundTruth[j].Properties.AtributesName, "track_id");
                        if (gtIndex >= 0)
                        {
                            gtTrack = groundTruth[j].Properties.AtributesValue[gtIndex];
                            BoundingBox gtPair = null;
                            int gtFrameNum = 0;
                            if (String.Compare(gtTrack, "", false) != 0)
                            {
                                for (int k = i + 1; k < AllImages.Count; k++)
                                {
                                    List<BoundingBox> groundTruthPlus;
                                    if (GtFromProject)
                                    {
                                        groundTruthPlus = AllImages[k].BoundBoxes;
                                    }
                                    else
                                    {
                                        groundTruthPlus = _gtObjects[k];
                                    }
                                    for (int l = 0; l < groundTruthPlus.Count; l++)
                                    {
                                        var pairGtIndex = Array.IndexOf(groundTruthPlus[l].Properties.AtributesName, "track_id");
                                        if (pairGtIndex >= 0)
                                        {
                                            if (String.Compare(gtTrack, groundTruthPlus[l].Properties.AtributesValue[pairGtIndex], false) == 0)
                                            {
                                                gtFrameNum = k;
                                                gtPair = groundTruthPlus[l];
                                                break;
                                            }
                                        }
                                    }
                                    if (gtPair != null)
                                    {
                                        break;
                                    }
                                }
                            }
                            //par v test objektoch
                            BoundingBox testPair = null;
                            int testFrameNum = 0;
                            string testId = null;
                            for (int k = 0; k < testObjects.Count; k++)
                            {
                                if (testObjects[k].PointA.X == groundTruth[j].PointA.X && testObjects[k].PointA.Y == groundTruth[j].PointA.Y
                                    && testObjects[k].PointB.X == groundTruth[j].PointB.X && testObjects[k].PointB.Y == groundTruth[j].PointB.Y)
                                {
                                    var testIndex = Array.IndexOf(testObjects[k].Properties.AtributesName, "track_id");
                                    if (testIndex >= 0)
                                    {
                                        testId = testObjects[k].Properties.AtributesValue[testIndex];
                                    }
                                }
                            }
                            if (testId == null)
                            {
                                DisableProject();
                                MessageBox.Show("Unable to calculate stats. ", "Data inconsistency error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (String.Compare(testId, "", false) != 0)
                            {
                                for (int k = i + 1; k < AllImages.Count; k++)
                                {
                                    List<BoundingBox> testObjectsPlus;
                                    if (TestFromProject)
                                    {
                                        testObjectsPlus = AllImages[k].BoundBoxes;
                                    }
                                    else
                                    {
                                        testObjectsPlus = _testObjects[k];
                                    }

                                    for (int l = 0; l < testObjectsPlus.Count; l++)
                                    {
                                        var pairTestIndex = Array.IndexOf(testObjectsPlus[l].Properties.AtributesName, "track_id");
                                        if (pairTestIndex >= 0)
                                        {
                                            if (String.Compare(testId, testObjectsPlus[l].Properties.AtributesValue[pairTestIndex], false) == 0)
                                            {
                                                testFrameNum = k;
                                                testPair = testObjectsPlus[l];
                                                dictionaryId = testId;
                                                break;
                                            }
                                        }
                                    }
                                    if (testPair != null)
                                    {
                                        break;
                                    }
                                }
                            }

                            //ak chyba v slovniku treba pridat
                            if (!_statsDictionary.ContainsKey(gtTrack))
                            {
                                _statsDictionary.Add(gtTrack, new int[] { 0, 0 });
                            }

                            //porovnanie
                            if (testPair == null && gtPair == null)//oba nezaradene
                            {
                                //spravne ale zatial nic
                            }
                            else if (testPair == null && gtPair != null)//povodny spojeny, testovaci nespojeny
                            {
                                _falseNegatives++;
                                _statsDictionary[gtTrack][1]++;
                            }
                            else if (testPair != null && gtPair == null)//povodny nespojeny, testovaci spojeny
                            {
                                _falsePositives++;
                                if (!_statsTestDictionary.ContainsKey(dictionaryId))
                                {
                                    _statsTestDictionary.Add(dictionaryId, 1);
                                }
                                else
                                {
                                    _statsTestDictionary[dictionaryId]++;
                                }
                            }
                            else if (testPair.PointA == gtPair.PointA && testPair.PointB == gtPair.PointB && testFrameNum == gtFrameNum)//spravne spojene
                            {
                                _truePositives++;
                                _statsDictionary[gtTrack][0]++;
                            }
                            else//nespravne spojene
                            {
                                _falseNegatives++;
                                _falsePositives++;
                                _statsDictionary[gtTrack][1]++;
                                if (!_statsTestDictionary.ContainsKey(dictionaryId))
                                {
                                    _statsTestDictionary.Add(dictionaryId, 1);
                                }
                                else
                                {
                                    _statsTestDictionary[dictionaryId]++;
                                }
                            }
                        }
                    }
                }
                tbTPositives.Text = _truePositives + "";
                tbFPositives.Text = _falsePositives + "";
                tbFNegatives.Text = _falseNegatives + "";

                double tpPerc = Math.Round((_truePositives * 100.0) / (_truePositives + _falseNegatives + _falsePositives), 2);
                if (Double.IsNaN(tpPerc))
                    tpPerc = 0;
                double fpPerc = Math.Round((_falsePositives * 100.0) / (_truePositives + _falseNegatives + _falsePositives), 2);
                if (Double.IsNaN(fpPerc))
                    fpPerc = 0;
                double fnPerc = Math.Round((_falseNegatives * 100.0) / (_truePositives + _falseNegatives + _falsePositives), 2);
                if (Double.IsNaN(fnPerc))
                    fnPerc = 0;

                tbTPPerc.Text = tpPerc + "%";
                tbFPPerc.Text = fpPerc + "%";
                tbFNPerc.Text = fnPerc + "%";
                tsProgressBar.Visible = false;
                tsStatusLabel.Visible = false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                DisableProject();
                MessageBox.Show("Inconsistent data. ", "Data inconsistency error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void DisableProject()
        {
            ProjectIsSet = false;
            cmbGtTracks.Enabled = false;
            chListTracks.Enabled = false;
            chbUncategorized.Enabled = false;
        }

        private void nmTruePositives_ValueChanged(object sender, EventArgs e)
        {
            _mainWin.TruePositiveWeight = (int)nmTruePositives.Value;
        }

        private void nmFalsePositives_ValueChanged(object sender, EventArgs e)
        {
            _mainWin.FalsePositiveWeight = (int)nmFalsePositives.Value;
        }

        private void nmFalseNegatives_ValueChanged(object sender, EventArgs e)
        {
            _mainWin.FalseNegativeWeight = (int)nmFalseNegatives.Value;
        }
    }
}
