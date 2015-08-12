using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;

using System.Diagnostics;
using System.Runtime.InteropServices;

using MemoryLib;

namespace BobyMultitools
{
    public partial class Win_Main
    {
        private bool Thread_Refresh_Is_On = false;

        private void Refresh_lb_Game()
        {
            lb_Game.Focus();
            lb_Game.Opacity = 0;
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Style = Style_ShugoLoading;
            img_ShugoLoading.Visibility = Visibility.Visible;
            if (Thread_Refresh_Is_On == false)
            {
                Thread_Refresh_Is_On = true;
                Thread tmp_thread = new Thread(Thread_SearchGame);
                tmp_thread.Start();
            }
        }

        private void Thread_SearchGame()
        {
            string[] itemsource = SearchGame();

            bool is_Update = false;

            in_Win_Main.Dispatcher.Invoke((Action)(() =>
                {
                    if (this.pg_ShugoLoading.Opacity != 0)
                        is_Update = true;
                }
            ));

            if (itemsource != null && is_Update == false)
            {
                in_Win_Main.Dispatcher.Invoke((Action)(() =>
                {
                    int save_last_index = lb_Game.SelectedIndex;
                    
                    lb_Game.ItemsSource = itemsource;
                    if (save_last_index != -1 && save_last_index < lb_Game.Items.Count)
                        lb_Game.SelectedItem = lb_Game.Items[save_last_index];
                    else
                        lb_Game.SelectedItem = lb_Game.Items[0];
                    lb_Game.Opacity = 1;
                    img_ShugoLoading.Visibility = Visibility.Hidden;
                    Thread_Refresh_Is_On = false;
                }));
            }
            else
            {
                Process[] pid = Process.GetProcessesByName("aion.bin");
                while (pid.Length == 0)
                {
                    in_Win_Main.Dispatcher.Invoke((Action)(() =>
                    {
                        img_ShugoLoading.Style = null;
                        img_ShugoLoading.Style = Style_ShugoLoading;
                    }));

                    pid = Process.GetProcessesByName("aion.bin");

                    Thread.Sleep(3000);
                }
                Thread_SearchGame();
            }
        }

        private static string[] SearchGame()
        {
            string[] db;
            Process[] pid = Process.GetProcessesByName("aion.bin");
            db = new string[pid.Length];

            if (pid.Length == 0)
                return null;

            for (int i = 0; i < pid.Length; i++)
            {
                db[i] = pid[i].Id + ": ";
                IntPtr hanble = Memory.OpenProcess(pid[i].Id);
                SplMemory.SetHanble(hanble);
                int Aion_DLL_Game = GetModuleBase("Game.dll", pid[i].Id);
                if (Aion_DLL_Game != 0)
                {
                    Offset.Loading(Aion_DLL_Game);
                    string name = SplMemory.ReadWchar(Aion_DLL_Game + Offset.Player.Name, 30);
                    byte lvl = SplMemory.ReadByte(Aion_DLL_Game + Offset.Player.Lvl);
                    if (name != string.Empty)
                        db[i] += name + " (" + lvl + ")";
                    else
                        db[i] += "(Login / Select.Character)";
                }
                else
                    db[i] += "(game.dll not found)";
                Memory.CloseHandle(hanble);
            }

            return (db);
        }

        private static int GetModuleBase(string modulename, int pid)
        {
            System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

            foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
                if (modulename == Module.ModuleName)
                    return Module.BaseAddress.ToInt32();
            return 0;
        }
    }
}
