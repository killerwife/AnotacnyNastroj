namespace Projekt.Forms
{
    partial class MainWindowApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowApplication));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinBbClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tracksEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackCompareToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setObjectPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createImagesFromBoundingBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateRealDataFromPolylineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useExternMethodForRecognizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.definePicturePropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tutotialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlThumbs = new System.Windows.Forms.Panel();
            this.stats = new System.Windows.Forms.StatusStrip();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabelCurrent = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelLoading = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbProperty = new System.Windows.Forms.GroupBox();
            this.pnlEditClass = new System.Windows.Forms.Panel();
            this.pnlAddClass = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlProperty = new System.Windows.Forms.Panel();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.chbRememberZoom = new System.Windows.Forms.CheckBox();
            this.pnlDeleteBbs = new System.Windows.Forms.Panel();
            this.pnlCopyBB = new System.Windows.Forms.Panel();
            this.chbIds = new System.Windows.Forms.CheckBox();
            this.chbRescale = new System.Windows.Forms.CheckBox();
            this.pnlFind = new System.Windows.Forms.Panel();
            this.pnlPolyline = new System.Windows.Forms.Panel();
            this.tbGoOnPicture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.txbZoom = new System.Windows.Forms.TextBox();
            this.pnlCoursor = new System.Windows.Forms.Panel();
            this.pnlBoundingBox = new System.Windows.Forms.Panel();
            this.pnlPainting = new System.Windows.Forms.Panel();
            this.pnlZoomIn = new System.Windows.Forms.Panel();
            this.pnlZoomOut = new System.Windows.Forms.Panel();
            this.cmbAllObject = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlPictAttribute = new System.Windows.Forms.Panel();
            this.btnNextThumbs = new System.Windows.Forms.Button();
            this.btnPreviousThumbs = new System.Windows.Forms.Button();
            this.gbSelectedObj = new System.Windows.Forms.GroupBox();
            this.pnlSelectObj = new System.Windows.Forms.Panel();
            this.pnlSelObjOptions = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSelObjY = new System.Windows.Forms.TextBox();
            this.tbSelObjX = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSelObjHeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSelObjWidth = new System.Windows.Forms.TextBox();
            this.btnSaveChangeSelOj = new System.Windows.Forms.Button();
            this.cbShowSelObj = new System.Windows.Forms.CheckBox();
            this.imgBoxSelectedObj = new Emgu.CV.UI.ImageBox();
            this.pnlCopyInfo = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.stats.SuspendLayout();
            this.gbProperty.SuspendLayout();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbSelectedObj.SuspendLayout();
            this.pnlSelectObj.SuspendLayout();
            this.pnlSelObjOptions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxSelectedObj)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1057, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip1ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newToolStripMenuItem.Text = "New ...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.CreateNewProject);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "Open ...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenProject);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAsToolStripMenuItem.Text = "Save as ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveProjectAsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.closeCurrentToolStripMenuItem,
            this.joinBbClassToolStripMenuItem,
            this.tracksEditorToolStripMenuItem,
            this.trackCompareToolToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.videoToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.importToolStripMenuItem.Text = "Open For Project";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.ImportImage);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.videoToolStripMenuItem.Text = "Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.ImportVideo);
            // 
            // closeCurrentToolStripMenuItem
            // 
            this.closeCurrentToolStripMenuItem.Name = "closeCurrentToolStripMenuItem";
            this.closeCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.closeCurrentToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.closeCurrentToolStripMenuItem.Text = "Close Current Image";
            this.closeCurrentToolStripMenuItem.Click += new System.EventHandler(this.CloseCurrentToolStripMenuItemClick);
            // 
            // joinBbClassToolStripMenuItem
            // 
            this.joinBbClassToolStripMenuItem.Name = "joinBbClassToolStripMenuItem";
            this.joinBbClassToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.joinBbClassToolStripMenuItem.Text = "Tracing Objects";
            this.joinBbClassToolStripMenuItem.Click += new System.EventHandler(this.JoinBbsToolStripMenuItemClick);
            // 
            // tracksEditorToolStripMenuItem
            // 
            this.tracksEditorToolStripMenuItem.Name = "tracksEditorToolStripMenuItem";
            this.tracksEditorToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.tracksEditorToolStripMenuItem.Text = "Tracking Editor(Cells)";
            this.tracksEditorToolStripMenuItem.Click += new System.EventHandler(this.tracksEditorToolStripMenuItem_Click);
            // 
            // trackCompareToolToolStripMenuItem
            // 
            this.trackCompareToolToolStripMenuItem.Name = "trackCompareToolToolStripMenuItem";
            this.trackCompareToolToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.trackCompareToolToolStripMenuItem.Text = "Track Compare Tool";
            this.trackCompareToolToolStripMenuItem.Click += new System.EventHandler(this.trackCompareToolToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setObjectPropertiesToolStripMenuItem,
            this.generateTraToolStripMenuItem,
            this.createImagesFromBoundingBoxesToolStripMenuItem,
            this.generateRealDataFromPolylineToolStripMenuItem,
            this.useExternMethodForRecognizeToolStripMenuItem,
            this.definePicturePropertyToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // setObjectPropertiesToolStripMenuItem
            // 
            this.setObjectPropertiesToolStripMenuItem.Name = "setObjectPropertiesToolStripMenuItem";
            this.setObjectPropertiesToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.setObjectPropertiesToolStripMenuItem.Text = "Set Default Properties";
            this.setObjectPropertiesToolStripMenuItem.Click += new System.EventHandler(this.SetObjectPropertiesMenuClick);
            // 
            // generateTraToolStripMenuItem
            // 
            this.generateTraToolStripMenuItem.Name = "generateTraToolStripMenuItem";
            this.generateTraToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.generateTraToolStripMenuItem.Text = "Generate Negative Data For BoundingBoxes";
            this.generateTraToolStripMenuItem.Click += new System.EventHandler(this.GenerateTraToolStripMenuItemClick);
            // 
            // createImagesFromBoundingBoxesToolStripMenuItem
            // 
            this.createImagesFromBoundingBoxesToolStripMenuItem.Name = "createImagesFromBoundingBoxesToolStripMenuItem";
            this.createImagesFromBoundingBoxesToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.createImagesFromBoundingBoxesToolStripMenuItem.Text = "Generate Real And Articifial Data From BoundingBoxes";
            this.createImagesFromBoundingBoxesToolStripMenuItem.Click += new System.EventHandler(this.CreateImagesFromBBsMenuClick);
            // 
            // generateRealDataFromPolylineToolStripMenuItem
            // 
            this.generateRealDataFromPolylineToolStripMenuItem.Name = "generateRealDataFromPolylineToolStripMenuItem";
            this.generateRealDataFromPolylineToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.generateRealDataFromPolylineToolStripMenuItem.Text = "Generate Real And Artificial Data From Polyline";
            this.generateRealDataFromPolylineToolStripMenuItem.Click += new System.EventHandler(this.GenerateRealDataFromPolylineToolClick);
            // 
            // useExternMethodForRecognizeToolStripMenuItem
            // 
            this.useExternMethodForRecognizeToolStripMenuItem.Name = "useExternMethodForRecognizeToolStripMenuItem";
            this.useExternMethodForRecognizeToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.useExternMethodForRecognizeToolStripMenuItem.Text = "Use External Method For Recognition";
            this.useExternMethodForRecognizeToolStripMenuItem.Click += new System.EventHandler(this.UseExternMethodForRecognizeMenuItemClick);
            // 
            // definePicturePropertyToolStripMenuItem
            // 
            this.definePicturePropertyToolStripMenuItem.Name = "definePicturePropertyToolStripMenuItem";
            this.definePicturePropertyToolStripMenuItem.Size = new System.Drawing.Size(361, 22);
            this.definePicturePropertyToolStripMenuItem.Text = "Define Picture Property";
            this.definePicturePropertyToolStripMenuItem.Click += new System.EventHandler(this.DefinePicturePropertyToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tutotialToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // tutotialToolStripMenuItem
            // 
            this.tutotialToolStripMenuItem.Name = "tutotialToolStripMenuItem";
            this.tutotialToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.tutotialToolStripMenuItem.Text = "Tutotial";
            this.tutotialToolStripMenuItem.Click += new System.EventHandler(this.TutotialToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // pnlThumbs
            // 
            this.pnlThumbs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThumbs.AutoScroll = true;
            this.pnlThumbs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlThumbs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlThumbs.Location = new System.Drawing.Point(42, 503);
            this.pnlThumbs.Name = "pnlThumbs";
            this.pnlThumbs.Size = new System.Drawing.Size(491, 120);
            this.pnlThumbs.TabIndex = 1;
            // 
            // stats
            // 
            this.stats.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusProgressBar,
            this.statusLabelCurrent,
            this.statusLabelLoading});
            this.stats.Location = new System.Drawing.Point(0, 626);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(1057, 22);
            this.stats.TabIndex = 2;
            this.stats.Text = "statusStrip1";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabelCurrent
            // 
            this.statusLabelCurrent.Name = "statusLabelCurrent";
            this.statusLabelCurrent.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabelLoading
            // 
            this.statusLabelLoading.Name = "statusLabelLoading";
            this.statusLabelLoading.Size = new System.Drawing.Size(0, 17);
            // 
            // gbProperty
            // 
            this.gbProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProperty.Controls.Add(this.pnlEditClass);
            this.gbProperty.Controls.Add(this.pnlAddClass);
            this.gbProperty.Controls.Add(this.label1);
            this.gbProperty.Controls.Add(this.pnlProperty);
            this.gbProperty.Controls.Add(this.cmbClass);
            this.gbProperty.Location = new System.Drawing.Point(810, 64);
            this.gbProperty.Name = "gbProperty";
            this.gbProperty.Size = new System.Drawing.Size(240, 279);
            this.gbProperty.TabIndex = 4;
            this.gbProperty.TabStop = false;
            this.gbProperty.Text = "Object Property";
            // 
            // pnlEditClass
            // 
            this.pnlEditClass.AccessibleDescription = "Upraviť triedu";
            this.pnlEditClass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlEditClass.BackgroundImage")));
            this.pnlEditClass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlEditClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlEditClass.Location = new System.Drawing.Point(208, 19);
            this.pnlEditClass.Name = "pnlEditClass";
            this.pnlEditClass.Size = new System.Drawing.Size(24, 23);
            this.pnlEditClass.TabIndex = 16;
            this.pnlEditClass.Click += new System.EventHandler(this.EditClassClick);
            // 
            // pnlAddClass
            // 
            this.pnlAddClass.AccessibleDescription = "Pridať triedu";
            this.pnlAddClass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlAddClass.BackgroundImage")));
            this.pnlAddClass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlAddClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlAddClass.Location = new System.Drawing.Point(177, 19);
            this.pnlAddClass.Name = "pnlAddClass";
            this.pnlAddClass.Size = new System.Drawing.Size(24, 23);
            this.pnlAddClass.TabIndex = 0;
            this.pnlAddClass.Click += new System.EventHandler(this.AddClassClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class";
            // 
            // pnlProperty
            // 
            this.pnlProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProperty.AutoScroll = true;
            this.pnlProperty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlProperty.Location = new System.Drawing.Point(6, 46);
            this.pnlProperty.Name = "pnlProperty";
            this.pnlProperty.Size = new System.Drawing.Size(228, 227);
            this.pnlProperty.TabIndex = 15;
            // 
            // cmbClass
            // 
            this.cmbClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbClass.Enabled = false;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.ItemHeight = 13;
            this.cmbClass.Location = new System.Drawing.Point(42, 21);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(127, 21);
            this.cmbClass.TabIndex = 1;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.CreatePropertyForClass);
            // 
            // pnlTools
            // 
            this.pnlTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTools.Controls.Add(this.chbRememberZoom);
            this.pnlTools.Controls.Add(this.pnlDeleteBbs);
            this.pnlTools.Controls.Add(this.pnlCopyBB);
            this.pnlTools.Controls.Add(this.chbIds);
            this.pnlTools.Controls.Add(this.chbRescale);
            this.pnlTools.Controls.Add(this.pnlFind);
            this.pnlTools.Controls.Add(this.pnlPolyline);
            this.pnlTools.Controls.Add(this.tbGoOnPicture);
            this.pnlTools.Controls.Add(this.label2);
            this.pnlTools.Controls.Add(this.pnlDelete);
            this.pnlTools.Controls.Add(this.txbZoom);
            this.pnlTools.Controls.Add(this.pnlCoursor);
            this.pnlTools.Controls.Add(this.pnlBoundingBox);
            this.pnlTools.Controls.Add(this.pnlPainting);
            this.pnlTools.Controls.Add(this.pnlZoomIn);
            this.pnlTools.Controls.Add(this.pnlZoomOut);
            this.pnlTools.Location = new System.Drawing.Point(12, 27);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(791, 31);
            this.pnlTools.TabIndex = 5;
            // 
            // chbRememberZoom
            // 
            this.chbRememberZoom.AccessibleDescription = "";
            this.chbRememberZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbRememberZoom.AutoSize = true;
            this.chbRememberZoom.Location = new System.Drawing.Point(427, 6);
            this.chbRememberZoom.Name = "chbRememberZoom";
            this.chbRememberZoom.Size = new System.Drawing.Size(84, 17);
            this.chbRememberZoom.TabIndex = 12;
            this.chbRememberZoom.Text = "RetainZoom";
            this.chbRememberZoom.UseVisualStyleBackColor = true;
            // 
            // pnlDeleteBbs
            // 
            this.pnlDeleteBbs.AccessibleDescription = "Delete all BBs from image";
            this.pnlDeleteBbs.AccessibleName = "";
            this.pnlDeleteBbs.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDeleteBbs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlDeleteBbs.BackgroundImage")));
            this.pnlDeleteBbs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlDeleteBbs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDeleteBbs.Location = new System.Drawing.Point(218, 0);
            this.pnlDeleteBbs.Name = "pnlDeleteBbs";
            this.pnlDeleteBbs.Size = new System.Drawing.Size(28, 28);
            this.pnlDeleteBbs.TabIndex = 10;
            this.pnlDeleteBbs.Click += new System.EventHandler(this.DeleteAllBbsFrom);
            this.pnlDeleteBbs.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlDeleteBbs.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlCopyBB
            // 
            this.pnlCopyBB.AccessibleDescription = "Copy BBs from previous image";
            this.pnlCopyBB.AccessibleName = "";
            this.pnlCopyBB.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCopyBB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCopyBB.BackgroundImage")));
            this.pnlCopyBB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlCopyBB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCopyBB.Location = new System.Drawing.Point(184, 0);
            this.pnlCopyBB.Name = "pnlCopyBB";
            this.pnlCopyBB.Size = new System.Drawing.Size(28, 28);
            this.pnlCopyBB.TabIndex = 9;
            this.pnlCopyBB.Click += new System.EventHandler(this.CopyBbsFrom);
            this.pnlCopyBB.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlCopyBB.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // chbIds
            // 
            this.chbIds.AccessibleDescription = "";
            this.chbIds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbIds.AutoSize = true;
            this.chbIds.Location = new System.Drawing.Point(517, 6);
            this.chbIds.Name = "chbIds";
            this.chbIds.Size = new System.Drawing.Size(81, 17);
            this.chbIds.TabIndex = 11;
            this.chbIds.Text = "HideTracks";
            this.chbIds.UseVisualStyleBackColor = true;
            this.chbIds.CheckStateChanged += new System.EventHandler(this.ChbIds_CheckedChanged);
            // 
            // chbRescale
            // 
            this.chbRescale.AccessibleDescription = "";
            this.chbRescale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbRescale.AutoSize = true;
            this.chbRescale.Location = new System.Drawing.Point(604, 6);
            this.chbRescale.Name = "chbRescale";
            this.chbRescale.Size = new System.Drawing.Size(75, 17);
            this.chbRescale.TabIndex = 10;
            this.chbRescale.Text = "AutoZoom";
            this.chbRescale.UseVisualStyleBackColor = true;
            this.chbRescale.CheckedChanged += new System.EventHandler(this.FitZoom);
            // 
            // pnlFind
            // 
            this.pnlFind.AccessibleDescription = "Find Objects (CTRL + F)";
            this.pnlFind.AccessibleName = "";
            this.pnlFind.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlFind.BackgroundImage")));
            this.pnlFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlFind.Location = new System.Drawing.Point(150, 0);
            this.pnlFind.Name = "pnlFind";
            this.pnlFind.Size = new System.Drawing.Size(28, 28);
            this.pnlFind.TabIndex = 8;
            this.pnlFind.Click += new System.EventHandler(this.ActiveFind);
            this.pnlFind.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlFind.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlPolyline
            // 
            this.pnlPolyline.AccessibleDescription = "Polyline";
            this.pnlPolyline.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPolyline.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPolyline.BackgroundImage")));
            this.pnlPolyline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPolyline.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlPolyline.Location = new System.Drawing.Point(60, 0);
            this.pnlPolyline.Name = "pnlPolyline";
            this.pnlPolyline.Size = new System.Drawing.Size(28, 28);
            this.pnlPolyline.TabIndex = 5;
            this.pnlPolyline.Click += new System.EventHandler(this.ActivePolyline);
            this.pnlPolyline.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlPolyline.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // tbGoOnPicture
            // 
            this.tbGoOnPicture.AccessibleDescription = "Go on image";
            this.tbGoOnPicture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbGoOnPicture.Location = new System.Drawing.Point(350, 4);
            this.tbGoOnPicture.Name = "tbGoOnPicture";
            this.tbGoOnPicture.Size = new System.Drawing.Size(37, 20);
            this.tbGoOnPicture.TabIndex = 9;
            this.tbGoOnPicture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GoOnKeyPress);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Go on image";
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "GO ON";
            // 
            // pnlDelete
            // 
            this.pnlDelete.AccessibleDescription = "Delete Selected Object (CTRL + DEL)";
            this.pnlDelete.AccessibleName = "";
            this.pnlDelete.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlDelete.BackgroundImage")));
            this.pnlDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDelete.Location = new System.Drawing.Point(120, 0);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(28, 28);
            this.pnlDelete.TabIndex = 7;
            this.pnlDelete.Click += new System.EventHandler(this.DeleteClick);
            this.pnlDelete.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlDelete.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // txbZoom
            // 
            this.txbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbZoom.Location = new System.Drawing.Point(685, 4);
            this.txbZoom.Name = "txbZoom";
            this.txbZoom.Size = new System.Drawing.Size(37, 20);
            this.txbZoom.TabIndex = 6;
            this.txbZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZoomEnter);
            // 
            // pnlCoursor
            // 
            this.pnlCoursor.AccessibleDescription = "Cursor";
            this.pnlCoursor.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCoursor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCoursor.BackgroundImage")));
            this.pnlCoursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlCoursor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCoursor.Location = new System.Drawing.Point(90, 0);
            this.pnlCoursor.Name = "pnlCoursor";
            this.pnlCoursor.Size = new System.Drawing.Size(28, 28);
            this.pnlCoursor.TabIndex = 5;
            this.pnlCoursor.Click += new System.EventHandler(this.ActiveCursor);
            this.pnlCoursor.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlCoursor.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlBoundingBox
            // 
            this.pnlBoundingBox.AccessibleDescription = "Bounding Box";
            this.pnlBoundingBox.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBoundingBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBoundingBox.BackgroundImage")));
            this.pnlBoundingBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlBoundingBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlBoundingBox.Location = new System.Drawing.Point(30, 0);
            this.pnlBoundingBox.Name = "pnlBoundingBox";
            this.pnlBoundingBox.Size = new System.Drawing.Size(28, 28);
            this.pnlBoundingBox.TabIndex = 4;
            this.pnlBoundingBox.Click += new System.EventHandler(this.ActiveBB);
            this.pnlBoundingBox.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlBoundingBox.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlPainting
            // 
            this.pnlPainting.AccessibleDescription = "Painting";
            this.pnlPainting.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPainting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPainting.BackgroundImage")));
            this.pnlPainting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlPainting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlPainting.Location = new System.Drawing.Point(0, 0);
            this.pnlPainting.Name = "pnlPainting";
            this.pnlPainting.Size = new System.Drawing.Size(28, 28);
            this.pnlPainting.TabIndex = 3;
            this.pnlPainting.Click += new System.EventHandler(this.ActivePaint);
            this.pnlPainting.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlPainting.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlZoomIn
            // 
            this.pnlZoomIn.AccessibleDescription = "Zoom In (CTRL + (+))";
            this.pnlZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlZoomIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlZoomIn.BackgroundImage")));
            this.pnlZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlZoomIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlZoomIn.Location = new System.Drawing.Point(759, 0);
            this.pnlZoomIn.Name = "pnlZoomIn";
            this.pnlZoomIn.Size = new System.Drawing.Size(28, 28);
            this.pnlZoomIn.TabIndex = 2;
            this.pnlZoomIn.Click += new System.EventHandler(this.ZoomIn);
            this.pnlZoomIn.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlZoomIn.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // pnlZoomOut
            // 
            this.pnlZoomOut.AccessibleDescription = "Zoom Out (CTRL + (-))";
            this.pnlZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlZoomOut.BackColor = System.Drawing.SystemColors.Control;
            this.pnlZoomOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlZoomOut.BackgroundImage")));
            this.pnlZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlZoomOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlZoomOut.Location = new System.Drawing.Point(728, 0);
            this.pnlZoomOut.Name = "pnlZoomOut";
            this.pnlZoomOut.Size = new System.Drawing.Size(28, 28);
            this.pnlZoomOut.TabIndex = 1;
            this.pnlZoomOut.Click += new System.EventHandler(this.ZoomOut);
            this.pnlZoomOut.MouseEnter += new System.EventHandler(this.HighlightIcon);
            this.pnlZoomOut.MouseLeave += new System.EventHandler(this.ResetIconBack);
            // 
            // cmbAllObject
            // 
            this.cmbAllObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAllObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAllObject.FormattingEnabled = true;
            this.cmbAllObject.Location = new System.Drawing.Point(842, 35);
            this.cmbAllObject.Name = "cmbAllObject";
            this.cmbAllObject.Size = new System.Drawing.Size(197, 21);
            this.cmbAllObject.TabIndex = 6;
            this.cmbAllObject.DropDown += new System.EventHandler(this.CmbAllObjectDropDown);
            this.cmbAllObject.SelectedIndexChanged += new System.EventHandler(this.Select);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(818, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "All";
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.Color.White;
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.imageBox.Location = new System.Drawing.Point(12, 64);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(791, 438);
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.ImgBoxClick);
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImgBoxPaint);
            this.imageBox.DoubleClick += new System.EventHandler(this.ImgBoxDoubleClick);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImgBoxMouseDown);
            this.imageBox.MouseEnter += new System.EventHandler(this.ImgBoxMouseEnter);
            this.imageBox.MouseLeave += new System.EventHandler(this.ImgBoxMouseLeave);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImgBoxMouseMove);
            this.imageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImgBoxMouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = "Click And Define New Attribute";
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pnlPictAttribute);
            this.groupBox2.Location = new System.Drawing.Point(565, 503);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 120);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Picture Property";
            // 
            // pnlPictAttribute
            // 
            this.pnlPictAttribute.AccessibleDescription = "Click And Define New Attribute";
            this.pnlPictAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPictAttribute.AutoScroll = true;
            this.pnlPictAttribute.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPictAttribute.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlPictAttribute.Location = new System.Drawing.Point(5, 13);
            this.pnlPictAttribute.Name = "pnlPictAttribute";
            this.pnlPictAttribute.Size = new System.Drawing.Size(228, 103);
            this.pnlPictAttribute.TabIndex = 0;
            // 
            // btnNextThumbs
            // 
            this.btnNextThumbs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextThumbs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNextThumbs.BackgroundImage")));
            this.btnNextThumbs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextThumbs.Location = new System.Drawing.Point(533, 503);
            this.btnNextThumbs.Name = "btnNextThumbs";
            this.btnNextThumbs.Size = new System.Drawing.Size(30, 120);
            this.btnNextThumbs.TabIndex = 9;
            this.btnNextThumbs.UseVisualStyleBackColor = true;
            this.btnNextThumbs.Click += new System.EventHandler(this.BtnNextThumbsClick);
            // 
            // btnPreviousThumbs
            // 
            this.btnPreviousThumbs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviousThumbs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreviousThumbs.BackgroundImage")));
            this.btnPreviousThumbs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreviousThumbs.Location = new System.Drawing.Point(12, 503);
            this.btnPreviousThumbs.Name = "btnPreviousThumbs";
            this.btnPreviousThumbs.Size = new System.Drawing.Size(30, 120);
            this.btnPreviousThumbs.TabIndex = 10;
            this.btnPreviousThumbs.UseVisualStyleBackColor = true;
            this.btnPreviousThumbs.Click += new System.EventHandler(this.BtnPreviousThumbsClick);
            // 
            // gbSelectedObj
            // 
            this.gbSelectedObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelectedObj.Controls.Add(this.pnlSelectObj);
            this.gbSelectedObj.Location = new System.Drawing.Point(810, 349);
            this.gbSelectedObj.Name = "gbSelectedObj";
            this.gbSelectedObj.Size = new System.Drawing.Size(240, 274);
            this.gbSelectedObj.TabIndex = 11;
            this.gbSelectedObj.TabStop = false;
            this.gbSelectedObj.Text = "Selected Object";
            // 
            // pnlSelectObj
            // 
            this.pnlSelectObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSelectObj.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSelectObj.Controls.Add(this.pnlSelObjOptions);
            this.pnlSelectObj.Controls.Add(this.btnSaveChangeSelOj);
            this.pnlSelectObj.Controls.Add(this.cbShowSelObj);
            this.pnlSelectObj.Controls.Add(this.imgBoxSelectedObj);
            this.pnlSelectObj.Location = new System.Drawing.Point(6, 12);
            this.pnlSelectObj.Name = "pnlSelectObj";
            this.pnlSelectObj.Size = new System.Drawing.Size(228, 258);
            this.pnlSelectObj.TabIndex = 0;
            // 
            // pnlSelObjOptions
            // 
            this.pnlSelObjOptions.Controls.Add(this.groupBox3);
            this.pnlSelObjOptions.Controls.Add(this.groupBox4);
            this.pnlSelObjOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlSelObjOptions.Name = "pnlSelObjOptions";
            this.pnlSelObjOptions.Size = new System.Drawing.Size(221, 66);
            this.pnlSelObjOptions.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbSelObjY);
            this.groupBox3.Controls.Add(this.tbSelObjX);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(111, 62);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Upper Left Corner";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "X";
            // 
            // tbSelObjY
            // 
            this.tbSelObjY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelObjY.Location = new System.Drawing.Point(43, 38);
            this.tbSelObjY.Name = "tbSelObjY";
            this.tbSelObjY.Size = new System.Drawing.Size(64, 20);
            this.tbSelObjY.TabIndex = 1;
            this.tbSelObjY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSelObjCheckValue);
            // 
            // tbSelObjX
            // 
            this.tbSelObjX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelObjX.Location = new System.Drawing.Point(43, 15);
            this.tbSelObjX.Name = "tbSelObjX";
            this.tbSelObjX.Size = new System.Drawing.Size(64, 20);
            this.tbSelObjX.TabIndex = 0;
            this.tbSelObjX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSelObjCheckValue);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbSelObjHeight);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbSelObjWidth);
            this.groupBox4.Location = new System.Drawing.Point(119, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(102, 62);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Height";
            // 
            // tbSelObjHeight
            // 
            this.tbSelObjHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelObjHeight.Location = new System.Drawing.Point(50, 38);
            this.tbSelObjHeight.Name = "tbSelObjHeight";
            this.tbSelObjHeight.Size = new System.Drawing.Size(46, 20);
            this.tbSelObjHeight.TabIndex = 1;
            this.tbSelObjHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSelObjCheckValue);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Width";
            // 
            // tbSelObjWidth
            // 
            this.tbSelObjWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelObjWidth.Location = new System.Drawing.Point(50, 15);
            this.tbSelObjWidth.Name = "tbSelObjWidth";
            this.tbSelObjWidth.Size = new System.Drawing.Size(46, 20);
            this.tbSelObjWidth.TabIndex = 0;
            this.tbSelObjWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSelObjCheckValue);
            // 
            // btnSaveChangeSelOj
            // 
            this.btnSaveChangeSelOj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveChangeSelOj.BackgroundImage")));
            this.btnSaveChangeSelOj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSaveChangeSelOj.Location = new System.Drawing.Point(146, 66);
            this.btnSaveChangeSelOj.Name = "btnSaveChangeSelOj";
            this.btnSaveChangeSelOj.Size = new System.Drawing.Size(75, 17);
            this.btnSaveChangeSelOj.TabIndex = 6;
            this.btnSaveChangeSelOj.UseVisualStyleBackColor = true;
            this.btnSaveChangeSelOj.Click += new System.EventHandler(this.BtnSaveChangeSelOjClick);
            // 
            // cbShowSelObj
            // 
            this.cbShowSelObj.AutoSize = true;
            this.cbShowSelObj.Checked = true;
            this.cbShowSelObj.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSelObj.Location = new System.Drawing.Point(3, 66);
            this.cbShowSelObj.Name = "cbShowSelObj";
            this.cbShowSelObj.Size = new System.Drawing.Size(132, 17);
            this.cbShowSelObj.TabIndex = 5;
            this.cbShowSelObj.Text = "Show Selected Object";
            this.cbShowSelObj.UseVisualStyleBackColor = true;
            this.cbShowSelObj.CheckedChanged += new System.EventHandler(this.ChShowSelObjChanged);
            // 
            // imgBoxSelectedObj
            // 
            this.imgBoxSelectedObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgBoxSelectedObj.Location = new System.Drawing.Point(1, 86);
            this.imgBoxSelectedObj.Name = "imgBoxSelectedObj";
            this.imgBoxSelectedObj.Size = new System.Drawing.Size(221, 168);
            this.imgBoxSelectedObj.TabIndex = 2;
            this.imgBoxSelectedObj.TabStop = false;
            // 
            // pnlCopyInfo
            // 
            this.pnlCopyInfo.AccessibleDescription = "";
            this.pnlCopyInfo.AccessibleName = "";
            this.pnlCopyInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCopyInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCopyInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCopyInfo.BackgroundImage")));
            this.pnlCopyInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlCopyInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCopyInfo.Location = new System.Drawing.Point(1033, 1);
            this.pnlCopyInfo.Name = "pnlCopyInfo";
            this.pnlCopyInfo.Size = new System.Drawing.Size(17, 23);
            this.pnlCopyInfo.TabIndex = 11;
            this.pnlCopyInfo.MouseEnter += new System.EventHandler(this.HighlightIcon4Tooltip);
            this.pnlCopyInfo.MouseLeave += new System.EventHandler(this.ResetIconBack4Tooltip);
            // 
            // MainWindowApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1057, 648);
            this.Controls.Add(this.pnlCopyInfo);
            this.Controls.Add(this.gbSelectedObj);
            this.Controls.Add(this.btnPreviousThumbs);
            this.Controls.Add(this.btnNextThumbs);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbAllObject);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.gbProperty);
            this.Controls.Add(this.stats);
            this.Controls.Add(this.pnlThumbs);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1073, 600);
            this.Name = "MainWindowApplication";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyKeyDown);
            this.Resize += new System.EventHandler(this.Form1Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stats.ResumeLayout(false);
            this.stats.PerformLayout();
            this.gbProperty.ResumeLayout(false);
            this.gbProperty.PerformLayout();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbSelectedObj.ResumeLayout(false);
            this.pnlSelectObj.ResumeLayout(false);
            this.pnlSelectObj.PerformLayout();
            this.pnlSelObjOptions.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxSelectedObj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tutotialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel pnlThumbs;
        private System.Windows.Forms.StatusStrip stats;
        private System.Windows.Forms.GroupBox gbProperty;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Panel pnlZoomOut;
        private System.Windows.Forms.Panel pnlZoomIn;
        private System.Windows.Forms.Panel pnlBoundingBox;
        private System.Windows.Forms.Panel pnlPainting;
        private System.Windows.Forms.Panel pnlCoursor;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.ComboBox cmbAllObject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbZoom;
        private System.Windows.Forms.Panel pnlDelete;
        private System.Windows.Forms.Panel pnlProperty;
        private System.Windows.Forms.Panel pnlAddClass;
        private System.Windows.Forms.Panel pnlEditClass;
        private System.Windows.Forms.ToolStripMenuItem setObjectPropertiesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlPictAttribute;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCurrent;
        private System.Windows.Forms.TextBox tbGoOnPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelLoading;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar;
        private System.Windows.Forms.ToolStripMenuItem generateTraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createImagesFromBoundingBoxesToolStripMenuItem;
        private System.Windows.Forms.Button btnNextThumbs;
        private System.Windows.Forms.Button btnPreviousThumbs;
        private System.Windows.Forms.Panel pnlPolyline;
        private System.Windows.Forms.ToolStripMenuItem generateRealDataFromPolylineToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbSelectedObj;
        private System.Windows.Forms.Panel pnlSelectObj;
        private System.Windows.Forms.Panel pnlFind;
        private Emgu.CV.UI.ImageBox imgBoxSelectedObj;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSelObjHeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSelObjWidth;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSelObjY;
        private System.Windows.Forms.TextBox tbSelObjX;
        private System.Windows.Forms.CheckBox cbShowSelObj;
        private System.Windows.Forms.Button btnSaveChangeSelOj;
        private System.Windows.Forms.Panel pnlSelObjOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useExternMethodForRecognizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem definePicturePropertyToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbRescale;
        private System.Windows.Forms.CheckBox chbIds;
        private System.Windows.Forms.Panel pnlCopyBB;
        private System.Windows.Forms.Panel pnlDeleteBbs;
        private System.Windows.Forms.ToolStripMenuItem joinBbClassToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbRememberZoom;
        private System.Windows.Forms.ToolStripMenuItem tracksEditorToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCopyInfo;
        private System.Windows.Forms.ToolStripMenuItem trackCompareToolToolStripMenuItem;
    }
}

