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
using NS_Aion_Game;
using _Threads;
using NS_Windows_And_Process;

namespace BobyMultitools
{
    public partial class Win_Radar
    {
        public static Image[] Img_Entity_Index_1 = new Image[300];
        public static Image[] Img_Entity_Index_2 = new Image[300];
        public static Image[] Img_Entity_Index_3 = new Image[300];
        public static Image[] Img_Entity_Index_4 = new Image[300];
        public static Image[] Img_Entity_Index_5 = new Image[300];
        public static Image[] Img_Entity_Index_6 = new Image[300];
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

        public static bool Image_Focus = false;
        public static int  ID_Focus = 0;
        public static int  ID_Target_Player = 0;

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

            DeclareMultiImage(Img_Entity_Index_1);
            DeclareMultiImage(Img_Entity_Index_2);
            DeclareMultiImage(Img_Entity_Index_3);
            DeclareMultiImage(Img_Entity_Index_4);
            DeclareMultiImage(Img_Entity_Index_5);
            DeclareMultiImage(Img_Entity_Index_6);

            Target.Style = in_Win_Main.Style_Radar_Target;
            Canvas_Radar.Children.Add(Target);

            t150 = new TimeSpan(0, 0, 0, 0, 150);
            t1 = new TimeSpan(0, 0, 0, 0, 1);

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

        public void DeclareMultiImage(Image[] image)
        {
            for (int i = 0; i < 300; i++)
            {
                image[i] = new Image();
                Canvas_Radar.Children.Add(image[i]);
                Canvas.SetLeft(image[i], -100);
                Canvas.SetTop(image[i], -100);
            }
        }

        public void HideUntilImage(Image[] image, int compteur)
        {
            for (int i = 300 - 1; i >= compteur; i--)
            {
                image[i].Source = null;
                Canvas.SetLeft(image[i], -100);
                Canvas.SetTop(image[i], -100);
            }
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            Radar_Image_To_Canvas_();      
        }

        private void Radar_Image_To_Canvas_()
        {
            try
            {
                if (this.IsVisible == true)
                {
                    int count_entity_index_1 = 0;
                    int count_entity_index_2 = 0;
                    int count_entity_index_3 = 0;
                    int count_entity_index_4 = 0;
                    int count_entity_index_5 = 0;
                    int count_entity_index_6 = 0;

                    Image_Focus = false;

                    string tWhere = SplMemory.ReadWchar(Aion_Game.Modules.Game + Offset.Game.Where, 60);

                    float PlayerX = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.X);
                    float PlayerY = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Y);

                    bool Player_IsFly = SplMemory.ReadByte(Aion_Game.Modules.Game + Offset.Player.IsFly) == 1;

                    float PlayerRot = 0;

                    if (SplMemory.ReadInt(Aion_Game.Modules.Game + Offset.Player.FreeCamRot) != 0)
                        PlayerRot = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.FreeCamRotH);
                    else
                        PlayerRot = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.CamRotH);

                    if (PlayerRot > 180)
                        PlayerRot = PlayerRot - 360;

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

