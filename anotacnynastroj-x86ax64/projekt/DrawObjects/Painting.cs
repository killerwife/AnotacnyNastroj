using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Projekt.DrawObjects
{
    /// <summary>
    /// Trieda reprezenujúca painting, je potomkom triedy DrawObject.
    /// </summary>
    public class Painting : DrawObject
    {
        /// <summary>
        /// Body tvoriace painting.
        /// </summary>
        public List<Point> Points { get; set; }

        private BoundingBox _boundingBox;
        /// <summary>
        /// Bounding box ohranicujuci painting.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get
            {
                return _boundingBox;
            }
            set { _boundingBox = value ?? CreateBB(); }
        }
        
        /// <summary>
        /// Konštruktor.
        /// </summary>
        /// <param name="id">ID paintingu.</param>
        public Painting(int id)
        { 
            ID = id;
            Name = "Painting " + ID;
            Points = new List<Point>();
            Properties = new ObjectProperties();
            BoundingBox = new BoundingBox(id) {Properties = Properties};
        }

        /// <summary>
        /// Prida obvodove body do paintingu. Prida tolko bodou aby bol pridavany bod
        /// spojeny s poslednym bodom paintingu
        /// </summary>
        /// <param name="point">bod ktory chceme pridat do paintingu</param>
        public void AddPoints(Point point)
        {
            if (Points.Contains(point)) return;
            //pre vytvorenie BB potrebujem lavy horny a pravy dolny bod to je - min a max X,Y
            if (BoundingBox.PointA.X > point.X) BoundingBox.PointA = new Point(point.X, BoundingBox.PointA.Y);
            else if (BoundingBox.PointB.X < point.X) BoundingBox.PointB = new Point(point.X, BoundingBox.PointB.Y);
            if (BoundingBox.PointA.Y > point.Y) BoundingBox.PointA = new Point(BoundingBox.PointA.X, point.Y);
            else if (BoundingBox.PointB.Y < point.Y) BoundingBox.PointB = new Point(BoundingBox.PointB.X, point.Y);

            if (!Points.Any()) Points.Add(point);
            else Points.AddRange(Line2Points(Points.Last(), point));
        }

        /// <summary>
        /// Vytvorenie BB ktory bude opisovat dany PA.
        /// </summary>
        /// <returns></returns>
        private BoundingBox CreateBB()
        {
            var bb = new BoundingBox(ID) {Properties = Properties};

            if (!Points.Any())
            {
                bb.PointA = new Point(0,0);
                return bb;
            }

            bb.PointA = Points[0];
            bb.PointB = Points[1];
            foreach (var point in Points)
            {
                if (bb.PointA.X > point.X) bb.PointA = new Point(point.X, bb.PointA.Y);
                else if (bb.PointB.X < point.X) bb.PointB = new Point(point.X, bb.PointB.Y);
                if (bb.PointA.Y > point.Y) bb.PointA = new Point(bb.PointA.X, point.Y);
                else if (bb.PointB.Y < point.Y) bb.PointB = new Point(bb.PointB.X, point.Y);
            }
            return bb;
        }

        /// <summary>
        /// Vytvorenie časti XML súboru, ktorá bude reprezentovať painting. 
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pomocou ktoreho sa vytvara subor</param>
        public override void DrawObject2Xml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("painting");
                xmlWriter.WriteStartElement("boundingbox");
                    xmlWriter.WriteStartElement("x_left_top");
                    xmlWriter.WriteString(BoundingBox.PointA.X.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("y_left_top");
                    xmlWriter.WriteString(BoundingBox.PointA.Y.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("width");
                    xmlWriter.WriteString(BoundingBox.Size.Width.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("height");
                    xmlWriter.WriteString(BoundingBox.Size.Height.ToString());
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("pixels");
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
        /// Nacitanie Paintingov zo zadaneho elementu xml
        /// </summary>
        /// <param name="xFigure">element xml</param>
        /// <returns>zoznam paintingov daneho xml elementu</returns>
        public override List<DrawObject> DrawObjectFromXml(XElement xFigure)
        {
            var imagePAs = new List<DrawObject>();
            int i = 0;
            foreach (XElement xPA in xFigure.Elements("paintings").Descendants("painting"))
            {
                var pa = new Painting(i++);
                var xPixels = xPA.Element("pixels");
                if (xPixels != null)
                {
                    string[] pixels = xPixels.Value.Split(';');
                    var pxls = new List<Point>();
                    foreach (var pixel in pixels)
                    {
                        string[] pxl = pixel.Split(',');
                        if (pxl.Count() == 2) pxls.Add(new Point(Convert.ToInt32(pxl[0]), Convert.ToInt32(pxl[1])));
                    }
                    pa.Points = pxls;
                }

                BoundingBox bb = null;
                var xBbox = xPA.Element("boundingbox");
                if (xBbox != null)
                {
                    bb = new BoundingBox(i++);
                    bb.PointA = new Point(Convert.ToInt32(xBbox.Element("x_left_top").Value),
                                          Convert.ToInt32(xBbox.Element("y_left_top").Value));
                    int w = Convert.ToInt32(xBbox.Element("width").Value);
                    int h = Convert.ToInt32(xBbox.Element("height").Value);
                    bb.PointB = new Point(bb.PointA.X + w, bb.PointA.Y + h);
                }
                pa.BoundingBox = bb;

                //este potrebujeme nacitat properties PA, ak nejake ma
                var op = LoadPropsFromXml(xPA);
                if (op != null) pa.Properties = op;
                imagePAs.Add(pa);
            }
            return imagePAs;
        }

        /// <summary>
        /// Spojenie dvoch bodou PA.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static IEnumerable<Point> Line2Points(Point a, Point b)
        {
            var points = new List<Point> {a};

            float dx = b.X - a.X;
            float dy = b.Y - a.Y;

            var m = (dx == 0) ? 0 : dy / dx;
            var c = a.Y - a.X * m;

            if (a.X < b.X)
            {
                for (int x = a.X + 1; x <= b.X; x++)
                {
                    var p = new Point(x, (int)Math.Round(m * x + c));
                    if (Math.Abs(points.Last().Y - p.Y) > 1)
                    {
                        var last = points.Last();
                        if (last.Y < p.Y)
                        {
                            for (var j = points.Last().Y + 1; j <= p.Y; j++)
                            {
                                points.Add(new Point(x, j));
                            }
                        }
                        else if (last.Y > p.Y)
                        {
                            for (var j = points.Last().Y - 1; j >= p.Y; j--)
                            {
                                points.Add(new Point(x, j));
                            }
                        }
                    }
                    else points.Add(p);
                }
            }
            else if (a.X > b.X)
            {
                for (int x = a.X - 1; x >= b.X; x--)
                {
                    var p = new Point(x, (int)Math.Round(m * x + c));
                    if (Math.Abs(points.Last().Y - p.Y) > 1)
                    {
                        var last = points.Last();
                        if (last.Y < p.Y)
                        {
                            for (var j = points.Last().Y + 1; j <= p.Y; j++)
                            {
                                points.Add(new Point(x, j));
                            }
                        }
                        else if (last.Y > p.Y)
                        {
                            for (var j = points.Last().Y - 1; j >= p.Y; j--)
                            {
                                points.Add(new Point(x, j));
                            }
                        }
                    }
                    else points.Add(p);
                }
            }
            else if (a.X == b.X)
            {
                if (Math.Abs(a.Y - b.Y) > 1)
                {
                    if (a.Y < b.Y)
                    {
                        for (int j = points.Last().Y + 1; j <= b.Y; j++)
                        {
                            points.Add(new Point(a.X, j));
                        }
                    }
                    else if (a.Y > b.Y)
                    {
                        for (int j = points.Last().Y - 1; j >= b.Y; j--)
                        {
                            points.Add(new Point(a.X, j));
                        }
                    }
                }
            }
            points.RemoveAt(0);
            if ((!points.Any()) || (points.Last() != b)) points.Add(b);
            return points;
        }
    }
}
