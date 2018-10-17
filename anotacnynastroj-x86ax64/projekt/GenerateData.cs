using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Projekt.DrawObjects;
using Projekt.Enums;
using Projekt.Figure;
using Projekt.Forms;

namespace Projekt
{
    class GenerateData
    {
        private List<BaseFigure> _openImages;

        /// <summary>
        /// Pocet umelych dat vygenerovanych pre jednotlive casti - Trenovacie, Validacne, Testovacie
        /// </summary>
        private int[] _countArtifData;

        /// <summary>
        /// Pocet realnych dat vygenerovanych pre jednotlive casti - Trenovacie, Validacne, Testovacie
        /// </summary>
        private int[] _countRealData;

        /// <summary>
        /// Nazvy XML suborov do ktorych budu ulozene originalne obrazky z ktorych vznikli jednotlive
        /// Trenovacie, Validacne, Testovacie
        /// </summary>
        private List<string> _nameOutXml;

        private Random _rnd;

        public GenerateData(List<BaseFigure> openImages)
        {
            _openImages = openImages;
            _countArtifData = new[]{ 0, 0, 0 };
            _countRealData = new[]{ 0, 0, 0 };
            _rnd = new Random((int)DateTime.Now.Ticks);
        }

        public GenerateData()
        {}

        /// <summary>
        /// Spustenie normalizacie a ukladania BBs ako obrazkov do vytvorenej adresarovej struktury
        /// </summary>
        /// <param name="progress">progress bar</param>
        /// <param name="crImgFromBB"></param>
        public void SaveBBAsAsImages(ProgressForm progress, CreateImageFromDrawObj crImgFromBB)
        {
            try
            {
                //string last = crImgFromBB.FolderToSave.TrimEnd('/', '\\').Split(new[] { '/', '\\' }).Last();
                //string homeFolder = crImgFromBB.FolderToSave.Substring(0, crImgFromBB.HomeFolder.Count() - last.Count());
                _rnd = new Random((int)DateTime.Now.Ticks);
                if (!Directory.Exists(crImgFromBB.FolderToSave))
                    Directory.CreateDirectory(crImgFromBB.FolderToSave);

                int count = _openImages.Sum(openImage => openImage.BoundBoxes.Count + openImage.Paintings.Count);
                if (count == 0)
                {
                    crImgFromBB.MyClosed();
                    progress.MyClosed();
                    MessageBox.Show("Ziadne data pre generovanie nenajdene.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    return;
                }

                _countArtifData = new[] { 0, 0, 0 };
                _countRealData = new[] { 0, 0, 0 };

                progress.Count = count;
                progress.Actuall = 0;
                string errorImages = "";
                int errorBB = 0;

                var xmlWriters = InicializeXMLWriters(crImgFromBB.FolderToSave);
                for (int k = 0; k < _openImages.Count; k++)
                {
                    BaseFigure parImage = _openImages[k];
                    var listsBbs = new List<List<BoundingBox>>{new List<BoundingBox>(), new List<BoundingBox>(), new List<BoundingBox>()};

                    using (Image<Bgr, byte> image = parImage.GetImage())
                    {
                        foreach (BoundingBox bb in parImage.BoundBoxes)
                        {
                            if (!SaveBBOfImage(progress, crImgFromBB, bb, image, xmlWriters, k, listsBbs, ref errorBB,
                                                ref errorImages))
                                return;
                        }
                        foreach (var painting in parImage.Paintings)
                        {
                            var bb = painting.BoundingBox;
                            bb.Properties = painting.Properties;
                            if (!SaveBBOfImage(progress, crImgFromBB, bb, image, xmlWriters, k, listsBbs, ref errorBB,
                                                ref errorImages))
                                return;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if(listsBbs[i].Any())
                            SaveBBToXMLForPart(xmlWriters[i], listsBbs[i], parImage);
                    }
                }

                if (errorImages != "" || errorBB != 0)
                {
                    string message1 = ((errorImages == "")
                                           ? ""
                                           : ("BoundingBoxy na obrázkoch číslo: " + errorImages + " \nnemajú definovanú triedu budú uložené do zložky:\n" +
                                              crImgFromBB.FolderToSave + "unclassified\\.")) +
                                      ((errorBB == 0)
                                           ? ""
                                           : ("\nBoli nájdené BoundingBoxy s chybnou veľkosťou, celkom: " + errorBB + ", tieto boli ignorované."));
                    MessageBox.Show(message1, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                CloseXMLWriters(xmlWriters);

                CreateLogFile(crImgFromBB);
                crImgFromBB.MyClosed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - GenerateData-SaveBBsAsImage()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// vrati false ak bolo zrusene ukladanie inak true.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="crImgFromBB"></param>
        /// <param name="bb"></param>
        /// <param name="image"></param>
        /// <param name="xmlWriters"></param>
        /// <param name="k"></param>
        /// <param name="listsBbs"></param>
        /// <param name="errorBB"></param>
        /// <param name="errorImages"></param>
        /// <returns></returns>
        private bool SaveBBOfImage(ProgressForm progress, CreateImageFromDrawObj crImgFromBB, BoundingBox bb, Image<Bgr, byte> image,
                              List<XmlWriter> xmlWriters, int k, List<List<BoundingBox>> listsBbs, ref int errorBB, ref string errorImages)
        {
            if (!CheckBBLocationAndSize(bb, image))
            {
                progress.Actuall++;
                errorBB++;
                return true;
            }

            //kontrola ci nebol priebeh nacitavania zruseny
            if (progress.IsCanceled())
            {
                CreateLogFile(crImgFromBB);
                progress.MyClosed();
                //Zrusime form CreateImageFromDrawObj
                crImgFromBB.MyClosed();
                CloseXMLWriters(xmlWriters);
                return false;
            }

            int dataType;//urcuje pre ktoru cast bol vygenerovany obrazok ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2
            if (!SaveDrawObjToImage(out dataType, bb, bb.GetRectangle(), image, crImgFromBB))
                errorImages += (k + 1) + ",";
            if (dataType != -1)
            {
                listsBbs[dataType].Add(bb);
            }

            progress.Actuall++;
            return true;
        }

        /// <summary>
        /// Ulozenie daneho bb do daneho xml podla toho pre ktoru cast bol ulozeny Trening, Validacia, Testovanie 
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="bbs"></param>
        /// <param name="parImage"></param>
        private void SaveBBToXMLForPart(XmlWriter xmlWriter, List<BoundingBox> bbs, BaseFigure parImage)
        {
            //musim zistit ci uz je dany image v xml alebo treba aj pre neho vytvorit data v xml
            xmlWriter.WriteStartElement("image");
                xmlWriter.WriteStartElement("src");
                    xmlWriter.WriteString(parImage.Source);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("attributes");
                    if (parImage.Properties != null)
                        for (int i = 0; i < parImage.Properties.Count; i++)
                        {
                            xmlWriter.WriteStartElement("attribute");
                            xmlWriter.WriteAttributeString("name", parImage.Properties[i][0]);
                            xmlWriter.WriteString(parImage.Properties[i][1]);
                            xmlWriter.WriteEndElement();
                        }
                xmlWriter.WriteEndElement();
            
                xmlWriter.WriteStartElement("boundingboxes");
                    foreach (var bb in bbs)
                    {
                        bb.DrawObject2Xml(xmlWriter);
                    }    
                xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Vrati true ak ma draw object pre ktory sa generuju data definovanu triedu(v properties). Ak draw object nema definovane property je rectangle ulozeny do zlozky unclaased
        /// Inak vrati false a data pre tento draw object ulozi do zlozky unclassified
        /// </summary>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        /// <param name="drawObj">draw object - BB, PL</param>
        /// <param name="rectangle">rectalgle ktory popisuje dany draw object</param>
        /// <param name="image">obrazok na ktorom sa nachadza draw object</param>
        /// <param name="crImgFromDrawObj"></param>
        /// <returns></returns>
        private bool SaveDrawObjToImage(out int dataType, DrawObject drawObj, Rectangle rectangle, Image<Bgr, byte> image, CreateImageFromDrawObj crImgFromDrawObj)
        {
            bool ret = true;
            //premena sluzi na uchovanie stavu ci treba alebo netreba generovat umele data.
            bool genArticifImage = false;
            
            dataType = -1;

            //treba vytvorit adresarovu strukturu podla property daneho bb ak ju uzivatel chce vytvorit
            //ak nie tak vytvorime zlozky len pre treningovu, validacnu a testovaciu cast
            string path;
            if (string.IsNullOrEmpty(drawObj.Properties.Class))
            {
                path = "unclassified\\";
                ret = false;
            }
            else
            {
                //potrebujem najskor zadelit dany bb do prislusnej skupiny podla percentualneho rozdelenia
                //cize ci sa ulozi do zlozky trenovacich validacnych alebo testovacich dat.
                var distr = GetDirectoryToSave(crImgFromDrawObj.PercentDistribution);
                genArticifImage = distr.Generate;
                dataType = distr.Type;
                path = distr.FolderName + "\\";

                if (crImgFromDrawObj.SettingForFolds.TypeOfSortDir == ETypeOfSortDir.RealArtifFoldAsFirst)
                    path += "{\"FOLDER1\"}\\{\"FOLDER2\"}\\"; //FOLDER1 nahradime prislusnym adresarom REAL alebo ARTIFICIAL
                                              //FOLDER2 nahradime prislusnou geom. transf. MOVE, ROTATE, SCALE, REFLECTION

                path += drawObj.Properties.Class + "\\";
                int maxCountAtrs = (drawObj.Properties.AtributesValue.Length > crImgFromDrawObj.SettingForFolds.MaxCountAttrs ? crImgFromDrawObj.SettingForFolds.MaxCountAttrs : drawObj.Properties.AtributesValue.Length);
                for (int i = 0; i < maxCountAtrs; i++)
                {
                    if (!String.IsNullOrEmpty(drawObj.Properties.AtributesValue[i])) 
                        path += drawObj.Properties.AtributesValue[i] + "\\";
                }
                
            }

            try
            {
                SaveImage(crImgFromDrawObj, rectangle, image, path, genArticifImage, dataType);
            }
             catch (Exception ex)
            {
                MessageBox.Show("Chyba pri generovani obrazkov z BB:" + ex.Message, "Error - GenerateData.SaveDrawObjToImage()",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return ret;
        }
  
        /// <summary>
        /// xmlWriters pre zapisovanie dat z ktorych boli vytvorene jednotlive treningove[0], validacne[1] a testovacie data[2].
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private List<XmlWriter> InicializeXMLWriters(string folder)
        {
            _nameOutXml = new List<string>{"Training" + DateTime.Now.ToString("HHmmssfff") + ".xml", "Validation" + DateTime.Now.ToString("HHmmssfff") + ".xml", "Testing" + DateTime.Now.ToString("HHmmssfff") + ".xml"};

            XmlWriterSettings xmlws = new XmlWriterSettings();
            xmlws.Indent = true;
            List<XmlWriter> xmlWriters = new List<XmlWriter>
                                             {
                                                 XmlWriter.Create(folder + _nameOutXml[0], xmlws),
                                                 XmlWriter.Create(folder + _nameOutXml[1], xmlws),
                                                 XmlWriter.Create(folder + _nameOutXml[2], xmlws)
                                             };

            foreach (var xmlWriter in xmlWriters)
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("data");
                xmlWriter.WriteStartElement("images");
            }
            return xmlWriters;
        }

        private void CloseXMLWriters(List<XmlWriter> xmlWriters)
        {
            foreach (var xmlWriter in xmlWriters)
            {
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }

        /// <summary>
        /// Vytvorenie zaznamu o nastaveniach pre dane generovanie umelych dat
        /// </summary>
        /// <param name="crImgFromDrawObj"></param>
        private void CreateLogFile(CreateImageFromDrawObj crImgFromDrawObj)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(crImgFromDrawObj.FolderToSave + "generate " + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".log", FileMode.Create);
                sw = new StreamWriter(fs);

                string gen = crImgFromDrawObj.Text + ":\n\n" +
                             "Vygenerovane data boli ulozene do zlozky:\n " +
                             "    - " + crImgFromDrawObj.FolderToSave + "\n\n"+
                             "Boli generovane:\n" +
                             (crImgFromDrawObj.GenRealData ? "    - Realne data - " + _countRealData.Sum() + " obrazkov\n" : "") +
                             (crImgFromDrawObj.GenArtificialData ? "    - Umele data - " + _countArtifData.Sum() + " obrazkov\n" : "");
                sw.WriteLine(gen);
 
                int type = crImgFromDrawObj.PercentDistribution.Values[0].Type;
                string str = "Percentualne rozdelenie generovanych dat:\n" +
                    "    - " + crImgFromDrawObj.PercentDistribution.Values[0].FolderName + " cast: " + crImgFromDrawObj.PercentDistribution.Keys[0] + "% - " + (type < 0 || type > 2 ? 0 : _countRealData[type]) + " skutocnych(RealData) obrazkov.\n";
                
                type = crImgFromDrawObj.PercentDistribution.Values[1].Type;
                str += "    - " + crImgFromDrawObj.PercentDistribution.Values[1].FolderName + " cast: " + crImgFromDrawObj.PercentDistribution.Keys[1] + "% - " + (type < 0 || type > 2 ? 0 : _countRealData[type]) + " skutocnych(RealData) obrazkov.\n";
                
                type = crImgFromDrawObj.PercentDistribution.Values[2].Type;
                str += "    - " + crImgFromDrawObj.PercentDistribution.Values[2].FolderName + " cast: " + crImgFromDrawObj.PercentDistribution.Keys[2] + "% - " + (type < 0 || type > 2 ? 0 : _countRealData[type]) + " skutocnych(RealData) obrazkov.\n";
                sw.WriteLine(str);

                if (!crImgFromDrawObj.Unnormalized())
                {
                    string s = "";
                    if (crImgFromDrawObj.DrawObjType == 0) s = "Nastavenia normalizacie:\n";
                    else if (crImgFromDrawObj.DrawObjType == 1) s = "Nastavenia velkosti okna:\n";
                    s += "    - pomer: " + crImgFromDrawObj.GetScale()[0] + ":" + crImgFromDrawObj.GetScale()[1] + "\n" +
                         "    - sirka: " + crImgFromDrawObj.ImageWidth + " px\n" +
                         "    - vyska: " + crImgFromDrawObj.ImageHeight + " px\n";
                    if (crImgFromDrawObj.DrawObjType == 1) s += "Velkost kroku: " + crImgFromDrawObj.StepLength + "\n";
                    sw.WriteLine(s);
                }
                else
                    sw.WriteLine("Data boli generovane bez normalizacie\n");

                if (crImgFromDrawObj.GenArtificialData)
                {
                    sw.WriteLine("Nastavenie generovania umelych dat:");
                    sw.WriteLine("    - data boli generovane pre:");
                    if (crImgFromDrawObj.ArtificTraining) sw.WriteLine("       - Trenovacia cast - " + _countArtifData[0] + " obrazkov");
                    if (crImgFromDrawObj.ArtificValidation) sw.WriteLine("       - Validacna cast - " + _countArtifData[1] + " obrazkov");
                    if (crImgFromDrawObj.ArtificTesting) sw.WriteLine("       - Testovacia cast - " + _countArtifData[2] + " obrazkov");
                    sw.WriteLine("    - jednotlive volby generovania:");

                    for (int i = 0; i < crImgFromDrawObj.SettingArtifTransf.Count; i++)
                    {
                        var setting = crImgFromDrawObj.SettingArtifTransf[i];
                        sw.WriteLine("       - " + setting + " (Vygenerovane v zlozke - " +
                                     "[Setting " + i + "-" + setting.Name + "])");
                    }
                }

                if (crImgFromDrawObj.DrawObjType == 0)
                {
                    string last = crImgFromDrawObj.HomeFolder.TrimEnd('/', '\\').Split(new[] { '/', '\\' }).Last();
                    string homeFolder = crImgFromDrawObj.HomeFolder.Substring(0, crImgFromDrawObj.HomeFolder.Count() - last.Count());

                    string outXml =
                    "\nJednotlive XML subory obsahujuce povodne data, z ktorych boli vytvorene TRENINGOVE, VALIDACNE, A TESTOVACIE data\n" +
                    "su: " + _nameOutXml[0] + "; " + _nameOutXml[1] + "; " + _nameOutXml[2] + ".\n" +
                    "Pre nacitanie je potrebne ich presunut do zlozky: " + homeFolder;
                    sw.Write(outXml);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error pri vytvarani log suboru:\n" + exc.Message, "Ërror.GenerateClass.CreateLogFile()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// Ulozenie obrazka bud bez zmeny(generovanie skutocnych dat) alebo so zmenou(generovanie umelych dat)
        /// alebo aj aj.
        /// </summary>
        /// <param name="crImgFromDrawObj"></param>
        /// <param name="rectangle">rectangle ktory sa ma ulozit</param>
        /// <param name="image">obrazok z ktoreho sa ma vyrezat rectangle</param>
        /// <param name="genPath">vygenerovana cast cesty v adresarovej strukture kde sa ma ulozit dany obrazok</param>
        /// <param name="genArticifImage">ak true tak sa maju pre dany obrazok vygenerovat umele data, zalezi do ktorej skupiny dat patri, cize ci sa pre tuto skuponu generuju umele data</param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void SaveImage(CreateImageFromDrawObj crImgFromDrawObj, Rectangle rectangle, Image<Bgr, byte> image, string genPath, bool genArticifImage, int dataType)
        {
            //pri generovani skutocnych dat skontrolujeme len ci chceme generovat normalizovane alebo nie
            string folderToSave = crImgFromDrawObj.FolderToSave + genPath.Replace("{\"FOLDER1\"}", "RealData").Replace("{\"FOLDER2\"}\\", "") +
                                  (crImgFromDrawObj.SettingForFolds.TypeOfSortDir == ETypeOfSortDir.RealArtifFoldAsLast ? "RealData\\" : "");
            if (crImgFromDrawObj.GenRealData)
            {
                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                string name = DateTime.Now.ToString("yyyyMMddHHmmssfff.") + crImgFromDrawObj.OutputImageFormat;
                if (crImgFromDrawObj.Unnormalized())
                    GetImageUnnormRect(rectangle, image).Save(folderToSave + name);
                else GetImageNormRect(rectangle, image, crImgFromDrawObj.GetSize(), crImgFromDrawObj.GetScale()).Save(folderToSave + name);

                if (dataType > -1 && dataType < 3) _countRealData[dataType]++;//zvysime pocet vygenerovanych realnych obr. pre tuto skupinu
            }

            //pri generovani umelych dat najskor skontrolujeme ci sa maju pre dany obrazok generovat ak nie return
            if (!crImgFromDrawObj.GenArtificialData || !genArticifImage) return;

            folderToSave = crImgFromDrawObj.FolderToSave + genPath.Replace("{\"FOLDER1\"}", "ArtificialData") +
                           (crImgFromDrawObj.SettingForFolds.TypeOfSortDir == ETypeOfSortDir.RealArtifFoldAsLast ? "ArtificialData\\" : "");

            //umely obrazok ulozime do rovnakej adresarovej struktury len pridame este zlozku s nazvom
            //zmeny ktora bola s obrazkom spravena (posun, rotacia, zmena velkosti, zrkadlenie) a v tejto
            //zlozke bude ulozeny dany obrazok
            //dalej je potrebne zistit ake zmeny sa maju s obrazkom vykonat pri tvorbe umelych dat

            int j = 0;
            if (crImgFromDrawObj.SettingArtifTransf.Any())
            {
                foreach (var setting in crImgFromDrawObj.SettingArtifTransf)
                {
                    string fold2Save = folderToSave;
                    if (crImgFromDrawObj.SettingForFolds.TypeOfSortDir == ETypeOfSortDir.RealArtifFoldAsFirst)
                        fold2Save = fold2Save.Replace("{\"FOLDER2\"}", "Setting " + j + "-" + setting.Name);
                    else
                        fold2Save += "Setting " + j + "-" + setting.Name + "\\";
                    j++;
                    if (!Directory.Exists(fold2Save))
                        Directory.CreateDirectory(fold2Save);

                    int countOk = setting.GenerateData(image, rectangle, fold2Save, crImgFromDrawObj.GetSize(), crImgFromDrawObj.Unnormalized(), crImgFromDrawObj.GetScale(), crImgFromDrawObj.OutputImageFormat);
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType] += countOk;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
          /*  if (crImgFromDrawObj.ArtificMoves.Any())
            {
                GenerateArtifDataMove(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }

            if (crImgFromDrawObj.ArtificRotates.Any())
            {
                GenerateArtifDataRotate(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }

            if (crImgFromDrawObj.ArtificScales.Any())
            {
                GenerateArtifDataScale(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }

            if (crImgFromDrawObj.ArtificReflections.Any())
            {
                GenerateArtifDataReflection(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }
            if (crImgFromDrawObj.ArtificBlurrings.Any())
            {
                GenerateArtifDataBlurring(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }
            if (crImgFromDrawObj.ArtificAdditives.Any())
            {
                GenerateArtifDataAdditiveNoise(crImgFromDrawObj, image, rectangle, folderToSave, dataType);
            }*/
        }

   /*     /// <summary>
        /// Vrati obrazok pre nahlad
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rectangle"></param>
        /// <param name="typeTransform"></param>
        /// <param name="setting"></param>
        /// <param name="change">ak null tak sa zmena vygeneruje inak sa pouzije change ako zmena</param>
        /// <returns></returns>
        public Image<Bgr, Byte> GetImageForPreview(Image<Bgr, Byte> image, Rectangle rectangle, ETransformType typeTransform, SettingArtifTransfBase setting, ref int? change)
        {
            Random rand = new Random();
            if (typeTransform != ETransformType.Reflection && change == null)
            {
                var sett = setting as SettingArtifTransf4Params;
                if (sett != null)
                {
                    var count = sett.Count;
                    change = sett.GetRandomValue()[rand.Next(count)];
                }
            }

            switch (typeTransform)
            {
                case ETransformType.Move:
                    return GetArtifMoveImage(rectangle, image, true, null, new Size(), (int) change);
                case ETransformType.Rotate:
                    return GetArtifRotateImage(rectangle, image, true, null, new Size(), (int) change);
                case ETransformType.Scale:
                    return GetArtifScaleImage(rectangle, image, true, null, new Size(), (int) change);
                case ETransformType.Reflection:
                    change = null;
                    return GetArtifReflectionImage(rectangle, image, true, null, new Size(), (SettingArtifTransfRefl) setting);
                case ETransformType.Blurring:
                    return GetArtifBlurringImage(rectangle, image, true, null, new Size(), (int)change);
                case ETransformType.AdditiveNoise:
                    return GetArtifAdditiveNoiseImage(rectangle, image, true, null, new Size(), (int)change);
                case ETransformType.ImpulseNoise:
                    return GetArtifImpulseNoiseImage(rectangle, image, true, null, new Size(), (int)change);
            }
            change = null;
            return null;
        }*/
/*
        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou POSUNU objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataMove(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int j = 0; j < crImgFromDrObj.ArtificMoves.Count; j++)
            {
                var move = crImgFromDrObj.ArtificMoves[j];

                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Move" + j);
                else
                    folderToSave += "Move" + j + "\\";

                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                List<int> vectors = GetUniqueRandValue(move[0], move[1], move[2], ref move[3]);
                for (int i = 0; i < move[3]; i++)
                {
                    using(var img = GetArtifMoveImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), vectors[i]))
                        img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
        }


        private Image<Bgr, byte> GetArtifMoveImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, int vector)
        {
            Rectangle normRect = rectangle;
            
            //musim nahodne urcit posun v smere x a y tak aby som dostal zadany vektor.
            Random rand2 = new Random();
            int deltaX = rand2.Next(-vector, vector);
            int deltaY = (int)Math.Sqrt(Math.Pow(vector, 2) - Math.Pow(deltaX, 2));

            normRect.X += deltaX;
            normRect.Y += rand2.NextDouble() < 0.5 ? -deltaY : deltaY;

            //nanormalizujeme velkost rectanglu na normalizovany rectangle ak sa ma normalizovat
            if (!unnormalized)
                CreateNormalizeRectangle(ref normRect, scale);

            return unnormalized ? FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).Copy(normRect)
                    : new Image<Bgr, byte>(FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).Copy(normRect).ToBitmap(size.Width, size.Height));
        }

        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou ROTACIE objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataRotate(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int j = 0; j < crImgFromDrObj.ArtificRotates.Count; j++)
            {
                var rotate = crImgFromDrObj.ArtificRotates[j];

                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Rotate" + j);
                else
                    folderToSave += "Rotate" + j + "\\";

                if (!Directory.Exists(folderToSave))
                Directory.CreateDirectory(folderToSave);

                List<int> angles = GetUniqueRandValue(rotate[0], rotate[1], rotate[2], ref rotate[3]);
                for (int i = 0; i < rotate[3]; i++)
                {
                    using(var img = GetArtifRotateImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), angles[i]))
                        img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
        }

        private Image<Bgr, byte> GetArtifRotateImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, int angle)
        {
            //nanormalizujeme velkost rectanglu na normalizovany rectangle ak sa ma normalizovat
            Rectangle normRect = rectangle;
            if (!unnormalized)
                CreateNormalizeRectangle(ref normRect, scale);

            var centerPoint = new PointF(normRect.X + normRect.Width * 0.5f, normRect.Height * 0.5f);

            return unnormalized ? FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).
                                  Rotate(angle, centerPoint, INTER.CV_INTER_CUBIC, new Bgr(Color.White), true).Copy(normRect)
                                : new Image<Bgr, byte>(FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).
                                  Rotate(angle, centerPoint, INTER.CV_INTER_CUBIC, new Bgr(Color.White), true).
                                  Copy(normRect).ToBitmap(size.Width, size.Height));
        }

        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou ZMENA VELKOSTI objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataScale(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int j = 0; j < crImgFromDrObj.ArtificScales.Count; j++)
            {
                var scale = crImgFromDrObj.ArtificScales[j];

                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Scale" + j);
                else
                    folderToSave += "Scale" + j + "\\";

                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                List<int> deltaScale = GetUniqueRandValue(scale[0], scale[1], scale[2], ref scale[3]);
                for (int i = 0; i < scale[3]; i++)
                {
                    using(var img = GetArtifScaleImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), deltaScale[i]))
                        img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
        }

        private Image<Bgr, byte> GetArtifScaleImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, int deltaScale)
        {
            //nanormalizujeme velkost rectangle na normalizovany rectangle ak sa ma normalizovat
            Rectangle normRect = rectangle;
            if (!unnormalized)
                CreateNormalizeRectangle(ref normRect, scale);
            //avsak tato zmena je v percentach (ak < 1 tak zmesnenie ak > 1 tak zvacsenie je to SCALE FAKTOR) potrebujem previest na pixle, 
            //a ked chcem zvacsit BB tak x a y suradnice boundingboxu sa musia zmensit a naopak
            //ak je zmena > 1 tak chceme zvacsovat objekt v bb(cize roztahovat BB), inak zmensujeme cize pridame
            //pixle z okolia a nastavime na povodnu velkost
            int deltaX = (int)Math.Round(normRect.Width * (deltaScale/100.0)) - normRect.Width;
            int deltaY = (int)Math.Round(normRect.Height * (deltaScale/100.0)) - normRect.Height;
            normRect.X += deltaX / 2;
            normRect.Y += deltaY / 2;
            normRect.Width += -deltaX;
            normRect.Height += -deltaY;

            return unnormalized
                        ? new Image<Bgr, byte>(FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).Copy(normRect).ToBitmap(rectangle.Width, rectangle.Height))
                        : new Image<Bgr, byte>(FillImageToRect(image, ref normRect, new Point(image.Width, image.Height)).Copy(normRect).ToBitmap(size.Width, size.Height));
        }

        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou ZRKADLENIE objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataReflection(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int i = 0; i < crImgFromDrObj.ArtificReflections.Count; i++)
            {
                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Reflection" + i);
                else
                    folderToSave += "Reflection" + i + "\\";

                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                using(var img = GetArtifReflectionImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), crImgFromDrObj.ArtificReflections[i]))
                    img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
            }
        }

        private Image<Bgr, byte> GetArtifReflectionImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, SettingArtifTransfRefl reflection)
        {
            //nanormalizujeme velkost BB na normalizovany rectangle ak sa ma normalizovat
            using (Image<Bgr, Byte> pomImage = unnormalized ? GetImageUnnormRect(rectangle, image) : GetImageNormRect(rectangle, image, size, scale))
            {
                if (reflection.Horizontal && reflection.Vertical) return pomImage.Flip(FLIP.HORIZONTAL).Flip(FLIP.VERTICAL);
                if (reflection.Horizontal)
                    return pomImage.Flip(FLIP.HORIZONTAL);
                if (reflection.Vertical)
                    return pomImage.Flip(FLIP.VERTICAL);
                return pomImage;
            }
        }

        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou pridania ADITIVNEHO SUMU do objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataAdditiveNoise(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int j = 0; j < crImgFromDrObj.ArtificAdditives.Count; j++)
            {
                var rotate = crImgFromDrObj.ArtificAdditives[j];

                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Additive" + j);
                else
                    folderToSave += "Additive" + j + "\\";

                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                List<int> standDevs = GetUniqueRandValue(rotate[0], rotate[1], rotate[2], ref rotate[3]);
                for (int i = 0; i < rotate[3]; i++)
                {
                    using (var img = GetArtifAdditiveNoiseImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), standDevs[i]))
                        img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
        }

        private Image<Bgr, byte> GetArtifAdditiveNoiseImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, int standDev)
        {
            //nanormalizujeme velkost rectangle na normalizovany rectangle ak sa ma normalizovat
            Rectangle normRect = rectangle;
            if (!unnormalized)
                CreateNormalizeRectangle(ref normRect, scale);

            _rand = new Random((int)DateTime.Now.Ticks);
            _standDev = standDev;
            var pomImage = unnormalized
                               ? GetImageUnnormRect(rectangle, image)
                               : GetImageNormRect(rectangle, image, size, scale);
            
            for (int i = 0; i < pomImage.Height; i++)
            {
                for (int j = 0; j < pomImage.Width; j++)
                {
                    var r = GetGaussRand();
                    pomImage[i, j] = new Bgr(pomImage[i, j].Blue + r, pomImage[i, j].Green + r, pomImage[i, j].Red + r);
                }
            }
            return pomImage;
            
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


        private Image<Bgr, byte> GetArtifImpulseNoiseImage(Rectangle rectangle, Image<Bgr, byte> image, bool unnormalized, List<int> scale, Size size, int max)
        {
            //nanormalizujeme velkost rectangle na normalizovany rectangle ak sa ma normalizovat
            var rnd = new Random((int)DateTime.Now.Ticks);
            var pomImage = unnormalized
                               ? GetImageUnnormRect(rectangle, image)
                               : GetImageNormRect(rectangle, image, size, scale);
            
            for (int i = 0; i < pomImage.Height; i++)
            {
                for (int j = 0; j < pomImage.Width; j++)
                {
                    int num = rnd.Next(max + 1);
                    if (num == 0)
                        pomImage[i, j] = new Bgr(Color.White);
                    else if (num == max)
                        pomImage[i, j] = new Bgr(Color.Black);
                }
            }
            return pomImage;
        }

        /// <summary>
        /// Generovanie umelych dat BB,PL s volbou ROZMAZANIA objektu v danom rectangle.
        /// </summary>
        /// <param name="crImgFromDrObj"></param>
        /// <param name="image">obrazok na ktorom je boundingbox</param>
        /// <param name="rectangle">rectangle ktory sa spracuva</param>
        /// <param name="folderToSave"></param>
        /// <param name="dataType">urcuje ake data sa idu generovat ak -1 tak chyba inak trenovacie = 0, validacne = 1, testovacie = 2</param>
        private void GenerateArtifDataBlurring(CreateImageFromDrawObj crImgFromDrObj, Image<Bgr, byte> image, Rectangle rectangle, string folderToSave, int dataType)
        {
            for (int j = 0; j < crImgFromDrObj.ArtificBlurrings.Count; j++)
            {
                var rotate = crImgFromDrObj.ArtificBlurrings[j];

                if (!crImgFromDrObj.CreateFolderStruct)
                    folderToSave = folderToSave.Replace("{\"FOLDER2\"}", "Blurring" + j);
                else
                    folderToSave += "Blurring" + j + "\\";

                if (!Directory.Exists(folderToSave))
                    Directory.CreateDirectory(folderToSave);

                List<int> kenelSizes = GetUniqueRandValue(rotate[0], rotate[1], rotate[2], ref rotate[3]);
                for (int i = 0; i < rotate[3]; i++)
                {
                    using (var img = GetArtifBlurringImage(rectangle, image, crImgFromDrObj.Unnormalized(), crImgFromDrObj.GetScale(), crImgFromDrObj.GetSize(), kenelSizes[i]))
                        img.Save(folderToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    if (dataType > -1 && dataType < 3) _countArtifData[dataType]++;//zvysime pocet vygenerovanych umelych obr. pre tuto skupinu
                }
            }
        }

        private Image<Bgr, byte> GetArtifBlurringImage(Rectangle rectangle, Image<Bgr, byte> image,
                                                           bool unnormalized, List<int> scale, Size size, int kenelSize)
        {
            //nanormalizujeme velkost rectangle na normalizovany rectangle ak sa ma normalizovat
            using (var pomImage = unnormalized
                               ? GetImageUnnormRect(rectangle, image)
                               : GetImageNormRect(rectangle, image, size, scale))
                return pomImage.SmoothGaussian(kenelSize, kenelSize, 0, 0);
        }

        */

        /// <summary>
        /// Vrati jeden z adresarov (trenovacia, validacna, testovacia) podla nahodneho cisla.
        /// </summary>
        /// <param name="percentDistr"></param>
        /// <returns></returns>
        private MyDistrType GetDirectoryToSave(SortedList<int, MyDistrType> percentDistr)
        {
            int random = _rnd.Next(1, 101);
            double paramP = 0;
            for (int i = 0; i < percentDistr.Count; i++)
            {
                if (random > paramP && random <= (percentDistr.Keys[i] + paramP))
                {
                    return percentDistr.Values[i];
                }
                paramP = paramP + percentDistr.Keys[i];
            }
            return null;
        }

        /// <summary>
        /// Vrati true ak je BB v poriadku inak false.
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        private bool CheckBBLocationAndSize(BoundingBox bb, Image<Bgr, byte> image)
        {
            //hranicny bod - pravy dolny roh obrazku
            Point maxCorner = new Point(image.Width, image.Height);
            
            //kontrola rozmerov BB, ci nevychadza BB z obrazku, ak ano tak oprava jeho rozmerov
            if (bb.PointA.X < 0) bb.PointA = new Point(0, bb.PointA.Y);
            if (bb.PointA.Y < 0) bb.PointA = new Point(bb.PointA.X, 0);
            if (bb.PointB.X > maxCorner.X) bb.PointB = new Point(maxCorner.X, bb.PointB.Y);
            if (bb.PointB.Y > maxCorner.Y) bb.PointB = new Point(bb.PointB.X, maxCorner.Y);

            //kontrola ci BB nie je len bod(jeho width alebo height == 0)
            if (bb.Size.Width == 0 || bb.Size.Height == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vytvorenie vysledneho obrazku, ktory bude reprezentovat nenormalizovany rectangle na danom obrazku
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image<Bgr, Byte> GetImageUnnormRect(Rectangle rectangle, Image<Bgr, byte> image)
        {
            return image.Copy(rectangle);
        }

        /// <summary>
        /// Vytvorenie vysledneho obrazku ktory bude reprezentovat normalizovany BB na danom obrazku.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="image"></param>
        /// <param name="size">normalizovana velkost</param>
        /// <param name="scale">normalizovany pomer stran aky maju mat BB</param>
        private Image<Bgr, Byte> GetImageNormRect(Rectangle rectangle, Image<Bgr, Byte> image, Size size, List<int> scale)
        {
            CreateNormalizeRectangle(ref rectangle, scale);

            //zvacsenie obrazku pridanim bielej plochy tak aby BB bol v obrazku a nasledne zmenit velkost rectangle na normalizovanu, 
            //pomer je normalizovany tak staci zvacsit velkost obrazku na normalizovanu
            return
                new Image<Bgr, byte>(
                    FillImageToRect(image, ref rectangle, new Point(image.Bitmap.Width, image.Bitmap.Height))
                        .Copy(rectangle).ToBitmap(size.Width, size.Height));
        }

        /// <summary>
        /// Zmenit velkost rectanglu na normalizovanu velkost, podla zadaneho pomeru, avsak jeho suradnice
        /// mozu byt aj zaporne alebo vacsie ako obrazok vtedy ak sa nachadza niekde v blizkosti okrajov obrazku
        /// </summary>
        /// <param name="normalRect">rectangel ktory treba znormalizovat</param>
        /// <param name="scale">normalizovany pomer stran aky maju mat rectangel</param>
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
        /// Doplnenie obrazku pozadim tak aby bol rectangle v obrazku - zvacsenie obrazku pridanim bielej plochy tak aby rectangle bol v obrazku
        /// a vratenie zmeneneho obrazku(s pridanymi plochami ak to bolo potrebne)
        /// </summary>
        /// <param name="image">obrazok ktory sa ma zmenit</param>
        /// <param name="normalRect">rectangle podla ktoreho sa ma obrazok zmenit v pripade ze ma zaporne suradnice, alebo je vacsi ako obrazok</param>
        /// <param name="maxCorner">hranicny bod - pravy dolny roh obrazku, na zistenie ci BB nevycnieva napravo alebo dole z obrazku</param>
        /// <returns></returns>
        private Image<Bgr, byte> FillImageToRect(Image<Bgr, byte> image, ref Rectangle normalRect, Point maxCorner)
        {
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
                using(var img = new Image<Bgr, byte>(deltaX, maxCorner.Y, new Bgr(255, 255, 255)))
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
        /// Generovanie negatívnych treningovych dat zo vsetkych obrazkov v projekte.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="dirToSave"></param>
        public void GenerateNegativeTrainingData(ProgressForm progress, int width, int height, string dirToSave)
        {
            int generated = 0;
            progress.Count = _openImages.Count;
            progress.Actuall = 0;

            Rectangle vyrez = new Rectangle(new Point(0,0), new Size(width,height));
            if (!Directory.Exists(dirToSave)) Directory.CreateDirectory(dirToSave);
            foreach (BaseFigure image in _openImages)
            {
                generated += GenerateNegativeTrainingData(image,vyrez,dirToSave);
                progress.Actuall++;
                progress.SetStatus("Generated " + generated + " images");
                //kontrola ci nebol priebeh nacitavania zruseny
                if (progress.IsCanceled())
                {
                    progress.MyClosed();
                    return;
                }
            }
        }

        /// <summary>
        /// Vytvaranie negatívnych treningovych dat, ktore su vyrezavane z obrazkou tak aby
        /// neprekryvali oznaceny BB na tomto obrazku.
        /// </summary>
        /// <param name="paImage"></param>
        /// <param name="vyrez"></param>
        /// <param name="dirToSave"></param>
        private int GenerateNegativeTrainingData(BaseFigure paImage, Rectangle vyrez, string dirToSave)
        {
            int num = 0;
            try
            {
                using (Image<Bgr, byte> image = paImage.GetImage())
                {
                    int w = image.Bitmap.Width, h = image.Bitmap.Height;
                    if (vyrez.Width > w || vyrez.Height > h) return 0; //ak je vyrez vacsi ako obrazok tak koniec

                    //inak zacneme vyrezavat
                    while (true)
                    {
                        if (vyrez.X + vyrez.Width <= w)
                        {
                            if (vyrez.Y + vyrez.Height <= h)
                            {
                                bool overlap = paImage.BoundBoxes.Any(bb => bb.GetRectangle().IntersectsWith(vyrez)); //ci sa vyrez prekryva s BB
                                if (!overlap)
                                    overlap = paImage.Paintings.Any(pa => pa.BoundingBox.GetRectangle().IntersectsWith(vyrez));
                                if (!overlap)
                                    {
                                        image.Copy(vyrez).Save(dirToSave + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                                        num++;
                                    }
                                vyrez.X += vyrez.Width;
                            }
                            else if (vyrez.Y + vyrez.Height > h)
                            {
                                break;
                            }
                        }
                        else if (vyrez.X + vyrez.Width > w)
                        {
                            if (vyrez.Y + 2*(vyrez.Height) <= h) // ci sa mi oplati posunut o riadok nizsie
                            {
                                vyrez.X = 0;
                                vyrez.Y += vyrez.Height;
                            }
                            else if (vyrez.Y + 2*(vyrez.Height) > h)
                                //neoplati sa posuvat o riadok nizsie nebude sa dat nic vyrezat, koniec
                            {
                                break;
                            }
                        }
                    }
                }
                return num;
            }
            catch (Exception exc)
            {
                return num;
            }
        }

        /// <summary>
        /// Generovanie dat pre vsetky polylines v projekte
        /// </summary>
        public void GenerateDataForPolylines(ProgressForm progress, CreateImageFromDrawObj crImgFrmPL/*, Graphics gr*/)
        {
            //_grph = gr;
            _countArtifData = new[] { 0, 0, 0 };
            _countRealData = new[] { 0, 0, 0 };
            int count = _openImages.Sum(image => image.Polylines.Count);
            progress.Count = count;
            progress.Actuall = 0;
            string errorImages = "";

            for (int k = 0; k < _openImages.Count; k++)
            {
                foreach (Polyline polyline in _openImages[k].Polylines)
                {
                    //kontrola ci nebol priebeh nacitavania zruseny
                    if (progress.IsCanceled())
                    {
                        CreateLogFile(crImgFrmPL);
                        progress.MyClosed();
                        //Zrusime form CreateImageFromDrawObj
                        crImgFrmPL.MyClosed();
                        return;
                    }

                    if (polyline.Points.Count < 2) continue;

                    //ak nema def. triedu budu ulozene data pre tuto polyline do zlozky unclasified
                    if (string.IsNullOrEmpty(polyline.Properties.Class))
                    {
                        errorImages += (k + 1) + ",";
                    }

                    using (Image<Bgr, byte> imageOrig = _openImages[k].GetImage())
                    {
                        SaveDataForPolyline(imageOrig, polyline, crImgFrmPL.GetRectangle(), crImgFrmPL.StepLength, crImgFrmPL);
                    }
                    progress.Actuall++;
                }
            }
            if (errorImages != "")
            {
                string message = ((errorImages == "") ? "" : ("Polyline na obrázku číslo: " + errorImages + " nemá definovanú triedu," +
                                         " \ndáta spojené s touto polyline budú uložené do zložky:\n" + crImgFrmPL.FolderToSave + "unclassified\\."));
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CreateLogFile(crImgFrmPL);
            crImgFrmPL.MyClosed();
        }

        /// <summary>
        /// Ulozenie dat konkretnej polyline
        /// </summary>
        /// <param name="image"></param>
        /// <param name="polyline"></param>
        /// <param name="rectangle"></param>
        /// <param name="step"></param>
        /// <param name="crImgFrmPL"></param>
        private void SaveDataForPolyline(Image<Bgr, byte> image, Polyline polyline,  Rectangle rectangle, int step, CreateImageFromDrawObj crImgFrmPL)
        {
            int rest = 0; //predstavuje dlzku ktora zostala z ciary tvoriacu polyline po vygenerovani posledneho obdlznika ktory sa este
                          //vosiel na tuto ciaru. Potrebne pre to aby sme vedeli kde zacat s vyrezavanim na nasledujucej ciary polylineu
            var xAxis = new LineSegment2D(new Point(0, 0), new Point(100, 0)); //x-ova os

            for (int i = 0; i < polyline.Points.Count - 1; i++)
            {
                Point firstPoint = polyline.Points[i]; //nastavim rectangle na prvy bod ciary
                LineSegment2D line = new LineSegment2D(polyline.Points[i], polyline.Points[i + 1]);

                if (line.Length + rest < step)
                {
                    rest += (int) line.Length;
                    if (i == polyline.Points.Count - 2)//sme na poslednej ciare a uz sa na nu nevojde rectangle, tak spravime rectangle na konci ciary
                    {
                        rectangle.Location = polyline.Points.Last();
                        CutRectangleFromPolyline(polyline, image, rectangle, crImgFrmPL);
                        return;
                    }
                    continue;
                }
                //potrebujeme vypocitat zmeny delta x,y pre posun rectangle o dany krok, angleRadians predstavuje uhol aky zviera ciara s x-ovou osou
                double angleRadians = Math.PI * Math.Abs(line.GetExteriorAngleDegree(xAxis)) / 180.0;
                double deltaX = (line.P1.X > line.P2.X ? -1 : 1) * Math.Abs(Math.Cos(angleRadians) * step);
                double deltaY = (line.P1.Y > line.P2.Y ? -1 : 1) * Math.Abs(Math.Sin(angleRadians) * step);

                int totalDistance = i == 0 ? -step : -rest; //celkova vzdialenost ktora bola spracovana z danej ciary tvoriacej polyline-u
                                                            //pri prvej ciare polyline-u davame -step lebo prvy ractangle sa umiestni na zacaitok ciary cize sa nic neprejde
                int count = totalDistance == 0 ? 1 : 0; // pocet krokov ktore sme spravili na danej ciare
                                                        // ak sa nachadzame medzi dvoma ciarami polyline-u, a stred posledneho rectangle z prvej ciary
                                                        // padol na spojnicu tychto dvoch ciar, potom aj stred prveho rectangle z druhej ciary padne na spojnicu tychto ciar
                                                        // cize count dame na 1 lebo už sme akoby na tejto druhej ciare vygenerovali jeden rectangle

                while (totalDistance + step <= line.Length)
                {
                    if (rest != 0) // nie je ziadny zvyšok z predchadzajzcej ciary
                    {
                        //vykona sa maximalne v prvkom kroku na kazdej ciare polyline v pripade ze ostal zvysok z predchadzajucej ciary polyline-u
                        double firstStep = 1 - (rest / (double)step); // predstavuje kolko percent z dlzky kroku sa spravi ak bol nejaky zostatok z predchadzajucej ciary
                        firstPoint = new Point((int)Math.Round(polyline.Points[i].X + firstStep * deltaX),
                                               (int)Math.Round(polyline.Points[i].Y + firstStep * deltaY));
                        rectangle.Location = firstPoint;
                        rest = 0;
                    }
                    else
                        rectangle.Location = new Point((int)Math.Round(firstPoint.X + deltaX * count), (int)Math.Round(firstPoint.Y + deltaY * count));
                    
                    totalDistance += step;
                    CutRectangleFromPolyline(polyline, image, rectangle, crImgFrmPL);
                    count++;
                }
                rest = (int) (line.Length - totalDistance);
            }
        }

        /// <summary>
        /// Vyrezeme rectangle z daneho obrazka pre PL
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="image"></param>
        /// <param name="rectangle"></param>
        /// <param name="crImgFrmPL"></param>
        private void CutRectangleFromPolyline(Polyline polyline, Image<Bgr, byte> image, Rectangle rectangle, CreateImageFromDrawObj crImgFrmPL )
        {
            //najskor musime posunut rectangle tak aby mal stred v bode kde ma teraz lavy horny roh
            Rectangle rec = new Rectangle(rectangle.Location,rectangle.Size);
            rec.X -= rec.Width / 2;
            rec.Y -= rec.Height / 2;

            //todo: save to training, validation, testing xml
            int dateType;
            SaveDrawObjToImage(out dateType, polyline, rec, image, crImgFrmPL);
        }
    }
}
