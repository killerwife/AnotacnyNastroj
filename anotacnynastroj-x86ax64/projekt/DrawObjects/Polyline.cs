using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Projekt.DrawObjects
{
    /// <summary>
    /// Trieda reprezentujuca polyline, potomok triedy DrawObject.
    /// </summary>
    public class Polyline : DrawObject
    {
        /// <summary>
        /// Body tvoriace polyline.
        /// </summary>
        public List<Point> Points { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">id polyline</param>
        public Polyline(int id)
        {
            ID = id;
            Name = "Polyline " + ID;
            Points = new List<Point>();
            Properties = new ObjectProperties();
        }

        /// <summary>
        /// Vytvorenie časti XML súboru, ktorá bude reprezentovať polyline. 
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pouzity pre vytvorenie dokumentu</param>
        public override void DrawObject2Xml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("polyline");
                xmlWriter.WriteStartElement("points");
                    foreach (Point point in Points)
                    {
                        xmlWriter.WriteString(point.X + "," + point.Y + ";");
                    }
                xmlWriter.WriteEndElement();

                if (Properties.Class != null)
                {
                    xmlWriter.WriteStartElement("class");
                    xmlWriter.WriteAttributeString("name", Properties.Class);

                    for (int i = 0; i < Properties.AtributesName.Length; i++)//zápis všetkých property paintingu
                    {
                        if (Properties.AtributesValue[i] == "") continue;
                        xmlWriter.WriteStartElement("attribute");
                        xmlWriter.WriteAttributeString("name", Properties.AtributesName[i]);
                        xmlWriter.WriteString(Properties.AtributesValue[i]);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Nacitanie Polylines zo zadaneho elementu xml
        /// </summary>
        /// <param name="xFigure">element xml</param>
        /// <returns>zoznam polylines daneho xml elementu</returns>
        public override List<DrawObject> DrawObjectFromXml(XElement xFigure)
        {
            var imagePLs = new List<DrawObject>();
            int i = 0;
            foreach (XElement xPL in xFigure.Elements("polylines").Descendants("polyline"))
            {
                var pl = new Polyline(i++);
                var xPoints = xPL.Element("points");
                if (xPoints != null)
                {
                    string[] points = xPoints.Value.Split(';');
                    var pnts = new List<Point>();
                    foreach (var point in points)
                    {
                        string[] pnt = point.Split(',');
                        if (pnt.Count() == 2) pnts.Add(new Point(Convert.ToInt32(pnt[0]), Convert.ToInt32(pnt[1])));
                    }
                    pl.Points = pnts;
                }

                //este potrebujeme nacitat properties BB, ak nejake ma
                var op = LoadPropsFromXml(xPL);
                if (op != null) pl.Properties = op;
                imagePLs.Add(pl);
            }
            return imagePLs;
        }
    }
}
