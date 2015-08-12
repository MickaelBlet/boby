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
            
            const int timeOutLimit = 23000;

            int step = 0;
            int time = 0;
            int timeOut = 0;

            bool Asmoche = false;
            
            float PlayerBaseX = float.NaN;
            float PlayerBaseY = float.NaN;

            while (true)
            {
                try
                {
                    if (ini_Win_Shulack.Is_Run && ini_Win_Shulack.Is_Boat)
                    {
                        if (Options == null)
                            GetOptions();

                        if (!Is_Player(find_Player))
                        {
                            find_Player = FindPlayerPtr();
                        }
                        if (find_Player != 0)
                        {
                            long player_status = SplMemory.ReadLong(find_Player + Offset.Entity.Status);
                            long player_loc = SplMemory.ReadLong(find_Player + Offset.Entity.Loc);

                            find_Roikinerk = FindRoikinerkPtr();
                            if (find_Roikinerk != 0 && player_status != 0 && player_loc != 0)
                            {
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
                                		PlayerBaseX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                		PlayerBaseY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                	}
                                }
                                
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.X, PlayerBaseX);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Player + Offset.Entity.Loc) + Offset.Loc.Y, PlayerBaseY);

                                float PlayerX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                float PlayerY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                float PlayerZ = SplMemory.ReadFloat(player_loc + Offset.Loc.Z);

                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.X, PlayerX);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Y, PlayerY);
                                SplMemory.WriteMemory(SplMemory.ReadLong(find_Roikinerk + Offset.Entity.Loc) + Offset.Loc.Z, PlayerZ);

                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]) + 0x28, 142);
                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_summary_dialog"]) + 0x28, 142);
                                SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_summary_other_dialog"]) + 0x28, 142);

                                Get = new Dictionary<string, Control>();
                                ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]), "dlg_dialog");

                                if (Get.ContainsKey("dlg_dialog/accept") && SplMemory.ReadByte(SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]) + (long)Offset.ChatDlg.IsOpen) % 2 == 1)
                                {
                                    if (time < Time())
                                    {
                                        if (Get["dlg_dialog/accept"].IsVisible())
                                        {
                                            Get["dlg_dialog/html_view/0/plus"].Click();
                                            Thread.Sleep(100);
                                            Get["dlg_dialog/accept"].Click();
                                        }
                                        else
                                        {
                                            if (Get.ContainsKey("dlg_dialog/html_view/1"))
                                                Get["dlg_dialog/html_view/1"].Click();
                                        }
                                        time = Time() + 400;
                                    }
                                }
                                else if (time < Time())
                                {
                                    if (SplMemory.ReadWchar(roikinerk_status + Offset.Status.Name, 60) == "Roikinerk")
                                        ini_Win_Choose.DlgAion("/Select Roikinerk");
                                    else
                                        ini_Win_Choose.DlgAion("/Select Uikinerk");
                                    Thread.Sleep(50);
                                    ini_Win_Choose.DlgAion("/Attack");
                                    time = Time() + 100;
                                }
                                step = 0;
                                timeOut = Time() + timeOutLimit;

                                if (AsmoIsNear())
                                    Asmoche = true;
                            }
                            else
                            {
                                SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 5);

                                float plonge = 883.5f;

                                ini_Win_Shulack.Dispatcher.Invoke((Action)(() =>
                                {
                                    if (ini_Win_Shulack.checkBox_deloot.IsChecked == false)
                                        plonge = 881;
                                }));

                                if (Asmoche)
                                {
                                    time = Time() + 180000;
                                    timeOut = time + timeOutLimit;
                                    Asmoche = false;
                                }

                                float PlayerX = SplMemory.ReadFloat(player_loc + Offset.Loc.X);
                                float PlayerY = SplMemory.ReadFloat(player_loc + Offset.Loc.Y);
                                float PlayerZ = SplMemory.ReadFloat(player_loc + Offset.Loc.Z);

                                if (timeOut < Time())
                                {
                                	step = 16;
                                    timeOut = Time() + timeOutLimit;
                                }

                                if (step == 0 && time < Time())
                                {
                                    step = 50;
                                    time = Time() + 100;
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
                                        step = 2;
                                    }
                                }

                                if (step == 2 && time < Time())
                                {
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
                                }

                                if (step == 3)
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }
                                    if (PlayerZ <= 885 && time < Time())
                                    {
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.5f);
                                        time = Time() + 100;
                                    }
                                    else if (PlayerZ > 885)
                                    {
                                        step = 4;
                                    }
                                }

                                if (step == 4 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 450.88f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.70f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.50f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    step = 5;
                                    time = Time() + 100;
                                }

                                if (step == 5 && time < Time())
                                {
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                    {
                                        if (Get_HP_Target() > 0 && time < Time())
                                        {
                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                            SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.1f);
                                            time = Time() + 1500;
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
                                            step = 55;
                                        }
                                    }
                                    else
                                        step = 4;
                                }

                                if (step == 55 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 460.88f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.70f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.50f);
                                    step = 6;
                                    time = Time() + 200;
                                }

                                if (step == 6 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 470.18f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 577.84f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 885.50f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 || Get_HP_Target() > 0)
                                        step = 7;
                                    time = Time() + 100;
                                }

                                if (step == 7)
                                {
                                    if (PlayerX < 470)
                                        step = 6;
                                    else
                                    {
                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                        {
                                            if (Get_HP_Target() > 0)
                                            {
                                                if (time < Time())
                                                {
                                                    SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.1f);
                                                    time = Time() + 1500;
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
                                                step = 8;
                                                timeOut = Time() + timeOutLimit - 2000;
                                            }
                                        }
                                        else
                                            step = 7;
                                    }
                                }

                                if (step == 8 && ClassRange() && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Pet.Loot) == 1)
                                {
                                	if (PlayerZ > 882 && PlayerY >= 540)
                                	{
                                		SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                	}
                                	else if (PlayerZ < 892 && PlayerY < 540)
                                	{
                                		SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.5f);
                                	}
                                	if (PlayerY >= 522 && time < Time())
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 1.30f);
                                	else if (PlayerY < 522){
                                		step = 9;
                                	}
                                }
                                else if (step == 8 && CommandSpecial())
                                	step = 338;
                                else if (step == 8)
                                	step = 16;

                                if (step == 9)
                                {
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.5f);
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 0.1f);
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.1f);
                                	if (PlayerZ > 892)
                                	{
                                		step = 93;
                                		time = Time() + 200;
                                	}
                                }
                                
                                if (step == 93)
                                {
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 1.30f);
                                	if (PlayerY < 514)
                                	{
                                		step = 11;
                                		time = Time() + 200;
                                	}
                                }
                                
                                if (step == 11 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 473.0f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.0f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 892.0f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    step = 112;
                                    time = Time() + 100;
                                }
                                
                                if (step == 112)
                                {
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                    Thread.Sleep(120);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
                                    Thread.Sleep(120);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
                                    Thread.Sleep(120);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.3f);
                                	if (PlayerZ < 890)
                                	{
                                		step = 12;
                                		time = Time() + 200;
                                	}
                                }

                                if (step == 12 && time < Time())
                                {
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                    {
                                        if (Get_HP_Target() > 0 && time < Time())
                                        {
                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                            SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.1f);
                                            time = Time() + 1500;
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
                                            step = 13;
                                        }
                                    }
                                    else
                                        step = 11;
                                }
                                
                                if (step == 13 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 463.00f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.00f);
                                    step = 14;
                                    time = Time() + 200;
                                }

                                if (step == 14 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 453.00f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.00f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 || Get_HP_Target() > 0)
                                        step = 152;
                                    time = Time() + 100;
                                }
                                
                                if (step == 152)
                                {
                                	if (PlayerZ > 890)
                                	{
	                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
	                                    Thread.Sleep(120);
	                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
	                                    Thread.Sleep(120);
	                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.3f);
	                                    Thread.Sleep(120);
	                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.3f);
                                	}
                                	else
                                	{
                                		step = 15;
                                		time = Time() + 200;
                                	}
                                }

                                if (step == 15)
                                {
                                    if (PlayerX > 453)
                                        step = 14;
                                    else
                                    {
                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                        {
                                            if (Get_HP_Target() > 0)
                                            {
                                                if (time < Time())
                                                {
                                                    SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 0.1f);
                                                    time = Time() + 1500;
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
                                            step = 15;
                                    }
                                }
                                
                                if (step == 338)
                                {
                                	SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) - 1f);
                                	if (PlayerZ < 875)
                                	{
                                		step = 339;
                                		time = Time() + 200;
                                	}
                                }
                                
                                if (step == 339)
                                {
                                	if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }
                                	if (PlayerY >= 515 && time < Time())
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Y, SplMemory.ReadFloat(player_loc + Offset.Loc.Y) - 1.20f);
                                	else if (PlayerY < 515){
                                		SplMemory.WriteMemory(player_loc + Offset.Loc.Z, 875f);
                                		step = 3310;
                                	}
                                    ini_Win_Shulack.Dispatcher.Invoke((Action)(() =>
                                    {
                                        if (ini_Win_Shulack.checkBox_deloot.IsChecked == false)
                                            time = Time() + 100;
                                    }));
                                }

                                if (step == 3310)
                                {
                                	if (SplMemory.ReadInt(player_status + Offset.Status.Stance) != EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }
                                    if (PlayerZ <= 878 && time < Time())
                                    {
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) + 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.3f);
                                        Thread.Sleep(120);
                                        SplMemory.WriteMemory(player_loc + Offset.Loc.X, SplMemory.ReadFloat(player_loc + Offset.Loc.X) - 0.3f);
                                        time = Time() + 100;
                                    }
                                    else if (PlayerZ > 878)
                                    {
                                        step = 3311;
                                    }
                                }
                                
                                if (step == 3311 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 473.0f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.0f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    step = 3312;
                                    time = Time() + 100;
                                }

                                if (step == 3312 && time < Time())
                                {
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                    {
                                        if (Get_HP_Target() > 0 && time < Time())
                                        {
                                            SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                            SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.1f);
                                            time = Time() + 1500;
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
                                            step = 3313;
                                        }
                                    }
                                    else
                                        step = 3311;
                                }
                                
                                 if (step == 3313 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 463.00f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.00f);
                                    step = 3314;
                                    time = Time() + 200;
                                }

                                if (step == 3314 && time < Time())
                                {
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.X, 453.00f);
                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Y, 514.00f);
                                    ini_Win_Choose.DlgAion("/Select Coffre au trésor");
                                    if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0 || Get_HP_Target() > 0)
                                        step = 3315;
                                    time = Time() + 100;
                                }

                                if (step == 3315)
                                {
                                    if (PlayerX > 453)
                                        step = 3314;
                                    else
                                    {
                                        if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) != 0)
                                        {
                                            if (Get_HP_Target() > 0)
                                            {
                                                if (time < Time())
                                                {
                                                    SplMemory.WriteMemory(player_status + Offset.Status.State, 1);
                                                    SplMemory.WriteMemory(player_loc + Offset.Loc.Z, SplMemory.ReadFloat(player_loc + Offset.Loc.Z) + 0.1f);
                                                    time = Time() + 1500;
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
                                            step = 3315;
                                    }
                                }
                                
                                if (step == 16 && Is_Player(find_Player))
                                {
                                    if (SplMemory.ReadInt(player_status + Offset.Status.Stance) == EnumAion.eStance.Combat)
                                    {
                                        ini_Win_Choose.DlgAion("/Skill Mode combat");
                                    }

                                    SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 5);

                                    SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]) + 0x28, 143);

                                    Get = new Dictionary<string, Control>();
                                    ToSearch(SplMemory.ReadLong((long)Offset.Base_windows.newbase["quest_dialog"]), "quest_dialog");

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
                            SplMemory.WriteMemory(player_status + (long)Offset.Status.No_Grav, 5);
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
        
        static bool ClassRange()
        {
        	bool PlayerRange = false;
        	try
            {
                foreach (var entity in ini_Win_Choose.in_threadentity.DicCopy.Values)
                {
                    if (entity.Type == (int)EnumAion.eType.Player)
                    	PlayerRange = (SplMemory.ReadInt(SplMemory.ReadLong(entity.PtrEntity + Offset.Entity.Status) + Offset.Status.WeaponStyle) > 7);
                }
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
                foreach (var entity in ini_Win_Choose.in_threadentity.DicCopy.Values)
                {
                    if (entity.Type == EnumAion.eType.User && entity.Race != EnumAion.eAttitude.Friendly)
                        return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public void ToSearch(long Base, string name)
        {
            SearchControl(Base, name);
        }

        void SearchControl(long Base, string name)
        {
            if (Base != 0 && Base != 0xCDCDCDCD)
            {
                long search = SplMemory.ReadLong(Base + 0x270);
                if (search != 0 && search != 0xCDCDCDCD)
                {
                    search = SplMemory.ReadLong(search);
                    if (search != 0 && search != 0xCDCDCDCD)
                    {
                        byte i = 0;
                        while (SplMemory.ReadLong(search + 0x8) != 0 && SplMemory.ReadLong(search + 0x8) != 0xCDCDCDCD)
                        {
                            string tmp_name = GetName(SplMemory.ReadLong(search + 0x8));
                            if (tmp_name == "")
                                tmp_name = name + "/" + i;
                            else
                                tmp_name = name + "/" + tmp_name;
                            Control tmp = new Control(tmp_name, SplMemory.ReadLong(search + 0x8));
                            if (!Get.ContainsKey(tmp_name))
                                Get.Add(tmp_name, tmp);
                            SearchControl(SplMemory.ReadLong(search + 0x8), tmp_name);
                            search = SplMemory.ReadLong(search);
                            i++;
                            if (i > 50)
                                break;
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
                double tmp_x = SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_x) + SplMemory.ReadDouble(this.Node + (long)Offset.Cube.ListW) / 2;
                double tmp_y = SplMemory.ReadDouble(this.Node + (long)Offset.Html.Bt_y) + SplMemory.ReadDouble(this.Node + (long)Offset.Cube.ListH) / 2;
                int x = (int)tmp_x;
                int y = (int)tmp_y;
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
                if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.x) == x
                    && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.y) == y)
                {
                    bool is_prevent = Options["prevent_mouse_move"].GetIntValue() == 0;
                    if (is_prevent)
                        Options["prevent_mouse_move"].ChangeValue(1);
                    ini_Win_Choose.SendClick();
                    if (is_prevent)
                    {
                        Thread.Sleep(50);
                        Options["prevent_mouse_move"].ChangeValue(0);
                    }
                }
            }
        }

        private int Get_HP_Target()
        {
            return (SplMemory.ReadInt(
                SplMemory.ReadLong(
                    SplMemory.ReadLong(
                        AionProcess.Modules.Game + (long)Offset.Entity.To_Target)
                    + (long)Offset.Entity.Status)
                + (long)Offset.Status.HP));
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
                bool is_prevent = Options["prevent_mouse_move"].GetIntValue() == 0;
                if (is_prevent)
                    Options["prevent_mouse_move"].ChangeValue(1);
                ini_Win_Choose.SendClick();
                if (is_prevent)
                {
                    Thread.Sleep(50);
                    Options["prevent_mouse_move"].ChangeValue(0);
                }
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
