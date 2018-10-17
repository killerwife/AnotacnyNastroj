using System.Collections.Generic;
using Projekt.DrawObjects;
using Projekt.Figure;
using Projekt.Forms;

namespace Projekt.ExportImport
{
    abstract class ImportExport
    {
        protected string FileToLoad { get; set; }
        protected List<BaseFigure> OpenImages;
        protected string ProjectFolder;
        protected bool Xml2;

        /// <summary>
        /// konstruktor - vyuzivany pri ukladani do suboru xml.
        /// </summary>
        /// <param name="openImages"></param>
        /// <param name="projectFolder"></param>
        protected ImportExport(List<BaseFigure> openImages, string projectFolder, bool xmlType)
        {
            OpenImages = openImages;
            ProjectFolder = projectFolder;
            FileToLoad = null;
            Xml2 = xmlType;
        }

        /// <summary>
        /// konstruktor - vyuzivany pri nacitavani zo suboru xml.
        /// </summary>
        /// <param name="fileToLoad">subor na nacitanie</param>
        protected ImportExport(string fileToLoad)
        {
            FileToLoad = fileToLoad;
            OpenImages = null;
            ProjectFolder = null;
            Xml2 = false;
        }

        public abstract int GetImpType();
        /// <summary>
        /// ulozenie projektu do suboru podla nastaveni
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract bool ExportProject(SetNameOutputFile options);
        
        /// <summary>
        /// nacitanie projektu zo suboru podla nastaveni
        /// </summary>
        /// <param name="imagesSrc">relativne cesty k obrazkom</param>
        /// <param name="imagesAttrs">atributy na urovni obrazka</param>
        /// <param name="imagesDrawObjects">objekty na obrazkoch</param>
        /// <param name="videosSrc">relativne cesty k snimkam videa</param>
        /// <param name="framesAttrs">atributy na urovni snimok videi</param>
        /// <param name="framesDrawObjects">objekty na jednotlivich snimkach videi</param>
        /// <returns></returns>
        public abstract bool ImportProject(ref List<string> imagesSrc, ref List<List<string[]>> imagesAttrs, ref List<List<DrawObject>> imagesDrawObjects, ref List<string> videosSrc, ref List<List<List<string[]>>> framesAttrs, ref List<List<List<DrawObject>>> framesDrawObjects);
    }
}
