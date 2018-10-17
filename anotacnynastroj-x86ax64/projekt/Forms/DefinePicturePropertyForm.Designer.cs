namespace Projekt.Forms
{
    partial class DefinePicturePropertyForm
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
            this.lblAdd = new System.Windows.Forms.Label();
            this.btnAddBoolean = new System.Windows.Forms.Button();
            this.btnMultivalue = new System.Windows.Forms.Button();
            this.pnlAttribute = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblAdd);
            this.groupBox1.Controls.Add(this.btnAddBoolean);
            this.groupBox1.Controls.Add(this.btnMultivalue);
            this.groupBox1.Controls.Add(this.pnlAttribute);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 204);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // lblAdd
            // 
            this.lblAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAdd.Location = new System.Drawing.Point(111, 180);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(71, 13);
            this.lblAdd.TabIndex = 2;
            this.lblAdd.Text = "Add Property:";
            // 
            // btnAddBoolean
            // 
            this.btnAddBoolean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBoolean.Location = new System.Drawing.Point(267, 175);
            this.btnAddBoolean.Name = "btnAddBoolean";
            this.btnAddBoolean.Size = new System.Drawing.Size(75, 23);
            this.btnAddBoolean.TabIndex = 1;
            this.btnAddBoolean.Text = "Boolean";
            this.btnAddBoolean.UseVisualStyleBackColor = true;
            this.btnAddBoolean.Click += new System.EventHandler(this.BtnAddBooleanAttributeClick);
            // 
            // btnMultivalue
            // 
            this.btnMultivalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultivalue.Location = new System.Drawing.Point(188, 175);
            this.btnMultivalue.Name = "btnMultivalue";
            this.btnMultivalue.Size = new System.Drawing.Size(75, 23);
            this.btnMultivalue.TabIndex = 0;
            this.btnMultivalue.Text = "Multivalue";
            this.btnMultivalue.UseVisualStyleBackColor = true;
            this.btnMultivalue.Click += new System.EventHandler(this.AddMultivalueAttributeClick);
            // 
            // pnlAttribute
            // 
            this.pnlAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAttribute.AutoScroll = true;
            this.pnlAttribute.Location = new System.Drawing.Point(3, 16);
            this.pnlAttribute.Name = "pnlAttribute";
            this.pnlAttribute.Size = new System.Drawing.Size(339, 153);
            this.pnlAttribute.TabIndex = 0;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(192, 212);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 9;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(271, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.MySaveClick);
            // 
            // DefinePicturePropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 241);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DefinePicturePropertyForm";
            this.Text = "Define Picture Property";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Button btnAddBoolean;
        private System.Windows.Forms.Button btnMultivalue;
        private System.Windows.Forms.Panel pnlAttribute;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button button3;
    }
}