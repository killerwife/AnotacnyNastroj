using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Projekt.DrawObjects;
using Projekt.Enums;
using Projekt.Figure;
using Projekt.Forms;

namespace Projekt.ExportImport
{
    class ImportExportCSV :ImportExport
    {
        private readonly EImportExportFormat _csvFormat;
        private readonly string _classBB;
        private readonly string _attrForClassId;

        /// <summary>
        /// Konstruktor - vyuzity pre export
        /// </summary>
        /// <param name="csvFormat">konkretny csv format</param>
        /// <param name="openImages">vsetky obrazky projektu</param>
        /// <param name="projectFolder">zlozka projektu</param>
        public ImportExportCSV(EImportExportFormat csvFormat, List<BaseFigure> openImages, string projectFolder, bool xmlType) : base(openImages, projectFolder, xmlType)
        {
            _csvFormat = csvFormat;
        }

        /// <summary>
        /// Konstruktor - vyuzity pre import
        /// </summary>
        /// <param name="file">subor na importovanie</param>
        /// <param name="csvFormat">konketny csv format</param>
        /// <param name="classBB"> nazov triedy, ktora sa ma priradit nacitanim BB, ak vrati -1 tak 
        /// nazov triedy nacitame z csv suboru, ktoru prestavuje hodnota classId v csv suboru</param>
        /// <param name="attrForClassId">nazov atributu ktremu sa pridadi classID z csv suboru v pripade ze classBB sa nerovna -1
        ///                              pretoze ak classBB je -1 tak tento parameter predstavuje nazov triedy pre nacitavany objekt</param>
        public ImportExportCSV(string file, EImportExportFormat csvFormat, string classBB, string attrForClassId)
            : base(file)
        {
            _csvFormat = csvFormat;
            _classBB = classBB;
            _attrForClassId = attrForClassId;
        }

