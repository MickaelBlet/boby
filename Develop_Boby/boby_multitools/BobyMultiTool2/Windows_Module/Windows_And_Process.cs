using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Windows_And_Process
{
    class Win
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool IsForeground(IntPtr whandle)
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
    }

    class Procs
    {
        public static bool IdOn(int id, string search)
        {
            Process[] pid = Process.GetProcessesByName(search);
            for (int i = 0; i < pid.Length; i++)
            {
                if (id == pid[i].Id)
                    return true;
            }
            return false;
        }
    }

    class App
    {
        public static bool IsActive()
        {
            foreach (Window wnd in Application.Current.Windows)
                if (wnd.IsActive) return true;
            return false;
        }
    }
}
