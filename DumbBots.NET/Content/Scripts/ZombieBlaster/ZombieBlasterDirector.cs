using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using DumbBotsNET.Api;

public class AIScript : IDirectorCommand
{
    private int _player1Score = -1;
    private int _player2Score = -1;

    private List<IUserEntity> _player1Zombies;
    private List<IUserEntity> _player2Zombies;

    public void AfterMapLoad(IDirectorApi api)
    {
        api.SetDefaultAmmo(0);
        api.SetBulletReloadTime(TimeSpan.FromSeconds(2));
    }

    public void Think(IDirectorApi api)
    {
        AddPlayer1Zombies(api);
        AddPlayer2Zombies(api);

        MovePlayer1Zombies(api);
        MovePlayer2Zombies(api);

        CheckZombieCollisions(api);

        api.SetPlayerScore(1, _player1Score);
        api.SetPlayerScore(2, _player2Score);
    }

    private void AddPlayer1Zombies(IDirectorApi api)
    {
        if (_player1Zombies == null)
        {
            _player1Zombies = new List<IUserEntity>();
        }

        foreach (var zombie in _player1Zombies)
        {
            if (zombie.Health == 0)
            {
                _player1Zombies.Remove(zombie);
                api.PlaySound(@"Sounds\\death1.wav");
            }
        }

        if (_player1Zombies.Count == 0)
        {
            _player1Score++;

            _player1Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 1)));
            _player1Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 3)));
            _player1Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 5)));
        }
    }

    private void AddPlayer2Zombies(IDirectorApi api)
    {
        if (_player2Zombies == null)
        {
            _player2Zombies = new List<IUserEntity>();
        }

        foreach (var zombie in _player2Zombies)
        {
            if (zombie.Health == 0)
            {
                _player2Zombies.Remove(zombie);
                api.PlaySound(@"Sounds\\death1.wav");
            }
        }

        if (_player2Zombies.Count == 0)
        {
            _player2Score++;

            _player2Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 9)));
            _player2Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 11)));
            _player2Zombies.Add(CreateZombie(api, api.GetPositionFromMapPoint(1, 13)));
        }
    }

    private void MovePlayer1Zombies(IDirectorApi api)
    {
        foreach (var zombie in _player1Zombies)
        {
            zombie.MoveToPoint(api.GetPlayerPosition(1));
        }
    }

    private void MovePlayer2Zombies(IDirectorApi api)
    {
        foreach (var zombie in _player2Zombies)
        {
            zombie.MoveToPoint(api.GetPlayerPosition(2));
        }
    }

    private void CheckZombieCollisions(IDirectorApi api)
    {
        foreach (var zombie in _player1Zombies)
        {
            if (zombie.TouchingPlayer(1))
            {
                zombie.Health = 0;
                api.RemoveCustomEntity(zombie);
                api.SetPlayerPosition(1, api.GetPositionFromMapPoint(13, 3));
                api.PlaySound(@"Sounds\\death1.wav");
                _player1Score--;
            }
        }

        foreach (var zombie in _player2Zombies)
        {
            if (zombie.TouchingPlayer(2))
            {
                zombie.Health = 0;
                api.RemoveCustomEntity(zombie);
                api.SetPlayerPosition(2, api.GetPositionFromMapPoint(13, 10));
                api.PlaySound(@"Sounds\\death1.wav");
                _player2Score--;
            }
        }
    }

    private IUserEntity CreateZombie(IDirectorApi api, Point pos)
    {
        var userEntity = api.CreateCustomEntity(@"Models\Sphere.3ds", @"Scripts\ZombieBlaster\ZombieSphere.png", pos);
        userEntity.SetRotationY(180);
        userEntity.SpeedModifier = 20;
        userEntity.Shootable = true;
        userEntity.IsObstacle = true;
        return userEntity;
    }
}