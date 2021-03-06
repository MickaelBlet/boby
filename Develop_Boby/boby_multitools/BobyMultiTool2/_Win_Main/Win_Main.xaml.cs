﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.IO.Compression;
using System.Reflection;

using System.Runtime.InteropServices; // DllImport

using _Threads;

namespace BobyMultitools
{
    public partial class Win_Main : Window
    {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public Win_Main     in_Win_Main                   = null;
        public Win_Radar    in_Win_Radar                  = null;
        public Win_Entity   in_Win_Entity                 = null;
        public Win_Cheat    in_Win_Cheat                  = null;
        public Win_Script   in_Win_Script                 = null;
        public Thread_Entity in_Thread_Entity             = null;
        public Aion_Game.EntityList in_Entity_List                = null;
        public Aion_Game.DialogList in_Dialog_List                = null;
        public Style        Style_ShugoLoading            = null;
        public Style        Style_Radar_Target            = null;

        public Win_Main()
        {
            in_Win_Main = this;
            InitializeComponent();

            this.Show();
            MemoryLib.Offset.Download(this);

            Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;
            Style_Radar_Target = this.FindResource("Style_Target") as Style;

            Refresh_lb_Game();

            cb_Cheat.IsChecked = Setting.Boby.Cheat.Show;
            cb_Radar.IsChecked = Setting.Boby.Radar.Show;
            cb_Entity.IsChecked = Setting.Boby.Entity.Show;
            cb_Script.IsChecked = Setting.Boby.Scripts.Show;
        }

        #region _Event gui_

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Full_Close();
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            Full_Close();
        }

        private void Bt_Minimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void cb_hide_Checked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar != null && Setting.Boby.Radar.Show)
            {
                in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Radar.Show();
                }));
            }

            if (in_Win_Entity != null && Setting.Boby.Entity.Show)
            {
                in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Entity.Show();
                }));
            }

            if (in_Win_Cheat != null && Setting.Boby.Cheat.Show)
            {
                in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Cheat.Show();
                }));
            }

            if (in_Win_Script != null && Setting.Boby.Scripts.Show)
            {
                in_Win_Script.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Script.Show();
                }));
            }
        }

        private void cb_hide_Unchecked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar != null)
            {
                in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Radar.Hide();
                }));
            }

            if (in_Win_Entity != null)
            {
                in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Entity.Hide();
                }));
            }

            if (in_Win_Cheat != null)
            {
                in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Cheat.Hide();
                }));
            }
        
            if (in_Win_Script != null)
            {
                in_Win_Script.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Script.Hide();
                }));
            }
        }

        private void Lb_Game_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Start_App();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Start_App();
        }

        private void Lb_Game_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // no view selector
            if (e.Key == Key.Up)
            {
                if (lb_Game.SelectedIndex - 1 >= 0)
                    lb_Game.SelectedItem = lb_Game.Items[lb_Game.SelectedIndex - 1];
            }
            else if (e.Key == Key.Down)
            {
                if (lb_Game.SelectedIndex + 1 < lb_Game.Items.Count)
                    lb_Game.SelectedItem = lb_Game.Items[lb_Game.SelectedIndex + 1];
            }
            else if (e.Key == Key.Enter)
            {
                Start_App();
            }
        }

        private void bt_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh_lb_Game();
        }

        private void cb_Radar_Checked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar != null)
            {
                in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Radar.Show();
                }));
            }
            Setting.Boby.Radar.Show = true;
        }

        private void cb_Radar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar != null)
            {
                in_Win_Radar.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Radar.Hide();
                }));
            }
            Setting.Boby.Radar.Show = false;
        }

        private void cb_Entity_Checked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Entity != null)
            {
                in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Entity.Show();
                }));
            }
            Setting.Boby.Entity.Show = true;
        }

        private void cb_Entity_Unchecked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Entity != null)
            {
                in_Win_Entity.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Entity.Hide();
                }));
            }
            Setting.Boby.Entity.Show = false;
        }

        private void cb_Cheat_Checked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Cheat != null)
            {
                in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Cheat.Show();
                }));
            }
            Setting.Boby.Cheat.Show = true;
        }

        private void cb_Cheat_Unchecked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Cheat != null)
            {
                in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Cheat.Hide();
                }));
            }
            Setting.Boby.Cheat.Show = false;
        }

        private void cb_Script_Checked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Script != null)
            {
                in_Win_Script.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Script.Show();
                }));
            }
            Setting.Boby.Scripts.Show = true;
        }

        private void cb_Script_Unchecked(object sender, RoutedEventArgs e)
        {
            if (in_Win_Script != null)
            {
                in_Win_Script.Dispatcher.Invoke((Action)(() =>
                {
                    in_Win_Script.Hide();
                }));
            }
            Setting.Boby.Scripts.Show = false;
        }
        #endregion

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top < 1)
                this.Top = 0;
            if (this.Left < 1)
                this.Left = 0;
            if (this.Top + this.Height + 1 > SystemParameters.VirtualScreenHeight)
                this.Top = SystemParameters.VirtualScreenHeight - this.Height;
            if (this.Left + this.Width + 1 > SystemParameters.VirtualScreenWidth)
                this.Left = SystemParameters.VirtualScreenWidth - this.Width;
        }
    }
}