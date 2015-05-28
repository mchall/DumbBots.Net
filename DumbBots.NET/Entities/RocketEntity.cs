using System;
using Game;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents a rocket entity
    /// </summary>
    internal class RocketEntity : ProjectileEntity
    {
        internal RocketEntity(SceneNode node, Vector3D destination, CombatEntity owner)
            : base(node, destination, owner)
        { }

        internal override int DamageDealt
        {
            get { return RuleManager.RocketDamage; }
        }

        internal override int SpeedModifier
        {
            get { return RuleManager.RocketSpeed; }
        }

        internal override int ReloadTime
        {
            get { return RuleManager.RocketReloadTime; }
        }
    }
}