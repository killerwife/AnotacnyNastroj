namespace Projekt.Forms
{
    partial class TestEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestEditorForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chbGtModeEnabled = new System.Windows.Forms.CheckBox();
            this.chbSync = new System.Windows.Forms.CheckBox();
            this.labImageCount = new System.Windows.Forms.Label();
            this.tbGoOnPicture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlPreviousFrame = new System.Windows.Forms.Panel();
            this.pnlNextFrame = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbGtTracks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chListTracks = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbUncategorized = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(12, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 350);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.imageBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.imageBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 350);
            this.splitContainer1.SplitterDistance = 585;
            this.splitContainer1.TabIndex = 0;
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.Color.White;
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(585, 350);
            this.imageBox.TabIndex = 4;
            this.imageBox.TabStop = false;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawToImageBox1);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseUp);
            // 
            // imageBox1
            // 
            this.imageBox1.BackColor = System.Drawing.Color.White;
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(0, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(581, 350);
            this.imageBox1.TabIndex = 4;
            this.imageBox1.TabStop = false;
            this.imageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawToImageBox2);
            this.imageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chbGtModeEnabled);
            this.panel2.Controls.Add(this.chbSync);
            this.panel2.Controls.Add(this.labImageCount);
            this.panel2.Controls.Add(this.tbGoOnPicture);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pnlPreviousFrame);
            this.panel2.Controls.Add(this.pnlNextFrame);
            this.panel2.Location = new System.Drawing.Point(12, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1170, 31);
            this.panel2.TabIndex = 8;
            // 
            // chbGtModeEnabled
            // 
            this.chbGtModeEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbGtModeEnabled.AutoSize = true;
            this.chbGtModeEnabled.Location = new System.Drawing.Point(927, 6);
            this.chbGtModeEnabled.Name = "chbGtModeEnabled";
            this.chbGtModeEnabled.Size = new System.Drawing.Size(77, 17);
            this.chbGtModeEnabled.TabIndex = 19;
            this.chbGtModeEnabled.Text = "Dual mode";
            this.chbGtModeEnabled.UseVisualStyleBackColor = true;
            this.chbGtModeEnabled.CheckedChanged += new System.EventHandler(this.chbGtModeEnabled_CheckedChanged);
            // 
            // chbSync
            // 
            this.chbSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSync.AutoSize = true;
            this.chbSync.Checked = true;
            this.chbSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSync.Location = new System.Drawing.Point(1010, 6);
            this.chbSync.Name = "chbSync";
            this.chbSync.Size = new System.Drawing.Size(81, 17);
            this.chbSync.TabIndex = 18;
            this.chbSync.Text = "Sync boxes";
            this.chbSync.UseVisualStyleBackColor = true;
            this.chbSync.CheckedChanged += new System.EventHandler(this.chbSync_CheckedChanged);
            // 
            // labImageCount
            // 
            this.labImageCount.AccessibleDescription = "Go on image";
            this.labImageCount.AutoSize = true;
            this.labImageCount.Location = new System.Drawing.Point(3, 7);
            this.labImageCount.Name = "labImageCount";
            this.labImageCount.Size = new System.Drawing.Size(75, 13);
            this.labImageCount.TabIndex = 17;
            this.labImageCount.Text = "Current image:";
            // 
            // tbGoOnPicture
            // 
            this.tbGoOnPicture.AccessibleDescription = "Go on image";
            this.tbGoOnPicture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbGoOnPicture.Location = new System.Drawing.Point(590, 4);
            this.tbGoOnPicture.Name = "tbGoOnPicture";
            this.tbGoOnPicture.Size = new System.Drawing.Size(37, 20);
            this.tbGoOnPicture.TabIndex = 16;
            this.tbGoOnPicture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GoOnKeyPress);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Go on image";
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(542, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "GO ON";
            // 
            // pnlPreviousFrame
            // 
            this.pnlPreviousFrame.AccessibleDescription = "Previous frame";
            this.pnlPreviousFrame.AccessibleName = "";
            this.pnlPreviousFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreviousFrame.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPreviousFrame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPreviousFrame.BackgroundImage")));
            this.pnlPreviousFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlPreviousFrame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlPreviousFrame.Location = new System.Drawing.Point(1097, 0);
            this.pnlPreviousFrame.Name = "pnlPreviousFrame";
            this.pnlPreviousFrame.Size = new System.Drawing.Size(31, 30);
            this.pnlPreviousFrame.TabIndex = 14;
            this.pnlPreviousFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SetPreviousFrame);
            this.pnlPreviousFrame.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlPreviousFrame.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlNextFrame
            // 
            this.pnlNextFrame.AccessibleDescription = "Next frame";
            this.pnlNextFrame.AccessibleName = "";
            this.pnlNextFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNextFrame.BackColor = System.Drawing.SystemColors.Control;
            this.pnlNextFrame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlNextFrame.BackgroundImage")));
            this.pnlNextFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlNextFrame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlNextFrame.Location = new System.Drawing.Point(1134, 0);
            this.pnlNextFrame.Name = "pnlNextFrame";
            this.pnlNextFrame.Size = new System.Drawing.Size(31, 30);
            this.pnlNextFrame.TabIndex = 13;
            this.pnlNextFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SetNextFrame);
            this.pnlNextFrame.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlNextFrame.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1194, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToCompareToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addFilesToCompareToolStripMenuItem
            // 
            this.addFilesToCompareToolStripMenuItem.Name = "addFilesToCompareToolStripMenuItem";
            this.addFilesToCompareToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.addFilesToCompareToolStripMenuItem.Text = "Add Files";
            this.addFilesToCompareToolStripMenuItem.Click += new System.EventHandler(this.addFilesToCompareToolStripMenuItem_Click);
            // 
            // cmbGtTracks
            // 
            this.cmbGtTracks.FormattingEnabled = true;
            this.cmbGtTracks.Location = new System.Drawing.Point(113, 24);
            this.cmbGtTracks.Name = "cmbGtTracks";
            this.cmbGtTracks.Size = new System.Drawing.Size(180, 21);
            this.cmbGtTracks.TabIndex = 10;
            this.cmbGtTracks.SelectedValueChanged += new System.EventHandler(this.cmbGtTracks_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ground truth tracks:";
            // 
            // chListTracks
            // 
            this.chListTracks.CheckOnClick = true;
            this.chListTracks.FormattingEnabled = true;
            this.chListTracks.Location = new System.Drawing.Point(395, 24);
            this.chListTracks.Name = "chListTracks";
            this.chListTracks.Size = new System.Drawing.Size(157, 124);
            this.chListTracks.TabIndex = 12;
            this.chListTracks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chListTracks_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Compared tracks:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.chbUncategorized);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbGtTracks);
            this.groupBox1.Controls.Add(this.chListTracks);
            this.groupBox1.Location = new System.Drawing.Point(12, 441);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 166);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Track options";
            // 
            // chbUncategorized
            // 
            this.chbUncategorized.AutoSize = true;
            this.chbUncategorized.Location = new System.Drawing.Point(9, 58);
            this.chbUncategorized.Name = "chbUncategorized";
            this.chbUncategorized.Size = new System.Drawing.Size(154, 17);
            this.chbUncategorized.TabIndex = 14;
            this.chbUncategorized.Text = "Show uncategorized points";
            this.chbUncategorized.UseVisualStyleBackColor = true;
            this.chbUncategorized.CheckedChanged += new System.EventHandler(this.chbUncategorized_CheckedChanged);
            // 
            // TestEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 628);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1210, 667);
            this.Name = "TestEditorForm";
            this.Text = "Track Compare Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndWork);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyKeyDown);
            this.Resize += new System.EventHandler(this.TestEditorForm_Resize);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Emgu.CV.UI.ImageBox imageBox;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlPreviousFrame;
        private System.Windows.Forms.Panel pnlNextFrame;
        private System.Windows.Forms.TextBox tbGoOnPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labImageCount;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToCompareToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbSync;
        private System.Windows.Forms.CheckBox chbGtModeEnabled;
        private System.Windows.Forms.ComboBox cmbGtTracks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chListTracks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbUncategorized;
    }
}