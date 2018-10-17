using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Projekt.DrawObjects
{
    /// <summary>
    /// Predok pre objekty, ktoré je možné kresliť na obrázok/frame.
    /// </summary>
    public abstract class DrawObject 
    {
        private int _id;

        //public virtual int Size { get; set; }

        /// <summary>
        /// Vlastnosti objektu.
        /// </summary>
        public ObjectProperties Properties { get; set; }

        /// <summary>
        /// Jedinečné ID objektu.
        /// ID = index v poli objektov obrázka.
        /// </summary>
        public int ID
        {
            get { return _id; }
            set 
            { 
                _id = value;
                Name = ToString().Split(new[]{'.'}).Last() + " " + ID;
            }
        }

        /// <summary>
        /// Názov objektu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// porovna objectProperty objektu s property poslanymi ako parameter.
        /// True ak ma draw objekt zhodne hodnoty atributov ako poslany object property, a je aj rovnakej triedy.
        /// Draw object moze mat definovane aj dalsie atrtibuty ako poslane property.
        /// </summary>
        /// <param name="op">objectProperty na porovnanie</param>
        /// <returns>True ak ma draw objekt zhodne hodnoty atributov ako poslany object property, a je aj rovnakej triedy, inak false</returns>
        public bool ComparePropertyWith(ObjectProperties op)
        {
            if (Properties.Class == null || !Equals(Properties.Class.ToLower(), op.Class.ToLower()))
                return false;
            int i = 0;
            foreach (string attrForeign in op.AtributesName)
            {
                int j = 0;
                foreach (string attrThis in Properties.AtributesName)
                {
                    if (!Equals(attrForeign.ToLower(), attrThis.ToLower())) //hladame nazov atributu this taky ako ma foreign
                    {
                        j++;
                        continue;
                    }
                    if (!Equals(Properties.AtributesValue[j].ToLower(), op.AtributesValue[i].ToLower()))//porovname hodnotu atributu
                        return false;
                    break;//zhodne pokracujeme dalsim atributom
                }
                //mohlo sa stat ze nebol najdeny v object property daneho draw objectu atribut treba skontrolovat
                if (j == Properties.AtributesName.Length)
                    return false;
                i++;
            }
            return true;
        }

        /// <summary>
        /// Vygenerovanie fragmentu xml pre vybraný objekt.
        /// </summary>
        /// <param name="xmlWriter">xmlWriter pouzity na vygenerovanie dokumetu</param>
        public abstract void DrawObject2Xml(XmlWriter xmlWriter);

        /// <summary>
        /// Nacitanie vsetkych draw objektov dadneho typu z fragmentu xml.
        /// </summary>
        /// <param name="xImage">element xml</param>
        /// <returns>zoznam najdenych drawObjektov v elemente xml</returns>
        public abstract List<DrawObject> DrawObjectFromXml(XElement xImage);

        /// <summary>
        /// Nacitanie dat o BB respektive PA,PL.
        /// </summary>
        /// <param name="xElement">xml element</param>
        /// <returns>zoznam ObjectProperty</returns>
        protected ObjectProperties LoadPropsFromXml(XElement xElement)
        {
            ObjectProperties op = null;
            var xClass = xElement.Element("class");            
            if (xClass != null)
            {
                string cl = xClass.Attribute("name").Value;
                List<string> clAttrName = new List<string>();
                List<string> clAttrValue = new List<string>();
                foreach (XElement xClassAttrs in xClass.Descendants("attribute"))
                {
                    clAttrName.Add(xClassAttrs.Attribute("name").Value);
                    clAttrValue.Add(xClassAttrs.Value);
                }
                op = new ObjectProperties(clAttrName.ToArray(), clAttrValue.ToArray(), cl);
            }
            return op;
        }

        protected ObjectProperties LoadPropsFromXml2(XElement xElement)
        {
            ObjectProperties op = null;
            var xClass = xElement.Element("class_name");
            if (xClass != null)
            {
                string cl = xClass.Element("project_id").Value;
                List<string> clAttrName = new List<string>();
                List<string> clAttrValue = new List<string>();
                foreach (XElement xClassAttrs in xClass.Descendants())
                {
                    if (xClassAttrs.Name.ToString() != "project_id")
                    {                        
                        clAttrName.Add(xClassAttrs.Name.ToString());
                        clAttrValue.Add(xClassAttrs.Value.ToString());
                    }
                }
                op = new ObjectProperties(clAttrName.ToArray(), clAttrValue.ToArray(), cl);
            }
            return op;
        }
    }
}
