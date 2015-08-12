/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/15/2014
 * Heure: 17:58
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Interaction logic for Win_Wiewer.xaml
	/// </summary>
	public partial class Win_Viewer : Window
	{
		public Win_Choose ini_Win_Choose = null;
		
		public Win_Viewer()
		{
			InitializeComponent();
			
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Win_Choose))
				{
					ini_Win_Choose = (window as Win_Choose);
				}
			}
			
			this.Left = ini_Win_Choose.ini_Settings.inMain.posxViewer;
			this.Top = 	ini_Win_Choose.ini_Settings.inMain.posyViewer;
		}
		
		void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (!ini_Win_Choose.ini_Win_Shulack.IsVisible)
				{
					ini_Win_Choose.ini_Win_Shulack.Show();
					ini_Win_Choose.ini_Win_Shulack.trayicon.Visible = false;
					this.Hide();
				}
			}
			else
			{
				DragMove();
			}
		}
		
		public void ActualiseValue()
		{
			Player_Name.Text = SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30).ToString();
			Player_Lvl.Text = SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Lvl).ToString();
			Player_Cube.Text = (SplMemory.ReadInt(
				SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
				+ (long)Offset.Cube.Size)-SplMemory.ReadInt(
			                    	SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Cube.BaseCount)
			                    	+ (long)Offset.Cube.Curent)).ToString();
			
			HP_Bar.Value = SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP) * 100 / SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.HP_Max);
			MP_Bar.Value = SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.MP) * 100 / SplMemory.ReadInt(AionProcess.Modules.Game+(long)Offset.Player.MP_Max);
		}
		
		void Window_LocationChanged(object sender, EventArgs e)
		{
			ini_Win_Choose.ini_Settings.inMain.posxViewer = (int)this.Left;
			ini_Win_Choose.ini_Settings.inMain.posyViewer = (int)this.Top;
		}
		
		void Button_Minimise_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
	}
}