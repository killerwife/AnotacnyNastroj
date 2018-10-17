using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre volbu reflection.
    /// Horizontal, vertical refleciton, kontrola ci vyplnene aspon jedno
    /// </summary>
    [Serializable]
    public class SettingArtifTransfRefl : SettingArtifTransfBase
    {
        /// <summary>
        /// true ak ma byt horizontalne zrkadlenie
        /// </summary>
        public bool Horizontal { get; private set; }
 
        /// <summary>
        /// true ak ma byt vertikalne zrkadlenie
        /// </summary>
        public bool Vertical { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        /// <param name="horizontal">true ak ma byt horizontalne zrkadlenie</param>
        /// <param name="vertical">true ak ma byt vertikalne zrkadlenie</param>
        public SettingArtifTransfRefl(ETransformType transformType, bool horizontal, bool vertical)
            : base(transformType)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public override bool CheckValue()
        {
            return Horizontal || Vertical;
        }

        public override int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat)
        {
            int countOk = 0;
            //nanormalizujeme velkost BB na normalizovany rectangle ak sa ma normalizovat
            using (var pomImage = unnormalized 
                                    ? image.Copy(rectangle) 
                                    : new Image<Bgr, byte>(
                                            FillImageToRect(image, ref rectangle, new Point(image.Bitmap.Width, image.Bitmap.Height), false, scale)
                                            .Copy(rectangle).ToBitmap(size.Width, size.Height)))
            {
                using(var img = Rotate(pomImage))
                    img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                countOk++;
            }
            return countOk;
        }

        public override Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle, ref int? lastChange)
        {
            lastChange = null;
            using (var img = image.Copy(rectangle))
                return Rotate(img);
        }

        private Image<Bgr, Byte> Rotate(Image<Bgr, Byte> image)
        {
            if (Horizontal && Vertical) return image.Flip(FLIP.HORIZONTAL).Flip(FLIP.VERTICAL);
            if (Horizontal)
                return image.Flip(FLIP.HORIZONTAL);
            if (Vertical)
                return image.Flip(FLIP.VERTICAL);
            return image;
        }

        public override string ToString()
        {
            return string.Format("{0} - Horizontalne: {1}, Vertikalne: {2}",Name, Horizontal, Vertical);
        }
    }
}
