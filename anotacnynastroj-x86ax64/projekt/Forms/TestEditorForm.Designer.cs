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
            this.chbRememberZoom = new System.Windows.Forms.CheckBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.tbFNPerc = new System.Windows.Forms.TextBox();
            this.tbFPPerc = new System.Windows.Forms.TextBox();
            this.tbTPPerc = new System.Windows.Forms.TextBox();
            this.tbFNegatives = new System.Windows.Forms.TextBox();
            this.tbFPositives = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTPositives = new System.Windows.Forms.TextBox();
            this.tbStatistic = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbFNegSel = new System.Windows.Forms.TextBox();
            this.tbFPosSel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbTPosSel = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.nmTruePositives = new System.Windows.Forms.NumericUpDown();
            this.nmFalsePositives = new System.Windows.Forms.NumericUpDown();
            this.nmFalseNegatives = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmTruePositives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFalsePositives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFalseNegatives)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1170, 369);
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
            this.splitContainer1.Size = new System.Drawing.Size(1170, 369);
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
            this.imageBox.Size = new System.Drawing.Size(585, 369);
            this.imageBox.TabIndex = 4;
            this.imageBox.TabStop = false;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawToImageBox1);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseDown);
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
            this.imageBox1.Size = new System.Drawing.Size(581, 369);
            this.imageBox1.TabIndex = 4;
            this.imageBox1.TabStop = false;
            this.imageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawToImageBox2);
            this.imageBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseDown);
            this.imageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chbRememberZoom);
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
            // chbRememberZoom
            // 
            this.chbRememberZoom.AccessibleDescription = "";
            this.chbRememberZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbRememberZoom.AutoSize = true;
            this.chbRememberZoom.Location = new System.Drawing.Point(837, 6);
            this.chbRememberZoom.Name = "chbRememberZoom";
            this.chbRememberZoom.Size = new System.Drawing.Size(84, 17);
            this.chbRememberZoom.TabIndex = 20;
            this.chbRememberZoom.Text = "RetainZoom";
            this.chbRememberZoom.UseVisualStyleBackColor = true;
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
            this.groupBox1.Location = new System.Drawing.Point(12, 450);
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(603, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 166);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stats";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(366, 141);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 115);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Full";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbFNPerc, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFPPerc, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbTPPerc, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbFNegatives, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFPositives, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbTPositives, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbStatistic, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 101);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(3, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 26);
            this.label13.TabIndex = 19;
            this.label13.Text = "Statistic:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbFNPerc
            // 
            this.tbFNPerc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFNPerc.Location = new System.Drawing.Point(207, 53);
            this.tbFNPerc.Name = "tbFNPerc";
            this.tbFNPerc.ReadOnly = true;
            this.tbFNPerc.Size = new System.Drawing.Size(105, 20);
            this.tbFNPerc.TabIndex = 18;
            // 
            // tbFPPerc
            // 
            this.tbFPPerc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFPPerc.Location = new System.Drawing.Point(207, 28);
            this.tbFPPerc.Name = "tbFPPerc";
            this.tbFPPerc.ReadOnly = true;
            this.tbFPPerc.Size = new System.Drawing.Size(105, 20);
            this.tbFPPerc.TabIndex = 17;
            // 
            // tbTPPerc
            // 
            this.tbTPPerc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTPPerc.Location = new System.Drawing.Point(207, 3);
            this.tbTPPerc.Name = "tbTPPerc";
            this.tbTPPerc.ReadOnly = true;
            this.tbTPPerc.Size = new System.Drawing.Size(105, 20);
            this.tbTPPerc.TabIndex = 16;
            // 
            // tbFNegatives
            // 
            this.tbFNegatives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFNegatives.Location = new System.Drawing.Point(97, 53);
            this.tbFNegatives.Name = "tbFNegatives";
            this.tbFNegatives.ReadOnly = true;
            this.tbFNegatives.Size = new System.Drawing.Size(104, 20);
            this.tbFNegatives.TabIndex = 5;
            // 
            // tbFPositives
            // 
            this.tbFPositives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFPositives.Location = new System.Drawing.Point(97, 28);
            this.tbFPositives.Name = "tbFPositives";
            this.tbFPositives.ReadOnly = true;
            this.tbFPositives.Size = new System.Drawing.Size(104, 20);
            this.tbFPositives.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "True Positives:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "False Positives:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "False Negatives:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTPositives
            // 
            this.tbTPositives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTPositives.Location = new System.Drawing.Point(97, 3);
            this.tbTPositives.Name = "tbTPositives";
            this.tbTPositives.ReadOnly = true;
            this.tbTPositives.Size = new System.Drawing.Size(104, 20);
            this.tbTPositives.TabIndex = 3;
            // 
            // tbStatistic
            // 
            this.tbStatistic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStatistic.Location = new System.Drawing.Point(97, 78);
            this.tbStatistic.Name = "tbStatistic";
            this.tbStatistic.ReadOnly = true;
            this.tbStatistic.Size = new System.Drawing.Size(104, 20);
            this.tbStatistic.TabIndex = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 115);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Selected";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.tbFNegSel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbFPosSel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbTPosSel, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(222, 81);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tbFNegSel
            // 
            this.tbFNegSel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFNegSel.Location = new System.Drawing.Point(105, 57);
            this.tbFNegSel.Name = "tbFNegSel";
            this.tbFNegSel.ReadOnly = true;
            this.tbFNegSel.Size = new System.Drawing.Size(114, 20);
            this.tbFNegSel.TabIndex = 5;
            // 
            // tbFPosSel
            // 
            this.tbFPosSel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFPosSel.Location = new System.Drawing.Point(105, 30);
            this.tbFPosSel.Name = "tbFPosSel";
            this.tbFPosSel.ReadOnly = true;
            this.tbFPosSel.Size = new System.Drawing.Size(114, 20);
            this.tbFPosSel.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 27);
            this.label7.TabIndex = 0;
            this.label7.Text = "True Positives:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(3, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 27);
            this.label8.TabIndex = 1;
            this.label8.Text = "False Positives:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(3, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 27);
            this.label9.TabIndex = 2;
            this.label9.Text = "False Negatives:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTPosSel
            // 
            this.tbTPosSel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTPosSel.Location = new System.Drawing.Point(105, 3);
            this.tbTPosSel.Name = "tbTPosSel";
            this.tbTPosSel.ReadOnly = true;
            this.tbTPosSel.Size = new System.Drawing.Size(114, 20);
            this.tbTPosSel.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(358, 115);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.nmTruePositives, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.nmFalsePositives, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.nmFalseNegatives, 1, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(222, 78);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 26);
            this.label10.TabIndex = 0;
            this.label10.Text = "True Positives:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(3, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 26);
            this.label11.TabIndex = 1;
            this.label11.Text = "False Positives:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label12.Location = new System.Drawing.Point(3, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 26);
            this.label12.TabIndex = 2;
            this.label12.Text = "False Negatives:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmTruePositives
            // 
            this.nmTruePositives.Location = new System.Drawing.Point(105, 3);
            this.nmTruePositives.Name = "nmTruePositives";
            this.nmTruePositives.Size = new System.Drawing.Size(114, 20);
            this.nmTruePositives.TabIndex = 3;
            this.nmTruePositives.ValueChanged += new System.EventHandler(this.nmTruePositives_ValueChanged);
            // 
            // nmFalsePositives
            // 
            this.nmFalsePositives.Location = new System.Drawing.Point(105, 29);
            this.nmFalsePositives.Name = "nmFalsePositives";
            this.nmFalsePositives.Size = new System.Drawing.Size(114, 20);
            this.nmFalsePositives.TabIndex = 4;
            this.nmFalsePositives.ValueChanged += new System.EventHandler(this.nmFalsePositives_ValueChanged);
            // 
            // nmFalseNegatives
            // 
            this.nmFalseNegatives.Location = new System.Drawing.Point(105, 55);
            this.nmFalseNegatives.Name = "nmFalseNegatives";
            this.nmFalseNegatives.Size = new System.Drawing.Size(114, 20);
            this.nmFalseNegatives.TabIndex = 5;
            this.nmFalseNegatives.ValueChanged += new System.EventHandler(this.nmFalseNegatives_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 625);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1194, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.MarqueeAnimationSpeed = 70;
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
            this.tsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tsProgressBar.Visible = false;
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(93, 17);
            this.tsStatusLabel.Text = "Counting stats...";
            this.tsStatusLabel.Visible = false;
            // 
            // TestEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 647);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1210, 686);
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
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmTruePositives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFalsePositives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFalseNegatives)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.CheckBox chbRememberZoom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbFNegatives;
        private System.Windows.Forms.TextBox tbFPositives;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTPositives;
        private System.Windows.Forms.TextBox tbFNPerc;
        private System.Windows.Forms.TextBox tbFPPerc;
        private System.Windows.Forms.TextBox tbTPPerc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbFNegSel;
        private System.Windows.Forms.TextBox tbFPosSel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbTPosSel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbStatistic;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nmTruePositives;
        private System.Windows.Forms.NumericUpDown nmFalsePositives;
        private System.Windows.Forms.NumericUpDown nmFalseNegatives;
    }
}