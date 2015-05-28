using System;
using System.Collections.Generic;
using Graphs;

namespace Pathfinding
{
    /// <summary>
    /// This base class holds information for running a search algorithm.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    /// <typeparam name="N">Node</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    public abstract class GraphSearchAlgorithm<N, E>
        where N : Node
        where E : Edge
    {
        protected SparseGraph<N, E> _sGraph;
        protected int _source;
        protected int _target;
        protected bool _found;
        protected List<int> _nodesInUse;

        public abstract List<int> PathOfNodes { get; }
        public abstract List<E> PathOfEdges { get; }

        public GraphSearchAlgorithm(SparseGraph<N, E> sGraph)
        {
            _sGraph = sGraph;
            _found = false;
        }

        public virtual void Initialize(int source, int target, List<int> nodesInUse)
        {
            _source = source;
            _target = target;
            _nodesInUse = nodesInUse;
            _found = false;
        }

        public abstract bool Search();
    }
}