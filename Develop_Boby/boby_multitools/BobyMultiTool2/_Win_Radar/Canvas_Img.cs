using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Media.Effects;
using System.Windows.Interop;

using MemoryLib;
using Aion_Process;
using Aion_Game;
using Windows_And_Process;

namespace BobyMultitools
{
    public partial class Win_Radar
    {
        public static Image[,] Img_Entity_Index = new Image[9, 300];
        public static int[] Last_Count_Index = new int[9];
        public static Image Player = new Image();
        public static Image Target = new Image();
        public static System.Windows.Shapes.Ellipse e1 = null;
        public static System.Windows.Shapes.Ellipse e2 = null;
        public static System.Windows.Shapes.Ellipse e3 = null;

        public static IntPtr Radar_Handle = IntPtr.Zero;
        public static IntPtr Radar_Setting_Handle = IntPtr.Zero;
        public static IntPtr Radar_Popup_Handle = IntPtr.Zero;

        DispatcherTimer messageTimer;

        public ImageSource Player_NotFly = null;
        public ImageSource Player_Fly = null;
        public ImageSource Aggro = null;
        public ImageSource Signal_Aggro = null;

        public static double sinr = 0;
        public static double cosr = 0;

        public static bool Image_Focus = false;
        public static long ID_Focus = 0;
        public static long ID_Target_Player = 0;

        public static TimeSpan t150 = TimeSpan.Zero;
        public static TimeSpan t1 = TimeSpan.Zero;

        private void Radar_Image_To_Canvas()
        {
            Player_NotFly = (ImageSource)Application.Current.FindResource("Entity.Player");
            Player_Fly = (ImageSource)Application.Current.FindResource("Entity.Player_Fly");
            Aggro = (ImageSource)Application.Current.FindResource("Sensory.Sensory_01");
            Signal_Aggro = (ImageSource)Application.Current.FindResource("Sensory.Sensory_03");

            e1 = new System.Windows.Shapes.Ellipse();
            e2 = new System.Windows.Shapes.Ellipse();
            e3 = new System.Windows.Shapes.Ellipse();

            e1.Height = 0;
            e1.Width = 0;
            e2.Height = 0;
            e2.Width = 0;
            e3.Height = 0;
            e3.Width = 0;

            e1.StrokeThickness = 1;
            e1.Stroke = ToBrush("#55FFFFFF");
            e2.StrokeThickness = 1;
            e2.Stroke = ToBrush("#55FFFFFF");
            e3.StrokeThickness = 1;
            e3.Stroke = ToBrush("#55FFFFFF");

            Canvas_Radar.Children.Add(e1);
            Canvas_Radar.Children.Add(e2);
            Canvas_Radar.Children.Add(e3);

            Player.Source = Player_NotFly;
            Canvas_Radar.Children.Add(Player);

            DeclareMultiImage(Img_Entity_Index);

            Target.Style = in_Win_Main.Style_Radar_Target;
            Canvas_Radar.Children.Add(Target);

            t150 = new TimeSpan(0, 0, 0, 0, 150);
            t1 = new TimeSpan(0, 0, 0, 0, 20);

            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = t1;
            messageTimer.Start();
        }

        public Brush ToBrush(string argb)
        {
            var bc = new BrushConverter();

            return (Brush)bc.ConvertFrom(argb);
        }

