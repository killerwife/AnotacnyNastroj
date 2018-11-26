using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt.Forms
{
    public partial class AddFilesToCompareForm : Form
    {
        public bool Project4Gt { get; set; }

        public bool Project4Test { get; set; }

        public string File4Gt { get; set; }

        public string File4Test { get; set; }

        public AddFilesToCompareForm()
        {
            InitializeComponent();
            chbGt.Checked = false;
            chbTest.Checked = false;
            Project4Gt = false;
            Project4Test = false;
        }

        /// <summary>
        /// Zmena stavu checkboxu gt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbGt_CheckedChanged(object sender, EventArgs e)
        {
            if (chbGt.Checked)
            {
                txtBoxGt.Enabled = false;
                Project4Gt = true;
            }
            else
            {
                txtBoxGt.Enabled = true;
                Project4Gt = false;
            }
        }

        /// <summary>
        /// Zmena stavu checkboxu test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTest.Checked)
            {
                txtBoxTest.Enabled = false;
                Project4Test = true;
            }
            else
            {
                txtBoxTest.Enabled = true;
                Project4Test = false;
            }
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if ((Project4Gt || !string.IsNullOrWhiteSpace(txtBoxGt.Text)) && (Project4Test || !string.IsNullOrWhiteSpace(txtBoxTest.Text)))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Chýbajúce dáta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        /// <summary>
        /// Otvorenie dialogu pre vyber suboru gt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxGt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "XML Files(*.XML)|*.XML";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtBoxGt.Text = ofd.FileName;
                File4Gt = ofd.FileName;
            }
        }

        /// <summary>
        /// Otvorenie dialogu pre vyber suboru test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxTest_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "XML Files(*.XML)|*.XML";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtBoxTest.Text = ofd.FileName;
                File4Test = ofd.FileName;
            }
        }
    }
}
