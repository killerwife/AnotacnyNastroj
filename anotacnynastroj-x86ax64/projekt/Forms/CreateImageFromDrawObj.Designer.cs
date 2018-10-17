namespace Projekt.Forms
{
    partial class CreateImageFromDrawObj
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
            this.gbNormalization = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbScale = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.chbUnnormalized = new System.Windows.Forms.CheckBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.tbFolderToSave = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDirStruct = new System.Windows.Forms.TextBox();
            this.btnDefDirStruct = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.gpStepLength = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbStepLength = new System.Windows.Forms.TextBox();
            this.chbGenRealData = new System.Windows.Forms.CheckBox();
            this.chbGenArtificialData = new System.Windows.Forms.CheckBox();
            this.gbPercentDistr = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTesting = new System.Windows.Forms.TextBox();
            this.tbTraining = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbValidation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbGenArtificialData = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddAditiveNoise = new System.Windows.Forms.Button();
            this.btnAddImpulseNoise = new System.Windows.Forms.Button();
            this.btnAddSmooth = new System.Windows.Forms.Button();
            this.btnAddSharpen = new System.Windows.Forms.Button();
            this.btnAddScale = new System.Windows.Forms.Button();
            this.btnAddReflection = new System.Windows.Forms.Button();
            this.btnAddRotate = new System.Windows.Forms.Button();
            this.btnAddMove = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlSettingGenArtificialData = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chbTesting = new System.Windows.Forms.CheckBox();
            this.chbValidation = new System.Windows.Forms.CheckBox();
            this.chbTraining = new System.Windows.Forms.CheckBox();
            this.btnLoadSetting = new System.Windows.Forms.Button();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.gbNormalization.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gpStepLength.SuspendLayout();
            this.gbPercentDistr.SuspendLayout();
            this.gbGenArtificialData.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNormalization
            // 
            this.gbNormalization.Controls.Add(this.label3);
            this.gbNormalization.Controls.Add(this.label2);
            this.gbNormalization.Controls.Add(this.tbHeight);
            this.gbNormalization.Controls.Add(this.tbWidth);
            this.gbNormalization.Controls.Add(this.tbScale);
            this.gbNormalization.Controls.Add(this.label1);
            this.gbNormalization.Location = new System.Drawing.Point(6, 226);
            this.gbNormalization.Name = "gbNormalization";
            this.gbNormalization.Size = new System.Drawing.Size(286, 106);
            this.gbNormalization.TabIndex = 0;
            this.gbNormalization.TabStop = false;
            this.gbNormalization.Text = "Settings Of Normalization";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Width";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(162, 76);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(100, 20);
            this.tbHeight.TabIndex = 4;
            this.tbHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            this.tbHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetHeightFromUser);
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(162, 50);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(100, 20);
            this.tbWidth.TabIndex = 3;
            this.tbWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            this.tbWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetWidthFromUser);
            // 
            // tbScale
            // 
            this.tbScale.Location = new System.Drawing.Point(162, 23);
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(100, 20);
            this.tbScale.TabIndex = 1;
            this.tbScale.Text = ":";
            this.tbScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueScalePress);
            this.tbScale.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetScaleFromUser);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scale (Width : Height)";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(589, 370);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreateClick);
            // 
            // chbUnnormalized
            // 
            this.chbUnnormalized.AutoSize = true;
            this.chbUnnormalized.Location = new System.Drawing.Point(6, 339);
            this.chbUnnormalized.Name = "chbUnnormalized";
            this.chbUnnormalized.Size = new System.Drawing.Size(200, 17);
            this.chbUnnormalized.TabIndex = 5;
            this.chbUnnormalized.Text = "Create Images Without Normalization";
            this.chbUnnormalized.UseVisualStyleBackColor = true;
            this.chbUnnormalized.CheckedChanged += new System.EventHandler(this.ChbUnnormalizedCheckedChanged);
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChangeFolder.Location = new System.Drawing.Point(6, 19);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(94, 23);
            this.btnChangeFolder.TabIndex = 6;
            this.btnChangeFolder.Text = "Change Folder";
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.BtnChangeFolderClick);
            // 
            // tbFolderToSave
            // 
            this.tbFolderToSave.Location = new System.Drawing.Point(106, 21);
            this.tbFolderToSave.Name = "tbFolderToSave";
            this.tbFolderToSave.ReadOnly = true;
            this.tbFolderToSave.Size = new System.Drawing.Size(186, 20);
            this.tbFolderToSave.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDirStruct);
            this.groupBox1.Controls.Add(this.btnDefDirStruct);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cmbOutputFormat);
            this.groupBox1.Controls.Add(this.gpStepLength);
            this.groupBox1.Controls.Add(this.chbGenRealData);
            this.groupBox1.Controls.Add(this.chbGenArtificialData);
            this.groupBox1.Controls.Add(this.chbUnnormalized);
            this.groupBox1.Controls.Add(this.gbPercentDistr);
            this.groupBox1.Controls.Add(this.btnChangeFolder);
            this.groupBox1.Controls.Add(this.gbNormalization);
            this.groupBox1.Controls.Add(this.tbFolderToSave);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 385);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // tbDirStruct
            // 
            this.tbDirStruct.Location = new System.Drawing.Point(150, 47);
            this.tbDirStruct.Name = "tbDirStruct";
            this.tbDirStruct.ReadOnly = true;
            this.tbDirStruct.Size = new System.Drawing.Size(143, 20);
            this.tbDirStruct.TabIndex = 22;
            // 
            // btnDefDirStruct
            // 
            this.btnDefDirStruct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDefDirStruct.Location = new System.Drawing.Point(6, 45);
            this.btnDefDirStruct.Name = "btnDefDirStruct";
            this.btnDefDirStruct.Size = new System.Drawing.Size(138, 23);
            this.btnDefDirStruct.TabIndex = 21;
            this.btnDefDirStruct.Text = "Define Directory Structure";
            this.btnDefDirStruct.UseVisualStyleBackColor = true;
            this.btnDefDirStruct.Click += new System.EventHandler(this.BtnDefDirStruct);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Output Image Format";
            // 
            // cmbOutputFormat
            // 
            this.cmbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputFormat.FormattingEnabled = true;
            this.cmbOutputFormat.Location = new System.Drawing.Point(150, 73);
            this.cmbOutputFormat.Name = "cmbOutputFormat";
            this.cmbOutputFormat.Size = new System.Drawing.Size(142, 21);
            this.cmbOutputFormat.TabIndex = 20;
            this.cmbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.OutputFormatChanged);
            // 
            // gpStepLength
            // 
            this.gpStepLength.Controls.Add(this.label12);
            this.gpStepLength.Controls.Add(this.tbStepLength);
            this.gpStepLength.Location = new System.Drawing.Point(6, 339);
            this.gpStepLength.Name = "gpStepLength";
            this.gpStepLength.Size = new System.Drawing.Size(286, 42);
            this.gpStepLength.TabIndex = 18;
            this.gpStepLength.TabStop = false;
            this.gpStepLength.Text = "Step Length";
            this.gpStepLength.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(100, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Length";
            // 
            // tbStepLength
            // 
            this.tbStepLength.Location = new System.Drawing.Point(162, 14);
            this.tbStepLength.Name = "tbStepLength";
            this.tbStepLength.Size = new System.Drawing.Size(100, 20);
            this.tbStepLength.TabIndex = 7;
            this.tbStepLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            // 
            // chbGenRealData
            // 
            this.chbGenRealData.AutoSize = true;
            this.chbGenRealData.Checked = true;
            this.chbGenRealData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbGenRealData.Location = new System.Drawing.Point(6, 113);
            this.chbGenRealData.Name = "chbGenRealData";
            this.chbGenRealData.Size = new System.Drawing.Size(121, 17);
            this.chbGenRealData.TabIndex = 17;
            this.chbGenRealData.Text = "Generate Real Data";
            this.chbGenRealData.UseVisualStyleBackColor = true;
            this.chbGenRealData.CheckedChanged += new System.EventHandler(this.CheckChangeRealGen);
            // 
            // chbGenArtificialData
            // 
            this.chbGenArtificialData.AutoSize = true;
            this.chbGenArtificialData.Location = new System.Drawing.Point(6, 136);
            this.chbGenArtificialData.Name = "chbGenArtificialData";
            this.chbGenArtificialData.Size = new System.Drawing.Size(135, 17);
            this.chbGenArtificialData.TabIndex = 16;
            this.chbGenArtificialData.Text = "Generate Artificial Data";
            this.chbGenArtificialData.UseVisualStyleBackColor = true;
            this.chbGenArtificialData.CheckedChanged += new System.EventHandler(this.CheckChangeArticifialGen);
            // 
            // gbPercentDistr
            // 
            this.gbPercentDistr.Controls.Add(this.label4);
            this.gbPercentDistr.Controls.Add(this.tbTesting);
            this.gbPercentDistr.Controls.Add(this.tbTraining);
            this.gbPercentDistr.Controls.Add(this.label6);
            this.gbPercentDistr.Controls.Add(this.tbValidation);
            this.gbPercentDistr.Controls.Add(this.label5);
            this.gbPercentDistr.Location = new System.Drawing.Point(6, 158);
            this.gbPercentDistr.Name = "gbPercentDistr";
            this.gbPercentDistr.Size = new System.Drawing.Size(286, 54);
            this.gbPercentDistr.TabIndex = 15;
            this.gbPercentDistr.TabStop = false;
            this.gbPercentDistr.Text = "Percentage Distribution Of Generated Data On:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Training";
            // 
            // tbTesting
            // 
            this.tbTesting.Location = new System.Drawing.Point(241, 24);
            this.tbTesting.Name = "tbTesting";
            this.tbTesting.Size = new System.Drawing.Size(35, 20);
            this.tbTesting.TabIndex = 14;
            this.tbTesting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            // 
            // tbTraining
            // 
            this.tbTraining.Location = new System.Drawing.Point(50, 24);
            this.tbTraining.Name = "tbTraining";
            this.tbTraining.Size = new System.Drawing.Size(35, 20);
            this.tbTraining.TabIndex = 12;
            this.tbTraining.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Testing";
            // 
            // tbValidation
            // 
            this.tbValidation.Location = new System.Drawing.Point(151, 24);
            this.tbValidation.Name = "tbValidation";
            this.tbValidation.Size = new System.Drawing.Size(35, 20);
            this.tbValidation.TabIndex = 13;
            this.tbValidation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckValueIntPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Validation";
            // 
            // gbGenArtificialData
            // 
            this.gbGenArtificialData.Controls.Add(this.groupBox5);
            this.gbGenArtificialData.Controls.Add(this.groupBox4);
            this.gbGenArtificialData.Location = new System.Drawing.Point(340, 12);
            this.gbGenArtificialData.Name = "gbGenArtificialData";
            this.gbGenArtificialData.Size = new System.Drawing.Size(324, 352);
            this.gbGenArtificialData.TabIndex = 9;
            this.gbGenArtificialData.TabStop = false;
            this.gbGenArtificialData.Text = "Generate Artificial Data";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnHelp);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.pnlSettingGenArtificialData);
            this.groupBox5.Location = new System.Drawing.Point(6, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(312, 278);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Setting";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(282, 218);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(24, 20);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelpClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddAditiveNoise);
            this.groupBox2.Controls.Add(this.btnAddImpulseNoise);
            this.groupBox2.Controls.Add(this.btnAddSmooth);
            this.groupBox2.Controls.Add(this.btnAddSharpen);
            this.groupBox2.Controls.Add(this.btnAddScale);
            this.groupBox2.Controls.Add(this.btnAddReflection);
            this.groupBox2.Controls.Add(this.btnAddRotate);
            this.groupBox2.Controls.Add(this.btnAddMove);
            this.groupBox2.Location = new System.Drawing.Point(2, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 69);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Setting";
            // 
            // btnAddAditiveNoise
            // 
            this.btnAddAditiveNoise.Location = new System.Drawing.Point(190, 15);
            this.btnAddAditiveNoise.Name = "btnAddAditiveNoise";
            this.btnAddAditiveNoise.Size = new System.Drawing.Size(81, 20);
            this.btnAddAditiveNoise.TabIndex = 25;
            this.btnAddAditiveNoise.Text = "Aditive Noise";
            this.btnAddAditiveNoise.UseVisualStyleBackColor = true;
            this.btnAddAditiveNoise.Click += new System.EventHandler(this.BtnAddAdditiveNoiseSettingClick);
            // 
            // btnAddImpulseNoise
            // 
            this.btnAddImpulseNoise.Location = new System.Drawing.Point(190, 41);
            this.btnAddImpulseNoise.Name = "btnAddImpulseNoise";
            this.btnAddImpulseNoise.Size = new System.Drawing.Size(81, 20);
            this.btnAddImpulseNoise.TabIndex = 24;
            this.btnAddImpulseNoise.Text = "Impulse Noise";
            this.btnAddImpulseNoise.UseVisualStyleBackColor = true;
            this.btnAddImpulseNoise.Click += new System.EventHandler(this.BtnAddImpulseNoiseSettingClick);
            // 
            // btnAddSmooth
            // 
            this.btnAddSmooth.Location = new System.Drawing.Point(129, 15);
            this.btnAddSmooth.Name = "btnAddSmooth";
            this.btnAddSmooth.Size = new System.Drawing.Size(55, 20);
            this.btnAddSmooth.TabIndex = 22;
            this.btnAddSmooth.Text = "Blurring";
            this.btnAddSmooth.UseVisualStyleBackColor = true;
            this.btnAddSmooth.Click += new System.EventHandler(this.BtnAddBlurringSettingClick);
            // 
            // btnAddSharpen
            // 
            this.btnAddSharpen.Location = new System.Drawing.Point(129, 41);
            this.btnAddSharpen.Name = "btnAddSharpen";
            this.btnAddSharpen.Size = new System.Drawing.Size(55, 20);
            this.btnAddSharpen.TabIndex = 23;
            this.btnAddSharpen.Text = "Sharpen";
            this.btnAddSharpen.UseVisualStyleBackColor = true;
            this.btnAddSharpen.Click += new System.EventHandler(this.BtnAddSharpenClick);
            // 
            // btnAddScale
            // 
            this.btnAddScale.Location = new System.Drawing.Point(3, 41);
            this.btnAddScale.Name = "btnAddScale";
            this.btnAddScale.Size = new System.Drawing.Size(51, 20);
            this.btnAddScale.TabIndex = 21;
            this.btnAddScale.Text = "Scale";
            this.btnAddScale.UseVisualStyleBackColor = true;
            this.btnAddScale.Click += new System.EventHandler(this.BtnAddScaleSettingClick);
            // 
            // btnAddReflection
            // 
            this.btnAddReflection.Location = new System.Drawing.Point(60, 41);
            this.btnAddReflection.Name = "btnAddReflection";
            this.btnAddReflection.Size = new System.Drawing.Size(63, 20);
            this.btnAddReflection.TabIndex = 20;
            this.btnAddReflection.Text = "Reflection";
            this.btnAddReflection.UseVisualStyleBackColor = true;
            this.btnAddReflection.Click += new System.EventHandler(this.BtnAddReflectionSettingClick);
            // 
            // btnAddRotate
            // 
            this.btnAddRotate.Location = new System.Drawing.Point(60, 15);
            this.btnAddRotate.Name = "btnAddRotate";
            this.btnAddRotate.Size = new System.Drawing.Size(63, 20);
            this.btnAddRotate.TabIndex = 19;
            this.btnAddRotate.Text = "Rotate";
            this.btnAddRotate.UseVisualStyleBackColor = true;
            this.btnAddRotate.Click += new System.EventHandler(this.BtnAddRotateSettingClick);
            // 
            // btnAddMove
            // 
            this.btnAddMove.Location = new System.Drawing.Point(3, 15);
            this.btnAddMove.Name = "btnAddMove";
            this.btnAddMove.Size = new System.Drawing.Size(51, 20);
            this.btnAddMove.TabIndex = 18;
            this.btnAddMove.Text = "Move";
            this.btnAddMove.UseVisualStyleBackColor = true;
            this.btnAddMove.Click += new System.EventHandler(this.BtnAddMoveSettingClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(258, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Remove";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Count";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Step";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(115, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "To";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "From";
            // 
            // pnlSettingGenArtificialData
            // 
            this.pnlSettingGenArtificialData.AutoScroll = true;
            this.pnlSettingGenArtificialData.Location = new System.Drawing.Point(2, 30);
            this.pnlSettingGenArtificialData.Name = "pnlSettingGenArtificialData";
            this.pnlSettingGenArtificialData.Size = new System.Drawing.Size(307, 171);
            this.pnlSettingGenArtificialData.TabIndex = 0;
            this.pnlSettingGenArtificialData.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.SettingRemovedFromPanel);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.chbTesting);
            this.groupBox4.Controls.Add(this.chbValidation);
            this.groupBox4.Controls.Add(this.chbTraining);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 43);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Generate Data For";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(282, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbTesting
            // 
            this.chbTesting.AutoSize = true;
            this.chbTesting.Location = new System.Drawing.Point(219, 19);
            this.chbTesting.Name = "chbTesting";
            this.chbTesting.Size = new System.Drawing.Size(61, 17);
            this.chbTesting.TabIndex = 3;
            this.chbTesting.Text = "Testing";
            this.chbTesting.UseVisualStyleBackColor = true;
            this.chbTesting.CheckedChanged += new System.EventHandler(this.CheckChangedTesting);
            // 
            // chbValidation
            // 
            this.chbValidation.AutoSize = true;
            this.chbValidation.Location = new System.Drawing.Point(117, 19);
            this.chbValidation.Name = "chbValidation";
            this.chbValidation.Size = new System.Drawing.Size(72, 17);
            this.chbValidation.TabIndex = 2;
            this.chbValidation.Text = "Validation";
            this.chbValidation.UseVisualStyleBackColor = true;
            this.chbValidation.CheckedChanged += new System.EventHandler(this.CheckChangedValidation);
            // 
            // chbTraining
            // 
            this.chbTraining.AutoSize = true;
            this.chbTraining.Location = new System.Drawing.Point(21, 19);
            this.chbTraining.Name = "chbTraining";
            this.chbTraining.Size = new System.Drawing.Size(64, 17);
            this.chbTraining.TabIndex = 1;
            this.chbTraining.Text = "Training";
            this.chbTraining.UseVisualStyleBackColor = true;
            this.chbTraining.CheckedChanged += new System.EventHandler(this.CheckChangedTraining);
            // 
            // btnLoadSetting
            // 
            this.btnLoadSetting.AccessibleDescription = "Load Setting For Generate";
            this.btnLoadSetting.Location = new System.Drawing.Point(340, 370);
            this.btnLoadSetting.Name = "btnLoadSetting";
            this.btnLoadSetting.Size = new System.Drawing.Size(90, 23);
            this.btnLoadSetting.TabIndex = 10;
            this.btnLoadSetting.Text = "Load Settings ";
            this.btnLoadSetting.UseVisualStyleBackColor = true;
            this.btnLoadSetting.Click += new System.EventHandler(this.BtnLoadSettingClick);
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Location = new System.Drawing.Point(436, 370);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(90, 23);
            this.btnSaveSetting.TabIndex = 11;
            this.btnSaveSetting.Text = "Save Setting";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.BtnSaveSettingClick);
            // 
            // CreateImageFromDrawObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 403);
            this.Controls.Add(this.btnSaveSetting);
            this.Controls.Add(this.btnLoadSetting);
            this.Controls.Add(this.gbGenArtificialData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreateImageFromDrawObj";
            this.Text = "Create Images From";
            this.Load += new System.EventHandler(this.LoadForm);
            this.gbNormalization.ResumeLayout(false);
            this.gbNormalization.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpStepLength.ResumeLayout(false);
            this.gpStepLength.PerformLayout();
            this.gbPercentDistr.ResumeLayout(false);
            this.gbPercentDistr.PerformLayout();
            this.gbGenArtificialData.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNormalization;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tbScale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox chbUnnormalized;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.TextBox tbFolderToSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbPercentDistr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTesting;
        private System.Windows.Forms.TextBox tbTraining;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbValidation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbGenArtificialData;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chbTesting;
        private System.Windows.Forms.CheckBox chbValidation;
        private System.Windows.Forms.CheckBox chbTraining;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlSettingGenArtificialData;
        private System.Windows.Forms.CheckBox chbGenRealData;
        private System.Windows.Forms.CheckBox chbGenArtificialData;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddScale;
        private System.Windows.Forms.Button btnAddReflection;
        private System.Windows.Forms.Button btnAddRotate;
        private System.Windows.Forms.Button btnAddMove;
        private System.Windows.Forms.GroupBox gpStepLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbStepLength;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnLoadSetting;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbOutputFormat;
        private System.Windows.Forms.Button btnAddAditiveNoise;
        private System.Windows.Forms.Button btnAddImpulseNoise;
        private System.Windows.Forms.Button btnAddSmooth;
        private System.Windows.Forms.Button btnAddSharpen;
        private System.Windows.Forms.TextBox tbDirStruct;
        private System.Windows.Forms.Button btnDefDirStruct;
    }
}