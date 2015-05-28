using System;

namespace DumbBotsNET.Api
{
    public interface IDirectorCommand
    {
        void AfterMapLoad(IDirectorApi api);
        void Think(IDirectorApi api);
    }
}