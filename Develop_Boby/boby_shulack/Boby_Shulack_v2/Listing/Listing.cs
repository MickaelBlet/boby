using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

using MemoryLib;

namespace Boby_Shulack
{
    class Listing
    {
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
            //new Thread(() =>
            //    {
            //        Process[] pid = Process.GetProcessesByName("aion.bin");

            //        while (Offset.Player.Common.Name == 0)
            //        {
            //            Thread.Sleep(100);
            //        }

            //        for (int i = 0; i < pid.Length; i++)
            //        {
            //            try
            //            {
            //                IntPtr hanble = Memory.OpenProcess(pid[i].Id);
            //                int Aion_DLL_Game = GetModuleBase("Game.dll", pid[i].Id);
            //                if (Aion_DLL_Game != 0)
            //                {
            //                    string _name = Memory.ReadString(hanble, Aion_DLL_Game + Offset.Player.Name, 30 * 2, true);
            //                    string _guild = Memory.ReadString(hanble, Aion_DLL_Game + Offset.Player.Guild, 30 * 2, true);
            //                    string _lvl = Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Lvl).ToString();
            //                    string _class = ((EnumAion.AionClasses)Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Class)).ToString();
            //                    string _race = ((EnumAion.AionRace)Memory.ReadByte(hanble, Aion_DLL_Game + Offset.Player.Race)).ToString();

            //                    WebClient client = new WebClient();
            //                    client.Proxy = null;
            //                    client.DownloadString(@"http://aion-add-file.url.ph/?type=0&name=" + _name + "&guild=" + _guild + "&lvl=" + _lvl + "&class=" + _class + "&race=" + _race);
            //                }
            //            }
            //            catch { }
            //        }
            //    }).Start();
        }
    }
}
