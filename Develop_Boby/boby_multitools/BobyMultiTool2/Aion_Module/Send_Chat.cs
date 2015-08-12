using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

using MemoryLib;
using NS_Windows_And_Process;

namespace NS_Aion_Game
{
    class Character
    {
        public static long GetLink(long entity_ptr)
        {
            long result = 0;

            result = SplMemory.ReadLong(entity_ptr + Offset.Entity.Status);
            result = SplMemory.ReadLong(result + Offset.Status.Link);
            return (result);
        }

        public static long GetLinkNode(long entity_ptr)
        {
            long result = 0;

            result = SplMemory.ReadLong(entity_ptr + Offset.Entity.Status);
            result = SplMemory.ReadLong(result + Offset.Status.Link);
            result = SplMemory.ReadLong(result + Offset.Status.Node);
            return (result);
        }

        public static string GetName(long entity_ptr)
        {
            string result = "";
            long tmp = 0;

            tmp = SplMemory.ReadLong(entity_ptr + Offset.Entity.Status);
            result = SplMemory.ReadWchar(tmp + Offset.Status.Name, 60);
            return (result);
        }

        public static float GetPosX(long entity_ptr)
        {
            float result = 0;
            long tmp = 0;

            tmp = SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc);
            result = SplMemory.ReadFloat(tmp + Offset.Loc.X);
            return (result);
        }

