﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;

using System.Threading;
using System.Threading.Tasks;

using NS_Aion_Game;
using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    /// <summary>
    /// Logique d'interaction pour Win_Buff.xaml
    /// </summary>
    public partial class Win_Quick : Window
    {
        public Style        Style_Play            = null;
        public Style        Style_Pause           = null;
        public ControlTemplate Bg_Base    = null;
        public ControlTemplate Bg_Play    = null;

        public static Win_Main in_Win_Main = null;
        public Win_Quick_Setting in_Win_Quick_Setting = null;
        public bool             is_play = false;

        public Win_Quick(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            Style_Play = this.FindResource("Style_Buff_Play_Button") as Style;
            Style_Pause = this.FindResource("Style_Buff_Pause_Button") as Style;
            Bg_Base = this.FindResource("BG_Win_buff") as ControlTemplate;
            Bg_Play = this.FindResource("BG_Win_buff1") as ControlTemplate;

            in_Win_Main = tmp_in_Win_Main;
            in_Win_Main.in_Win_Quick = this;

            try
            {
                tFile_PreviewMouseLeftButtonDown(null, null);
                string[] ssplit = in_Win_Main.in_Setting.in_Quick.File.Get_Value().Split('\\');
                tFile.SelectedItem = ssplit[ssplit.Length - 1];
            }
            catch
            { }
            
            this.Top = in_Win_Main.in_Setting.in_Quick.Top.Get_Value();
            this.Left = in_Win_Main.in_Setting.in_Quick.Left.Get_Value();

            if (in_Win_Main.in_Setting.in_Quick.Show.Get_Value())
                this.Show();

            in_Win_Quick_Setting = new Win_Quick_Setting(in_Win_Main);

            Quick_Sequence();
        }

        bool InCombo(string filestronc)
        {
            for (int i = 0; i < tFile.Items.Count; i++)
            {
                if (filestronc == tFile.Items[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            in_Win_Main.in_Setting.in_Quick.Left.Set_Value((int)this.Left);
            in_Win_Main.in_Setting.in_Quick.Top.Set_Value((int)this.Top);
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Quick_Setting.IsVisible)
            {
                in_Win_Quick_Setting.Hide();
            }
            else
            {
                in_Win_Quick_Setting.Show();
            }
        }

        private void tFile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\QuickFiles\";

                string[] files = Directory.GetFiles(appPath, "*.xml");

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filestronc = files[i].Remove(0, appPath.Length);
                        if (!InCombo(filestronc))
                            tFile.Items.Add(filestronc);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void tFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\QuickFiles\";
                string filename = tFile.SelectedValue.ToString();

                in_Win_Quick_Setting.quick_collect.Clear();

                XmlSerializer serializer = new XmlSerializer(typeof(QuickCollection));

                using (FileStream stream = new FileStream(appPath + filename, FileMode.Open))
                {
                    IEnumerable<Quick> personData = (IEnumerable<Quick>)serializer.Deserialize(stream);

                    foreach (Quick p in personData)
                    {
                        in_Win_Quick_Setting.quick_collect.Add(p);
                    }
                }

                in_Win_Main.in_Setting.in_Quick.File.Set_Value(appPath + filename);

                string[] ssplit = in_Win_Main.in_Setting.in_Quick.File.Get_Value().Split('\\');
                in_Win_Quick_Setting.tFile.SelectedItem = ssplit[ssplit.Length - 1];
            }
            catch
            { }
        }

        private void bt_Play_Click(object sender, RoutedEventArgs e)
        {
            if (is_play)
            {
                is_play = false;
                bt_Play.Style = Style_Play;
                BG.Template = Bg_Base;
            }
            else
            {
                is_play = true;
                bt_Play.Style = Style_Pause;
                BG.Template = Bg_Play;
            }
        }
    }
}