        public void DeclareMultiImage(Image[,] image)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 300; i++)
                {
                    image[j, i] = new Image();
                    Canvas_Radar.Children.Add(image[j, i]);
                    Canvas.SetLeft(image[j, i], -100);
                    Canvas.SetTop(image[j, i], -100);
                }
            }
        }

        public void HideUntilImage(Image[,] image, int[] compteur, int[] last_compteur)
        {
            for (int j = 0; j < 9; j++)
            {
                if (last_compteur[j] > compteur[j])
                {
                    for (int i = last_compteur[j] - 1; i >= compteur[j]; i--)
                    {
                        image[j, i].Source = null;
                        Canvas.SetLeft(image[j, i], -100);
                        Canvas.SetTop(image[j, i], -100);
                    }
                }
            }
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            Radar_Image_To_Canvas_();
        }

        private void Radar_Image_To_Canvas_()
        {
            if (Game.Base == 0 || Game.Pid == 0)
            {
                Thread.Sleep(500);
                return;
            }
            if (this.IsVisible == false)
                return;
            try
            {
                int[] count_entity_index = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                Image_Focus = false;

                string tWhere = SplMemory.ReadWchar(Game.Base + Offset.Game.Where, 60);

                float PlayerX = SplMemory.ReadFloat(Game.Base + Offset.Player.X);
                float PlayerY = SplMemory.ReadFloat(Game.Base + Offset.Player.Y);

                bool Player_IsFly = SplMemory.ReadByte(Game.Base + Offset.Player.IsFly) == 1;

                float PlayerRot = 0;

                if (SplMemory.ReadInt(Game.Base + Offset.Player.FreeCamRot) != 0)
                    PlayerRot = SplMemory.ReadFloat(Game.Base + Offset.Player.FreeCamRotH);
                else
                    PlayerRot = SplMemory.ReadFloat(Game.Base + Offset.Player.CamRotH);

                if (PlayerRot > 180)
                    PlayerRot = PlayerRot - 360;

                float Angle_X = PlayerRot;
                if (c_North.IsChecked)
                    Angle_X = -90;

                if (Angle_X > 0 && Angle_X < 180)
                    Angle_X = Angle_X * -1;
                else if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = (360 - Math.Abs(Angle_X)) * -1;

                double radian = Angle_X * (3.14 / 180) * -1;

                sinr = Math.Sin(radian);
                cosr = Math.Cos(radian);

                if (Player_IsFly)
                    Player.Source = Player_Fly;
                else
                    Player.Source = Player_NotFly;

                Canvas.SetLeft(Target, -100);
                Canvas.SetTop(Target, -100);

                e1.Width = 26 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                e2.Width = 51 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                e3.Width = 76 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                e1.Height = 26 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                e2.Height = 51 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                e3.Height = 76 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();

                Canvas.SetLeft(Player, Canvas_Radar.Width / 2 - Player.ActualWidth / 2);
                Canvas.SetTop(Player, Canvas_Radar.Height / 2 - Player.ActualHeight / 2);
                Canvas.SetLeft(e1, Canvas_Radar.Width / 2 - e1.Width / 2);
                Canvas.SetTop(e1, Canvas_Radar.Height / 2 - e1.Height / 2);
                Canvas.SetLeft(e2, Canvas_Radar.Width / 2 - e2.Width / 2);
                Canvas.SetTop(e2, Canvas_Radar.Height / 2 - e2.Height / 2);
                Canvas.SetLeft(e3, Canvas_Radar.Width / 2 - e3.Width / 2);
                Canvas.SetTop(e3, Canvas_Radar.Height / 2 - e3.Height / 2);

                North.RenderTransform = new RotateTransform(-Angle_X + 90, North.ActualWidth / 2d, North.ActualHeight / 2d);
                View.RenderTransform = new RotateTransform(-Angle_X - PlayerRot, North.ActualWidth / 2d, North.ActualHeight / 2d);

                foreach (var view in Entity_To_View.View.Values)
                {
                    if (view.img == null && view.entity.Type != eType.Player)
                        continue;

                    try
                    {
                        long ID = SplMemory.ReadLong(view.entity.NodeStatus + Offset.Status.ID);
                        if (ID != view.entity.Id)
                            continue;
                    }
                    catch
                    { }

                    //entity.Update();

                    if (ID_Target_Player == view.entity.Id
                        && ID_Target_Player != Aion_Game.Player.Id
                        && ID_Target_Player != 0)
                    {
                        if (view.entity.Type == eType.Player || view.entity.Distance3D > 120)
                        {
                            Canvas.SetLeft(Target, -100);
                            Canvas.SetTop(Target, -100);
                        }
                        else
                        {
                            PosImage(Target, view.entity.X, view.entity.Y, PlayerRot, PlayerX, PlayerY);
                            if (Target.IsMouseDirectlyOver)
                            {
                                in_Win_Radar_Popup.PopupContent(view.entity);
                                in_Win_Radar_Popup.Opacity = 1d;
                                in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(Target) - in_Win_Radar_Popup.Width;
                                in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(Target) - in_Win_Radar_Popup.Height;
                                Image_Focus = true;
                            }
                        }
                    }
                    else if (IsAggro(view.entity.NodeStatus) &&
                     (c_NPC.IsChecked && view.entity.Type == eType.NPC ||
                      c_Ally.IsChecked && view.entity.Type == eType.User && view.entity.Attitude == fAttitude.Friendly ||
                      c_Ennemy.IsChecked && view.entity.Type == eType.User && view.entity.Attitude != fAttitude.Friendly
                     )
                     && view.entity.Hp != 0 && view.entity.HpPercent != 0
                     && count_entity_index[view.radar_img_index] < 300)
                    {
                        ImageOnRadar(Img_Entity_Index[view.radar_img_index, count_entity_index[view.radar_img_index]], Aggro, view.entity, PlayerRot, PlayerX, PlayerY, 0.6f);
                        count_entity_index[view.radar_img_index]++;
                    }

                    if (view.entity.Type == eType.Player)
                    {
                        float RotPlayer = 0;
                        try
                        {
                            if (view.entity.VehicleNode != 0)
                                RotPlayer = SplMemory.ReadFloat(SplMemory.ReadLong(SplMemory.ReadLong(view.entity.VehicleNode + Offset.Status.Node) + Offset.Entity.Status) + Offset.Status.Rot);
                            else
                                RotPlayer = SplMemory.ReadFloat(SplMemory.ReadLong(view.entity.Node + Offset.Entity.Status) + Offset.Status.Rot);
                            ID_Target_Player = SplMemory.ReadLong(SplMemory.ReadLong(view.entity.Node + Offset.Entity.Status) + Offset.Status.TargetId);
                        }
                        catch { }

                        float Rot = PlayerRot;
                        if (c_North.IsChecked)
                            Rot = 270;

                        Player.RenderTransform = new RotateTransform((RotPlayer - Rot) * -1, Player.ActualWidth / 2, Player.ActualHeight / 2);
                    }
                    else
                    {
                        if (count_entity_index[view.radar_img_index] < 300 && view.in_radar)
                        {
                            ImageOnRadar(Img_Entity_Index[view.radar_img_index, count_entity_index[view.radar_img_index]], view.img, view.entity, PlayerRot, PlayerX, PlayerY);
                            count_entity_index[view.radar_img_index]++;
                        }
                    }
                }

                if (!Image_Focus)
                {
                    in_Win_Radar_Popup.Topmost = false;
                    in_Win_Radar_Popup.Opacity = 0d;
                    ID_Focus = 0;
                }
                else
                {
                    in_Win_Radar_Popup.Topmost = true;
                    in_Win_Radar_Popup.Opacity = 1d;
                }

                HideUntilImage(Img_Entity_Index, count_entity_index, Last_Count_Index);
                Last_Count_Index = count_entity_index;

                //if (Radar_Setting_Handle == IntPtr.Zero)
                //    Radar_Setting_Handle = new WindowInteropHelper(this.in_Win_Radar_Setting).Handle;

                //if (Radar_Popup_Handle == IntPtr.Zero)
                //    Radar_Popup_Handle = new WindowInteropHelper(this.in_Win_Radar_Popup).Handle;

                //if (Radar_Handle == IntPtr.Zero)
                //    Radar_Handle = new WindowInteropHelper(this).Handle;

                //if (!(Windows_And_Process.WindowsIsForeground(Aion_Game.whandle)
                //    || Windows_And_Process.WindowsIsForeground(Radar_Handle)
                //    || Windows_And_Process.WindowsIsForeground(Radar_Setting_Handle)
                //    || Windows_And_Process.WindowsIsForeground(Radar_Popup_Handle)))
                //{
                //    messageTimer.Interval = t150;
                //}
                //else
                //    messageTimer.Interval = t1;
            }
            catch { Console.WriteLine("CanvasImg: Entity"); }
        }

        void ImageOnRadar(Image image, ImageSource source, Entity entity, float PlayerRot, float PlayerX, float PlayerY)
        {
            if (source == null)
                return;

            image.Source = source;

            try
            {
                long NodeLoc = SplMemory.ReadLong(entity.Node + Offset.Entity.Loc);

                if (NodeLoc == 0 || NodeLoc == 0xCDCDCDCD)
                    PosImage(image, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY);
                else
                {
                    float entity_X = SplMemory.ReadFloat(NodeLoc + Offset.Loc.X);
                    float entity_Y = SplMemory.ReadFloat(NodeLoc + Offset.Loc.Y);

                    if (entity_X == float.NaN || entity_X == 0 || entity_Y == float.NaN || entity_Y == 0)
                        PosImage(image, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY);
                    else
                        PosImage(image, entity_X, entity_Y, PlayerRot, PlayerX, PlayerY);
                }
            }
            catch
            {
                Console.WriteLine("Problem PosImage.");
                PosImage(image, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY);
            }
            try
            {
                if (image.IsMouseDirectlyOver)
                {
                    in_Win_Radar_Popup.PopupContent(entity);
                    in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(image) - in_Win_Radar_Popup.Width;
                    in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(image) - in_Win_Radar_Popup.Height;
                    ID_Focus = entity.Id;
                    Image_Focus = true;
                }
            }
            catch
            { }
        }

        void ImageOnRadar(Image image, ImageSource source, Entity entity, float PlayerRot, float PlayerX, float PlayerY, float Scale)
        {
            if (source == null)
            {
                Console.WriteLine("Problem PosImage.");
                return;
            }

            image.Source = source;

            try
            {
                float entity_X = SplMemory.ReadFloat(entity.NodeLocation + Offset.Loc.X);
                float entity_Y = SplMemory.ReadFloat(entity.NodeLocation + Offset.Loc.Y);
                float entity_Z = SplMemory.ReadFloat(entity.NodeLocation + Offset.Loc.Z);

                entity.X = entity_X;
                entity.Y = entity_Y;
                entity.Z = entity_Z;

                PosImage(image, entity_X, entity_Y, PlayerRot, PlayerX, PlayerY, Scale);
            }
            catch
            {
                PosImage(image, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY, Scale);
            }
            try
            {
                /*if (image.IsMouseDirectlyOver)
                {
                    in_Win_Radar_Popup.PopupContent(entity);
                    in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(image) - in_Win_Radar_Popup.Width;
                    in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(image) - in_Win_Radar_Popup.Height;
                    ID_Focus = entity.ID;
                    Image_Focus = true;
                }*/
            }
            catch
            { }
        }

        private void PosImage(Image image, float PosX, float PosY, float PlayerRot, float PlayerX, float PlayerY)
        {
            if (image == null || image.Source == null)
                return;

            double Diff_X;
            double Diff_Y;

            double cx = (PosX - PlayerX) / -2;
            double cy = (PosY - PlayerY) / -2;

            float zoom = in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
            float size = in_Win_Main.in_Setting.in_Radar.Size.Get_Value();

            Diff_X = ((cx * cosr) + (cy * sinr)) * zoom;
            Diff_Y = ((cx * sinr) - (cy * cosr)) * zoom;

            image.Width = image.Source.Width * size;
            image.Height = image.Source.Height * size;

            Diff_X = Diff_X + Canvas_Radar.Width / 2 - image.Width / 2;
            Diff_Y = Diff_Y + Canvas_Radar.Height / 2 - image.Height / 2;

            Canvas.SetLeft(image, Diff_X);
            Canvas.SetTop(image, Diff_Y);
        }

        private void PosImage(Image image, float PosX, float PosY, float PlayerRot, float PlayerX, float PlayerY, float Scale)
        {
            if (image == null || image.Source == null)
                return;

            double Diff_X;
            double Diff_Y;

            double cx = (PosX - PlayerX) / -2;
            double cy = (PosY - PlayerY) / -2;

            float zoom = in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();

            Diff_X = ((cx * cosr) + (cy * sinr)) * zoom;
            Diff_Y = ((cx * sinr) - (cy * cosr)) * zoom;

            image.Width = image.Source.Width * Scale;
            image.Height = image.Source.Height * Scale;

            Diff_X = Diff_X + Canvas_Radar.Width / 2 - image.Width / 2;
            Diff_Y = Diff_Y + Canvas_Radar.Height / 2 - image.Height / 2;

            Canvas.SetLeft(image, Diff_X);
            Canvas.SetTop(image, Diff_Y);
        }

        private bool IsAggro(long nodeStatus)
        {
            try
            {
                if (SplMemory.ReadInt(nodeStatus + Offset.Status.TargetId) == Aion_Game.Player.Id)
                    return true;
                else
                    return false;
            }
            catch
            { return false; }
        }
    }
}
