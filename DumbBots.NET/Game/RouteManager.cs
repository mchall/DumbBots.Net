using System;
using System.Collections.Generic;
using Entities;
using Graphs;
using Pathfinding;

namespace Game
{
    /// <summary>
    /// Class manages the creation of a path search algorithm and returns the result
    /// </summary>
    internal static class RouteManager
    {
        private static List<int> GetNodesInUse(TravellingEntity owner)
        {
            List<int> usedNodes = new List<int>();
            UpdateUsedNodesList(ref usedNodes, SceneNodeManager.Player1, owner);
            UpdateUsedNodesList(ref usedNodes, SceneNodeManager.Player2, owner);

            foreach (var customEntity in SceneNodeManager.CustomEntities)
            {
                if (customEntity.IsObstacle)
                    UpdateUsedNodesList(ref usedNodes, customEntity, owner);
            }

            usedNodes.RemoveAll(n => n == GetNodes.GetNearestNodeToPosition(owner.Node.Position));

            return usedNodes;
        }

        private static void UpdateUsedNodesList(ref List<int> usedNodesList, TravellingEntity entity, TravellingEntity owner)
        {
            if (owner == entity) return;
            usedNodesList.Add(GetNodes.GetNearestNodeToPosition(entity.Node.Position));
        }

        internal static List<int> Search(TravellingEntity owner, int start, int end)
        {
            AStarSearch<NavigationNode, WeightedEdge> asSearch =
                new AStarSearch<NavigationNode, WeightedEdge>(LevelManager.SparseGraph, new HeuristicMethod<NavigationNode,
                WeightedEdge>(Heuristics<NavigationNode, WeightedEdge>.EuclideanDistance));
            asSearch.Initialize(start, end, GetNodesInUse(owner));
            if (asSearch.Search())
            {
                return asSearch.PathOfNodes;
            }
            return new List<int>();
        }
    }
}