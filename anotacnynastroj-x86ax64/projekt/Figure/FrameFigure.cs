using System;
using System.IO;
using System.Linq;
using System.Xml;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Projekt.Figure
{
    /// <summary>
    /// Trieda reprezentujúca konkrétny frame z videa.
    /// </summary>
    class FrameFigure : BaseFigure
    {
        /// <summary>
        /// Poradove cislo frame v danom videu.
        /// </summary>
        public int NumberFrame { get; set; }

        /// <summary>
        /// Relativna cesta k videu na ktorom sa nachadza dany frame.
        /// (od xml suboru). Od zlozky projektu(vratane nej)
        /// </summary>
        public string VideoSource { get; set; }

        private readonly string _frame;//absolutna cesta k obrazku ktory reprezentuje dany frame.

        /// <summary>
        /// Konštruktor.
        /// </summary>
        /// <param name="frame">Absolutna cesta k obrazku ktory reprezentuje dany frame.</param>
        /// <param name="videoSource">Relativna cesta k videu na ktorom sa nachádza dany frame.(od xml suboru). Od zlozky projektu(vratane nej)</param>
        /// <param name="numberFrame">Číslo frame v danom videu.</param>
        public FrameFigure(string frame, string videoSource, int numberFrame)
            : base(string.Format("{0}{1}.png", videoSource.Remove(videoSource.Length - videoSource.Split(new[] { '/', '\\' }).Last().Length), numberFrame))
        {
            _frame = frame;
            VideoSource = videoSource;
            NumberFrame = numberFrame;
        }

        /// <summary>
        /// vratenie obrazku reprezentujuceho danu snimku videa
        /// </summary>
        /// <returns>obrazk reprezentujuce danu snimku videa</returns>
        public override Image<Bgr, Byte> GetImage()
        {
            return new Image<Bgr, Byte>(_frame);
        }

        /// <summary>
        /// Vytvorenie časti XML súboru, ktorá bude reprezentovať frame z videa a objekty na ňom.
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pouzity pre vytovrenie dokumetu</param>
        public override void ExportXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("frame");
            xmlWriter.WriteStartElement("frame-number");
            xmlWriter.WriteString(NumberFrame+"");
            xmlWriter.WriteEndElement();
            base.ExportXML(xmlWriter);
        }

    }
}
