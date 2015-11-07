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

        static byte Get_Target_type(long addr)
        {
            long ID_Target_Player = SplMemory.ReadLong(addr + Offset.Status.TargetId);
            long PTR_Target = SplMemory.ReadLong(Game.Base + Offset.Entity.To_Target);
            if (PTR_Target != 0 && ID_Target_Player != 0)
            {
                long ID_Target = SplMemory.ReadLong(SplMemory.ReadLong(PTR_Target + Offset.Entity.Status) + Offset.Status.ID);
                byte TYPE_Target = SplMemory.ReadByte(PTR_Target + Offset.Entity.Type);
                if (ID_Target_Player == ID_Target)
                    return TYPE_Target;
            }
            return 0;
        }

        static bool If_User()
        {
            return SplMemory.ReadInt(SplMemory.ReadLong(Game.Base + Offset.EntityList.Pointer) + Offset.EntityList.User) > 1;
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

        static long Get_Link_Player(long addr)
        {
            long tmp = addr;
            if (SplMemory.ReadLong(SplMemory.ReadLong(tmp + Offset.Entity.Status) + Offset.Status.Link) != 0)
                tmp = SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(tmp + Offset.Entity.Status) + Offset.Status.Link) + Offset.Status.Node);
            return tmp;
        }

        void _Sequence_1(object sender, EventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Cheat.Show.Get_Value())
            {
                try
                {
                    s_Safety = (cb_Safety.IsChecked == true) && If_User();
                    s_Key = cb_Key.IsChecked == true;
                    s_No_Grav = cb_NoGrav.IsChecked == true;
                    s_ZLock = cb_ZLock.IsChecked == true;

                    v_Atk_Sspeed = (int)sl_Atk.Value;

                    if (!Is_Player(s_PlayerPtr))
                    {
                        if ((s_PlayerPtr = FindPlayerPtr()) == 0)
                            return; // Player not found
                    }

                    s_Link_PlayerPtr = Get_Link_Player(s_PlayerPtr);

                    if (s_Link_PlayerPtr == 0)
                        return; // Link not found

                    long    PlayerEntity   = SplMemory.ReadLong(s_PlayerPtr + Offset.Entity.Status);
                    long    PtrPlayerLoc   = SplMemory.ReadLong(s_Link_PlayerPtr + Offset.Entity.Loc);
                    double  ModPosZ        = (double)SplMemory.ReadFloat(PtrPlayerLoc + Offset.Loc.Z);
                    int     AtkSpeedBase   = SplMemory.ReadInt(Game.Base + Offset.Player.AtkSpeed);
                    int     AtkSpeed       = AtkSpeedBase - AtkSpeedBase * v_Atk_Sspeed / 100;
                    float   MoveSpeedBase  = SplMemory.ReadFloat(Game.Base + Offset.Player.MoveSpeed);
                    float   MoveSpeed      = MoveSpeedBase + MoveSpeedBase * 1 / 100; // add 1% run

                    //SplMemory.WriteWchar(PlayerEntity + Offset.Status.Info, Aion_Game.Player.entity.X.ToString("0.00") + " " + Aion_Game.Player.Y.ToString("0.00") + " " + Aion_Game.Player.Z.ToString("0.00"), 30);

                    if (AtkSpeed == 0)
                        AtkSpeed = 1;

                    if ((s_Safety || Get_Target_type(PlayerEntity) == eType.User) && Environment.GetCommandLineArgs().Length == 1 && AtkSpeed < (AtkSpeedBase - AtkSpeedBase * 25 / 100))
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            lb_val_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FF555555");
                            lb_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FF555555");
                            SplMemory.WriteMemory(PlayerEntity + Offset.Status.AtkSpeed, AtkSpeedBase - AtkSpeedBase * 25 / 100);
                            lb_val_atk_speed.Content = "25 %";
                        }));
                    }
                    else
                    {
                        SplMemory.WriteMemory(PlayerEntity + Offset.Status.AtkSpeed, AtkSpeed);
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            lb_val_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FFEFEDC4");
                            lb_atk_speed.Foreground = in_Win_Main.in_Win_Entity.ToBrush("#FFEFEDC4");
                            lb_val_atk_speed.Content = sl_Atk.Value.ToString() + " %";
                        }));
                    }

                    SplMemory.WriteMemory(PlayerEntity + Offset.Status.MoveSpeed, MoveSpeed);

                    if (Get_Link_Player(s_PlayerPtr) != 0)
                        PlayerEntity = SplMemory.ReadLong(s_Link_PlayerPtr + Offset.Entity.Status);
                    if (SplMemory.ReadByte(PlayerEntity + Offset.Status.No_Grav) != 7)
                    {
                        tmp_ZLock = -1;
                        if (SplMemory.ReadByte(Game.Base + Offset.Player.IsFly) == 0)
                        {
                            if (s_No_Grav)
                            {
                                if (s_Safety)
                                {
                                    SplMemory.WriteMemory(PlayerEntity + Offset.Status.No_Grav, 0);
                                    SplMemory.WriteMemory(SplMemory.ReadLong(s_PlayerPtr + Offset.Entity.Status) + Offset.Status.No_Skill, 0);
                                }
                                else
                                {
                                    SplMemory.WriteMemory(PlayerEntity + Offset.Status.No_Grav, 5);
                                    SplMemory.WriteMemory(SplMemory.ReadLong(s_PlayerPtr + Offset.Entity.Status) + Offset.Status.No_Skill, 0);
                                }
                            }
                            else if (SplMemory.ReadByte(Game.Base + Offset.Player.IsFly) == 0 && SplMemory.ReadByte(PlayerEntity + Offset.Status.No_Grav) != 7)
                                SplMemory.WriteMemory(PlayerEntity + Offset.Status.No_Grav, 0);
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
                                    float tmp_plan = SplMemory.ReadFloat(PlayerEntity + Offset.Loc.Zplan);
                                    if (tmp_plan < 1.0f)
                                        tmp_ZLock += (1.0f - tmp_plan);
                                    SplMemory.WriteMemory(PtrPlayerLoc + Offset.Loc.Z, tmp_ZLock);
                                }
                            }
                        }
                        else
                            tmp_ZLock = -1;
                    }
                }
                catch
                { }
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

                if (s_Link_PlayerPtr != 0 && !s_Safety)
                {
                    long PtrPlayerLoc	= SplMemory.ReadLong(s_Link_PlayerPtr + (long)Offset.Entity.Loc);
                    double ModPosX 		= (double)SplMemory.ReadFloat(PtrPlayerLoc + (long)Offset.Loc.X);
                    double ModPosY 		= (double)SplMemory.ReadFloat(PtrPlayerLoc + (long)Offset.Loc.Y);
                    double ModPosZ 		= (double)SplMemory.ReadFloat(PtrPlayerLoc + (long)Offset.Loc.Z);
                    double CamRotH 		= (double)SplMemory.ReadFloat(Game.Base + (long)Offset.Player.CamRotH) / 180d;
                    float PosX;
                    float PosY;
                    float PosZ;
                    
                    switch (Move)
                    {
                        case "ACCFOR":
                            PosX = (float)ModPosX - (float)((s_Acc_dst + 1) / 100d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                            PosY = (float)ModPosY + (float)((s_Acc_dst + 1) / 100d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.X, PosX);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Y, PosY);
                            break;
                        case "ACCUP":
                            PosZ = (float)ModPosZ + (float)((s_Acc_dst + 1) / 100d);
                            tmp_ZLock = tmp_ZLock + (float)((s_Acc_dst + 1) / 100d);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Z, PosZ);
                            break;
                        case "ACCDOWN":
                            PosZ = (float)ModPosZ - (float)((s_Acc_dst + 1) / 100d);
                            tmp_ZLock = tmp_ZLock - (float)((s_Acc_dst + 1) / 100d);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Z, PosZ);
                            break;
                        case "SUPFOR":
                            PosX = (float)ModPosX - (float)((s_Sup_dst + 1) / 10d) * (float)Math.Sin(CamRotH * Math.PI) * -1;
                            PosY = (float)ModPosY + (float)((s_Sup_dst + 1) / 10d) * (float)Math.Cos(CamRotH * Math.PI) * -1;
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.X, PosX);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Y, PosY);
                            break;
                        case "SUPUP":
                            PosZ = (float)ModPosZ + (float)((s_Sup_dst + 1) / 10d);
                            tmp_ZLock = tmp_ZLock + (float)((s_Sup_dst + 1) / 10d);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Z, PosZ);
                            break;
                        case "SUPDOWN":
                            PosZ = (float)ModPosZ - (float)((s_Sup_dst + 1) / 10d);
                            tmp_ZLock = tmp_ZLock - (float)((s_Sup_dst + 1) / 10d);
                            SplMemory.WriteMemory(PtrPlayerLoc + (long)Offset.Loc.Z, PosZ);
                            break;
                    }
                }
            }
            catch { }
        }

        void _Sequence_2(object sender, EventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Cheat.Show.Get_Value() && WindowsIs())
            {
                bool cb_Key_IsChecked = false;
                cb_Key_IsChecked = (bool)cb_Key.IsChecked;
                if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierToKey.Get_Value(),
                               in_Win_Main.in_Setting.in_CheatKey.keyToKey.Get_Value()))
                {
                    cb_Key.IsChecked = !cb_Key.IsChecked;
                    while (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierToKey.Get_Value(),
                                      in_Win_Main.in_Setting.in_CheatKey.keyToKey.Get_Value()))
                    {
                        Thread.Sleep(20);
                    }
                }
                if (cb_Key_IsChecked)
                {
                    if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierNoGrav.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keyNoGrav.Get_Value()))
                    {
                        cb_NoGrav.IsChecked = !cb_NoGrav.IsChecked;
                        while (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierNoGrav.Get_Value(),
                                          in_Win_Main.in_Setting.in_CheatKey.keyNoGrav.Get_Value()))
                        {
                            Thread.Sleep(20);
                        }
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierZLock.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keyZLock.Get_Value()))
                    {
                        cb_ZLock.IsChecked = !cb_ZLock.IsChecked;
                        while (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierZLock.Get_Value(),
                                          in_Win_Main.in_Setting.in_CheatKey.keyZLock.Get_Value()))
                        {
                            Thread.Sleep(20);
                        }
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierAccFor.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keyAccFor.Get_Value()))
                    {
                        MoveCheat("ACCFOR");
                        Thread.Sleep(20);
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierAccUp.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keyAccUp.Get_Value()))
                    {
                        MoveCheat("ACCUP");
                        Thread.Sleep(20);
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierAccDown.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keyAccDown.Get_Value()))
                    {
                        MoveCheat("ACCDOWN");
                        Thread.Sleep(20);
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierSupFor.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keySupFor.Get_Value()))
                    {
                        MoveCheat("SUPFOR");
                        Thread.Sleep(20);
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierSupUp.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keySupUp.Get_Value()))
                    {
                        MoveCheat("SUPUP");
                        Thread.Sleep(20);
                    }
                    else if (KeyIsPress(in_Win_Main.in_Setting.in_CheatKey.modifierSupDown.Get_Value(),
                                   in_Win_Main.in_Setting.in_CheatKey.keySupDown.Get_Value()))
                    {
                        MoveCheat("SUPDOWN");
                        Thread.Sleep(20);
                    }
                }
            }
        }

        /*
        **  BOSS
        */
        void _Sequence_3(object sender, EventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Cheat.Show.Get_Value() && WindowsIs())
            {

            }
        }
    }
}
