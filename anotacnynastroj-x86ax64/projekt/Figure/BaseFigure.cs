using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.DrawObjects;

namespace Projekt.Figure
{
    /// <summary>
    /// Spoločný predok pre obrázok a frame z videa.
    /// </summary>
    public abstract class BaseFigure 
    {
        /// <summary>
        /// Cesta k danému obrázku/frame videa.
        /// Relativna cesta k danému figure (od xml suboru). Od zlozky projektu(vratane nej).
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Všeobecné atribúty obrázka/frame.
        /// Kazda polozka zoznamu obsahuje dvojicu: nazov a hodnota atributu
        /// </summary>
        public List<string[]> Properties { get; set; }

        /// <summary>
        /// Boundingboxy na danom obrazku/frame.
        /// </summary>
        public List<BoundingBox> BoundBoxes { get; set; }

        /// <summary>
        /// Paintingy na obrazku/frame.
        /// </summary>
        public List<Painting> Paintings { get; set; }

        /// <summary>
        /// Polylines na obrazku/frame.
        /// </summary>
        public List<Polyline> Polylines { get; set; }

        /// <summary>
        /// Zapamatanie zoom na danom obrazku.
        /// </summary>
        public double Zooom { get; set; }
 
        /// <summary>
        /// Zapamatanie pozicie verticalneho scrollu.
        /// </summary>
        public int VerticalScroll { get; set; }
        
        /// <summary>
        /// Zapamatanie pozicie horizontalneho scrollu.
        /// </summary>
        public int HorizontScroll { get; set; }
        public int HScroll { get; set; }
        public int VScroll { get; set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="source">Relativna cesta k danému figure (od xml suboru). Od zlozky projektu(vratane nej)</param>
        protected BaseFigure(string source)
        {
            Source = source;
            BoundBoxes = new List<BoundingBox>();
            Paintings = new List<Painting>();
            Polylines = new List<Polyline>();
            Zooom = 1;
            VerticalScroll = HorizontScroll = 0;
            HScroll = VScroll = 0;
        }

        /// <summary>
        /// Metoda pre vygenerovanie XML súboru.
        /// </summary>
        /// <param name="xmlWriter">xmlXriter pouzity na vutvaranie xml suboru</param>
        public virtual void ExportXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("attributes");
            if (Properties != null)
                for (int i = 0; i < Properties.Count; i++)
                {
                    xmlWriter.WriteStartElement("attribute");
                    xmlWriter.WriteAttributeString("name", Properties[i][0]);
                    xmlWriter.WriteString(Properties[i][1]);
                    xmlWriter.WriteEndElement();
                }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("boundingboxes");
            foreach (BoundingBox bb in BoundBoxes)
            {
                bb.DrawObject2Xml(xmlWriter);
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("paintings");
            foreach (Painting paint in Paintings)
            {
                paint.DrawObject2Xml(xmlWriter);
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("polylines");
            foreach (Polyline polyline in Polylines)
            {
                polyline.DrawObject2Xml(xmlWriter);
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        public virtual void ExportXML2(XmlWriter xmlWriter)
        {
            //xmlWriter.WriteStartElement("attributes");
            //if (Properties != null)
            //    for (int i = 0; i < Properties.Count; i++)
            //    {
            //        xmlWriter.WriteStartElement("attribute");
            //        xmlWriter.WriteAttributeString("name", Properties[i][0]);
            //        xmlWriter.WriteString(Properties[i][1]);
            //        xmlWriter.WriteEndElement();
            //    }
            //xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("boundingboxes");
            if (BoundBoxes.Count == 0)
            {
                xmlWriter.WriteString("");
            }
            foreach (BoundingBox bb in BoundBoxes)
            {
                bb.DrawObject2Xml2(xmlWriter);
            }
            xmlWriter.WriteEndElement();       

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Vratenie obrazku/snimky videa
        /// </summary>
        /// <returns>vytvoreny obrazok/snimka</returns>
        public abstract Image<Bgr, Byte> GetImage();

        /// <summary>
        /// Pridanie paintingu na dany obrazok
        /// </summary>
        /// <param name="start">prvy bod paintingu</param>
        /// <param name="width">sirka daneho obrazku</param>
        /// <param name="height">vyska daneho obrazku</param>
        public void AddPainting(Point start, int width, int height)
        {
            var painting = new Painting(Paintings.Count);

            var actuall = new Point((int)Math.Floor(start.X / Zooom + HorizontScroll),
                                      (int)Math.Floor(start.Y / Zooom + VerticalScroll));
            if (actuall.X < 0) actuall.X = 0;
            if (actuall.Y < 0) actuall.Y = 0;
            if (actuall.X > width) actuall.X = width;
            if (actuall.Y > height) actuall.Y = height;

            painting.BoundingBox.PointA = actuall;
            painting.BoundingBox.PointB = actuall;
            painting.AddPoints(actuall);
            Paintings.Add(painting);
        }

        /// <summary>
        /// Pridanie BoundingBoxu na dany obrazok.
        /// Ziskanie prveho bodu, ktory bude tvorit boundingbox.
        /// </summary>
        /// <param name="point">pociatocny bod(prvy bod BB)</param>
        public void AddBoundingBox(Point point)
        {
            var boundbox = new BoundingBox(BoundBoxes.Count);
            boundbox.PointA = new Point((int)(point.X / Zooom + HorizontScroll), (int)(point.Y / Zooom + VerticalScroll));
            BoundBoxes.Add(boundbox);
        }

        public void AddBoundingBox(BoundingBox box)
        {
            BoundBoxes.Add(box);
        }

        public void DeleteAllBbs()
        {
            BoundBoxes.Clear();
        }

        /// <summary>
        /// Pridanie Polyline na dany obrazok.
        /// /// </summary>
        /// <param name="start">prvy bod paintingu</param>
        /// <param name="width">sirka daneho obrazku</param>
        /// <param name="height">vyska daneho obrazku</param>
        public void AddPolyLine(Point start, int width, int height)
        {
            var polyline = new Polyline(Polylines.Count);
            var actuall = new Point((int)Math.Floor(start.X / Zooom + HorizontScroll), 
                                      (int)Math.Floor(start.Y / Zooom + VerticalScroll));
            if (actuall.X < 0) actuall.X = 0;
            if (actuall.Y < 0) actuall.Y = 0;
            if (actuall.X > width) actuall.X = width;
            if (actuall.Y > height) actuall.Y = height;

            polyline.Points.Add(actuall);
            Polylines.Add(polyline);
        }

    }
}
