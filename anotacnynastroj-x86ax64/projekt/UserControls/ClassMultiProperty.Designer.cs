namespace Projekt.UserControls
{
    partial class ClassMultiProperty
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbAtribute = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbFinding = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbAtribute
            // 
            this.cmbAtribute.FormattingEnabled = true;
            this.cmbAtribute.Location = new System.Drawing.Point(80, 4);
            this.cmbAtribute.Name = "cmbAtribute";
            this.cmbAtribute.Size = new System.Drawing.Size(90, 21);
            this.cmbAtribute.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            // 
            // cbFinding
            // 
            this.cbFinding.AutoSize = true;
            this.cbFinding.Location = new System.Drawing.Point(184, 7);
            this.cbFinding.Name = "cbFinding";
            this.cbFinding.Size = new System.Drawing.Size(15, 14);
            this.cbFinding.TabIndex = 2;
            this.cbFinding.UseVisualStyleBackColor = true;
            this.cbFinding.Visible = false;
            // 
            // ClassMultiProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFinding);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmbAtribute);
            this.Name = "ClassMultiProperty";
            this.Size = new System.Drawing.Size(202, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAtribute;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox cbFinding;
    }
}
