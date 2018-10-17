using System.ComponentModel;

namespace Projekt.Enums
{
    /// <summary>
    /// Reprezentuje typ ako sa maju usporiadat adresare
    /// pri generovani umelych a realnych dat.
    /// </summary>
    public enum ETypeOfSortDir
    {
        /// <summary>
        /// Real/Artificial Folder As Last true
        /// </summary>
        [Description("Real/Artificial Folder As Last")]
        RealArtifFoldAsLast = 0, 
        
        /// <summary>
        /// Real/Artificial Folder As First
        /// </summary>
        [Description("Real/Artificial Folder As First")]
        RealArtifFoldAsFirst = 1
    }
}