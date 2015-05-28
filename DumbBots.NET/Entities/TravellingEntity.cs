using System;
using System.Collections.Generic;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents an entity that travels around the map
    /// </summary>
    internal class TravellingEntity : Entity
    {
        internal List<int> Route { get; set; }
        internal Vector3D Destination { get; set; }

        internal TravellingEntity(SceneNode node)
            : base(node)
        {
            Route = new List<int>();
            Destination = node.Position;
        }
    }
}