using Projekt.Enums;
using Projekt.SettingOfTransformation;

namespace Projekt.Interfaces
{

    /// <summary>
    /// Rozhranie ktore musi implementovat kazdy UserControl pre transformacie
    /// </summary>
    public interface IBaseUseControlArtifSeting
    {
        /// <summary>
        /// Typ transformacie, vid. ETransformType
        /// </summary>
        ETransformType TypeTransformation { get; }

        /// <summary>
        /// Nastavenie transforamcie
        /// </summary>
        /// <param name="setting">nastavenia</param>
        /// <returns>true ak ok inak false</returns>
        bool SetSetting(SettingArtifTransfBase setting);

        /// <summary>
        /// vratenie nastaveni transformacie
        /// </summary>
        /// <returns>nastavenie danej transformacie</returns>
        SettingArtifTransfBase GetSetting();

        /// <summary>
        /// kontrola uzivatelom zadefinovanych hodnot
        /// </summary>
        /// <returns>true ak ok inak false</returns>
        bool CheckUserDefinedSetting();
    }
}