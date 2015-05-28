using System;
using IrrlichtNETCP;

namespace Core
{
    /// <summary>
    /// Static class with often used global variables
    /// </summary>
    internal static class Globals
    {
        internal static IrrlichtDevice Device { get; set; }

        internal static VideoDriver Driver
        {
            get { return Device.VideoDriver; }
        }

        internal static SceneManager Scene
        {
            get { return Device.SceneManager; }
        }

        internal static GUIEnvironment Gui
        {
            get { return Device.GUIEnvironment; }
        }

        internal static float GameSpeed
        {
            get { return Device.Timer.Speed; }
            set
            {
                if (Device != null)
                    Device.Timer.Speed = value;
            }
        }
    }
}