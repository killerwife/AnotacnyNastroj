using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Zakladna trieda pre nastavenia umelych transformacii.
    /// </summary>
    [Serializable]
    public abstract class SettingArtifTransfBase
    {
        /// <summary>
        /// Typ transformacie, vid. ETransformType
        /// </summary>
        public ETransformType TransformType { get; private set; }
        
        /// <summary>
        /// Nazov transformacie, pre zobrazenie uzivatelovi
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        protected SettingArtifTransfBase(ETransformType transformType)
        {
            TransformType = transformType;
            Name = transformType.ToString();
        }

        /// <summary>
        /// Kontrola hodnot
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        public abstract bool CheckValue();

        /// <summary>
        /// Vygenerovanie dat podla zadefinovanych nastaveny
        /// </summary>
        /// <param name="image">obrazok z ktoreho s bude generovat</param>
        /// <param name="rectangle">rectangle ktory sa ma vygenerovat</param>
        /// <param name="folderToSave">adresar kde sa maju ulozit data</param>
        /// <param name="size">v pripade noramlizacie nova velkost</param>
        /// <param name="unnormalized">nema but normalizovany true inka false</param>
        /// <param name="scale">v pripade normalizacie novy pomer</param>
        /// <param name="imgFormat">format obrazku</param>
        /// <returns>pocet vygenerovanych dat</returns>
        public abstract int GenerateData(Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, Size size, bool unnormalized, List<int> scale, EImageFormat imgFormat);

        /// <summary>
        /// Vratenie transformovanieho obrazku pre nahlad
        /// </summary>
        /// <param name="image">obrazok z ktoreho s bude generovat</param>
        /// <param name="rectangle">rectangle ktory sa ma vygenerovat</param>
        /// <param name="lastChange">posledna zmena pouzita pre vytvorenie nahladu</param>
        /// <param name="size">size pre drobj</param>
        /// <returns>transformovany obrazok</returns>
        public abstract Image<Bgr, byte> GetTransformImageForPreview(Image<Bgr, byte> image, Rectangle rectangle,  ref int? lastChange);

        /// <summary>
        /// Vypis chybovej hlasky
        /// </summary>
        /// <param name="message">chybova hlaska</param>
        protected void MsgError(string message)
        {
            MessageBox.Show(message, "Error: generovanie umelých dát - voľba " + TransformType, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Doplnenie obrazku pozadim tak aby bol rectangle v obrazku - zvacsenie obrazku pridanim bielej plochy tak aby rectangle bol v obrazku
        /// a vratenie zmeneneho obrazku(s pridanymi plochami ak to bolo potrebne)
        /// </summary>
        /// <param name="image">obrazok ktory sa ma zmenit</param>
        /// <param name="normalRect">rectangle podla ktoreho sa ma obrazok zmenit v pripade ze ma zaporne suradnice, alebo je vacsi ako obrazok</param>
        /// <param name="maxCorner">hranicny bod - pravy dolny roh obrazku, na zistenie ci BB nevycnieva napravo alebo dole z obrazku</param>
        /// <param name="unnormalized">true ak sa nema normalizovat, false ak sa ma</param>
        /// <param name="scale">v pripade ze sa ide normalizovat pouzije sa tento pomer na normalizaciu</param>
        /// <returns>obrazok doplneny pozadim, ak bolo treba</returns>
        protected Image<Bgr, byte> FillImageToRect(Image<Bgr, byte> image, ref Rectangle normalRect, Point maxCorner, bool unnormalized, List<int> scale)
        {
            if (!unnormalized)
                CreateNormalizeRectangle(ref normalRect, scale);

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

        /// <summary>
        /// Zmenit velkost rectanglu na normalizovanu velkost, podla zadaneho pomeru, avsak jeho suradnice
        /// mozu byt aj zaporne alebo vacsie ako obrazok vtedy ak sa nachadza niekde v blizkosti okrajov obrazku
        /// </summary>
        /// <param name="normalRect">rectangel ktory treba znormalizovat</param>
        /// <param name="scale">normalizovany pomer stran aky maju mat rectangle</param>
        private void CreateNormalizeRectangle(ref Rectangle normalRect, List<int> scale)
        {
            //musime najst hodnotu ktora je pre normalizovany pomer a pri danom rozmere rectanglu akoby zakladnou
            //jednotkou ktorou ked vynasobim normalizovany pomer dostanem rozmery rectanglu v tomto pomere pricom sa rectangel zvacsi minimalne
            int vysledok = (int)Math.Ceiling((double)normalRect.Width / scale[0]);
            if (vysledok * scale[1] < normalRect.Height)
                vysledok = (int)Math.Ceiling((double)normalRect.Height / scale[1]);

            normalRect.X += (normalRect.Width - (vysledok * scale[0])) / 2;//lomeno dvoma lebo pridavam zmenu sirky na obe strany(aby sa bb zmenil rovnomerne doprava aj dolava)
            normalRect.Y += (normalRect.Height - (vysledok * scale[1])) / 2;//lomeno dvoma lebo pridavam zmenu vysky na obe strany(aby sa bb zmenil rovnomerne hore aj dole)
            normalRect.Size = new Size(vysledok * scale[0], vysledok * scale[1]);
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>nastavenia v string</returns>
        public override string ToString()
        {
            return string.Format("{0} - pocetnost: 1", Name);
        }
    }
}
