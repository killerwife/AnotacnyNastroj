using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Projekt.UserControls;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca uzivatelske rozhranie pre zadefinovania parametrov vyhladavania
    /// </summary>
    public partial class FindObjectsInProject : Form
    {
        /// <summary>
        /// pre spristupnenie vsetkych tried
        /// </summary>
        private readonly ClassGenerator _myClasses;

        private static FindObjectsInProject _instance = null;

        /// <summary>
        /// pre singleton
        /// </summary>
        public static FindObjectsInProject Instance { get { return _instance; } }

        /// <summary>
        /// zoznam tried a ich vlastnosti
        /// </summary>
        private readonly List<ObjectClass> _objClasses;

        /// <summary>
        /// Nazov triedy, nazvy atributov a ich hodnoty ktore vybral uzivatel pre triedu ktora sa ma hladat
        /// </summary>
        public ObjectProperties PropsToFind { get; set; }

        private FindObjectEventHandler _findObjectEvent;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="myClasses">pristup k triedam nastroja</param>
        /// <param name="findObjectEvent">eventHandler</param>
        public FindObjectsInProject(ClassGenerator myClasses, ref FindObjectEventHandler findObjectEvent)
        {
            InitializeComponent();
            _findObjectEvent = findObjectEvent;
            _myClasses = (ClassGenerator)myClasses.Clone();
            PropsToFind = new ObjectProperties();
            _objClasses = new List<ObjectClass>();
            Init();
            _instance = this;
        }

        /// <summary>
        /// Inicializacia tried a ich vlatnosti.
        /// </summary>
        private void Init()
        {
            foreach (var myClass in _myClasses.GetAllClasses())
            {
                _objClasses.Add(new ObjectClass(myClass));
                cmbAllClasses.Items.Add(_objClasses.Last());

                _myClasses.CreatePropertiesControls(myClass, _objClasses.Last().PanelProps);

                foreach (var panelProp in _objClasses.Last().PanelProps.Controls)
                {
                    if (panelProp.GetType() == typeof(ClassMultiProperty))
                    {
                        ((ClassMultiProperty)panelProp).SetCanFinding();
                    }
                    else if (panelProp.GetType() == typeof(ClassBoolProperty))
                    {
                        ((ClassBoolProperty)panelProp).SetCanFinding();
                    }
                }
            }          
        }

        /// <summary>
        /// uzivatel zmenil triedu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedClassChanged(object sender, EventArgs e)
        {
            if(cmbAllClasses.SelectedIndex == -1) return;
            foreach (var classs in _objClasses)
            {
                if (classs.PropsToFind.Class != cmbAllClasses.Text)
                {
                    continue;
                }
                gbClassProps.Controls.Clear();
                gbClassProps.Controls.Add(classs.PanelProps);
            }
            
        }

        /// <summary>
        /// klik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFindClick(object sender, EventArgs e)
        {
            //treba nacitat nastavenia od uzivatela
            if (cmbAllClasses.SelectedIndex == -1)
            {
                MessageBox.Show("Neboli zvolene ziadne parametre vyhladavania", "Error - Zle parametre vhladavania.", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            var objectClass = cmbAllClasses.Items[cmbAllClasses.SelectedIndex] as ObjectClass;
            if (objectClass == null)
            {
                MessageBox.Show("Chyba pri hladani, skuste znovu", "Error - FindObjectInProject.BtnFindClick()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            objectClass.SetAttrsAndSelValue();
            PropsToFind = objectClass.PropsToFind;
            if (_findObjectEvent != null) _findObjectEvent(PropsToFind);
        }

        /// <summary>
        /// zatvarame okno treba zrusit vyhladavanie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyFormClosed(object sender, FormClosedEventArgs e)
        {
            _findObjectEvent(null);//nechcem nic hladat
            _instance = null;
            this.Dispose();
        }

        private void FormActivated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void FormDeactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.8;
        }

    }

    internal class ObjectClass
    {
        /// <summary>
        /// Nazov triedy, nazvy atributov a ich hodnoty ktore vybral uzivatel pre tuto triedu
        /// </summary>
        public ObjectProperties PropsToFind{ get; set; }

        /// <summary>
        /// Panel s property
        /// </summary>
        public Panel PanelProps { get; set; }

        public ObjectClass(string name)
        {
            PanelProps = new Panel();
            PropsToFind = new ObjectProperties();
            PanelProps.AutoScroll = true;
            PanelProps.Size = new Size(228,217);
            PanelProps.Location = new Point(6,20);
            PropsToFind.Class = name;
        }

        /// <summary>
        /// Nastavenie hodnot atributov
        /// </summary>
        public void SetAttrsAndSelValue()
        {
            List<string> attrsName = new List<string>();
            List<string> attrsValue = new List<string>();

            foreach (var control in PanelProps.Controls.Cast<object>().Where(control => control != null))
            {
                if (typeof (ClassMultiProperty) == control.GetType() && (control as ClassMultiProperty).Finding)
                {
                    var classMultiProperty = control as ClassMultiProperty;
                    if (classMultiProperty != null)
                    {
                        attrsName.Add(classMultiProperty.GetName());
                        attrsValue.Add(classMultiProperty.GetSelectedValue());
                    }
                }
                else if (typeof (ClassBoolProperty) == control.GetType() && (control as ClassBoolProperty).Finding)
                {
                    var classBoolProperty = control as ClassBoolProperty;
                    if (classBoolProperty != null)
                    {
                        attrsName.Add(classBoolProperty.AtrName);
                        attrsValue.Add(classBoolProperty.Checked ? "true" : "false");
                    }
                    
                }
            }
            PropsToFind.AtributesName = attrsName.ToArray();
            PropsToFind.AtributesValue = attrsValue.ToArray();
        }

        public override string ToString()
        {
            return PropsToFind.Class;
        }
    }
}
