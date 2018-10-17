using System.Windows.Forms;
using Projekt.Figure;

namespace Projekt
{
    /// <summary>
    /// Panel reprezentujúci miniatútu obrazku.
    /// </summary>
    class ThumbPanel : Panel
    {
        /// <summary>
        /// Obrazok zobrazeny na panely.
        /// </summary>
        public BaseFigure Image { get; set; }

        /// <summary>
        /// Konštruktor.
        /// </summary>
        /// <param name="image">obrazok na panely.</param>
        public ThumbPanel(BaseFigure image)
        {
            Image = image;
        }
    }
}
