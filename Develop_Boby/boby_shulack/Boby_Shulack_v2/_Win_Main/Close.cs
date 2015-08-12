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

namespace Boby_Shulack
{
    public partial class Win_Main
    {
        private bool In_Close = false;

        public void Full_Close()
        {
            if (In_Close == false)
            {
                In_Close = true;
                //if (in_Win_Radar != null)
                //{
                //    in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                //    {
                //        in_Win_Radar.Hide();
                //    }));
                //}
                //if (in_Win_Entity != null)
                //{
                //    in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                //    {
                //        in_Win_Entity.Hide();
                //    }));
                //}
                //if (in_Win_Cheat != null)
                //{
                //    in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                //    {
                //        in_Win_Cheat.Hide();
                //    }));
                //}
                in_Setting.Save();
                Environment.Exit(0);
            }
        }
    }
}
