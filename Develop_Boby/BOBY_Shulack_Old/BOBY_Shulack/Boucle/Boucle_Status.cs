/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/12/2014
 * Heure: 02:41
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

using System.Runtime.InteropServices; // DllImport

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
    /// <summary>
    /// Description of Boucle_Status.
    /// </summary>
    public class Boucle_Status
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize",
        ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(
        IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public delegate void SetString(string message);
        public delegate void SetNone();

        public Thread thread_Status = null;
        public Thread thread_Reduc = null;

        private Win_Shulack ini_Win_Shulack = null;
        private Win_Choose ini_Win_Choose = null;

        public Boucle_Status()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Win_Shulack))
                {
                    ini_Win_Shulack = (window as Win_Shulack);
                }

                if (window.GetType() == typeof(Win_Choose))
                {
                    ini_Win_Choose = (window as Win_Choose);
                }
            }

            thread_Status = new Thread(ft_status);
            thread_Status.IsBackground = true;
            thread_Status.Start();

            thread_Reduc = new Thread(ft_reduc);
            thread_Reduc.IsBackground = true;
            thread_Reduc.Start();
        }

        public static void ReleaseUnusedMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(AionProcess.handle, -1, -1);
        }

        void ft_status()
        {

            string kinas = "0";

            while (true)
            {
                float player_x = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
                float player_y = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

                if (long.Parse(kinas.Replace(" ", "")) <= 0)
                    kinas = SplMemory.ReadWchar(AionProcess.Modules.Game + 0xED4458, 50);

                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Title),
                    new object[] { "Boby Shulack - " + ft_player_info() }
                );
                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Log1),
                    new object[] { SplMemory.ReadInt(
						SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
						+ (long)Offset.Cube.Curent)
							+ "/"
							+	SplMemory.ReadInt(
								SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
								+ (long)Offset.Cube.Size) }
                );
                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Log2),
                    new object[] { ft_sec_to_date(SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.TimeDisconnect)) }
                );
                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Player_Name),
                    new object[] { ft_player_info() }
                );
                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Tray_Text),
                    new object[] { ft_player_info() }
                );
                if ((player_x > 644 && player_x < 652 && player_y > 348 && player_y < 354)
                    || (player_x > 623 && player_x < 628 && player_y > 348 && player_y < 354))
                    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsJump, (byte)1);
                if (player_x > 551 && player_x < 563 && player_y > 530 && player_y < 542)
                    ini_Win_Choose.DlgAion("/Skill AFK");
//                if ((player_x > 616 && player_x < 639 && player_y > 330 && player_y < 344)
//                    || (player_x > 551 && player_x < 563 && player_y > 530 && player_y < 542)
//                    || (player_x > 1983.145142f - 8 && player_x < 1983.145142f + 8 && player_y > 1746.883667f - 8 && player_y < 1746.883667f + 8)
//                    || (player_x > 499.0f - 8 && player_x < 499.0 + 8 && player_y > 2039.9 - 8 && player_y < 2039.9 + 8))
             	if ((player_x > 616 && player_x < 639 && player_y > 330 && player_y < 344)
                	    || (player_x > 551 && player_x < 563 && player_y > 530 && player_y < 542))
                    ini_Win_Shulack.Dispatcher.Invoke(
                        new SetNone(ini_Win_Shulack.Set_Enable_Start),
                        new object[] { }
                    );
                else
                    ini_Win_Shulack.Dispatcher.Invoke(
                        new SetNone(ini_Win_Shulack.Set_Disable_Start),
                        new object[] { }
                    );
                ini_Win_Choose.ini_Win_Viewer.Dispatcher.Invoke(
                    new SetNone(ini_Win_Choose.ini_Win_Viewer.ActualiseValue),
                    new object[] { }
                );
                ini_Win_Choose.ini_Win_Viewer.Dispatcher.Invoke(
                    new SetNone(ini_Win_Choose.ini_Win_Status.Message_Status),
                    new object[] { }
                );
                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.ShowText),
                    new object[] { (SplMemory.ReadInt(
				SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
				+ (long)Offset.Cube.Size)-SplMemory.ReadInt(
			                    	SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
			                    	+ (long)Offset.Cube.Curent)).ToString() }
                );

                ini_Win_Shulack.Dispatcher.Invoke(
                    new SetString(ini_Win_Shulack.Set_Count),
                    new object[] {
                        ini_Win_Choose.ini_Boucle_Skills.count_shulack.ToString()
                    }
               );

                if (SplMemory.ReadWchar(AionProcess.Modules.Game + 0xED4458, 50) != "0")
                {
                    ini_Win_Shulack.Dispatcher.Invoke(
                        new SetString(ini_Win_Shulack.Set_Kinas),
                        new object[] {
                        (long.Parse(SplMemory.ReadWchar(AionProcess.Modules.Game + 0xED4458, 50).Replace(" ", "")) - long.Parse(kinas.Replace(" ", ""))).ToString()
                    }
                   );
                }

                Thread.Sleep(1000);
            }
        }

        void ft_reduc()
        {
            while (true)
            {
                ReleaseUnusedMemory();
                Thread.Sleep(60000);
            }
        }

        string ft_sec_to_date(int Time)
        {
            string result = "";
            Time -= 3600;
            int sec = Time % 60;
            int min = (Time / 60) % 60;
            int hour = (Time / 3600);

            result = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
            return result;
        }

        string ft_player_info()
        {
            string result = "";
            string name = SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30);
            int lvl = SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Lvl);

            result = string.Format("{0:00} ({1:00})", name, lvl);
            return result;
        }
    }
}
