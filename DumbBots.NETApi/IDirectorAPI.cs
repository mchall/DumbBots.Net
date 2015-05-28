using System;
using System.Drawing;

namespace DumbBotsNET.Api
{
    public interface IDirectorApi
    {
        #region Extensibility

        IUserEntity CreateCustomEntity(string modelFile, string textureFile, Point position);

        bool RemoveCustomEntity(IUserEntity userEntity);

        #endregion Extensibility

        #region Messaging

        void SendMessageToPlayers(string message, TimeSpan time);

        void SendMessageToSelf(string message, TimeSpan time);

        void SendMessageToPlayer(int player, string message, TimeSpan time);

        string FetchMessage();

        #endregion Messaging

        #region Rules

        void SetBazookaRespawnTime(TimeSpan value);

        void SetBulletDamage(int value);

        void SetBulletReloadTime(TimeSpan value);

        void SetBulletSpeedModifier(int value);

        void SetDefaultAmmo(int value);

        void SetMaxAmmo(int value);

        void SetMaxHealth(int value);

        void SetMedkitHealthBoost(int value);

        void SetMedkitRespawnTime(TimeSpan value);

        void SetRocketDamage(int value);

        void SetRocketReloadTime(TimeSpan value);

        void SetRocketSpeedModifier(int value);

        void SetPlayerSpeed(int player, double value);

        void SetPlayerDamageModifier(int player, double modifier);

        void SetGameSpeed(float value);

        #endregion Rules

        #region Player Interaction

        Point GetPlayerPosition(int player);

        void SetPlayerPosition(int player, Point position);

        void HealPlayer(int player, int health);

        void GivePlayerAmmo(int player, int ammo);

        int GetPlayerScore(int player);

        void SetPlayerScore(int player, int score);

        int GetPlayerHealth(int player);

        int GetPlayerAmmo(int player);

        int GetPlayerCustomEntitiesDestroyed(int player);

        #endregion Player Interaction

        #region Game Information

        TimeSpan GetGameTime();

        Point GetPositionFromMapPoint(int x, int y);

        #endregion Game Information

        #region Sound

        void PlaySound(string filename);

        #endregion Sound

        #region Text

        void SayText(string message, System.Drawing.Color color, Point position);

        #endregion Text

        #region Keyboard

        bool IsKeyDown(params System.Windows.Forms.Keys[] keys);

        #endregion Keyboard
    }
}