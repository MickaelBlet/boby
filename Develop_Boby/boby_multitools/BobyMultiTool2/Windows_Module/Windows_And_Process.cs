using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NS_Windows_And_Process
{
    class Windows_And_Process
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool WindowsIsForeground(IntPtr whandle)
        {
            bool isactive = false;
            if (whandle != IntPtr.Zero)
            {
                IntPtr CWhandle = GetForegroundWindow();
                if (CWhandle == whandle)
                    isactive = true;
            }
            return isactive;
        }

        public static bool ProcessIdOn(int id, string search)
        {
            Process[] pid = Process.GetProcessesByName(search);
            for (int i = 0; i < pid.Length; i++)
            {
                if (id == pid[i].Id)
                    return true;
            }
            return false;
        }

        public static bool IsApplicationActive()
        {
            foreach (Window wnd in Application.Current.Windows)
                if (wnd.IsActive) return true;
            return false;
        }
    }
}
