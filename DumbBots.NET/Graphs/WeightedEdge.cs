using System;

namespace Graphs
{
    /// <summary>
    /// This class holds information for creating a weighted edge.
    /// Inherits from base Edge class.
    /// </summary>
    /// <remarks>
    /// Based on code from the book "Programming Game AI by Example" by Mat Buckland (ISBN:1556220782)
    /// Also based on Vicente's code from the JAD engine (which is a C# implementation of the above mentioned book's code)
    /// (http://www.codeplex.com/JADENGINE)
    /// </remarks>
    [Serializable]
    public class WeightedEdge : Edge
    {
        public double Cost { get; set; }

        public WeightedEdge()
            : base()
        {
            Cost = 1;
        }

        public WeightedEdge(int start, int end, double cost)
            : base(start, end)
        {
            this.Cost = cost;
        }

        public static WeightedEdge Create(NavigationNode start, NavigationNode end)
        {
            return new WeightedEdge(start.Index, end.Index, start.Position.DistanceFrom(end.Position));
        }
    }
}