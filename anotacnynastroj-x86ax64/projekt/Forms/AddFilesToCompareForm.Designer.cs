namespace Projekt.Forms
{
    partial class AddFilesToCompareForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxGt = new System.Windows.Forms.TextBox();
            this.chbGt = new System.Windows.Forms.CheckBox();
            this.chbTest = new System.Windows.Forms.CheckBox();
            this.txtBoxTest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chbTest);
            this.panel1.Controls.Add(this.txtBoxTest);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.chbGt);
            this.panel1.Controls.Add(this.txtBoxGt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 107);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ground truth:";
            // 
            // txtBoxGt
            // 
            this.txtBoxGt.Location = new System.Drawing.Point(17, 27);
            this.txtBoxGt.Name = "txtBoxGt";
            this.txtBoxGt.Size = new System.Drawing.Size(258, 20);
            this.txtBoxGt.TabIndex = 1;
            this.txtBoxGt.Click += new System.EventHandler(this.txtBoxGt_Click);
            // 
            // chbGt
            // 
            this.chbGt.AutoSize = true;
            this.chbGt.Location = new System.Drawing.Point(281, 29);
            this.chbGt.Name = "chbGt";
            this.chbGt.Size = new System.Drawing.Size(80, 17);
            this.chbGt.TabIndex = 3;
            this.chbGt.Text = "Use project";
            this.chbGt.UseVisualStyleBackColor = true;
            this.chbGt.CheckedChanged += new System.EventHandler(this.chbGt_CheckedChanged);
            // 
            // chbTest
            // 
            this.chbTest.AutoSize = true;
            this.chbTest.Location = new System.Drawing.Point(281, 68);
            this.chbTest.Name = "chbTest";
            this.chbTest.Size = new System.Drawing.Size(80, 17);
            this.chbTest.TabIndex = 7;
            this.chbTest.Text = "Use project";
            this.chbTest.UseVisualStyleBackColor = true;
            this.chbTest.CheckedChanged += new System.EventHandler(this.chbTest_CheckedChanged);
            // 
            // txtBoxTest
            // 
            this.txtBoxTest.Location = new System.Drawing.Point(17, 66);
            this.txtBoxTest.Name = "txtBoxTest";
            this.txtBoxTest.Size = new System.Drawing.Size(258, 20);
            this.txtBoxTest.TabIndex = 5;
            this.txtBoxTest.Click += new System.EventHandler(this.txtBoxTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Test file:";
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(316, 125);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 1;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // AddFilesToCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 158);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddFilesToCompareForm";
            this.Text = "Add Files To Compare";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbTest;
        private System.Windows.Forms.TextBox txtBoxTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbGt;
        private System.Windows.Forms.TextBox txtBoxGt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butOk;
    }
}