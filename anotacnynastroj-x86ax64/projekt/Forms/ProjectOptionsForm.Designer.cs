namespace Projekt.Forms
{
    partial class ProjectOptionsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trbCreatedClrOpacity = new System.Windows.Forms.TrackBar();
            this.pnlToolCreatedClr = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trbCreatingClrOpacity = new System.Windows.Forms.TrackBar();
            this.pnlToolCreatingClr = new System.Windows.Forms.Panel();
            this.nudToolWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTool = new System.Windows.Forms.Label();
            this.cmbTools = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlGuideLineOptions = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.trbGuideLineOpacity = new System.Windows.Forms.TrackBar();
            this.pnlGuideLineClr = new System.Windows.Forms.Panel();
            this.nudGuideLine = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbShowGuideLine = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudFontWidth = new System.Windows.Forms.NumericUpDown();
            this.lTxtSize = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbCreatedClrOpacity)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbCreatingClrOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlGuideLineOptions.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbGuideLineOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGuideLine)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontWidth)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.nudToolWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTool);
            this.groupBox1.Controls.Add(this.cmbTools);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools Options";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.trbCreatedClrOpacity);
            this.groupBox4.Controls.Add(this.pnlToolCreatedClr);
            this.groupBox4.Location = new System.Drawing.Point(111, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(99, 100);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Created Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Opacity";
            // 
            // trbCreatedClrOpacity
            // 
            this.trbCreatedClrOpacity.Location = new System.Drawing.Point(6, 49);
            this.trbCreatedClrOpacity.Maximum = 255;
            this.trbCreatedClrOpacity.Minimum = 1;
            this.trbCreatedClrOpacity.Name = "trbCreatedClrOpacity";
            this.trbCreatedClrOpacity.Size = new System.Drawing.Size(87, 45);
            this.trbCreatedClrOpacity.TabIndex = 6;
            this.trbCreatedClrOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbCreatedClrOpacity.Value = 255;
            this.trbCreatedClrOpacity.Scroll += new System.EventHandler(this.CreatedColorOpacityChange);
            // 
            // pnlToolCreatedClr
            // 
            this.pnlToolCreatedClr.AccessibleDescription = "Click To Change";
            this.pnlToolCreatedClr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolCreatedClr.Location = new System.Drawing.Point(6, 19);
            this.pnlToolCreatedClr.Name = "pnlToolCreatedClr";
            this.pnlToolCreatedClr.Size = new System.Drawing.Size(87, 25);
            this.pnlToolCreatedClr.TabIndex = 2;
            this.pnlToolCreatedClr.Click += new System.EventHandler(this.PnlToolCreatedColorClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.trbCreatingClrOpacity);
            this.groupBox3.Controls.Add(this.pnlToolCreatingClr);
            this.groupBox3.Location = new System.Drawing.Point(6, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(99, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Creating Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Opacity";
            // 
            // trbCreatingClrOpacity
            // 
            this.trbCreatingClrOpacity.Location = new System.Drawing.Point(6, 49);
            this.trbCreatingClrOpacity.Maximum = 255;
            this.trbCreatingClrOpacity.Minimum = 1;
            this.trbCreatingClrOpacity.Name = "trbCreatingClrOpacity";
            this.trbCreatingClrOpacity.Size = new System.Drawing.Size(87, 45);
            this.trbCreatingClrOpacity.TabIndex = 10;
            this.trbCreatingClrOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbCreatingClrOpacity.Value = 255;
            this.trbCreatingClrOpacity.Scroll += new System.EventHandler(this.CreatingColorOpacityChange);
            // 
            // pnlToolCreatingClr
            // 
            this.pnlToolCreatingClr.AccessibleDescription = "Click To Change";
            this.pnlToolCreatingClr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolCreatingClr.Location = new System.Drawing.Point(6, 19);
            this.pnlToolCreatingClr.Name = "pnlToolCreatingClr";
            this.pnlToolCreatingClr.Size = new System.Drawing.Size(87, 25);
            this.pnlToolCreatingClr.TabIndex = 1;
            this.pnlToolCreatingClr.Click += new System.EventHandler(this.PnlToolCreatingColorClick);
            // 
            // nudToolWidth
            // 
            this.nudToolWidth.Location = new System.Drawing.Point(107, 47);
            this.nudToolWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudToolWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudToolWidth.Name = "nudToolWidth";
            this.nudToolWidth.Size = new System.Drawing.Size(97, 20);
            this.nudToolWidth.TabIndex = 1;
            this.nudToolWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width";
            // 
            // lblTool
            // 
            this.lblTool.AutoSize = true;
            this.lblTool.Location = new System.Drawing.Point(12, 22);
            this.lblTool.Name = "lblTool";
            this.lblTool.Size = new System.Drawing.Size(35, 13);
            this.lblTool.TabIndex = 3;
            this.lblTool.Text = "Name";
            // 
            // cmbTools
            // 
            this.cmbTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTools.FormattingEnabled = true;
            this.cmbTools.Location = new System.Drawing.Point(107, 19);
            this.cmbTools.Name = "cmbTools";
            this.cmbTools.Size = new System.Drawing.Size(97, 21);
            this.cmbTools.TabIndex = 2;
            this.cmbTools.DropDown += new System.EventHandler(this.CmbToolsDropDown);
            this.cmbTools.SelectedIndexChanged += new System.EventHandler(this.SelectedToolOptionChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(381, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlGuideLineOptions);
            this.groupBox2.Controls.Add(this.cbShowGuideLine);
            this.groupBox2.Location = new System.Drawing.Point(229, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 152);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Guide Line Options";
            // 
            // pnlGuideLineOptions
            // 
            this.pnlGuideLineOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGuideLineOptions.Controls.Add(this.groupBox5);
            this.pnlGuideLineOptions.Controls.Add(this.nudGuideLine);
            this.pnlGuideLineOptions.Controls.Add(this.label2);
            this.pnlGuideLineOptions.Location = new System.Drawing.Point(3, 41);
            this.pnlGuideLineOptions.Name = "pnlGuideLineOptions";
            this.pnlGuideLineOptions.Size = new System.Drawing.Size(194, 103);
            this.pnlGuideLineOptions.TabIndex = 6;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.trbGuideLineOpacity);
            this.groupBox5.Controls.Add(this.pnlGuideLineClr);
            this.groupBox5.Location = new System.Drawing.Point(3, 26);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(185, 74);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Color And Opacity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Opacity";
            // 
            // trbGuideLineOpacity
            // 
            this.trbGuideLineOpacity.Location = new System.Drawing.Point(1, 25);
            this.trbGuideLineOpacity.Maximum = 255;
            this.trbGuideLineOpacity.Minimum = 1;
            this.trbGuideLineOpacity.Name = "trbGuideLineOpacity";
            this.trbGuideLineOpacity.Size = new System.Drawing.Size(87, 45);
            this.trbGuideLineOpacity.TabIndex = 10;
            this.trbGuideLineOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbGuideLineOpacity.Value = 255;
            this.trbGuideLineOpacity.Scroll += new System.EventHandler(this.GuideLineOpacityChanged);
            // 
            // pnlGuideLineClr
            // 
            this.pnlGuideLineClr.AccessibleDescription = "Click To Change";
            this.pnlGuideLineClr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGuideLineClr.Location = new System.Drawing.Point(107, 25);
            this.pnlGuideLineClr.Name = "pnlGuideLineClr";
            this.pnlGuideLineClr.Size = new System.Drawing.Size(72, 23);
            this.pnlGuideLineClr.TabIndex = 1;
            this.pnlGuideLineClr.Click += new System.EventHandler(this.PnlGuideLineColorClick);
            // 
            // nudGuideLine
            // 
            this.nudGuideLine.Location = new System.Drawing.Point(91, 3);
            this.nudGuideLine.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudGuideLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGuideLine.Name = "nudGuideLine";
            this.nudGuideLine.Size = new System.Drawing.Size(97, 20);
            this.nudGuideLine.TabIndex = 1;
            this.nudGuideLine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width";
            // 
            // cbShowGuideLine
            // 
            this.cbShowGuideLine.AutoSize = true;
            this.cbShowGuideLine.Checked = true;
            this.cbShowGuideLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowGuideLine.Location = new System.Drawing.Point(7, 21);
            this.cbShowGuideLine.Name = "cbShowGuideLine";
            this.cbShowGuideLine.Size = new System.Drawing.Size(151, 17);
            this.cbShowGuideLine.TabIndex = 5;
            this.cbShowGuideLine.Text = "Show Guide Line In Image";
            this.cbShowGuideLine.UseVisualStyleBackColor = true;
            this.cbShowGuideLine.CheckedChanged += new System.EventHandler(this.SHowGuideLineChanage);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(358, 231);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(17, 23);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.nudFontWidth);
            this.groupBox6.Controls.Add(this.lTxtSize);
            this.groupBox6.Location = new System.Drawing.Point(7, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(216, 121);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Class ID options";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.panel2);
            this.groupBox8.Location = new System.Drawing.Point(111, 54);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(99, 57);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Rectangle color";
            // 
            // panel2
            // 
            this.panel2.AccessibleDescription = "Click To Change";
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(6, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 25);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.PnlRectangleColorClick);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.panel1);
            this.groupBox7.Location = new System.Drawing.Point(6, 54);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(99, 57);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Text color";
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "Click To Change";
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 25);
            this.panel1.TabIndex = 1;
            this.panel1.Click += new System.EventHandler(this.PnlTextColorClick);
            // 
            // nudFontWidth
            // 
            this.nudFontWidth.Location = new System.Drawing.Point(107, 24);
            this.nudFontWidth.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudFontWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFontWidth.Name = "nudFontWidth";
            this.nudFontWidth.Size = new System.Drawing.Size(97, 20);
            this.nudFontWidth.TabIndex = 8;
            this.nudFontWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lTxtSize
            // 
            this.lTxtSize.AutoSize = true;
            this.lTxtSize.Location = new System.Drawing.Point(12, 26);
            this.lTxtSize.Name = "lTxtSize";
            this.lTxtSize.Size = new System.Drawing.Size(49, 13);
            this.lTxtSize.TabIndex = 8;
            this.lTxtSize.Text = "Text size";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(443, 221);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(435, 195);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "General";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(435, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Class Id";
            // 
            // ProjectOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 266);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProjectOptionsForm";
            this.Text = "Project Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbCreatedClrOpacity)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbCreatingClrOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlGuideLineOptions.ResumeLayout(false);
            this.pnlGuideLineOptions.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbGuideLineOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGuideLine)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFontWidth)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlToolCreatingClr;
        private System.Windows.Forms.Label lblTool;
        private System.Windows.Forms.ComboBox cmbTools;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudToolWidth;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudGuideLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlGuideLineClr;
        private System.Windows.Forms.Panel pnlGuideLineOptions;
        private System.Windows.Forms.CheckBox cbShowGuideLine;
        private System.Windows.Forms.Panel pnlToolCreatedClr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trbCreatedClrOpacity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trbCreatingClrOpacity;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trbGuideLineOpacity;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudFontWidth;
        private System.Windows.Forms.Label lTxtSize;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
    }
}