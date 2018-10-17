using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre volbu Addaptive and Impulse noise.
    /// From, To, Step, Count - kontrola hodnot. Tu sa pocet vygenerovanych dat nekontroluje podla kroku a rozsahu, lebo
    /// vytvaranie tohto sumu je vzdy nahodne cize pre jednu hodnotu z intervalu je mozne vygenerovat nekonecne mnozstvo obrazkov
    /// ktore budu zakazdym ine.
    /// </summary>
    [Serializable]
    public class SettingArtifTransfNoise : SettingArtifTransf4Params
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        /// <param name="from">od</param>
        /// <param name="to">do</param>
        /// <param name="step">krok</param>
        /// <param name="count">pocetnost</param>
        public SettingArtifTransfNoise(ETransformType transformType, int @from, int to, int step, int count)
            : base(transformType, @from, to, step, count)
        {
        }

        public override bool CheckValue()
        {
            //ak je zly rozsah od - do
            if (From > To || (From == 0 && To == 0) || From < 1 || To > 100)
            {
                MsgError("Zle zadaný rozsah (from, to). Je predstavuje hodnoty v percentach a musi byt z intervalu <1-100>.");
                return false;
            }

            //ak krok alebo pocetnost <= 0
            if (Step < 0 || Count <= 0)
            {
                MsgError("Zle zadané hodnoty kroku alebo početnosti.");
                return false;
            }

            if (Step == 0 && From == 0)
            {
                MsgError("Pri zadanom kroku 0 nemoze byt from 0.");
                return false;
            }
            return true;
        }

        public override int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat)
        {
            int countOk = 0;
            var values = GetRandomValue();
            switch (TransformType)
            {
                case ETransformType.AdditiveNoise:
                    for (int k = 0; k < Count; k++)
                    {
                        using (var pomImage = GetAdditiveNoiseImage(image, rectangle, size, unnormalized, scale, values[k]))
                        {
                            pomImage.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                            countOk++;
                        }
                    }
                break;

                case  ETransformType.ImpulseNoise:
                    for (int k = 0; k < Count; k++)
                    {
                        using (var pomImage = GetImpulseNoiseImage(image, rectangle, size, unnormalized, scale, values[k]))
                        {
                            pomImage.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff.") + imgFormat);
                            countOk++;
                        }
                    }
                break;
            }
            return countOk;
        }

        private Image<Bgr, byte> GetAdditiveNoiseImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale, int value)
        {
            _rand = new Random((int)DateTime.Now.Ticks);
            _standDev = (int)Math.Round(value * 2.55);
            //nanormalizujeme velkost rectangle na normalizovany rectangle ak sa ma normalizovat
            var pomImage = unnormalized
                                      ? image.Copy(rectangle)
                                      : new Image<Bgr, byte>(FillImageToRect(image, ref rectangle,
                                                            new Point(image.Bitmap.Width, image.Bitmap.Height), false,scale)
                                                .Copy(rectangle).ToBitmap(size.Width, size.Height));
            
            for (int i = 0; i < pomImage.Height; i++)
            {
                for (int j = 0; j < pomImage.Width; j++)
                {
                    var r = GetGaussRand();
                    pomImage[i, j] = new Bgr(pomImage[i, j].Blue + r/* DajHodnotu()*/, pomImage[i, j].Green + r/*DajHodnotu()*/, pomImage[i, j].Red + r/*DajHodnotu()*/);
                }
            }
            return pomImage;
        }

        private Image<Bgr, byte> GetImpulseNoiseImage(Image<Bgr, byte> image, Rectangle rectangle, Size size, bool unnormalized, List<int> scale, int value)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var rndImp = new Random((int)DateTime.Now.Ticks);
            var pomImage = unnormalized
                               ? image.Copy(rectangle)
                               : new Image<Bgr, byte>(FillImageToRect(image, ref rectangle,
                                                     new Point(image.Bitmap.Width, image.Bitmap.Height), false,scale)
                                         .Copy(rectangle).ToBitmap(size.Width, size.Height));

            for (int i = 0; i < pomImage.Height; i++)
            {
                for (int j = 0; j < pomImage.Width; j++)
                {
                    if (rndImp.NextDouble() < value/100.0)
                    {
                        int num = rnd.Next(2);//value + 1);
                        if (num == 0)
                            pomImage[i, j] = new Bgr(Color.White);
                        else if (num == 1)
                            pomImage[i, j] = new Bgr(Color.Black);
                    }                    
                }
            }
            return pomImage;
        }

        public override Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle, ref int? lastChange)
        {
            var rand = new Random();
            if (lastChange == null)
                lastChange = GetRandomValue()[rand.Next(Count)];

            switch (TransformType)
            {
                    case ETransformType.AdditiveNoise:
                    return GetAdditiveNoiseImage(image, rectangle, new Size(), true, null, lastChange.Value);
                    case ETransformType.ImpulseNoise:
                    return GetImpulseNoiseImage(image, rectangle, new Size(), true, null, lastChange.Value);
            }

            lastChange = null;
            return null;
        }

        private Random _rand;
        int _standDev; //standardna odchylka
        /// <summary>
        /// Normalne rozdelenie dava hodnotu od (-nekonecna, + nekonecno) treba to upravit na nejaky rozumny interval
        /// pr. (μ-3*σ, μ+3*σ)
        /// Stredna hodnota je astavena na 0
        /// </summary>
        /// <returns></returns>
        private double GetGaussRand()
        {
            const int mean = 0; //stredna hodnota je nula
            double pom = Math.Sqrt(-2.0 * Math.Log(_rand.NextDouble())) * Math.Cos(2 * Math.PI * _rand.NextDouble());
            while ((mean + _standDev * pom) < (mean - (3 * _standDev)) || (mean + _standDev * pom) > (mean + (3 * _standDev)))
            {
                pom = Math.Sqrt(-2.0 * Math.Log(_rand.NextDouble())) * Math.Cos(2 * Math.PI * _rand.NextDouble());
            }
            return (mean + _standDev * pom);
        }

        public override List<int> GetRandomValue()
        {
            //horna hranica intervalu z ktoreho budeme generovat cisla <0; ceiling>
            int ceiling = Math.Abs(From - To);

            //maximalny pocet hodnot ktore vieme z daneho intervalu vygenerovat pri danom kroku
            int maxCount = Step == 0 ? 1 : ceiling / Step + 1;

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

            var values = new List<int>();
            var rand = new Random((int)DateTime.Now.Ticks);
            while (values.Count != Count)
            {
                values.Add(Step * rand.Next(maxCount) + From);
            }

            return values;
        }
    }
}
