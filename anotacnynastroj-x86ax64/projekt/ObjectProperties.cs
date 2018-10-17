using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    /// <summary>
    /// Trieda reprezentujúca vlastnosti nakreslených objektov na obrázku.
    /// </summary>
    public class ObjectProperties
    {
        /// <summary>
        /// Trieda objektu.
        /// </summary>
        public string Class { get; set;  }

        /// <summary>
        /// Názvy atribútov danej triedy.
        /// </summary>
        public string[] AtributesName { get; set; }

        /// <summary>
        /// Hodnoty atribútov danej triedy.
        /// </summary>
        public string[] AtributesValue { get; set; }

        /// <summary>
        /// Parametrický konštruktor.
        /// </summary>
        /// <param name="atributeName">Pole názvov atribútov.</param>
        /// <param name="atributeValue">Pole hodnôt atribútov.</param>
        /// <param name="pClass">Názov triedy objektu.</param>
        public ObjectProperties(string[] atributeName, string[] atributeValue, string pClass)
        {
            Class = pClass;
            AtributesName = atributeName;
            AtributesValue = atributeValue;
        }

        /// <summary>
        /// Bezparametrický konštruktor.
        /// </summary>
        public ObjectProperties()
        {
            Class = null;
            AtributesName = null;
            AtributesValue = null;
        }

    }
}
