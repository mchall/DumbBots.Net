using System;
using Core;
using Entities;
using IrrlichtNETCP;

namespace Game
{
    /// <summary>
    /// This class is responsible for handling the rendering of the debug information.
    /// </summary>
    internal static class DebugRenderer
    {
        private static bool _nodesShown;

        internal static bool ShowDebugInfo
        {
            get { return false; }
        }

        internal static void DrawBoundingBoxes()
        {
            if (ShowDebugInfo)
            {
                Globals.Driver.Draw2DLine(new IrrlichtNETCP.Position2D(0, 0), new IrrlichtNETCP.Position2D(0, 0), IrrlichtNETCP.Color.Blue);

                foreach (var entity in SceneNodeManager.WallSceneNodes)
                {
                    Globals.Driver.Draw3DBox(entity.BoundingBox, Color.Blue);
                }

                foreach (var entity in SceneNodeManager.BazookaEntities)
                {
                    Globals.Driver.Draw3DBox(entity.BoundingBox, Color.Blue);
                }

                foreach (var entity in SceneNodeManager.MedkitEntities)
                {
                    Globals.Driver.Draw3DBox(entity.BoundingBox, Color.Blue);
                }

                foreach (var entity in SceneNodeManager.CustomEntities)
                {
                    Globals.Driver.Draw3DBox(entity.BoundingBox, Color.Blue);
                }

                foreach (var entity in CombatManager.ProjectileList)
                {
                    Globals.Driver.Draw3DBox(entity.BoundingBox, Color.Blue);
                    Globals.Driver.Draw3DLine(entity.Node.Position, entity.Destination, Color.Purple);
                }

                Globals.Driver.Draw3DBox(Globals.Scene.GetSceneNodeFromID(1).TransformedBoundingBox, Color.Blue);
                Globals.Driver.Draw3DBox(Globals.Scene.GetSceneNodeFromID(2).TransformedBoundingBox, Color.Blue);

                if (LevelManager.SparseGraph != null)
                {
                    for (int i = 0; i < LevelManager.SparseGraph.NumNodes; i++)
                    {
                        if (!_nodesShown)
                        {
                            var node = LevelManager.SparseGraph.GetNode(i);
                            Globals.Scene.AddBillboardTextSceneNode(Globals.Gui.BuiltInFont, i.ToString(), null, new Dimension2Df(50, 50), node.Position, -1, Color.Red, Color.Red);
                        }

                        foreach (var edge in LevelManager.SparseGraph.GetEdgesFromNode(i))
                        {
                            Vector3D start = LevelManager.SparseGraph.GetNode(edge.Start).Position;
                            Vector3D end = LevelManager.SparseGraph.GetNode(edge.End).Position;
                            Globals.Driver.Draw3DLine(start, end, Color.Purple);
                        }
                    }

                    _nodesShown = true;
                }
                else
                {
                    _nodesShown = false;
                }
            }
        }

        internal static void DrawCollisionBoundingBox(Entity node, Color color)
        {
            if (ShowDebugInfo)
                Globals.Driver.Draw3DBox(node.BoundingBox, color);
        }

        internal static void DrawLine3D(Line3D line, Color color)
        {
            if (ShowDebugInfo)
                Globals.Driver.Draw3DLine(line, color);
        }
    }
}