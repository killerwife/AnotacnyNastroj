namespace Projekt.UserControls
{
    partial class SettingArticifialDataEmpty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingArticifialDataEmpty));
            this.btnRemoveSetting = new System.Windows.Forms.Button();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemoveSetting
            // 
            this.btnRemoveSetting.AccessibleDescription = "Remove";
            this.btnRemoveSetting.AccessibleName = "Remove";
            this.btnRemoveSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveSetting.BackgroundImage")));
            this.btnRemoveSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveSetting.Location = new System.Drawing.Point(268, 7);
            this.btnRemoveSetting.Name = "btnRemoveSetting";
            this.btnRemoveSetting.Size = new System.Drawing.Size(19, 20);
            this.btnRemoveSetting.TabIndex = 14;
            this.btnRemoveSetting.UseVisualStyleBackColor = true;
            this.btnRemoveSetting.Click += new System.EventHandler(this.BtnRemoveSettingClick);
            // 
            // pnlSetting
            // 
            this.pnlSetting.Controls.Add(this.btnPreview);
            this.pnlSetting.Controls.Add(this.lblName);
            this.pnlSetting.Location = new System.Drawing.Point(0, 0);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(265, 35);
            this.pnlSetting.TabIndex = 13;
            // 
            // btnPreview
            // 
            this.btnPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreview.BackgroundImage")));
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreview.Location = new System.Drawing.Point(231, 7);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(31, 20);
            this.btnPreview.TabIndex = 11;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreviewClick);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(2, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Sharpening";
            // 
            // SettingArticifialDataEmpty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnRemoveSetting);
            this.Controls.Add(this.pnlSetting);
            this.Name = "SettingArticifialDataEmpty";
            this.Size = new System.Drawing.Size(290, 35);
            this.pnlSetting.ResumeLayout(false);
            this.pnlSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveSetting;
        private System.Windows.Forms.Panel pnlSetting;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblName;
    }
}
