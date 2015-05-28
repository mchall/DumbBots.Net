using System;
using System.Drawing;
using System.Windows.Forms;

namespace DumbBotsNET.Api
{
    public interface IPlayerApi
    {
        #region Extensibility

        Point[] GetCustomEntityPositions();

        int GetCustomEntitiesDestroyed();

        bool CustomEntityVisible(Point point);

        #endregion Extensibility

        #region Entity movement

        void Stop();

        bool MoveToPoint(Point p);

        bool MoveUp();

        bool MoveUpLeft();

        bool MoveUpRight();

        bool MoveDown();

        bool MoveDownLeft();

        bool MoveDownRight();

        bool MoveRight();

        bool MoveLeft();

        void MoveToRandomLocation();

        #endregion Entity movement

        #region Get Items

        void GetRandomBazooka();

        void GetNearestBazooka();

        void GetRandomMedkit();

        void GetNearestMedkit();

        #endregion Get Items

        #region Messaging

        void SendMessage(string message, TimeSpan time);

        void SendMessageToFoe(string message, TimeSpan time);

        string FetchMessage();

        #endregion Messaging

        #region Entity information

        int GetHealth();

        int GetAmmo();

        Point GetPosition();

        int GetScoreDiff();

        double GetHitAccuracy();

        int GetHitsTaken();

        int GetTotalShots();

        int GetLookAngle();

        Point GetPointInFrontOfPlayer();

        #endregion Entity information

        #region Enemy information

        bool GetEnemySighted();

        Point GetEnemyPosition();

        int GetEnemyLookAngle();

        double GetDistanceFromEnemy();

        Point GetPointInFrontOfEnemy();

        #endregion Enemy information

        #region Game information

        TimeSpan GetGameTime();

        Point GetPositionFromMapPoint(Point point);

        bool PointVisible(Point p);

        int GetNumberOfVisibleBazookas();

        int GetNumberofVisibleMedkits();

        Point[] GetAllMedkitLocations();

        Point[] GetVisibleMedkitLocations();

        Point[] GetAllBazookaLocations();

        Point[] GetVisibleBazookaLocations();

        Point[] GetAllWallLocations();

        Point[] GetEnemyFiredRocketLocations();

        Point[] GetEnemyFiredBulletLocations();

        #endregion Game information

        #region Display Text

        void SayText(string message);

        void SayText(string message, Color colour);

        #endregion Display Text

        #region Combat

        void ShootRocket(Point pos);

        void ShootBullet(Point pos);

        void HurtSelf(int damage);

        #endregion Combat

        #region Sound

        void PlaySound(string filename);

        #endregion Sound

        #region Keyboard

        bool IsKeyDown(params Keys[] keys);

        #endregion Keyboard
    }
}