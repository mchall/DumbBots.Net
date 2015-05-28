using System;
using System.Drawing;
using DumbBotsNET.Api;

public class AIScript : ICommand
{
    private bool right = true;

    public void Think(IPlayerApi api)
    {
        HandleMessage(api, api.FetchMessage());

        if (right)
            right = api.MoveRight();
        else
            right = !api.MoveLeft();
    }

    private void HandleMessage(IPlayerApi api, string message)
    {
        if (message == "start" || message == "quack")
        {
            api.SayText("Quack");
            api.SendMessage("stop", TimeSpan.FromSeconds(1));
            api.SendMessage("quack", TimeSpan.FromSeconds(2));
        }

        if (message == "stop")
        {
            api.SayText("");
        }

        if (message == "end")
        {
            api.SendMessageToFoe("end:" + api.GetHitsTaken().ToString(), TimeSpan.FromSeconds(0));
        }
    }
}