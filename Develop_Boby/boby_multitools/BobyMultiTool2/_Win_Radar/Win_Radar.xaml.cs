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

            this.Left = in_Win_Main.in_Setting.in_Radar.Left.Get_Value();
            this.Top = in_Win_Main.in_Setting.in_Radar.Top.Get_Value();

            //s_BG = this.BG.Source;
            //s_View = this.View.Source;
            s_North = this.North.Source;
            s_BG_Setting = this.BG_Setting.Source;

            #region _Check_Box_Ini_
            c_NPC.IsChecked         = in_Win_Main.in_Setting.in_Radar.NPC.Get_Value();
            c_Ally.IsChecked        = in_Win_Main.in_Setting.in_Radar.Ally.Get_Value();
            c_Ennemy.IsChecked      = in_Win_Main.in_Setting.in_Radar.Hostile.Get_Value();
            c_Gather.IsChecked      = in_Win_Main.in_Setting.in_Radar.Gather.Get_Value();
            c_IconPlus.IsChecked    = in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value();
            c_North.IsChecked       = in_Win_Main.in_Setting.in_Radar.Nord.Get_Value();
            //c_Overlay.IsChecked     = in_Win_Main.in_Setting.in_Radar.Overlay.Get_Value();
            c_BG.IsChecked          = in_Win_Main.in_Setting.in_Radar.BGon.Get_Value();
            #endregion

            if (in_Win_Main.in_Setting.in_Radar.Show.Get_Value())
                this.Show();

            in_Win_Radar_Popup = new Win_Radar_Popup();
            in_Win_Radar_Setting = new Win_Radar_Setting(tmp_in_Win_Main);

            if (c_BG.IsChecked)
            {
                //this.BG.Source = null;
                //this.View.Source = null;
                this.North.Source = null;
                this.BG_Setting.Source = null;
                this.bt_Setting.Opacity = 0;
            }

            Radar_View();
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
            in_Win_Main.in_Setting.in_Radar.Left.Set_Value((int)this.Left);
            in_Win_Main.in_Setting.in_Radar.Top.Set_Value((int)this.Top);
            if (in_Win_Radar_Popup.Opacity == 1d)
            {
                if (select_is_ready)
                {
                    in_Win_Radar_Popup.Opacity = 0d;
                    select_is_ready = false;
                    Thread Select = new Thread(() => SlashSelect(in_Win_Radar_Popup.entityPtr));
                    Select.Start();
                }
            }
        }

        void SlashSelect(long entityPtr)
        {
            Character.Select(entityPtr);
            select_is_ready = true;
        }

        private void c_NPC_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.NPC.Set_Value(true);
        }

        private void c_NPC_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.NPC.Set_Value(false);
        }

        private void c_Ally_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Ally.Set_Value(true);
        }

        private void c_Ally_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Ally.Set_Value(false);
        }

        private void c_Ennemy_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Hostile.Set_Value(true);
        }

        private void c_Ennemy_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Hostile.Set_Value(false);
        }

        private void c_Gather_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Gather.Set_Value(true);
        }

        private void c_Gather_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Gather.Set_Value(false);
        }

        private void c_IconPlus_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.IconPlus.Set_Value(true);
        }

        private void c_IconPlus_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.IconPlus.Set_Value(false);
        }

        private void c_North_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Nord.Set_Value(true);
        }

        private void c_North_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Nord.Set_Value(false);
        }

        private void c_Overlay_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Overlay.Set_Value(true);
        }

        private void c_Overlay_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Radar.Overlay.Set_Value(false);
        }

        private void c_BG_Checked(object sender, RoutedEventArgs e)
        {
            //this.BG.Source = null;
            //this.View.Source = null;
            this.North.Source = null;
            this.BG_Setting.Source = null;
            this.bt_Setting.Opacity = 0;
            in_Win_Main.in_Setting.in_Radar.BGon.Set_Value(true);
        }

        private void c_BG_Unchecked(object sender, RoutedEventArgs e)
        {
            //this.BG.Source = s_BG;
            //this.View.Source = s_View;
            this.North.Source = s_North;
            this.BG_Setting.Source = s_BG_Setting;
            this.bt_Setting.Opacity = 1;
            in_Win_Main.in_Setting.in_Radar.BGon.Set_Value(false);
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
                    if (in_Win_Main.in_Setting.in_Radar.Size.Get_Value() > 0.9)
                        in_Win_Main.in_Setting.in_Radar.Size.Set_Value(in_Win_Main.in_Setting.in_Radar.Size.Get_Value() - 0.1f);
                }
                else
                {
                    if (in_Win_Main.in_Setting.in_Radar.Size.Get_Value() < 2)
                        in_Win_Main.in_Setting.in_Radar.Size.Set_Value(in_Win_Main.in_Setting.in_Radar.Size.Get_Value() + 0.1f);
                }
                in_Win_Radar_Setting.sl_size.Value = (int)((in_Win_Main.in_Setting.in_Radar.Size.Get_Value() - 0.9) * 91d);
            }
            else
            {
                if (e.Delta < 0)
                {
                    if (in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() > 1.50)
                        in_Win_Main.in_Setting.in_Radar.Zoom.Set_Value(in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() - 0.02f);
                }
                else
                {
                    in_Win_Main.in_Setting.in_Radar.Zoom.Set_Value(in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() + 0.02f);
                }
                in_Win_Radar_Setting.sl_zoom.Value = (int)((in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() - 1.5d) * 50d);
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
