using System;
using IrrlichtNETCP;
using System.Drawing;

namespace Graphs
{
    /// <summary>
    /// This class holds information for creating a navigation node.
    /// Inherits from base Node class.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE) 
    /// </remarks>
    [Serializable]
    public class NavigationNode : Node
    {
        public Vector3D Position { get; private set; }
        public Point MapPoint { get; private set; }

        public NavigationNode()
            : base()
        { }

        public NavigationNode(Vector3D pos, Point mapPoint)
            : base()
        {
            Position = pos;
            MapPoint = mapPoint;
        }
    }
}