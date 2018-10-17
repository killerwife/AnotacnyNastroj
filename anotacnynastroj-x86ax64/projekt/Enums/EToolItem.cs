namespace Projekt.Enums
{
    /// <summary>
    /// Zoznam vsetkych nastrojov na paneli nastrojov.
    /// </summary>
    public enum EToolItem
    {
        /// <summary>
        /// ziadny nastroj
        /// </summary>
        None = -1,

        /// <summary>
        /// nastroj painting
        /// </summary>
        Painting = 0,

        /// <summary>
        /// nastroj boundingbox
        /// </summary>
        BoundingBox = 1,

        /// <summary>
        /// nastroj polyline
        /// </summary>
        Polyline = 2,

        /// <summary>
        /// nastroj cursor
        /// </summary>
        Cursor = 3,

        /// <summary>
        /// nastroj delete
        /// </summary>
        Delete = 4,

        /// <summary>
        /// nastroj find
        /// </summary>
        Find = 5
    }
}