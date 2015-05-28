using System;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents a projectile entity
    /// </summary>
    internal abstract class ProjectileEntity : Entity
    {
        internal Vector3D Destination { get; set; }
        internal CombatEntity Owner { get; set; }

        internal ProjectileEntity(SceneNode node, Vector3D destination, CombatEntity owner)
            : base(node)
        {
            Destination = destination;
            Owner = owner;
        }

        internal abstract int DamageDealt { get; }
        internal abstract int SpeedModifier { get; }
        internal abstract int ReloadTime { get; }
    }
}