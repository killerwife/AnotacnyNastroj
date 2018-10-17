namespace Projekt.Enums
{
    /// <summary>
    /// Typy jednotlivych transformacii
    /// </summary>
    public enum ETransformType
    {
        /// <summary>
        /// posun
        /// </summary>
        Move = 0,

        /// <summary>
        /// rotacia
        /// </summary>
        Rotate = 1,

        /// <summary>
        /// zmena velkosti
        /// </summary>
        Scale = 2,

        /// <summary>
        /// zrkadlenie
        /// </summary>
        Reflection = 3,

        /// <summary>
        /// rozmazanie
        /// </summary>
        Blurring = 4,

        /// <summary>
        /// zaostrenie
        /// </summary>
        Sharpen = 5,

        /// <summary>
        /// aditivny sum
        /// </summary>
        AdditiveNoise = 6,

        /// <summary>
        /// impulzny sum
        /// </summary>
        ImpulseNoise = 7
    }
}
