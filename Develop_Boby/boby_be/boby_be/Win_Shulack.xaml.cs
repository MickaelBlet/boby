/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 10/02/2014
 * Heure: 22:54
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Interop;
using System.Windows.Controls.Primitives;

using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices; // DllImport

using AddProcess;
using MemoryLib;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

using System.Net.Mail;

using System.ComponentModel;

namespace BOBY_Shulack
{
    /// <summary>
    /// Interaction logic for Win_Shulack.xaml
    /// </summary>
    public partial class Win_Shulack : Window
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public System.Windows.Forms.NotifyIcon trayicon = new System.Windows.Forms.NotifyIcon();

        public Win_Choose ini_Win_Choose = null;
        public Win_Loot ini_Win_Loot = null;
        public Win_Skills ini_Win_Skills = null;

        public bool Is_Run = false;
        public bool Is_Assist = false;
        public bool Is_Boat = false;

        public Font m_font = new Font("Arial", 10);
        public Color m_col = Color.White;
        public Color bg_col = Color.Black;

        public System.Drawing.Icon[] listicon = null;

        public void ShowText(string text)
        {
            /*int num = int.Parse(text);
			
            if (num < 100)
            {
                trayicon.Icon.Dispose();
                trayicon.Icon = listicon[num];
            }*/
        }

        public System.Drawing.Icon[] SetIconList()
        {
            System.Drawing.Icon[] list = new System.Drawing.Icon[100];

            Brush brush = new SolidBrush(m_col);

            // Create a bitmap and draw text on it
            for (int i = 0; i < 99; i++)
            {
                using (Bitmap bitmap = new Bitmap(16, 16))
                {
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.Clear(bg_col);
                    graphics.DrawString(string.Format("{0:00}", i), m_font, brush, -1, 0);
                    graphics.Dispose();

                    // Convert the bitmap with text to an Icon
                    IntPtr hIcon = bitmap.GetHicon();
                    System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(hIcon);

                    list[i] = icon;
                }
            }

            return list;
        }

        public void Set_Tray_Text(string str)
        {
            this.trayicon.Text = str;
        }

        public void Set_Log1(string str)
        {
            this.tbLog1.Text = str;
        }

        public void Set_Log2(string str)
        {
            this.tbLog2.Text = str;
        }

        public void Set_Player_Name(string str)
        {
            this.Player_Name.Text = str;
        }

        public void Set_Title(string str)
        {
            this.Title = str;
        }

        public void Set_Count(string str)
        {
            this.Shulack_Count.Text = "Shulack(s) Kill : " + str;
        }

        public void Set_Kinas(string str)
        {
            this.Kinas_Count.Text = "Kinas : " + String.Format("{0:N0}", long.Parse(str));
        }

        public void Set_Enable_Start()
        {
            this.Button_Start.IsEnabled = true;
        }

        public void Set_Disable_Start()
        {
            this.Button_Start.IsEnabled = false;
        }

