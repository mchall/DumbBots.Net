using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Core;
using Graphs;
using IrrlichtNETCP;

namespace Game
{
    /// <summary>
    /// Manages the creation of a level
    /// </summary>
    internal static class LevelManager
    {
        internal static MapItem[,] Map { get; private set; }
        internal static SparseGraph<NavigationNode, WeightedEdge> SparseGraph { get; set; }

        static LevelManager()
        {
            Map = new MapItem[15, 15];
        }

        internal static void UpdateLevelGraph()
        {
            if (SparseGraph == null)
            {
                CreateRoutingGraph();
            }
        }

        internal static void CreateLevel(string filename)
        {
            if (!File.Exists(filename))
            {
                CreateDefaultLevel();
            }
            else
            {
                SparseGraph = null;
                Map = new MapItem[15, 15];

                try
                {
                    int lineCount = 0;
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            while (!reader.EndOfStream)
                            {
                                string temp = reader.ReadLine();
                                for (int i = 0; i < temp.Length; i++)
                                {
                                    Map[lineCount, i] = (MapItem)int.Parse(temp[i].ToString());
                                }
                                lineCount++;
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    System.Windows.Forms.MessageBox.Show("Invalid Map size.", "Map Load Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    CreateDefaultLevel();
                }
            }
            CreateMap();
        }

        private static void CreateDefaultLevel()
        {
            int[,] defaultMap = new int[,]
             {
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                 {1,4,0,0,0,0,0,0,0,0,0,0,0,0,1},
                 {1,0,1,1,1,1,1,2,2,1,1,1,1,0,1},
                 {1,0,1,3,0,0,0,0,0,0,0,3,1,0,1},
                 {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                 {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                 {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                 {1,0,2,0,0,0,0,0,0,0,0,0,2,0,1},
                 {1,0,2,0,0,0,0,0,0,0,0,0,2,0,1},
                 {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                 {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                 {1,0,1,3,0,0,0,0,0,0,0,3,1,0,1},
                 {1,0,1,1,1,1,1,2,2,1,1,1,1,0,1},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,5,1},
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
             }; // 0 = empty, 1 = wall, 2 = bazooka, 3 = health, 4 = Team1 start, 5 = Team2 start

            Map = new MapItem[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Map[i, j] = (MapItem)defaultMap[i, j];
                }
            }
        }

        private static void CreateMap()
        {
            SceneNodeManager.CreateLight(new Vector3D(0, 0, 0));
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Vector3D pos = new Vector3D(x * 100, 30, y * 100) - new Vector3D(750, 0, 750);
                    switch (Map[x, y])
                    {
                        case (MapItem.Wall):
                            SceneNodeManager.CreateBoxSceneNode(pos);
                            break;
                        case (MapItem.Bazooka):
                            SceneNodeManager.CreateBazookaSceneNode(pos);
                            break;
                        case (MapItem.Medpack):
                            SceneNodeManager.CreateMedipackSceneNode(pos);
                            break;
                    }
                }
            }
        }

        private static void CreateRoutingGraph()
        {
            SparseGraph = new SparseGraph<NavigationNode, WeightedEdge>(true);

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    NavigationNode node = new NavigationNode(new Vector3D(x * 100, 30, y * 100) - new Vector3D(750, 0, 750), new Point(x, y));
                    bool isObstacle = (Map[x, y] == MapItem.Wall) || ((Map[x, y] == MapItem.InvisibleWall));
                    if (!isObstacle)
                        SparseGraph.AddNode(node);
                }
            }

            for (int x = 0; x < SparseGraph.NumNodes; x++)
            {
                for (int y = 0; y < SparseGraph.NumNodes; y++)
                {
                    NavigationNode xNode = SparseGraph.GetNode(x);
                    NavigationNode yNode = SparseGraph.GetNode(y);
                    if (xNode.Position.DistanceFrom(yNode.Position) < 150)
                    {
                        if (CollisionManager.CanMoveBetween(xNode.Position, yNode.Position))
                        {
                            SparseGraph.AddEdge(WeightedEdge.Create(SparseGraph.GetNode(x), SparseGraph.GetNode(y)));
                        }
                    }
                }
            }
        }

        internal static Vector3D GetTeamStartPosition(Team team)
        {
            MapItem mapItemCode;
            switch (team)
            {
                case (Team.Team1):
                    mapItemCode = MapItem.Player1Start;
                    break;
                case (Team.Team2):
                    mapItemCode = MapItem.Player2Start;
                    break;
                default:
                    throw new NotSupportedException(team.ToString());
            }

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(0); y++)
                {
                    if (Map[x, y] == mapItemCode)
                    {
                        return new Vector3D(x * 100, 30, y * 100) - new Vector3D(750, 0, 750);
                    }
                }
            }
            throw new NotSupportedException(String.Format("Start point required for {0}", team.ToString()));
        }
    }
}