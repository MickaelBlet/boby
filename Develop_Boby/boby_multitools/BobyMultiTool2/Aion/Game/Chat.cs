using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices; // DllImport

using MemoryLib;
using Aion_Process;
using Windows_And_Process;

namespace Aion_Game
{ 
    public class Chat
    {
        private static Window main;
        public const byte CHAT_ISCLOSE = 6;
        public const byte CHAT_ISOPEN = 7;

        public static void Ini(Window in_Main)
        {
            main = in_Main;
            Thread T_inichat = new Thread(_Ini);
            T_inichat.SetApartmentState(ApartmentState.STA);
            T_inichat.Start();
        }

        public static void _Ini()
        {
            while (true)
            {
                try
                {
                    if (GetValueChatInputIni() == 0)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    if (GetValueChatInputIni() < 255)
                    {
                        if (GetValueChatIsOpen())
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_I);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_N);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_I);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_T);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_I);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_A);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_L);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_I);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_S);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_A);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_T);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_I);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_O);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_N);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_0);
                                Send_Key.Send2(Game.Whandle, eWindowsVirtualKey.K_0);
                            }
                            Thread.Sleep(1000);
                        }
                        if (!GetValueChatIsOpen())
                            Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                    }
                    else if (GetValueChatInputIni() < 257)
                    {
                        if (GetValueChatIsOpen())
                        {
                    		long adressChat = GetAddressChatForText();
		                    if (adressChat != 0)
		                    {
			                    SplMemory.WriteWchar(adressChat, "", 255);
                                Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                                Thread.Sleep(50);
                                Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                                Thread.Sleep(50);
                                Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_BACK);
                                Thread.Sleep(50);
                                Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
		                    }
                    	}
                        if (!GetValueChatIsOpen())
                        {
                            main.Dispatcher.Invoke(() => { 
                                main.WindowState = WindowState.Minimized;
                            });
                            return ;
                    	}
                    }
                }
                catch { }
                Thread.Sleep(100);
            }
        }

        public static void Send(string text)
        {
            try
            {
                if (text != "" && GetValueChatInputIni() >= 255)
                {
                    long adressChat = GetAddressChatForText();
                    if (adressChat == 0)
                        return;
                    SplMemory.WriteWchar(adressChat, text, 255);
                    if (GetValueChatIsOpen())
                    {
                        (new Thread(() =>
                        {
                            Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                        })).Start();
                    }
                    else
                    {
                        (new Thread(() =>
                        {
                            Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                        })).Start();
                        (new Thread(() =>
                        {
                            Send_Key.Send(Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                        })).Start();
                    }
                }
            }
            catch { }
        }

        public static short GetValueChatInputIni()
        {
            try
            {
                long chat_emoticon = DialogList.GetDialog("chat_input_dialog>chat_emoticon").Node;
                if (chat_emoticon != 0)
                    return(SplMemory.ReadShort(chat_emoticon + Offset.Chat.Length));
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static bool GetValueChatIsOpen()
        {
            try
            {
                return DialogList.GetDialog("chat_input_dialog").Open;
            }
            catch
            {
                return false;
            }
        }

        public static long GetAddressChatForText()
        {
            try
            {
                long chat_emoticon = DialogList.GetDialog("chat_input_dialog>chat_emoticon").Node;
                return (SplMemory.ReadLong(chat_emoticon + Offset.Chat.Input));
            }
            catch
            {
                return 0;
            }
        }
    }
}
