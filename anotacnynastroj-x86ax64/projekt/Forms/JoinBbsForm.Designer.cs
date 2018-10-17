namespace Projekt.Forms
{
    partial class JoinBbsForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMWeight = new System.Windows.Forms.NumericUpDown();
            this.groupBoxMetrics = new System.Windows.Forms.GroupBox();
            this.radioButtonPercent = new System.Windows.Forms.RadioButton();
            this.radioButtonPixel = new System.Windows.Forms.RadioButton();
            this.numericUpDownWWeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWSWindow = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMWeight)).BeginInit();
            this.groupBoxMetrics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 283);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(199, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDown5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDown4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDownMWeight);
            this.groupBox1.Controls.Add(this.groupBoxMetrics);
            this.groupBox1.Controls.Add(this.numericUpDownWWeight);
            this.groupBox1.Controls.Add(this.numericUpDownWSWindow);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 264);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Visual relation weight";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.DecimalPlaces = 1;
            this.numericUpDown5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown5.Location = new System.Drawing.Point(125, 226);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown5.TabIndex = 15;
            this.numericUpDown5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Minimum difference";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(125, 200);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown4.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Movement weight";
            // 
            // numericUpDownMWeight
            // 
            this.numericUpDownMWeight.DecimalPlaces = 1;
            this.numericUpDownMWeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMWeight.Location = new System.Drawing.Point(125, 174);
            this.numericUpDownMWeight.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMWeight.Name = "numericUpDownMWeight";
            this.numericUpDownMWeight.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownMWeight.TabIndex = 11;
            this.numericUpDownMWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBoxMetrics
            // 
            this.groupBoxMetrics.Controls.Add(this.radioButtonPercent);
            this.groupBoxMetrics.Controls.Add(this.radioButtonPixel);
            this.groupBoxMetrics.Location = new System.Drawing.Point(9, 55);
            this.groupBoxMetrics.Name = "groupBoxMetrics";
            this.groupBoxMetrics.Size = new System.Drawing.Size(317, 52);
            this.groupBoxMetrics.TabIndex = 10;
            this.groupBoxMetrics.TabStop = false;
            this.groupBoxMetrics.Text = "Search window metrics";
            // 
            // radioButtonPercent
            // 
            this.radioButtonPercent.AutoSize = true;
            this.radioButtonPercent.Location = new System.Drawing.Point(64, 20);
            this.radioButtonPercent.Name = "radioButtonPercent";
            this.radioButtonPercent.Size = new System.Drawing.Size(61, 17);
            this.radioButtonPercent.TabIndex = 1;
            this.radioButtonPercent.Text = "percent";
            this.radioButtonPercent.UseVisualStyleBackColor = true;
            // 
            // radioButtonPixel
            // 
            this.radioButtonPixel.AutoSize = true;
            this.radioButtonPixel.Checked = true;
            this.radioButtonPixel.Location = new System.Drawing.Point(7, 20);
            this.radioButtonPixel.Name = "radioButtonPixel";
            this.radioButtonPixel.Size = new System.Drawing.Size(51, 17);
            this.radioButtonPixel.TabIndex = 0;
            this.radioButtonPixel.TabStop = true;
            this.radioButtonPixel.Text = "pixels";
            this.radioButtonPixel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWWeight
            // 
            this.numericUpDownWWeight.DecimalPlaces = 1;
            this.numericUpDownWWeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownWWeight.Location = new System.Drawing.Point(252, 122);
            this.numericUpDownWWeight.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWWeight.Name = "numericUpDownWWeight";
            this.numericUpDownWWeight.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownWWeight.TabIndex = 9;
            this.numericUpDownWWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownWSWindow
            // 
            this.numericUpDownWSWindow.DecimalPlaces = 1;
            this.numericUpDownWSWindow.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownWSWindow.Location = new System.Drawing.Point(252, 20);
            this.numericUpDownWSWindow.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWSWindow.Name = "numericUpDownWSWindow";
            this.numericUpDownWSWindow.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownWSWindow.TabIndex = 8;
            this.numericUpDownWSWindow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ignored frames";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(125, 148);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown3.TabIndex = 4;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(125, 122);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size change";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(125, 20);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size of search window";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.HelpClick);
            // 
            // JoinBbsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 318);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "JoinBbsForm";
            this.Text = "Tracing objects";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingCancelWorker);
            this.Load += new System.EventHandler(this.JoinBbsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMWeight)).EndInit();
            this.groupBoxMetrics.ResumeLayout(false);
            this.groupBoxMetrics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownWWeight;
        private System.Windows.Forms.NumericUpDown numericUpDownWSWindow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxMetrics;
        private System.Windows.Forms.RadioButton radioButtonPercent;
        private System.Windows.Forms.RadioButton radioButtonPixel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Button button2;
    }
}