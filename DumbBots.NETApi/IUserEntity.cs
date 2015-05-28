using System;
using System.Drawing;

namespace DumbBotsNET.Api
{
    public interface IUserEntity
    {
        bool Shootable { get; set; }

        bool IsObstacle { get; set; }

        double SpeedModifier { get; set; }

        double DamageModifier { get; set; }

        int Health { get; set; }

        int Ammo { get; set; }

        void SetScale(float scale);

        void SetTexture(string fileName);

        void MoveToPoint(Point position);

        void SetAnimationSpeed(int speed);

        void PlayAnimation(int start, int end, bool loop);

        bool CanSeePlayer(int player);

        void FireBullet(Point pos);

        void FireRocket(Point pos);

        bool TouchingPlayer(int player);

        void SetRotationY(int value);

        void SayText(string message);

        void SayText(string message, Color colour);
    }
}