        public Win_Shulack()
        {
            InitializeComponent();

            this.Left = SystemParameters.VirtualScreenWidth / 2 - this.Width / 2;
            this.Top = 250;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Win_Choose))
                {
                    ini_Win_Choose = (window as Win_Choose);
                }
            }

            //listicon = SetIconList();

            ini_Win_Loot = ini_Win_Choose.ini_Win_Loot;

            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/boby_be;component/Icon.ico")).Stream;
            trayicon.Icon = new System.Drawing.Icon(iconStream);
            trayicon.Text = "BOBY Shulack";
            iconStream.Close();

            trayicon.Click += new System.EventHandler(this.menuItemSelectGame_Click);
        }

        void Button_Minimise_Click(object sender, RoutedEventArgs e)
        {
            this.trayicon.Visible = true;
            ini_Win_Skills = ini_Win_Choose.ini_Win_Skills;
            ini_Win_Skills.Hide();
            ini_Win_Loot.Hide();
            this.Hide();
        }

        void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            FullClose();
        }

        void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void Show_Game()
        {
            ShowWindow(AionProcess.whandle, SW_SHOW);
        }

        public void FullClose()
        {
            ini_Win_Skills = ini_Win_Choose.ini_Win_Skills;
            //ini_Settings.Save();
            ini_Win_Loot.Save_List();
            ini_Win_Skills.Save_List();
            if (MessageBox.Show(this, "Do you want to close this application?",
                                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Show_Game();
                ini_Win_Choose.ini_Settings.Save();
                //ni.Visible = false;
                Environment.Exit(0);
            }
            else
            {
                // Do not close the window
            }
        }

        void Button_Recycle_List_Click(object sender, RoutedEventArgs e)
        {
            if (ini_Win_Loot.IsVisible)
            {
                ini_Win_Loot.Hide();
                ini_Win_Loot.Save_List();
            }
            else
            {
                Window_LocationChanged(null, null);
                ini_Win_Loot.Show();
            }
        }

        void Window_LocationChanged(object sender, EventArgs e)
        {
            ini_Win_Skills = ini_Win_Choose.ini_Win_Skills;
            if (this.Top < 0)
                this.Top = 0;
            if (this.Left < -8)
                this.Left = -8;
            if (this.Top + this.Height - 9 > SystemParameters.VirtualScreenHeight)
                this.Top = SystemParameters.VirtualScreenHeight - this.Height + 8;
            if (this.Left + this.Width - 9 > SystemParameters.VirtualScreenWidth)
                this.Left = SystemParameters.VirtualScreenWidth - this.Width + 9;

            if (this.Top + ini_Win_Loot.Height > SystemParameters.VirtualScreenHeight)
                ini_Win_Loot.Top = SystemParameters.VirtualScreenHeight - ini_Win_Loot.Height;
            else
                ini_Win_Loot.Top = this.Top;
            if ((this.Left + this.Width - 9) + ini_Win_Loot.Width > SystemParameters.VirtualScreenWidth)
                ini_Win_Loot.Left = this.Left - ini_Win_Loot.Width + 9;
            else
                ini_Win_Loot.Left = this.Left + this.Width - 9;

            if (this.Top + ini_Win_Skills.Height > SystemParameters.VirtualScreenHeight)
                ini_Win_Skills.Top = SystemParameters.VirtualScreenHeight - ini_Win_Skills.Height;
            else
                ini_Win_Skills.Top = this.Top;
            if ((this.Left + this.Width - 9) + ini_Win_Skills.Width > SystemParameters.VirtualScreenWidth)
                ini_Win_Skills.Left = this.Left - ini_Win_Skills.Width + 9;
            else
                ini_Win_Skills.Left = this.Left + this.Width - 9;
        }

        void Button_Skills_List_Click(object sender, RoutedEventArgs e)
        {
            ini_Win_Choose.ini_Boucle_Bateau.ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]), "quest_dialog");
            //ini_Win_Skills = ini_Win_Choose.ini_Win_Skills;
            //if (ini_Win_Skills.IsVisible)
            //{
            //    ini_Win_Skills.Hide();
            //    ini_Win_Skills.Save_List();
            //}
            //else
            //{
            //    Window_LocationChanged(null, null);
            //    ini_Win_Skills.Show();
            //}
        }

        void Button_Hide_Click(object sender, RoutedEventArgs e)
        {
            ini_Win_Choose.HideAION();
        }

        void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            ini_Win_Choose.DlgAionIni();
            if (!Is_Run && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1)
            {
                MessageBox.Show("Pet Loot !?");
                return;
            }
            if (!Is_Assist)
                Is_Run = !Is_Run;
            if (!Is_Run)
            {
                Button_Viewer.IsEnabled = false;
                Button_Start.Content = "Start";
            }
            else
            {
                Button_Viewer.IsEnabled = true;
                Button_Start.Content = "Stop";
                float player_x = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
                float player_y = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);
                if (player_x > 551 && player_x < 563 && player_y > 530 && player_y < 542
                   && Is_Assist == false)
                {
                    Is_Assist = true;
                    ini_Win_Choose.Start_Assist();
                    Is_Assist = false;
                }
                else if (player_x > 1983.145142f - 8 && player_x < 1983.145142f + 8 && player_y > 1746.883667f - 8 && player_y < 1746.883667f + 8)
                {
                    ini_Win_Choose.DlgAion("/anon");
                    Button_Viewer.IsEnabled = true;
                    Button_Start.Content = "Stop";
                    Is_Boat = true;
                    ini_Win_Choose.ini_Boucle_Delete.thread_Delete.Abort();
                    ini_Win_Choose.ini_Boucle_Skills.thread_Chain.Abort();
                }
                else if (player_x > 499.0f - 8 && player_x < 499.0 + 8 && player_y > 2039.9 - 8 && player_y < 2039.9 + 8)
                {
                    ini_Win_Choose.DlgAion("/anon");
                    Button_Viewer.IsEnabled = true;
                    Button_Start.Content = "Stop";
                    Is_Boat = true;
                    ini_Win_Choose.ini_Boucle_Delete.thread_Delete.Abort();
                    ini_Win_Choose.ini_Boucle_Skills.thread_Chain.Abort();
                }
                else
                    ini_Win_Choose.DlgAion("/anon");
            }
        }

        public bool IsCheck_Recycle_Loot()
        {
            if (checkBox_deloot.IsChecked == true)
                return true;
            else
                return false;
        }

        void Button_Viewer_Click(object sender, RoutedEventArgs e)
        {
            if (ini_Win_Choose.ini_Win_Viewer.IsVisible)
            {
                ini_Win_Choose.ini_Win_Viewer.Hide();
            }
            else
            {
                ini_Win_Choose.ini_Win_Viewer.Show();
                Button_Minimise_Click(null, null);
            }
        }

        private void menuItemSelectGame_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            if (!this.IsVisible)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
                this.trayicon.Visible = false;
            }
        }
    }
}