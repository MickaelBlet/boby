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
            Aion_Process.Game.Pid = 0;
            Aion_Process.Game.Base = 0;
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
            List<Game_View> itemsource = SearchGame();

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

        private static List<Game_View> SearchGame()
        {
            List<Game_View> db = new List<Game_View>();
            Process[] pid = Process.GetProcessesByName("aion.bin");

            if (pid.Length == 0)
                return null;

            for (int i = 0; i < pid.Length; i++)
            {
                string Pid;
                string Name;
                string Lvl = "";
                int Class = 0;
                ImageSource class_image = null;

                Pid = pid[i].Id.ToString();
                IntPtr hanble = Memory.OpenProcess(pid[i].Id);
                SplMemory.SetHanble(hanble);
                int Aion_DLL_Game = Aion_Process.Game.GetModuleBase("Game.dll", pid[i].Id);
                if (Aion_DLL_Game != 0 && Offset.Loading(Aion_DLL_Game, Aion_Process.Game.GetModuleVersion("Game.dll", pid[i].Id), hanble))
                {
                    string name = SplMemory.ReadWchar(Aion_DLL_Game + Offset.Player.Name, 30);
                    byte lvl = SplMemory.ReadByte(Aion_DLL_Game + Offset.Player.Lvl);
                    Class = SplMemory.ReadByte(Aion_DLL_Game + Offset.Player.Class);
                    if (name != string.Empty)
                    {
                        Name = name;
                        Lvl = "Lvl: " + lvl.ToString();
                        class_image = (ImageSource)Application.Current.FindResource("Class_Icon." + Class);
                    }
                    else
                    {
                        Pid = "0";
                        Name = "(Login / Select.Character)";
                        Lvl = "Pid: " + pid[i].Id.ToString();
                    }
                }
                else
                {
                    Pid = "0";
                    Name = "(game version not found)";
                    Lvl = "Pid: " + pid[i].Id.ToString();
                }
                db.Add(new Game_View { Pid = Pid, Name = Name, Lvl = Lvl, Class = "", graph_class = class_image, Race = "" });
                Memory.CloseHandle(hanble);
            }

            return (db);
        }
    }

    public class Game_View
    {
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Lvl { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }

        public ImageSource graph_class { get; set; }
        
        public Game_View()
        {
        }

        public void Copy(Entity_View tmp)
        {
            Name = tmp.Name;
            Lvl = tmp.Lvl;
            Class = tmp.Class;
            Race = tmp.Race;
            graph_class = tmp.graph_class;
        }
    }
}
