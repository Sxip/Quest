using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Library.Window
{
    public class WindowGameAttachment
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr handle, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        internal static void Resize(ref Panel gamePanel)
        {
            var gameWindowAttached = GameWindowAttached;
            if (gameWindowAttached)
            {
                MoveWindow(GameWindowHandle, 0, 0, gamePanel.Width, gamePanel.Height, true);
            }
        }

        public static bool GameWindowAttached;

        public static int GwlStyle = -16;

        public static int WsVisible = 268435456;

        public static IntPtr GameWindowHandle;

        public static IntPtr OriginalWindowStyle;

        public static Rect OriginalWindowPos;

        public struct Rect
        {
            public int left;

            public int top;

            public int right;

            public int bottom;
        }
    }
}