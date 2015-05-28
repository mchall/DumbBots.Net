using System;
using System.Collections.Generic;
using Core;

namespace Game
{
    /// <summary>
    /// This class is responsible for the game world rules
    /// </summary>
    internal static class RuleManager
    {
        internal static float GameSpeed { get; set; }

        internal static Dictionary<Team, double> PlayerSpeedModifier { get; set; }
        internal static Dictionary<Team, double> PlayerDamageModifier { get; set; }

        internal static int BulletDamage { get; set; }
        internal static int BulletSpeedModifier { get; set; }
        internal static int BulletReloadTime { get; set; }

        internal static int RocketDamage { get; set; }
        internal static int RocketSpeed { get; set; }
        internal static int RocketReloadTime { get; set; }

        internal static int MaxHealth { get; set; }
        internal static int MaxAmmo { get; set; }
        internal static int DefaultAmmo { get; set; }

        internal static int MedkitHealthBoost { get; set; }

        internal static int MedkitRespawnTime { get; set; }
        internal static int BazookaRespawnTime { get; set; }

        static RuleManager()
        {
            Reset();
        }

        internal static void Reset()
        {
            GameSpeed = 1f;

            PlayerSpeedModifier = new Dictionary<Team, double>();
            PlayerSpeedModifier[Team.Team1] = 5;
            PlayerSpeedModifier[Team.Team2] = 5;

            PlayerDamageModifier = new Dictionary<Team, double>();
            PlayerDamageModifier[Team.Team1] = 1;
            PlayerDamageModifier[Team.Team2] = 1;

            BulletDamage = 10;
            BulletSpeedModifier = 2;
            BulletReloadTime = 400;

            RocketDamage = 40;
            RocketSpeed = 1;
            RocketReloadTime = 2000;

            MaxHealth = 100;
            MaxAmmo = 3;
            DefaultAmmo = 1;

            MedkitHealthBoost = 25;

            MedkitRespawnTime = 10000;
            BazookaRespawnTime = 10000;
        }
    }
}