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
using System.Runtime.InteropServices; // DllImport

using MemoryLib;
using Aion_Process;
using Aion_Game;
using Windows_And_Process;

namespace BobyMultitools
{
    public partial class Win_Cheat
    {
        #region DllImport
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        #endregion

        DispatcherTimer messageTimer_1;
        DispatcherTimer messageTimer_2;
        DispatcherTimer messageTimer_3;

        private void Cheat_Sequence()
        {
            /*Thread T_Stats = new Thread(Sequence_1);
            T_Stats.SetApartmentState(ApartmentState.STA);
            T_Stats.Start();
            Thread T_Keys = new Thread(Sequence_2);
            T_Keys.SetApartmentState(ApartmentState.STA);
            T_Keys.Start();
            Thread T_Boss = new Thread(Sequence_3);
            T_Boss.SetApartmentState(ApartmentState.STA);
            T_Boss.Start();*/
            Sequence_1();
            Sequence_2();
            Sequence_3();
        }

        private void Sequence_1()
        {
            messageTimer_1 = new DispatcherTimer();
            messageTimer_1.Tick += new EventHandler(_Sequence_1);
            messageTimer_1.Interval = new TimeSpan(0, 0, 0, 0, 20);
            messageTimer_1.Start();
            //System.Windows.Threading.Dispatcher.Run();
        }

        private void Sequence_2()
        {
            messageTimer_2 = new DispatcherTimer();
            messageTimer_2.Tick += new EventHandler(_Sequence_2);
            messageTimer_2.Interval = new TimeSpan(0, 0, 0, 0, 20);
            messageTimer_2.Start();
            //System.Windows.Threading.Dispatcher.Run();
        }

        private void Sequence_3()
        {
            messageTimer_3 = new DispatcherTimer();
            messageTimer_3.Tick += new EventHandler(_Sequence_3);
            messageTimer_3.Interval = new TimeSpan(0, 0, 0, 0, 900);
            messageTimer_3.Start();
            //System.Windows.Threading.Dispatcher.Run();
        }

        static long s_PlayerPtr = 0;
        static long s_Link_PlayerPtr = 0;
        static bool s_Safety = false;
        static bool s_Key = false;
        static bool s_No_Grav = false;
        static bool s_ZLock = false;

        static double s_Acc_dst = 0;
        static double s_Sup_dst = 0;

        static float tmp_ZLock = -1;

        static int v_Atk_Sspeed = 0;

        /*
        **  STATS
        */

        static bool If_User()
        {
            return SplMemory.ReadInt(SplMemory.ReadLong(Game.Base + Offset.EntityList.Map) + Offset.EntityList.UserCount) > 1;
        }

        static bool Is_Player(long addr)
        {
            return (Aion_Game.eType)SplMemory.ReadByte(addr + Offset.Entity.Type) == Aion_Game.eType.Player;
        }

        static long FindPlayerPtr()
        {
            try
            {
                foreach (var entity in Aion_Game.EntityList.uList.Values)
                {
                    if (entity.Type == Aion_Game.eType.Player)
                        return entity.Node;
                }
            }
            catch (Exception)
            {

            }
            return 0;
        }

