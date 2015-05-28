using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Core;
using DumbBotsNET.Api;
using Entities;
using Game;
using Graphs;
using IrrlichtNETCP;
using Messaging;
using Misc;

namespace Scripting
{
    /// <summary>
    /// Static class that allows the script to call simplified methods
    /// </summary>
    public class ScriptMethods : IPlayerApi
    {
        private class RandomLocationInfo
        {
            public int DestinationNode { get; set; }
            public uint Timeout { get; set; }
        }

        internal CombatEntity Entity { get; set; }
        internal CombatEntity Enemy { get; set; }

        private Dictionary<CombatEntity, RandomLocationInfo> _randomLocations;

        public ScriptMethods()
        {
            _randomLocations = new Dictionary<CombatEntity, RandomLocationInfo>();
        }

        #region Extensibility

        public Point[] GetCustomEntityPositions()
        {
            Point[] pointArray = new Point[SceneNodeManager.CustomEntities.Count];
            for (int i = 0; i < SceneNodeManager.CustomEntities.Count; i++)
            {
                pointArray[i] = new Point((int)SceneNodeManager.CustomEntities[i].Node.Position.X, (int)SceneNodeManager.CustomEntities[i].Node.Position.Z);
            }
            return pointArray;
        }

        public int GetCustomEntitiesDestroyed()
        {
            return Entity.CustomEntitiesDestroyed;
        }

        public bool CustomEntityVisible(Point point)
        {
            foreach (var customEntity in SceneNodeManager.CustomEntities)
            {
                if (customEntity.Node.Position.DistanceFrom(new Vector3D(point.X, 30, point.Y)) < 1)
                {
                    return CollisionManager.EntityVisible(Entity, customEntity);
                }
            }
            return false;
        }

        #endregion Extensibility

        #region Entity movement

        public void Stop()
        {
            Entity.Node.RemoveAnimators();
            Entity.Route.Clear();
        }

