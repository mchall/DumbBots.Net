using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Core;
using DumbBots.NET.Properties;
using Entities;
using Game;
using Graphs;
using IrrlichtNETCP;
using MDXInfo.Util.Script;
using Messaging;
using Scripting;

namespace DumbBots.NET
{
    public partial class frmMain : Form
    {
        private ScriptEditorControl _txtCode;
        private CombatEntity _team1Player;
        private CombatEntity _team2Player;
        private List<string> _referencedAssemblies;

        public frmMain()
        {
            InitializeComponent();
            _referencedAssemblies = new List<string>();
            AddCodeEditor();
        }

        private void AddCodeEditor()
        {
            _txtCode = new ScriptEditorControl();
            _txtCode.Dock = DockStyle.Fill;
            _txtCode.ConfigurationManager.Language = "cs";
            _txtCode.Margins.Margin0.Width = 35;

            _txtCode.Parent = pnlCodeArea;
            _txtCode.Dock = DockStyle.Fill;
            _txtCode.BringToFront();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Show();
            ScriptRunner.ReflectScripts();

            SoundManager.PlaySound = Settings.Default.Sound;
            UpdateSoundUI();

            InitializeDevice(pnlRendering);

            if (Settings.Default.ReferencedAssemblies != null)
            {
                foreach (var referencedAssembly in Settings.Default.ReferencedAssemblies)
                {
                    _referencedAssemblies.Add(referencedAssembly);
                }
                UpdateExternalReferences();
            }

            LoadScript(String.IsNullOrEmpty(Settings.Default.ScriptFile) ? Application.StartupPath + "\\Scripts\\Default.cs" : Settings.Default.ScriptFile);
            PlayLevel(String.IsNullOrEmpty(Settings.Default.MapFile) ? Application.StartupPath + "\\Maps\\DM_default.txt" : Settings.Default.MapFile);
        }

        private void PlayLevel(string mapFile)
        {
            pnlRendering.Enabled = false;
            Globals.Device.Timer.Time = 0;
            CombatManager.ClearEntities();
            CollisionManager.ClearSpawningItems();
            SceneNodeManager.ResetNodes();
            CombatManager.ResetLists();
            LevelManager.CreateLevel(mapFile);
            pnlRendering.Enabled = true;
            MessageDispatcher.ClearQueue();
            ScriptRunner.ReflectScripts();
            RuleManager.Reset();
            if (Globals.Device != null)
            {
                Run(pnlRendering);
            }
        }

        private void InitializeDevice(Control c)
        {
            Globals.Device = new IrrlichtDevice(DriverType.Direct3D8, new Dimension2D(1024, 768), 32, false, false, false, true, c.Handle);

            if (Globals.Device == null)
            {
                MessageBox.Show("Could not create video device.", "Video device error");
                return;
            }
        }

        private void Run(Control c)
        {
            #region Initialization

            _team1Player = SceneNodeManager.CreateTeam1SceneNode(LevelManager.GetTeamStartPosition(Team.Team1));
            _team1Player.FloatingText = SceneNodeManager.CreateBillboardText(_team1Player.Node, String.Empty);

            _team2Player = SceneNodeManager.CreateTeam2SceneNode(LevelManager.GetTeamStartPosition(Team.Team2));
            _team2Player.FloatingText = SceneNodeManager.CreateBillboardText(_team2Player.Node, String.Empty);

            var mainCamera = SceneNodeManager.CreateCameraSceneNode(new Vector3D(0, 1100, 0));
            Globals.Scene.ActiveCamera = mainCamera;

            ScriptRunner.DirectorAfterMapLoad(_team1Player, _team2Player);

            #endregion Initialization

            while (Globals.Device.Run() && c.Enabled) //Game loop
            {
                GC.Collect();
                Globals.Driver.ClearZBuffer();

                RenderManager.BeginRender();

                LevelManager.UpdateLevelGraph();

                CollisionManager.CheckProjectileCollisions(_team1Player);
                CollisionManager.CheckProjectileCollisions(_team2Player);
                CollisionManager.CheckPlayerCollisions(_team1Player, _team2Player);
                CollisionManager.CheckSpawningItems();
                CollisionManager.CheckBazookaCollisions(_team1Player);
                CollisionManager.CheckBazookaCollisions(_team2Player);
                CollisionManager.CheckMedkitCollisions(_team1Player);
                CollisionManager.CheckMedkitCollisions(_team2Player);

                Thinking(_team1Player, _team2Player);
                Thinking(_team2Player, _team1Player);
                ScriptRunner.DirectorThink(_team1Player, _team2Player);
                foreach (var customEntity in SceneNodeManager.CustomEntities)
                {
                    MoveEntity(customEntity);
                }

                CheckForDeath(_team1Player, _team2Player);
                CheckForDeath(_team2Player, _team1Player);

                UpdateLabels();

                RenderManager.EndRender();
            }
        }

