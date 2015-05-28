using System;

namespace DumbBotsNET.Api
{
    public interface ICommand
    {
        void Think(IPlayerApi api);
    }
}