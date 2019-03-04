namespace Projekt.Forms
{
    partial class TrackEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackEditorForm));
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPreviousFrame = new System.Windows.Forms.Panel();
            this.pnlNextFrame = new System.Windows.Forms.Panel();
            this.pnlSplitTracks = new System.Windows.Forms.Panel();
            this.pnlConnectTracks = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbStartPoint = new System.Windows.Forms.RadioButton();
            this.rbEndPoint = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.Color.White;
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.imageBox.Location = new System.Drawing.Point(12, 51);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(960, 539);
            this.imageBox.TabIndex = 3;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.ImgBoxClick);
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawToImageBox);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseDown);
            this.imageBox.MouseEnter += new System.EventHandler(this.ImgBoxMouseEnter);
            this.imageBox.MouseLeave += new System.EventHandler(this.ImgBoxMouseLeave);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseUp);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(981, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(157, 319);
            this.checkedListBox1.TabIndex = 4;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.StateChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(978, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gap:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(981, 359);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.GapChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlPreviousFrame);
            this.panel1.Controls.Add(this.pnlNextFrame);
            this.panel1.Controls.Add(this.pnlSplitTracks);
            this.panel1.Controls.Add(this.pnlConnectTracks);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 31);
            this.panel1.TabIndex = 7;
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
            this.pnlPreviousFrame.Location = new System.Drawing.Point(887, 0);
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
            this.pnlNextFrame.Location = new System.Drawing.Point(924, 0);
            this.pnlNextFrame.Name = "pnlNextFrame";
            this.pnlNextFrame.Size = new System.Drawing.Size(31, 30);
            this.pnlNextFrame.TabIndex = 13;
            this.pnlNextFrame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SetNextFrame);
            this.pnlNextFrame.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlNextFrame.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlSplitTracks
            // 
            this.pnlSplitTracks.AccessibleDescription = "Split two tracks";
            this.pnlSplitTracks.AccessibleName = "";
            this.pnlSplitTracks.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSplitTracks.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSplitTracks.BackgroundImage")));
            this.pnlSplitTracks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSplitTracks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSplitTracks.Location = new System.Drawing.Point(40, 0);
            this.pnlSplitTracks.Name = "pnlSplitTracks";
            this.pnlSplitTracks.Size = new System.Drawing.Size(31, 30);
            this.pnlSplitTracks.TabIndex = 12;
            this.pnlSplitTracks.Click += new System.EventHandler(this.SelectSplitTracks);
            this.pnlSplitTracks.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlSplitTracks.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlConnectTracks
            // 
            this.pnlConnectTracks.AccessibleDescription = "Make connection between two tracks";
            this.pnlConnectTracks.AccessibleName = "";
            this.pnlConnectTracks.BackColor = System.Drawing.SystemColors.Control;
            this.pnlConnectTracks.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlConnectTracks.BackgroundImage")));
            this.pnlConnectTracks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlConnectTracks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlConnectTracks.Location = new System.Drawing.Point(3, 0);
            this.pnlConnectTracks.Name = "pnlConnectTracks";
            this.pnlConnectTracks.Size = new System.Drawing.Size(31, 30);
            this.pnlConnectTracks.TabIndex = 11;
            this.pnlConnectTracks.Click += new System.EventHandler(this.SelectConnTracks);
            this.pnlConnectTracks.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlConnectTracks.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(981, 385);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 92);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect options";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(35, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "all";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "tracks";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.DisplayStyleChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "points";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.DisplayStyleChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(984, 482);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Frames+:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(987, 498);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown2.TabIndex = 10;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.FramesChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbStartPoint);
            this.groupBox2.Controls.Add(this.rbEndPoint);
            this.groupBox2.Location = new System.Drawing.Point(981, 524);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 66);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Split options";
            // 
            // rbStartPoint
            // 
            this.rbStartPoint.AutoSize = true;
            this.rbStartPoint.Location = new System.Drawing.Point(6, 42);
            this.rbStartPoint.Name = "rbStartPoint";
            this.rbStartPoint.Size = new System.Drawing.Size(71, 17);
            this.rbStartPoint.TabIndex = 1;
            this.rbStartPoint.Text = "start point";
            this.rbStartPoint.UseVisualStyleBackColor = true;
            // 
            // rbEndPoint
            // 
            this.rbEndPoint.AutoSize = true;
            this.rbEndPoint.Checked = true;
            this.rbEndPoint.Location = new System.Drawing.Point(6, 19);
            this.rbEndPoint.Name = "rbEndPoint";
            this.rbEndPoint.Size = new System.Drawing.Size(69, 17);
            this.rbEndPoint.TabIndex = 0;
            this.rbEndPoint.TabStop = true;
            this.rbEndPoint.Text = "end point";
            this.rbEndPoint.UseVisualStyleBackColor = true;
            // 
            // TrackEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 645);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.imageBox);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1166, 683);
            this.Name = "TrackEditorForm";
            this.Text = "Track Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EndWork);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyKeyDown);
            this.Resize += new System.EventHandler(this.FormResize);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlConnectTracks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Panel pnlSplitTracks;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbStartPoint;
        private System.Windows.Forms.RadioButton rbEndPoint;
        private System.Windows.Forms.Panel pnlPreviousFrame;
        private System.Windows.Forms.Panel pnlNextFrame;
    }
}