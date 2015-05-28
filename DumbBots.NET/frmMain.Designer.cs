namespace DumbBots.NET
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dlgOpenScript = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveScript = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpenMap = new System.Windows.Forms.OpenFileDialog();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.btnLoadMap = new System.Windows.Forms.ToolStripButton();
            this.btnMapEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSound = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSoundOn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSoundOff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.referenceDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgAddReference = new System.Windows.Forms.OpenFileDialog();
            this.collapsibleSplitContainer1 = new Misc.CollapsibleSplitContainer();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlRendering = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnPlayer1Info = new System.Windows.Forms.ToolStripButton();
            this.btnPlayer2Info = new System.Windows.Forms.ToolStripButton();
            this.pnlCodeArea = new System.Windows.Forms.Panel();
            this.lbErrors = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnSaveScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddReference = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteReference = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateTeam1 = new System.Windows.Forms.ToolStripButton();
            this.btnUpdateTeam2 = new System.Windows.Forms.ToolStripButton();
            this.btnUpdateDirector = new System.Windows.Forms.ToolStripButton();
            this.btnBasicScriptEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collapsibleSplitContainer1)).BeginInit();
            this.collapsibleSplitContainer1.Panel1.SuspendLayout();
            this.collapsibleSplitContainer1.Panel2.SuspendLayout();
            this.collapsibleSplitContainer1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.pnlCodeArea.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgOpenScript
            // 
            this.dlgOpenScript.Filter = "Code files|*.cs;*.vb";
            this.dlgOpenScript.RestoreDirectory = true;
            // 
            // dlgSaveScript
            // 
            this.dlgSaveScript.Filter = "C# files|*.cs|VB files|*.vb";
            this.dlgSaveScript.RestoreDirectory = true;
            // 
            // dlgOpenMap
            // 
            this.dlgOpenMap.Filter = "Text files|*.txt";
            this.dlgOpenMap.RestoreDirectory = true;
            // 
            // tsMenu
            // 
            this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadMap,
            this.btnMapEditor,
            this.toolStripSeparator4,
            this.btnBasicScriptEditor,
            this.toolStripSeparator1,
            this.btnSound,
            this.toolStripSeparator5,
            this.btnHelp});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(974, 25);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip2";
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadMap.Image")));
            this.btnLoadMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(80, 22);
            this.btnLoadMap.Text = "Load Map";
            this.btnLoadMap.Click += new System.EventHandler(this.mnuFileLoadMap_Click);
            // 
            // btnMapEditor
            // 
            this.btnMapEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnMapEditor.Image")));
            this.btnMapEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapEditor.Name = "btnMapEditor";
            this.btnMapEditor.Size = new System.Drawing.Size(85, 22);
            this.btnMapEditor.Text = "Map Editor";
            this.btnMapEditor.Click += new System.EventHandler(this.mnuMapEditor_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSound
            // 
            this.btnSound.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSoundOn,
            this.btnSoundOff});
            this.btnSound.Image = ((System.Drawing.Image)(resources.GetObject("btnSound.Image")));
            this.btnSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(70, 22);
            this.btnSound.Text = "Sound";
            // 
            // btnSoundOn
            // 
            this.btnSoundOn.Name = "btnSoundOn";
            this.btnSoundOn.Size = new System.Drawing.Size(152, 22);
            this.btnSoundOn.Text = "On";
            this.btnSoundOn.Click += new System.EventHandler(this.mnuSoundOn_Click);
            // 
            // btnSoundOff
            // 
            this.btnSoundOff.Name = "btnSoundOff";
            this.btnSoundOff.Size = new System.Drawing.Size(152, 22);
            this.btnSoundOff.Text = "Off";
            this.btnSoundOff.Click += new System.EventHandler(this.mnuSoundOff_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHelp
            // 
            this.btnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.referenceDocumentToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(61, 22);
            this.btnHelp.Text = "Help";
            // 
            // referenceDocumentToolStripMenuItem
            // 
            this.referenceDocumentToolStripMenuItem.Name = "referenceDocumentToolStripMenuItem";
            this.referenceDocumentToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.referenceDocumentToolStripMenuItem.Text = "Reference Document";
            this.referenceDocumentToolStripMenuItem.Click += new System.EventHandler(this.ReferenceDocumentToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // dlgAddReference
            // 
            this.dlgAddReference.Filter = "Assemblies|*.dll";
            this.dlgAddReference.Multiselect = true;
            this.dlgAddReference.RestoreDirectory = true;
            // 
            // collapsibleSplitContainer1
            // 
            this.collapsibleSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.collapsibleSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collapsibleSplitContainer1.Location = new System.Drawing.Point(0, 25);
            this.collapsibleSplitContainer1.Name = "collapsibleSplitContainer1";
            this.collapsibleSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // collapsibleSplitContainer1.Panel1
            // 
            this.collapsibleSplitContainer1.Panel1.Controls.Add(this.pnlMain);
            // 
            // collapsibleSplitContainer1.Panel2
            // 
            this.collapsibleSplitContainer1.Panel2.Controls.Add(this.pnlCodeArea);
            this.collapsibleSplitContainer1.Size = new System.Drawing.Size(974, 716);
            this.collapsibleSplitContainer1.SplitterDistance = 447;
            this.collapsibleSplitContainer1.SplitterWidth = 8;
            this.collapsibleSplitContainer1.TabIndex = 22;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRendering);
            this.pnlMain.Controls.Add(this.toolStrip2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(974, 447);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlRendering
            // 
            this.pnlRendering.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRendering.Location = new System.Drawing.Point(0, 25);
            this.pnlRendering.Name = "pnlRendering";
            this.pnlRendering.Size = new System.Drawing.Size(974, 422);
            this.pnlRendering.TabIndex = 1;
            this.pnlRendering.Resize += new System.EventHandler(this.PnlRendering_Resize);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayer1Info,
            this.btnPlayer2Info});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(974, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnPlayer1Info
            // 
            this.btnPlayer1Info.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayer1Info.Image")));
            this.btnPlayer1Info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayer1Info.Name = "btnPlayer1Info";
            this.btnPlayer1Info.Size = new System.Drawing.Size(23, 22);
            // 
            // btnPlayer2Info
            // 
            this.btnPlayer2Info.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPlayer2Info.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayer2Info.Image")));
            this.btnPlayer2Info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayer2Info.Name = "btnPlayer2Info";
            this.btnPlayer2Info.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPlayer2Info.Size = new System.Drawing.Size(23, 22);
            // 
            // pnlCodeArea
            // 
            this.pnlCodeArea.Controls.Add(this.lbErrors);
            this.pnlCodeArea.Controls.Add(this.toolStrip1);
            this.pnlCodeArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCodeArea.Location = new System.Drawing.Point(0, 0);
            this.pnlCodeArea.Name = "pnlCodeArea";
            this.pnlCodeArea.Size = new System.Drawing.Size(974, 261);
            this.pnlCodeArea.TabIndex = 21;
            // 
            // lbErrors
            // 
            this.lbErrors.BackColor = System.Drawing.SystemColors.Control;
            this.lbErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbErrors.FormattingEnabled = true;
            this.lbErrors.Location = new System.Drawing.Point(0, 218);
            this.lbErrors.Name = "lbErrors";
            this.lbErrors.Size = new System.Drawing.Size(974, 43);
            this.lbErrors.TabIndex = 24;
            this.lbErrors.DoubleClick += new System.EventHandler(this.LabelErrors_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnSaveScript,
            this.toolStripSeparator2,
            this.btnAddReference,
            this.btnDeleteReference,
            this.toolStripSeparator3,
            this.btnUpdateTeam1,
            this.btnUpdateTeam2,
            this.btnUpdateDirector});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 8, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(974, 31);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(53, 20);
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.mnuFileLoadScript_Click);
            // 
            // btnSaveScript
            // 
            this.btnSaveScript.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveScript.Image")));
            this.btnSaveScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(51, 20);
            this.btnSaveScript.Text = "Save";
            this.btnSaveScript.Click += new System.EventHandler(this.mnuFileSaveScript_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnAddReference
            // 
            this.btnAddReference.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReference.Image")));
            this.btnAddReference.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddReference.Name = "btnAddReference";
            this.btnAddReference.Size = new System.Drawing.Size(104, 20);
            this.btnAddReference.Text = "Add Reference";
            this.btnAddReference.Click += new System.EventHandler(this.btnAddReference_Click);
            // 
            // btnDeleteReference
            // 
            this.btnDeleteReference.Enabled = false;
            this.btnDeleteReference.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteReference.Image")));
            this.btnDeleteReference.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteReference.Name = "btnDeleteReference";
            this.btnDeleteReference.Size = new System.Drawing.Size(134, 20);
            this.btnDeleteReference.Text = "Remove Reference";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnUpdateTeam1
            // 
            this.btnUpdateTeam1.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateTeam1.Image")));
            this.btnUpdateTeam1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateTeam1.Name = "btnUpdateTeam1";
            this.btnUpdateTeam1.Size = new System.Drawing.Size(107, 20);
            this.btnUpdateTeam1.Text = "Update Team 1";
            this.btnUpdateTeam1.Click += new System.EventHandler(this.btnCompileTeam1_Click);
            // 
            // btnUpdateTeam2
            // 
            this.btnUpdateTeam2.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateTeam2.Image")));
            this.btnUpdateTeam2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateTeam2.Name = "btnUpdateTeam2";
            this.btnUpdateTeam2.Size = new System.Drawing.Size(107, 20);
            this.btnUpdateTeam2.Text = "Update Team 2";
            this.btnUpdateTeam2.Click += new System.EventHandler(this.btnCompileTeam2_Click);
            // 
            // btnUpdateDirector
            // 
            this.btnUpdateDirector.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateDirector.Image")));
            this.btnUpdateDirector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateDirector.Name = "btnUpdateDirector";
            this.btnUpdateDirector.Size = new System.Drawing.Size(110, 20);
            this.btnUpdateDirector.Text = "Update Director";
            this.btnUpdateDirector.Click += new System.EventHandler(this.btnCompileDirector_Click);
            // 
            // btnBasicScriptEditor
            // 
            this.btnBasicScriptEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnBasicScriptEditor.Image")));
            this.btnBasicScriptEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBasicScriptEditor.Name = "btnBasicScriptEditor";
            this.btnBasicScriptEditor.Size = new System.Drawing.Size(163, 22);
            this.btnBasicScriptEditor.Text = "Launch Basic Script Editor";
            this.btnBasicScriptEditor.Click += new System.EventHandler(this.btnBasicScriptEditor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 741);
            this.Controls.Add(this.collapsibleSplitContainer1);
            this.Controls.Add(this.tsMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DumbBots.NET";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.Deactivate += new System.EventHandler(this.FrmMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.collapsibleSplitContainer1.Panel1.ResumeLayout(false);
            this.collapsibleSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.collapsibleSplitContainer1)).EndInit();
            this.collapsibleSplitContainer1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.pnlCodeArea.ResumeLayout(false);
            this.pnlCodeArea.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.OpenFileDialog dlgOpenScript;
        private System.Windows.Forms.SaveFileDialog dlgSaveScript;
        private System.Windows.Forms.OpenFileDialog dlgOpenMap;
        private System.Windows.Forms.Panel pnlCodeArea;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddReference;
        private System.Windows.Forms.OpenFileDialog dlgAddReference;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSaveScript;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnUpdateTeam1;
        private System.Windows.Forms.ToolStripButton btnUpdateTeam2;
        private System.Windows.Forms.ToolStripButton btnUpdateDirector;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton btnLoadMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton btnSound;
        private System.Windows.Forms.ToolStripMenuItem btnSoundOn;
        private System.Windows.Forms.ToolStripMenuItem btnSoundOff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnMapEditor;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnPlayer1Info;
        private System.Windows.Forms.ToolStripButton btnPlayer2Info;
        private System.Windows.Forms.Panel pnlRendering;
        private System.Windows.Forms.ToolStripDropDownButton btnDeleteReference;
        private Misc.CollapsibleSplitContainer collapsibleSplitContainer1;
        private System.Windows.Forms.ListBox lbErrors;
        private System.Windows.Forms.ToolStripDropDownButton btnHelp;
        private System.Windows.Forms.ToolStripMenuItem referenceDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnBasicScriptEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}