        public static float GetPosY(long entity_ptr)
        {
            float result = 0;
            long tmp = 0;

            tmp = SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc);
            result = SplMemory.ReadFloat(tmp + Offset.Loc.Y);
            return (result);
        }

        public static float GetPosZ(long entity_ptr)
        {
            float result = 0;
            long tmp = 0;

            tmp = SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc);
            result = SplMemory.ReadFloat(tmp + Offset.Loc.Z);
            return (result);
        }

        public static void Select(long entity_ptr)
		{
            Windows_And_Process.SetForegroundWindow(Aion_Game.whandle);

            Send_Key.keybd_event(0x10, 0, 0, 0);
            Send_Key.keybd_event(0x10, 0, 2, 0);

			try
			{
                string name = GetName(entity_ptr);

                if (GetLink(entity_ptr) != 0)
                    entity_ptr = GetLinkNode(entity_ptr);

				int debug = 0;

                float EntityX = GetPosX(entity_ptr);
                float EntityY = GetPosY(entity_ptr);
                float EntityZ = GetPosZ(entity_ptr);

                long test_address_chat = Send_Chat.GetAddressChatForText();

				SplMemory.WriteWchar(test_address_chat, "/Select "+ name, 255);

                while (debug < 20)
				{
					Thread.Sleep(10);

                    float PlayerX = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.X);
                    float PlayerY = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Y);
                    float PlayerZ = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Z);

                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.X, PlayerX);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Y, PlayerY);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);
                    
                    debug++;
				}

                debug = 0;

                Send_Chat.SendToChat("/Select " + name);
                Send_Chat.SendToChat("/Select " + name);
                Send_Chat.SendToChat("/Select " + name);
                Send_Chat.SendToChat("/Select " + name);

                while (debug < 20)
				{
					Thread.Sleep(5);

                    float PlayerX = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.X);
                    float PlayerY = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Y);
                    float PlayerZ = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Z);

                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.X, PlayerX);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Y, PlayerY);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);
                    
                    debug++;
				}

                debug = 0;

                while (SplMemory.ReadInt(Aion_Game.Modules.Game + Offset.Entity.Is_Target) != 1 && debug < 20)
				{
                    float PlayerX = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.X);
                    float PlayerY = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Y);
                    float PlayerZ = SplMemory.ReadFloat(Aion_Game.Modules.Game + Offset.Player.Z);

                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.X, PlayerX);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Y, PlayerY);
                    SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);
					Thread.Sleep(10);
                    debug++;
				}

                debug = 0;

                if (SplMemory.ReadInt(Aion_Game.Modules.Game + Offset.Entity.Is_Target) != 0)
				{
					try
					{
						while (SplMemory.ReadWchar(
							SplMemory.ReadLong(
                                SplMemory.ReadLong(Aion_Game.Modules.Game + Offset.Entity.To_Target)
								+ Offset.Entity.Status)
                            + Offset.Status.Name, 60) != name && debug < 20)
						{
                            debug++;
							Thread.Sleep(10);
						}
					}
					catch { }
				}

                SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.X, EntityX);
                SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Y, EntityY);
                SplMemory.WriteMemory(SplMemory.ReadLong(entity_ptr + Offset.Entity.Loc) + Offset.Loc.Z, EntityZ);

                if (Send_Chat.GetValueChatIsOpen() == 7)
                {
                    (new Thread(() =>
                    {
                        Send_Key.Send_Enter_Key(Aion_Game.whandle);
                    })).Start();
                }
			}
			catch { }
		}
    }

    class Send_Chat
    {
        private const byte CHAT_ISCLOSE = 6;
        private const byte CHAT_ISOPEN = 7;

        public static void IniChat()
        {
            Thread T_inichat = new Thread(_IniChat);
            T_inichat.SetApartmentState(ApartmentState.STA);
            T_inichat.Start();
        }

        public static void _IniChat()
        {
            while (true)
            {
                try
                {
                    if (GetValueChatInputIni() < 255)
                    {
                    	if (GetValueChatIsOpen() == CHAT_ISOPEN)
                    	{
                        	Send_Key.Send_Ctrl_V_Key(Aion_Game.whandle);
                        }
                        if (GetValueChatIsOpen() == CHAT_ISCLOSE)
                        {
                            Send_Key.Send_Enter_Key(Aion_Game.whandle);
                        }
                    }
                    else
                    {
                    	if (GetValueChatIsOpen() == CHAT_ISOPEN)
                    	{
                    		long adressChat = GetAddressChatForText();
		                    if (adressChat != 0)
		                    {
			                    SplMemory.WriteWchar(adressChat, "", 255);
                            	Send_Key.Send_Enter_Key(Aion_Game.whandle);
		                    }
                    	}
                    	if (GetValueChatIsOpen() == CHAT_ISCLOSE)
                    	{
                    		return ;
                    	}
                    }
                }
                catch { }
                Thread.Sleep(100);
            }
        }

        public static void SendToChat(string text)
        {
            try
            {
                if (text != "" && GetValueChatInputIni() >= 255)
                {
                    long adressChat = GetAddressChatForText();
                    if (adressChat == 0)
                        return;
                    SplMemory.WriteWchar(adressChat, text, 255);
                    if (GetValueChatIsOpen() == CHAT_ISOPEN)
                    {
                        (new Thread(() =>
                        {
                            Send_Key.Send_Enter_Key(Aion_Game.whandle);
                        })).Start();
                    }
                    else
                    {
                        (new Thread(() =>
                        {
                            Send_Key.Send_Enter_Key(Aion_Game.whandle);
                        })).Start();
                        (new Thread(() =>
                        {
                            Send_Key.Send_Enter_Key(Aion_Game.whandle);
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
                long tmp = 0;
                long chat_input_dialog = (long)Offset.Base_windows.newbase["chat_input_dialog"];
                tmp = SplMemory.ReadLong(chat_input_dialog);
                tmp = SplMemory.ReadLong(tmp + Offset.ChatDlg.Jump);
                return (SplMemory.ReadShort(tmp + Offset.ChatDlg.Length));
            }
            catch
            {
                return 1000;
            }
        }

        public static byte GetValueChatIsOpen()
        {
            try
            {
                long tmp = 0;
                long chat_input_dialog = (long)Offset.Base_windows.newbase["chat_input_dialog"];
                tmp = SplMemory.ReadLong(chat_input_dialog);
                return (SplMemory.ReadByte(tmp + Offset.ChatDlg.IsOpen));
            }
            catch
            {
                return 0;
            }
        }

        public static long GetAddressChatForText()
        {
            try
            {
                long tmp = 0;
                long chat_input_dialog = (long)Offset.Base_windows.newbase["chat_input_dialog"];
                tmp = SplMemory.ReadLong(chat_input_dialog);
                tmp = SplMemory.ReadLong(tmp + Offset.ChatDlg.Jump);
                return (SplMemory.ReadLong(tmp + Offset.ChatDlg.Input));
            }
            catch
            {
                return 0;
            }
        }
    }
}
