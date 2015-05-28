namespace Graphs
{
    /// <summary>
    /// This delegate represents an heuristic distance function for the A* search
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    /// <typeparam name="N">Navigation Node</typeparam>
    /// <typeparam name="E">Weighted Edge</typeparam>
    /// <param name="sGraph">Graph where search takes place</param>
    /// <param name="node1">First node</param>
    /// <param name="node2">Second node</param>
    /// <returns>The heuristic distance from node1 to node2 in the graph</returns>
    public delegate double HeuristicMethod<N, E>(SparseGraph<N, E> sGraph, int node1, int node2)
        where N : NavigationNode
        where E : WeightedEdge;
}