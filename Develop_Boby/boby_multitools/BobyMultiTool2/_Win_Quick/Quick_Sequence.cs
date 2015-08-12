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
    public partial class Win_Quick
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

        private long PlayerPtr;

        DispatcherTimer messageTimer_1;

        private void Quick_Sequence()
        {
            Thread T_Quick = new Thread(Sequence_1);
            T_Quick.SetApartmentState(ApartmentState.STA);
            T_Quick.Start();
        }

        private void Sequence_1()
        {
            messageTimer_1 = new DispatcherTimer();
            messageTimer_1.Tick += new EventHandler(_Sequence_1);
            messageTimer_1.Interval = new TimeSpan(0, 0, 0, 0, 1);
            messageTimer_1.Start();
            System.Windows.Threading.Dispatcher.Run();
        }

        void _Sequence_1(object sender, EventArgs e)
        {
            try
            {
                if (in_Win_Main.in_Setting.in_Quick.Show.Get_Value())
                {
                    long gameBase = Aion_Game.Modules.Game;

                    if (SplMemory.ReadByte(PlayerPtr + (long)Offset.Entity.Type) != EnumAion.eType.Player)
                        this.PlayerPtr = FindPlayerPtr();

                    if (PlayerPtr != 0)
                    {
                        long PlayerEntity = SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status);

                        if (WindowsIs() && this.is_play)
                        {
                            if (PlayerPtr != 0 && PlayerPtr != 0)
                            {
                                string Player_WeaponStyle = "" + (EnumAion.AionWeaponStyle)SplMemory.ReadInt(SplMemory.ReadLong(PlayerPtr + Offset.Entity.Status) + Offset.Status.WeaponStyle);

                                for (int i = 0; i < in_Win_Main.in_Win_Quick.in_Win_Quick_Setting.quick_collect.Count; i++)
                                {
                                    if (in_Win_Main.in_Win_Quick.in_Win_Quick_Setting.quick_collect[i].TYPE == Player_WeaponStyle)
                                    {
                                        if (in_Win_Main.in_Win_Quick.in_Win_Quick_Setting.quick_collect[i].ID == 0)
                                            SplMemory.WriteMemory(gameBase + Offset.Player.QuickBar, 9);
                                        else
                                            SplMemory.WriteMemory(gameBase + Offset.Player.QuickBar, in_Win_Main.in_Win_Quick.in_Win_Quick_Setting.quick_collect[i].ID - 1);
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
    }
}
