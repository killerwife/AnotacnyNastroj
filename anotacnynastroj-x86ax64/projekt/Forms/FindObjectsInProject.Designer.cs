namespace Projekt.Forms
{
    partial class FindObjectsInProject
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
            this.label2 = new System.Windows.Forms.Label();
            this.gbClassProps = new System.Windows.Forms.GroupBox();
            this.cmbAllClasses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.gbClassProps);
            this.groupBox1.Controls.Add(this.cmbAllClasses);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 316);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Finding Property";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Finding";
            // 
            // gbClassProps
            // 
            this.gbClassProps.Location = new System.Drawing.Point(6, 60);
            this.gbClassProps.Name = "gbClassProps";
            this.gbClassProps.Size = new System.Drawing.Size(240, 250);
            this.gbClassProps.TabIndex = 2;
            this.gbClassProps.TabStop = false;
            this.gbClassProps.Text = "Class Property";
            // 
            // cmbAllClasses
            // 
            this.cmbAllClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllClasses.FormattingEnabled = true;
            this.cmbAllClasses.Location = new System.Drawing.Point(74, 23);
            this.cmbAllClasses.Name = "cmbAllClasses";
            this.cmbAllClasses.Size = new System.Drawing.Size(103, 21);
            this.cmbAllClasses.TabIndex = 1;
            this.cmbAllClasses.SelectedIndexChanged += new System.EventHandler(this.SelectedClassChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(182, 324);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.BtnFindClick);
            // 
            // FindObjectsInProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 352);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FindObjectsInProject";
            this.Text = "FindObjectsInProject";
            this.Activated += new System.EventHandler(this.FormActivated);
            this.Deactivate += new System.EventHandler(this.FormDeactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyFormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbClassProps;
        private System.Windows.Forms.ComboBox cmbAllClasses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label2;
    }
}