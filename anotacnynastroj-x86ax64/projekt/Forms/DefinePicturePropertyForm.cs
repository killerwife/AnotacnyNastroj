using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projekt.UserControls;

namespace Projekt.Forms
{
    public partial class DefinePicturePropertyForm : Form
    {
        private const string pathToFileSeting = @"Data\Setting\PictureProps.dat";

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DefinePicturePropertyForm()
        {
            InitializeComponent();
            LoadAndShowDefineAttr();
        }

        private void LoadAndShowDefineAttr()
        {
            if (!File.Exists(pathToFileSeting)) return;
            FileStream fs = null;
            StreamReader sr = null;
            var props = new List<string>();
            try
            {
                fs = new FileStream(pathToFileSeting, FileMode.Open);
                sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    props.Add(sr.ReadLine());
                }
                ShowAttrs(props);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Naspravny format suboru s nastaveniami pre obrazky.\n"+ ex.Message, "Error-Project.CreatePictureProperty()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
                if (sr != null) sr.Close();
            }          
        }

        private void ShowAttrs(List<string> props)
        {
            try
            {
                foreach (var prop in props)
                {
                    var array = prop.Split(';');
                    if (array[1] == "01")
                    {
                        var control = new AddBoolAttribute();
                        control.SetNamePropertyString(array[0]);
                        AddAtributeToPanel(control);
                    }
                    else
                    {
                        var control = new AddMultiAttribute();
                        control.SetNameAndPropertyString(array);
                        AddAtributeToPanel(control);
                    }
                }
            }
            catch (Exception)
            {}
           
        }

        /// <summary>
        /// Obslúženie akcie kliknutia na tlačítko Save. Uloženie triedy spolu s atribútmi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MySaveClick(object sender, EventArgs e)
        {
            TextWriter sw = null;
            try
            {
                sw = new StreamWriter(pathToFileSeting, false);
                foreach (Control control in pnlAttribute.Controls)
                {
                    if (control.GetType() == typeof (AddMultiAttribute))
                    {
                        string values = ((AddMultiAttribute) control).GetPropertyString();
                        if (values != null) sw.WriteLine(values);
                        else
                        {
                            MessageBox.Show("Niektori z viachodnotovych atributov nema zadefinovane meno alebo hodnoty", "Zle vyplnene hodnotu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (control.GetType() == typeof (AddBoolAttribute))
                    {
                        string value = ((AddBoolAttribute) control).GetPropertyString();
                        if (value != null) sw.WriteLine(value + ";01");
                        else
                        {
                            MessageBox.Show("Niektori z boolenovskych atributov nema zadefinovane meno", "Zle vyplnene hodnotu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                "Error- DefinePicturePropertyForm.MySaveClick()",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                ;
            }
            finally
            {
                if (sw != null) sw.Close();
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Obsluženie akcie kliknutia na tlačítko Add - pridanie viachodnotovych atribútov pre vytváranú triedu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMultivalueAttributeClick(object sender, EventArgs e)
        {
            AddAtributeToPanel(new AddMultiAttribute());
        }

        /// <summary>
        /// Pridanie UserControl na panel s vlastnostami pre atribut.
        /// </summary>
        /// <param name="control"></param>
        private void AddAtributeToPanel(UserControl control)
        {
            pnlAttribute.VerticalScroll.Value = 0;
            control.Location = new Point(3, (3 + 26) * pnlAttribute.Controls.Count);
            pnlAttribute.Controls.Add(control);
        }

        /// <summary>
        /// Obsluženie akcie kliknutia na tlačítko Add - pridanie dvojhodnotovych atribútov pre vytváranú triedu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddBooleanAttributeClick(object sender, EventArgs e)
        {
            AddAtributeToPanel(new AddBoolAttribute());
        }

        /// <summary>
        /// Obsluzenie kliknutia na tlacidlo help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Hodnoty pre viachodnotové atribúty zadávajte oddelené čiarkami alebo bodkočiarkami, " +
                            "pričom za poslednú hodnotou sa čiarka resp. bodkočiarka nedáva.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