        private void LoadScript(string filename)
        {
            _txtCode.ReferencedAssemblies.AddRange(new string[] { "System.dll", "DumbBots.NET.Api.dll" });
            _txtCode.ReferencedAssemblies.AddRange(_referencedAssemblies);
            if (File.Exists(filename))
                _txtCode.Text = File.ReadAllText(filename);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //HACK: objectdisposedexception throw by irrlicht when window is maximized!?!
            this.WindowState = FormWindowState.Normal;
            pnlRendering.Enabled = false;
            Settings.Default.Save();
        }

        private void FrmMain_Deactivate(object sender, EventArgs e)
        {
            Globals.GameSpeed = 0;
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            Globals.GameSpeed = RuleManager.GameSpeed;
        }

        private void Thinking(CombatEntity entity, CombatEntity enemy)
        {
            ScriptRunner.Think(entity, enemy);
            MoveEntity(entity);
        }

        private void MoveEntity(CombatEntity entity)
        {
            entity.Node.RemoveAnimators();
            if (entity.Route.Count > 0)
            {
                Vector3D destination;
                if (entity.Route.Count > 1)
                    destination = LevelManager.SparseGraph.GetNode(entity.Route[1]).Position;
                else
                {
                    destination = LevelManager.SparseGraph.GetNode(entity.Route[0]).Position;
                }

                entity.Destination = destination;

                Line3D travelLine = new Line3D(entity.Node.Position, entity.Destination);

                if (travelLine.Length > 0)
                {
                    uint travelTime = (uint)(travelLine.Length * entity.SpeedModifier);

                    Animator anim = Globals.Scene.CreateFlyStraightAnimator(entity.Node.Position, entity.Destination, travelTime, false);
                    entity.Node.AddAnimator(anim);

                    entity.TargetRotation = travelLine.Vector.HorizontalAngle + entity.Rotation;
                }
            }
            entity.UpdateRotation();
        }

        private void CheckForDeath(CombatEntity entity, CombatEntity enemy)
        {
            if (entity.Health == 0)
            {
                SoundManager.PlayDeath();
                enemy.Score += 1;
                int temp = GetNodes.GetFurthestNodeFromPosition(enemy.Node.Position);
                entity.Destination = LevelManager.SparseGraph.GetNode(temp).Position;

                if (entity.Destination == enemy.Destination)
                {
                    temp = GetNodes.GetFurthestNodeFromPosition(enemy.Destination);
                    entity.Destination = LevelManager.SparseGraph.GetNode(temp).Position;
                }

                entity.Route = new List<int>();
                entity.Node.RemoveAnimators();
                Animator anim = Globals.Scene.CreateFlyStraightAnimator(entity.Node.Position, entity.Destination, 0, false);
                entity.Node.AddAnimator(anim);

                entity.Health = RuleManager.MaxHealth;
                entity.Ammo = RuleManager.DefaultAmmo;
            }
        }

        private void UpdateLabels()
        {
            btnPlayer1Info.Text = String.Format("Score: {0}", _team1Player.Score);
            btnPlayer2Info.Text = String.Format("Score: {0}", _team2Player.Score);
        }

        private void PnlRendering_Resize(object sender, EventArgs e)
        {
            if (Globals.Device != null)
            {
                float aspectRatio = (float)pnlRendering.Width / pnlRendering.Height;
                Globals.Scene.ActiveCamera.AspectRatio = aspectRatio;
            }
        }

        private void UpdateExternalReferences()
        {
            btnDeleteReference.Enabled = false;
            btnDeleteReference.DropDownItems.Clear();
            if (_referencedAssemblies != null && _referencedAssemblies.Count > 0)
            {
                btnDeleteReference.Enabled = true;
                foreach (string assemblyLocation in _referencedAssemblies)
                {
                    btnDeleteReference.DropDownItems.Add(assemblyLocation, null, (s, e) =>
                    {
                        _referencedAssemblies.Remove(assemblyLocation);
                        if (Settings.Default.ReferencedAssemblies != null)
                        {
                            Settings.Default.ReferencedAssemblies.Remove(assemblyLocation);
                        }
                        UpdateExternalReferences();
                    });
                }
            }
        }

