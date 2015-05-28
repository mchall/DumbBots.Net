using System;
using System.Collections.Generic;
using System.Drawing;
using Game;
using IrrlichtNETCP;

namespace Graphs
{
    /// <summary>
    /// This class contains static methods for returning the furthest or nearest node to a position.
    /// </summary>
    internal static class GetNodes
    {
        internal static Vector3D GetPositionFromMapPoint(Point point)
        {
            for (int i = 0; i < LevelManager.SparseGraph.NumNodes; i++)
            {
                var node = LevelManager.SparseGraph.GetNode(i);
                if (node.MapPoint == point)
                {
                    return node.Position;
                }
            }

            return new Vector3D();
        }

        internal static List<int> GetNearestNodesToPosition(Vector3D pos)
        {
            Dictionary<int, double> distanceDictionary = new Dictionary<int, double>();
            for (int i = 0; i < LevelManager.SparseGraph.NumNodes; i++)
            {
                var node = LevelManager.SparseGraph.GetNode(i);
                distanceDictionary[i] = pos.DistanceFrom(node.Position);
            }

            List<int> nearestNodes = new List<int>();
            double smallestDistance = 50000;
            foreach (var pair in distanceDictionary)
            {
                if (pair.Value == smallestDistance)
                {
                    nearestNodes.Add(pair.Key);
                }
                if (pair.Value < smallestDistance)
                {
                    nearestNodes.Clear();
                    nearestNodes.Add(pair.Key);
                    smallestDistance = pair.Value;
                }
            }

            return nearestNodes;
        }

        internal static int GetNearestNodeToPosition(Vector3D pos)
        {
            var nodes = GetNearestNodesToPosition(pos);
            if (nodes.Count == 0)
                return -1;
            else return nodes[0];
        }

        internal static int GetFurthestNodeFromPosition(Vector3D pos)
        {
            Dictionary<int, double> distanceDictionary = new Dictionary<int, double>();
            for (int i = 0; i < LevelManager.SparseGraph.NumNodes; i++)
            {
                var node = LevelManager.SparseGraph.GetNode(i);
                distanceDictionary[i] = pos.DistanceFrom(node.Position);
            }

            int furthestNode = -1;
            double largestDistance = 0;
            foreach (var pair in distanceDictionary)
            {
                if (pair.Value > largestDistance)
                {
                    furthestNode = pair.Key;
                    largestDistance = pair.Value;
                }
            }

            return furthestNode;
        }
    }
}