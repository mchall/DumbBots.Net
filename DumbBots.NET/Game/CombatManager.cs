using System;
using System.Collections.Generic;
using System.Drawing;
using Core;
using Entities;
using IrrlichtNETCP;

namespace Game
{
    /// <summary>
    /// Manages combat between entities
    /// </summary>
    internal static class CombatManager
    {
        internal static List<ProjectileEntity> ProjectileList { get; private set; }

        static CombatManager()
        {
            ProjectileList = new List<ProjectileEntity>();
        }

        internal static void FireRocket(CombatEntity shooter, Point pos)
        {
            if ((shooter.ReloadTime == 0) && (shooter.Ammo > 0))
            {
                Vector3D dest = new Vector3D(pos.X, shooter.Node.Position.Y, pos.Y);

                Line3D lineRotation = new Line3D(shooter.Node.Position, dest);
                shooter.TargetRotation = lineRotation.Vector.HorizontalAngle + shooter.Rotation;

                SoundManager.PlayRocketFire();
                RocketEntity rocket = new RocketEntity(SceneNodeManager.CreateRocketSceneNode(), dest, shooter);
                FireProjectile(rocket);
                shooter.Ammo -= 1;
                shooter.TotalShots += 1;
                shooter.ReloadTime = Globals.Device.Timer.Time + rocket.ReloadTime;
            }
            else if (Globals.Device.Timer.Time >= shooter.ReloadTime)
            {
                shooter.ReloadTime = 0;
            }
        }

        internal static void FireBullet(CombatEntity shooter, Point pos)
        {
            if (shooter.ReloadTime == 0)
            {
                Vector3D dest = new Vector3D(pos.X, shooter.Node.Position.Y, pos.Y);

                Line3D lineRotation = new Line3D(shooter.Node.Position, dest);
                shooter.TargetRotation = lineRotation.Vector.HorizontalAngle + shooter.Rotation;

                SoundManager.PlayGunFire();
                BulletEntity bullet = new BulletEntity(SceneNodeManager.CreateBulletSceneNode(), dest, shooter);
                FireProjectile(bullet);
                shooter.TotalShots += 1;
                shooter.ReloadTime = Globals.Device.Timer.Time + bullet.ReloadTime;
            }
            else if (Globals.Device.Timer.Time >= shooter.ReloadTime)
            {
                shooter.ReloadTime = 0;
            }
        }

        private static void FireProjectile(ProjectileEntity rocket)
        {
            Line3D lineFire = new Line3D(rocket.Owner.Node.Position, rocket.Destination); //line of fire

            //Lengthen the path of the rocket
            Vector3D v = lineFire.Vector;
            v.SetLength(v.Length * 100000);
            v.Y = 0;
            lineFire.End = v;
            rocket.Destination = v;

            uint time = uint.Parse((Math.Round(lineFire.Length, 0)).ToString());
            Animator anim = Globals.Scene.CreateFlyStraightAnimator(rocket.Owner.Node.Position, rocket.Destination, (uint)(time * rocket.SpeedModifier), false);
            rocket.Node.AddAnimator(anim);
            ProjectileList.Add(rocket);
        }

        internal static void ResetLists()
        {
            ProjectileList = new List<ProjectileEntity>();
        }

        internal static void ClearEntities()
        {
            for (int i = 0; i < ProjectileList.Count; i++)
            {
                Globals.Scene.AddToDeletionQueue(ProjectileList[i].Node);
            }
            ProjectileList = new List<ProjectileEntity>();
        }
    }
}