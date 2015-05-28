using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphs
{
    /// <summary>
    /// This class holds information for creating a basic node.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE)
    /// </remarks>
    [Serializable]
    public class Node
    {
        public const int InvalidNode = -1;

        public int Index { get; set; }

        public Node()
        {
            Index = Node.InvalidNode;
        }
    }
}