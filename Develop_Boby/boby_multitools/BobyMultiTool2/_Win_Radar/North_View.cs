using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using NS_Aion_Game;
using MemoryLib;

namespace BobyMultitools
{
    public partial class Win_Radar
    {
        DispatcherTimer messageTimer2;

        private void Radar_View()
        {
            Thread T_Radar_North = new Thread(Radar_North_);
            T_Radar_North.SetApartmentState(ApartmentState.STA);
            T_Radar_North.Start();
        }

        private void Radar_North_()
        {
            messageTimer2 = new DispatcherTimer();
            messageTimer2.Tick += new EventHandler(_Radar_North);
            messageTimer2.Interval = new TimeSpan(0, 0, 0, 0, 10);
            messageTimer2.Start();
            System.Windows.Threading.Dispatcher.Run();
        }

        void _Radar_North(object sender, EventArgs e)
        {
            float PlayerRot = 0;
            float Rot = 0;

            if (SplMemory.ReadInt(Aion_Game.Modules.Game + Offset.Player.FreeCamRot) != 0)
                PlayerRot = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.FreeCamRotH);
            else
                PlayerRot = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.CamRotH);

            if (PlayerRot > 180)
                PlayerRot = PlayerRot - 360;

            Rot = PlayerRot;
            if (Rot < 0) Rot = 360 + Rot;
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (c_North.IsChecked == true)
                    Rot = 270;
                North.RenderTransform = new RotateTransform(Rot + 90, North.ActualWidth / 2d, North.ActualHeight / 2d);
                View.RenderTransform = new RotateTransform(Rot - PlayerRot, View.ActualWidth / 2d, View.ActualHeight / 2d);
            }));
        }
    }
}
