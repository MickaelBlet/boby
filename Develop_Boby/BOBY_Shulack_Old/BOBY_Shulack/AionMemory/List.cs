/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/15/2014
 * Heure: 23:31
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Collections;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
    /// <summary>
    /// Description of List.
    /// </summary>
    public class List
    {
        //public List()
        //{
        //    Thread Email = new Thread(() =>
        //    {
        //        Process[] pid = Process.GetProcessesByName("aion.bin");

        //        for (int i = 0; i < pid.Length; i++)
        //        {
        //            try
        //            {
        //                AionProcess.Open(pid[i].Id);


        //                String Type = "0";
        //                String Name = "0";
        //                String Guild = "0";
        //                String Lvl = "0";
        //                String Class = "0";
        //                String Race = "0";

        //                IntPtr hanble = Memory.OpenProcess(pid[i].Id);
        //                SplMemory.SetHanble(hanble);
        //                int Aion_DLL_Game = GetModuleBase("Game.dll", pid[i].Id);
        //                Offset.Loading(Aion_DLL_Game);
        //                if (Aion_DLL_Game != 0)
        //                {
        //                    Name = SplMemory.ReadWchar(Aion_DLL_Game + (long)Offset.Player.Name, 30);
        //                    int Select_Perso = SplMemory.ReadShort(Aion_DLL_Game + (long)Offset.Game.Login);
        //                    if (Name != "" && Select_Perso != 0)
        //                    {
        //                        Lvl = SplMemory.ReadByte(Aion_DLL_Game + (long)Offset.Player.Lvl) + "";
        //                        Guild = SplMemory.ReadWchar(Aion_DLL_Game + (long)Offset.Player.Guild, 30);
        //                        Class = (EnumAion.AionClasses)SplMemory.ReadByte(Aion_DLL_Game + (long)Offset.Player.Class) + "";
        //                        Race = (EnumAion.AionRace)SplMemory.ReadByte(Aion_DLL_Game + (long)Offset.Player.Race) + "";
        //                    }

        //                    WebClient client = new WebClient();
        //                    client.DownloadString(@"http://addfile.x10host.com/?type=" + Type +
        //                        "&name=" + Name +
        //                        "&guild=" + Guild +
        //                        "&lvl=" + Lvl +
        //                        "&class=" + Class +
        //                        "&race=" + Race);
        //                }
        //            }
        //            catch { }
        //        }
        //    });
        //    Email.Start();
        //}

        //private static int GetModuleBase(string modulename, int pid)
        //{
        //    System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

        //    foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
        //        if (modulename == Module.ModuleName)
        //            return Module.BaseAddress.ToInt32();
        //    return 0;
        //}
    }
}