        void _Sequence_1(object sender, EventArgs e)
        {
            try
            {
                if (!Setting.Boby.Cheat.Show)
                    return;

                s_Safety = (cb_Safety.IsChecked == true) && If_User();
                s_Key = cb_Key.IsChecked == true;
                s_No_Grav = cb_NoGrav.IsChecked == true;
                s_ZLock = cb_ZLock.IsChecked == true;

                v_Atk_Sspeed = (int)sl_Atk.Value;

                if (!Player.IsPlayer())
                    return;

                double ModPosZ = (double)Player.Z;
                int AtkSpeedBase = SplMemory.ReadInt(Game.Base + Offset.Player.Atk_Speed);
                int AtkSpeed = (int)Player.AtkSpeed - (int)Player.AtkSpeed * v_Atk_Sspeed / 100;
                float MoveSpeedBase = SplMemory.ReadFloat(Game.Base + Offset.Player.Mov_Speed);
                float MoveSpeed = MoveSpeedBase + MoveSpeedBase * 1 / 100; // add 1% run

                //SplMemory.WriteWchar(PlayerEntity + Offset.Status.Info, Aion_Game.Player.entity.X.ToString("0.00") + " " + Aion_Game.Player.Y.ToString("0.00") + " " + Aion_Game.Player.Z.ToString("0.00"), 30);

                if (AtkSpeed == 0)
                    AtkSpeed = 1;

                if (s_Safety && AtkSpeed < (Player.AtkSpeed - Player.AtkSpeed * 25 / 100))
                {
                    //lb_val_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FF555555");
                    //lb_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FF555555");
                    Player.AtkSpeed = Player.AtkSpeed - Player.AtkSpeed * 25 / 100;
                    lb_val_atk_speed.Content = "25 %";
                }
                else
                {
                    Player.AtkSpeed = AtkSpeed;
                    //lb_val_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FFEFEDC4");
                    //lb_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FFEFEDC4");
                    lb_val_atk_speed.Content = sl_Atk.Value.ToString() + " %";
                }

                Player.MoveSpeed = MoveSpeed;

                if (Player.NoGrav == 7)
                {
                    // Plane
                    if (s_ZLock)
                    {
                        if (tmp_ZLock == -1)
                            tmp_ZLock = (float)Player.Z;
                        if (Player.Z != tmp_ZLock)
                        {
                            if (!s_Safety)
                            {
                                float tmp_plan = Player.ZCollision;
                                if (tmp_plan < 1.0f)
                                    tmp_ZLock += (1.0f - tmp_plan);
                                Player.Z = tmp_ZLock;
                            }
                        }
                    }
                    else
                        tmp_ZLock = -1;
                }
                else
                {
                    // No Plane
                    tmp_ZLock = -1;
                    if (Player.IsFly)
                    {
                        Player.NoGrav = 5;
                        return;
                    }
                    if (s_No_Grav)
                    {
                        if (s_Safety)
                        {
                            Player.NoGrav = 0;
                            Player.NoSkill = 0;
                        }
                        else
                        {
                            Player.NoGrav = 5;
                            Player.NoSkill = 0;
                        }
                    }
                    else if (Player.NoGrav != 7)
                        Player.NoGrav = 0;
                }
                /*
                if (SplMemory.ReadByte(PlayerEntity + Offset.Entity.No_Grav) != 7)
                {
                    tmp_ZLock = -1;
                    if (SplMemory.ReadByte(Game.Base + Offset.Player.IsFly) == 0)
                    {
                        if (s_No_Grav)
                        {
                            if (s_Safety)
                            {
                                SplMemory.WriteMemory(PlayerEntity + Offset.Entity.No_Grav, 0);
                                SplMemory.WriteMemory(SplMemory.ReadLong(s_PlayerPtr + Offset.Entity.Status) + Offset.Entity.No_Skill, 0);
                            }
                            else
                            {
                                SplMemory.WriteMemory(PlayerEntity + Offset.Entity.No_Grav, 5);
                                SplMemory.WriteMemory(SplMemory.ReadLong(s_PlayerPtr + Offset.Entity.Status) + Offset.Entity.No_Skill, 0);
                            }
                        }
                        else if (SplMemory.ReadByte(Game.Base + Offset.Player.IsFly) == 0 && SplMemory.ReadByte(PlayerEntity + Offset.Entity.No_Grav) != 7)
                            SplMemory.WriteMemory(PlayerEntity + Offset.Entity.No_Grav, 0);
                    }
                }
                else
                {
                    if (s_ZLock)
                    {
                        if (tmp_ZLock == -1)
                            tmp_ZLock = (float)ModPosZ;
                        if (ModPosZ != tmp_ZLock)
                        {
                            if (!s_Safety)
                            {
                                float tmp_plan = SplMemory.ReadFloat(PlayerEntity + Offset.Entity.Zplan);
                                if (tmp_plan < 1.0f)
                                    tmp_ZLock += (1.0f - tmp_plan);
                                SplMemory.WriteMemory(PtrPlayerLoc + Offset.Entity.Z, tmp_ZLock);
                            }
                        }
                    }
                    else
                        tmp_ZLock = -1;
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
        **  KEYS
        */

        bool KeyIsPress(int modifier, int key)
        {
            if (key == 0)
                return false;

            bool modifierIsPress = false;

            switch (modifier)
            {
                case 0:
                    modifierIsPress = true;
                    break;
                case 1://alt
                    if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
                        modifierIsPress = true;
                    break;
                case 2://ctrl
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                        modifierIsPress = true;
                    break;
                case 3://alt+ctrl
                    if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0 &&
                        (Keyboard.Modifiers & ModifierKeys.Control) != 0)
                        modifierIsPress = true;
                    break;
                case 4://shift
                    if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                        modifierIsPress = true;
                    break;
                case 5://alt+shift
                    if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0 &&
                        (Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                        modifierIsPress = true;
                    break;
                case 6://ctrl+shift
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != 0 &&
                        (Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                        modifierIsPress = true;
                    break;
                case 7://alt+ctrl+shift
                    if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0 &&
                        (Keyboard.Modifiers & ModifierKeys.Control) != 0 &&
                        (Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                        modifierIsPress = true;
                    break;
            }

            string keyStr = ((Key)key).ToString();

            if (modifierIsPress && Keyboard.IsKeyDown((Key)Enum.Parse(typeof(Key), keyStr)))
                return true;
            else
                return false;
        }

        bool WindowsIs()
        {
            bool isactive = false;
            if (Game.Whandle != IntPtr.Zero)
            {
                IntPtr MWhandle = Game.Whandle;
                IntPtr CWhandle = GetForegroundWindow();
                if (MWhandle == CWhandle)
                    isactive = true;
            }
            return isactive;
        }

        public void MoveCheat(string Move)
        {
            try
            {
                s_Safety = (cb_Safety.IsChecked == true) && If_User();
                s_Key = cb_Key.IsChecked == true;
                s_No_Grav = cb_NoGrav.IsChecked == true;
                s_ZLock = cb_ZLock.IsChecked == true;

                s_Acc_dst = in_Win_Cheat_Setting.sl_acc_dist.Value;
                s_Sup_dst = in_Win_Cheat_Setting.sl_Sup_dist.Value;

                v_Atk_Sspeed = (int)sl_Atk.Value;

                if (!Player.IsPlayer() || s_Safety)
                    return;

                double ModPosX = (double)Player.X;
                double ModPosY = (double)Player.Y;
                double ModPosZ = (double)Player.Z;
                double CamRotH = (double)SplMemory.ReadFloat(Game.Base + Offset.Player.CamRot_H) / 180d;
                float PosX;
                float PosY;
                float PosZ;

                switch (Move)
                {
                    case "ACCFOR":
                        PosX = (float)ModPosX - (float)((s_Acc_dst + 1) / 100d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                        PosY = (float)ModPosY + (float)((s_Acc_dst + 1) / 100d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                        Player.X = PosX;
                        Player.Y = PosY;
                        break;
                    case "ACCUP":
                        PosZ = (float)ModPosZ + (float)((s_Acc_dst + 1) / 100d);
                        tmp_ZLock = tmp_ZLock + (float)((s_Acc_dst + 1) / 100d);
                        Player.Z = PosZ;
                        break;
                    case "ACCDOWN":
                        PosZ = (float)ModPosZ - (float)((s_Acc_dst + 1) / 100d);
                        tmp_ZLock = tmp_ZLock - (float)((s_Acc_dst + 1) / 100d);
                        Player.Z = PosZ;
                        break;
                    case "SUPFOR":
                        PosX = (float)ModPosX - (float)((s_Sup_dst + 1) / 10d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                        PosY = (float)ModPosY + (float)((s_Sup_dst + 1) / 10d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                        Player.X = PosX;
                        Player.Y = PosY;
                        break;
                    case "SUPUP":
                        PosZ = (float)ModPosZ + (float)((s_Sup_dst + 1) / 10d);
                        tmp_ZLock = tmp_ZLock + (float)((s_Sup_dst + 1) / 10d);
                        Player.Z = PosZ;
                        break;
                    case "SUPDOWN":
                        PosZ = (float)ModPosZ - (float)((s_Sup_dst + 1) / 10d);
                        tmp_ZLock = tmp_ZLock - (float)((s_Sup_dst + 1) / 10d);
                        Player.Z = PosZ;
                        break;
                }
            }
            catch { }
        }

        void _Sequence_2(object sender, EventArgs e)
        {
            if (Setting.Boby.Cheat.Show && WindowsIs())
            {
                bool cb_Key_IsChecked = false;
                cb_Key_IsChecked = (bool)cb_Key.IsChecked;
                if (KeyIsPress(Setting.Boby.CheatKey.modifierToKey, Setting.Boby.CheatKey.keyToKey))
                {
                    cb_Key.IsChecked = !cb_Key.IsChecked;
                    while (KeyIsPress(Setting.Boby.CheatKey.modifierToKey, Setting.Boby.CheatKey.keyToKey))
                        Thread.Sleep(20);
                }
                if (!cb_Key_IsChecked)
                    return;
                if (KeyIsPress(Setting.Boby.CheatKey.modifierNoGrav, Setting.Boby.CheatKey.keyNoGrav))
                {
                    cb_NoGrav.IsChecked = !cb_NoGrav.IsChecked;
                    while (KeyIsPress(Setting.Boby.CheatKey.modifierNoGrav, Setting.Boby.CheatKey.keyNoGrav))
                        Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierZLock, Setting.Boby.CheatKey.keyZLock))
                {
                    cb_ZLock.IsChecked = !cb_ZLock.IsChecked;
                    while (KeyIsPress(Setting.Boby.CheatKey.modifierZLock, Setting.Boby.CheatKey.keyZLock))
                        Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierAccFor, Setting.Boby.CheatKey.keyAccFor))
                {
                    MoveCheat("ACCFOR");
                    Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierAccUp, Setting.Boby.CheatKey.keyAccUp))
                {
                    MoveCheat("ACCUP");
                    Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierAccDown, Setting.Boby.CheatKey.keyAccDown))
                {
                    MoveCheat("ACCDOWN");
                    Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierSupFor, Setting.Boby.CheatKey.keySupFor))
                {
                    MoveCheat("SUPFOR");
                    Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierSupUp, Setting.Boby.CheatKey.keySupUp))
                {
                    MoveCheat("SUPUP");
                    Thread.Sleep(20);
                }
                else if (KeyIsPress(Setting.Boby.CheatKey.modifierSupDown, Setting.Boby.CheatKey.keySupDown))
                {
                    MoveCheat("SUPDOWN");
                    Thread.Sleep(20);
                }
            }
        }

        /*
        **  BOSS
        */
        void _Sequence_3(object sender, EventArgs e)
        {
            if (Setting.Boby.Cheat.Show && WindowsIs())
            {

            }
        }
    }
}
