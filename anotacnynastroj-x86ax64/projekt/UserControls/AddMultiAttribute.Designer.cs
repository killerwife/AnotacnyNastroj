namespace Projekt.UserControls
{
    partial class AddMultiAttribute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMultiAttribute));
            this.tbValues = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbValues
            // 
            this.tbValues.AccessibleDescription = "";
            this.tbValues.Location = new System.Drawing.Point(195, 3);
            this.tbValues.Name = "tbValues";
            this.tbValues.Size = new System.Drawing.Size(100, 20);
            this.tbValues.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Values";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(46, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name";
            // 
            // btnRemoveSetting
            // 
            this.btnRemoveSetting.AccessibleDescription = "Remove";
            this.btnRemoveSetting.AccessibleName = "Remove";
            this.btnRemoveSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveSetting.BackgroundImage")));
            this.btnRemoveSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveSetting.Location = new System.Drawing.Point(295, 2);
            this.btnRemoveSetting.Name = "btnRemoveSetting";
            this.btnRemoveSetting.Size = new System.Drawing.Size(19, 20);
            this.btnRemoveSetting.TabIndex = 13;
            this.btnRemoveSetting.UseVisualStyleBackColor = true;
            this.btnRemoveSetting.Click += new System.EventHandler(this.BtnRemoveSettingClick);
            // 
            // AddMultiAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemoveSetting);
            this.Controls.Add(this.tbValues);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Name = "AddMultiAttribute";
            this.Size = new System.Drawing.Size(317, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValues;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoveSetting;
    }
}
