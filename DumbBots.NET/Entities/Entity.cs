using System;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents a basic moving entity
    /// </summary>
    internal abstract class Entity
    {
        internal SceneNode Node { get; set; }
        internal Vector3D Rotation { get; set; }
        internal Vector3D TargetRotation { get; set; }

        internal Box3D BoundingBox
        {
            get { return Node.TransformedBoundingBox; }
        }

        internal Entity(SceneNode node)
        {
            Node = node;
            Rotation = new Vector3D(0, 0, 0);
        }

        internal void UpdateRotation()
        {
            Node.Rotation = Node.Rotation.GetInterpolated(TargetRotation, 0.9f);
        }
    }
}