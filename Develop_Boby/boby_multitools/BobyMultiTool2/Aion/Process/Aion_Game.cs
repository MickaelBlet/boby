using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices; // DllImport

using MemoryLib;

namespace Aion_Process
{
    public class Game
    {
        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        public static int Pid;
        public static IntPtr Phandle;
        public static IntPtr Whandle;
        public static int Base;
        public static string Version;

        public static IntPtr tmpWhandle;

        public static bool Open(int paramPid)
        {
            Pid = paramPid;
            Thread tmp_thread_update_handle = new Thread(Thread_Update_handle);
            tmp_thread_update_handle.Start();
            Thread tmp_thread_update_Whandle = new Thread(Thread_Update_Whandle);
            tmp_thread_update_Whandle.Start();
            tmp_thread_update_handle.Join();
            if (Phandle != IntPtr.Zero)
            {
                Thread tmp_thread_update_Modules = new Thread(Thread_Update_Modules);
                tmp_thread_update_Modules.Start();
                tmp_thread_update_Modules.Join();
            }
            if (Base != 0)
                MemoryLib.Offset.Loading(Base, Version, Phandle);
            return (Base != 0);
        }

        private static void Thread_Update_handle()
        {
            Phandle = Memory.OpenProcess(Pid);
        }

        private static void Thread_Update_Whandle()
        {
            Whandle = WHandleFromPid(Pid);
        }

        private static void Thread_Update_Modules()
        {
            Base = GetModuleBase("Game.dll", Pid);
            Version = GetModuleVersion("Game.dll", Pid);
            SplMemory.SetHanble(Phandle);
        }

        private static IntPtr WHandleFromPid(int pid)
        {
            IntPtr wHandle = IntPtr.Zero;
            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("aion.bin");

            for (int i = procs.Length - 1; i >= 0; i--)
            {
                if (procs[i].Id == pid)
                {
                    wHandle = procs[i].MainWindowHandle;
                    if (wHandle == IntPtr.Zero)
                    {
                        System.Diagnostics.ProcessThread pt = procs[i].Threads[0];
                        EnumThreadWindows((uint)pt.Id, new EnumThreadDelegate(EnumThreadCallback), IntPtr.Zero);
                        wHandle = tmpWhandle;
                    }
                    return wHandle;
                }
            }
            return wHandle;
        }

        static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
        {
            tmpWhandle = hWnd;
            return true;
        }


        public static int GetModuleBase(string modulename, int pid)
        {
            System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

            foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
                if (modulename == Module.ModuleName)
                    return Module.BaseAddress.ToInt32();
            return 0;
        }
        public static string GetModuleVersion(string modulename, int pid)
        {
            System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

            foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
                if (modulename == Module.ModuleName)
                    return Module.FileVersionInfo.FileVersion;
            return string.Empty;
        }
    }
}
