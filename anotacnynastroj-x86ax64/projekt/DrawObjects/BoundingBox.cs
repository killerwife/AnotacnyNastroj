using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace Projekt.DrawObjects
{
    /// <summary>
    /// Trieda reprezentujúca BoundingBox, je potomkom triedy DrawObject.
    /// </summary>
    public class BoundingBox : DrawObject, ICloneable
    {
        /// <summary>
        /// Ľavý horný roh boundingboxu.
        /// </summary>
        public Point PointA { get; set; }

        private Point _pointb;
        /// <summary>
        /// Pravý dolný roh boundingboxu.
        /// </summary>
        public Point PointB {
            get { return _pointb; }
            set 
            { 
                _pointb = value; 
                if(PointA.X > PointB.X)
                {
                    int tmp = PointA.X;
                    PointA = new Point(_pointb.X, PointA.Y);
                    _pointb = new Point(tmp, _pointb.Y);
                }
                if (PointA.Y > PointB.Y)
                {
                    int tmp = PointA.Y;
                    PointA = new Point(PointA.X, _pointb.Y);
                    _pointb = new Point(_pointb.X, tmp);
                }
            }
        }

        /// <summary>
        /// Konštruktor
        /// </summary>
        /// <param name="id">ID boundingBoxu.</param>
        public BoundingBox(int id)
        {
            ID = id;
            //Name = "BoundingBox " + ID;
            Properties = new ObjectProperties();
        }

        /// <summary>
        /// Konštruktor
        /// </summary>
        /// <param name="id">ID boundingBoxu.</param>
        /// <param name="rectangle">Rectangle reprezentujuci dany boundingbox</param>
        /// <param name="class">Trieda boundingboxu, do ktorej ma byt zaradeny</param>
        public BoundingBox(int id, Rectangle rectangle, string @class)
        {
            ID = id;
            PointA = rectangle.Location;
            PointB = new Point(rectangle.Right, rectangle.Bottom);
            Properties = new ObjectProperties();
            Properties.Class = @class;
        }

        /// <summary>
        /// Vypočítanie veľkosti strán boundingboxu.
        /// </summary>
        public Size Size 
        { 
            get
            {
                int a = Math.Abs(PointA.X - PointB.X);
                int b = Math.Abs(PointA.Y - PointB.Y);
                return new Size(a,b);
            }
        }

        /// <summary>
        /// Vráti pravouholník reprezentujúci boundingbox.
        /// </summary>
        /// <returns>Rectangle reprezentujúci boundingbox.</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(PointA, Size);
        }

        /// <summary>
        /// Vytvorenie časti XML súboru, ktorá bude reprezentovať boundingbox. 
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pomocou ktoreho sa vytvara subor</param>
        public override void DrawObject2Xml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("boundingbox");
            xmlWriter.WriteStartElement("x_left_top");
            xmlWriter.WriteString(PointA.X.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("y_left_top");
            xmlWriter.WriteString(PointA.Y.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("width");
            xmlWriter.WriteString(Size.Width.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("height");
            xmlWriter.WriteString(Size.Height.ToString());
            xmlWriter.WriteEndElement();

            if (Properties.Class != null)
            {
                xmlWriter.WriteStartElement("class");
                xmlWriter.WriteAttributeString("name", Properties.Class);

                for (int i = 0; i < Properties.AtributesName.Length; i++)//zápis všetkých property daneho BB.
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

        public void DrawObject2Xml2(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("boundingbox");
            xmlWriter.WriteStartElement("x_left_top");
            xmlWriter.WriteString(PointA.X.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("y_left_top");
            xmlWriter.WriteString(PointA.Y.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("width");
            xmlWriter.WriteString(Size.Width.ToString());
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("height");
            xmlWriter.WriteString(Size.Height.ToString());
            xmlWriter.WriteEndElement();

            if (Properties.Class != null)
            {
                xmlWriter.WriteStartElement("class_name");

                xmlWriter.WriteStartElement("project_id");
                xmlWriter.WriteString(Properties.Class);
                xmlWriter.WriteEndElement();

                /**if (Properties.AtributesName.Length == 0)
                {
                    xmlWriter.WriteString("");
                }**/
                for (int i = 0; i < Properties.AtributesName.Length; i++)//zápis všetkých property daneho BB.
                {
            //        if (Properties.AtributesValue[i] == "") continue;                    
                    xmlWriter.WriteStartElement(Properties.AtributesName[i]);            
                    xmlWriter.WriteString(Properties.AtributesValue[i]);
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Nacitanie BoundingBoxov zo zadaneho elementu xml
        /// </summary>
        /// <param name="xFigure">element xml</param>
        /// <returns>zoznam boundingboxov daneho xml elementu</returns>
        public override List<DrawObject> DrawObjectFromXml(XElement xFigure)
        {
            var imageBBs = new List<DrawObject>();
            int i = 0;
            foreach (XElement xBB in xFigure.Elements("boundingboxes").Descendants("boundingbox"))
            {
                var bb = new BoundingBox(i++);
                bb.PointA = new Point(Convert.ToInt32(xBB.Element("x_left_top").Value),
                                      Convert.ToInt32(xBB.Element("y_left_top").Value));
                int w = Convert.ToInt32(xBB.Element("width").Value);
                int h = Convert.ToInt32(xBB.Element("height").Value);
                bb.PointB = new Point(bb.PointA.X + w, bb.PointA.Y + h);

                //este potrebujeme nacitat properties BB, ak nejake ma
                var op = LoadPropsFromXml(xBB);
                if (op != null) bb.Properties = op;
                imageBBs.Add(bb);
            }
            return imageBBs;
        }

        public List<DrawObject> DrawObjectFromXml2(XElement xFigure)
        {
            var imageBBs = new List<DrawObject>();
            int i = 0;
            foreach (XElement xBB in xFigure.Elements("boundingboxes").Descendants("boundingbox"))
            {
                var bb = new BoundingBox(i++);
                bb.PointA = new Point(Convert.ToInt32(xBB.Element("x_left_top").Value),
                                      Convert.ToInt32(xBB.Element("y_left_top").Value));
                int w = Convert.ToInt32(xBB.Element("width").Value);
                int h = Convert.ToInt32(xBB.Element("height").Value);
                bb.PointB = new Point(bb.PointA.X + w, bb.PointA.Y + h);

                //este potrebujeme nacitat properties BB, ak nejake ma
                var op = LoadPropsFromXml2(xBB);
                if (op != null) bb.Properties = op;
                imageBBs.Add(bb);
            }
            return imageBBs;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
