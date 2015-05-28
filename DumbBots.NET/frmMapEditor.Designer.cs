namespace DumbBots.NET
{
    partial class MapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEditor));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.rbEmpty = new System.Windows.Forms.RadioButton();
            this.rbWall = new System.Windows.Forms.RadioButton();
            this.rbBazooka = new System.Windows.Forms.RadioButton();
            this.rbMedkit = new System.Windows.Forms.RadioButton();
            this.rbP1Start = new System.Windows.Forms.RadioButton();
            this.rbP2Start = new System.Windows.Forms.RadioButton();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbInvisible = new System.Windows.Forms.RadioButton();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.SystemColors.Control;
            this.pnlButtons.Location = new System.Drawing.Point(142, 12);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(456, 457);
            this.pnlButtons.TabIndex = 0;
            // 
            // rbEmpty
            // 
            this.rbEmpty.AutoSize = true;
            this.rbEmpty.Checked = true;
            this.rbEmpty.Location = new System.Drawing.Point(52, 125);
            this.rbEmpty.Name = "rbEmpty";
            this.rbEmpty.Size = new System.Drawing.Size(54, 17);
            this.rbEmpty.TabIndex = 1;
            this.rbEmpty.Text = "Empty";
            this.rbEmpty.UseVisualStyleBackColor = true;
            this.rbEmpty.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbWall
            // 
            this.rbWall.AutoSize = true;
            this.rbWall.Location = new System.Drawing.Point(52, 217);
            this.rbWall.Name = "rbWall";
            this.rbWall.Size = new System.Drawing.Size(46, 17);
            this.rbWall.TabIndex = 0;
            this.rbWall.Text = "Wall";
            this.rbWall.UseVisualStyleBackColor = true;
            this.rbWall.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbBazooka
            // 
            this.rbBazooka.AutoSize = true;
            this.rbBazooka.Location = new System.Drawing.Point(52, 268);
            this.rbBazooka.Name = "rbBazooka";
            this.rbBazooka.Size = new System.Drawing.Size(67, 17);
            this.rbBazooka.TabIndex = 0;
            this.rbBazooka.Text = "Bazooka";
            this.rbBazooka.UseVisualStyleBackColor = true;
            this.rbBazooka.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbMedkit
            // 
            this.rbMedkit.AutoSize = true;
            this.rbMedkit.Location = new System.Drawing.Point(52, 319);
            this.rbMedkit.Name = "rbMedkit";
            this.rbMedkit.Size = new System.Drawing.Size(57, 17);
            this.rbMedkit.TabIndex = 1;
            this.rbMedkit.Text = "Medkit";
            this.rbMedkit.UseVisualStyleBackColor = true;
            this.rbMedkit.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbP1Start
            // 
            this.rbP1Start.AutoSize = true;
            this.rbP1Start.Location = new System.Drawing.Point(52, 368);
            this.rbP1Start.Name = "rbP1Start";
            this.rbP1Start.Size = new System.Drawing.Size(63, 17);
            this.rbP1Start.TabIndex = 0;
            this.rbP1Start.Text = "P1 Start";
            this.rbP1Start.UseVisualStyleBackColor = true;
            this.rbP1Start.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbP2Start
            // 
            this.rbP2Start.AutoSize = true;
            this.rbP2Start.Location = new System.Drawing.Point(52, 419);
            this.rbP2Start.Name = "rbP2Start";
            this.rbP2Start.Size = new System.Drawing.Size(63, 17);
            this.rbP2Start.TabIndex = 0;
            this.rbP2Start.Text = "P2 Start";
            this.rbP2Start.UseVisualStyleBackColor = true;
            this.rbP2Start.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(121, 41);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Map";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 59);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 41);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Map";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbInvisible
            // 
            this.rbInvisible.AutoSize = true;
            this.rbInvisible.Location = new System.Drawing.Point(52, 172);
            this.rbInvisible.Name = "rbInvisible";
            this.rbInvisible.Size = new System.Drawing.Size(87, 17);
            this.rbInvisible.TabIndex = 8;
            this.rbInvisible.Text = "Invisible Wall";
            this.rbInvisible.UseVisualStyleBackColor = true;
            this.rbInvisible.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DumbBots.NET.Properties.Resources.invisiblewall;
            this.pictureBox7.Location = new System.Drawing.Point(16, 172);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(30, 30);
            this.pictureBox7.TabIndex = 7;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DumbBots.NET.Properties.Resources.P2Start;
            this.pictureBox4.Location = new System.Drawing.Point(16, 419);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 30);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DumbBots.NET.Properties.Resources.P1Start;
            this.pictureBox5.Location = new System.Drawing.Point(16, 368);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(30, 30);
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DumbBots.NET.Properties.Resources.Medkit;
            this.pictureBox6.Location = new System.Drawing.Point(16, 319);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(30, 30);
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DumbBots.NET.Properties.Resources.Bazooka;
            this.pictureBox3.Location = new System.Drawing.Point(16, 268);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DumbBots.NET.Properties.Resources.Brick;
            this.pictureBox2.Location = new System.Drawing.Point(16, 217);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DumbBots.NET.Properties.Resources.Blank;
            this.pictureBox1.Location = new System.Drawing.Point(16, 125);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 486);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.rbInvisible);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rbP2Start);
            this.Controls.Add(this.rbP1Start);
            this.Controls.Add(this.rbMedkit);
            this.Controls.Add(this.rbWall);
            this.Controls.Add(this.rbBazooka);
            this.Controls.Add(this.rbEmpty);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MapEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DumbBots.NET Map Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.RadioButton rbEmpty;
        private System.Windows.Forms.RadioButton rbWall;
        private System.Windows.Forms.RadioButton rbBazooka;
        private System.Windows.Forms.RadioButton rbMedkit;
        private System.Windows.Forms.RadioButton rbP1Start;
        private System.Windows.Forms.RadioButton rbP2Start;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.RadioButton rbInvisible;
    }
}

