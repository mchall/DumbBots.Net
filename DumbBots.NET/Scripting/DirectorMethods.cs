using System;
using System.Collections.Generic;
using System.Drawing;
using Core;
using Entities;
using Game;
using Graphs;
using IrrlichtNETCP;
using Messaging;
using DumbBotsNET.Api;
using Misc;

namespace Scripting
{
    /// <summary>
    /// Static class that allows the script to call simplified methods
    /// </summary>
    public class DirectorMethods : IDirectorApi
    {
        internal TextSceneNode Text { get; set; }

        #region Extensibility

        public IUserEntity CreateCustomEntity(string modelFile, string textureFile, Point position)
        {
            return new UserEntity(SceneNodeManager.CreateCustomEntity(modelFile, textureFile, position));
        }

        public bool RemoveCustomEntity(IUserEntity userEntity)
        {
            var customEntity = userEntity as UserEntity;
            if (SceneNodeManager.CustomEntities.Contains(customEntity.CustomEntity))
            {
                SceneNodeManager.CustomEntities.Remove(customEntity.CustomEntity);
                Globals.Scene.AddToDeletionQueue(customEntity.CustomEntity.Node);
                return true;
            }
            return false;
        }

        #endregion Extensibility

        #region Messaging

        public void SendMessageToPlayers(string message, TimeSpan time)
        {
            SendMessageToPlayer(1, message, time);
            SendMessageToPlayer(2, message, time);
        }

        public void SendMessageToSelf(string message, TimeSpan time)
        {
            Message m = new Message(0, 0, message, Globals.Device.Timer.Time + ((uint)time.TotalMilliseconds));
            MessageDispatcher.AddMessageToQueue(m);
        }

        public void SendMessageToPlayer(int player, string message, TimeSpan time)
        {
            Message m = new Message(0, GetPlayerFromInt(player).Node.ID, message, Globals.Device.Timer.Time + ((uint)time.TotalMilliseconds));
            MessageDispatcher.AddMessageToQueue(m);
        }

        public string FetchMessage()
        {
            var msg = MessageDispatcher.TryFetchMessage(0, Globals.Device.Timer.Time);
            return msg != null ? msg.MessageString : String.Empty;
        }

        #endregion Messaging

        #region Rules

        public void SetBazookaRespawnTime(TimeSpan value)
        {
            RuleManager.BazookaRespawnTime = (int)value.TotalMilliseconds;
        }

        public void SetBulletDamage(int value)
        {
            RuleManager.BulletDamage = value;
        }

        public void SetBulletReloadTime(TimeSpan value)
        {
            RuleManager.BulletReloadTime = (int)value.TotalMilliseconds;
        }

        public void SetBulletSpeedModifier(int value)
        {
            RuleManager.BulletSpeedModifier = value;
        }

        public void SetDefaultAmmo(int value)
        {
            RuleManager.DefaultAmmo = value;
        }

        public void SetMaxAmmo(int value)
        {
            RuleManager.MaxAmmo = value;
        }

        public void SetMaxHealth(int value)
        {
            RuleManager.MaxHealth = value;
        }

        public void SetMedkitHealthBoost(int value)
        {
            RuleManager.MedkitHealthBoost = value;
        }

        public void SetMedkitRespawnTime(TimeSpan value)
        {
            RuleManager.MedkitRespawnTime = (int)value.TotalMilliseconds;
        }

        public void SetRocketDamage(int value)
        {
            RuleManager.RocketDamage = value;
        }

        public void SetRocketReloadTime(TimeSpan value)
        {
            RuleManager.RocketReloadTime = (int)value.TotalMilliseconds;
        }

        public void SetRocketSpeedModifier(int value)
        {
            RuleManager.RocketSpeed = value;
        }

        public void SetPlayerSpeed(int player, double value)
        {
            RuleManager.PlayerSpeedModifier[GetPlayerFromInt(player).Team] = value;
        }

