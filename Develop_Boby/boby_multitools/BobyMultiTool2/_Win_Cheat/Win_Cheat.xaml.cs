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
using System.Windows.Interop;

using System.Runtime.InteropServices;

using MemoryLib;

namespace BobyMultitools
{
    public partial class Win_Cheat : Window
    {
        public static Win_Main in_Win_Main = null;
        public Win_Cheat_Setting in_Win_Cheat_Setting = null;
        public Win_Cheat_Move in_Win_Cheat_Move = null;

        public Style style_List_Header = null;
        public Style style_List_Header_Green = null;
        public Style style_List_Header_Red = null;

        public bool select_is_ready = true;

        public Win_Cheat(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;
            in_Win_Main.in_Win_Cheat = this;
         
            in_Win_Cheat_Setting = new Win_Cheat_Setting(in_Win_Main);
            in_Win_Cheat_Move = new Win_Cheat_Move(in_Win_Main);

            this.sl_Atk.Value = in_Win_Main.in_Setting.in_Cheat.Attack_Speed.Get_Value();
            sl_Atk_ValueChanged(null, null);

            this.Left = in_Win_Main.in_Setting.in_Cheat.Left.Get_Value();
            this.Top = in_Win_Main.in_Setting.in_Cheat.Top.Get_Value();

            Cheat_Sequence();
        }

        #region _Event gui_
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void Bt_Minimise_Click(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Minimized;
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Cheat_Setting.IsVisible)
                in_Win_Cheat_Setting.Hide();
            else
                in_Win_Cheat_Setting.Show();
        }

        private void bt_Move_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Cheat_Move.IsVisible)
                in_Win_Cheat_Move.Hide();
            else
                in_Win_Cheat_Move.Show();
        }

        private void Rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            in_Win_Main.in_Setting.in_Cheat.Left.Set_Value((int)this.Left);
            in_Win_Main.in_Setting.in_Cheat.Top.Set_Value((int)this.Top);
        }


        private void sl_Atk_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Cheat.Attack_Speed.Set_Value((int)sl_Atk.Value);
            lb_val_atk_speed.Content = sl_Atk.Value.ToString() + " %";
        }

        #endregion

        private void Window_StateChanged(object sender, EventArgs e)
        {
			if (this.WindowState == WindowState.Normal)
				this.ShowInTaskbar = false;
			else
				this.ShowInTaskbar = true;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top < 0)
                this.Top = 0;
            if (this.Left < -8)
                this.Left = -8;
            if (this.Top + this.Height - 8 > SystemParameters.VirtualScreenHeight)
                this.Top = SystemParameters.VirtualScreenHeight - this.Height + 8;
            if (this.Left + this.Width - 9 > SystemParameters.VirtualScreenWidth)
                this.Left = SystemParameters.VirtualScreenWidth - this.Width + 9;
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (this.Top < 0)
                this.Top = 0;
            if (this.Left < -8)
                this.Left = -8;
            if (this.Top + this.Height - 8 > SystemParameters.VirtualScreenHeight)
                this.Top = SystemParameters.VirtualScreenHeight - this.Height + 8;
            if (this.Left + this.Width - 9 > SystemParameters.VirtualScreenWidth)
                this.Left = SystemParameters.VirtualScreenWidth - this.Width + 9;
        }
    }
}