        public bool MoveToPoint(Point p)
        {
            Vector3D pos = new Vector3D(p.X, 30, p.Y);
            var nodes = GetNodes.GetNearestNodesToPosition(pos);
            foreach (var node in nodes)
            {
                List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), node);
                if (route.Count > 1)
                {
                    Entity.Route = route;
                    return true;
                }
            }
            return false;
        }

        public bool MoveUp()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(-100, 0, 0);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveUpLeft()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(-100, 0, -100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveUpRight()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(-100, 0, 100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveDown()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(100, 0, 0);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveDownLeft()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(100, 0, -100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveDownRight()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(100, 0, 100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveRight()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(0, 0, 100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public bool MoveLeft()
        {
            Vector3D pos = Entity.Node.Position + new Vector3D(0, 0, -100);
            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
            Entity.Route = route;
            return route.Count > 1;
        }

        public void MoveToRandomLocation()
        {
            if (!_randomLocations.ContainsKey(Entity))
                _randomLocations.Add(Entity, new RandomLocationInfo());

            int destination;
            if (_randomLocations[Entity].Timeout >= Globals.Device.Timer.Time && Entity.Route.Count > 1)
            {
                destination = _randomLocations[Entity].DestinationNode;
            }
            else
            {
                Random rand = new Random();
                destination = rand.Next(LevelManager.SparseGraph.NumNodes);
                _randomLocations[Entity].DestinationNode = destination;
                _randomLocations[Entity].Timeout = Globals.Device.Timer.Time + 1000;
            }

            List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), destination);
            Entity.Route = route;
        }

        #endregion Entity movement

        #region Get Items

        public void GetRandomBazooka()
        {
            if (SceneNodeManager.VisibleBazookaEntities.Count > 0)
            {
                Random rand = new Random();
                Vector3D pos = SceneNodeManager.VisibleBazookaEntities[rand.Next(SceneNodeManager.VisibleBazookaEntities.Count)].Node.Position;
                List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
                Entity.Route = route;
            }
        }

        public void GetNearestBazooka()
        {
            int shortestRoute = 5000;
            SceneNodeManager.VisibleBazookaEntities.ForEach(bazooka =>
            {
                Vector3D pos = bazooka.Node.Position;
                List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
                if (route.Count > 0 && route.Count < shortestRoute)
                {
                    Entity.Route = route;
                    shortestRoute = route.Count;
                }
            });
        }

        public void GetRandomMedkit()
        {
            if (SceneNodeManager.VisibleMedkitEntities.Count > 0)
            {
                Random rand = new Random();
                Vector3D pos = SceneNodeManager.VisibleMedkitEntities[rand.Next(SceneNodeManager.VisibleMedkitEntities.Count)].Node.Position;
                List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
                Entity.Route = route;
            }
        }

        public void GetNearestMedkit()
        {
            int shortestRoute = 5000;
            SceneNodeManager.VisibleMedkitEntities.ForEach(medkit =>
                {
                    Vector3D pos = medkit.Node.Position;
                    List<int> route = RouteManager.Search(Entity, GetNodes.GetNearestNodeToPosition(Entity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
                    if (route.Count > 0 && route.Count < shortestRoute)
                    {
                        Entity.Route = route;
                        shortestRoute = route.Count;
                    }
                });
        }

        #endregion Get Items

        #region Messaging

        public void SendMessage(string message, TimeSpan time)
        {
            Message m = new Message(Entity.Node.ID, Entity.Node.ID, message, Globals.Device.Timer.Time + ((uint)time.TotalMilliseconds));
            MessageDispatcher.AddMessageToQueue(m);
        }

        public void SendMessageToFoe(string message, TimeSpan time)
        {
            Message m = new Message(Entity.Node.ID, Enemy.Node.ID, message, Globals.Device.Timer.Time + ((uint)time.TotalMilliseconds));
            MessageDispatcher.AddMessageToQueue(m);
        }

        public string FetchMessage()
        {
            var msg = MessageDispatcher.TryFetchMessage(Entity.Node.ID, Globals.Device.Timer.Time);
            return msg != null ? msg.MessageString : String.Empty;
        }

        #endregion Messaging

        #region Entity information

        public int GetHealth()
        {
            return Entity.Health;
        }

        public int GetAmmo()
        {
            return Entity.Ammo;
        }

        public Point GetPosition()
        {
            return new Point((int)Entity.Node.Position.X, (int)Entity.Node.Position.Z);
        }

        public int GetScoreDiff()
        {
            return Entity.Score - Enemy.Score;
        }

        public double GetHitAccuracy()
        {
            if (Entity.TotalShots > 0)
            {
                double hitsTaken = Enemy.HitsTaken;
                double totShots = Entity.TotalShots;
                return hitsTaken / totShots;
            }
            else
            {
                return 0;
            }
        }

        public int GetHitsTaken()
        {
            return Entity.HitsTaken;
        }

        public int GetTotalShots()
        {
            return Entity.TotalShots;
        }

        public int GetLookAngle()
        {
            return (int)Entity.TargetRotation.Y;
        }

        public Point GetPointInFrontOfPlayer()
        {
            return GetPointInFrontOfEntity(Entity);
        }

        #endregion Entity information

        #region Enemy information

        public bool GetEnemySighted()
        {
            return CollisionManager.EntityVisible(Entity, Enemy);
        }

        public Point GetEnemyPosition()
        {
            return new Point((int)Enemy.Node.Position.X, (int)Enemy.Node.Position.Z);
        }

        public int GetEnemyLookAngle()
        {
            return (int)Enemy.TargetRotation.Y;
        }

        public double GetDistanceFromEnemy()
        {
            return Entity.Node.Position.DistanceFrom(Enemy.Node.Position);
        }

        public Point GetPointInFrontOfEnemy()
        {
            return GetPointInFrontOfEntity(Enemy);
        }

        #endregion Enemy information

        #region Game information

        public TimeSpan GetGameTime()
        {
            return new TimeSpan(Globals.Device.Timer.Time);
        }

        public Point GetPositionFromMapPoint(Point point)
        {
            var position = GetNodes.GetPositionFromMapPoint(point);
            return new Point((int)position.X, (int)position.Z);
        }

        public bool PointVisible(Point p)
        {
            Line3D line = new Line3D(Entity.BoundingBox.Center, new Vector3D(p.X, 30, p.Y));
            return CollisionManager.CanMoveBetween(line.Start, line.End);
        }

        public int GetNumberOfVisibleBazookas()
        {
            return SceneNodeManager.VisibleBazookaEntities.Count;
        }

        public int GetNumberofVisibleMedkits()
        {
            return SceneNodeManager.VisibleMedkitEntities.Count;
        }

        public Point[] GetAllMedkitLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in SceneNodeManager.MedkitEntities)
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetVisibleMedkitLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in SceneNodeManager.VisibleMedkitEntities)
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetAllBazookaLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in SceneNodeManager.BazookaEntities)
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetVisibleBazookaLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in SceneNodeManager.VisibleBazookaEntities)
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetAllWallLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in SceneNodeManager.WallSceneNodes)
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetEnemyFiredRocketLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in CombatManager.ProjectileList.Where(p => p.Owner == Enemy && p is RocketEntity))
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        public Point[] GetEnemyFiredBulletLocations()
        {
            List<Point> pointList = new List<Point>();
            foreach (var entity in CombatManager.ProjectileList.Where(p => p.Owner == Enemy && p is BulletEntity))
            {
                pointList.Add(new Point((int)entity.Node.Position.X, (int)entity.Node.Position.Z));
            }
            return pointList.ToArray();
        }

        #endregion Game information

        #region Display Text

        public void SayText(string message)
        {
            Globals.Scene.AddToDeletionQueue(Entity.FloatingText);
            Entity.FloatingText = SceneNodeManager.CreateBillboardText(Entity.Node, message);
        }

        public void SayText(string message, System.Drawing.Color colour)
        {
            Globals.Scene.AddToDeletionQueue(Entity.FloatingText);
            Entity.FloatingText = SceneNodeManager.CreateBillboardText(Entity.Node, message, colour);
        }

        #endregion Display Text

        #region Combat

        public void ShootRocket(Point pos)
        {
            CombatManager.FireRocket(Entity, pos);
        }

        public void ShootBullet(Point pos)
        {
            CombatManager.FireBullet(Entity, pos);
        }

        public void HurtSelf(int damage)
        {
            Entity.Health -= damage;
        }

        #endregion Combat

        #region Sound

        public void PlaySound(string filename)
        {
            if (SoundManager.PlaySound)
            {
                SoundManager.PlayCustom(filename);
            }
        }

        #endregion Sound

        #region Keyboard

        public bool IsKeyDown(params System.Windows.Forms.Keys[] keys)
        {
            return KeyboardHelper.KeyIsDown(keys);
        }

        #endregion Keyboard

        #region Helpers

        private Point GetPointInFrontOfEntity(CombatEntity entity)
        {
            var rotation = entity.TargetRotation + new Vector3D(0, 90, 0);

            var x = (float)(100 * Math.Cos(rotation.Y * NewMath.DEGTORAD));
            var z = (float)(100 * Math.Sin(rotation.Y * NewMath.DEGTORAD));

            var pointVector = entity.Node.Position + new Vector3D(x, 0, -z);

            return new Point((int)pointVector.X, (int)pointVector.Z);
        }

        #endregion Helpers
    }
}