using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre volbu sharpening.
    /// Ziadna kontrola, ak volba zaskrtnuta generuje sa zaostrenie, vzdy len jeden obrazok.
    /// </summary>
    [Serializable]
    public class SettingArtifTransfSharpen : SettingArtifTransfBase
    {
        [NonSerialized]
        private ConvolutionKernelF _kernel = new ConvolutionKernelF(new float[,] { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } }, new Point(1, 1));
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        public SettingArtifTransfSharpen(ETransformType transformType)
            : base(transformType)
        {
        }

        /// <summary>
        /// Kontrola hodnot
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public override bool CheckValue()
        {
            return true;
        }

        public override int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat)
        {
            int countOk = 0;
            using (var pomImage = GetSharpeningImage(image,rectangle,size,unnormalized,scale))
            {
                pomImage.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                countOk++;
            }
            return countOk;
        }

        private Image<Bgr, byte> GetSharpeningImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale)
        {
            var pomImage = unnormalized
                               ? image.Copy(rectangle)
                               : new Image<Bgr, byte>(FillImageToRect(image, ref rectangle,
                                                     new Point(image.Bitmap.Width, image.Bitmap.Height), false, scale)
                                                     .Copy(rectangle).ToBitmap(size.Width, size.Height));
            
            using (var imgConv = pomImage.Convert<Gray, byte>().Convolution(_kernel))
            {
                for (int i = 0; i < pomImage.Height; i++)
                {
                    for (int j = 0; j < pomImage.Width; j++)
                    {
                        if (imgConv[i, j].Intensity == 0) continue;
                        pomImage[i, j] = new Bgr(pomImage[i, j].Blue + imgConv[i, j].Intensity, pomImage[i, j].Green + imgConv[i, j].Intensity, pomImage[i, j].Red + imgConv[i, j].Intensity);
                    }
                }
            }
            return pomImage;
        }

        public override Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle, ref int? lastChange)
        {
            lastChange = null;
            return GetSharpeningImage(image, rectangle, new Size(), true, null);
        }
    }
}
