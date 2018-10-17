using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Projekt.DrawObjects;
using Projekt.Figure;
using Projekt.Forms;

namespace Projekt.ExportImport
{
    class ImportExportXML : ImportExport
    {
        /// <summary>
        /// konstruktor - vyuzivany pri ukladani do suboru xml.
        /// </summary>
        /// <param name="openImages"></param>
        /// <param name="projectFolder"></param>
        public ImportExportXML(List<BaseFigure> openImages, string projectFolder, bool xmlType) : base(openImages, projectFolder, xmlType)
        {}

        /// <summary>
        /// konstruktor - vyuzivany pri nacitavani zo suboru xml.
        /// </summary>
        public ImportExportXML(string file) : base(file)
        {}

        public int ImportedType
        {
            get;
            set;
        }

        public override int GetImpType()
        {
            return ImportedType;
        }

        public override bool ExportProject(SetNameOutputFile options)
        {
            if (SaveProjectToXML(options.GetOutputName()))
            {
                MessageBox.Show("Ukladanie úspešne ukončené", "Upozornenie", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        public bool ExportProjectImmediately(string fileName)
        {
            if (SaveProjectToXML(fileName))
            {
                MessageBox.Show("Ukladanie úspešne ukončené", "Upozornenie", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sluzi pre ulozenie projektu z prikazovenho riadku
        /// </summary>
        /// <param name="fileFromCmd">nazov suboru kde sa ma ulozit projekt</param>
        /// <returns>bool ak ok inak false</returns>
        public bool ExportProjectExecuteCmd(string fileFromCmd)
        {
            return SaveProjectToXML(fileFromCmd);
        }

        private bool SaveProjectToXML(string nameXml)
        {
            if (Xml2)
            {
                return SaveProjectToXML2(nameXml);
            }
            else
            {
                try
                {
                    List<BaseFigure> images = new List<BaseFigure>();
                    List<BaseFigure> frames = new List<BaseFigure>();

                    foreach (BaseFigure image in OpenImages)
                    {
                        if (typeof(ImageFigure) == image.GetType())
                            images.Add(image);
                        else if (typeof(FrameFigure) == image.GetType())
                            frames.Add(image);
                    }
                    string last = ProjectFolder.Split(new[] { '/', '\\' }).Last();
                    string projFolder = ProjectFolder.Substring(0, ProjectFolder.Count() - last.Count());
                    var xmlws = new XmlWriterSettings();
                    xmlws.Indent = true;

                    XmlWriter xmlWriter = XmlWriter.Create(projFolder + nameXml, xmlws);

                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("data");

                    // Vygenerovanie casti XML pre obrazky
                    xmlWriter.WriteStartElement("images");
                    foreach (BaseFigure image in images)
                    {
                        image.ExportXML(xmlWriter);
                    }
                    xmlWriter.WriteEndElement();

                    // Vygenerovanie casti XML pre snimky videa
                    string src = "";
                    xmlWriter.WriteStartElement("videos");
                    foreach (BaseFigure frame in frames)
                    {
                        if (frame as FrameFigure == null) continue;
                        if (src != (frame as FrameFigure).VideoSource)
                        {
                            if (!src.Equals(""))
                            {  //Zapis end-tagu pre video. if - preskocenie pri prvom snimku, ked este nieje zapisany start-tag
                                xmlWriter.WriteEndElement();    //  frames
                                xmlWriter.WriteEndElement();    //  video                         
                            }
                            src = (frame as FrameFigure).VideoSource;
                            xmlWriter.WriteStartElement("video");
                            xmlWriter.WriteStartElement("src");
                            xmlWriter.WriteString(src);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("frames");
                        }
                        frame.ExportXML(xmlWriter);
                    }
                    if (!src.Equals("")) xmlWriter.WriteEndElement();   //Ak video neobsahuje snimky, nezapisovat end-tag
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Close();
                    return true;
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }
        }

        private bool SaveProjectToXML2(string nameXml)
        {
            try
            {
                List<BaseFigure> images = new List<BaseFigure>();
                List<BaseFigure> frames = new List<BaseFigure>();

                foreach (BaseFigure image in OpenImages)
                {
                    if (typeof(ImageFigure) == image.GetType())
                        images.Add(image);
                    else if (typeof(FrameFigure) == image.GetType())
                        frames.Add(image);
                }
                string last = ProjectFolder.Split(new[] { '/', '\\' }).Last();
                string projFolder = ProjectFolder.Substring(0, ProjectFolder.Count() - last.Count());
                var xmlws = new XmlWriterSettings();
                xmlws.Indent = true;

                XmlWriter xmlWriter = XmlWriter.Create(projFolder + nameXml, xmlws);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("data");
                
                xmlWriter.WriteStartElement("metadata");
                xmlWriter.WriteStartElement("data_id");
                xmlWriter.WriteString("sa_dataset");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("parent");
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("version_major");
                xmlWriter.WriteString("2");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("xml_sid");
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("description");
                xmlWriter.WriteString("Anotacny nastroj v1.02");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("images");
                foreach (BaseFigure image in images)
                {
                    image.ExportXML2(xmlWriter);
                }
                xmlWriter.WriteEndElement();                
                
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return true;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        /// <summary>
        /// Nacitanie vsetkych objektov z XML suboru.
        /// </summary>
        /// <param name="imagesSrc"></param>
        /// <param name="imagesAttrs"></param>
        /// <param name="imagesDrawObjects"></param>
        /// <param name="videosSrc"></param>
        /// <param name="framesAttrs"></param>
        /// <param name="framesDrawObject"></param>
        /// <returns></returns>
        public override bool ImportProject(ref List<string> imagesSrc, ref List<List<string[]>> imagesAttrs, ref List<List<DrawObject>> imagesDrawObjects,
                                                       ref List<string> videosSrc, ref List<List<List<string[]>>> framesAttrs, ref List<List<List<DrawObject>>> framesDrawObject)
        {
            try
            {
                XElement xelement = XElement.Load(FileToLoad);
                LoadImage(xelement, ref imagesSrc, ref imagesAttrs, ref imagesDrawObjects);
                LoadFrame(xelement, ref videosSrc, ref framesAttrs, ref framesDrawObject);
            }
            catch (Exception)
            {
                MessageBox.Show("Vstupný XML súbor nemá požadovaný formát.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool LoadImage(XElement xelement, ref List<string> imagesSrc, ref List<List<string[]>> imagesAttrs, ref List<List<DrawObject>> imagesDrawObjects)
        {
            //nacitame cesty k obrazkom
            var metadataXml = xelement.Element("metadata") == null;
                        
            if (!xelement.Elements("images").Descendants("image").Any())
                return false;

            var path = from img in xelement.Elements("images").Descendants("image") select img.Element("src").Value;
            imagesSrc = path.ToList();

            //nacitame vseobecne vlastnosti jednotlivych obrazkov
            //pre kazdy obrazok tvoria zaznamy dvojice: nazov-hodnota atributu
            imagesAttrs = new List<List<string[]>>();
            
            foreach (XElement xImage in xelement.Elements("images").Descendants("image"))
            {
                imagesAttrs.Add(GetFigureAtribute(xImage));
                var dro = new List<DrawObject>();
                if (metadataXml)
                {
                    ImportedType = 1;
                    dro.AddRange(new BoundingBox(1).DrawObjectFromXml(xImage));
                    dro.AddRange(new Polyline(1).DrawObjectFromXml(xImage));
                    dro.AddRange(new Painting(1).DrawObjectFromXml(xImage));
                }
                else
                {
                    ImportedType = 2;
                    dro.AddRange(new BoundingBox(1).DrawObjectFromXml2(xImage));
                }
                imagesDrawObjects.Add(dro);
            }
            return true;
        }

        private bool LoadFrame(XElement xelement, ref List<string> videosSrc, ref List<List<List<string[]>>> framesAttrs, ref List<List<List<DrawObject>>> framesDrawObjects)
        {
            //nacitame cesty k obrazkom
            if (!xelement.Elements("videos").Descendants("video").Any())
                return false;

            var path = from img in xelement.Elements("videos").Descendants("video") select img.Element("src").Value;
            videosSrc = path.ToList();
            
            foreach (XElement xVideo in xelement.Elements("videos").Descendants("video"))
            {
                //nacitame vseobecne vlastnosti jednotlivych frameov
                //pre kazdy frameov tvoria zaznamy dvojice: nazov-hodnota atributu
                //pricom prvy bude vzdy atribut reprezentujuci cislo daneho frame
                var frmsAttrs = new List<List<string[]>>();
                var frmsDrawObjs = new List<List<DrawObject>>();
                foreach (XElement xFrame in xVideo.Elements("frames").Descendants("frame"))
                {
                    frmsAttrs.Add(new List<string[]> { new[] { "frame-number", xFrame.Element("frame-number").Value } });
                    frmsAttrs.Last().AddRange(GetFigureAtribute(xFrame));
                    frmsDrawObjs.Add(new BoundingBox(1).DrawObjectFromXml(xFrame));
                    frmsDrawObjs.Last().AddRange(new Painting(1).DrawObjectFromXml(xFrame));
                    frmsDrawObjs.Last().AddRange(new Polyline(1).DrawObjectFromXml(xFrame));
                }
                framesAttrs.Add(frmsAttrs);
                framesDrawObjects.Add(frmsDrawObjs);
            }
            return true;
        }

        private static List<string[]> GetFigureAtribute(XElement xFigure)
        {
            var imageAtr = new List<string[]>(); //zaznamy pre konkretny obrazok
            foreach (XElement xAttr in xFigure.Elements("attributes").Descendants("attribute"))
            {
                imageAtr.Add(new[] {xAttr.Attribute("name").Value, xAttr.Value});
            }
            return imageAtr;
        }

    }
}
