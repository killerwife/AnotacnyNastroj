namespace Projekt.Enums
{
    /// <summary>
    /// Enum reprezentujuci formaty pre import a export
    /// </summary>
    public enum EImportExportFormat
    {
        /// <summary>
        /// vlastny formát anotacneho nastroja
        /// </summary>
        Xml = 0,

        /// <summary>
        /// format: image_path;left_top_x;left_top_y; right_bottom_x;right_bottom_y;classid
        /// </summary>
        Csv2Point = 1,

        /// <summary>
        /// format: image_path;left_top_x;left_top_y;width;height;classid
        /// </summary>
        CsvPointSize = 2,

        Xml2 = 3
    }
}