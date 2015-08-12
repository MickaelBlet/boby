using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

using MemoryLib;

using System.Runtime.InteropServices; // DllImport

namespace NS_Aion_Game
{
    public class Aion_Game
    {
        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        #region Structs

        public struct ModuleBase
        {
            public int Game;
            public string Version_Game;

            public ModuleBase(int pid)
            {
                this.Game = 0;
                this.Version_Game = "";

                System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);
                foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
                {
                    if (Module.ModuleName == "Game.dll")
                    {
                        this.Game = Module.BaseAddress.ToInt32();
                        this.Version_Game = Module.FileVersionInfo.FileVersion;
                    }
                }
            }
        }

        #endregion

        public static IntPtr handle;
        public static ModuleBase Modules;
        public static IntPtr whandle;
        public static int pid;
        public static List<IntPtr> listhandle;

        public static bool Open(int Pid)
        {
            pid = Pid;
            Thread tmp_thread_update_handle = new Thread(Thread_Update_handle);
            tmp_thread_update_handle.Start();
            Thread tmp_thread_update_Whandle = new Thread(Thread_Update_Whandle);
            tmp_thread_update_Whandle.Start();
            tmp_thread_update_handle.Join();
            if (handle != IntPtr.Zero)
            {
                Thread tmp_thread_update_Modules = new Thread(Thread_Update_Modules);
                tmp_thread_update_Modules.Start();
                tmp_thread_update_Modules.Join();
            }
            return (Modules.Game != 0);
        }

        private static void Thread_Update_handle()
        {
            handle = Memory.OpenProcess(pid);
        }

        private static void Thread_Update_Whandle()
        {
            whandle = WHandleFromPid(pid);
        }

        private static void Thread_Update_Modules()
        {
            Modules = new ModuleBase(pid);
            SplMemory.SetHanble(handle);
        }

        private static IntPtr WHandleFromPid(int pid)
        {
            IntPtr wHandle = IntPtr.Zero;
            Process[] procs = Process.GetProcessesByName("aion.bin");
            listhandle = new List<IntPtr>();

            for (int i = procs.Length - 1; i >= 0; i--)
            {
                if (procs[i].Id == pid)
                {
                    wHandle = procs[i].MainWindowHandle;
                    if (wHandle == IntPtr.Zero)
                    {
                        foreach (ProcessThread pt in procs[i].Threads)
                        {
                            EnumThreadWindows((uint)pt.Id, new EnumThreadDelegate(EnumThreadCallback), IntPtr.Zero);
                            wHandle = listhandle.First();
                            break;
                        }
                    }
                    return wHandle;
                }
            }
            return wHandle;
        }

        static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
        {
            listhandle.Add(hWnd);
            return true;
        }
    }
}
