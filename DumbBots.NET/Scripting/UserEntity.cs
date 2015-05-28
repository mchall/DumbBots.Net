using System;
using System.Drawing;
using Core;
using DumbBotsNET.Api;
using Entities;
using Game;
using Graphs;
using IrrlichtNETCP;

namespace Scripting
{
    /// <summary>
    /// Represents a user entity
    /// </summary>
    public class UserEntity : IUserEntity
    {
        internal CustomEntity CustomEntity { get; private set; }

        public bool Shootable
        {
            get { return CustomEntity.Shootable; }
            set { CustomEntity.Shootable = value; }
        }

        public bool IsObstacle
        {
            get { return CustomEntity.IsObstacle; }
            set { CustomEntity.IsObstacle = value; }
        }

        public double SpeedModifier
        {
            get { return CustomEntity.SpeedModifier; }
            set { CustomEntity.SpeedModifier = value; }
        }

        public double DamageModifier
        {
            get { return CustomEntity.DamageModifier; }
            set { CustomEntity.DamageModifier = value; }
        }

        public int Health
        {
            get { return CustomEntity.Health; }
            set { CustomEntity.Health = value; }
        }

        public int Ammo
        {
            get { return CustomEntity.Ammo; }
            set { CustomEntity.Ammo = value; }
        }

        internal UserEntity(CustomEntity customEntity)
        {
            CustomEntity = customEntity;
        }

        public void SetScale(float scale)
        {
            CustomEntity.Node.Scale = new Vector3D(scale, scale, scale);
        }

        public void SetTexture(string fileName)
        {
            CustomEntity.Node.SetMaterialTexture(0, Globals.Driver.GetTexture(fileName));
        }

        public void MoveToPoint(Point position)
        {
            Vector3D pos = new Vector3D(position.X, 30, position.Y);
            CustomEntity.Route = RouteManager.Search(CustomEntity, GetNodes.GetNearestNodeToPosition(CustomEntity.Node.Position), GetNodes.GetNearestNodeToPosition(pos));
        }

        public void SetAnimationSpeed(int speed)
        {
            (CustomEntity.Node as AnimatedMeshSceneNode).AnimationSpeed = speed;
        }

        public void PlayAnimation(int start, int end, bool loop)
        {
            (CustomEntity.Node as AnimatedMeshSceneNode).SetFrameLoop(start, end);
            (CustomEntity.Node as AnimatedMeshSceneNode).LoopMode = loop;
        }

        public bool CanSeePlayer(int player)
        {
            if (player == 1)
                return CollisionManager.EntityVisible(SceneNodeManager.Player1, CustomEntity);
            else if (player == 2)
                return CollisionManager.EntityVisible(SceneNodeManager.Player2, CustomEntity);
            else return false;
        }

        public void FireBullet(Point pos)
        {
            CombatManager.FireBullet(CustomEntity, pos);
        }

        public void FireRocket(Point pos)
        {
            CombatManager.FireRocket(CustomEntity, pos);
        }

        public bool TouchingPlayer(int player)
        {
            if (player == 1)
                return CustomEntity.BoundingBox.IntersectsWithBox(SceneNodeManager.Player1.BoundingBox);
            else if (player == 2)
                return CustomEntity.BoundingBox.IntersectsWithBox(SceneNodeManager.Player2.BoundingBox);
            else return false;
        }

        public void SetRotationY(int value)
        {
            CustomEntity.Rotation = new Vector3D(0, value, 0);
        }

        public void SayText(string message)
        {
            Globals.Scene.AddToDeletionQueue(CustomEntity.FloatingText);
            CustomEntity.FloatingText = SceneNodeManager.CreateBillboardText(CustomEntity.Node, message);
        }

        public void SayText(string message, System.Drawing.Color colour)
        {
            Globals.Scene.AddToDeletionQueue(CustomEntity.FloatingText);
            CustomEntity.FloatingText = SceneNodeManager.CreateBillboardText(CustomEntity.Node, message, colour);
        }
    }
}