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
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre volbu rotate, move, scale
    /// From, To, Step, Count - kontrola ci je mozne z daneho intrevalu pri danom kroku vygenerovat dany pocet
    /// </summary>
    [Serializable]
    public class SettingArtifTransfRoMoSc : SettingArtifTransf4Params
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        /// <param name="from">od</param>
        /// <param name="to">do</param>
        /// <param name="step">krok</param>
        /// <param name="count">pocetnost</param>
        public SettingArtifTransfRoMoSc(ETransformType transformType, int @from, int to, int step, int count)
            : base(transformType, @from, to, step, count)
        {
        }

        public override bool CheckValue()
        {
            if (!base.CheckValue()) return false;

            //ak sa z daneho rozsahu neda vygenerovat pozadavany pocet dat
            if (Count > Math.Abs(From - To) / Step + 1)
            {
                MsgError("Ľutujem no pri zadanom nastavení rozsahu a kroku sa neda vygenerovať zadaný počet umelých dát.");
                return false;
            }

            //treba tiez skontrolovat ci sa da vygenerovat pozadovany pocet hodnot z daneho rozsahu pri danom kroku
            //za podmienky ze nesmie byt vygenerovana nula(to by pre umely obazok znamenalo ze bude rovnaky ako original)
            //to je situlacia ze je v rozsahu(interval od - do) aj nula a tato moze byt pri danom kroku z tohto rozsahu vygenerovana 
            //cize treba zvacsit rozsah tak aby sa dal vygenerovat pozadovany pocet hodnot aj bez nuly
            if (Count == Math.Abs(From - To) / Step + 1 && From <= 0 && To >= 0 && From % Step == 0)
            {
                MsgError("Ľutujem no pri zadanom nastavení rozsahu <" + From + ";" + To + "> a kroku  " + Step +
                                "\nsa neda vygenerovať " + Count + " umelých dát. Pretože rozsah obsahuje" +
                                "\naj nulu a tá može byť vygenerovaná pri danom kroku čo by znamenalo," +
                                "\nže umelý obrázok bude totožný s originálom, preto je k dispozícii z " +
                                "\ntohto intervalu o jednu hodotu menej. Zmenšite krok alebo zvačšite rozsah. ");
                return false;
            }
            return true;
        }

        /// <summary>
        /// vrati zoznam nahodnych cisel s danym poctom unikatnych hodnotu z rozsahu od-do, pri danom kroku.
        /// </summary>
        /// <returns></returns>
        public override List<int> GetRandomValue()
        {
            //horna hranica intervalu z ktoreho budeme generovat cisla <0; ceiling>
            int ceiling = Math.Abs(From - To);

            //maximalny pocet hodnot ktore vieme z daneho intervalu vygenerovat pri danom kroku
            int maxCount = ceiling / Step + 1;

            //kontrola ci sa da vygenerovat pozadovany pocet hodnot z daneho rozsahu pri danom kroku
            if (Count > maxCount) Count = maxCount;

            //treba tiez skontrolovat ci sa da vygenerovat pozadovany pocet hodnot z daneho rozsahu pri danom kroku
            //za podmienky ze nesmie byt vygenerovana nula(to by pre umely obazok znamenalo ze bude rovnaky ako original)
            //to je situlacia ze je v rozsahu(interval od - do) aj nula a tato moze byt pri danom kroku z tohto rozsahu vygenerovana 
            //cize treba zvacsit rozsah tak aby sa dal vygenerovat pozadovany pocet hodnot aj bez nuly
            if (Count == maxCount && From <= 0 && To >= 0 && From % Step == 0)
            {
                maxCount++;
            }

            var uniqueValue = new List<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            while (uniqueValue.Count != Count)
            {
                int value = Step * rand.Next(maxCount) + From;
                while (uniqueValue.Contains(value) || value == 0)
                {
                    value = Step * rand.Next(maxCount) + From;
                }
                uniqueValue.Add(value);
            }

            return uniqueValue;
        }

        public override int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat)
        {
            int countOk = 0;
            var values = GetRandomValue();
            switch (TransformType)
            {
                case ETransformType.Rotate:                   
                    Rectangle normRect = rectangle;
                    var img = FillImageToRect(image, ref normRect, new Point(image.Width, image.Height), unnormalized, scale);                   
                    for (int i = 0; i < Count; i++)
                    {
                        using (var imgRot = GetRotateImage(img, normRect, size, unnormalized, values[i]))
                        {
                            imgRot.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                            countOk++;
                        }
                    }                                           
                    break;
                case ETransformType.Move:
                    for (int i = 0; i < Count; i++)
                    {
                        using (var imgMov = GetMoveImage(image, rectangle, size, unnormalized, scale, values[i]))
                        {
                            imgMov.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                            countOk++;
                        }

                    }
                    break;
                case ETransformType.Scale:
                    for (int i = 0; i < Count; i++)
                    {
                        using(var imgScl = GetScaleImage(image, rectangle, size, unnormalized, scale, values[i]))
                        {
                            imgScl.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                            countOk++;
                        }

                    }
                    break;
            }
            return countOk;
        }

        private Image<Bgr, byte> GetRotateImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, int angle)
        {
            var centerPoint = new PointF(rectangle.X + rectangle.Width * 0.5f, rectangle.Y + rectangle.Height * 0.5f);

            return unnormalized
                       ? image.Rotate(angle, centerPoint, INTER.CV_INTER_CUBIC, new Bgr(Color.White), true)
                            .Copy(rectangle)
                       : new Image<Bgr, byte>(image.Rotate(angle, centerPoint, INTER.CV_INTER_CUBIC,
                                                         new Bgr(Color.White), true).
                                                  Copy(rectangle).ToBitmap(size.Width, size.Height));
        }

        private Image<Bgr, byte> GetMoveImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale, int vector)
        {
            var rnd = new Random((int) DateTime.Now.Ticks);
            //musim nahodne urcit posun v smere x a y tak aby som dostal zadany vektor.                    
            var deltaX = rnd.Next(-vector, vector);
            var deltaY = (int)Math.Sqrt(Math.Pow(vector, 2) - Math.Pow(deltaX, 2));

            rectangle.X += deltaX;
            rectangle.Y += rnd.NextDouble() < 0.5 ? -deltaY : deltaY;

            return unnormalized
                       ? FillImageToRect(image, ref rectangle, new Point(image.Width, image.Height), true, scale)
                             .Copy(rectangle)
                       : new Image<Bgr, byte>(
                             FillImageToRect(image, ref rectangle, new Point(image.Width, image.Height), false, scale)
                                 .Copy(rectangle).ToBitmap(size.Width, size.Height));
        }

        private Image<Bgr, byte> GetScaleImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale, int deltaScale)
        {
            var rect = rectangle;
            double sc = deltaScale/100.0;
            int deltaW = (int)Math.Round((rect.Width - (sc * rect.Width))/sc);
            int deltaH = (int)Math.Round((rect.Height - (sc * rect.Height))/sc);
            rect.Width += deltaW;
            rect.Height += deltaH;

            rect.X -= deltaW / 2;
            rect.Y -= deltaH / 2;

            if (rect.Width < 1) rect.Width = 1;
            if (rect.Height < 1) rect.Height = 1;

            return unnormalized
                       ? new Image<Bgr, byte>(
                             FillImageToRect(image, ref rect, new Point(image.Width, image.Height), true, scale)
                                 .Copy(rect).ToBitmap(rectangle.Width, rectangle.Height))
                       : new Image<Bgr, byte>(
                             FillImageToRect(image, ref rect, new Point(image.Width, image.Height), false, scale)
                                 .Copy(rect).ToBitmap(size.Width, size.Height));
        }

        public override Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle, ref int? lastChange)
        {
            Random rand = new Random();
            if(lastChange == null)
                lastChange = GetRandomValue()[rand.Next(Count)];
            switch (TransformType)
            {
                case ETransformType.Rotate:
                    using (var img = FillImageToRect(image, ref rectangle, 
                                    new Point(image.Width, image.Height),true, null))
                    {
                        return GetRotateImage(img, rectangle, new Size(), true, lastChange.Value);
                    }                   
                case ETransformType.Move:
                    return GetMoveImage(image, rectangle, new Size(), true, null, lastChange.Value);
                case ETransformType.Scale:
                    return GetScaleImage(image, rectangle, new Size(), true, null, lastChange.Value);
            }
            lastChange = null;
            return null;
        }
    }
}
