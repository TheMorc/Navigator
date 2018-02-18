using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Navigator_CSharp
{
    class taskbar_brainz
    {
        #region importing functions from DLLs
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam,
            ref RECT pvParam, uint fWinIni);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
            int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region enums, structs, etc
        private static RECT m_rcOldDesktopRect;
        private static IntPtr m_hTaskBar;

        public enum Style : int
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public enum SPI : int
        {
            SPI_SETWORKAREA = 0x002F,
            SPI_GETWORKAREA = 0x0030
        }
        #endregion

        #region functions
        public static void kill()
        {
            //"killing" taskbar
            m_hTaskBar = FindWindow("Shell_TrayWnd", null);
            if ((int)m_hTaskBar != 0)
            {
                ShowWindow(m_hTaskBar, (int)Style.Hide);
            }

            //"killing" taskbar window docking
            m_rcOldDesktopRect.left = SystemInformation.WorkingArea.Left;
            m_rcOldDesktopRect.top = SystemInformation.WorkingArea.Top;
            m_rcOldDesktopRect.right = SystemInformation.WorkingArea.Right;
            m_rcOldDesktopRect.bottom = SystemInformation.WorkingArea.Bottom;
            RECT rc;
            rc.left = SystemInformation.VirtualScreen.Left;
            rc.top = SystemInformation.VirtualScreen.Top;
            rc.right = SystemInformation.VirtualScreen.Right;
            rc.bottom = SystemInformation.VirtualScreen.Bottom - 32;
            SystemParametersInfo((int)SPI.SPI_SETWORKAREA, 0, ref rc, 0);
        }

        public static void create()
        {
            //creating new taskbar window docking

            SystemParametersInfo((int)SPI.SPI_SETWORKAREA, 0, ref m_rcOldDesktopRect, 0);

            //creating new bar
            if ((int)m_hTaskBar != 0)
            {
                ShowWindow(m_hTaskBar, (int)Style.Show);
            }
        }
    }
    #endregion
}
