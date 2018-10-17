using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Projekt.Figure;
using Projekt.UserControls;

namespace Projekt
{
    /// <summary>
    /// Vseobecne vlastnosti obrazku.
    /// </summary>
    class PictureProperty
    {
        /// <summary>
        /// Atribúty obrázku/frame.
        /// </summary>
        private List<string> UserDefValue { get; set; }

        private Panel _pnlAtr;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="pnlAtr">panel na ktorom su atributy.</param>
        /// <param name="attributes">Zoznam parametrov podla ktorych sa vytvoria atributy. 
        /// Prva hodnota parametru je nazov atributu, dalsie hodnoty parametra su hodnoty atributu.</param>
        public PictureProperty(Panel pnlAtr, List<string[]> attributes)
        {
            UserDefValue = new List<string>();
            _pnlAtr = pnlAtr;
            CreateAttribute(attributes);
        }

        /// <summary>
        /// Vytvorenie a pridanie atributov na panel s atributmi.
        /// </summary>
        /// <param name="attributes">Zoznam parametrov podla ktorych sa vytvoria atributy. 
        /// Prva hodnota parametru je nazov atributu, dalsie hodnoty parametra su hodnoty atributu.</param>
        private void CreateAttribute(List<string[]> attributes)
        {
            UserDefValue.Clear();
            _pnlAtr.Controls.Clear();

            int i = 0;
            foreach (string[] attribute in attributes)
            {
                List<string> value = attribute.ToList();
                value.RemoveAt(0);
                UserDefValue.Add(value[0]);
                var control = new PictureAttribute(attribute[0], value);
                control.Location = new Point(0,30*i);
                _pnlAtr.Controls.Add(control);
                i++;
            }
        }

        /// <summary>
        /// Nastavenie atributov obrazka podal uzivatelom preddefinovanych hodnot
        /// </summary>
        public void SetUserDefine()
        {
            for (int i = 0; i < _pnlAtr.Controls.Count; i++)
            {
                Control control = _pnlAtr.Controls[i];
                if (control.GetType() == typeof(PictureAttribute))
                {
                    ((PictureAttribute)control).SetSelectedValue(UserDefValue[i]);
                }
            }
        }

        /// <summary>
        /// Vyplnenie atributov na panely podľa zadanych hodnot.
        /// </summary>
        /// <param name="properties">property pre vyplnenie</param>
        public void SetPictureProps(List<string[]> properties)
        {
            if (properties != null)
            {
                for (int i = 0; i < _pnlAtr.Controls.Count; i++)
                {
                    Control control = _pnlAtr.Controls[i];
                    if (control.GetType() == typeof (PictureAttribute))
                    {
                        if (i < properties.Count) ((PictureAttribute) control).SetSelectedValue(properties[i][1]);
                        else break;
                    }
                }
            }
        }

        /// <summary>
        /// Uloženie atribútov obrázku/frame.
        /// </summary>
        /// <param name="image">obrazok ktoreho atributy sa maju nastavit</param>
        public void SavePictureProps(BaseFigure image)
        {
            if (image == null) return;
            List<string[]> properties = new List<string[]>();
            foreach (Control control in _pnlAtr.Controls)
            {
                if (control.GetType() == typeof(PictureAttribute))
                {
                    properties.Add(new[] { ((PictureAttribute)control).AttributeName, ((PictureAttribute)control).GetSelectedValue() });
                }
            }
            image.Properties = properties;
        }

    }
}
