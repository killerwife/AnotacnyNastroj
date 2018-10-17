using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Projekt.Interfaces
{
    /// <summary>
    /// rozhranie ktore implementuje Projekt. Obsahuje metody potrebne pre pristup k obrazkom
    /// ako aj metody pre aplikovanie zmien na obrazky
    /// </summary>
    public interface IExternalUseProject
    { 
        /// <summary>
        /// Vrati zoznam absolutnych ciest ku vsetkych obrazkov v projekte.
        /// </summary>
        /// <returns>zoznam absolutnych ciest ku vsetkych obrazkov v projekte</returns>
        List<string> GetAllProjectImages();

        /// <summary>
        /// Ako parameter dostane zoznam struktur reprezentujucich boundingBoxy pre kazdy obrazok. (Preto "List Listov").
        /// Kazdy obrazok ma zoznam struktur boundingBoxov. Zoznam obrazkov bude spracovavany v poradi v akom bol
        /// poskytnuty metodou GetAllProjectImages. V pripade ze pre obrazok nebol najdeny
        /// rectangle tak tento zoznam je null alebo Empty.
        /// Na zaklade tohto zoznamu prida pre jednotlive obrazky boundingboxy. 
        /// </summary>
        /// <param name="rectanglesOfImages">zoznam struktur reprezentujucich boundingBoxy pre kazdy obrazok</param>
        void AddBoundingBoxesToImages(List<List<BoundingBoxStructure>> rectanglesOfImages);
    }

    /// <summary>
    /// Struktura reprezentujuca boundingboxy
    /// </summary>
    public class BoundingBoxStructure
    {
        /// <summary>
        /// Stvoruholnik reprezentujuci boundingBox.
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Nazov triedy do ktorej bude zaradeny boundingBox.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Konstruktor pre zadefinovanie struktury reprezentujucej boundingbox.
        /// </summary>
        /// <param name="rectangle">Stvoruholnik reprezentujuci boundingBox.</param>
        /// <param name="className">Nazov triedy do ktorej bude zaradeny boundingBox.</param>
        public BoundingBoxStructure(Rectangle rectangle, string className)
        {
            Rectangle = rectangle;
            ClassName = className;
        }
    }
}