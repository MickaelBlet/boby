using System;
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

using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_Cheat_Move : Window
    {
        public static Win_Main in_Win_Main = null;
        public Hashtable Image_File_Mini = null;
        public Hashtable Image_File_Real = null;

        public IconCollection icon_collect;
        public IconSaveCollection icon_save_collect;
        public BuffCollection buff_collect;

        public Win_Cheat_Move(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;
        }

        private void hotkey_for_textbox(object textBoxZlock, object p1, object p2)
        {
            throw new NotImplementedException();
        }

        private void rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                double CamRotH = (double)SplMemory.ReadFloat(Aion_Process.Game.Base + Offset.Player.CamRotH) / 180d;
                float PosX = (float)((0.5 + 1) / 5d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                float PosY = (float)((0.5 + 1) / 5d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                Aion_Game.Player.X -= PosX;
                Aion_Game.Player.Y += PosY;
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                Aion_Game.Player.Z += 0.5f;
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                Aion_Game.Player.Z -= 0.5f;
            }
        }

        private void BigUp_Click(object sender, RoutedEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                Aion_Game.Player.Z += 5f;
            }
        }

        private void BigDown_Click(object sender, RoutedEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                Aion_Game.Player.Z -= 5f;
            }
        }

        private void Forward_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Aion_Game.Player.Node != 0)
            {
                double CamRotH = (double)SplMemory.ReadFloat(Aion_Process.Game.Base + Offset.Player.CamRotH) / 180d;
                float PosX = (float)((0.5 + 1) / 5d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                float PosY = (float)((0.5 + 1) / 5d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                Aion_Game.Player.X -= PosX;
                Aion_Game.Player.Y += PosY;
            }
        }
    }
}