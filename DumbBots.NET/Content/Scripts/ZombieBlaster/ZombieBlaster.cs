using System;
using System.Drawing;
using System.IO;
using DumbBotsNET.Api;

public class AIScript : ICommand
{
    public void Think(IPlayerApi api)
    {
        bool seenZombie = false;

        Point[] points = api.GetCustomEntityPositions();
        foreach (Point point in points)
        {
            if (api.CustomEntityVisible(point))
            {
                seenZombie = true;
                api.ShootBullet(point);
                break;
            }
        }

        if (!seenZombie)
        {
            api.MoveToRandomLocation();
        }
    }
}