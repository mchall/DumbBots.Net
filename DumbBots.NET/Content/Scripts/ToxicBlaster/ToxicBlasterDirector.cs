using System;
using System.Drawing;
using System.IO;
using DumbBotsNET.Api;

public class AIScript : IDirectorCommand
{
    public void AfterMapLoad(IDirectorApi api)
    {
        CreateCustomEntities(api);
        api.SendMessageToPlayers("Start", TimeSpan.FromSeconds(0));
    }

    public void Think(IDirectorApi api)
    {

    }

    private void CreateCustomEntities(IDirectorApi api)
    {
        int[,] map = new int[15, 15];
        int lineCount = 0;
        using (FileStream fs = new FileStream(@"Scripts\ToxicBlaster\Locations.txt", FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        map[lineCount, i] = int.Parse(temp[i].ToString());
                    }
                    lineCount++;
                }
            }
        }

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (map[x, y] == 2) //Custom Scenenode
                {
                    Point p = new Point((x * 100 - 750), (y * 100 - 750));
                    var customEntity = api.CreateCustomEntity(@"Scripts\ToxicBlaster\toxic.md2", @"Scripts\ToxicBlaster\toxic.jpg", p);
                    customEntity.SetScale(1.5f);
                    customEntity.Shootable = true;
                    customEntity.SetAnimationSpeed(0);
                }
            }
        }
    }
}