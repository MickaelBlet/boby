/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/14/2014
 * Heure: 03:29
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; // DllImport

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Description of Boucle_Delete.
	/// </summary>
	public class Boucle_Delete
	{
		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern IntPtr GetForegroundWindow();
		
		public Thread thread_Delete = null;
		public delegate bool GetBool();
		public delegate void SetString(List<string> list);
		
		private Win_Shulack ini_Win_Shulack = null;
		private Win_Choose ini_Win_Choose = null;
		
		private bool WindowsIs()
		{
			bool isactive = false;
			if (AionProcess.whandle != IntPtr.Zero)
			{
				IntPtr MWhandle = AionProcess.whandle;
				IntPtr CWhandle = GetForegroundWindow();
				if (MWhandle == CWhandle)
					isactive =  true;
			}
			return isactive;
		}
		
		public Boucle_Delete()
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Win_Choose))
				{
					ini_Win_Choose = (window as Win_Choose);
				}
			}
			
			ini_Win_Shulack = ini_Win_Choose.ini_Win_Shulack;
			
			thread_Delete = new Thread(ft_chain);
			thread_Delete.IsBackground = true;
			thread_Delete.Start();
		}
		
		public void ft_chain()
		{
			while (true)
			{
				try
				{
					//long CubeBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"]);
					
					List<string> Name_User = new List<string>();

                    foreach (var entity in ini_Win_Choose.in_threadentity2.DicCopy.Values)
                    {
                        //if (entity.Type == EnumAion.eType.User && entity.Race != EnumAion.eAttitude.Friendly && entity.Name != "Fashionboby")
                        if (entity.Type == EnumAion.eType.User)
                        {
                            //MessageBox.Show("true");
                            Name_User.Add(entity.Name);
                        }
                    }
					
					ini_Win_Choose.ini_Win_Skills.Dispatcher.Invoke(
						new SetString(ini_Win_Choose.Set_List_Skill_Item),
						new object[] { Name_User }
					);
					/*if (SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Entity.Is_Target) == 1)
					{
						//test;
					}
					else
					{
						Thread.Sleep(500);
						SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["system_menu_dialog"])+(long)Offset.Base_windows.IsOpen, (byte)142);
						SplMemory.WriteMemory(SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"])+(long)Offset.Base_windows.IsOpen, (byte)143);
						if (IsCheckRecycleLoot())
							ini_Win_Choose.ini_Html._Delete_Loot(ItemCube, SplMemory.ReadInt(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount) + (long)Offset.Cube.Size)
							                                     - SplMemory.ReadInt(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount) + (long)Offset.Cube.Curent));
					}*/
				}
				catch{}
				Thread.Sleep(1000);
				//if (!WindowsIs())
				//{
				//	ini_Win_Choose.SendMousseMove(3000, 3000);
				//	SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, 3000);
				//	SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, 3000);
				//}
			}
		}
		
		private bool IsCheckRecycleLoot()
		{
			object result = ini_Win_Shulack.Dispatcher.Invoke(
				new GetBool(ini_Win_Shulack.IsCheck_Recycle_Loot),
				new object[] {}
			);
			if (result.ToString() == "True")
				return true;
			else
				return false;
		}
	}
}
