using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Entities;
using IrrlichtNETCP;

namespace Game
{
    /// <summary>
    /// Handles the collisions of the game
    /// </summary>
    internal static class CollisionManager
    {
        private static Dictionary<Entity, long> _respawnDictionary;

        static CollisionManager()
        {
            _respawnDictionary = new Dictionary<Entity, long>();
        }

        internal static bool CanMoveBetween(Vector3D start, Vector3D end)
        {
            Line3D line = new Line3D(start, end);

            foreach (var wallNode in SceneNodeManager.WallSceneNodes)
            {
                if (IntersectsWithLine(wallNode.BoundingBox, line))
                    return false;
            }
            foreach (var customEntity in SceneNodeManager.CustomEntities.Where(e => e.IsObstacle))
            {
                if (IntersectsWithLine(customEntity.BoundingBox, line))
                    return false;
            }
            return true;
        }

        internal static bool EntityVisible(Entity origin, Entity target)
        {
            Vector3D[] originEdges = null;
            origin.BoundingBox.GetEdges(out originEdges);

            Vector3D[] targetEdges = null;
            target.BoundingBox.GetEdges(out targetEdges);

            foreach (var originEdge in originEdges)
            {
                foreach (var targetEdge in targetEdges)
                {
                    Line3D line = new Line3D(originEdge, targetEdge);
                    DebugRenderer.DrawLine3D(line, Color.Red);
                    foreach (var wallNode in SceneNodeManager.WallSceneNodes)
                    {
                        if (IntersectsWithLine(wallNode.BoundingBox, line))
                            return false;
                    }
                }
            }

            return true;
        }

        private static bool IntersectsWithLine(Box3D box, Line3D line)
        {
            Vector3D lineVector = line.Vector.Normalize();
            float halfLength = (float)(line.Length * 0.5);
            Vector3D t = box.Center - line.Middle;
            Vector3D e = (box.MaxEdge - box.MinEdge); e = e * (float)(0.5);

            if ((Math.Abs(t.X) > e.X + halfLength * Math.Abs(lineVector.X)) ||
                (Math.Abs(t.Y) > e.Y + halfLength * Math.Abs(lineVector.Y)) ||
                (Math.Abs(t.Z) > e.Z + halfLength * Math.Abs(lineVector.Z)))
                return false;

            float r = e.Y * (float)Math.Abs(lineVector.Z) + e.Z * Math.Abs(lineVector.Y);
            if (Math.Abs(t.Y * lineVector.Z - t.Z * lineVector.Y) > r)
                return false;

            r = e.X * (float)Math.Abs(lineVector.Z) + e.Z * Math.Abs(lineVector.X);
            if (Math.Abs(t.Z * lineVector.X - t.X * lineVector.Z) > r)
                return false;

            r = e.X * (float)Math.Abs(lineVector.Y) + e.Y * Math.Abs(lineVector.X);
            if (Math.Abs(t.X * lineVector.Y - t.Y * lineVector.X) > r)
                return false;

            return true;
        }

        internal static void CheckProjectileCollisions(CombatEntity entity)
        {
            List<ProjectileEntity> projectilesToRemove = new List<ProjectileEntity>();
            foreach (ProjectileEntity projectile in CombatManager.ProjectileList)
            {
                if (projectile.BoundingBox.IntersectsWithBox(entity.BoundingBox)) //Hits entity
                {
                    if (entity != projectile.Owner) //Entity did not hit itself
                    {
                        SoundManager.PlayPain();
                        entity.Health -= (int)(projectile.DamageDealt * projectile.Owner.DamageModifier);
                        entity.HitsTaken += 1;

                        projectilesToRemove.Add(projectile);
                        continue;
                    }
                }
                foreach (WallEntity node in SceneNodeManager.WallSceneNodes)
                {
                    if (projectile.BoundingBox.IntersectsWithBox(node.BoundingBox))
                    {
                        projectilesToRemove.Add(projectile);
                        DebugRenderer.DrawCollisionBoundingBox(node, Color.Red);
                        break;
                    }
                }
                for (int i = 0; i < SceneNodeManager.CustomEntities.Count; i++)
                {
                    var customEntity = SceneNodeManager.CustomEntities[i];
                    if (customEntity.Shootable == true)
                    {
                        if (projectile.BoundingBox.IntersectsWithBox(customEntity.BoundingBox))
                        {
                            if (entity.Node.ID == projectile.Owner.Node.ID)
                            {
                                customEntity.Health -= (int)(projectile.DamageDealt * projectile.Owner.DamageModifier);
                                if (customEntity.Health == 0)
                                {
                                    Globals.Scene.AddToDeletionQueue(customEntity.Node);
                                    SceneNodeManager.CustomEntities.Remove(customEntity);

                                    entity.CustomEntitiesDestroyed += 1;
                                }

                                projectilesToRemove.Add(projectile);
                                break;
                            }
                        }
                    }
                }
            }

            projectilesToRemove.ForEach(p =>
                {
                    CombatManager.ProjectileList.Remove(p);
                    Globals.Scene.AddToDeletionQueue(p.Node);
                });
        }

        internal static void ClearSpawningItems()
        {
            _respawnDictionary.Clear();
        }

        internal static void CheckSpawningItems()
        {
            foreach (var pair in _respawnDictionary)
            {
                if (Globals.Device.Timer.Time >= pair.Value)
                {
                    var entity = pair.Key;
                    entity.Node.Visible = true;
                }
            }
        }

        internal static void CheckMedkitCollisions(CombatEntity player)
        {
            foreach (MedkitEntity medkit in SceneNodeManager.VisibleMedkitEntities)
            {
                if ((player.Health < RuleManager.MaxHealth) && (player.BoundingBox.IntersectsWithBox(medkit.BoundingBox)))
                {
                    SoundManager.PlayGetHealth();
                    medkit.Node.Visible = false;
                    _respawnDictionary[medkit] = Globals.Device.Timer.Time + RuleManager.MedkitRespawnTime;
                    player.Health += medkit.HealthBoost;
                }
            }
        }

        internal static void CheckBazookaCollisions(CombatEntity player)
        {
            foreach (BazookaEntity bazooka in SceneNodeManager.VisibleBazookaEntities)
            {
                if ((player.Ammo < RuleManager.MaxAmmo) && (player.BoundingBox.IntersectsWithBox(bazooka.BoundingBox)))
                {
                    SoundManager.PlayGetBazooka();
                    bazooka.Node.Visible = false;
                    _respawnDictionary[bazooka] = Globals.Device.Timer.Time + RuleManager.BazookaRespawnTime;
                    player.Ammo++;
                }
            }
        }

        internal static void CheckPlayerCollisions(CombatEntity player, CombatEntity other)
        {
            if (player.BoundingBox.IntersectsWithBox(other.BoundingBox))
            {
                player.Health -= (int)(1 * other.DamageModifier);
                other.Health -= (int)(1 * player.DamageModifier);
            }
        }
    }
}