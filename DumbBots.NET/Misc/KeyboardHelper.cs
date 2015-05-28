using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Misc
{
    public static class KeyboardHelper
    {
        sealed class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern short GetKeyState(int nVirtKey);

            public const int KF_UP = 0x8000;

            public static bool KeyIsDown(int nVirtKey)
            {
                return ((GetKeyState(nVirtKey) & KF_UP) == KF_UP);
            }
        }

        public static bool KeyIsDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (!NativeMethods.KeyIsDown((int)key))
                    return false;
            }
            return true;
        }
    }
}