                    foreach (var entity in in_Win_Main.in_Thread_Entity.DicCopy.Values)
                    {
                        if ((entity._Image != null || entity._Image_Object != null || entity._Image_Where != null || entity._Icon != null || entity.Type == EnumAion.eType.Player))
                        {
                            int ID = 0;

                            try
                            {
                                ID = SplMemory.ReadInt(SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Status) + Offset.Status.ID);
                            }
                            catch
                            { }

                            if (ID == entity.ID)
                            {
                                //entity.Update();

                                if (ID_Target_Player == entity.ID
                                    && ID_Target_Player != Thread_Entity.ePlayer.ID
                                    && ID_Target_Player != 0)
                                {
                                    if (entity.Type == EnumAion.eType.Player || entity.DistanceReal > 120)
                                    {
                                        Canvas.SetLeft(Target, -100);
                                        Canvas.SetTop(Target, -100);
                                    }
                                    else
                                    {
                                        PosImage(Target, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY);
                                        if (Target.IsMouseDirectlyOver)
                                        {
                                            in_Win_Radar_Popup.PopupContent(entity);
                                            in_Win_Radar_Popup.Opacity = 1d;
                                            in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(Target) - in_Win_Radar_Popup.Width;
                                            in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(Target) - in_Win_Radar_Popup.Height;
                                            Image_Focus = true;
                                        }
                                    }
                                }
                                else if (IsAggro(entity) &&
                                 (c_NPC.IsChecked && entity.Type == EnumAion.eType.NPC ||
                                  c_Ally.IsChecked && entity.Type == EnumAion.eType.User && entity.Race == EnumAion.eAttitude.Friendly ||
                                  c_Ennemy.IsChecked && entity.Type == EnumAion.eType.User && entity.Race != EnumAion.eAttitude.Friendly
                                 )
                                 && entity.HP != 0 && entity.HP_Percent != 0
                                 && count_entity_index_1 < 300)
                                {
                                    ImageOnRadar(Img_Entity_Index_1[count_entity_index_1], Aggro, entity, PlayerRot, PlayerX, PlayerY, 0.6f);
                                    count_entity_index_1++;
                                }

                                if (entity.Type == EnumAion.eType.Player)
                                {
                                    float RotPlayer = 0;
                                    try
                                    {
                                        if (entity.Link != 0)
                                            RotPlayer = SplMemory.ReadFloat(SplMemory.ReadLong(SplMemory.ReadLong(entity.Link + Offset.Status.Node) + Offset.Entity.Status) + Offset.Status.Rot);
                                        else
                                            RotPlayer = SplMemory.ReadFloat(SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Status) + Offset.Status.Rot);
                                        ID_Target_Player = SplMemory.ReadInt(SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Status) + Offset.Status.TargetId);
                                    }
                                    catch { }

                                    float Rot = PlayerRot;
                                    if (c_North.IsChecked)
                                        Rot = 270;

                                    Player.RenderTransform = new RotateTransform((RotPlayer - Rot) * -1, Player.ActualWidth / 2, Player.ActualHeight / 2);
                                }
                                else if (entity._Icon != null && (entity.HP != 0 || entity.Type == EnumAion.eType.Gather) && entity.Stance != EnumAion.eStance.Dead)
                                {
                                    if (count_entity_index_6 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_6[count_entity_index_6], entity._Icon.IMG_SRC, entity, PlayerRot, PlayerX, PlayerY, entity._Icon.SCALE);
                                        count_entity_index_6++;
                                    }
                                    if (entity._Icon.AGGRO_SCALE > 0 && count_entity_index_1 < 300)
                                    {
                                        float scale = entity._Icon.AGGRO_SCALE / 16 * in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() / 2.65f;
                                        ImageOnRadar(Img_Entity_Index_1[count_entity_index_1], Signal_Aggro, entity, PlayerRot, PlayerX, PlayerY, scale);
                                        count_entity_index_1++;
                                    }
                                }
                                else if (entity._Image_Where != null)
                                {
                                    if (count_entity_index_5 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_5[count_entity_index_5], entity._Image_Where, entity, PlayerRot, PlayerX, PlayerY);
                                        count_entity_index_5++;
                                    }
                                }
                                else if (c_Gather.IsChecked && entity.Type == EnumAion.eType.Gather)
                                {
                                    if (count_entity_index_1 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_1[count_entity_index_1], entity._Image, entity, PlayerRot, PlayerX, PlayerY);
                                        count_entity_index_1++;
                                    }
                                }
                                else if (c_NPC.IsChecked && (entity.Type == EnumAion.eType.Pet || entity.Type == EnumAion.eType.NPC || entity.Type == EnumAion.eType.PlaceableObject))
                                {
                                    if (entity._Image_Object != null)
                                    {
                                        if (count_entity_index_3 < 300)
                                        {
                                            ImageOnRadar(Img_Entity_Index_3[count_entity_index_3], entity._Image_Object, entity, PlayerRot, PlayerX, PlayerY);
                                            count_entity_index_3++;
                                        }
                                    }
                                    else
                                    {
                                        if (count_entity_index_1 < 300)
                                        {
                                            ImageOnRadar(Img_Entity_Index_1[count_entity_index_1], entity._Image, entity, PlayerRot, PlayerX, PlayerY);
                                            count_entity_index_1++;
                                        }
                                    }
                                }
                                else if (entity.Type == EnumAion.eType.User && entity.Race == EnumAion.eAttitude.Friendly && (entity.Group || entity.Force))
                                {
                                    if (count_entity_index_5 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_5[count_entity_index_5], entity._Image, entity, PlayerRot, PlayerX, PlayerY);
                                        count_entity_index_5++;
                                    }
                                }
                                else if (c_Ally.IsChecked && entity.Type == EnumAion.eType.User && entity.Race == EnumAion.eAttitude.Friendly)
                                {
                                    if (count_entity_index_2 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_2[count_entity_index_2], entity._Image, entity, PlayerRot, PlayerX, PlayerY);
                                        count_entity_index_2++;
                                    }
                                }
                                else if (c_Ennemy.IsChecked && entity.Type == EnumAion.eType.User && entity.Race != EnumAion.eAttitude.Friendly)
                                {
                                    if (count_entity_index_4 < 300)
                                    {
                                        ImageOnRadar(Img_Entity_Index_4[count_entity_index_4], entity._Image, entity, PlayerRot, PlayerX, PlayerY);
                                        count_entity_index_4++;
                                    }
                                }
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

                    HideUntilImage(Img_Entity_Index_1, count_entity_index_1);
                    HideUntilImage(Img_Entity_Index_2, count_entity_index_2);
                    HideUntilImage(Img_Entity_Index_3, count_entity_index_3);
                    HideUntilImage(Img_Entity_Index_4, count_entity_index_4);
                    HideUntilImage(Img_Entity_Index_5, count_entity_index_5);
                    HideUntilImage(Img_Entity_Index_6, count_entity_index_6);

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
            }
            catch { }
        }

        void ImageOnRadar(Image image, ImageSource source, Entity entity, float PlayerRot, float PlayerX, float PlayerY)
        {
            if (source == null)
                return;

            image.Source = source;

            try
            {
                long EntityLoc = SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Loc);
                float entity_X = SplMemory.ReadFloat(EntityLoc + Offset.Loc.X);
                float entity_Y = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Y);
                float entity_Z = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Z);

                entity.X = entity_X;
                entity.Y = entity_Y;
                entity.Z = entity_Z;

                PosImage(image, entity_X, entity_Y, PlayerRot, PlayerX, PlayerY);
            }
            catch
            {
                PosImage(image, entity.X, entity.Y, PlayerRot, PlayerX, PlayerY);
            }
            try
            {
                if (image.IsMouseDirectlyOver)
                {
                    in_Win_Radar_Popup.PopupContent(entity);
                    in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(image) - in_Win_Radar_Popup.Width;
                    in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(image) - in_Win_Radar_Popup.Height;
                    ID_Focus = entity.ID;
                    Image_Focus = true;
                }
            }
            catch
            { }
        }

        void ImageOnRadar(Image image, ImageSource source, Entity entity, float PlayerRot, float PlayerX, float PlayerY, float Scale)
        {
            if (source == null)
                return;
            
            image.Source = source;

            try
            {
                long EntityLoc = SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Loc);
                float entity_X = SplMemory.ReadFloat(EntityLoc + Offset.Loc.X);
                float entity_Y = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Y);
                float entity_Z = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Z);

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
                if (image.IsMouseDirectlyOver)
                {
                    in_Win_Radar_Popup.PopupContent(entity);
                    in_Win_Radar_Popup.Left = this.Left + Canvas.GetLeft(image) - in_Win_Radar_Popup.Width;
                    in_Win_Radar_Popup.Top = this.Top + Canvas.GetTop(image) - in_Win_Radar_Popup.Height;
                    ID_Focus = entity.ID;
                    Image_Focus = true;
                }
            }
            catch
            { }
        }

        private void PosImage(Image image, float PosX, float PosY, float PlayerRot, float PlayerX, float PlayerY)
        {
            if (image != null && image.Source != null)
            {
                double Diff_X;
                double Diff_Y;
                float Angle_X = PlayerRot;
                if (c_North.IsChecked) Angle_X = -90;

                if (Angle_X > 0 && Angle_X < 180)
                    Angle_X = Angle_X * -1;
                else if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = (360 - Math.Abs(Angle_X)) * -1;

                double Diff_X2 = PosX - PlayerX;
                double Diff_Y2 = PosY - PlayerY;

                double radian = Angle_X * (3.14 / 180) * -1;

                double cx = (PosX - PlayerX) / 2 + PlayerX;
                double cy = (PosY - PlayerY) / 2 + PlayerY;

                double sinr = Math.Sin(radian);
                double cosr = Math.Cos(radian);

                cx = PlayerX - cx;
                cy = PlayerY - cy;

                Diff_X = ((cx * cosr) + (cy * sinr));
                Diff_X *= in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                Diff_Y = ((cx * sinr) - (cy * cosr));
                Diff_Y *= in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();

                image.Width = image.Source.Width * in_Win_Main.in_Setting.in_Radar.Size.Get_Value();
                image.Height = image.Source.Height * in_Win_Main.in_Setting.in_Radar.Size.Get_Value();

                Diff_X = Diff_X + Canvas_Radar.Width / 2 - image.Width / 2;
                Diff_Y = Diff_Y + Canvas_Radar.Height / 2 - image.Height / 2;

                Canvas.SetLeft(image, Diff_X);
                Canvas.SetTop(image, Diff_Y);
            }
        }

        private void PosImage(Image image, float PosX, float PosY, float PlayerRot, float PlayerX, float PlayerY, float Scale)
        {
            if (image != null && image.Source != null)
            {
                double Diff_X;
                double Diff_Y;
                float Angle_X = PlayerRot;
                if (c_North.IsChecked) Angle_X = -90;

                if (Angle_X > 0 && Angle_X < 180)
                    Angle_X = Angle_X * -1;
                else if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = (360 - Math.Abs(Angle_X)) * -1;

                double Diff_X2 = PosX - PlayerX;
                double Diff_Y2 = PosY - PlayerY;

                double radian = Angle_X * (3.14 / 180) * -1;

                double cx = (PosX - PlayerX) / 2 + PlayerX;
                double cy = (PosY - PlayerY) / 2 + PlayerY;

                double sinr = Math.Sin(radian);
                double cosr = Math.Cos(radian);

                cx = PlayerX - cx;
                cy = PlayerY - cy;

                Diff_X = ((cx * cosr) + (cy * sinr));
                Diff_X *= in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();
                Diff_Y = ((cx * sinr) - (cy * cosr));
                Diff_Y *= in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value();

                image.Width = image.Source.Width * Scale;
                image.Height = image.Source.Height * Scale;

                Diff_X = Diff_X + Canvas_Radar.Width / 2 - image.Width / 2;
                Diff_Y = Diff_Y + Canvas_Radar.Height / 2 - image.Height / 2;

                Canvas.SetLeft(image, Diff_X);
                Canvas.SetTop(image, Diff_Y);
            }
        }

        private bool IsAggro(Entity entity)
        {
            try
            {
                if (SplMemory.ReadInt(entity._PtrEntity + Offset.Status.TargetId) == Thread_Entity.ePlayer.ID)
                    return true;
                else
                    return false;
            }
            catch
            { return false; }
        }
    }
}
