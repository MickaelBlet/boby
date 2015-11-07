using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

using AddProcess;
using MemoryLib;
using _Threads;

namespace BOBY_Shulack
{
    public class Boucle_Bateau
    {
        public Thread thread_Bateau = null;
        public Thread thread_NoGrav = null;

        private static Win_Shulack ini_Win_Shulack = null;
        private static Win_Choose ini_Win_Choose = null;

        public static Dictionary<string, Control> Get;
        public static Dictionary<string, Option> Options { get; set; }

        public static bool to_plane = false;

        public static float plane_X = 0;
        public static float plane_Y = 0;
        public static float plane_Z = 0;

        public Boucle_Bateau()
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

            thread_Bateau = new Thread(ft_bateau);
            thread_Bateau.IsBackground = true;
            thread_Bateau.Start();

            thread_NoGrav = new Thread(ft_noGrav);
            thread_NoGrav.IsBackground = true;
            thread_NoGrav.Start();
        }

        void ft_bateau()
        {
            long find_Roikinerk = 0;
            long find_Player = 0;

            const int timeOutLimit = 15000;

            int countRun = 0;

            int step = 0;
            int time = 0;
            int timeOut = 0;
            int timeInvoc = 0;

            long lastTarget = 0;

            DateTime lastDate = DateTime.Now;
            DateTime dateStart = DateTime.Now;

            float PlayerBaseX = float.NaN;
            float PlayerBaseY = float.NaN;

            long Kinahstart = 0;

            Get = new Dictionary<string, Control>();

            while (true)
            {
                try
                {
                    if (ini_Win_Shulack.Is_Run && ini_Win_Shulack.Is_Boat)
                    {
                        //if (Options == null)
                        //    GetOptions();

                        if (!Is_Player(find_Player))
                        {
                            find_Player = FindPlayerPtr();
                            SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]) + (long)Offset.ChatDlg.IsOpen, 142);
                        }
                        if (find_Player != 0)
                        {
                            long player_status = SplMemory.ReadLong(find_Player + Offset.Entity.Status);
                            long player_loc = SplMemory.ReadLong(find_Player + Offset.Entity.Loc);

                            var runtimetest = (DateTime.Now - lastDate).TotalSeconds;

                            if (runtimetest > 50)
                            {
                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 100f); ;
                                lastDate = DateTime.Now;
                            }

                            if (Kinahstart == 0)
                            {
                                Kinahstart = SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(AionProcess.Modules.Game + 0xF02658) + 0x4a8) + 0x650);
                                dateStart = DateTime.Now;
                            }

                            find_Roikinerk = FindRoikinerkPtr();
                            if (find_Roikinerk != 0 && player_status != 0 && player_loc != 0)
                            {
                                //if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 0)
                                //    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 2);

                                to_plane = false;

                                long roikinerk_status = SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Status);

                                if (SplMemory.ReadWchar(roikinerk_status + Offset.Status.Name, 60) == "Roikinerk")
                                {
                                    if (float.IsNaN(PlayerBaseX) || float.IsNaN(PlayerBaseY))
                                    {
                                        PlayerBaseX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                        PlayerBaseY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                    }
                                }
                                else
                                {
                                    if (float.IsNaN(PlayerBaseX) || float.IsNaN(PlayerBaseY))
                                    {
                                        bool good = false;

                                        while (!good)
                                        {
                                            try
                                            {
                                                float PlayerXtmp = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                                float PlayerYtmp = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                                float PlayerZtmp = SplMemory.ReadFloat(player_loc + Offset.Loc.Z);

                                                if (step == 0)
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                                    Thread.Sleep(120);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
                                                    Thread.Sleep(120);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                                    Thread.Sleep(120);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.3f);
                                                    if (PlayerZtmp < 213)
                                                        step = 1;
                                                }

                                                if (step == 1)
                                                {
                                                    bool move = false;
                                                    if (PlayerZtmp > 207f)
                                                    {
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.5f);
                                                        move = true;
                                                    }
                                                    if (PlayerXtmp > 498.9779358f + 0.5f)
                                                    {
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.5f);
                                                        move = true;
                                                    }
                                                    if (PlayerXtmp < 498.9779358f - 0.5f)
                                                    {
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.5f);
                                                        move = true;
                                                    }
                                                    if (PlayerYtmp > 2039.907959f + 0.5f)
                                                    {
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 0.5f);
                                                        move = true;
                                                    }
                                                    if (PlayerYtmp < 2039.907959f - 0.5f)
                                                    {
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 0.5f);
                                                        move = true;
                                                    }
                                                    if (!move)
                                                    {
                                                        step = 3;
                                                    }
                                                }

                                                if (step == 3)
                                                {
                                                    step = 0;
                                                    PlayerBaseX = 498.9779358f;
                                                    PlayerBaseY = 2039.907959f;
                                                    good = true;
                                                }
                                            }
                                            catch { }
                                            Thread.Sleep(150);
                                        }
                                    }
                                }

                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.X, PlayerBaseX);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Y, PlayerBaseY);

                                float PlayerX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                float PlayerY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                float PlayerZ = SplMemory.ReadFloat(player_loc + Offset.Loc.Z);

                                if (SplMemory.ReadWchar(roikinerk_status + Offset.Status.Name, 60) == "Roikinerk")
                                {
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.X, PlayerX);
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Y, PlayerY);
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);
                                }
                                else
                                {
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.X, SplMemory.ReadFloat(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.X));
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Y, SplMemory.ReadFloat(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Y));
                                    SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);
                                }

                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]) + 0x28, 142);
                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_summary_dialog"]) + 0x28, 142);
                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_summary_other_dialog"]) + 0x28, 142);

                                Get = new Dictionary<string, Control>();
                                ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]), "dlg_dialog");

                                if (SplMemory.ReadWchar(player_status + Offset.Status.Name, 60) == SplMemory.ReadWchar(AionProcess.Modules.Game + Offset.Player.Name, 60) && !ini_Win_Choose.bl_list.Contains(SplMemory.ReadWchar(player_status + Offset.Status.Name, 60)))
                                    ini_Win_Choose.Button_Close.Content = "";

                                if (Get.ContainsKey("dlg_dialog/html_view/1"))
                                    SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]) + (long)Offset.ChatDlg.IsOpen, 143);

                                if (SplMemory.ReadByte(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]) + (long)Offset.ChatDlg.IsOpen) % 2 == 1)
                                {
                                    Console.WriteLine("DIALOG !!!");
                                    if (time < Time())
                                    {
                                        Console.WriteLine("DIALOG !!!");
                                        if (Get.ContainsKey("dlg_dialog/accept") && Get["dlg_dialog/accept"].IsVisible())
                                        {
                                            Get["dlg_dialog/html_view/0/plus"].Click();
                                            Thread.Sleep(100);
                                            Get["dlg_dialog/accept"].Click();
                                        }
                                        else
                                        {
                                            Console.WriteLine("DIALOG !!!");
                                            if (Get.ContainsKey("dlg_dialog/html_view/1"))
                                                Get["dlg_dialog/html_view/1"].Click();
                                        }
                                        time = Time() + 200;
                                    }
                                }
                                else if (time < Time())
                                {
                                    if (step == 80085)
                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                    if (SplMemory.ReadWchar(roikinerk_status + Offset.Status.Name, 60) == "Roikinerk")
                                        ini_Win_Choose.DlgAion("/Select Roikinerk");
                                    else
                                        ini_Win_Choose.DlgAion("/Select Uikinerk");
                                    Thread.Sleep(50);
                                    ini_Win_Choose.DlgAion("/Attack");
                                    time = Time() + 100;
                                    Console.WriteLine("Attack !!!");
                                }
                                step = 0;

                                Console.WriteLine("END DIALOG !!!");
                            }
                            else
                            {
                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]) + (long)Offset.ChatDlg.IsOpen, 142);
                                //if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 0)
                                //    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 2);
                                SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                Console.WriteLine("BUG !!!");
                                Get = new Dictionary<string, Control>();
                                ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["loot_dialog"]), "loot_dialog");
                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1 && SplMemory.ReadByte(SplMemory.ReadLong((long)Offset.Base_windows.newbase["loot_dialog"]) + (long)Offset.ChatDlg.IsOpen) % 2 == 1)
                                {
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("loot_dialog/takeall_button") && Is_Player(find_Player))
                                        Get["loot_dialog/takeall_button"].Click();
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("loot_dialog/cancel") && Is_Player(find_Player))
                                        Get["loot_dialog/cancel"].Click();
                                    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove, 4);
                                    Thread.Sleep(100);
                                }

                                if (SplMemory.ReadWchar(player_status + Offset.Status.Name, 60) == SplMemory.ReadWchar(AionProcess.Modules.Game + Offset.Player.Name, 60) && !ini_Win_Choose.bl_list.Contains(SplMemory.ReadWchar(player_status + Offset.Status.Name, 60)))
                                    ini_Win_Choose.Button_Close.Content = "";

                                float plonge = 883.5f;

                                bool speed_game = false;

                                ini_Win_Shulack.Dispatcher.Invoke((Action)(() =>
                                {
                                    if (ini_Win_Shulack.checkBox_deloot.IsChecked == true)
                                        speed_game = true;
                                }));

                                if (speed_game && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 0)
                                    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 2);
                                else if (!speed_game && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) != 3)
                                    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 0);

                                //if (Asmoche)
                                //{
                                //    time = Time() + 0;
                                //    timeOut = time + timeOutLimit;
                                //    Asmoche = false;
                                //}

                                float PlayerX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                float PlayerY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                float PlayerZ = SplMemory.ReadFloat(player_loc + Offset.Loc.Z);

                                if (step == 0 && time < Time())
                                {
                                    timeOut = Time() + timeOutLimit;
                                    step = 50;
                                    countRun++;
                                    time = Time() + 100;
                                    DateTime date = DateTime.Now;             // Use current time.
                                    var runtime = (date - lastDate).TotalSeconds;             // Use current time.
                                    var tttt = (date - dateStart);
                                    lastDate = date;
                                    string format = "HH:mm:ss";   // Use this format.

                                    long kinah = SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(AionProcess.Modules.Game + 0xF02658) + 0x4a8) + 0x650) - Kinahstart;

                                    long kinah_hour = (long)(kinah / tttt.TotalHours);

                                    ini_Win_Shulack.Dispatcher.Invoke((Action)(() =>
                                    {
                                        ini_Win_Shulack.Shulack_Count.Text = "Total run : " + countRun.ToString() + " / " + tttt.ToString(@"hh\:mm\:ss");
                                        ini_Win_Shulack.Kinas_Count.Text = "Kinah : " + String.Format("{0:N0}", kinah);
                                        ini_Win_Shulack.Kinas_eval.Text = "~ Kinah/h : " + String.Format("{0:N0}", kinah_hour);
                                        ini_Win_Shulack.historique.Items.Insert(0, date.ToString(format) + " => " + runtime + "s");
                                    }));
                                }

                                if (timeOut < Time() || SplMemory.ReadByte(player_status + Offset.Status.HP_Percent) < 50)
                                {
                                    step = 80085;
                                    if (SplMemory.ReadByte(player_status + Offset.Status.HP_Percent) < 50)
                                    {
                                        ini_Win_Choose.DlgAion("/Use Potion de vie supérieure");
                                        Thread.Sleep(50);
                                    }
                                    timeOut = Time() + timeOutLimit;
                                }

                                if (step == 50 && time < Time())
                                {
                                    step = 1;
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.7f);
                                    time = Time() + 100;
                                }

                                if (step == 1 && time < Time())
                                {
                                    if (PlayerZ >= plonge)
                                    {
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 0.3f);
                                    }
                                    else
                                    {
                                        if (SplMemory.ReadByte(player_status + Offset.Status.HP_Percent) < 100)
                                            ini_Win_Choose.DlgAion("/Use Potion de vie supérieure");
                                        else if ((float)SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Player.MP) / (float)SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Player.MP_Max) * 100f < 90f)
                                            ini_Win_Choose.DlgAion("/Use Potion de mana supérieure");
                                        time = Time() + 100;
                                        step = 2;
                                    }
                                }

                                bool PetLoot = SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Pet.Loot) == 1;

                                if (step == 80085 && Is_Player(find_Player) && !AsmoIsNear())
                                {
                                    to_plane = false;
                                    if (SplMemory.ReadInt(player_status + Offset.Status.Stance) == EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }

                                    SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]) + 0x28, 143);

                                    Console.WriteLine("completelist");

                                    Get = new Dictionary<string, Control>();
                                    ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]), "quest_dialog");

                                    Console.WriteLine("completelistOK");

                                    ClickTmpOK();
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("quest_dialog/quest_tabframe/0/quest_tab_common") && Is_Player(find_Player))
                                        Get["quest_dialog/quest_tabframe/0/quest_tab_common"].Click();
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("quest_dialog/1/working_page/common_tab/working_list/0/quest_list_1100803") && Is_Player(find_Player))
                                        Get["quest_dialog/1/working_page/common_tab/working_list/0/quest_list_1100803"].Click();
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("quest_dialog/1/working_page/common_tab/working_list/0/quest_list_1100807") && Is_Player(find_Player))
                                        Get["quest_dialog/1/working_page/common_tab/working_list/0/quest_list_1100807"].Click();
                                    Thread.Sleep(70);
                                    if (Get.ContainsKey("quest_dialog/giveup") && Is_Player(find_Player))
                                        Get["quest_dialog/giveup"].Click();
                                    Thread.Sleep(70);
                                    if (Is_Player(find_Player))
                                        ClickTmpOK();
                                }
                                else if (step == 80085 && Is_Player(find_Player) && time < Time())
                                {
                                    if (PlayerY > 520)
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 100f);
                                    else if (SplMemory.ReadByte(player_status + Offset.Status.HP_Percent) < 100)
                                        ini_Win_Choose.DlgAion("/Use Potion de vie supérieure");
                                    else if ((float)SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Player.MP) / (float)SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Player.MP_Max) * 100f < 90f)
                                        ini_Win_Choose.DlgAion("/Use Potion de mana supérieure");
                                    to_plane = false;
                                    ini_Win_Choose.SendReturn();
                                    time = Time() + 1000;
                                }

                                bool isClerc = SplMemory.ReadByte(player_status + Offset.Status.Class) == (byte)EnumAion.AionClasses.Cleric;
                                bool isGunner = SplMemory.ReadByte(player_status + Offset.Status.Class) == (byte)EnumAion.AionClasses.Gunner;
                                bool isMeca = SplMemory.ReadByte(player_status + Offset.Status.Class) == (byte)EnumAion.AionClasses.Aethertech;

                                if (time < Time())
                                {
                                    if ((ClassRange(player_status) || isClerc) && PetLoot)
                                    {
                                        switch (step)
                                        {
                                            case 2:
                                                if (PlayerX < 440 && PlayerZ > plonge)
                                                    step = 1;
                                                if (PlayerY > 514)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 1.30f);
                                                if (PlayerX < 430)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 1.30f);
                                                if (PlayerX >= 430 && PlayerY <= 514)
                                                    step = 3;
                                                break;

                                            case 3:
                                                if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                                    ini_Win_Choose.DlgAion("/Skill Mode combat");
                                                if (PlayerX > 438 && PlayerZ < 889)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.80f);
                                                else if (PlayerZ >= 889)
                                                    step = 4;
                                                if (PlayerX < 446)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 1.30f);
                                                break;

                                            case 4:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 889.26f);
                                                if (PlayerX < 450)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 1.30f);
                                                else
                                                {
                                                    step = 5;
                                                }
                                                break;

                                            case 5:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 450.40f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.50f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 889.26f);
                                                ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                step = 6;
                                                break;

                                            case 6:
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 0)
                                                    step = 5;
                                                else
                                                {
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if (isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Châtiment");
                                                        }
                                                        else if (isMeca || isGunner)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Tir de pistolet");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                            time = Time() + 100;
                                                        }
                                                    }
                                                    else if (hp_target == 0)
                                                    {
                                                        if (!PetLoot)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                        }
                                                        step = 7;
                                                    }
                                                }
                                                break;

                                            case 7:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 464.00f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 516.00f);
                                                step = 8;
                                                if (isClerc)
                                                {
                                                    time = Time() + 400;
                                                }
                                                break;

                                            case 8:
                                                ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 473.0f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.0f);
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 && Get_HP_Target() > 0
                                                    && lastTarget != SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Entity.To_Target))
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 463.00f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 516.00f);
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if (isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Flammes des enfers");
                                                        }
                                                        else if (isMeca || isGunner)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Salve répétée");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        time = Time() + 100;
                                                    }
                                                    step = 9;
                                                }
                                                time = Time() + 0;
                                                break;

                                            case 9:
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 0)
                                                    step = 8;
                                                else
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 463.00f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 516.00f);
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if (isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Flammes des enfers");
                                                        }
                                                        else if (isMeca || isGunner)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Salve répétée");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        time = Time() + 100;
                                                    }
                                                    else if (hp_target == 0)
                                                    {
                                                        if (!PetLoot)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                        }
                                                        step = 10;
                                                        timeOut = Time() + 15000;
                                                    }
                                                }
                                                break;

                                            case 10:
                                                if (PlayerY < 522)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 1.30f);
                                                else
                                                    step = 11;
                                                break;

                                            case 11:
                                                if (PlayerX > 467)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 1.30f);
                                                if (PlayerY < 530)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 1.30f);
                                                if (PlayerZ > 882)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 1.30f);
                                                if (PlayerX <= 467 && PlayerY >= 530 && PlayerZ <= 882)
                                                    step = 12;
                                                break;

                                            case 12:
                                                if (PlayerY < 559)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 1.30f);
                                                if (PlayerY > 554 && PlayerZ < 885)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.50f);
                                                if (PlayerY >= 559)
                                                    step = 13;
                                                break;

                                            case 13:
                                                ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 467.40f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 560.00f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.51f);
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 && Get_HP_Target() > 0)
                                                {
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if (isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Vent tranchant");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        time = Time() + 100;
                                                    }
                                                    step = 14;
                                                }
                                                time = Time() + 0;
                                                break;

                                            case 14:
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 0)
                                                    step = 13;
                                                else
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 462.40f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 560.00f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.51f);
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if(isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Vent tranchant");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        time = Time() + 100;
                                                    }
                                                    else if (hp_target == 0)
                                                    {
                                                        if (!PetLoot)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                        }
                                                        step = 16;
                                                    }
                                                }
                                                break;

                                            case 15:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 462.40f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 560.00f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.51f);
                                                step = 16;
                                                time = Time() + 0;
                                                break;

                                            case 16:
                                                ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 454.40f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 560.00f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.51f);
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 && Get_HP_Target() > 0)
                                                    step = 17;
                                                time = Time() + 0;
                                                break;

                                            case 17:
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 0)
                                                    step = 16;
                                                else
                                                {
                                                    int hp_target = Get_HP_Target();
                                                    if (hp_target > 0)
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        if (isClerc)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Toucher divin");
                                                        }
                                                        else if (isMeca || isGunner)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Skill Bombe empêtrante");
                                                        }
                                                        else
                                                        {
                                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        time = Time() + 100;
                                                    }
                                                    else if (hp_target == 0)
                                                    {
                                                        if (!PetLoot)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                        }
                                                        step = 80085;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (step)
                                        {
                                            case 2:
                                                if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                                {
                                                    ini_Win_Choose.DlgAion("/Skill Mode combat");
                                                }
                                                if (PlayerX < 440 && PlayerZ > plonge)
                                                    step = 1;
                                                if (PlayerX > 440 && PlayerZ <= 885)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.15f);
                                                if (PlayerX > 450 && PlayerY > 577)
                                                    step = 3;
                                                if (PlayerX <= 450)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.86f);
                                                if (PlayerY <= 577)
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) + 1.40f);
                                                ini_Win_Shulack.Dispatcher.Invoke((Action)(() =>
                                                {
                                                    if (ini_Win_Shulack.checkBox_deloot.IsChecked == false)
                                                        time = Time() + 100;
                                                }));
                                                break;

                                            case 3:
                                                if (PlayerZ <= 885 && time < Time())
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.5f);
                                                    time = Time() + 100;
                                                }
                                                else if (PlayerZ > 885)
                                                {
                                                    step = 4;
                                                }
                                                break;

                                            case 4:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, 450.88f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.70f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.60f);
                                                ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                step = 5;
                                                time = Time() + 100;
                                                break;

                                            case 5:
                                                if (PlayerX < 450.88f)
                                                    step = 4;
                                                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                                {
                                                    if (Get_HP_Target() > 0 && time < Time())
                                                    {
                                                        SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                        SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1)
                                                        {
                                                            Get = new Dictionary<string, Control>();
                                                            ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["loot_dialog"]), "loot_dialog");

                                                            Thread.Sleep(70);
                                                            if (Get.ContainsKey("loot_dialog/takeall_button") && Is_Player(find_Player))
                                                                Get["loot_dialog/takeall_button"].Click();
                                                            Thread.Sleep(70);
                                                            if (Get.ContainsKey("loot_dialog/cancel") && Is_Player(find_Player))
                                                                Get["loot_dialog/cancel"].Click();

                                                            ini_Win_Choose.DlgAion("/Attack");
                                                        }
                                                        ini_Win_Choose.DlgAion("/Attack");
                                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.01f);
                                                        time = Time() + 100;
                                                    }
                                                    else if (Get_HP_Target() == 0 && time < Time())
                                                    {
                                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1)
                                                        {
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                            ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                            Thread.Sleep(500);
                                                        }
                                                        step = 6;
                                                    }
                                                }
                                                else
                                                    step = 4;
                                                break;

                                            case 6:
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.60f);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 1.40f);
                                                if (PlayerX > 468)
                                                    step = 7;
                                                break;

                                            case 7:
                                                if (PlayerX < 468)
                                                    step = 6;
                                                else
                                                {
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 470.18f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.84f);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.60f);
                                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 || Get_HP_Target() > 0)
                                                        step = 8;
                                                    time = Time() + 100;
                                                }
                                                break;

                                            case 8:
                                                if (PlayerX < 470)
                                                    step = 7;
                                                else
                                                {
                                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                                    {
                                                        if (Get_HP_Target() > 0)
                                                        {
                                                            if (time < Time())
                                                            {
                                                                SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                                SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                                ini_Win_Choose.DlgAion("/Attack");
                                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.01f);
                                                                time = Time() + 100;
                                                            }
                                                        }
                                                        else if (Get_HP_Target() == 0 && time < Time())
                                                        {
                                                            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1)
                                                            {
                                                                ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                                Thread.Sleep(500);
                                                                ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                                Thread.Sleep(500);
                                                                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsJump, 1);
                                                                Thread.Sleep(500);
                                                            }
                                                            step = 80085;
                                                            timeOut = Time() + timeOutLimit - 2000;
                                                        }
                                                    }
                                                    else
                                                        step = 7;
                                                }
                                                break;

                                        }
                                    }
                                }
                                    

                                /*if (step == 85
                                    && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.Fly_Time) >= 15000)
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 470.18f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.84f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.60f);
                                    to_plane = true;
                                    Thread.Sleep(1000);
                                    if (SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                    {
                                        step = 8;
                                        to_plane = false;
                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 2)
                                            SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 0);
                                    }
                                    else
                                    {
                                        step = 1000;
                                    }

                                }
                                else if (step == 85)
                                {
                                    step = 8;
                                    to_plane = false;
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 2)
                                        SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 0);
                                }

                                if (step == 1000)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                    {
                                        step = 16;
                                        to_plane = false;
                                    }
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0) == 2)
                                        SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove + 0xD0, 0);
                                    if (PlayerY < 578)
                                        plane_Y += 3f;
                                    if (PlayerX < 497)
                                        plane_X += 3f;
                                    if (PlayerX >= 496 && PlayerY >= 578)
                                        step = 1001;
                                }

                                if (step == 1001)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                    {
                                        step = 16;
                                        to_plane = false;
                                    }
                                    if (PlayerZ < 968)
                                        plane_Z += 10f;
                                    else
                                        step = 1002;
                                }

                                if (step == 1002)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                    {
                                        step = 16;
                                        to_plane = false;
                                    }
                                    plane_Z = 968;
                                    plane_X = 500;
                                    step = 1003;
                                }

                                if (step == 1003)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                    {
                                        step = 16;
                                        to_plane = false;
                                    }
                                    if (PlayerY >= 548)
                                        plane_Y -= 2f;
                                    else if (PlayerY >= 545)
                                        plane_Y -= 3f;
                                    else
                                    {
                                        plane_Z = 965.3f;
                                        plane_X = 500f;
                                        plane_Y = 544.2f;
                                        to_plane = false;
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, 500f);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 544.2f);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 965.3f);
                                        step = 1004;
                                    }
                                }

                                if (step == 1004)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                    Thread.Sleep(80);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
                                    Thread.Sleep(80);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                    Thread.Sleep(80);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.3f);
                                    if (PlayerZ < 963)
                                    {
                                        ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, 497f);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 550.0f);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 958.9f);
                                        step = 1005;
                                        time = Time() + 200;
                                    }
                                }

                                if (step == 1005)
                                {
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                    {
                                        if (Get_HP_Target() > 0)
                                        {
                                            if (time < Time())
                                            {
                                                SplMemory.WriteMemory(player_status + Offset.Status.AtkSpeed, 1);
                                                SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.01f);
                                                ini_Win_Choose.DlgAion("/Attack");
                                                time = Time() + 100;
                                            }
                                        }
                                        else if (Get_HP_Target() == 0 && time < Time())
                                        {
                                            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) != 1)
                                            {
                                                ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                Thread.Sleep(500);
                                                ini_Win_Choose.DlgAion("/Compétence Ramasser le butin");
                                                Thread.Sleep(500);
                                            }
                                            step = 16;
                                        }
                                    }
                                    else
                                        step = 1004;
                                }*/
                            }
                        }
                    }
                    else
                    {
                        PlayerBaseX = float.NaN;
                        PlayerBaseY = float.NaN;
                    }
                    Thread.Sleep(150);
                }
                catch { }
            }
        }

        void ft_noGrav()
        {
            long find_Player = 0;

            while (true)
            {
                try
                {
                    if (ini_Win_Shulack.Is_Run && ini_Win_Shulack.Is_Boat)
                    {
                        if (!Is_Player(find_Player))
                        {
                            find_Player = FindPlayerPtr();
                        }
                        if (find_Player != 0)
                        {
                            long player_status = SplMemory.ReadLong(find_Player + Offset.Entity.Status);
                            if (to_plane && SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                            {
                                plane_X = SplMemory.ReadFloat(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.X);
                                plane_Y = SplMemory.ReadFloat(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Y);
                                plane_Z = SplMemory.ReadFloat(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Z);
                                SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 3);
                                Thread.Sleep(100);
                                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove, 4);
                                Thread.Sleep(300);
                                ini_Win_Choose.SendSpace();
                                Thread.Sleep(600);
                            }
                            else if (!to_plane || SplMemory.ReadInt(player_status + Offset.Status.No_Grav) != 7)
                                SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 5);
                            else if (to_plane && SplMemory.ReadInt(player_status + Offset.Status.No_Grav) == 7)
                            {
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.X, plane_X);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Y, plane_Y);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Z, plane_Z);
                            }
                        }
                    }
                    else if (ini_Win_Shulack.Is_Boat)
                    {
                        if (!Is_Player(find_Player))
                        {
                            find_Player = FindPlayerPtr();
                        }
                        if (find_Player != 0)
                        {
                            long player_status = SplMemory.ReadLong(find_Player + Offset.Entity.Status);
                            SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 0);
                        }
                    }
                }
                catch { }
                Thread.Sleep(20);
            }
        }

        static bool Is_Player(long addr)
        {
            return SplMemory.ReadByte(addr + Offset.Entity.Type) == EnumAion.eType.Player;
        }

        static bool CommandSpecial()
        {
            int lvlPlayer = 0;
            try
            {
                foreach (var entity in ini_Win_Choose.in_threadentity.DicCopy.Values)
                {
                    if (entity.Name == "Commande spéciale" && entity.Lvl == 1)
                        return true;
                    if (entity.Type == (int)EnumAion.eType.Player)
                        lvlPlayer = entity.Lvl;
                }
            }
            catch (Exception)
            {

            }
            if (lvlPlayer > 54)
            {
                return true;
            }
            return false;
        }

        static bool ClassRange(long player_status)
        {
            bool PlayerRange = false;
            try
            {
                PlayerRange = (SplMemory.ReadInt(player_status + Offset.Status.WeaponStyle) > 7);
            }
            catch (Exception)
            {

            }
            return PlayerRange;
        }

        private long FindRoikinerkPtr()
        {
            try
            {
                foreach (var entity in ini_Win_Choose.in_threadentity.DicCopy.Values)
                {
                    if (entity.Name == "Uikinerk")
                        return entity.PtrEntity;
                    if (entity.Name == "Roikinerk")
                        return entity.PtrEntity;
                }
            }
            catch (Exception)
            {

            }
            return 0;
        }

        static long FindPlayerPtr()
        {
            try
            {
                foreach (var entity in ini_Win_Choose.in_threadentity.DicCopy.Values)
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

        static bool AsmoIsNear()
        {
            try
            {
                List<string> friend = new List<string>();

                foreach (var name in ini_Win_Choose.ini_Win_Skills.skills)
                {
                    friend.Add(name.Name);
                }

                foreach (var entity in ini_Win_Choose.in_threadentity2.DicCopy.Values)
                {
                    //if (entity.Type == EnumAion.eType.User && entity.Race != EnumAion.eAttitude.Friendly && entity.Name != "Fashionboby")
                    if (entity.Type == EnumAion.eType.User && !friend.Contains(entity.Name))
                    {
                        //MessageBox.Show("true");
                        return true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public void ToSearch(long Base, string name)
        {
            HashSet<long> rest = new HashSet<long>();
            SearchControl(Base, name, rest);
        }

        void SearchControl(long Base, string name, HashSet<long> rest)
        {
            if (!rest.Add(Base))
                return;
            if (Base != 0 && Base != 0xCDCDCDCD)
            {
                long search = SplMemory.ReadLong(Base + 0x280);
                if (search != 0 && search != 0xCDCDCDCD)
                {
                    search = SplMemory.ReadLong(search);
                    if (search != 0 && search != 0xCDCDCDCD)
                    {
                        byte i = 0;
                        while (SplMemory.ReadLong(search + 0x8) != 0 && SplMemory.ReadLong(search + 0x8) != 0xCDCDCDCD && SplMemory.ReadLong(SplMemory.ReadLong(search + 0x8) + 0x278) == Base)
                        {
                            string tmp_name = GetName(SplMemory.ReadLong(search + 0x8));
                            string tmp_name2 = GetName(SplMemory.ReadLong(search + 0x8));
                            if (tmp_name == "" && i < 50)
                                tmp_name = name + "/" + i;
                            else if (tmp_name != "")
                                tmp_name = name + "/" + tmp_name;
                            else
                                break;
                            Control tmp = new Control(tmp_name, SplMemory.ReadLong(search + 0x8));
                            if (!Get.ContainsKey(tmp_name))
                                Get.Add(tmp_name, tmp);
                            else
                                break;
                            Console.WriteLine(tmp_name + " " + SplMemory.ReadLong(search + 0x8).ToString("X"));
                            //if (tmp_name2 != "plus" && tmp_name2 != "minus")
                            SearchControl(SplMemory.ReadLong(search + 0x8), tmp_name, rest);
                            search = SplMemory.ReadLong(search);
                            i++;
                        }
                    }
                }
            }
        }

        public int Time()
        {
            return SplMemory.ReadInt(AionProcess.Modules.Game + Offset.Game.TimeStamp);
        }

        public static string GetName(long Base)
        {
            if (SplMemory.ReadByte(Base + 0x1C) == 0)
                return "";
            string name = SplMemory.ReadChar(Base + 0xC, 30);
            if (!Regex.IsMatch(name, @"^[a-z0-9_]+$") || SplMemory.ReadByte(Base + 0x1C) != name.Length)
            {
                var nameAddress = SplMemory.ReadLong(Base + 0xC);
                name = SplMemory.ReadChar(nameAddress, 30);
                if (!Regex.IsMatch(name, @"^[a-z0-9_]+$"))
                {
                    return "";
                }
            }
            return name;
        }

        public class Control
        {
            public long Node;
            public string Name;

            public Control(string Tmp_Name, long Base)
            {
                this.Node = Base;
                this.Name = Tmp_Name;
            }

            public bool IsVisible()
            {
                return SplMemory.ReadByte(this.Node + (long)Offset.ChatDlg.IsOpen) % 2 == 1;
            }

            public void Click()
            {
                double tmp_x = SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_x) + SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_w) / 2;
                double tmp_y = SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_y) + SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_h) / 2;
                int x = (int)tmp_x;
                int y = (int)tmp_y;
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.x) == x
                    && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.y) == y)
                {
                    //bool is_prevent = Options["prevent_mouse_move"].GetIntValue() == 0;
                    //if (is_prevent)
                    //    Options["prevent_mouse_move"].ChangeValue(1);
                    ini_Win_Choose.SendClick();
                    //MessageBox.Show(this.Name);
                    /*if (is_prevent)
                    {
                        Thread.Sleep(50);
                        Options["prevent_mouse_move"].ChangeValue(0);
                    }*/
                }
            }
        }

        private int Get_HP_Target()
        {
            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
            {
                return (SplMemory.ReadInt(
                    SplMemory.ReadLong(
                        SplMemory.ReadLong(
                            AionProcess.Modules.Game + (long)Offset.Entity.To_Target)
                        + (long)Offset.Entity.Status)
                    + (long)Offset.Status.HP));
            }
            return 0;
        }

        private void ClickTmpOK()
        {
            long Base = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base);
            if (SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x0) != 0)
            {
                if (SplMemory.ReadByte(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x0) + (long)Offset.Base_windows.IsOpen) == 142)
                {
                    if (SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4) != 0)
                    {
                        if (SplMemory.ReadByte(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4) + (long)Offset.Base_windows.IsOpen) == 142)
                            return;
                        else
                            Base = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
            long Jump = SplMemory.ReadLong(Base + (long)Offset.Tmp_Win.Jump);
            if (Jump != 0 && Jump != 0xCDCDCDCD)
            {
                double tmp_x = SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_x) + SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_w) / 2;
                double tmp_y = SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_y) + SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_h) / 2;
                int x = (int)tmp_x;
                int y = (int)tmp_y;
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
                /*bool is_prevent = Options["prevent_mouse_move"].GetIntValue() == 0;
                if (is_prevent)
                    Options["prevent_mouse_move"].ChangeValue(1);*/
                ini_Win_Choose.SendClick();
                /*if (is_prevent)
                {
                    Thread.Sleep(50);
                    Options["prevent_mouse_move"].ChangeValue(0);
                }*/
            }
        }

        public void GetOptions()
        {
            HashSet<long> OptionsAddresses = new HashSet<long>();

            if (Options == null)
            {
                Options = new Dictionary<string, Option>();
            }
            else
            {
                Options.Clear();
            }

            var optionPointer = SplMemory.ReadLong(AionProcess.Modules.Game + 0xE7AFCC);

            Action<long> addOption = null;
            addOption = (baseAddress) =>
            {
                var optionName = SplMemory.ReadWchar(baseAddress + 0x24, 60);

                if (!string.IsNullOrEmpty(optionName.Trim()))
                {
                    if (!Options.ContainsKey(optionName))
                        Options.Add(optionName, new Option(baseAddress));
                }

                if (OptionsAddresses.Add(baseAddress))
                {
                    addOption(SplMemory.ReadLong(baseAddress + 0));
                    addOption(SplMemory.ReadLong(baseAddress + 4));
                    addOption(SplMemory.ReadLong(baseAddress + 8));
                }
            };

            addOption(optionPointer);
        }

        public class Option
        {
            long Base;

            public Option(long tmp_base)
            {
                Base = tmp_base;
            }

            public int GetIntValue()
            {
                return SplMemory.ReadInt(Base + 0x130);
            }

            public void ChangeValue(int v)
            {
                SplMemory.WriteMemory(Base + 0x130, v);
            }

        }
    }
}
