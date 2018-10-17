namespace Projekt.Forms
{
    partial class SetCsvOptions
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
            this.cmbFormatType = new System.Windows.Forms.ComboBox();
            this.cmbClassId = new System.Windows.Forms.ComboBox();
            this.cmbClassesBB = new System.Windows.Forms.ComboBox();
            this.lblClassId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbFormatType);
            this.groupBox1.Controls.Add(this.cmbClassId);
            this.groupBox1.Controls.Add(this.cmbClassesBB);
            this.groupBox1.Controls.Add(this.lblClassId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "akemu atributu danej triedy sa priradi hodnota ClassId zo suboru csv";
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Format Type";
            // 
            // cmbFormatType
            // 
            this.cmbFormatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormatType.FormattingEnabled = true;
            this.cmbFormatType.Location = new System.Drawing.Point(138, 20);
            this.cmbFormatType.Name = "cmbFormatType";
            this.cmbFormatType.Size = new System.Drawing.Size(121, 21);
            this.cmbFormatType.TabIndex = 4;
            this.cmbFormatType.SelectedIndexChanged += new System.EventHandler(this.CmbFormatTypeSelectedIndexChanged);
            // 
            // cmbClassId
            // 
            this.cmbClassId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassId.FormattingEnabled = true;
            this.cmbClassId.Location = new System.Drawing.Point(138, 75);
            this.cmbClassId.Name = "cmbClassId";
            this.cmbClassId.Size = new System.Drawing.Size(121, 21);
            this.cmbClassId.TabIndex = 3;
            // 
            // cmbClassesBB
            // 
            this.cmbClassesBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassesBB.FormattingEnabled = true;
            this.cmbClassesBB.Location = new System.Drawing.Point(138, 47);
            this.cmbClassesBB.Name = "cmbClassesBB";
            this.cmbClassesBB.Size = new System.Drawing.Size(121, 21);
            this.cmbClassesBB.TabIndex = 1;
            this.cmbClassesBB.SelectedIndexChanged += new System.EventHandler(this.SelectedClassBBsChanged);
            // 
            // lblClassId
            // 
            this.lblClassId.AccessibleDescription = "akemu atributu danej triedy sa priradi hodnota ClassId zo suboru csv";
            this.lblClassId.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.lblClassId.AutoSize = true;
            this.lblClassId.Location = new System.Drawing.Point(9, 78);
            this.lblClassId.Name = "lblClassId";
            this.lblClassId.Size = new System.Drawing.Size(43, 13);
            this.lblClassId.TabIndex = 2;
            this.lblClassId.Text = "ClassID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Class Of BoundingBoxes";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(206, 118);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(3, 118);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(20, 23);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
            // 
            // SetCsvOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 146);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetCsvOptions";
            this.Text = "Set Options For Csv Format";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbClassId;
        private System.Windows.Forms.ComboBox cmbClassesBB;
        private System.Windows.Forms.Label lblClassId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFormatType;
    }
}