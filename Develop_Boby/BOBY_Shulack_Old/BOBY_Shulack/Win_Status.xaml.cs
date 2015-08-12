/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 18/02/2014
 * Heure: 18:51
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Interop;
using System.Windows.Controls.Primitives;

using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices; // DllImport

using AddProcess;
using MemoryLib;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

using System.Net.Mail;

using System.ComponentModel;

namespace BOBY_Shulack
{
	/// <summary>
	/// Interaction logic for Win_Status.xaml
	/// </summary>
	public partial class Win_Status : Window
	{
		public Win_Status()
		{
			InitializeComponent();
		}
		
		public void Message_Status()
		{
			float player_x = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
			float player_y = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);
			
			if (SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30) == "")
			{
				this.Show();
				this.Top = SystemParameters.VirtualScreenHeight - this.Height-38;
				this.Left = SystemParameters.VirtualScreenWidth - this.Width+9;
				IntPtr hwnd = new WindowInteropHelper(this).Handle;
				Win32.makeTransparent(hwnd);
				this.tbLog1.Text = AionProcess.pid + "\n" + "Déconnection";
			}
			else if (player_x > 551 && player_x < 563 && player_y > 530 && player_y < 542)
			{
				this.Show();
				this.Top = SystemParameters.VirtualScreenHeight - this.Height-38;
				this.Left = SystemParameters.VirtualScreenWidth - this.Width+9;
				IntPtr hwnd = new WindowInteropHelper(this).Handle;
				Win32.makeTransparent(hwnd);
				this.tbLog1.Text =  SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30) + "\n" +
					"Début d'instance. (mort ?)";
			}
			else
				this.Hide();
		}
	}
}