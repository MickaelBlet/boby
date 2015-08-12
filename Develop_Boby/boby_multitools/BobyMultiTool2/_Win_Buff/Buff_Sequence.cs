using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices; // DllImport

using MemoryLib;
using NS_Aion_Game;
using _Threads;
using NS_Windows_And_Process;

namespace BobyMultitools
{
    public partial class Win_Buff
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        }
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private long PlayerPtr;

        public int []Buff_player = null;

        private DateTime TimeAfk = new DateTime();
        private float PlayerAfkX = 0;
        private float PlayerAfkY = 0;
        private float PlayerAfkZ = 0;

        bool Is_not_open = false;
        double Last_Cube_Width = 0;
        double Last_Cube_Height = 0;

        DispatcherTimer messageTimer_1;
        DispatcherTimer messageTimer_2;

        private void Buff_Sequence()
        {
            Thread T_Buff = new Thread(Sequence_1);
            T_Buff.SetApartmentState(ApartmentState.STA);
            T_Buff.Start();
            Thread T_Buff2 = new Thread(Sequence_2);
            T_Buff2.SetApartmentState(ApartmentState.STA);
            T_Buff2.Start();
        }

        private void Sequence_1()
        {
            messageTimer_1 = new DispatcherTimer();
            messageTimer_1.Tick += new EventHandler(_Sequence_1);
            messageTimer_1.Interval = new TimeSpan(0, 0, 0, 0, 250);
            messageTimer_1.Start();
            System.Windows.Threading.Dispatcher.Run();
        }

        private void Sequence_2()
        {
            messageTimer_2 = new DispatcherTimer();
            messageTimer_2.Tick += new EventHandler(_Sequence_2);
            messageTimer_2.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            messageTimer_2.Start();
            System.Windows.Threading.Dispatcher.Run();
        }

        void _Sequence_1(object sender, EventArgs e)
        {
            try
            {
                if (in_Win_Main.in_Setting.in_Buff.Show.Get_Value())
                {
                    long gameBase = Aion_Game.Modules.Game;

                    if (SplMemory.ReadByte(PlayerPtr + (long)Offset.Entity.Type) != EnumAion.eType.Player)
                        this.PlayerPtr = FindPlayerPtr();

                    if (PlayerPtr != 0)
                    {
                        long PlayerEntity = SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status);

                        ListBuff(PlayerEntity);

                        if (WindowsIs() && this.is_play && !BuffFufu())
                        {
                            if (GetValue_Chat_Is_Open() == 6 && !AfkMode())
                            {
                                int Player_State = SplMemory.ReadInt(PlayerEntity + (long)Offset.Status.State);
                                int Player_Stance = SplMemory.ReadInt(PlayerEntity + (long)Offset.Status.Stance);
                                long Player_Link = SplMemory.ReadLong(PlayerEntity + (long)Offset.Status.Link);
                                int Player_Class = SplMemory.ReadInt(PlayerEntity + (long)Offset.Status.Class);

                                if (SplMemory.ReadInt(PlayerEntity + (long)Offset.Status.HP) > 0 &&
                                    (Player_State == 0 ||
                                     Player_State == 1 ||
                                     Player_State == 7) &&
                                    (Player_Link == 0 || Player_Class == 13) &&
                                    SplMemory.ReadInt(PlayerEntity + (long)Offset.Status.No_Grav) != 7 &&
                                    !(Player_Stance == 11 ||
                                      Player_Stance == 3 ||
                                      SplMemory.ReadByte(SplMemory.ReadLong((long)Offset.Base_windows.newbase["skill_delay_dialog"]) + (long)Offset.Base_windows.IsOpen) % 2 == 1 ||
                                      SplMemory.ReadByte(SplMemory.ReadLong((long)Offset.Base_windows.newbase["enchant_delay_dialog"]) + (long)Offset.Base_windows.IsOpen) % 2 == 1))
                                {
                                    for (int i = 0; i < this.in_Win_Buff_Setting.buff_collect.Count; i++)
                                    {
                                        if (!IfBuffExist(this.in_Win_Buff_Setting.buff_collect[i].ID) && this.in_Win_Buff_Setting.buff_collect[i].ID != 0)
                                        {
                                            if (this.in_Win_Buff_Setting.buff_collect[i].ID <= 5000)
                                            {
                                                if (this.in_Win_Buff_Setting.buff_collect[i].COMMAND[0] == '<')
                                                {
                                                    string transformString = this.in_Win_Buff_Setting.buff_collect[i].COMMAND.ToString().Replace("<", "");
                                                    transformString = transformString.Replace(">", "");
                                                    Debug.WriteLine("!!!");
                                                    Send_Chat.SendToChat("/Skill " + transformString);
                                                }
                                                else
                                                {
                                                    Debug.WriteLine("!!!");
                                                    Send_Chat.SendToChat("/Skill " + this.in_Win_Buff_Setting.buff_collect[i].COMMAND.ToString());
                                                }
                                            }
                                        }
                                    }

                                    Check_Item();

                                    if (Is_not_open)
                                    {
                                        long CubeBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"]);
                                        SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, Last_Cube_Height);
                                        SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, Last_Cube_Width);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        void _Sequence_2(object sender, EventArgs e)
        {
            try
            {
                bool is_visible = false;
                this.Dispatcher.Invoke((Action)(() =>
                    {
                        is_visible = this.in_Win_Buff_Setting.IsVisible;
                    }
                ));
                if (is_visible)
                {
                    try
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                            {
                                this.in_Win_Buff_Setting.player_buff_collect.Clear();
                                for (int i = 0; i < this.Buff_player.Length; i++)
                                {
                                    ImageSource source = null;
                                    if (this.in_Win_Buff_Setting.is_read_file)
                                    {
                                        if (this.in_Win_Buff_Setting.File_img.ContainsKey(this.Buff_player[i].ToString()))
                                            source = (ImageSource)this.in_Win_Buff_Setting.File_img[this.Buff_player[i].ToString()];
                                    }
                                    this.in_Win_Buff_Setting.player_buff_collect.Add(new Buff { ID = this.Buff_player[i], COMMAND = this.Buff_player[i].ToString(), IMG = source });
                                }
                            }));
                    }
                    catch
                    { }
                }
            }
            catch
            { }
        }

        public void Check_Item()
        {
            long CubeBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"]);

            string ItemCube = "";
            int Item_CD = -1;
            Is_not_open = false;

            if (SplMemory.ReadByte(CubeBase + Offset.Base_windows.IsOpen) == 142)
            {
                Is_not_open = true;

                Last_Cube_Width = SplMemory.ReadDouble(CubeBase + (long)Offset.Cube.ListW);
                Last_Cube_Height = SplMemory.ReadDouble(CubeBase + (long)Offset.Cube.ListH);

                SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)143);
            }

            foreach (var keyValue in in_Win_Buff_Setting.buff_collect)
            {
                for (int i=0; i < 27; i++)
                {
                    if (Is_not_open && SplMemory.ReadByte(CubeBase + Offset.Base_windows.IsOpen) == 142)
                    {
                        SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, Last_Cube_Width);
                        SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, Last_Cube_Height);
                        SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)143);
                        return;
                    }
                    if (keyValue.ID != 0 && !IfBuffExist(keyValue.ID))
                    {
                        long Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + i * 4);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
                        }
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            ItemCube = SplMemory.ReadWchar(Item, 100);
                        }
                        else
                            ItemCube = "";
                        if (keyValue.ID > 5000)
                        {
                            if (SplMemory.ReadInt(Aion_Game.Modules.Game + (long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
                            {
                                if (keyValue.COMMAND[0] == '<')
                                {
                                    string transformString = keyValue.COMMAND.ToString().Replace("<", "");
                                    transformString = transformString.Replace(">", "");
                                    if (transformString == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + transformString);
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    if (keyValue.COMMAND == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + keyValue.COMMAND.ToString());
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                        Item_CD = -1;
                        Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 4);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + i * 4);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
                        }
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            ItemCube = SplMemory.ReadWchar(Item, 100);
                        }
                        else
                            ItemCube = "";
                        if (keyValue.ID > 5000)
                        {
                            if (SplMemory.ReadInt(Aion_Game.Modules.Game + (long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
                            {
                                if (keyValue.COMMAND[0] == '<')
                                {
                                    string transformString = keyValue.COMMAND.ToString().Replace("<", "");
                                    transformString = transformString.Replace(">", "");
                                    if (transformString == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + transformString);
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    if (keyValue.COMMAND == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + keyValue.COMMAND.ToString());
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                        Item_CD = -1;
                        Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 8);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + i * 4);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
                        }
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            ItemCube = SplMemory.ReadWchar(Item, 100);
                        else
                            ItemCube = "";
                        if (keyValue.ID > 5000)
                        {
                            if (SplMemory.ReadInt(Aion_Game.Modules.Game + (long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
                            {
                                if (keyValue.COMMAND[0] == '<')
                                {
                                    string transformString = keyValue.COMMAND.ToString().Replace("<", "");
                                    transformString = transformString.Replace(">", "");
                                    if (transformString == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + transformString);
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    if (keyValue.COMMAND == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + keyValue.COMMAND.ToString());
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                        Item_CD = -1;
                        Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 12);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + i * 4);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                        {
                            Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
                        }
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
                        if (Item != 0 && Item != 0xCDCDCDCD)
                            ItemCube = SplMemory.ReadWchar(Item, 100);
                        else
                            ItemCube = "";
                        if (keyValue.ID > 5000)
                        {
                            if (SplMemory.ReadInt(Aion_Game.Modules.Game + (long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
                            {
                                if (keyValue.COMMAND[0] == '<')
                                {
                                    string transformString = keyValue.COMMAND.ToString().Replace("<", "");
                                    transformString = transformString.Replace(">", "");
                                    if (transformString == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + transformString);
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    if (keyValue.COMMAND == ItemCube && Item_CD != -1)
                                    {
                                        Send_Chat.SendToChat("/Use " + keyValue.COMMAND.ToString());
                                        if (Is_not_open)
                                        {
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListW, 0d);
                                            SplMemory.WriteMemory(CubeBase + (long)Offset.Cube.ListH, 0d);
                                            SplMemory.WriteMemory(CubeBase + Offset.Base_windows.IsOpen, (byte)142);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                        Item_CD = -1;
                    }
                }
            }
        }

        private bool AfkMode()
        {
            float playerAfkX = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.X);
            float playerAfkY = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.Y);
            float playerAfkZ = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.Z);

            if (PlayerAfkX != playerAfkX ||
                PlayerAfkY != playerAfkY ||
                PlayerAfkZ != playerAfkZ)
            {
                this.PlayerAfkX = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.X);
                this.PlayerAfkY = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.Y);
                this.PlayerAfkZ = SplMemory.ReadFloat(Aion_Game.Modules.Game + (long)Offset.Player.Z);
                TimeAfk = DateTime.Now.AddMinutes(4);
            }
            if (TimeAfk < DateTime.Now)
                return true;
            else
                return false;
        }

        private long FindPlayerPtr()
        {
            try
            {
                foreach (Entity entity in in_Win_Main.in_Thread_Entity.DicCopy.Values)
                {
                    if (entity.Type == (int)EnumAion.eType.Player)
                        return entity.PtrEntity;
                }
            }
            catch (Exception)
            {

            }
            return 0;
        }

        private bool WindowsIs()
        {
            bool isactive = false;
            if (Aion_Game.whandle != IntPtr.Zero)
            {
                IntPtr MWhandle = Aion_Game.whandle;
                IntPtr CWhandle = GetForegroundWindow();
                if (MWhandle == CWhandle)
                    isactive = true;
            }
            return isactive;
        }

        byte GetValue_Chat_Is_Open()
        {
            return SplMemory.ReadByte(
                SplMemory.ReadLong(
                    (long)Offset.Base_windows.newbase["chat_input_dialog"]) + (long)Offset.ChatDlg.IsOpen);
        }

        int ListBuffType(long _PtrEntity, long list, int index)
        {
            int lenght = 0;
            for (long j = SplMemory.ReadLong(_PtrEntity + list); j < SplMemory.ReadLong(_PtrEntity + list + 4); j += 0x4)
                lenght++;
            for (int i = 0; i < lenght; i++)
            {
                this.Buff_player[index] = SplMemory.ReadInt(SplMemory.ReadLong(SplMemory.ReadLong(_PtrEntity + list) + i * 0x4) + 0x4);
                index++;
            }
            return index;
        }

        void ListBuff(long _PtrEntity)
        {
            int lenghtBuff = SplMemory.ReadInt(_PtrEntity + (long)Offset.Status.BuffCount);
            if (lenghtBuff > 0)
            {
                this.Buff_player = new int[lenghtBuff];
                int index = 0;
                for (long i = 0; i < 0x80; i += 0x10)
                    index = ListBuffType(_PtrEntity, (long)Offset.Status.BuffArray + i, index);
            }
            else
            {
                this.Buff_player = new int[1];
                this.Buff_player[0] = 0;
            }
        }

        private bool IfBuffExist(int Buff)
        {
            for (int i = 0; i < this.Buff_player.Length; i++)
            {
                if (this.Buff_player[i] == Buff)
                    return true;
            }
            return false;
        }

        private bool BuffFufu()
        {
            for (int i = 0; i < this.Buff_player.Length; i++)
            {
                if (this.Buff_player[i] == 559 ||
                    this.Buff_player[i] == 582 ||
                    this.Buff_player[i] == 851 ||
                    this.Buff_player[i] == 948 ||
                    this.Buff_player[i] == 2735)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
