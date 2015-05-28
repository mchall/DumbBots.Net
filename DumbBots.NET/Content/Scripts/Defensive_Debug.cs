using System;
using System.Drawing;
using DumbBotsNET.Api;

public class AIScript : ICommand
{
    public void Think(IPlayerApi api)
    {
        if (!api.GetEnemySighted())
        {
            if ((api.GetAmmo() < 3) && (api.GetNumberOfVisibleBazookas() > 0))
            {
                api.SayText("Get_Bazooka");
                api.GetNearestBazooka();
            }
            else
            {
                if ((api.GetHealth() < 100) && (api.GetNumberofVisibleMedkits() > 0))
                {
                    api.SayText("Get_Medkit");
                    api.GetNearestMedkit();
                }
                else
                {
                    api.SayText("Move_Random");
                    api.MoveToRandomLocation();
                }
            }
        }
        else
        {
            if ((api.GetHealth() < 20) && (api.GetNumberofVisibleMedkits() > 0))
            {
                api.SayText("Get_Medkit");
                api.GetNearestMedkit();
            }
            else
            {
                if (api.GetAmmo() == 0)
                {
                    Random rand = new Random();
                    api.ShootBullet(new Point(api.GetEnemyPosition().X + rand.Next(20), api.GetEnemyPosition().Y + rand.Next(20)));
                    api.MoveToRandomLocation();
                    api.SayText("Shoot_Bullet");
                }
                else
                {
                    Random rand = new Random();
                    api.ShootRocket(new Point(api.GetEnemyPosition().X + rand.Next(20), api.GetEnemyPosition().Y + rand.Next(20)));
                    api.MoveToRandomLocation();
                    api.SayText("Shoot_Rocket");
                }
            }
        }
    }
}