using System;
using System.Drawing;
using DumbBotsNET.Api;

public class AIScript : IDirectorCommand
{
    public void AfterMapLoad(IDirectorApi api)
    {
        api.SetRocketDamage(0);
        api.SetBulletDamage(0);
        api.SendMessageToPlayers("start", TimeSpan.FromSeconds(0));
        api.SendMessageToPlayers("end", TimeSpan.FromSeconds(10));
    }

    public void Think(IDirectorApi api)
    {
    }
}