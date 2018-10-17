using Projekt.DrawObjects;
using Projekt.Figure;
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
    public partial class JoinBbsForm : Form
    {
        private List<BaseFigure> OpenImages { get; set; }
        private List<CompareObj> compareObjects;
        private BackgroundWorker worker;
        private bool exitThread;
        private int searchWindow, sizeChange, ignoreFrames;
        private bool windowMetrics;
        private double searchWindowW, sizeChangeW, movementW;
        private int minGap;
        public JoinBbsForm(List<BaseFigure> images)
        {
            OpenImages = images;
            InitializeComponent();            
            //progressBar1.Maximum = images.Count;
            //progressBar1.Step = 1;
            //progressBar1.Value = 0;
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            exitThread = false;
            windowMetrics = false;
            searchWindowW = sizeChangeW = movementW = 0;
            searchWindow = sizeChange = ignoreFrames = 0;
            minGap = 0;
            compareObjects = new List<CompareObj>();
            numericUpDown5.Enabled = false;
        }

        private void JoinBbsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchWindow = (int)numericUpDown1.Value;
            sizeChange = (int)numericUpDown2.Value;
            ignoreFrames = (int)numericUpDown3.Value;
            searchWindowW = (double)numericUpDownWSWindow.Value;
            sizeChangeW = (double)numericUpDownWWeight.Value;
            movementW = (double)numericUpDownMWeight.Value;
            minGap = (int)numericUpDown4.Value;
            windowMetrics = radioButtonPixel.Checked;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            groupBoxMetrics.Enabled = false;
            numericUpDownWSWindow.Enabled = false;
            numericUpDownWWeight.Enabled = false;
            numericUpDownMWeight.Enabled = false;
            numericUpDown4.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;            
            worker.RunWorkerAsync();
            
            //for (int i = 0; i <= 10000000; i++)
            //{
                //System.Threading.Thread.Sleep(1000);
                //progressBar1.PerformStep();

            //}
            //foreach (BaseFigure image in OpenImages)
            //{ 
                
            //}
            /**
            for (int i = 0; i < OpenImages.Count; i++)
            {
                if (i == 0 && OpenImages[0].BoundBoxes.Count > 0)
                {
                    drawObj = OpenImages[0].BoundBoxes[0];
                }
                foreach (BoundingBox b in OpenImages[i].BoundBoxes)
                {
                    if (!b.Equals(drawObj))
                    {
                        b.Properties = drawObj.Properties;
                    }
                }
            }
            **/
            
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < OpenImages.Count; i++)
            {
                if (!exitThread)
                {
                    if (compareObjects.Count == 0)
                    {
                        foreach (BoundingBox b in OpenImages[i].BoundBoxes)
                        {
                            compareObjects.Add(new CompareObj(b));
                        }
                    }
                    else 
                    {
                        for (int j = compareObjects.Count - 1; j >= 0; j--)
                        {
                            foreach (BoundingBox b in OpenImages[i].BoundBoxes)
                            {
                                if (compareObjects[j].isSuitable(searchWindow, sizeChange, b, windowMetrics))
                                {
                                    compareObjects[j].checkAdept(b, movementW, searchWindowW, sizeChangeW, minGap);
                                }
                            }
                            if (compareObjects[j].adeptsCount() != 1)
                            {
                                compareObjects[j].clearAdepts();
                                compareObjects[j].Frame = compareObjects[j].Frame + 1;
                                if (compareObjects[j].Frame >= ignoreFrames)
                                {
                                    compareObjects.Remove(compareObjects[j]);
                                }
                            }
                        }
                        for (int j = 0; j < compareObjects.Count; j++)
                        {
                            if (compareObjects[j].adeptsCount() == 1)
                            {
                                for (int k = j+1; k < compareObjects.Count; k++)
                                {
                                    if (compareObjects[k].adeptsCount() == 1)
                                    {                                        
                                        if (compareObjects[j].getAdept().Equals(compareObjects[k].getAdept()))
                                        {
                                            compareObjects[j].Found = true;
                                            compareObjects[k].Found = true;
                                        }
                                    }
                                }
                            }
                        }
                        for (int j = compareObjects.Count-1; j >= 0; j--)
                        {
                            if (compareObjects[j].Found)
                            {
                                compareObjects[j].clearAdepts();
                                compareObjects[j].Frame = compareObjects[j].Frame + 1;
                                if (compareObjects[j].Frame >= ignoreFrames)
                                {
                                    compareObjects.Remove(compareObjects[j]);
                                }
                            }
                            else 
                            {
                                if (compareObjects[j].adeptsCount() == 1)
                                {
                                    compareObjects[j].swap();
                                    compareObjects[j].clearAdepts();
                                    compareObjects[j].Frame = 0;
                                }
                            }                            
                        }
                        foreach (BoundingBox b in OpenImages[i].BoundBoxes)
                        {
                            bool t = false;
                            foreach (CompareObj c in compareObjects)
                            {
                                if (b.Equals(c.Figure))
                                {
                                    t = true;
                                }
                            }
                            if (!t)
                            {
                                compareObjects.Add(new CompareObj(b));    
                            }
                        }
                    }
                    var percentage = (i + 1) * 100 / OpenImages.Count;
                    worker.ReportProgress(percentage);
                }
                else
                {
                    break;
                }
            }            
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!exitThread)
            {
                MessageBox.Show("Tracking done. Manual check needed.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void FormClosingCancelWorker(object sender, FormClosingEventArgs e)
        {
            exitThread = true;   
        }

        private void HelpClick(object sender, EventArgs e)
        {
            MessageBox.Show("Size of search window - maximum possible x/y coordinates change.\n" +
                            "Size of search window - Unit can be switched between percentage or pixel. Weight can be set.\n" +
                            "Size change - maximum possible width/height change. Unit is percentage. Weight can be set.\n" +
                            "Ignored frames - number of frames where object may dissapear.\n" +
                            "Movement weight - Weight of direction change.\n" +
                            "Minimum difference - Minimum score difference between 2 adepts.\n" +                            
                            "Visual relation - work in progress\n", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
