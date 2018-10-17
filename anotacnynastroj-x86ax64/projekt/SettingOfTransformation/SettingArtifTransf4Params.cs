using System;
using System.Collections.Generic;
using Projekt.Enums;

namespace Projekt.SettingOfTransformation
{
    /// <summary>
    /// Trieda reprezentujuca nastavenia transformacii pre generovanie umelych dat, pre ktore vygenerovanie 
    /// je potrebne zadat vsetky 4 parametre - from, to, step, count.
    /// </summary>
    [Serializable]
    public abstract class SettingArtifTransf4Params : SettingArtifTransfBase
    {
        /// <summary>
        /// od
        /// </summary>
        public int From { get; protected set; }

        /// <summary>
        /// do
        /// </summary>
        public int To { get; private set; }

        /// <summary>
        /// krok
        /// </summary>
        public int Step { get; private set; }

        /// <summary>
        /// pocetnost
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="transformType">Typ transformacie, vid. ETransformType</param>
        /// <param name="from">od</param>
        /// <param name="to">do</param>
        /// <param name="step">krok</param>
        /// <param name="count">pocetnost</param>
        protected SettingArtifTransf4Params(ETransformType transformType, int @from, int to, int step, int count)
            : base(transformType)
        {
            Count = count;
            Step = step;
            To = to;
            From = @from;
        }

        /// <summary>
        /// Kontrola hodnot
        /// </summary>
        /// <returns>true ak OK, inak false</returns>
        public override bool CheckValue()
        {
            //ak je zly rozsah od - do
            if (From > To || (From == 0 && To == 0))
            {
                MsgError("Zle zadaný rozsah (from, to).");
                return false;
            }

            //ak krok alebo pocetnost <= 0
            if (Step <= 0 || Count <= 0)
            {
                MsgError("Zle zadané hodnoty kroku alebo početnosti.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>vrati nastavenia v tvare stringu</returns>
        public override string ToString()
        {
            return string.Format("{0} - od: {1}, do: {2}, krok: {3}, pocetnost: {4}.", Name, From, To, Step, Count);
        }

        /// <summary>
        /// Vytvorenie zoznamu nahodnych hodnot podla zadefinovanych parametrov
        /// </summary>
        /// <returns>zoznamu nahodnych hodnot podla zadefinovanych parametrov</returns>
        public abstract List<int> GetRandomValue();
    }
}