        private void LabelErrors_DoubleClick(object sender, EventArgs e)
        {
            if (lbErrors.SelectedItem != null && lbErrors.SelectedItem is ErrorInformation)
            {
                var lineNumber = (lbErrors.SelectedItem as ErrorInformation).LineNumber;
                _txtCode.Lines[lineNumber - 1].Select();
                _txtCode.Focus();
            }
        }

        #region Menu Methods

        private void btnCompileTeam1_Click(object sender, EventArgs e)
        {
            CompileScript(Team.Team1);
        }

        private void btnCompileTeam2_Click(object sender, EventArgs e)
        {
            CompileScript(Team.Team2);
        }

        private void btnCompileDirector_Click(object sender, EventArgs e)
        {
            CompileScript(Team.Director);
        }

        private void CompileScript(Team team)
        {
            SoundManager.PlayUpdate();
            ScriptCompiler sc = new ScriptCompiler();

            if (team == Team.Director)
            {
                PopulateErrors(sc.CompileDirectorScript("C#", _txtCode.Text, _referencedAssemblies));
                ScriptRunner.ReflectDirectorScript();
            }
            else
            {
                PopulateErrors(sc.CompileScript("C#", _txtCode.Text, (int)team, _referencedAssemblies));
                ScriptRunner.ReflectScript(team);
            }
        }

        private void PopulateErrors(List<ErrorInformation> errorList)
        {
            lbErrors.Items.Clear();
            if (errorList.Count > 0)
            {
                lbErrors.Items.AddRange(errorList.ToArray());
            }
            else
            {
                lbErrors.Items.Add("Build succeeded");
            }
        }

        private void mnuFileSaveScript_Click(object sender, EventArgs e)
        {
            Globals.GameSpeed = 0;
            dlgSaveScript.InitialDirectory = Application.StartupPath + "\\Scripts";
            if (dlgSaveScript.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlgSaveScript.FileName, _txtCode.Text);
            }
            Globals.GameSpeed = RuleManager.GameSpeed;
        }

        private void mnuFileLoadScript_Click(object sender, EventArgs e)
        {
            dlgOpenScript.InitialDirectory = Application.StartupPath + "\\Scripts";
            if (dlgOpenScript.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.ScriptFile = dlgOpenScript.FileName;
                LoadScript(dlgOpenScript.FileName);
            }
        }

        private void mnuFileLoadMap_Click(object sender, EventArgs e)
        {
            Globals.GameSpeed = 0;
            dlgOpenMap.InitialDirectory = Application.StartupPath + "\\Maps";
            if (dlgOpenMap.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.MapFile = dlgOpenMap.FileName;
                PlayLevel(dlgOpenMap.FileName);
            }
            Globals.GameSpeed = RuleManager.GameSpeed;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void mnuSoundOff_Click(object sender, EventArgs e)
        {
            Settings.Default.Sound = SoundManager.PlaySound = false;
            UpdateSoundUI();
        }

        private void mnuSoundOn_Click(object sender, EventArgs e)
        {
            Settings.Default.Sound = SoundManager.PlaySound = true;
            UpdateSoundUI();
        }

        private void UpdateSoundUI()
        {
            if (Settings.Default.Sound)
            {
                btnSoundOn.Checked = true;
                btnSoundOff.Checked = false;
            }
            else
            {
                btnSoundOn.Checked = false;
                btnSoundOff.Checked = true;
            }
        }

        private void mnuMapEditor_Click(object sender, EventArgs e)
        {
            Globals.GameSpeed = 0;
            using (MapEditor editor = new MapEditor())
            {
                editor.ShowDialog();
            }
            Globals.GameSpeed = RuleManager.GameSpeed;
        }

        private void btnAddReference_Click(object sender, EventArgs e)
        {
            dlgAddReference.InitialDirectory = Application.StartupPath;
            if (dlgAddReference.ShowDialog() == DialogResult.OK)
            {
                _referencedAssemblies.AddRange(dlgAddReference.FileNames);
                if (Settings.Default.ReferencedAssemblies == null)
                {
                    Settings.Default.ReferencedAssemblies = new System.Collections.Specialized.StringCollection();
                }
                Settings.Default.ReferencedAssemblies.AddRange(dlgAddReference.FileNames);
            }
            UpdateExternalReferences();
        }

        private void ReferenceDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Help\\DumbBots.htm");
        }

        private void btnBasicScriptEditor_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\BasicScriptEditor\\DumbBots.BasicCoder.exe");
        }

        #endregion Menu Methods
    }
}