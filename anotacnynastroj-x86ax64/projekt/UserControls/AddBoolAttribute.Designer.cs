namespace Projekt.UserControls
{
    partial class AddBoolAttribute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBoolAttribute));
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.btnRemoveSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(46, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Name";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(202, 6);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 12;
            this.checkBox.UseVisualStyleBackColor = true;
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
            // AddBoolAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemoveSetting);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Name = "AddBoolAttribute";
            this.Size = new System.Drawing.Size(317, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Button btnRemoveSetting;
    }
}
