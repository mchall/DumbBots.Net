using System;
using System.Drawing;
using DumbBotsNET.Api;

public class AIScript : ICommand
{
    public void Think(IPlayerApi api)
    {
        HandleMessage(api, api.FetchMessage());
    }

    private void HandleMessage(IPlayerApi api, string message)
    {
        if (message == "start" || message == "shoot")
        {
            api.ShootBullet(api.GetEnemyPosition());
            api.SendMessage("shoot", TimeSpan.FromSeconds(0));
        }

        if (message.StartsWith("end"))
        {
            api.SayText("Shot duck " + message.Split(':')[1] + " time(s)", Color.Red);
        }
    }
}