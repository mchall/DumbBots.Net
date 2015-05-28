using System;
using IrrlichtNETCP;

namespace Graphs
{
    /// <summary>
    /// Heuristics to be used in the A* search
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    /// <typeparam name="N">Navigation Node</typeparam>
    /// <typeparam name="E">Weighted Edge</typeparam>
    public static class Heuristics<N, E>
        where N : NavigationNode
        where E : WeightedEdge
    {
        public static double EuclideanDistance(SparseGraph<N, E> sGraph, int node1, int node2)
        {
            return (sGraph.GetNode(node1).Position - sGraph.GetNode(node2).Position).LengthSQ;
        }

        public static double ManhattanDistance(SparseGraph<N, E> sGraph, int node1, int node2)
        {
            Vector3D aux;

            aux = sGraph.GetNode(node1).Position - sGraph.GetNode(node2).Position;
            return Math.Abs(aux.X) + Math.Abs(aux.Y) + Math.Abs(aux.Z);
        }

        public static double DjisktraDistance(SparseGraph<N, E> sGraph, int node1, int node2)
        {
            return 0;
        }
    }
}