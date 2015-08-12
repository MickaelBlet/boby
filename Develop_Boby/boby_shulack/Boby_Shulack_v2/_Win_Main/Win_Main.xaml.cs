
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


namespace Boby_Shulack
{
    public partial class Win_Main : Window
    {
        public Setting      in_Setting                    = null;
        public Win_Main     in_Win_Main                   = null;
       // public Win_Radar    in_Win_Radar                  = null;
       // public Win_Entity   in_Win_Entity                 = null;
        //public Win_Cheat    in_Win_Cheat                  = null;
       // public Win_Buff     in_Win_Buff                   = null;
       // public Win_Quick    in_Win_Quick                  = null;
       // public Thread_Entity in_Thread_Entity             = null;
        public Style        Style_ShugoLoading            = null;
        public Style        Style_Radar_Target            = null;

        void CheckUpdate()
        {
            if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe"))
            {
                string fileVersion = AssemblyName.GetAssemblyName(@"./" + "boby_update" + ".exe").Version.ToString();
                string check_version_web = "";

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://aion-add-file.url.ph/GetVersion.php?file=" + "boby_update" + ".exe");
                    }
                    catch (Exception)
                    {
                    }
                }

                if (check_version_web != "" && check_version_web != "..." && fileVersion != check_version_web)
                {
                    MessageBox.Show(this, "Download auto boby_update.exe", "Info");
                    using (WebClient Client = new WebClient())
                    {
                        Client.Proxy = null;
                        Client.DownloadFile(new Uri("http://aion-add-file.url.ph/Boby_Download.php?file=" + "boby_update.exe"), @".\boby_update.exe");
                    }
                }
            }
            if (!File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe"))
            {
                MessageBox.Show(this, "Download auto boby_update.exe", "Info");
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadFile(new Uri("http://aion-add-file.url.ph/Boby_Download.php?file=" + "boby_update.exe"), @".\boby_update.exe");
                }
            }
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = @".\";
                startInfo.Verb = "runas";
                startInfo.Arguments = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location);
                startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
                while (Process.GetProcessesByName("boby_update").Count() <= 0)
                    ;
                while (Process.GetProcessesByName("boby_update").Count() > 0)
                    ;
                this.Show();
            }
            catch
            {
                //in_Win_Main.Hide();
                //MessageBox.Show(in_Win_Main, "boby_update.exe not found.", "Error");
                //Environment.Exit(0);
            }
        }

        public Win_Main()
        {
            InitializeComponent();

            in_Win_Main = this;
            if (!File.Exists(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location) + ".vshost.exe"))
                CheckUpdate();
            Listing.List();

            in_Setting = new Setting(in_Win_Main);

            Online_Users_Sequence();

            Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;
            Style_Radar_Target = this.FindResource("Style_Target") as Style;

            Refresh_lb_Game();

            //cb_Radar.IsChecked = in_Setting.in_Radar.Show.Get_Value();
            //cb_Entity.IsChecked = in_Setting.in_Entity.Show.Get_Value();
            //cb_Buff.IsChecked = in_Setting.in_Buff.Show.Get_Value();
            //cb_Cheat.IsChecked = in_Setting.in_Cheat.Show.Get_Value();
            //cb_Quick.IsChecked = in_Setting.in_Quick.Show.Get_Value();
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
            //if (in_Win_Radar != null && in_Setting.in_Radar.Show.Get_Value() == true)
            //{
            //    in_Win_Radar.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Radar.Show();
            //    }));
            //}

            //if (in_Win_Entity != null && in_Setting.in_Entity.Show.Get_Value() == true)
            //{
            //    in_Win_Entity.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Entity.Show();
            //    }));
            //}

            //if (in_Win_Cheat != null && in_Setting.in_Cheat.Show.Get_Value() == true)
            //{
            //    in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Cheat.Show();
            //    }));
            //}

            //if (in_Win_Buff != null && in_Setting.in_Buff.Show.Get_Value() == true)
            //{
            //    in_Win_Buff.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Buff.Show();
            //    }));
            //}

            //if (in_Win_Quick != null && in_Setting.in_Quick.Show.Get_Value() == true)
            //{
            //    in_Win_Quick.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Quick.Show();
            //    }));
            //}
        }

        private void cb_hide_Unchecked(object sender, RoutedEventArgs e)
        {
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
        
            //if (in_Win_Buff != null)
            //{
            //    in_Win_Buff.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Buff.Hide();
            //    }));
            //}

            //if (in_Win_Quick != null)
            //{
            //    in_Win_Quick.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Quick.Hide();
            //    }));
            //}
        }

        private void Rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
            //if (in_Win_Radar != null)
            //{
            //    in_Win_Radar.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Radar.Show();
            //    }));
            //}
            in_Setting.in_Radar.Show.Set_Value(true);
        }

        private void cb_Radar_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Radar != null)
            //{
            //    in_Win_Radar.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Radar.Hide();
            //    }));
            //}
            in_Setting.in_Radar.Show.Set_Value(false);
        }

        private void cb_Entity_Checked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Entity != null)
            //{
            //    in_Win_Entity.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Entity.Show();
            //    }));
            //}
            in_Setting.in_Entity.Show.Set_Value(true);
        }

        private void cb_Entity_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Entity != null)
            //{
            //    in_Win_Entity.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Entity.Hide();
            //    }));
            //}
            in_Setting.in_Entity.Show.Set_Value(false);
        }

        private void cb_Cheat_Checked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Cheat != null)
            //{
            //    in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Cheat.Show();
            //    }));
            //}
            in_Setting.in_Cheat.Show.Set_Value(true);
        }

        private void cb_Cheat_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Cheat != null)
            //{
            //    in_Win_Cheat.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Cheat.Hide();
            //    }));
            //}
            in_Setting.in_Cheat.Show.Set_Value(false);
        }

        private void cb_Buff_Checked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Buff != null)
            //{
            //    in_Win_Buff.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Buff.Show();
            //    }));
            //}
            in_Setting.in_Buff.Show.Set_Value(true);
        }

        private void cb_Buff_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Buff != null)
            //{
            //    in_Win_Buff.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Buff.Hide();
            //    }));
            //}
            in_Setting.in_Buff.Show.Set_Value(false);
        }

        private void cb_Quick_Checked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Quick != null)
            //{
            //    in_Win_Quick.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Quick.Show();
            //    }));
            //}
            in_Setting.in_Quick.Show.Set_Value(true);
        }

        private void cb_Quick_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (in_Win_Quick != null)
            //{
            //    in_Win_Quick.Dispatcher.Invoke((Action)(() =>
            //    {
            //        in_Win_Quick.Hide();
            //    }));
            //}
            in_Setting.in_Quick.Show.Set_Value(false);
        }
        #endregion
    }
}