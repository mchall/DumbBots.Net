using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Core;

namespace DumbBots.NET
{
    public partial class MapEditor : Form
    {
        private MapItem _currentPaint;
        private List<Button> _buttonList;

        public MapEditor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _buttonList = new List<Button>();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Button btn = new Button();
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.Image = Properties.Resources.Blank;
                    btn.Size = new Size(30, 30);
                    btn.Location = new Point(j * 30, i * 30);
                    btn.Click += new EventHandler(b_Click);
                    btn.Tag = MapItem.Empty;
                    _buttonList.Add(btn);
                    pnlButtons.Controls.Add(btn);
                }
            }
            rbEmpty.Select();
            _currentPaint = MapItem.Empty;

            rbEmpty.Tag = MapItem.Empty;
            rbBazooka.Tag = MapItem.Bazooka;
            rbMedkit.Tag = MapItem.Medpack;
            rbP1Start.Tag = MapItem.Player1Start;
            rbP2Start.Tag = MapItem.Player2Start;
            rbWall.Tag = MapItem.Wall;
            rbInvisible.Tag = MapItem.InvisibleWall;
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (_currentPaint == MapItem.Player1Start)
            {
                _buttonList.FindAll(b => (MapItem)b.Tag == MapItem.Player1Start).ForEach(b =>
                    {
                        b.Tag = MapItem.Empty;
                        b.Image = GetBitmap(MapItem.Empty);
                    });
            }

            if (_currentPaint == MapItem.Player2Start)
            {
                _buttonList.FindAll(b => (MapItem)b.Tag == MapItem.Player2Start).ForEach(b =>
                {
                    b.Tag = MapItem.Empty;
                    b.Image = GetBitmap(MapItem.Empty);
                });
            }

            btn.Tag = _currentPaint;
            btn.Image = GetBitmap(_currentPaint);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.DefaultExt = ".txt";
                ofd.Multiselect = false;
                ofd.InitialDirectory = Application.StartupPath + "\\Maps";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(ofd.FileName);
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            int index = (i * 15) + j;
                            Button btn = (Button)pnlButtons.Controls[index];
                            MapItem mapItem = (MapItem)int.Parse(lines[i][j].ToString());
                            btn.Tag = mapItem;
                            btn.Image = GetBitmap(mapItem);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool P1StartFound = _buttonList.Exists(b => (MapItem)b.Tag == MapItem.Player1Start);
            bool P2StartFound = _buttonList.Exists(b => (MapItem)b.Tag == MapItem.Player2Start);
            if (!P1StartFound || !P2StartFound)
            {
                MessageBox.Show("Both players require a starting point", "Map Save Error", MessageBoxButtons.OK);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = ".txt";
                sfd.InitialDirectory = Application.StartupPath + "\\Maps";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = new string[15];
                    for (int i = 0; i < 15; i++)
                    {
                        string line = String.Empty;
                        for (int j = 0; j < 15; j++)
                        {
                            int index = (i * 15) + j;
                            Button btn = (Button)pnlButtons.Controls[index];
                            line += (int)btn.Tag;
                        }
                        lines[i] = line;
                    }
                    File.WriteAllLines(sfd.FileName, lines);
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
                _currentPaint = (MapItem)(sender as RadioButton).Tag;
        }

        private Bitmap GetBitmap(MapItem mapItem)
        {
            switch (mapItem)
            {
                case (MapItem.Bazooka): return Properties.Resources.Bazooka;
                case (MapItem.Medpack): return Properties.Resources.Medkit;
                case (MapItem.Player1Start): return Properties.Resources.P1Start;
                case (MapItem.Player2Start): return Properties.Resources.P2Start;
                case (MapItem.Wall): return Properties.Resources.Brick;
                case (MapItem.InvisibleWall): return Properties.Resources.invisiblewall;
                default: return Properties.Resources.Blank;
            }
        }
    }
}