        public void SetPlayerDamageModifier(int player, double modifier)
        {
            RuleManager.PlayerSpeedModifier[GetPlayerFromInt(player).Team] = modifier;
        }

        public void SetGameSpeed(float value)
        {
            RuleManager.GameSpeed = value;
            Globals.GameSpeed = RuleManager.GameSpeed;
        }

        #endregion Rules

        #region Player Interaction

        public Point GetPlayerPosition(int player)
        {
            return new Point((int)GetPlayerFromInt(player).Node.Position.X, (int)GetPlayerFromInt(player).Node.Position.Z);
        }

        public void SetPlayerPosition(int player, Point position)
        {
            SetPlayerPosition(GetPlayerFromInt(player), position);
        }

        public void HealPlayer(int player, int health)
        {
            GetPlayerFromInt(player).Health += health;
        }

        public void GivePlayerAmmo(int player, int ammo)
        {
            GetPlayerFromInt(player).Ammo += ammo;
        }

        public int GetPlayerScore(int player)
        {
            return GetPlayerFromInt(player).Score;
        }

        public void SetPlayerScore(int player, int score)
        {
            GetPlayerFromInt(player).Score = score;
        }

        public int GetPlayerHealth(int player)
        {
            return GetPlayerFromInt(player).Health;
        }

        public int GetPlayerAmmo(int player)
        {
            return GetPlayerFromInt(player).Ammo;
        }

        public int GetPlayerCustomEntitiesDestroyed(int player)
        {
            return GetPlayerFromInt(player).CustomEntitiesDestroyed;
        }

        #endregion Player Interaction

        #region Game Information

        public TimeSpan GetGameTime()
        {
            return new TimeSpan(Globals.Device.Timer.Time);
        }

        public Point GetPositionFromMapPoint(int x, int y)
        {
            var position = GetNodes.GetPositionFromMapPoint(new Point(x, y));
            return new Point((int)position.X, (int)position.Z);
        }

        #endregion Game Information

        #region Sound

        public void PlaySound(string filename)
        {
            if (SoundManager.PlaySound)
            {
                SoundManager.PlayCustom(filename);
            }
        }

        #endregion Sound

        #region Text

        public void SayText(string message, System.Drawing.Color color, Point position)
        {
            if (Text != null)
                Globals.Scene.AddToDeletionQueue(Text);

            GUIFont font = Globals.Gui.GetFont("Textures\\roboto.png");
            Text = Globals.Scene.AddBillboardTextSceneNode(font, message, null, new Dimension2Df(message.Length * 40, 150), new Vector3D(position.X, 100, position.Y), -1, IrrlichtNETCP.Color.FromBCL(color), IrrlichtNETCP.Color.FromBCL(color));
            Text.SetMaterialFlag(MaterialFlag.Lighting, false);
        }

        #endregion Text

        #region Keyboard

        public bool IsKeyDown(params System.Windows.Forms.Keys[] keys)
        {
            return KeyboardHelper.KeyIsDown(keys);
        }

        #endregion Keyboard

        #region Helpers

        private void SetPlayerPosition(CombatEntity player, Point position)
        {
            var temp = GetNodes.GetNearestNodeToPosition(new Vector3D(position.X, 30, position.Y));
            player.Destination = LevelManager.SparseGraph.GetNode(temp).Position;
            player.Route = new List<int>();
            player.Node.RemoveAnimators();
            Animator anim = Globals.Scene.CreateFlyStraightAnimator(player.Node.Position, player.Destination, 0, false);
            player.Node.AddAnimator(anim);
        }

        private CombatEntity GetPlayerFromInt(int player)
        {
            switch (player)
            {
                case (1): return SceneNodeManager.Player1;
                case (2): return SceneNodeManager.Player2;
                default: throw new NotSupportedException(player.ToString());
            }
        }

        #endregion Helpers
    }
}