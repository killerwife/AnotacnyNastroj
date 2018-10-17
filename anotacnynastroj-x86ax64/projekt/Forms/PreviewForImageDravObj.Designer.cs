namespace Projekt.Forms
{
    partial class PreviewForImageDravObj
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
            this.components = new System.ComponentModel.Container();
            this.btnAnotherSetting = new System.Windows.Forms.Button();
            this.imgBoxOrigin = new Emgu.CV.UI.ImageBox();
            this.Original = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imgBoxAftChange = new Emgu.CV.UI.ImageBox();
            this.btnAnotherImage = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxOrigin)).BeginInit();
            this.Original.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxAftChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnotherSetting
            // 
            this.btnAnotherSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnotherSetting.Location = new System.Drawing.Point(716, 360);
            this.btnAnotherSetting.Name = "btnAnotherSetting";
            this.btnAnotherSetting.Size = new System.Drawing.Size(106, 23);
            this.btnAnotherSetting.TabIndex = 3;
            this.btnAnotherSetting.Text = "Another Setting";
            this.btnAnotherSetting.UseVisualStyleBackColor = true;
            this.btnAnotherSetting.Click += new System.EventHandler(this.BtnAnotherSettingClick);
            // 
            // imgBoxOrigin
            // 
            this.imgBoxOrigin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgBoxOrigin.Location = new System.Drawing.Point(3, 16);
            this.imgBoxOrigin.Name = "imgBoxOrigin";
            this.imgBoxOrigin.Size = new System.Drawing.Size(404, 334);
            this.imgBoxOrigin.TabIndex = 2;
            this.imgBoxOrigin.TabStop = false;
            // 
            // Original
            // 
            this.Original.Controls.Add(this.imgBoxOrigin);
            this.Original.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Original.Location = new System.Drawing.Point(0, 0);
            this.Original.Name = "Original";
            this.Original.Size = new System.Drawing.Size(410, 353);
            this.Original.TabIndex = 4;
            this.Original.TabStop = false;
            this.Original.Text = "Original";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imgBoxAftChange);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 353);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "After Changes";
            // 
            // imgBoxAftChange
            // 
            this.imgBoxAftChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgBoxAftChange.Location = new System.Drawing.Point(3, 16);
            this.imgBoxAftChange.Name = "imgBoxAftChange";
            this.imgBoxAftChange.Size = new System.Drawing.Size(410, 334);
            this.imgBoxAftChange.TabIndex = 2;
            this.imgBoxAftChange.TabStop = false;
            // 
            // btnAnotherImage
            // 
            this.btnAnotherImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnotherImage.Location = new System.Drawing.Point(604, 360);
            this.btnAnotherImage.Name = "btnAnotherImage";
            this.btnAnotherImage.Size = new System.Drawing.Size(106, 23);
            this.btnAnotherImage.TabIndex = 6;
            this.btnAnotherImage.Text = "Another Image";
            this.btnAnotherImage.UseVisualStyleBackColor = true;
            this.btnAnotherImage.Click += new System.EventHandler(this.BtnAnotherImageClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Original);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(830, 353);
            this.splitContainer1.SplitterDistance = 410;
            this.splitContainer1.TabIndex = 7;
            // 
            // PreviewForImageDravObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 387);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnAnotherImage);
            this.Controls.Add(this.btnAnotherSetting);
            this.Name = "PreviewForImageDravObj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview For Setting";
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxOrigin)).EndInit();
            this.Original.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxAftChange)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAnotherSetting;
        private Emgu.CV.UI.ImageBox imgBoxOrigin;
        private System.Windows.Forms.GroupBox Original;
        private System.Windows.Forms.GroupBox groupBox2;
        private Emgu.CV.UI.ImageBox imgBoxAftChange;
        private System.Windows.Forms.Button btnAnotherImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}