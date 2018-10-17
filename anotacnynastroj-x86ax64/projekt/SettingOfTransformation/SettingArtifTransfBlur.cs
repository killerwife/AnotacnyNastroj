using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre volbu blurring.
    /// From, To, Step, Count -kontrola len neparne hodnoty z daneho intrevalu pri danom kroku a danom pocte
    /// </summary>
    [Serializable]
    public class SettingArtifTransfBlur : SettingArtifTransf4Params
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        /// <param name="from">od</param>
        /// <param name="to">do</param>
        /// <param name="step">krok</param>
        /// <param name="count">pocetnost</param>
        public SettingArtifTransfBlur(ETransformType transformType, int @from, int to, int step, int count)
            : base(transformType, @from, to, step, count)
        {
        }

        /// <summary>
        /// kontrola hodnot
        /// </summary>
        /// <returns></returns>
        public override bool CheckValue()
        {
            if (!base.CheckValue()) return false;

            //ak sa z daneho rozsahu neda vygenerovat pozadavany pocet dat
            var nums = Enumerable.Range(From, Math.Abs(From - To)+1).Where(x => x % 2 != 0).Select(x => x);

            if (Count > (nums.Count() - 1) / Step + 1)
            {
                MsgError("Ľutujem no pri zadanom nastavení rozsahu a kroku sa neda vygenerovať zadaný počet umelých dát.\nVyberaju sa z intrvalu len neparne cisla lebo velkost masky moze byt len neparna hodnota.");
                return false;
            }

            return true;
        }

        public override int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat)
        {
            int countOk = 0;
            var kernelSizes = GetRandomValue();
            for (int i = 0; i < Count; i++)
            {
                using (var pomImage = GetBlurringImage(image,rectangle,size,unnormalized,scale,kernelSizes[i]))
                {
                    pomImage.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                    countOk++;
                }
                    
            }
            return countOk;
        }

        private Image<Bgr, byte> GetBlurringImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale, int kernelSize)
        {
            using (var pomImage = unnormalized
                                      ? image.Copy(rectangle)
                                      : new Image<Bgr, byte>(FillImageToRect(image, ref rectangle,
                                                            new Point(image.Bitmap.Width, image.Bitmap.Height), false, scale)
                                                .Copy(rectangle).ToBitmap(size.Width, size.Height)))
            {
                return pomImage.SmoothGaussian(kernelSize, kernelSize, 0, 0);
            }
        }

        public override Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle, ref int? lastChange)
        {
            var rand = new Random();
            if (lastChange == null)
                lastChange = GetRandomValue()[rand.Next(Count)];

            return GetBlurringImage(image, rectangle, new Size(), true, null, lastChange.Value);
        }

        public override List<int> GetRandomValue()
        {
            var nums = Enumerable.Range(From, Math.Abs(From - To)+1).Where(x => x % 2 != 0).Select(x => x).ToList();
            int numsCount = (nums.Count()-1)/Step+1;

            var uniquevalues = new List<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            while (uniquevalues.Count < Count)
            {
                int value = nums[Step * rand.Next(numsCount)];
                while (uniquevalues.Contains(value) || value == 0)
                {
                    value = nums[Step * rand.Next(numsCount)];
                }
                uniquevalues.Add(value);
            }
            return uniquevalues;
        }
    }
}
