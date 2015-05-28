using System;
using Core;
using Game;
using IrrlichtNETCP;

namespace Entities
{
    /// <summary>
    /// Represents an entity involved in combat
    /// </summary>
    internal class CombatEntity : TravellingEntity
    {
        internal Team Team { get; set; }
        internal int Score { get; set; }
        internal long ReloadTime { get; set; }
        internal int TotalShots { get; set; }
        internal int HitsTaken { get; set; }
        internal TextSceneNode FloatingText { get; set; }
        internal int CustomEntitiesDestroyed { get; set; }

        internal CombatEntity(SceneNode node, Team team)
            : base(node)
        {
            Team = team;
            Health = RuleManager.MaxHealth;
            Ammo = RuleManager.DefaultAmmo;
        }

        internal virtual double SpeedModifier
        {
            get { return RuleManager.PlayerSpeedModifier[Team]; }
            set { }
        }

        internal virtual double DamageModifier
        {
            get { return RuleManager.PlayerDamageModifier[Team]; }
            set { }
        }

        private int _health;
        internal int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                ValidateNumber(ref _health, RuleManager.MaxHealth, 0);
            }
        }

        private int _ammo;
        internal int Ammo
        {
            get { return _ammo; }
            set
            {
                _ammo = value;
                ValidateNumber(ref _ammo, RuleManager.MaxAmmo, 0);
            }
        }

        private void ValidateNumber(ref int number, int max, int min)
        {
            if (number > max)
                number = (int)max;
            if (number < min)
                number = (int)min;
        }
    }
}