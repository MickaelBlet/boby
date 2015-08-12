/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/13/2014
 * Heure: 06:07
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Description of Boucle_Skills.
	/// </summary>
	public class Boucle_Skills
	{
		private Ability_List ini_Ability_List = null;
		private Win_Skills ini_Win_Skills = null;
		private Win_Choose ini_Win_Choose = null;
		private Win_Shulack ini_Win_Shulack = null;
		public Thread thread_Chain = null;
		
		public int []Buff_player;

        public long count_shulack = 0;
		
		public string[] list_repetable = {
			"Annihilation",
			"Salve spirituelle",
			"Tir rapide d'âme",
			"Salve rapide",
			"Salve répétée",
			"Tir rapide répétés",
			"Salve inhibitrice",
			"Tourment répété"
		};
		
		public string regex_list = null;
		
		public long PlayerPtr;
		
		public Boucle_Skills()
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Win_Choose))
				{
					ini_Win_Choose = (window as Win_Choose);
				}
			}
			
			ini_Ability_List = ini_Win_Choose.ini_Ability_List;
			ini_Win_Skills = ini_Win_Choose.ini_Win_Skills;
			ini_Win_Shulack = ini_Win_Choose.ini_Win_Shulack;
			
			regex_list = String.Format("({0})", String.Join("|", list_repetable));
			
			thread_Chain = new Thread(ft_chain);
			thread_Chain.IsBackground = true;
			thread_Chain.Start();
		}
		
		void ft_chain()
		{
			bool launch = false;
            bool target_is_a_live = false;
			
			while (true)
			{
				try{
                    if (ini_Win_Shulack.Is_Run && !ini_Win_Shulack.Is_Assist && !ini_Win_Shulack.Is_Boat)
					{
						if (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP) == 0)
						{
							Thread.Sleep(10000);
							ini_Win_Shulack.Show_Game();
							while (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP) == 0)
							{
								Rez();
								Thread.Sleep(1000);
							}
							ini_Win_Shulack.Is_Run = false;
						}
						else
						{
							if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.ID) == 0)
							{
								ini_Win_Shulack.Show_Game();
								if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.TimeDisconnect) - 3600 < 10)
									System.Windows.Forms.MessageBox.Show("Déconnection ?\n25h", "!!!!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None,
									                                     System.Windows.Forms.MessageBoxDefaultButton.Button1, (System.Windows.Forms.MessageBoxOptions)0x40000);
								else
									System.Windows.Forms.MessageBox.Show("Déconnection ?", "!!!!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None,
									                                     System.Windows.Forms.MessageBoxDefaultButton.Button1, (System.Windows.Forms.MessageBoxOptions)0x40000);
								ini_Win_Shulack.ini_Win_Loot.Save_List();
								ini_Win_Shulack.ini_Win_Skills.Save_List();
								ini_Win_Choose.ini_Settings.Save();
								Environment.Exit(0);
							}
							if (SplMemory.ReadByte(PlayerPtr + (long)Offset.Entity.Type) != EnumAion.eType.Player)
								this.PlayerPtr = FindPlayerPtr();
							if (PlayerPtr != 0)
							{
								long PlayerEntity = SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status);
								ListBuff(PlayerEntity);
								int AtkSpeedBase = SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.AtkSpeed);
								int AtkSpeed = AtkSpeedBase - AtkSpeedBase * 10 / 100;
								SplMemory.WriteMemory(PlayerEntity + (long)Offset.Status.AtkSpeed, AtkSpeed);
								if (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Entity.Is_Target) == 1
								    && Get_HP_Target() > 0
								    && Get_Name_Target() == "Guetteur shulack")
								{
                                    target_is_a_live = true;
									ini_Ability_List.UpdateChainsManager();
									if (ini_Ability_List.ChainsSkills != null && !launch)
									{
										foreach (var keyValue in ini_Ability_List.ChainsSkills.OrderByDescending(item => item.Value.Address))
										{
											var keyValue2 = keyValue.Value;
											if (SplMemory.ReadInt(keyValue2.Address + (long)Offset.Chain.Timeout) == 0x0
                                                && SplMemory.ReadInt(keyValue2.Address + (long)Offset.Chain.IsElapsed) == 0x0)
											{
												string Name = Id_To_Name(keyValue2.AbilityID);
												if (Name != "")
												{
													ini_Win_Choose.DlgAion("/Skill " + Name);
													launch = true;
												}
											}
										}
									}
									ini_Ability_List.UpdateChainsManager2();
									if (ini_Ability_List.ChainsSkills2 != null && !launch)
									{
										foreach (var keyValue in ini_Ability_List.ChainsSkills2.OrderByDescending(item => item.Value.Address))
										{
											var keyValue2 = keyValue.Value;
											if (SplMemory.ReadInt(keyValue2.Address + (long)Offset.Chain.Timeout) == 0x0
											   && SplMemory.ReadInt(keyValue2.Address + (long)Offset.Chain.IsElapsed) == 0x0)
											{
												string Name = Id_To_Name(keyValue2.AbilityID);
												if (Name != "")
												{
													ini_Win_Choose.DlgAion("/Skill " + Name);
													launch = true;
												}
											}
										}
									}
									if (!launch)
									{
										foreach (var keyValue in ini_Win_Skills.skills)
										{
											string Name = Buff_To_Name(keyValue.Name);
											if (Name != "")
											{
												if ((keyValue.Type == "AllTime" || keyValue.Type == "OnFight") && Check_HP_MP(keyValue.HP, keyValue.MP))
												{
													ini_Win_Choose.DlgAion("/Skill " + Name);
													launch = true;
													break;
												}
											}
										}
									}
								}
								else if (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Entity.Is_Target) == 1)
								{
									ini_Win_Choose.SendEscape();
									Thread.Sleep(500);
								}
								else
								{
                                    if (target_is_a_live)
                                    {
                                        target_is_a_live = false;
                                        count_shulack++;
                                    }
									ini_Win_Choose.Start_Place();
									if (!Buff_Is_Active(19520))
									{
										ini_Win_Shulack.Is_Run = false;
										ini_Win_Choose.Recharge();
										ini_Win_Shulack.Is_Run = true;
									}
									if (!launch)
									{
										foreach (var keyValue in ini_Win_Skills.skills)
										{
											string Name = Buff_To_Name(keyValue.Name);
											if (Name != "")
											{
												if ((keyValue.Type == "AllTime") && Check_HP_MP(keyValue.HP, keyValue.MP))
												{
													ini_Win_Choose.DlgAion("/Skill " + Name);
													launch = true;
													break;
												}
											}
										}
									}
									if (!launch)
									{
										foreach (var keyValue in ini_Win_Skills.skills)
										{
											string Name = Buff_To_Name(keyValue.Name);
											if (Name != "")
											{
												if (keyValue.Type == "OffFight" && Check_HP_MP(keyValue.HP, keyValue.MP))
												{
													ini_Win_Choose.DlgAion("/Skill " + Name);
													launch = true;
													break;
												}
											}
										}
									}
								}
								Check_Item();
							}
						}
					}
				}
				catch{}
				Thread.Sleep(250);
				launch = false;
			}
		}
		
		public void Rez()
		{
			long Base = SplMemory.ReadLong((long)Offset.Base_windows.newbase["resurrect_dialog"]);
			if (SplMemory.ReadByte(Base + (long)Offset.Base_windows.IsOpen) != 14)
			{
				long Jump = SplMemory.ReadLong(Base + (long)Offset.Resurrect.Jump);
				if (Jump != 0 && Jump != 0xCDCDCDCD)
				{
					double tmp_x = SplMemory.ReadDouble(Jump + (long)Offset.Resurrect.Bt_x) + SplMemory.ReadDouble(Jump + (long)Offset.Resurrect.Bt_w)/2;
					double tmp_y = SplMemory.ReadDouble(Jump + (long)Offset.Resurrect.Bt_y) + SplMemory.ReadDouble(Jump + (long)Offset.Resurrect.Bt_h)/2;
					SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, (int)tmp_x);
					SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, (int)tmp_y);
					ini_Win_Choose.SendClick();
				}
			}
		}
		
		public string Id_To_Name(int id)
		{
			if (ini_Ability_List.Abilities != null)
			{
				foreach (var keyValue in ini_Ability_List.Abilities.Values)
				{
					if (id == keyValue.ID)
					{
						if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp)
						   > SplMemory.ReadInt(keyValue.Address + (long)Offset.Ability.CooldownEnd))
							return (keyValue.Name);
						else if (Regex.IsMatch(keyValue.Name, regex_list))
							return (keyValue.Name);
					}
				}
			}
			return ("");
		}
		
		public string Skill_To_Name(string name)
		{
			if (ini_Ability_List.Abilities != null)
			{
				foreach (var keyValue in ini_Ability_List.Abilities.Values)
				{
					if (keyValue.Name.Contains(name))
					{
						if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp)
						   > SplMemory.ReadInt(keyValue.Address + (long)Offset.Ability.CooldownEnd))
							return (keyValue.Name);
					}
				}
			}
			return ("");
		}
		
		public string Buff_To_Name(string name)
		{
			if (ini_Ability_List.Abilities != null)
			{
				foreach (var keyValue in ini_Ability_List.Abilities.Values)
				{
					if (keyValue.Name.Contains(name))
					{
						//MessageBox.Show(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp).ToString());
						//MessageBox.Show(SplMemory.ReadInt(keyValue.Address + (long)Offset.Ability.CooldownEnd).ToString());
						if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp)
						   > SplMemory.ReadInt(keyValue.Address + (long)Offset.Ability.CooldownEnd))
						{
							if (!Buff_Is_Active(keyValue.ID))
								return (keyValue.Name);
						}
					}
				}
			}
			return ("");
		}
		
		int ListBuffType(long _PtrEntity, long list, int index)
		{
			int lenght = 0;
			for (long j = SplMemory.ReadLong(_PtrEntity + list); j < SplMemory.ReadLong(_PtrEntity + list + 4); j += 0x4)
				lenght++;
			for (int i = 0; i < lenght; i++)
			{
				this.Buff_player[index] = SplMemory.ReadInt(SplMemory.ReadLong(SplMemory.ReadLong(_PtrEntity + list) + i * 0x4) + 0x4);
				index++;
			}
			return index;
		}
		
		public void ListBuff(long _PtrEntity)
		{
			int lenghtBuff = SplMemory.ReadInt(_PtrEntity + (long)Offset.Status.BuffCount);
			if (lenghtBuff > 0)
			{
				this.Buff_player = new int[lenghtBuff];
				int index = 0;
				for (long i = 0; i < 0x80; i += 0x10)
					index = ListBuffType(_PtrEntity, (long)Offset.Status.BuffArray + i, index);
			}
			else
			{
				this.Buff_player = new int[1];
				this.Buff_player[0] = 0;
			}
		}
		
		public bool Buff_Is_Active(int ID)
		{
			for (int i = 0; i < this.Buff_player.Length; i++)
			{
				if (this.Buff_player[i] == ID)
					return true;
			}
			return false;
		}
		
		public bool Check_HP_MP(int HP, int MP)
		{
			if (HP == 0 && MP == 0)
				return (true);
			int pourcentHP = SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP) * 100 / SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP_Max);
			int pourcentMP = SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.MP) * 100 / SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.MP_Max);
			if (HP != 0)
			{
				if (pourcentHP < HP)
					return (true);
			}
			if (MP != 0)
			{
				if (pourcentMP < MP)
					return (true);
			}
			return (false);
		}
		
		private long FindPlayerPtr()
		{
			long entityMap = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.EntityList.Pointer);
			long entityArray = SplMemory.ReadLong(entityMap + (long)Offset.EntityList.Array);
			
			return TraverseNodePlayer(SplMemory.ReadLong(entityArray));
		}
		
		private long TraverseNodePlayer(long nodePtr)
		{
			try
			{
				long entityPtr = SplMemory.ReadLong(nodePtr + 12);
                if (SplMemory.ReadByte(entityPtr + (long)Offset.Entity.Type) == EnumAion.eType.Player)
					return entityPtr;
				long rightPtr = SplMemory.ReadLong(nodePtr + 4);
				if (rightPtr != 0 && rightPtr != 0xCDCDCDCD)
				{
					return (TraverseNodePlayer(rightPtr));
				}
			}
			catch (Exception)
			{
				// uh oh, changing memory structure?
				// lets just back away slowly.
			}
			return 0;
		}
		
		private int Get_HP_Target()
		{
			return (SplMemory.ReadInt(
				SplMemory.ReadLong(
					SplMemory.ReadLong(
						AionProcess.Modules.Game+(long)Offset.Entity.To_Target)
					+ (long)Offset.Entity.Status)
				+(long)Offset.Status.HP));
		}
		
		private string Get_Name_Target()
		{
			if (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Entity.Is_Target) == 1)
			{
				long tmp = SplMemory.ReadLong(SplMemory.ReadLong(AionProcess.Modules.Game+(long)Offset.Entity.To_Target) + (long)Offset.Entity.Status);
				if (tmp != 0 && tmp != 0xCDCDCDCD)
					return (SplMemory.ReadWchar(tmp +(long)Offset.Status.Name, 60));
			}
			return "";
		}
		
		private void Check_Item()
		{
			long CubeBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"]);
			
			string ItemCube = "";
			int Item_CD = -1;
			
			for (int i=0;i < 27;i++)
			{
				long Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + i * 4);
				if (Item != 0 && Item != 0xCDCDCDCD)
				{
					Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
				}
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
				if (Item != 0 && Item != 0xCDCDCDCD)
					ItemCube = SplMemory.ReadWchar(Item, 100);
				else
					ItemCube = "";
				foreach (var keyValue in ini_Win_Skills.skills)
				{
					if (keyValue.Type == "Item" && Check_HP_MP(keyValue.HP, keyValue.MP))
					{
						if (keyValue.Name == ItemCube && Item_CD != -1)
						{
							if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
								ini_Win_Choose.DlgAion("/Use " + keyValue.Name);
						}
					}
				}
				Item_CD = -1;
				Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 4);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + i * 4);
				if (Item != 0 && Item != 0xCDCDCDCD)
				{
					Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
				}
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
				if (Item != 0 && Item != 0xCDCDCDCD)
					ItemCube = SplMemory.ReadWchar(Item, 100);
				else
					ItemCube = "";
				foreach (var keyValue in ini_Win_Skills.skills)
				{
					if (keyValue.Type == "Item" && Check_HP_MP(keyValue.HP, keyValue.MP))
					{
						if (keyValue.Name == ItemCube && Item_CD != -1)
						{
							if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
								ini_Win_Choose.DlgAion("/Use " + keyValue.Name);
						}
					}
				}
				Item_CD = -1;
				Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 8);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + i * 4);
				if (Item != 0 && Item != 0xCDCDCDCD)
				{
					Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
				}
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
				if (Item != 0 && Item != 0xCDCDCDCD)
					ItemCube = SplMemory.ReadWchar(Item, 100);
				else
					ItemCube = "";
				foreach (var keyValue in ini_Win_Skills.skills)
				{
					if (keyValue.Type == "Item" && Check_HP_MP(keyValue.HP, keyValue.MP))
					{
						if (keyValue.Name == ItemCube && Item_CD != -1)
						{
							if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
								ini_Win_Choose.DlgAion("/Use " + keyValue.Name);
						}
					}
				}
				Item_CD = -1;
				Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 12);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + i * 4);
				if (Item != 0 && Item != 0xCDCDCDCD)
				{
					Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
				}
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
				if (Item != 0 && Item != 0xCDCDCDCD)
					Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
				if (Item != 0 && Item != 0xCDCDCDCD)
					ItemCube = SplMemory.ReadWchar(Item, 100);
				else
					ItemCube = "";
				foreach (var keyValue in ini_Win_Skills.skills)
				{
					if (keyValue.Type == "Item" && Check_HP_MP(keyValue.HP, keyValue.MP))
					{
						if (keyValue.Name == ItemCube && Item_CD != -1)
						{
							if(SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
								ini_Win_Choose.DlgAion("/Use " + keyValue.Name);
						}
					}
				}
				Item_CD = -1;
                Item = SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 16);
                if (Item != 0 && Item != 0xCDCDCDCD)
                    Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ListItem);
                if (Item != 0 && Item != 0xCDCDCDCD)
                    Item = SplMemory.ReadLong(Item + i * 4);
                if (Item != 0 && Item != 0xCDCDCDCD)
                {
                    Item_CD = SplMemory.ReadInt(Item + (long)Offset.Cube.ItemCD);
                    Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName1);
                }
                if (Item != 0 && Item != 0xCDCDCDCD)
                    Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName2);
                if (Item != 0 && Item != 0xCDCDCDCD)
                    Item = SplMemory.ReadLong(Item + (long)Offset.Cube.ItemName3);
                if (Item != 0 && Item != 0xCDCDCDCD)
                    ItemCube = SplMemory.ReadWchar(Item, 100);
                else
                    ItemCube = "";
                foreach (var keyValue in ini_Win_Skills.skills)
                {
                    if (keyValue.Type == "Item" && Check_HP_MP(keyValue.HP, keyValue.MP))
                    {
                        if (keyValue.Name == ItemCube && Item_CD != -1)
                        {
                            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Game.TimeStamp) > Item_CD || Item_CD == 0)
                                ini_Win_Choose.DlgAion("/Use " + keyValue.Name);
                        }
                    }
                }
			}
		}
	}
}
