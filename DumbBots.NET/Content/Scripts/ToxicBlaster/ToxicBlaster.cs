using System;
using System.Drawing;
using System.IO;
using DumbBotsNET.Api;

public class AIScript : ICommand
{
    private bool started = false;
    private bool finished = false;

    public void Think(IPlayerApi api)
    {
        if (!started)
        {
            string message = api.FetchMessage();
            if (message == "Start")
            {
                started = true;
            }
        }
        else
        {
            int temp = GetNearestPollution(api);
            if (temp != -1)
            {
                if (api.CustomEntityVisible(api.GetCustomEntityPositions()[temp]))
                {
                    api.ShootBullet(api.GetCustomEntityPositions()[temp]);
                }
                else
                {
                    api.MoveToPoint(api.GetCustomEntityPositions()[temp]);
                }
            }
            else
            {
                if (!finished)
                {
                    api.SayText(String.Format("Pollution cleared: {0}", api.GetCustomEntitiesDestroyed()));
                    finished = true;
                }
            }
        }
    }

    private int GetNearestPollution(IPlayerApi api)
    {
        Point[] points = api.GetCustomEntityPositions();
        if (points.Length == 0)
        {
            return -1;
        }

        Point currentPos = api.GetPosition();
        double shortestDist = 99999;
        int index = -1;
        for (int i = 0; i < points.Length; i++)
        {
            Point p = points[i];
            double X = Math.Abs(currentPos.X - p.X);
            double Y = Math.Abs(currentPos.Y - p.Y);
            double dist = Math.Sqrt((X * X) + (Y * Y));
            if (dist < shortestDist)
            {
                shortestDist = dist;
                index = i;
            }
        }
        return index;
    }
}