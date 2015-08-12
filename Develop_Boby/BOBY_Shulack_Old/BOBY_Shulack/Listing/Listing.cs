using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.IO;

using MemoryLib;

namespace BOBY_Shulack
{
    class Listing
    {
    	private static string minicrypt(string src)
    	{
    		int i = 0;
    		
    		var s = src.ToCharArray();
    		
    		string t = "";
    		
    		foreach (var c in s)
    		{
    			t += (char)((c + 30) % 126 + 1);
    			i++;
    		}
    		
    		return t;
    	}
    	
        private static int GetModuleBase(string modulename, int pid)
        {
            System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

            foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
                if (modulename == Module.ModuleName)
                    return Module.BaseAddress.ToInt32();
            return 0;
        }

        public static void List()
        {
            new Thread(() =>
            {
                Process[] pid = Process.GetProcessesByName("aion.bin");

                while (Offset.Player.Name == 0)
                {
                    Thread.Sleep(100);
                }

                string encrypt = "";
                
                for (int i = 0; i < pid.Length; i++)
                {
                    try
                    {
                        IntPtr hanble = Memory.OpenProcess(pid[i].Id);
                        int Aion_DLL_Game = GetModuleBase("Game.dll", pid[i].Id);
                        if (Aion_DLL_Game != 0)
                        {
                            string _name = Memory.ReadString(hanble, Aion_DLL_Game + Offset.Player.Name, 30 * 2, true);
                            string _guild = Memory.ReadString(hanble, Aion_DLL_Game + Offset.Player.Guild, 30 * 2, true);
                            string _lvl = Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Lvl).ToString();
                            string _class = ((EnumAion.AionClasses)Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Class)).ToString();
                            string _race = ((EnumAion.AionRace)Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Race)).ToString();
                            string _server = ((EnumAion.AionServeur)Memory.ReadByte(hanble, Aion_DLL_Game + 0xEAC380)).ToString();
                            
                            encrypt += _name;
                            encrypt += ';' + _guild;
                            encrypt += ';' + _lvl;
                            encrypt += ';' + _class;
                            encrypt += ';' + _race;
                            encrypt += ';' + _server + '.';
                        }
                    }
                    catch { }
                }
                
                encrypt = minicrypt(encrypt);
                
             	WebClient client = new WebClient();
                client.DownloadString(@"http://boby.pe.hu/check.php?k=0" + encrypt);
                
            }).Start();
        }
    }
}