        public override bool ExportProject(SetNameOutputFile options)
        {
            int dontSave;//pocet BB ktore sa neulozili, z dovodu ze classId mal byt niektory z atributov BB no dany BB nemal vyplneny tento atribut
            if (SaveProjectToCSV(options.GetOutputName(), options.GetClassToSave(), options.GetClassID(), out dontSave))
            {
                MessageBox.Show("Ukladanie ukončené" +
                                (dontSave > 0 ? (". " + dontSave + " boundingBoxy, neboli uložené, pretože classID reprezentoval atribút, ktorý tieto boundingBoxy nemali vyplnený.") : " úspešne."),
                                "Upozornenie", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        public override int GetImpType()
        {
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameCsv"></param>
        /// <param name="className">trieda ktora sa ma ulozit ak ak className = -1 tak sa budu ukladat vsetky triedy</param>
        /// <param name="classId">nazov atributu ktoreho hodnota bude reprezentovat danu triedu. Ak je vybrana konkretne trieda 
        /// (GetClassTuSave != -1) pre ukladanie tak classid bude predstavovat nazov atributu ktoreho hodnota bude reprezentovat danu triedu</param>
        /// <param name="dontSave">pocet BB ktore sa naulozili, z dovodu ze classId mal byt niektory z atributov BB no dany BB nemal vyplneny tento atribut</param>
        /// <returns>true ak ok inak false</returns>
        private bool SaveProjectToCSV(string nameCsv, string className, string classId, out int dontSave)
        {
            //List<BaseFigure> images = OpenImages.Where(image => typeof (ImageFigure) == image.GetType()).ToList();

            string last = ProjectFolder.Split(new[] { '/', '\\' }).Last();
            string projFolder = ProjectFolder.Substring(0, ProjectFolder.Count() - last.Count());

            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(projFolder + nameCsv, FileMode.Create);
                sw = new StreamWriter(fs);
                dontSave = OpenImages.Sum(figure => ExportFigureToCSV(sw, figure, className, classId));
            }
            catch (Exception exc)
            {
                dontSave = 0;
                throw new Exception(exc.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            return true;
        }

        /// <summary>
        /// Vytvorenie časti CSV súboru, ktorá bude reprezentovať obrázok a objekty na ňom.
        /// </summary>
        /// <param name="sw">streamWriter pouzity pre vytvorenie daneho dokumentu</param>
        /// <param name="figure">obrazok snimka videa ktora sa ma ulozit do suboru</param>
        /// <param name="className">trieda ktora sa ma ulozit ak ak className = -1 tak sa budu ukladat vsetky triedy</param>
        /// <param name="classId">nazov atributu ktoreho hodnota bude reprezentovat danu triedu. Ak je vybrana konkretne trieda 
        ///     (GetClassTuSave != -1) pre ukladanie tak classid bude predstavovat nazov atributu ktoreho hodnota bude reprezentovat danu triedu</param>
        /// <returns>vracia pocet BB ktore sa naulozili, z dovodu ze classId mal byt niektory z atributov BB
        /// no dany BB nemal vyplneny tento atribut</returns>
        private int ExportFigureToCSV(StreamWriter sw, BaseFigure figure, string className, string classId)
        {
            int dontSave = 0;
            try
            {
                foreach (BoundingBox bb in figure.BoundBoxes)
                {
                    if (!ExportBBToCsv(sw, figure.Source, className, classId, bb)) dontSave++;
                }

                foreach (var painting in figure.Paintings)
                {
                    var bb = painting.BoundingBox;
                    bb.Properties = painting.Properties;
                    if (!ExportBBToCsv(sw, figure.Source, className, classId, bb)) dontSave++;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Chyba pri exportovani do CSV." + ex.Message, "Error-ImageFigure.ExportCSV()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dontSave;
        }

        private bool ExportBBToCsv(StreamWriter sw, string figSource, string className, string classId, BoundingBox bb)
        {
            if (className.Equals("-1")) // ak -1 tak sa ukladaju vsetky triedy pricom classID je nazov triedy
            {
                WriteBBToFile(sw, figSource, bb, bb.Properties.Class);
                return true;
            }
            if (!bb.Properties.Class.Equals(className)) return false;// ak bb nie je triedy aku urcuje className tak neukladame tento bb
            for (int i = 0; i < bb.Properties.AtributesName.Length; i++)
            {
                string atr = bb.Properties.AtributesName[i];
                if (!atr.Equals(classId))
                {
                    if (i == bb.Properties.AtributesName.Length - 1) //dany BB nema atribut s danym nazvom
                        return false;
                    continue;
                }
                if (String.IsNullOrEmpty(bb.Properties.AtributesValue[i])) //dany BB nema vyplnenu hodnotu atributu
                    return false;
                WriteBBToFile(sw, figSource, bb, bb.Properties.AtributesValue[i]);
                return true;
            }
            return false;
        }

        private void WriteBBToFile(StreamWriter sw, string figSource, BoundingBox bb, string classId)
        {
            switch (_csvFormat)
            {
             case EImportExportFormat.Csv2Point:
                    sw.WriteLine(figSource + ";" + bb.PointA.X + ";" + bb.PointA.Y + ";" + bb.PointB.X + ";" +
                             bb.PointB.Y + ";" + classId);
                    break;
             case EImportExportFormat.CsvPointSize:
                    sw.WriteLine(figSource + ";" + bb.PointA.X + ";" + bb.PointA.Y + ";" + bb.Size.Width + ";" +
                             bb.Size.Height + ";" + classId);
                    break;
            }
        }

        public override bool ImportProject(ref List<string> imagesSrc, ref List<List<string[]>> imagesAttrs, ref List<List<DrawObject>> imagesDrawObjects, ref List<string> videosSrc,
                                   ref List<List<List<string[]>>> framesAttrs, ref List<List<List<DrawObject>>> framesDrawObjects)
        {
            return LoadProjectFromCSV(ref imagesSrc, ref imagesDrawObjects);
        }

        /// <summary>
        /// Nacitanie vsetkych BB z CSV suboru.
        /// </summary>
        /// <param name="imagesSrc">zoznam ciest k obrazkom</param>
        /// <param name="imagesBBs">zoznamy BBs jednotlivych obrazkov</param>
        /// <returns></returns>
        private bool LoadProjectFromCSV(ref List<string> imagesSrc, ref List<List<DrawObject>> imagesBBs)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(FileToLoad, FileMode.Open);
                sr = new StreamReader(fs);
                var bbs = new List<DrawObject>();
                ObjectProperties op;
                BoundingBox bb;
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!String.IsNullOrEmpty(line))
                    {
                        string[] param = line.Split(new[] {';'});

                        op = _classBB == "-1"
                                 ? new ObjectProperties(new string[] {}, new string[] {}, param[5])
                                 : new ObjectProperties(new[] {_attrForClassId}, new string[] {param[5]}, _classBB);

                        bb = LoadBBFromFile(param, op, ref i);

                        if (!imagesSrc.Any())
                        {
                            bbs.Add(bb);
                            imagesSrc.Add(param[0]);
                        }
                        else
                        {
                            if (imagesSrc.Last() == param[0])
                                bbs.Add(bb);
                            else
                            {
                                imagesBBs.Add(bbs.GetRange(0, bbs.Count));
                                bbs.Clear();
                                i = 0;
                                bb.ID = i++;
                                bbs.Add(bb);
                                imagesSrc.Add(param[0]);
                            }
                        }
                    }
                }

                imagesBBs.Add(bbs);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri nacitavani csv:" + ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if(sr != null)sr.Close();
                if(fs != null)fs.Close();
            }
            return true;
        }

        private BoundingBox LoadBBFromFile(string[] param, ObjectProperties op, ref int i)
        {
            BoundingBox bb = new BoundingBox(i++);
            bb.PointA = new Point(Int32.Parse(param[1]), Int32.Parse(param[2]));
            switch (_csvFormat)
            {
                case EImportExportFormat.Csv2Point:
                    bb.PointB = new Point(Int32.Parse(param[3]), Int32.Parse(param[4]));
                    break;
                case EImportExportFormat.CsvPointSize:
                    bb.PointB = new Point(bb.PointA.X + Int32.Parse(param[3]), bb.PointA.Y + Int32.Parse(param[4]));
                    break;
            }
            bb.Properties = op;
            return bb;
        }
    }
}
