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

using MemoryLib;

namespace BobyMultitools
{
    public partial class Win_Radar : Window
    {
        public Win_Main in_Win_Main = null;
        public Win_Radar_Popup in_Win_Radar_Popup = null;
        public Win_Radar_Setting in_Win_Radar_Setting = null;

        public ImageSource s_BG = null;
        public ImageSource s_View = null;
        public ImageSource s_North = null;
        public ImageSource s_BG_Setting = null;

        public bool select_is_ready = true;

        public Win_Radar(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;
            in_Win_Main.in_Win_Radar = this;

            this.Left = Setting.Boby.Radar.Left;
            this.Top = Setting.Boby.Radar.Top;

            s_North = this.North.Source;
            //s_BG_Setting = this.BG_Setting.Source;

            #region _Check_Box_Ini_
            c_NPC.IsChecked = Setting.Boby.Radar.NPC;
            c_Ally.IsChecked = Setting.Boby.Radar.Ally;
            c_Ennemy.IsChecked = Setting.Boby.Radar.Hostile;
            c_Gather.IsChecked = Setting.Boby.Radar.Gather;
            c_IconPlus.IsChecked = Setting.Boby.Radar.IconPlus;
            c_North.IsChecked = Setting.Boby.Radar.North;
            c_BG.IsChecked = Setting.Boby.Radar.BGon;
            #endregion

            if (Setting.Boby.Radar.Show)
                this.Show();

            in_Win_Radar_Popup = new Win_Radar_Popup();
            in_Win_Radar_Setting = new Win_Radar_Setting(tmp_in_Win_Main);

            if (c_BG.IsChecked)
            {
                this.BG.Opacity = 0;
                this.View.Opacity = 0;
                this.North.Source = null;
                this.button_Setting.Visibility = Visibility.Hidden;
                this.bt_Setting.Opacity = 0;
            }

            Radar_Image_To_Canvas();
        }

        #region _Event gui_
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            Setting.Boby.Radar.Left = this.Left;
            Setting.Boby.Radar.Top = this.Top;
            if (in_Win_Radar_Popup.Opacity == 1d)
            {
                if (select_is_ready)
                {
                    in_Win_Radar_Popup.Opacity = 0d;
                    select_is_ready = false;
                    Thread Select = new Thread(() => SlashSelect(in_Win_Radar_Popup.entity));
                    Select.Start();
                }
            }
        }

        void SlashSelect(Aion_Game.Entity entity)
        {
            entity.Select();
            select_is_ready = true;
        }

        private void c_NPC_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.NPC = true;
        }

        private void c_NPC_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.NPC = false;
        }

        private void c_Ally_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Ally = true;
        }

        private void c_Ally_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Ally = false;
        }

        private void c_Ennemy_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Hostile = true;
        }

        private void c_Ennemy_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Hostile = false;
        }

        private void c_Gather_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Gather = true;
        }

        private void c_Gather_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Gather = false;
        }

        private void c_IconPlus_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.IconPlus = true;
        }

        private void c_IconPlus_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.IconPlus = false;
        }

        private void c_North_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.North = true;
        }

        private void c_North_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.North = false;
        }

        private void c_BG_Checked(object sender, RoutedEventArgs e)
        {
            this.BG.Opacity = 0;
            this.View.Opacity = 0;
            this.North.Source = null;
            this.button_Setting.Visibility = Visibility.Hidden;
            this.bt_Setting.Opacity = 0;
            Setting.Boby.Radar.BGon = true;
        }

        private void c_BG_Unchecked(object sender, RoutedEventArgs e)
        {
            this.BG.Opacity = 1;
            this.View.Opacity = 1;
            this.North.Source = s_North;
            this.button_Setting.Visibility = Visibility.Visible;
            this.bt_Setting.Opacity = 1;
            Setting.Boby.Radar.BGon = false;
        }

        private void b_close_Click(object sender, RoutedEventArgs e)
        {
            in_Win_Main.Full_Close();
        }
        #endregion

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                if (e.Delta < 0)
                {
                    if (Setting.Boby.Radar.Size > 0.9)
                        Setting.Boby.Radar.Size -= 0.1;
                }
                else
                {
                    if (Setting.Boby.Radar.Size < 2)
                        Setting.Boby.Radar.Size += 0.1;
                }
                //in_Win_Radar_Setting.sl_size.Value = (Setting.Boby.Radar.Size - 0.9) * 91d;
            }
            else
            {
                if (e.Delta < 0)
                {
                    if (Setting.Boby.Radar.Zoom > 1.5)
                        Setting.Boby.Radar.Zoom -= 0.02;
                }
                else
                {
                      Setting.Boby.Radar.Zoom += 0.02;
                }
                //in_Win_Radar_Setting.sl_zoom.Value = (Setting.Boby.Radar.Zoom - 1.5) * 50d;
            }
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar_Setting.IsVisible)
                in_Win_Radar_Setting.Hide();
            else
                in_Win_Radar_Setting.Show();
        }

        private void b_minimise_Click(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.ShowInTaskbar = false;
            else
                this.ShowInTaskbar = true;
        }

        private void b_setting_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Radar_Setting.IsVisible)
                in_Win_Radar_Setting.Hide();
            else
                in_Win_Radar_Setting.Show();
        }
    }
}
