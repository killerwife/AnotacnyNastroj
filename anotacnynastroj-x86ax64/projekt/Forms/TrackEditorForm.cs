using Projekt.DrawObjects;
using Projekt.Figure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt.Forms
{
    public partial class TrackEditorForm : Form
    {
        private MainWindowApplication _mainWin;
        private BaseFigure _currentImg;
        private string _selectedObj;        
        private int _imageNum;
        private int _gapSize;
        private int _framesPlus;
        private int _activeTool;
        private double _actualZoom;

        private Bitmap _btm = new Bitmap(1, 1);
        Graphics _graphics = null;

        public List<BaseFigure> AllImages { get; set; }        
        private List<BoundingBox> BBtracks;
        
        public TrackEditorForm(MainWindowApplication paMain)
        {
            InitializeComponent();
            _mainWin = paMain;
            _selectedObj = null;            
            BBtracks = new List<BoundingBox>();
            _gapSize = 3;
            _activeTool = 0;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            numericUpDown2.Enabled = false;
            _framesPlus = 1;
            imageBox.MouseWheel += ImgeBoxOnMouseWheel;
            _actualZoom = 1;
        }

        public string SelectedObj
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

        private void EndWork(object sender, FormClosingEventArgs e)
        {
            _mainWin.TrackWin = null; 
        }

        public void SetImageToBox(BaseFigure image)
        {
            _currentImg = image;            
            _imageNum = AllImages.IndexOf(image);
            if (imageBox.Image != null) imageBox.Image.Dispose();
            imageBox.Image = image == null ? null : image.GetImage();
            SetZoom();
            //imageBox.SetZoomScale(_currentImg.Zooom, new Point(0, 0));
            //SetMaxOfScrollBar();            
            imageBox.Refresh();
        }

        private void SetMaxOfScrollBar()
        {
            int horizMax =
                (int)Math.Ceiling(imageBox.Image.Size.Width - ((imageBox.ClientSize.Width - imageBox.VerticalScrollBar.Width) / _currentImg.Zooom));

            int vertMax =
                (int)Math.Ceiling(imageBox.Image.Size.Height - ((imageBox.ClientSize.Height - imageBox.HorizontalScrollBar.Height) / _currentImg.Zooom));

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

        private void SetZoom()
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
            _actualZoom = Math.Floor(zoomNum * 100) / 100;        
            imageBox.SetZoomScale(Math.Floor(zoomNum * 100) / 100, new Point(0, 0));
        }

        private void FormResize(object sender, EventArgs e)
        {
            SetZoom();
        }

        public void SelectObject(DrawObject paObj)
        {
            if (paObj != null)
            {
                if (_selectedObj != null)
                {
                    var oldPos = checkedListBox1.Items.IndexOf(_selectedObj);
                    if (oldPos >= 0)
                    {
                        checkedListBox1.SetItemCheckState(oldPos, CheckState.Unchecked);
                    }
                }

                int trIdIndex = Array.IndexOf(paObj.Properties.AtributesName, "track_id");
                if (trIdIndex >= 0)
                {
                    if (String.Compare(paObj.Properties.AtributesValue[trIdIndex], "", false) != 0)
                    {
                        _selectedObj = paObj.Properties.AtributesValue[trIdIndex];                        
                    }
                    else
                    {
                        _selectedObj = null;                        
                    }
                }
                else
                {
                    _selectedObj = null;                    
                }

                BBtracks.Clear();
                if (_selectedObj != null)
                {
                    for (int i = 0; i < AllImages.Count; i++)
                    {
                        for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                        {
                            var ind = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                            if (ind >= 0)
                            {
                                if (String.Compare(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind], _selectedObj, false) == 0)
                                {
                                    BBtracks.Add(AllImages[i].BoundBoxes[j]);
                                    break;
                                }
                            }
                        }
                    }

                    var itPos = checkedListBox1.Items.IndexOf(_selectedObj);
                    if (itPos >= 0)
                    {
                        checkedListBox1.SetItemCheckState(itPos, CheckState.Indeterminate);
                    }
                }
                else
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }    
                }
            }
            else
            {
                if (_activeTool == 1)
                {
                    _activeTool = 0;
                    ((Panel)pnlConnectTracks).ResetBackColor();
                    groupBox1.Enabled = false;
                    numericUpDown2.Enabled = false;
                }
                if (_activeTool != 2)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
                else
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemCheckState(i) == CheckState.Indeterminate)
                        {
                            checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                        }                        
                    }
                }
                _selectedObj = null;                
                BBtracks.Clear();
            }
            
            imageBox.Refresh();
        }

        private void DrawToImageBox(object sender, PaintEventArgs e)
        {
            if (imageBox.Image == null) return;
            _btm = new Bitmap(imageBox.Image.Bitmap.Width, imageBox.Image.Bitmap.Height);
            _graphics = Graphics.FromImage(_btm);
            _graphics.Clear(Color.FromArgb(0, 255, 255, 255));

            if (_selectedObj == null)
            {
                foreach (var bb in _currentImg.BoundBoxes)
                {
                    var xCor = bb.PointA.X + ((bb.PointB.X - bb.PointA.X) / 2) - 3;
                    var yCor = bb.PointA.Y + ((bb.PointB.Y - bb.PointA.Y) / 2) - 3;

                    _graphics.DrawRectangle(new Pen(Color.Crimson), xCor, yCor, 6, 6);
                    _graphics.FillRectangle(new SolidBrush(Color.Crimson), xCor, yCor, 6, 6);
                }
            }
            else
            {                
                foreach (var bb in _currentImg.BoundBoxes)
                {
                    var ind = Array.IndexOf(bb.Properties.AtributesName, "track_id");
                    if (ind >= 0)
                    {
                        if (String.Compare(bb.Properties.AtributesValue[ind], _selectedObj, false) == 0)
                        {
                            var xCor = bb.PointA.X + ((bb.PointB.X - bb.PointA.X) / 2) - 3;
                            var yCor = bb.PointA.Y + ((bb.PointB.Y - bb.PointA.Y) / 2) - 3;

                            _graphics.DrawRectangle(new Pen(Color.Aqua), xCor, yCor, 6, 6);
                            _graphics.FillRectangle(new SolidBrush(Color.Aqua), xCor, yCor, 6, 6);
                            break;
                        }
                    }
                }

                if (BBtracks.Count > 1)
                {
                    for (int i = 0; i < BBtracks.Count - 1; i++)
                    {
                        var xCor1 = BBtracks[i].PointA.X + ((BBtracks[i].PointB.X - BBtracks[i].PointA.X) / 2);
                        var yCor1 = BBtracks[i].PointA.Y + ((BBtracks[i].PointB.Y - BBtracks[i].PointA.Y) / 2);

                        var xCor2 = BBtracks[i + 1].PointA.X + ((BBtracks[i + 1].PointB.X - BBtracks[i + 1].PointA.X) / 2);
                        var yCor2 = BBtracks[i + 1].PointA.Y + ((BBtracks[i + 1].PointB.Y - BBtracks[i + 1].PointA.Y) / 2);

                        _graphics.DrawLine(new Pen(Color.Aqua, 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));
                    }
                }
            }
            if (_activeTool == 1)
            {
                DrawConnectTracks();
            }
            else
            {
                DrawSecondary();
            }

            e.Graphics.DrawImageUnscaled(_btm, 0, 0);
            _graphics.Dispose(); 
        }

        public void AddCheckBoxesItems()
        {
            int num;
            for (int i = 0; i < AllImages.Count; i++)
            {
                for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                {
                    var ind = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                    if (ind >= 0)
                    {
                        if (!checkedListBox1.Items.Contains(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind]) &&
                            String.Compare(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind], "", true) != 0 &&
                            int.TryParse(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind], out num))
                        {                            
                            checkedListBox1.Items.Add(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind]);
                        }
                    }
                }    
            }
            checkedListBox1.Sorted = true;
        }

        private void DrawSecondary()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)             
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    for (int j = 0; j < _currentImg.BoundBoxes.Count; j++)
                    {
                        var ind = Array.IndexOf(_currentImg.BoundBoxes[j].Properties.AtributesName, "track_id");
                        if (ind >= 0)
                        {
                            if (String.Compare(_currentImg.BoundBoxes[j].Properties.AtributesValue[ind], checkedListBox1.Items[i].ToString(), false) == 0)                            
                            {
                                var xCor = _currentImg.BoundBoxes[j].PointA.X + ((_currentImg.BoundBoxes[j].PointB.X - _currentImg.BoundBoxes[j].PointA.X) / 2) - 3;
                                var yCor = _currentImg.BoundBoxes[j].PointA.Y + ((_currentImg.BoundBoxes[j].PointB.Y - _currentImg.BoundBoxes[j].PointA.Y) / 2) - 3;
                                Color objColor = SelectColor(checkedListBox1.Items[i].ToString());
                                _graphics.DrawRectangle(new Pen(objColor), xCor, yCor, 6, 6);
                                _graphics.FillRectangle(new SolidBrush(objColor), xCor, yCor, 6, 6);
                                //DrawSecondaryTracks(objColor, checkedListBox1.Items[i].ToString());
                                break;
                            }
                        }
                    }
                }                
            }

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    Color objColor = SelectColor(checkedListBox1.Items[i].ToString());
                    DrawSecondaryTracks(objColor, checkedListBox1.Items[i].ToString());
                }    
            }
        }

        private void DrawSecondaryTracks(Color lineColor, string trackId)
        {
            //List<BoundingBox> secTracks2 = new List<BoundingBox>();
            int min = 0; int max = 0;

            if (_imageNum - _gapSize >= 0)
            {
                min = _imageNum - _gapSize;
            }
            else
            {
                min = 0;
            }
            max = _imageNum;
            DrawLines4Secondary(min, max, lineColor, trackId, true);

            if (_imageNum + _gapSize < AllImages.Count)
            {
                max = _imageNum + _gapSize;
            }
            else
            {
                max = AllImages.Count - 1;
            }
            min = _imageNum;
            DrawLines4Secondary(min, max, lineColor, trackId, false);
        }

        private void DrawLines4Secondary(int min, int max, Color color, string trackId, bool transparency)
        {
            List<BoundingBox> secTracks1 = new List<BoundingBox>();

            for (int i = min; i <= max; i++)
            {
                for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                {
                    var ind = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                    if (ind >= 0)
                    {
                        if (String.Compare(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind], trackId, false) == 0)
                        {
                            secTracks1.Add(AllImages[i].BoundBoxes[j]);
                            break;
                        }
                    }
                }
            }

            if (secTracks1.Count > 1)
            {
                for (int i = 0; i < secTracks1.Count - 1; i++)
                {
                    var xCor1 = secTracks1[i].PointA.X + ((secTracks1[i].PointB.X - secTracks1[i].PointA.X) / 2);
                    var yCor1 = secTracks1[i].PointA.Y + ((secTracks1[i].PointB.Y - secTracks1[i].PointA.Y) / 2);

                    var xCor2 = secTracks1[i + 1].PointA.X + ((secTracks1[i + 1].PointB.X - secTracks1[i + 1].PointA.X) / 2);
                    var yCor2 = secTracks1[i + 1].PointA.Y + ((secTracks1[i + 1].PointB.Y - secTracks1[i + 1].PointA.Y) / 2);

                    if (transparency)
                    {
                        _graphics.DrawLine(new Pen(Color.FromArgb(127, color), 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));
                    }
                    else
                    {
                        _graphics.DrawLine(new Pen(color, 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));
                    }
                }
            }    
        }

        private Color SelectColor(string initValue)
        {
            Random rndGen = new Random(Int32.Parse(initValue));
            var colorNum = rndGen.Next(1, 25);

            switch (colorNum)
            {
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.BlueViolet;
                case 3:
                    return Color.Brown;
                case 4:
                    return Color.Chartreuse;
                case 5:
                    return Color.Orange;
                case 6:
                    return Color.DarkGreen;
                case 7:
                    return Color.Gold;
                case 8:
                    return Color.Black;
                case 9:
                    return Color.BurlyWood;
                case 10:
                    return Color.Chocolate;
                case 11:
                    return Color.Coral;
                case 12:
                    return Color.CornflowerBlue;
                case 13:
                    return Color.DarkBlue;
                case 14:
                    return Color.DarkCyan;
                case 15:
                    return Color.DarkGoldenrod;
                case 16:
                    return Color.DarkOrange;
                case 17:
                    return Color.DarkOrchid;
                case 18:
                    return Color.DarkSalmon;
                case 19:
                    return Color.Fuchsia;
                case 20:
                    return Color.GreenYellow;
                case 21:
                    return Color.HotPink;
                case 22:
                    return Color.Indigo;
                case 23:
                    return Color.OrangeRed;
                default:
                    return Color.Red;
            }
        }

        private void StateChanged(object sender, ItemCheckEventArgs e)
        {                       
            this.BeginInvoke((MethodInvoker)(() => imageBox.Refresh()));
        }

        private void GapChanged(object sender, EventArgs e)
        {
            _gapSize = (int)numericUpDown1.Value;
            imageBox.Refresh();
        }

        private void FramesChanged(object sender, EventArgs e)
        {
            _framesPlus = (int)numericUpDown2.Value;
            imageBox.Refresh();
        }

        private void DrawConnectTracks()
        {
            if (_imageNum + _framesPlus >= AllImages.Count)
            { return; }

            if (radioButton1.Checked)
            {
                UnassignedPointsDraw();                    
            }
            else if (radioButton2.Checked)
            {
                TracksSecDraw();
            }
            else
            {
                UnassignedPointsDraw();
                TracksSecDraw();
            }
        }

        private void UnassignedPointsDraw()
        {
            for (int i = 0; i < AllImages[_imageNum + _framesPlus].BoundBoxes.Count; i++)
            {
                var ind = Array.IndexOf(AllImages[_imageNum + _framesPlus].BoundBoxes[i].Properties.AtributesName, "track_id");
                if (ind >= 0)
                {
                    if (String.Compare(AllImages[_imageNum + _framesPlus].BoundBoxes[i].Properties.AtributesValue[ind], "", false) == 0)
                    {
                        var xCor = AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointA.X + ((AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointB.X - AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointA.X) / 2) - 3;
                        var yCor = AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointA.Y + ((AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointB.Y - AllImages[_imageNum + _framesPlus].BoundBoxes[i].PointA.Y) / 2) - 3;
                        _graphics.DrawRectangle(new Pen(Color.Crimson), xCor, yCor, 6, 6);
                        _graphics.FillRectangle(new SolidBrush(Color.Crimson), xCor, yCor, 6, 6);
                    }
                }
            }    
        }

        private void TracksSecDraw()
        {
            var min = _imageNum + _framesPlus;
            var max = 0;
            if (_imageNum + _framesPlus + _gapSize < AllImages.Count)
            {
                max = _imageNum + _framesPlus + _gapSize;
            }
            else
            {
                max = AllImages.Count - 1;
            }

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    Color objColor = SelectColor(checkedListBox1.Items[i].ToString());
                    List<BoundingBox> secTracks = new List<BoundingBox>();
                    var drawTrack = false;
                    for (int j = min; j <= max; j++)
                    {
                        for (int k = 0; k < AllImages[j].BoundBoxes.Count; k++)
                        {
                            var ind = Array.IndexOf(AllImages[j].BoundBoxes[k].Properties.AtributesName, "track_id");
                            if (ind >= 0)
                            {
                                if (String.Compare(AllImages[j].BoundBoxes[k].Properties.AtributesValue[ind], checkedListBox1.Items[i].ToString(), false) == 0)
                                {
                                    secTracks.Add(AllImages[j].BoundBoxes[k]);
                                    if (j == min)
                                    {
                                        var xCor = AllImages[j].BoundBoxes[k].PointA.X + ((AllImages[j].BoundBoxes[k].PointB.X - AllImages[j].BoundBoxes[k].PointA.X) / 2) - 3;
                                        var yCor = AllImages[j].BoundBoxes[k].PointA.Y + ((AllImages[j].BoundBoxes[k].PointB.Y - AllImages[j].BoundBoxes[k].PointA.Y) / 2) - 3;
                                        _graphics.DrawRectangle(new Pen(objColor), xCor, yCor, 6, 6);
                                        _graphics.FillRectangle(new SolidBrush(objColor), xCor, yCor, 6, 6);
                                        drawTrack = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    if (secTracks.Count > 1 && drawTrack)
                    {
                        for (int j = 0; j < secTracks.Count - 1; j++)
                        {
                            var xCor1 = secTracks[j].PointA.X + ((secTracks[j].PointB.X - secTracks[j].PointA.X) / 2);
                            var yCor1 = secTracks[j].PointA.Y + ((secTracks[j].PointB.Y - secTracks[j].PointA.Y) / 2);

                            var xCor2 = secTracks[j + 1].PointA.X + ((secTracks[j + 1].PointB.X - secTracks[j + 1].PointA.X) / 2);
                            var yCor2 = secTracks[j + 1].PointA.Y + ((secTracks[j + 1].PointB.Y - secTracks[j + 1].PointA.Y) / 2);

                            _graphics.DrawLine(new Pen(objColor, 2), new Point(xCor1, yCor1), new Point(xCor2, yCor2));
                        }
                    }
                }
            }    
        }

        #region panel
        
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

        private void SelectConnTracks(object sender, EventArgs e)
        {
            if (_selectedObj != null)
            {
                if (_activeTool == 1)
                {
                    _activeTool = 0;
                    ((Panel)pnlConnectTracks).ResetBackColor();
                    groupBox1.Enabled = false;
                    numericUpDown2.Enabled = false;
                }
                else
                {
                    if (_activeTool == 2)
                    {
                        ((Panel)pnlSplitTracks).ResetBackColor();
                        groupBox2.Enabled = false;     
                    }
                    _activeTool = 1;
                    pnlConnectTracks.BackColor = Color.SteelBlue;
                    groupBox1.Enabled = true;
                    numericUpDown2.Enabled = true;
                }
                imageBox.Refresh();
            }
            else
            {
                MessageBox.Show("Je potrebné zvoliť track!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SelectSplitTracks(object sender, EventArgs e)
        {
            if (_activeTool == 2)
            {
                _activeTool = 0;
                ((Panel)pnlSplitTracks).ResetBackColor();
                groupBox2.Enabled = false;                
            }
            else
            {
                if (_activeTool == 1)
                {
                    ((Panel)pnlConnectTracks).ResetBackColor();
                    groupBox1.Enabled = false;
                    numericUpDown2.Enabled = false;    
                }
                _activeTool = 2;
                pnlSplitTracks.BackColor = Color.SteelBlue;
                groupBox2.Enabled = true;                
            }
            imageBox.Refresh();
        }

        private void ImgBoxMouseEnter(object sender, EventArgs e)
        {
            if (!imageBox.Focused)
                imageBox.Focus();
            imageBox.Cursor = Cursors.Cross;
        }

        private void ImgBoxMouseLeave(object sender, EventArgs e)
        {
            if (imageBox.Focused)
                imageBox.Parent.Focus();            
        }

        #endregion 

        private void DisplayStyleChanged(object sender, EventArgs e)
        {
            imageBox.Refresh();
        }

        public void ResetCheckboxesItems()
        {
            checkedListBox1.Items.Clear();
            AddCheckBoxesItems();
        }

        private void ImgBoxClick(object sender, EventArgs e)
        {
            if (_currentImg == null || _activeTool == 0)
                return;
            var mouseButton = (MouseEventArgs)e;
            if (mouseButton.Button == MouseButtons.Middle)
                return;

            var mouse = (e as MouseEventArgs);
            var pointClick = UnzoomAndUnscrollPoint(mouse.Location);

            if (_activeTool == 2)
            {
                SplitTrack(pointClick);    
            }

            if (_activeTool == 1)
            {
                ConnectTracks(pointClick);
            }
        }

        private void ImgeBoxOnMouseWheel(object sender, MouseEventArgs e)
        {
            if (_currentImg == null)          
                return;

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
            {
                return;
            }

            _actualZoom = _actualZoom * scale;            
        }

        private Point UnzoomAndUnscrollPoint(Point p)
        {
            return new Point((int)Math.Floor(p.X / _actualZoom + imageBox.HorizontalScrollBar.Value),
                             (int)Math.Floor(p.Y / _actualZoom + imageBox.VerticalScrollBar.Value));            
        }

        private void SplitTrack(Point p)
        { 
            BoundingBox wBb = null;
            int bbIndex = -1;

            for (int i = 0; i < _currentImg.BoundBoxes.Count; i++)
            {
                bbIndex = Array.IndexOf(_currentImg.BoundBoxes[i].Properties.AtributesName, "track_id");
                if (bbIndex >= 0)
                {
                    var trId = _currentImg.BoundBoxes[i].Properties.AtributesValue[bbIndex];
                    if (checkedListBox1.CheckedItems.Contains(trId) || String.Compare(trId, _selectedObj, false) == 0)
                    {
                        var xCor = _currentImg.BoundBoxes[i].PointA.X + ((_currentImg.BoundBoxes[i].PointB.X - _currentImg.BoundBoxes[i].PointA.X) / 2) - 3;
                        var yCor = _currentImg.BoundBoxes[i].PointA.Y + ((_currentImg.BoundBoxes[i].PointB.Y - _currentImg.BoundBoxes[i].PointA.Y) / 2) - 3;
                        if (p.X >= xCor && p.X <= xCor + 6 && p.Y >= yCor && p.Y <= yCor + 6)
                        {
                            wBb = _currentImg.BoundBoxes[i];
                            break;
                        }
                    }
                }
            }

            if (wBb != null)
            { 
                var changedIndex = wBb.Properties.AtributesValue[bbIndex];
                var newIndex = ReturnNewIndex();

                var beginFrame = 0;
                var endFrame = 0;
                if (rbEndPoint.Checked)
                {
                    beginFrame = _imageNum + 1;
                    endFrame = AllImages.Count;
                }
                else
                {
                    if (_imageNum != 0)
                    {
                        beginFrame = 0;
                        endFrame = _imageNum;
                    }
                }

                int changedBbs = 0;
                for (int i = beginFrame; i < endFrame; i++)
                {
                    for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                    {
                        if (String.Compare(wBb.Properties.Class, AllImages[i].BoundBoxes[j].Properties.Class, false) == 0 &&
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue.Length > bbIndex &&
                            String.Compare(changedIndex, AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex], false) == 0)
                        {
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex] = newIndex.ToString();
                            changedBbs++;
                            break;
                        }
                    }
                }

                if (changedBbs > 0)
                {
                    checkedListBox1.Items.Add(newIndex.ToString());
                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.Count-1, CheckState.Checked);  
                }

                RedrawSelectedTrack();
                
                imageBox.Refresh();
            }
        }

        private void RedrawSelectedTrack()
        {
            if (_selectedObj != null)
            {
                BBtracks.Clear();
                for (int i = 0; i < AllImages.Count; i++)
                {
                    for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                    {
                        var ind = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                        if (ind >= 0)
                        {
                            if (String.Compare(AllImages[i].BoundBoxes[j].Properties.AtributesValue[ind], _selectedObj, false) == 0)
                            {
                                BBtracks.Add(AllImages[i].BoundBoxes[j]);
                                break;
                            }
                        }
                    }
                }
            }    
        }

        private void ConnectTracks(Point p)
        {
            BoundingBox wBb = null;
            int bbIndex = -1;

            int nextImageNum = _imageNum + (int)numericUpDown2.Value; 

            if(nextImageNum < AllImages.Count)
            {
                for (int i = 0; i < AllImages[nextImageNum].BoundBoxes.Count; i++)
                {
                    bbIndex = Array.IndexOf(AllImages[nextImageNum].BoundBoxes[i].Properties.AtributesName, "track_id");
                    if (bbIndex >= 0)
                    {
                        var trId = AllImages[nextImageNum].BoundBoxes[i].Properties.AtributesValue[bbIndex];
                        if (radioButton3.Checked)
                        {
                            if (checkedListBox1.CheckedItems.Contains(trId) || String.Compare(trId, "", false) == 0)
                            {
                                var xCor = AllImages[nextImageNum].BoundBoxes[i].PointA.X + ((AllImages[nextImageNum].BoundBoxes[i].PointB.X - AllImages[nextImageNum].BoundBoxes[i].PointA.X) / 2) - 3;
                                var yCor = AllImages[nextImageNum].BoundBoxes[i].PointA.Y + ((AllImages[nextImageNum].BoundBoxes[i].PointB.Y - AllImages[nextImageNum].BoundBoxes[i].PointA.Y) / 2) - 3;
                                if (p.X >= xCor && p.X <= xCor + 6 && p.Y >= yCor && p.Y <= yCor + 6)
                                {
                                    wBb = AllImages[nextImageNum].BoundBoxes[i];
                                    break;
                                }
                            }
                        }
                        else if (radioButton2.Checked)
                        {
                            if (checkedListBox1.CheckedItems.Contains(trId))
                            {
                                var xCor = AllImages[nextImageNum].BoundBoxes[i].PointA.X + ((AllImages[nextImageNum].BoundBoxes[i].PointB.X - AllImages[nextImageNum].BoundBoxes[i].PointA.X) / 2) - 3;
                                var yCor = AllImages[nextImageNum].BoundBoxes[i].PointA.Y + ((AllImages[nextImageNum].BoundBoxes[i].PointB.Y - AllImages[nextImageNum].BoundBoxes[i].PointA.Y) / 2) - 3;
                                if (p.X >= xCor && p.X <= xCor + 6 && p.Y >= yCor && p.Y <= yCor + 6)
                                {
                                    wBb = AllImages[nextImageNum].BoundBoxes[i];
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (String.Compare(trId, "", false) == 0)
                            {
                                var xCor = AllImages[nextImageNum].BoundBoxes[i].PointA.X + ((AllImages[nextImageNum].BoundBoxes[i].PointB.X - AllImages[nextImageNum].BoundBoxes[i].PointA.X) / 2) - 3;
                                var yCor = AllImages[nextImageNum].BoundBoxes[i].PointA.Y + ((AllImages[nextImageNum].BoundBoxes[i].PointB.Y - AllImages[nextImageNum].BoundBoxes[i].PointA.Y) / 2) - 3;
                                if (p.X >= xCor && p.X <= xCor + 6 && p.Y >= yCor && p.Y <= yCor + 6)
                                {
                                    wBb = AllImages[nextImageNum].BoundBoxes[i];
                                    break;
                                }
                            }    
                        }
                    }
                }
            }

            if (wBb == null)
            {
                return;
            }

            if (String.Compare(wBb.Properties.AtributesValue[bbIndex], "", false) == 0)
            {
                ChangeSelectedId();
                wBb.Properties.AtributesValue[bbIndex] = _selectedObj;
            }
            else
            {
                ChangeSelectedId();
                DeleteUnusedTrack(wBb, nextImageNum-1, bbIndex);
                var objClass = wBb.Properties.Class;
                var objId = wBb.Properties.AtributesValue[bbIndex];
                for (int i = nextImageNum; i < AllImages.Count; i++)
                {
                    for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                    {
                        if (String.Compare(objClass, AllImages[i].BoundBoxes[j].Properties.Class, false) == 0 &&
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue.Length > bbIndex &&
                            String.Compare(objId, AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex], false) == 0)
                        {
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex] = _selectedObj;                            
                            break;    
                        }
                    }
                }
            }

            RedrawSelectedTrack();
            imageBox.Refresh();
        }

        private int ReturnNewIndex()
        {
            var newIndex = 0;
            if (checkedListBox1.Items.Count == 0)
            {
                newIndex = 0;
            }
            else
            {
                newIndex = Convert.ToInt32(checkedListBox1.Items[checkedListBox1.Items.Count - 1]) + 1;
            }

            return newIndex;
        }

        private void ChangeSelectedId()
        {
            var newIndex = ReturnNewIndex();
            var changed = 0;
            int bbIndex = -1;

            for (int i = _imageNum + 1; i < AllImages.Count; i++)
            {
                for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                {
                    bbIndex = Array.IndexOf(AllImages[i].BoundBoxes[j].Properties.AtributesName, "track_id");
                    if (bbIndex >= 0)
                    {
                        var trId = AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex];
                        if (String.Compare(trId, _selectedObj, false) == 0)
                        {
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue[bbIndex] = newIndex.ToString();
                            changed++;
                            break;    
                        }
                    }
                }        
            }

            if (changed > 0)
            {
                checkedListBox1.Items.Add(newIndex.ToString());   
            }
        }

        private void DeleteUnusedTrack(BoundingBox trackOb, int startFrame, int attrPos)
        {
            var trackCount = 0;
            for (int i = startFrame; i >= 0; i--)
            {
                for (int j = 0; j < AllImages[i].BoundBoxes.Count; j++)
                {
                    if (String.Compare(trackOb.Properties.Class, AllImages[i].BoundBoxes[j].Properties.Class, false) == 0 &&
                            AllImages[i].BoundBoxes[j].Properties.AtributesValue.Length > attrPos &&
                            String.Compare(trackOb.Properties.AtributesValue[attrPos], AllImages[i].BoundBoxes[j].Properties.AtributesValue[attrPos], false) == 0)
                    {
                        trackCount++;
                        break;
                    }   
                }
                if (trackCount != 0)
                {                    
                    break;
                }
            }

            if (trackCount == 0)
            {
                checkedListBox1.Items.Remove(trackOb.Properties.AtributesValue[attrPos]);
            }
        }

        /// <summary>
        /// Kliknutie na tlacidlo "predosly snimok"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPreviousFrame(object sender, MouseEventArgs e)
        {
            _mainWin.ChangeFrameManually(false);
        }

        /// <summary>
        /// Kliknutie na tlacidlo "dalsi snimok"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetNextFrame(object sender, MouseEventArgs e)
        {
            _mainWin.ChangeFrameManually(true);
        }

        /// <summary>
        /// Obsluha stlacenia tlacidla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Right) //ctrl + ->
            {
                _mainWin.ChangeFrameManually(true);
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Left)//ctrl + <-
            {
                _mainWin.ChangeFrameManually(false);
            }
        }
    }
}
