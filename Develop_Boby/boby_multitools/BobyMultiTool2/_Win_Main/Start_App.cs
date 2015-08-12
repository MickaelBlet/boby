using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

using NS_Aion_Game;
using _Threads;
using MemoryLib;

namespace BobyMultitools
{
    public partial class Win_Main
    {
        private void Start_App()
        {
            if (lb_Game.SelectedItem != null)
            {
                this.cb_hide.IsChecked = true;

                string content = lb_Game.SelectedItem.ToString();
                string[] words = content.Split(':');
                int id = Convert.ToInt32(words[0]);

                Open_ID(id);
            }
        }

        private void Open_ID(int id)
        {
            if (id > 0 && Aion_Game.Open(id))
            {
                do
                {
                    Offset.Loading(Aion_Game.Modules.Game);
                    if (!Offset.Base_windows.newbase.ContainsKey("chat_input_dialog"))
                        break;
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                } while (!Offset.Base_windows.newbase.ContainsKey("chat_input_dialog"));

                Send_Chat.IniChat();

                Start_Scan_Entity();

                Start_Radar();
                Start_Entity();
                Start_Cheat();
                Start_Buff();
                Start_Quick();
                
				this.WindowState = System.Windows.WindowState.Minimized;
            }
            else
                Refresh_lb_Game();
        }

        public void Start_Scan_Entity()
        {
            if (in_Thread_Entity == null)
            {
                Thread T_Scan_Entity = new Thread(Scan_Entity_);
                T_Scan_Entity.SetApartmentState(ApartmentState.STA);
                T_Scan_Entity.Start();
            }
        }

        public void Scan_Entity_()
        {
            Thread_Entity in_Thread_Entity = new Thread_Entity(in_Win_Main);
            System.Windows.Threading.Dispatcher.Run();
        }

        public void Start_Radar()
        {
            if (in_Win_Radar == null)
            {
                Thread T_Radar = new Thread(Radar_);
                T_Radar.SetApartmentState(ApartmentState.STA);
                T_Radar.Start();
            }
            else
            {
                in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Radar.Topmost = false;
                    in_Win_Radar.Topmost = true;
                }));
            }
        }

        public void Radar_()
        {
            try
            {
                Win_Radar in_Win_Radar = new Win_Radar(in_Win_Main);
                if (in_Setting.in_Radar.Show.Get_Value())
                    in_Win_Radar.Show();
                in_Win_Radar.Closed += (sender2, e2) => in_Win_Radar.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }

        public void Start_Entity()
        {
            if (in_Win_Entity == null)
            {
                Thread T_Entity = new Thread(Entity_);
                T_Entity.SetApartmentState(ApartmentState.STA);
                T_Entity.Start();
            }
            else
            {
                in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Entity.Topmost = false;
                    in_Win_Entity.Topmost = true;
                }));
            }
        }

        public void Entity_()
        {
            try
            {
                Win_Entity in_Win_Entity = new Win_Entity(in_Win_Main);
                if (in_Setting.in_Entity.Show.Get_Value())
                    in_Win_Entity.Show();
                in_Win_Entity.Closed += (sender2, e2) => in_Win_Entity.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }

        public void Start_Cheat()
        {
            if (in_Win_Cheat == null)
            {
                Thread T_Cheat = new Thread(Cheat_);
                T_Cheat.SetApartmentState(ApartmentState.STA);
                T_Cheat.Start();
            }
            else
            {
                in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Cheat.Topmost = false;
                    in_Win_Cheat.Topmost = true;
                }));
            }
        }

        public void Cheat_()
        {
            try
            {
                Win_Cheat in_Win_Cheat = new Win_Cheat(in_Win_Main);
                if (in_Setting.in_Cheat.Show.Get_Value())
                    in_Win_Cheat.Show();
                in_Win_Cheat.Closed += (sender2, e2) => in_Win_Cheat.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }

        public void Start_Buff()
        {
            if (in_Win_Buff == null)
            {
                Thread T_Buff = new Thread(Buff_);
                T_Buff.SetApartmentState(ApartmentState.STA);
                T_Buff.Start();
            }
            else
            {
                in_Win_Buff.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Buff.Topmost = false;
                    in_Win_Buff.Topmost = true;
                }));
            }
        }

        public void Buff_()
        {
            try
            {
                Win_Buff in_Win_Buff = new Win_Buff(in_Win_Main);
                if (in_Setting.in_Buff.Show.Get_Value())
                    in_Win_Buff.Show();
                in_Win_Buff.Closed += (sender2, e2) => in_Win_Buff.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }

        public void Start_Quick()
        {
            if (in_Win_Quick == null)
            {
                Thread T_Quick = new Thread(Quick_);
                T_Quick.SetApartmentState(ApartmentState.STA);
                T_Quick.Start();
            }
            else
            {
                in_Win_Quick.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Quick.Topmost = false;
                    in_Win_Quick.Topmost = true;
                }));
            }
        }

        public void Quick_()
        {
            try
            {
                Win_Quick in_Win_Quick = new Win_Quick(in_Win_Main);
                if (in_Setting.in_Quick.Show.Get_Value())
                    in_Win_Quick.Show();
                in_Win_Quick.Closed += (sender2, e2) => in_Win_Quick.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }
    }
}
