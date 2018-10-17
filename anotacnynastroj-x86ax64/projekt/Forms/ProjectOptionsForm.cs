using Projekt.Enums;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Projekt.Forms
{
    /// <summary>
    /// Trieda reprezentujuca uzivatelske rozhranie  pre umoznenie zadefinovania nastaveni projektu
    /// </summary>
    public partial class ProjectOptionsForm : Form
    {
        /// <summary>
        /// nastavenia projektu
        /// </summary>
        public ProjectOptions ProjOptions { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="projectOptions">nastavenia projektu</param>
        public ProjectOptionsForm(ProjectOptions projectOptions)
        {
            InitializeComponent();
            ProjOptions = new ProjectOptions(projectOptions);
            cmbTools.DataSource = Enum.GetValues(typeof(EDrawObject));
            SetOptionsAsProject();
        }

        /// <summary>
        /// Nastavenia projektu zobrazime na forme.
        /// </summary>
        private void SetOptionsAsProject()
        {
            cbShowGuideLine.Checked = ProjOptions.ShowGuideLine;

            nudGuideLine.Value = (int)ProjOptions.GuideLinePen.PenWidth;
            pnlGuideLineClr.BackColor = ProjOptions.GuideLinePen.PenColor;
            trbGuideLineOpacity.Value = ProjOptions.GuideLinePen.PenColor.A;

            nudFontWidth.Value = (int)ProjOptions.ClassFont.PenWidth;
            panel1.BackColor = ProjOptions.ClassFont.PenColor;
            panel2.BackColor = ProjOptions.RectColor;
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            ProjOptions.ShowGuideLine = cbShowGuideLine.Checked;
            ProjOptions.GuideLinePen.PenColor = pnlGuideLineClr.BackColor;
            ProjOptions.GuideLinePen.PenWidth = (int)nudGuideLine.Value;

            SetToolsOptionsAsForm();

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Zmena created farby po kliknuti na panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlToolCreatedColorClick(object sender, EventArgs e)
        {
            ChangePnlColorAfterSelectColor(sender as Panel, trbCreatedClrOpacity.Value);            
        }

        /// <summary>
        /// Zmena creating farby po kliknuti na panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlToolCreatingColorClick(object sender, EventArgs e)
        {
            ChangePnlColorAfterSelectColor(sender as Panel, trbCreatingClrOpacity.Value);
        }

        /// <summary>
        /// Change guide lines color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlGuideLineColorClick(object sender, EventArgs e)
        {
            ChangePnlColorAfterSelectColor(sender as Panel, trbGuideLineOpacity.Value);
        }

        private void PnlTextColorClick(object sender, EventArgs e)
        {
            ChangePnlColorAfterSelectColor(sender as Panel, 255);
        }

        private void PnlRectangleColorClick(object sender, EventArgs e)
        {
            ChangePnlColorAfterSelectColor(sender as Panel, 255);
        }

        private void ChangePnlColorAfterSelectColor(Panel panel, int opacity)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (panel != null) panel.BackColor = Color.FromArgb(opacity, cd.Color);
            }
        }

        /// <summary>
        /// zmenil sa vyber v comboboxe nastrojov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedToolOptionChanged(object sender, EventArgs e)
        {
            switch (cmbTools.SelectedIndex)
            {
                case (int)EDrawObject.Painting:
                    SetToolOptionsOnForm((int)ProjOptions.CreatedPAPen.PenWidth, ProjOptions.CreatedPAPen.PenColor, ProjOptions.CreatingPAColor.Color);
                    break;
                case (int)EDrawObject.BoundingBox:
                    SetToolOptionsOnForm((int)ProjOptions.CreatedBBPen.PenWidth, ProjOptions.CreatedBBPen.PenColor, ProjOptions.CreatingBBColor.Color);
                    break;
                case (int)EDrawObject.Polyline:
                    SetToolOptionsOnForm((int)ProjOptions.CreatedPLPen.PenWidth, ProjOptions.CreatedPLPen.PenColor, ProjOptions.CreatingPLPenStruct.PenColor);
                    break;
                default:
                    return;
            }
        }

        private void SetToolOptionsOnForm(int width, Color created, Color creating)
        {
            nudToolWidth.Value = width;
            pnlToolCreatingClr.BackColor = creating;
            trbCreatingClrOpacity.Value = creating.A;
            pnlToolCreatedClr.BackColor = created;
            trbCreatedClrOpacity.Value = created.A;
        }

        /// <summary>
        ///  Ide sa zmenit hodnota treba ulozit nastavenia pre doterajsiu volbu toolu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbToolsDropDown(object sender, EventArgs e)
        {
            SetToolsOptionsAsForm();
        }

        private void SetToolsOptionsAsForm()
        {
            ProjOptions.RectColor = Color.FromArgb(255, panel2.BackColor);
            ProjOptions.ClassFont.PenColor = Color.FromArgb(255, panel1.BackColor);
            ProjOptions.ClassFont.PenWidth = (float)nudFontWidth.Value;
            switch (cmbTools.SelectedIndex)
            {
                case (int)EDrawObject.Painting:
                    ProjOptions.CreatedPAPen.PenColor = Color.FromArgb(trbCreatedClrOpacity.Value, pnlToolCreatedClr.BackColor);
                    ProjOptions.CreatedPAPen.PenWidth = (float)nudToolWidth.Value;
                    ProjOptions.CreatingPAColor = new SolidBrush(Color.FromArgb(trbCreatingClrOpacity.Value, pnlToolCreatingClr.BackColor));
                    break;
                case (int)EDrawObject.BoundingBox:
                    ProjOptions.CreatedBBPen.PenColor = Color.FromArgb(trbCreatedClrOpacity.Value, pnlToolCreatedClr.BackColor);
                    ProjOptions.CreatedBBPen.PenWidth = (float)nudToolWidth.Value;
                    ProjOptions.CreatingBBColor = new SolidBrush(Color.FromArgb(trbCreatingClrOpacity.Value, pnlToolCreatingClr.BackColor));
                    break;
                case (int)EDrawObject.Polyline:
                    ProjOptions.CreatedPLPen.PenColor = Color.FromArgb(trbCreatedClrOpacity.Value, pnlToolCreatedClr.BackColor);
                    ProjOptions.CreatingPLPenStruct.PenWidth = ProjOptions.CreatedPLPen.PenWidth = (float)nudToolWidth.Value;
                    ProjOptions.CreatingPLPenStruct.PenColor = Color.FromArgb(trbCreatingClrOpacity.Value, pnlToolCreatingClr.BackColor);
                    break;
                default:
                    return;
            }
        }

        private void SHowGuideLineChanage(object sender, EventArgs e)
        {
           pnlGuideLineOptions.Enabled = cbShowGuideLine.Checked;
        }

        /// <summary>
        /// Zmena trackBaru pre opacity created color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedColorOpacityChange(object sender, EventArgs e)
        {
            pnlToolCreatedClr.BackColor = Color.FromArgb(trbCreatedClrOpacity.Value, pnlToolCreatedClr.BackColor);
        }

        /// <summary>
        /// Zmena trackBaru pre opacity creating color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatingColorOpacityChange(object sender, EventArgs e)
        {
            pnlToolCreatingClr.BackColor = Color.FromArgb(trbCreatingClrOpacity.Value, pnlToolCreatingClr.BackColor);
        }

        /// <summary>
        /// Zmena trackBaru pre guideLine color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuideLineOpacityChanged(object sender, EventArgs e)
        {
            pnlGuideLineClr.BackColor = Color.FromArgb(trbGuideLineOpacity.Value, pnlGuideLineClr.BackColor);
        }

        private void BtnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Farba sa meni kliknutim na danu farbu, nasledne sa zobrazi paleta farieb\n" +
                            "kde sa vyberie nova farba.\n" +
                            "Width predstavuje hrubku ciar objektov vztvorenych jednotlivymi nastrojmi.\n" +
                            "Volba 'ShowGuideLineInImage' sluzi na povolenie resp. zakazanie zobrazenia\n" +
                            "vodiacich ciar.\n" +
                            "CreatingColor predstavuje farbu objektov pri ich vytvarani konkretnym nastrojom.\n" +
                            "CreatedColor predstavuje farbu objektov ktore su uz vytvorene konkretnym nastrojom\n", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// trieda reprezentujuca nastavenia pre projekt
    /// </summary>
    [Serializable]
    public class ProjectOptions : ICloneable
    {
        private const string File_With_Options = "options.dat";

        /// <summary>
        /// Konstruktor ak ma not null parameter, skopruje nastavenia z tohto parametra
        /// </summary>
        /// <param name="projOpt"></param>
        public ProjectOptions(ProjectOptions projOpt = null)
        {
            SetDefaultOptions(projOpt == null ? null : (ProjectOptions)projOpt.Clone());
        }

        #region

        private Color _creatingPAColor;
        private Color _creatingBBColor;

        /// <summary>
        /// Farba pre painting ktory sa vytvara
        /// </summary>
        public SolidBrush CreatingPAColor 
        {
            get { return new SolidBrush(_creatingPAColor); }
            set { _creatingPAColor = value.Color; }
        }

        /// <summary>
        /// Farba pre boundingbox ktory sa vytvara
        /// </summary>
        public SolidBrush CreatingBBColor
        {
            get { return new SolidBrush(_creatingBBColor); }
            set { _creatingBBColor = value.Color; }
        }

        /// <summary>
        /// Struktura pre nastavenie PL ktory sa vytvara
        /// </summary>
        public StructureForPen CreatingPLPenStruct { get; set; }

        /// <summary>
        /// Vrati pen pre PL ktory sa vytvara podla zadefinovaneho zoomu
        /// </summary>
        /// <param name="zoom">zoom pre ktory sa ma vytvorit vlastnosti PL ktory sa vytvara</param>
        /// <returns>Pen pre PL ktory sa vytvara</returns>
        public Pen GetCreatingPLPen(double zoom)
        {
            return SetZoomForPen(CreatingPLPenStruct, zoom);
        }

        private Color _rectColor;
        public Color RectColor
        {
            get { return _rectColor; }
            set { _rectColor = value; }
        }
        private StructureForPen _classFont;
        public StructureForPen ClassFont
        {
            get { return _classFont; }
            set { _classFont = value; }
        }
        #endregion

        #region
        public StructureForPen CreatedPAPen { get; set; }
        public Pen GetCreatedPAPen(double zoom)
        {
            return SetZoomForPen(CreatedPAPen, zoom);
        }

        public StructureForPen CreatedBBPen { get; set; }
        public Pen GetCreatedBBPen(double zoom)
        {
            return SetZoomForPen(CreatedBBPen, zoom);
        }

        public StructureForPen CreatedPLPen { get; set; }
        public Pen GetCreatedPLPen(double zoom)
        {
            return SetZoomForPen(CreatedPLPen, zoom);
        }
        #endregion

        public StructureForPen SelectedObjectPen { private get; set; }
        public Pen GetSelectedObjectPen(double zoom)
        {
            return SetZoomForPen(SelectedObjectPen, zoom);
        }

        public StructureForPen GuideLinePen { get; set; }
        public Pen GetGuideLinePen(double zoom)
        {
            return SetZoomForPen(GuideLinePen, zoom);
        }

        public StructureForPen FoundObjPen { private get; set; }
        public Pen GetFoundObjPen(double zoom)
        {
            return SetZoomForPen(FoundObjPen, zoom);
        }

        public bool ShowGuideLine { get; set; }

        private Pen SetZoomForPen(StructureForPen penStruct, double zoom)
        {
            return new Pen(penStruct.PenColor, (int)Math.Ceiling(penStruct.PenWidth / zoom));
        }

        /// <summary>
        /// Nastavi defaultne nastavenia, ak vsak ako paramenter pride projectoptions ktory nie je null
        /// nastavia sa nastavenia podla neho
        /// </summary>
        /// <param name="projOpt"></param>
        private void SetDefaultOptions(ProjectOptions projOpt = null)
        {
            CreatingPAColor = projOpt == null ? new SolidBrush(Color.FromArgb(100, 50, 255, 0)) : projOpt.CreatingPAColor;
            CreatingBBColor = projOpt == null ? new SolidBrush(Color.FromArgb(64, 255, 0, 0)) : projOpt.CreatingBBColor;
            CreatingPLPenStruct = projOpt == null ? new StructureForPen(Color.FromArgb(100, 0, 50, 255), 5) : projOpt.CreatingPLPenStruct;

            CreatedPAPen = projOpt == null ? new StructureForPen(Color.FromArgb(120, 51, 255, 0), 3) : projOpt.CreatedPAPen;
            CreatedBBPen = projOpt == null ? new StructureForPen(Color.Crimson, 5) : projOpt.CreatedBBPen;
            CreatedPLPen = projOpt == null ? new StructureForPen(Color.SteelBlue, CreatingPLPenStruct.PenWidth) : projOpt.CreatedPLPen;

            SelectedObjectPen = projOpt == null ? new StructureForPen(Color.Aqua, 5) : projOpt.SelectedObjectPen;
            GuideLinePen = projOpt == null ? new StructureForPen(Color.FromArgb(150, 200, 100, 100), 2) : projOpt.GuideLinePen;
            FoundObjPen = projOpt == null ? new StructureForPen(Color.LightSalmon, 5) : projOpt.FoundObjPen;

            ShowGuideLine = projOpt == null ? true : projOpt.ShowGuideLine;

            //RectColor = Color.Black;
            //ClassFont = new StructureForPen(Color.White, 5);
            ClassFont = projOpt == null ? new StructureForPen(Color.Black, 15) : projOpt.ClassFont;
            RectColor = projOpt == null ? Color.Red : projOpt.RectColor;
        }

        /// <summary>
        /// Ulozenie nastaveni
        /// </summary>
        /// <param name="projectFolder">cesta kde sa maju ulozit nastavenia</param>
        public void SaveProjectOptions(string projectFolder)
        {
            try
            {
                var fileToSave = string.Format("{0}\\{1}", projectFolder, File_With_Options);
                var formater = new BinaryFormatter();
                using (var stream = new FileStream(fileToSave, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    formater.Serialize(stream, this);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Problem pri ukladani nastaveni: " + exc.Message, "Error - ProjectOptions - SaveProjectOptions()", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nacitanie nastaveni projektu
        /// </summary>
        /// <param name="projectFolder">cesta k suboru</param>
        /// <returns>nastavenia projektu</returns>
        public static ProjectOptions LoadProjectOtions(string projectFolder)
        {
            try
            {
                var fileToSave = string.Format("{0}\\{1}", projectFolder, File_With_Options);
                if (!File.Exists(fileToSave))
                    return new ProjectOptions();

                var formater = new BinaryFormatter();
                using (var stream = new FileStream(fileToSave, FileMode.Open, FileAccess.Read))
                {
                    return (ProjectOptions)formater.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Problem pri nacitavani nastaveni projektu, budu nacitane defaultne nastavenia. Error: " + exc.Message,
                                "Error - ProjectOption - LoadProjectPotions()", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return new ProjectOptions();
            }
        }

        /// <summary>
        /// Vytvorenie kopie nastaveni
        /// </summary>
        /// <returns>kopia nastaveni</returns>
        public object Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }
    }

    [Serializable]
    public class StructureForPen
    {
        public Color PenColor { get; set; }

        public float PenWidth { get; set; }

        public StructureForPen(Color penColor, float penWidth)
        {
            PenColor = penColor;
            PenWidth = penWidth;
        }
    }
}
