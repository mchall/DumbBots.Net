using System;
using IrrlichtNETCP;
using Game;

namespace Entities
{
    /// <summary>
    /// Represents a bullet entity
    /// </summary>
    internal class BulletEntity : ProjectileEntity
    {
        internal BulletEntity(SceneNode node, Vector3D destination, CombatEntity owner)
            : base(node, destination, owner)
        { }

        internal override int DamageDealt
        {
            get { return RuleManager.BulletDamage; }
        }

        internal override int SpeedModifier
        {
            get { return RuleManager.BulletSpeedModifier; }
        }

        internal override int ReloadTime
        {
            get { return RuleManager.BulletReloadTime; }
        }
    }
}