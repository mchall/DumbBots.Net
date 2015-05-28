using System;
using System.Collections.Generic;
using Misc;

namespace Graphs
{
    /// <summary>
    /// This class holds information for creating a sparse graph.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    /// <typeparam name="N">Node type</typeparam>
    /// <typeparam name="E">Edge type</typeparam>
    public class SparseGraph<N, E>
        where N : Node
        where E : Edge
    {
        private List<N> _nodes;
        private List<List<E>> _edges;
        private bool _isDigraph;
        private int _nextNode;

        public SparseGraph(bool isDigraph)
        {
            this._isDigraph = isDigraph;
            _nextNode = 0; //New graph - so first node will have index 0
            _nodes = new List<N>();
            _edges = new List<List<E>>();
        }

        public int NumNodes
        {
            get { return _nodes.Count; }
        }

        public int NumActiveNodes
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _nodes.Count; ++i)
                {
                    if (_nodes[i].Index != -1)
                    {
                        ++count;
                    }
                }
                return count;
            }
        }

        public int NumEdges
        {
            get
            {
                int total = 0;
                for (int i = 0; i < _edges.Count; ++i)
                {
                    total += _edges[i].Count;
                }
                return total;
            }
        }

        public bool isNodePresent(int index)
        {
            if ((_nodes[index].Index == -1) || (index >= _nodes.Count) || (index < 0))
            {
                return false;
            }
            return true;
        }

        public bool isEdgePresent(int start, int end)
        {
            if (isNodePresent(start) && isNodePresent(end))
            {
                for (int i = 0; i < _edges[start].Count; ++i)
                {
                    if (_edges[start][i].End == end) //Start point connected to end point
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public N GetNode(int index)
        {
            if ((index < 0) || (index >= _nodes.Count))
            {
                throw new Exception("Invalid index");
            }
            return _nodes[index];
        }

        public E GetEdge(int start, int end)
        {
            if (!isNodePresent(end))
            {
                throw new Exception("Invalid End Point");
            }
            if (!isNodePresent(start))
            {
                throw new Exception("Invalid Start Point");
            }
            for (int i = 0; i < _edges[start].Count; ++i)
            {
                if (_edges[start][i].End == end) //Start point connected to end point
                {
                    return _edges[start][i];
                }
            }
            throw new Exception("Invalid edge");
        }

        public List<E> GetEdgesFromNode(int nodeIndex)
        {
            if ((nodeIndex < 0) || (nodeIndex > _nodes.Count))
            {
                throw new Exception("Invalid index");
            }
            return _edges[nodeIndex];
        }

        public void AddEdge(E edge)
        {
            if (isNodePresent(edge.Start) && isNodePresent(edge.End))
            {
                if (!isEdgePresent(edge.Start, edge.End)) //Check if it already exists
                {
                    _edges[edge.Start].Add(edge);
                }
            }
            if (_isDigraph)
            {
                //Will add the reversed edge to the graph
                //For example: Node A and Node B are connected by edge a->b
                //In a digraph there will then also need to be edge b->a
                E newEdge = (E)edge.Clone();
                newEdge.Start = edge.End;
                newEdge.End = edge.Start;

                _edges[edge.End].Add(newEdge);
            }
        }

        public void RemoveEdge(int start, int end)
        {
            if (isNodePresent(start) && isNodePresent(end))
            {
                for (int i = 0; i < _edges[start].Count; ++i)
                {
                    if (_edges[start][i].End == end)
                    {
                        _edges[start].Remove(_edges[start][i]);
                        break;
                    }
                }

                if (_isDigraph) //Remove the edge going in the opposite direction
                {
                    for (int i = 0; i < _edges[end].Count; ++i)
                    {
                        if (_edges[end][i].End == start)
                        {
                            _edges[start].Remove(_edges[end][i]);
                            break;
                        }
                    }
                }
            }
        }

        public void AddNode(N node)
        {
            if (node.Index == Node.InvalidNode)
            {
                node.Index = _nextNode++;
                _nodes.Add(node);
                _edges.Add(new List<E>());
            }
            else
            {
                throw new Exception("A node can´t have an index before being added to a graph");
            }
        }
    }
}