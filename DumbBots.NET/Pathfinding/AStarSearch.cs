using System;
using System.Collections.Generic;
using Graphs;
using Misc;

namespace Pathfinding
{
    /// <summary>
    /// This class holds information for running an A* graph search algorithm. It takes cost into account.
    /// Inherited from base GraphSearchAlgorithm class.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    /// <typeparam name="N">Navigation Node</typeparam>
    /// <typeparam name="E">Weighted Edge</typeparam>
    public class AStarSearch<N, E> : GraphSearchAlgorithm<N, E>
        where N : NavigationNode
        where E : WeightedEdge
    {
        private List<double> _totCosts;
        private List<double> _realCosts;
        private List<E> _shortestPathTree;
        private List<E> _searchFrontier;
        private IndexedPriorityQueue<double> _queue;

        private HeuristicMethod<N, E> _heuristic;

        public AStarSearch(SparseGraph<N, E> sGraph, HeuristicMethod<N, E> heuristic)
            : base(sGraph)
        {
            _heuristic = heuristic;
        }

        public override List<int> PathOfNodes
        {
            get
            {
                List<int> path;
                int aux;

                path = new List<int>();
                if (!_found)
                {
                    return path; //return empty path
                }

                //Get the list of nodes from source to target. The shortest patrh tree is visited in reverse order
                path.Insert(0, _target);
                aux = _target;
                while (aux != _source)
                {
                    aux = _shortestPathTree[aux].Start;
                    path.Insert(0, aux);
                }

                return path;
            }
        }

        public override List<E> PathOfEdges
        {
            get
            {
                List<E> path;
                int aux;

                path = new List<E>();
                if (!_found)
                {
                    return path; //return empty path
                }

                //Get the list of edges from source to target. The shortest patrh tree is visited in reverse order
                aux = _target;
                while (aux != _source)
                {
                    path.Insert(0, _shortestPathTree[aux]);
                    aux = _shortestPathTree[aux].Start;
                }

                return path;
            }
        }

        public override void Initialize(int source, int target, List<int> nodesInUse)
        {
            _totCosts = new List<double>(_sGraph.NumNodes);
            _realCosts = new List<double>(_sGraph.NumNodes);
            _shortestPathTree = new List<E>(_sGraph.NumNodes);
            _searchFrontier = new List<E>(_sGraph.NumNodes);

            for (int i = 0; i < _sGraph.NumNodes; i++)
            {
                _totCosts.Add(0.0);
                _realCosts.Add(0.0);
                _shortestPathTree.Add(default(E));
                _searchFrontier.Add(default(E));
            }

            //Initialize the indexed priority queue and push the start node
            _queue = new IndexedPriorityQueue<double>(_totCosts);
            _queue.Push(source);

            base.Initialize(source, target, nodesInUse);
        }

        public override bool Search()
        {
            int closestNode;
            List<E> edges;
            double estCost, realCost;

            int searchIncrement = 0;

            while (_queue.Count != 0)
            {
                searchIncrement++;

                closestNode = _queue.Pop(); //Get the node with shortest cost from source node (first in queue)
                _shortestPathTree[closestNode] = _searchFrontier[closestNode];

                if (searchIncrement <= 1 && _nodesInUse.Contains(closestNode)) //Don't bother checking
                    continue;

                if (closestNode == _target) //Found target node
                {
                    _found = true;
                    return _found;
                }

                edges = _sGraph.GetEdgesFromNode(closestNode); //All edges connected to node
                for (int i = 0; i < edges.Count; i++)
                {
                    estCost = _heuristic(_sGraph, _target, edges[i].End); //Estimate cost

                    realCost = _realCosts[closestNode] + edges[i].Cost; //Cost to get to node at end of egde

                    if (_searchFrontier[edges[i].End] == null) //Add newly discovered node
                    {
                        _totCosts[edges[i].End] = realCost + estCost;
                        _realCosts[edges[i].End] = realCost;

                        _queue.Push(edges[i].End);
                        _searchFrontier[edges[i].End] = edges[i];
                    }

                    else
                    {
                        if ((realCost < _realCosts[edges[i].End]) && (_shortestPathTree[edges[i].End] == null)) //Shorter path found
                        {
                            _totCosts[edges[i].End] = realCost + estCost;
                            _realCosts[edges[i].End] = realCost;

                            _queue.ChangePriority(edges[i].End);
                            _searchFrontier[edges[i].End] = edges[i];
                        }
                    }
                }
            }

            _found = false;
            return _found;
        }
    }
}