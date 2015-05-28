using System;
using System.Collections.Generic;
using System.Linq;

namespace DumbBots.BasicCoder
{
    public static class Commands
    {
        public static List<Command> CommandList
        {
            get
            {
                return new List<Command>()
                {
                    //Constants
                    new MaxAmmoCommand(),
                    new MaxHealthCommand(),
                    new TrueCommand(),
                    new FalseCommand(),

                    //Movement
                    new GetNearestBazookaCommand(),
                    new GetNearestMedkitCommand(),
                    new GetRandomBazookaCommand(),
                    new GetRandomMedkitCommand(),
                    new MoveRandomCommand(),
                    new MoveToPointCommand(),
                    new StopCommand(),

                    //World information
                    new GetBazookaCountCommand(),
                    new GetMedkitCountCommand(),

                    //Player information
                    new GetHealthCommand(),
                    new GetPositionCommand(),
                    new GetAmmoCommand(),
                    new SayTextCommand(),

                    //Enemy information
                    new GetEnemySightedCommand(),
                    new GetEnemyPositionCommand(),

                    //Combat
                    new HurtSelfCommand(),
                    new ShootBulletCommand(),
                    new ShootRocketCommand(),
                };
            }
        }
    }
}