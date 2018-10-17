namespace Projekt.UserControls
{
    partial class ClassBoolProperty
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
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.cbFinding = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(80, 7);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 1;
            this.checkBox.UseVisualStyleBackColor = true;
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
            // ClassBoolProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFinding);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.lblName);
            this.Name = "ClassBoolProperty";
            this.Size = new System.Drawing.Size(202, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.CheckBox cbFinding;
    }
}
