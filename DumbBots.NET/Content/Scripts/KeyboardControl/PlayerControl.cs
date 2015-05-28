using System;
using System.Drawing;
using DumbBotsNET.Api;
using System.Windows.Forms;

public class AIScript : ICommand
{
    public void Think(IPlayerApi api)
    {
		if(api.IsKeyDown(Keys.Up, Keys.Left))
		{
			api.MoveUpLeft();
		}
		else if(api.IsKeyDown(Keys.Up, Keys.Right))
		{
			api.MoveUpRight();
		}
		else if(api.IsKeyDown(Keys.Down, Keys.Left))
		{
			api.MoveDownLeft();
		}
		else if(api.IsKeyDown(Keys.Down, Keys.Right))
		{
			api.MoveDownRight();
		}
		else if(api.IsKeyDown(Keys.Up))
		{
			api.MoveUp();
		}
		else if(api.IsKeyDown(Keys.Down))
		{
			api.MoveDown();
		}
		else if(api.IsKeyDown(Keys.Right))
		{
			api.MoveRight();
		}
		else if(api.IsKeyDown(Keys.Left))
		{
			api.MoveLeft();
		}

		if(api.IsKeyDown(Keys.S))
		{
			api.ShootBullet(api.GetPointInFrontOfPlayer());
		}
    }
}