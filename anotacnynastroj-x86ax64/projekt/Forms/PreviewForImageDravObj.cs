using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.Figure;
using Projekt.SettingOfTransformation;

namespace Projekt.Forms
{
    /// <summary>
    /// Tento form sluzi na zobrazenie nahladu uzivatelovi, tento nahlad presdtavuje obrazok na ktorom
    /// su uzivatelovi prezentovane nastavenia ake zvolil pre generovanie dat z draw objectov. Moze si vizualne
    /// skontrolovat nastavenia.
    /// </summary>
    public partial class PreviewForImageDravObj : Form
    {
        private readonly List<BaseFigure> _images;
        private readonly int _typeDrObj; //0 ak generujeme preBB, 1 ak pre PL

        private readonly SettingArtifTransfBase _setting; //nastavenia pre upravu

        private Rectangle _selectRectangle; //rectangle ktory sa zobrazuje
        private BaseFigure _selectImage; //obrazok na ktorom je vybrany rectangle
        private int? _lastChange;

        private int _imageNum, _drawObjNum; //pre nastavovanie dalsich obrazkov. Pamatanie posledneho zobrazeneho objektu. Inicializacia -1

        private Size _sizePolyline;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="images">obrazky z ktorych sa maju vytvarat nahlady</param>
        /// <param name="typeDrObj">BB alebo PA (BB - 0,PL - 1)</param>
        /// <param name="setting">nastavenie podla ktorych sa ma transformovat obrazok pre nahlad</param>
        /// <param name="size">velkost pre nahlady polyline</param>
        public PreviewForImageDravObj(List<BaseFigure> images, int typeDrObj, SettingArtifTransfBase setting, Size size)
        {
            _sizePolyline = size;
            _selectRectangle = new Rectangle();
            _images = images;
            _typeDrObj = typeDrObj;
            _setting = setting;
            _imageNum = _drawObjNum = -1;
            _lastChange = null;
            _selectImage = null;
            InitializeComponent();
            SetImagesToGrid();
        }

