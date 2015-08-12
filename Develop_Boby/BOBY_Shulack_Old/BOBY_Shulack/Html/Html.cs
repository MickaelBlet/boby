/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/13/2014
 * Heure: 20:45
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Description of Html.
	/// </summary>
	public class Html
	{
		public delegate void SetString(List<string> list);
		
		private Win_Choose ini_Win_Choose = null;
		
		public Html()
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Win_Choose))
				{
					ini_Win_Choose = (window as Win_Choose);
				}
			}
		}
		
		public void Clic_Bt(int n)
		{
			long ptr_bt_base = Get_Base_Bt();
			for (int i = 1; i <= (n + 1);i++)
				ptr_bt_base = SplMemory.ReadLong(ptr_bt_base);
			long tmp = SplMemory.ReadLong(ptr_bt_base + (long)Offset.Html.Bt_Jump);
			double tmp_x = SplMemory.ReadDouble(tmp + (long)Offset.Html.Bt_x) + SplMemory.ReadDouble(tmp + (long)Offset.Html.Bt_w)/2;
			double tmp_y = SplMemory.ReadDouble(tmp + (long)Offset.Html.Bt_y) + SplMemory.ReadDouble(tmp + (long)Offset.Html.Bt_h)/2;
			int x = (int)tmp_x;
			int y = (int)tmp_y;
			SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
			SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
			ini_Win_Choose.SendClick();
		}
		
		private long Get_Base_Bt()
		{
			long result = SplMemory.ReadLong((long)Offset.Base_windows.newbase["dlg_dialog"]);
			result = SplMemory.ReadLong(result + (long)Offset.Html.Jump);
			result = SplMemory.ReadLong(result + (long)Offset.Html.Bt_Base);
			return result;
		}
		
		public void _Delete_Loot(string[] ItemCube, int CubeCount)
		{
			int x = 0;
			int y = 0;
			
			long CubeBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["inventory_dialog"]);
			
			double ItemSize = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List)
				+ (long)Offset.Cube.ItemListW);
			
			int[] UseCubeX = new int[135];
			int[] UseCubeY = new int[135];
			
			double ListX = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List)
				+ (long)Offset.Cube.ListX);

			double ListY1 = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List)
				+ (long)Offset.Cube.ListY);
			double ListY2 = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List+ 4)
				+ (long)Offset.Cube.ListY);
			double ListY3 = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 8)
				+ (long)Offset.Cube.ListY);
			double ListY4 = SplMemory.ReadDouble(
				SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 12)
				+ (long)Offset.Cube.ListY);
            double ListY5 = SplMemory.ReadDouble(
                SplMemory.ReadLong(CubeBase + (long)Offset.Cube.Base_List + 16)
                + (long)Offset.Cube.ListY);
			
			UseCubeX[0] = (int)(ListX + ItemSize/2);
			UseCubeY[0] = (int)(ListY1 + ItemSize/2);
			UseCubeX[27] = (int)(ListX + ItemSize/2);
			UseCubeY[27] = (int)(ListY2 + ItemSize/2);
			UseCubeX[54] = (int)(ListX + ItemSize/2);
			UseCubeY[54] = (int)(ListY3 + ItemSize/2);
			UseCubeX[81] = (int)(ListX + ItemSize/2);
			UseCubeY[81] = (int)(ListY4 + ItemSize/2);
            UseCubeX[108] = (int)(ListX + ItemSize/2);
            UseCubeY[108] = (int)(ListY5 + ItemSize/2);
			
			for (int l = 0; l < 5;l++)
			{
				for (int j = 0; j < 3;j++)
				{
					for (int i = 0; i < 9;i++)
					{
						UseCubeX[i+9*j+l*27] = (int)(UseCubeX[l*27] + ItemSize*i);
						UseCubeY[i+9*j+l*27] = (int)(UseCubeY[l*27] + ItemSize*j);
					}
				}
			}
			
			for (int i = 0; i < 135;i++)
			{
				if (ItemCube[i] != "")
				{
					foreach (var key in ini_Win_Choose.ini_Win_Loot.loot)
					{
						if (ItemCube[i] == key.Name)
						{
							x = UseCubeX[i];
							y = UseCubeY[i];
							break ;
						}
					}
				}
				if (x != 0 || y != 0)
					break ;
			}
			
			if (x == 0 && y == 0 && CubeCount <= 0)
			{
				foreach (var key in ini_Win_Choose.ini_Win_Loot.loot2)
				{
					for (int i = 0; i < 135;i++)
					{
						if (ItemCube[i] != "")
						{
							if (ItemCube[i] == key.Name)
							{
								x = UseCubeX[i];
								y = UseCubeY[i];
								break ;
							}
						}
					}
					if (x != 0 || y != 0)
						break ;
				}
			}
			
			if (x == 0 && y == 0)
				return ;

			SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
			SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
			ini_Win_Choose.SendLeftMousseDown();
			ini_Win_Choose.SendLeftMousseUp(3000, 3000);
			Thread.Sleep(1000);
			long Base = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base);
			if (SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x0) != 0)
			{
				if (SplMemory.ReadByte(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x0) + (long)Offset.Base_windows.IsOpen) == 142)
				{
					if (SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4) != 0)
					{
						if (SplMemory.ReadByte(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4) + (long)Offset.Base_windows.IsOpen) == 142)
							return ;
						else
							Base = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Tmp_Win.Base + 0x4);
					}
					else
					{
						return ;
					}
				}
			}
			else
			{
				return ;
			}
			long Jump = SplMemory.ReadLong(Base + (long)Offset.Tmp_Win.Jump);
			if (Jump != 0 && Jump != 0xCDCDCDCD)
			{
				double tmp_x = SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_x) + SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_w)/2;
				double tmp_y = SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_y) + SplMemory.ReadDouble(Jump + (long)Offset.Tmp_Win.Bt_h)/2;
				x = (int)tmp_x;
				y = (int)tmp_y;
				SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.x, x);
				SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Game.y, y);
				ini_Win_Choose.SendClick();
			}
		}
	}
}
