using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Emgu.CV;
using Emgu.CV.Structure;
using Projekt.DrawObjects;

namespace Projekt.Figure
{
    /// <summary>
    /// Trieda reprezentujúca Obrázok, ktorá je potomkom triedy BaseFigure.
    /// </summary>
    public class ImageFigure : BaseFigure
    {
        private readonly string _image;
        /// <summary>
        /// Konštruktor.
        /// </summary>
        /// <param name="image">Konkrétny obrázok.(uplna cesta k obrazku)</param>
        /// <param name="source">Relativna cesta k danému obrázku (od xml suboru). Od zlozky projektu(vratane nej)</param>
        public ImageFigure(string image, string source) : base(source)
        {
            _image = image;
        }

        /// <summary>
        /// vratenie obrazku
        /// </summary>
        /// <returns>obrazok</returns>
        public override Image<Bgr, Byte> GetImage()
        {
            return new Image<Bgr, Byte>(_image);
        }

        /// <summary>
        /// Vytvorenie časti XML súboru, ktorá bude reprezentovať obrázok a objekty na ňom.
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pouzity na vytvorenie dokumentu</param>
        public override void ExportXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("image");
                xmlWriter.WriteStartElement("src");
                    xmlWriter.WriteString(Source);
                xmlWriter.WriteEndElement();
            base.ExportXML(xmlWriter);
        }

        public override void ExportXML2(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("image");
            xmlWriter.WriteStartElement("src");
            xmlWriter.WriteString(Source);
            xmlWriter.WriteEndElement();
            base.ExportXML2(xmlWriter);
        }
    }
}
