using System;
using Game;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents a custom entity
    /// </summary>
    internal class CustomEntity : CombatEntity
    {
        internal bool Shootable { get; set; }
        internal bool IsObstacle { get; set; }

        private double _speedModifier;
        internal override double SpeedModifier
        {
            get { return _speedModifier; }
            set { _speedModifier = value; }
        }

        private double _damageModifier;
        internal override double DamageModifier
        {
            get { return _damageModifier; }
            set { _damageModifier = value; }
        }

        internal CustomEntity(SceneNode node)
            : base(node, Core.Team.Custom)
        {
            _damageModifier = 1;
            _speedModifier = 1;
            Health = 1;
            Ammo = 1;
        }
    }
}