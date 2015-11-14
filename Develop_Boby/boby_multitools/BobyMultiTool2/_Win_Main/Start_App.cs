using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

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

                Game_View content = (Game_View)lb_Game.SelectedItem;
                int id = int.Parse(content.Pid);

                Open_ID(id);
            }
        }

        private void Open_ID(int id)
        {
            if (id > 0 && Aion_Process.Game.Open(id))
            {
                Offset.SendCharacter();
                Ping.Online_Users_Sequence();
                Start_Scan_Entity();
                Start_Scan_Dialog();
                Aion_Game.Chat.Ini(this);

                Start_Radar();
                Start_Entity();
                Start_Cheat();
                Start_Script();
                //Start_Quick();
            }
            else
                Refresh_lb_Game();
        }

        public void Start_Scan_Entity()
        {
            if (in_Entity_List == null)
            {
                Thread T_Scan_Entity = new Thread(Scan_Entity_);
                T_Scan_Entity.SetApartmentState(ApartmentState.STA);
                T_Scan_Entity.Start();
            }
        }

        public void Scan_Entity_()
        {
            in_Entity_List = new Aion_Game.EntityList(0);
            Entity_To_View.Start();
            Aion_Game.Player.IniTimer();
            System.Windows.Threading.Dispatcher.Run();
        }

        public void Start_Scan_Dialog()
        {
            if (in_Dialog_List == null)
            {
                Thread T_Scan_Dialog = new Thread(Scan_Dialog_);
                T_Scan_Dialog.SetApartmentState(ApartmentState.STA);
                T_Scan_Dialog.Start();
            }
        }

        public void Scan_Dialog_()
        {
            in_Dialog_List = new Aion_Game.DialogList(0);
            new Aion_Game.AbilityList(0);
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
                if (Setting.Boby.Radar.Show)
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
                if (Setting.Boby.Entity.Show)
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
                if (Setting.Boby.Cheat.Show)
                    in_Win_Cheat.Show();
                in_Win_Cheat.Closed += (sender2, e2) => in_Win_Cheat.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }

        public void Start_Script()
        {
            if (in_Win_Script == null)
            {
                Thread T_Script = new Thread(Script_);
                T_Script.SetApartmentState(ApartmentState.STA);
                T_Script.Start();
            }
            else
            {
                in_Win_Script.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Script.Topmost = false;
                    in_Win_Script.Topmost = true;
                }));
            }
        }

        public void Script_()
        {
            try
            {
                Win_Script in_Win_Script = new Win_Script(in_Win_Main);
                if (Setting.Boby.Scripts.Show)
                    in_Win_Script.Show();
                in_Win_Script.Closed += (sender2, e2) => in_Win_Script.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch { }
        }
    }
}