        /// <summary>
        /// Nastavenie obrazkov na grid
        /// </summary>
        private void SetImagesToGrid()
        {
            try
            {
                if (!SelectImageAndRect())
                {
                    imgBoxOrigin.Image = imgBoxAftChange.Image = null;
                    return;
                }

                using (var img = _selectImage.GetImage())
                {
                    var rect = _selectRectangle;
                    if (_typeDrObj == 1)
                    {
                        imgBoxOrigin.Image = FillImageToRect(img, ref rect, new Point(img.Width, img.Height)).Copy(rect);
                        using (var transfImg = _setting.GetTransformImageForPreview(FillImageToRect(img, ref rect, new Point(img.Width, img.Height)), rect, ref _lastChange))
                            imgBoxAftChange.Image = transfImg;
                    }
                    else
                    {
                        imgBoxOrigin.Image = img.Copy(rect);
                        using (var transfImg = _setting.GetTransformImageForPreview(img, rect, ref _lastChange))
                            imgBoxAftChange.Image = transfImg;  
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - PreviewForImageDravObj - SetImagesToGrid()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SelectImageAndRect()
        {
            switch (_typeDrObj)
            {
                case 0:
                    _selectImage = GetBBFromImg(out _selectRectangle);
                    break;
                case 1:
                    _selectImage = GetPLFromImg(out _selectRectangle);
                    break;
            }

            if (_selectImage == null)
            {
                MessageBox.Show("Nenasli sa dalsie vhodne data pre vytvorenie nahladu. \nPri dalsom kliknuti sa bude sa prehladavat odznovu.", "Warnning - PreviewForImageDravObj - SelectImageAndRect()", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Nastavenie DrawObjectu ktory sa bude zobrazovat ako nahlad
        /// </summary>
        private void CheckNumDrawObjForPrew()
        {
            if (_imageNum == -1 || _imageNum >= _images.Count())
            {
                _imageNum = _drawObjNum = 0;
            }
        }

        private BaseFigure GetBBFromImg(out Rectangle rectangle)
        {
            //vyber BB,PABB
            rectangle = new Rectangle();
            try
            {  
                CheckNumDrawObjForPrew();
                for (int i = _imageNum; i < _images.Count; i++)
                {
                    if (_drawObjNum >= _images[i].BoundBoxes.Count + _images[i].Paintings.Count)
                    {
                        _drawObjNum = 0;
                        _imageNum++;
                        continue;
                    }

                    rectangle = _drawObjNum < _images[i].BoundBoxes.Count ? _images[i].BoundBoxes[_drawObjNum].GetRectangle() : _images[i].Paintings[_drawObjNum - _images[i].BoundBoxes.Count].BoundingBox.GetRectangle();
                    _drawObjNum++;
                    if (rectangle.Size == new Size(0, 0))
                        continue;
                    return _images[i];
                }
                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - PreviewForImageDravObj - GetBBFromImg()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }
        }

        private BaseFigure GetPLFromImg(out Rectangle rectangle)
        {
            //vyber PL
            rectangle = new Rectangle();
            CheckNumDrawObjForPrew();

            for (int i = _imageNum; i < _images.Count; i++)
            {
                if (_drawObjNum >= _images[i].Polylines.Count)
                {
                    _drawObjNum = 0;
                    _imageNum++;
                    continue;
                }
                var point = _images[i].Polylines[_drawObjNum].Points[0];
                rectangle = new Rectangle(point.X - _sizePolyline.Width / 2, point.Y - _sizePolyline.Height / 2, _sizePolyline.Width, _sizePolyline.Height);
                _drawObjNum++;
                return _images[i];
            }
            return null;
        }

        private void BtnAnotherImageClick(object sender, EventArgs e)
        {
            //todo: pri move je problem lebo sa generuje aj smer !!!

            SetImagesToGrid();
        }

        private void BtnAnotherSettingClick(object sender, EventArgs e)
        {
            try
            {
                if (_selectImage == null) return;
                _lastChange = null;
                using (var img = _selectImage.GetImage())
                {
                    imgBoxAftChange.Image = _setting.GetTransformImageForPreview(img, _selectRectangle, ref _lastChange); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - BtnAnotherImageClick()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Doplnenie obrazku pozadim tak aby bol rectangle v obrazku - zvacsenie obrazku pridanim bielej plochy tak aby rectangle bol v obrazku
        /// a vratenie zmeneneho obrazku(s pridanymi plochami ak to bolo potrebne)
        /// </summary>
        /// <param name="image">obrazok ktory sa ma zmenit</param>
        /// <param name="normalRect">rectangle podla ktoreho sa ma obrazok zmenit v pripade ze ma zaporne suradnice, alebo je vacsi ako obrazok</param>
        /// <param name="maxCorner">hranicny bod - pravy dolny roh obrazku, na zistenie ci BB nevycnieva napravo alebo dole z obrazku</param>
        /// <returns>obrazok doplneny pozadim, ak bolo treba</returns>
        private Image<Bgr, byte> FillImageToRect(Image<Bgr, byte> image, ref Rectangle normalRect, Point maxCorner)
        {
            if (normalRect.X < 0) //pridame nalavo od obrazku bielu plochu tak siroku aby sa x zmenilo na 0
            {
                image = new Image<Bgr, byte>(Math.Abs(normalRect.X), maxCorner.Y, new Bgr(255, 255, 255)).ConcateHorizontal(image);
                maxCorner.X += Math.Abs(normalRect.X);
                normalRect.X = 0;
            }
            if (normalRect.Y < 0) //pridame nad obrazok bielu plochu tak siroku aby sa y zmenilo na 0
            {
                image = new Image<Bgr, byte>(maxCorner.X, Math.Abs(normalRect.Y), new Bgr(255, 255, 255)).ConcateVertical(image);
                maxCorner.Y += Math.Abs(normalRect.Y);
                normalRect.Y = 0;
            }
            if (normalRect.X + normalRect.Width > maxCorner.X)//pridame napravo od obrazku bielu plochu tak siroku aby x bolo v obrazku nie mimo neho     
            {
                int deltaX = normalRect.X + normalRect.Width - maxCorner.X;
                using (var img = new Image<Bgr, byte>(deltaX, maxCorner.Y, new Bgr(255, 255, 255)))
                    image = image.ConcateHorizontal(img);
                maxCorner.X += deltaX;
            }
            if (normalRect.Y + normalRect.Height > maxCorner.Y)//pridame pod obrazok bielu plochu tak siroku aby y bolo v obrazku nie mimo neho    
            {
                int deltaY = normalRect.Y + normalRect.Height - maxCorner.Y;
                using (var img = new Image<Bgr, byte>(maxCorner.X, deltaY, new Bgr(255, 255, 255)))
                    image = image.ConcateVertical(img);
                maxCorner.Y += deltaY;
            }

            //ak nepresiel ani jeden if to znamena ze normalizovany rectangle nezasahuje mimo obrazku cize sa nic nepridalo k obrazku
            return image;
        }
    }
}
