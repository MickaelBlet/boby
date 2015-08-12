using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; // DllImport
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

namespace NS_Windows_And_Process
{
    class Send_Key
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void Send_Enter_Key(IntPtr whandle)
        {
            SendMessage(whandle, 0x0100, (IntPtr)0x0D, IntPtr.Zero);
        }

        public static void Send_Return_Key(IntPtr whandle)
        {
            SendMessage(whandle, 0x100, (IntPtr)0x08, IntPtr.Zero);
        }

        public static void Send_Ctrl_V_Key(IntPtr whandle)
        {
            if (Windows_And_Process.WindowsIsForeground(whandle))
            {
                System.Windows.Forms.SendKeys.SendWait("^{v}");
            }
            else
            {
                keybd_event(0x11, 0, 0, 0);// presses ctrl
                SendMessage(whandle, 0x0100, (IntPtr)0x56, IntPtr.Zero);
                keybd_event(0x11, 0, 2, 0); //releases ctrl
            }
        }
    }
}
