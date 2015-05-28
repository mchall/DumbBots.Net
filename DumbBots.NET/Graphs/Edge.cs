using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphs
{
    /// <summary>
    /// This class holds information for creating a basic edge.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE)
    /// </remarks>
    [Serializable]
    public class Edge
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Edge()
        {
            Start = Node.InvalidNode;
            End = Node.InvalidNode;
        }

        public Edge(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}