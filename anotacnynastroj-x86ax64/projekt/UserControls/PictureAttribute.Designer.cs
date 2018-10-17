namespace Projekt.UserControls
{
    partial class PictureAttribute
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
            this.lblName = new System.Windows.Forms.Label();
            this.cmbAtribute = new System.Windows.Forms.ComboBox();
            this.cbAtribute = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "label1";
            // 
            // cmbAtribute
            // 
            this.cmbAtribute.FormattingEnabled = true;
            this.cmbAtribute.Location = new System.Drawing.Point(80, 4);
            this.cmbAtribute.Name = "cmbAtribute";
            this.cmbAtribute.Size = new System.Drawing.Size(119, 21);
            this.cmbAtribute.TabIndex = 2;
            // 
            // cbAtribute
            // 
            this.cbAtribute.AutoSize = true;
            this.cbAtribute.Location = new System.Drawing.Point(129, 7);
            this.cbAtribute.Name = "cbAtribute";
            this.cbAtribute.Size = new System.Drawing.Size(15, 14);
            this.cbAtribute.TabIndex = 4;
            this.cbAtribute.UseVisualStyleBackColor = true;
            this.cbAtribute.Visible = false;
            // 
            // PictureAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbAtribute);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmbAtribute);
            this.Name = "PictureAttribute";
            this.Size = new System.Drawing.Size(202, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cmbAtribute;
        private System.Windows.Forms.CheckBox cbAtribute;
    }
}
