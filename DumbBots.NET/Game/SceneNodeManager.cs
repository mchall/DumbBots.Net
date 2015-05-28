using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Entities;
using Graphs;
using IrrlichtNETCP;

namespace Game
{
    /// <summary>
    /// This static class manages the creation of simple scene node objects
    /// </summary>
    internal static class SceneNodeManager
    {
        internal static CombatEntity Player1 { get; private set; }
        internal static CombatEntity Player2 { get; private set; }

        internal static List<WallEntity> WallSceneNodes { get; private set; }
        internal static List<CustomEntity> CustomEntities { get; private set; }
        internal static List<BazookaEntity> BazookaEntities { get; private set; }
        internal static List<MedkitEntity> MedkitEntities { get; private set; }

        internal static List<BazookaEntity> VisibleBazookaEntities
        {
            get { return BazookaEntities.Where(e => e.Node.Visible).ToList(); }
        }

        internal static List<MedkitEntity> VisibleMedkitEntities
        {
            get { return MedkitEntities.Where(e => e.Node.Visible).ToList(); }
        }

        static SceneNodeManager()
        {
            WallSceneNodes = new List<WallEntity>();
            CustomEntities = new List<CustomEntity>();
            BazookaEntities = new List<BazookaEntity>();
            MedkitEntities = new List<MedkitEntity>();
        }

        internal static CombatEntity CreateTeam1SceneNode(Vector3D position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh("Models\\sphere.3ds");
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\SphereRed.png"));
            node.Position = position;
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            node.ID = 1;
            Player1 = new CombatEntity(node, Team.Team1) { Rotation = new Vector3D(0, 180, 0) };
            return Player1;
        }

        internal static CombatEntity CreateTeam2SceneNode(Vector3D position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh("Models\\sphere.3ds");
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\SphereBlue.png"));
            node.Position = position;
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            node.ID = 2;
            Player2 = new CombatEntity(node, Team.Team2) { Rotation = new Vector3D(0, 180, 0) };
            return Player2;
        }

        internal static TextSceneNode CreateBillboardText(SceneNode node, string message)
        {
            return CreateBillboardText(node, message, System.Drawing.Color.Green);
        }

        internal static TextSceneNode CreateBillboardText(SceneNode node, string message, System.Drawing.Color colour)
        {
            GUIFont font = Globals.Gui.GetFont("Textures\\roboto.png");
            TextSceneNode txtnode = Globals.Scene.AddBillboardTextSceneNode(font, message, node, new Dimension2Df(message.Length * 40, 150), new Vector3D(0, 100, 0), -1, Color.FromBCL(colour), Color.FromBCL(colour));
            txtnode.SetMaterialFlag(MaterialFlag.Lighting, false);
            return txtnode;
        }

        internal static SceneNode CreateBoxSceneNode(Vector3D position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh("Models\\wall.3ds");
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\brick_d.jpg"));
            node.Position = position;
            node.ID = 3;
            WallSceneNodes.Add(new WallEntity(node));
            return node;
        }

        internal static LightSceneNode CreateLight(Vector3D position)
        {
            LightSceneNode light = Globals.Scene.AddLightSceneNode(null, position + new Vector3D(0, 250, 0), Colorf.TransparentWhite, 1500f, -1);
            return light;
        }

        internal static SceneNode CreateRocketSceneNode()
        {
            SceneNode node = Globals.Scene.AddSphereSceneNode(10f, 20, null);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\rocket.jpg"));
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            return node;
        }

        internal static SceneNode CreateBulletSceneNode()
        {
            SceneNode node = Globals.Scene.AddSphereSceneNode(5f, 20, null);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\rocket.jpg"));
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            return node;
        }

        internal static AnimatedMeshSceneNode CreateBazookaSceneNode(Vector3D position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh("Models\\Bazooka.md2");
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\bazooka.jpg"));
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            node.Scale = new Vector3D(1.5f, 1.5f, 1.5f);
            Animator animation = Globals.Scene.CreateRotationAnimator(new Vector3D(0, 1, 0)); //Rotate bazooka
            node.AddAnimator(animation);
            node.Position = position;
            BazookaEntities.Add(new BazookaEntity(node));
            return node;
        }

        internal static AnimatedMeshSceneNode CreateMedipackSceneNode(Vector3D position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh("Models\\medpack.md2");
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture("Textures\\medkit.png"));
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            node.Scale = new Vector3D(2f, 2f, 2f);
            Animator animation = Globals.Scene.CreateRotationAnimator(new Vector3D(0, 1, 0)); //Rotate medkit
            node.AddAnimator(animation);
            node.Position = position;
            MedkitEntities.Add(new MedkitEntity(node));
            return node;
        }

        internal static CustomEntity CreateCustomEntity(string modelFile, string textureFile, System.Drawing.Point position)
        {
            AnimatedMesh mesh = Globals.Scene.GetMesh(modelFile);
            AnimatedMeshSceneNode node = Globals.Scene.AddAnimatedMeshSceneNode(mesh);
            node.SetMaterialTexture(0, Globals.Driver.GetTexture(textureFile));
            node.SetMaterialFlag(MaterialFlag.Lighting, false);
            node.Position = new Vector3D(position.X, 30, position.Y);
            node.AnimationSpeed = 0;

            CustomEntity entity = new CustomEntity(node);
            CustomEntities.Add(entity);

            return entity;
        }

        internal static CameraSceneNode CreateCameraSceneNode(Vector3D position)
        {
            CameraSceneNode camera = Globals.Scene.AddCameraSceneNodeMaya(null, 100f, 100f, 1000f, -1);
            camera.Position = position;
            return camera;
        }

        /// <summary>
        /// Resets the numbering of the nodes
        /// </summary>
        internal static void ResetNodes()
        {
            Globals.Scene.Clear();
            WallSceneNodes = new List<WallEntity>();
            CustomEntities = new List<CustomEntity>();
            BazookaEntities = new List<BazookaEntity>();
            MedkitEntities = new List<MedkitEntity>();
        }
    }
}