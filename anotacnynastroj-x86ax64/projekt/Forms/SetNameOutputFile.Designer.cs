namespace Projekt.Forms
{
    partial class SetNameOutputFile
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
            this.label1 = new System.Windows.Forms.Label();
            this.gbSetOutputName = new System.Windows.Forms.GroupBox();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbCsvOption = new System.Windows.Forms.GroupBox();
            this.cmbClassID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbClassToSave = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbSetOutputName.SuspendLayout();
            this.gbCsvOption.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // gbSetOutputName
            // 
            this.gbSetOutputName.Controls.Add(this.cbName);
            this.gbSetOutputName.Controls.Add(this.btnSave);
            this.gbSetOutputName.Controls.Add(this.label1);
            this.gbSetOutputName.Location = new System.Drawing.Point(12, 91);
            this.gbSetOutputName.Name = "gbSetOutputName";
            this.gbSetOutputName.Size = new System.Drawing.Size(421, 64);
            this.gbSetOutputName.TabIndex = 1;
            this.gbSetOutputName.TabStop = false;
            this.gbSetOutputName.Text = "Set Name Of Output File";
            // 
            // cbName
            // 
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(133, 26);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(101, 21);
            this.cbName.TabIndex = 3;
            this.cbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValue);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // gbCsvOption
            // 
            this.gbCsvOption.Controls.Add(this.cmbClassID);
            this.gbCsvOption.Controls.Add(this.label3);
            this.gbCsvOption.Controls.Add(this.cmbClassToSave);
            this.gbCsvOption.Controls.Add(this.label2);
            this.gbCsvOption.Location = new System.Drawing.Point(192, 12);
            this.gbCsvOption.Name = "gbCsvOption";
            this.gbCsvOption.Size = new System.Drawing.Size(241, 73);
            this.gbCsvOption.TabIndex = 4;
            this.gbCsvOption.TabStop = false;
            this.gbCsvOption.Text = "Csv Options";
            // 
            // cmbClassID
            // 
            this.cmbClassID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassID.FormattingEnabled = true;
            this.cmbClassID.Location = new System.Drawing.Point(100, 42);
            this.cmbClassID.Name = "cmbClassID";
            this.cmbClassID.Size = new System.Drawing.Size(121, 21);
            this.cmbClassID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "Ak je vybrana konkretna trieda ktora sa ma ulozit, tak je mozne vybrat ako classI" +
    "D niektoru z hodnot atributov tejto tiredy. Inak je classID nazov triedy.";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ClassID";
            // 
            // cmbClassToSave
            // 
            this.cmbClassToSave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassToSave.FormattingEnabled = true;
            this.cmbClassToSave.Location = new System.Drawing.Point(100, 17);
            this.cmbClassToSave.Name = "cmbClassToSave";
            this.cmbClassToSave.Size = new System.Drawing.Size(121, 21);
            this.cmbClassToSave.TabIndex = 1;
            this.cmbClassToSave.SelectedIndexChanged += new System.EventHandler(this.SelectedClassToSaveChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Class To Save";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbFormat);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 73);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Output Format";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(12, 162);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(20, 23);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(55, 17);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(106, 21);
            this.cmbFormat.TabIndex = 5;
            this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.SelectedFormatChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Format";
            // 
            // SetNameOutputFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 191);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.gbSetOutputName);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbCsvOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetNameOutputFile";
            this.Text = "Set Output Name";
            this.gbSetOutputName.ResumeLayout(false);
            this.gbSetOutputName.PerformLayout();
            this.gbCsvOption.ResumeLayout(false);
            this.gbCsvOption.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbSetOutputName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.GroupBox gbCsvOption;
        private System.Windows.Forms.ComboBox cmbClassID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbClassToSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label label4;
    }
}