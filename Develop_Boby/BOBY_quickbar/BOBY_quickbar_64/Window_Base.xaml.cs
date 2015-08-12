/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 08/05/2014
 * Heure: 19:46
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
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using System.IO;

using AddProcess;
using MemoryLib;
using Ini;

namespace BOBY_quickbar
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window_Base : Window
	{
		string version = "64";
		
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int X;
			public int Y;
			public int Width;
			public int Height;
		}

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);
		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		
		public double m_MouseX = 0;
		public double m_MouseY = 0;
		
		public int Offset_ctrl = 0;
		public int Offset_alt = 0;
		public int Offset_extra = 0;
		public int Offset_sep1 = 0;
		public int Offset_sep2 = 0;
		
		public double ctrl_x = 0;
		public double ctrl_y = 0;
		public double ctrl_w = 0;
		public double ctrl_h = 0;
		public double[] ctrl_shortcut_x = new double[12];
		public double[] ctrl_shortcut_y = new double[12];
		public double alt_x = 0;
		public double alt_y = 0;
		public double alt_w = 0;
		public double alt_h = 0;
		public double[] alt_shortcut_x = new double[12];
		public double[] alt_shortcut_y = new double[12];
		public double extra_x = 0;
		public double extra_y = 0;
		public double extra_w = 0;
		public double extra_h = 0;
		public double[] extra_shortcut_x = new double[12];
		public double[] extra_shortcut_y = new double[12];
		public double sep1_x = 0;
		public double sep1_y = 0;
		public double sep1_w = 0;
		public double sep1_h = 0;
		public double[] sep1_shortcut_x = new double[12];
		public double[] sep1_shortcut_y = new double[12];
		public double sep2_x = 0;
		public double sep2_y = 0;
		public double sep2_w = 0;
		public double sep2_h = 0;
		public double[] sep2_shortcut_x = new double[12];
		public double[] sep2_shortcut_y = new double[12];
		
		public int Offset_shortcut = 0;
		
		int form_toggle_ctrl = 0;
		int form_toggle_alt = 0;
		int form_toggle_extra = 0;
		int form_toggle_sep1 = 0;
		int form_toggle_sep2 = 0;
		
		bool Is_Full = false;
		bool Detail = false;
		
		long Offset_left = 0;
		long Offset_top = 0;
		long Offset_width = 0;
		long Offset_height = 0;
		
		long Offset_decale = 0;
		long Offset_size = 0;
		
		public string is_select = "";
		
		DispatcherTimer messageTimer;
		
		public Window_Base()
		{
			InitializeComponent();

			quickbar_dialog_ctrl.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_ctrl.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_alt.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_alt.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_extra.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_extra.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_sep1.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_sep1.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_sep2.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_sep2.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut1.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut1.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut2.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut2.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut3.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut3.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut4.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut4.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut5.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut5.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut6.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut6.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut7.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut7.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut8.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut8.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut9.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut9.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut10.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut10.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut11.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut11.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			quickbar_dialog_shortcut12.PreviewMouseLeftButtonDown+=new MouseButtonEventHandler(Radio_MouseLeftButtonUp);
			quickbar_dialog_shortcut12.PreviewMouseMove+=new MouseEventHandler(Radio_MouseMove);
			
			string[] args = Environment.GetCommandLineArgs();
			try
			{
				AionProcess.Open(int.Parse(args[1]));
			}
			catch
			{
				Environment.Exit(0);
			}
			
			if (!File.Exists("./" + AionProcess.Modules.Version_game + "_" + version + ".ini"))
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.WorkingDirectory = @".\";
				startInfo.Arguments = args[1];
				startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+@".\Create_ini_" + version + ".exe";
				Process.Start(startInfo);

				while (!File.Exists("./" + AionProcess.Modules.Version_game + "_" + version + ".ini"))
				{
					Thread.Sleep(1000);
				}
				Thread.Sleep(5000);
			}
			
			IniFile ini = new IniFile("./" + AionProcess.Modules.Version_game + "_" + version + ".ini");
			Offset_ctrl = int.Parse(ini.IniReadValue("Base", "quickbar_dialog_ctrl"));
			Offset_alt = int.Parse(ini.IniReadValue("Base", "quickbar_dialog_alt"));
			Offset_extra = int.Parse(ini.IniReadValue("Base", "quickbar_dialog_extra"));
			Offset_sep1 = int.Parse(ini.IniReadValue("Base", "quickbar_dialog_sep1"));
			Offset_sep2 = int.Parse(ini.IniReadValue("Base", "quickbar_dialog_sep2"));
			Offset_shortcut = int.Parse(ini.IniReadValue("shortcut", "shortcut1"));
			
			if (version == "32")
			{
				Offset_left = 0x48;
				Offset_top = 0x50;
				Offset_width = 0x58;
				Offset_height = 0x60;
				Offset_decale = 0x268;
				Offset_size = 0x4;
			}
			else
			{
				Offset_left = 0x50;
				Offset_top = 0x58;
				Offset_width = 0x60;
				Offset_height = 0x68;
				Offset_decale = 0x280;
				Offset_size = 0x8;
			}
			SetForegroundWindow(AionProcess.whandle);
			Start_all();
			try
			{
				IniFile ini_last = new IniFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Last_File.ini");
				string str_last_file = ini_last.IniReadValue("Last", "File");
				
				IniFile ini_file = new IniFile(str_last_file);
				
				ctrl_x = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x"));
				ctrl_y = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y"));
				ctrl_w = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "w"));
				ctrl_h = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "h"));
				ctrl_shortcut_x[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x1"));
				ctrl_shortcut_y[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y1"));
				ctrl_shortcut_x[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x2"));
				ctrl_shortcut_y[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y2"));
				ctrl_shortcut_x[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x3"));
				ctrl_shortcut_y[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y3"));
				ctrl_shortcut_x[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x4"));
				ctrl_shortcut_y[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y4"));
				ctrl_shortcut_x[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x5"));
				ctrl_shortcut_y[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y5"));
				ctrl_shortcut_x[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x6"));
				ctrl_shortcut_y[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y6"));
				ctrl_shortcut_x[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x7"));
				ctrl_shortcut_y[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y7"));
				ctrl_shortcut_x[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x8"));
				ctrl_shortcut_y[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y8"));
				ctrl_shortcut_x[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x9"));
				ctrl_shortcut_y[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y9"));
				ctrl_shortcut_x[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x10"));
				ctrl_shortcut_y[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y10"));
				ctrl_shortcut_x[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x11"));
				ctrl_shortcut_y[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y11"));
				ctrl_shortcut_x[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "x12"));
				ctrl_shortcut_y[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_ctrl", "y12"));
				
				alt_x = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x"));
				alt_y = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y"));
				alt_w = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "w"));
				alt_h = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "h"));
				alt_shortcut_x[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x1"));
				alt_shortcut_y[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y1"));
				alt_shortcut_x[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x2"));
				alt_shortcut_y[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y2"));
				alt_shortcut_x[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x3"));
				alt_shortcut_y[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y3"));
				alt_shortcut_x[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x4"));
				alt_shortcut_y[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y4"));
				alt_shortcut_x[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x5"));
				alt_shortcut_y[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y5"));
				alt_shortcut_x[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x6"));
				alt_shortcut_y[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y6"));
				alt_shortcut_x[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x7"));
				alt_shortcut_y[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y7"));
				alt_shortcut_x[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x8"));
				alt_shortcut_y[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y8"));
				alt_shortcut_x[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x9"));
				alt_shortcut_y[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y9"));
				alt_shortcut_x[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x10"));
				alt_shortcut_y[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y10"));
				alt_shortcut_x[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x11"));
				alt_shortcut_y[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y11"));
				alt_shortcut_x[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "x12"));
				alt_shortcut_y[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_alt", "y12"));
				
				extra_x = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x"));
				extra_y = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y"));
				extra_w = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "w"));
				extra_h = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "h"));
				extra_shortcut_x[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x1"));
				extra_shortcut_y[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y1"));
				extra_shortcut_x[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x2"));
				extra_shortcut_y[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y2"));
				extra_shortcut_x[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x3"));
				extra_shortcut_y[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y3"));
				extra_shortcut_x[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x4"));
				extra_shortcut_y[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y4"));
				extra_shortcut_x[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x5"));
				extra_shortcut_y[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y5"));
				extra_shortcut_x[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x6"));
				extra_shortcut_y[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y6"));
				extra_shortcut_x[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x7"));
				extra_shortcut_y[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y7"));
				extra_shortcut_x[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x8"));
				extra_shortcut_y[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y8"));
				extra_shortcut_x[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x9"));
				extra_shortcut_y[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y9"));
				extra_shortcut_x[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x10"));
				extra_shortcut_y[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y10"));
				extra_shortcut_x[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x11"));
				extra_shortcut_y[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y11"));
				extra_shortcut_x[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "x12"));
				extra_shortcut_y[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_extra", "y12"));
				
				sep1_x = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x"));
				sep1_y = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y"));
				sep1_w = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "w"));
				sep1_h = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "h"));
				sep1_shortcut_x[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x1"));
				sep1_shortcut_y[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y1"));
				sep1_shortcut_x[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x2"));
				sep1_shortcut_y[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y2"));
				sep1_shortcut_x[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x3"));
				sep1_shortcut_y[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y3"));
				sep1_shortcut_x[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x4"));
				sep1_shortcut_y[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y4"));
				sep1_shortcut_x[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x5"));
				sep1_shortcut_y[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y5"));
				sep1_shortcut_x[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x6"));
				sep1_shortcut_y[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y6"));
				sep1_shortcut_x[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x7"));
				sep1_shortcut_y[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y7"));
				sep1_shortcut_x[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x8"));
				sep1_shortcut_y[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y8"));
				sep1_shortcut_x[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x9"));
				sep1_shortcut_y[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y9"));
				sep1_shortcut_x[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x10"));
				sep1_shortcut_y[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y10"));
				sep1_shortcut_x[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x11"));
				sep1_shortcut_y[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y11"));
				sep1_shortcut_x[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "x12"));
				sep1_shortcut_y[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep1", "y12"));
				
				sep2_x = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x"));
				sep2_y = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y"));
				sep2_w = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "w"));
				sep2_h = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "h"));
				sep2_shortcut_x[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x1"));
				sep2_shortcut_y[0] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y1"));
				sep2_shortcut_x[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x2"));
				sep2_shortcut_y[1] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y2"));
				sep2_shortcut_x[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x3"));
				sep2_shortcut_y[2] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y3"));
				sep2_shortcut_x[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x4"));
				sep2_shortcut_y[3] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y4"));
				sep2_shortcut_x[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x5"));
				sep2_shortcut_y[4] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y5"));
				sep2_shortcut_x[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x6"));
				sep2_shortcut_y[5] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y6"));
				sep2_shortcut_x[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x7"));
				sep2_shortcut_y[6] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y7"));
				sep2_shortcut_x[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x8"));
				sep2_shortcut_y[7] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y8"));
				sep2_shortcut_x[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x9"));
				sep2_shortcut_y[8] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y9"));
				sep2_shortcut_x[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x10"));
				sep2_shortcut_y[9] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y10"));
				sep2_shortcut_x[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x11"));
				sep2_shortcut_y[10] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y11"));
				sep2_shortcut_x[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "x12"));
				sep2_shortcut_y[11] = double.Parse(ini_file.IniReadValue("quickbar_dialog_sep2", "y12"));
				
				this.WindowState = WindowState.Minimized;
			}
			catch{}
			messageTimer = new DispatcherTimer();
			messageTimer.Tick += new EventHandler(messageTimer_Tick);
			messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			messageTimer.Start();
		}
		
		void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		
		void Button_Open(object sender, RoutedEventArgs e)
		{
			if (!Directory.Exists("./Save"))
				Directory.CreateDirectory("./Save");
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Save";
			dlg.DefaultExt = ".ini"; // Default file extension
			dlg.Filter = " |*.ini"; // Filter files by extension

			Nullable<bool> result = dlg.ShowDialog();

			if (result == true)
			{
				try
				{
					IniFile ini = new IniFile(dlg.FileName);
					
					ctrl_x = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x"));
					ctrl_y = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y"));
					ctrl_w = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "w"));
					ctrl_h = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "h"));
					ctrl_shortcut_x[0] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x1"));
					ctrl_shortcut_y[0] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y1"));
					ctrl_shortcut_x[1] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x2"));
					ctrl_shortcut_y[1] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y2"));
					ctrl_shortcut_x[2] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x3"));
					ctrl_shortcut_y[2] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y3"));
					ctrl_shortcut_x[3] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x4"));
					ctrl_shortcut_y[3] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y4"));
					ctrl_shortcut_x[4] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x5"));
					ctrl_shortcut_y[4] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y5"));
					ctrl_shortcut_x[5] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x6"));
					ctrl_shortcut_y[5] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y6"));
					ctrl_shortcut_x[6] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x7"));
					ctrl_shortcut_y[6] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y7"));
					ctrl_shortcut_x[7] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x8"));
					ctrl_shortcut_y[7] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y8"));
					ctrl_shortcut_x[8] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x9"));
					ctrl_shortcut_y[8] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y9"));
					ctrl_shortcut_x[9] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x10"));
					ctrl_shortcut_y[9] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y10"));
					ctrl_shortcut_x[10] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x11"));
					ctrl_shortcut_y[10] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y11"));
					ctrl_shortcut_x[11] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "x12"));
					ctrl_shortcut_y[11] = double.Parse(ini.IniReadValue("quickbar_dialog_ctrl", "y12"));
					
					alt_x = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x"));
					alt_y = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y"));
					alt_w = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "w"));
					alt_h = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "h"));
					alt_shortcut_x[0] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x1"));
					alt_shortcut_y[0] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y1"));
					alt_shortcut_x[1] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x2"));
					alt_shortcut_y[1] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y2"));
					alt_shortcut_x[2] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x3"));
					alt_shortcut_y[2] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y3"));
					alt_shortcut_x[3] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x4"));
					alt_shortcut_y[3] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y4"));
					alt_shortcut_x[4] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x5"));
					alt_shortcut_y[4] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y5"));
					alt_shortcut_x[5] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x6"));
					alt_shortcut_y[5] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y6"));
					alt_shortcut_x[6] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x7"));
					alt_shortcut_y[6] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y7"));
					alt_shortcut_x[7] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x8"));
					alt_shortcut_y[7] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y8"));
					alt_shortcut_x[8] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x9"));
					alt_shortcut_y[8] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y9"));
					alt_shortcut_x[9] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x10"));
					alt_shortcut_y[9] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y10"));
					alt_shortcut_x[10] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x11"));
					alt_shortcut_y[10] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y11"));
					alt_shortcut_x[11] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "x12"));
					alt_shortcut_y[11] = double.Parse(ini.IniReadValue("quickbar_dialog_alt", "y12"));
					
					extra_x = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x"));
					extra_y = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y"));
					extra_w = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "w"));
					extra_h = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "h"));
					extra_shortcut_x[0] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x1"));
					extra_shortcut_y[0] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y1"));
					extra_shortcut_x[1] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x2"));
					extra_shortcut_y[1] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y2"));
					extra_shortcut_x[2] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x3"));
					extra_shortcut_y[2] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y3"));
					extra_shortcut_x[3] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x4"));
					extra_shortcut_y[3] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y4"));
					extra_shortcut_x[4] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x5"));
					extra_shortcut_y[4] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y5"));
					extra_shortcut_x[5] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x6"));
					extra_shortcut_y[5] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y6"));
					extra_shortcut_x[6] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x7"));
					extra_shortcut_y[6] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y7"));
					extra_shortcut_x[7] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x8"));
					extra_shortcut_y[7] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y8"));
					extra_shortcut_x[8] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x9"));
					extra_shortcut_y[8] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y9"));
					extra_shortcut_x[9] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x10"));
					extra_shortcut_y[9] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y10"));
					extra_shortcut_x[10] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x11"));
					extra_shortcut_y[10] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y11"));
					extra_shortcut_x[11] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "x12"));
					extra_shortcut_y[11] = double.Parse(ini.IniReadValue("quickbar_dialog_extra", "y12"));
					
					sep1_x = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x"));
					sep1_y = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y"));
					sep1_w = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "w"));
					sep1_h = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "h"));
					sep1_shortcut_x[0] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x1"));
					sep1_shortcut_y[0] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y1"));
					sep1_shortcut_x[1] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x2"));
					sep1_shortcut_y[1] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y2"));
					sep1_shortcut_x[2] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x3"));
					sep1_shortcut_y[2] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y3"));
					sep1_shortcut_x[3] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x4"));
					sep1_shortcut_y[3] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y4"));
					sep1_shortcut_x[4] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x5"));
					sep1_shortcut_y[4] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y5"));
					sep1_shortcut_x[5] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x6"));
					sep1_shortcut_y[5] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y6"));
					sep1_shortcut_x[6] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x7"));
					sep1_shortcut_y[6] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y7"));
					sep1_shortcut_x[7] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x8"));
					sep1_shortcut_y[7] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y8"));
					sep1_shortcut_x[8] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x9"));
					sep1_shortcut_y[8] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y9"));
					sep1_shortcut_x[9] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x10"));
					sep1_shortcut_y[9] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y10"));
					sep1_shortcut_x[10] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x11"));
					sep1_shortcut_y[10] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y11"));
					sep1_shortcut_x[11] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "x12"));
					sep1_shortcut_y[11] = double.Parse(ini.IniReadValue("quickbar_dialog_sep1", "y12"));
					
					sep2_x = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x"));
					sep2_y = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y"));
					sep2_w = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "w"));
					sep2_h = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "h"));
					sep2_shortcut_x[0] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x1"));
					sep2_shortcut_y[0] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y1"));
					sep2_shortcut_x[1] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x2"));
					sep2_shortcut_y[1] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y2"));
					sep2_shortcut_x[2] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x3"));
					sep2_shortcut_y[2] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y3"));
					sep2_shortcut_x[3] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x4"));
					sep2_shortcut_y[3] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y4"));
					sep2_shortcut_x[4] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x5"));
					sep2_shortcut_y[4] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y5"));
					sep2_shortcut_x[5] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x6"));
					sep2_shortcut_y[5] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y6"));
					sep2_shortcut_x[6] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x7"));
					sep2_shortcut_y[6] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y7"));
					sep2_shortcut_x[7] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x8"));
					sep2_shortcut_y[7] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y8"));
					sep2_shortcut_x[8] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x9"));
					sep2_shortcut_y[8] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y9"));
					sep2_shortcut_x[9] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x10"));
					sep2_shortcut_y[9] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y10"));
					sep2_shortcut_x[10] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x11"));
					sep2_shortcut_y[10] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y11"));
					sep2_shortcut_x[11] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "x12"));
					sep2_shortcut_y[11] = double.Parse(ini.IniReadValue("quickbar_dialog_sep2", "y12"));
					
					IniFile last_ini = new IniFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Last_File.ini");
					last_ini.IniWriteValue("Last", "File", dlg.FileName);
				}
				catch
				{}
			}
		}
		
		void Button_Save(object sender, RoutedEventArgs e)
		{
			if (!Directory.Exists("./Save"))
				Directory.CreateDirectory("./Save");
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Save";
			dlg.DefaultExt = ".ini"; // Default file extension
			dlg.Filter = " |*.ini"; // Filter files by extension

			Nullable<bool> result = dlg.ShowDialog();

			if (result == true)
			{
				try
				{
					IniFile ini = new IniFile(dlg.FileName);
					
					ini.IniWriteValue("quickbar_dialog_ctrl", "x", ctrl_x.ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y", ctrl_y.ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "w", ctrl_w.ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "h", ctrl_h.ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x1", ctrl_shortcut_x[0].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y1", ctrl_shortcut_y[0].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x2", ctrl_shortcut_x[1].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y2", ctrl_shortcut_y[1].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x3", ctrl_shortcut_x[2].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y3", ctrl_shortcut_y[2].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x4", ctrl_shortcut_x[3].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y4", ctrl_shortcut_y[3].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x5", ctrl_shortcut_x[4].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y5", ctrl_shortcut_y[4].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x6", ctrl_shortcut_x[5].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y6", ctrl_shortcut_y[5].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x7", ctrl_shortcut_x[6].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y7", ctrl_shortcut_y[6].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x8", ctrl_shortcut_x[7].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y8", ctrl_shortcut_y[7].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x9", ctrl_shortcut_x[8].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y9", ctrl_shortcut_y[8].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x10", ctrl_shortcut_x[9].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y10", ctrl_shortcut_y[9].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x11", ctrl_shortcut_x[10].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y11", ctrl_shortcut_y[10].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "x12", ctrl_shortcut_x[11].ToString());
					ini.IniWriteValue("quickbar_dialog_ctrl", "y12", ctrl_shortcut_y[11].ToString());
					
					ini.IniWriteValue("quickbar_dialog_alt", "x", alt_x.ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y", alt_y.ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "w", alt_w.ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "h", alt_h.ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x1", alt_shortcut_x[0].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y1", alt_shortcut_y[0].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x2", alt_shortcut_x[1].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y2", alt_shortcut_y[1].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x3", alt_shortcut_x[2].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y3", alt_shortcut_y[2].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x4", alt_shortcut_x[3].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y4", alt_shortcut_y[3].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x5", alt_shortcut_x[4].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y5", alt_shortcut_y[4].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x6", alt_shortcut_x[5].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y6", alt_shortcut_y[5].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x7", alt_shortcut_x[6].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y7", alt_shortcut_y[6].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x8", alt_shortcut_x[7].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y8", alt_shortcut_y[7].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x9", alt_shortcut_x[8].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y9", alt_shortcut_y[8].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x10", alt_shortcut_x[9].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y10", alt_shortcut_y[9].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x11", alt_shortcut_x[10].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y11", alt_shortcut_y[10].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "x12", alt_shortcut_x[11].ToString());
					ini.IniWriteValue("quickbar_dialog_alt", "y12", alt_shortcut_y[11].ToString());
					
					ini.IniWriteValue("quickbar_dialog_extra", "x", extra_x.ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y", extra_y.ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "w", extra_w.ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "h", extra_h.ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x1", extra_shortcut_x[0].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y1", extra_shortcut_y[0].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x2", extra_shortcut_x[1].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y2", extra_shortcut_y[1].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x3", extra_shortcut_x[2].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y3", extra_shortcut_y[2].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x4", extra_shortcut_x[3].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y4", extra_shortcut_y[3].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x5", extra_shortcut_x[4].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y5", extra_shortcut_y[4].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x6", extra_shortcut_x[5].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y6", extra_shortcut_y[5].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x7", extra_shortcut_x[6].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y7", extra_shortcut_y[6].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x8", extra_shortcut_x[7].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y8", extra_shortcut_y[7].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x9", extra_shortcut_x[8].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y9", extra_shortcut_y[8].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x10", extra_shortcut_x[9].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y10", extra_shortcut_y[9].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x11", extra_shortcut_x[10].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y11", extra_shortcut_y[10].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "x12", extra_shortcut_x[11].ToString());
					ini.IniWriteValue("quickbar_dialog_extra", "y12", extra_shortcut_y[11].ToString());
					
					ini.IniWriteValue("quickbar_dialog_sep1", "x", sep1_x.ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y", sep1_y.ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "w", sep1_w.ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "h", sep1_h.ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x1", sep1_shortcut_x[0].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y1", sep1_shortcut_y[0].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x2", sep1_shortcut_x[1].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y2", sep1_shortcut_y[1].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x3", sep1_shortcut_x[2].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y3", sep1_shortcut_y[2].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x4", sep1_shortcut_x[3].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y4", sep1_shortcut_y[3].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x5", sep1_shortcut_x[4].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y5", sep1_shortcut_y[4].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x6", sep1_shortcut_x[5].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y6", sep1_shortcut_y[5].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x7", sep1_shortcut_x[6].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y7", sep1_shortcut_y[6].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x8", sep1_shortcut_x[7].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y8", sep1_shortcut_y[7].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x9", sep1_shortcut_x[8].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y9", sep1_shortcut_y[8].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x10", sep1_shortcut_x[9].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y10", sep1_shortcut_y[9].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x11", sep1_shortcut_x[10].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y11", sep1_shortcut_y[10].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "x12", sep1_shortcut_x[11].ToString());
					ini.IniWriteValue("quickbar_dialog_sep1", "y12", sep1_shortcut_y[11].ToString());
					
					ini.IniWriteValue("quickbar_dialog_sep2", "x", sep2_x.ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y", sep2_y.ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "w", sep2_w.ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "h", sep2_h.ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x1", sep2_shortcut_x[0].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y1", sep2_shortcut_y[0].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x2", sep2_shortcut_x[1].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y2", sep2_shortcut_y[1].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x3", sep2_shortcut_x[2].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y3", sep2_shortcut_y[2].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x4", sep2_shortcut_x[3].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y4", sep2_shortcut_y[3].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x5", sep2_shortcut_x[4].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y5", sep2_shortcut_y[4].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x6", sep2_shortcut_x[5].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y6", sep2_shortcut_y[5].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x7", sep2_shortcut_x[6].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y7", sep2_shortcut_y[6].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x8", sep2_shortcut_x[7].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y8", sep2_shortcut_y[7].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x9", sep2_shortcut_x[8].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y9", sep2_shortcut_y[8].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x10", sep2_shortcut_x[9].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y10", sep2_shortcut_y[9].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x11", sep2_shortcut_x[10].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y11", sep2_shortcut_y[10].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "x12", sep2_shortcut_x[11].ToString());
					ini.IniWriteValue("quickbar_dialog_sep2", "y12", sep2_shortcut_y[11].ToString());
				}
				catch
				{}
			}
		}

		
		void Button_Click_Full_Screen(object sender, RoutedEventArgs e)
		{
			Is_Full = !Is_Full;
		}
		
		void Button_Click_Change(object sender, RoutedEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
				form_toggle_ctrl = ChangeFormQuickBar(Offset_ctrl, form_toggle_ctrl);
			if (is_select == "quickbar_dialog_alt")
				form_toggle_alt = ChangeFormQuickBar(Offset_alt, form_toggle_alt);
			if (is_select == "quickbar_dialog_extra")
				form_toggle_extra = ChangeFormQuickBar(Offset_extra, form_toggle_extra);
			if (is_select == "quickbar_dialog_sep1")
				form_toggle_sep1 = ChangeFormQuickBar(Offset_sep1, form_toggle_sep1);
			if (is_select == "quickbar_dialog_sep2")
				form_toggle_sep2 = ChangeFormQuickBar(Offset_sep2, form_toggle_sep2);
		}
		
		void Quickbar_dialog_ctrl_Checked(object sender, RoutedEventArgs e)
		{
			is_select = "quickbar_dialog_ctrl";
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = quickbar_dialog_ctrl.Margin.Left + 5;
			_margin.Top = quickbar_dialog_ctrl.Margin.Top + 5;
			Change.Margin = _margin;
			Detail = false;
		}
		
		void Quickbar_dialog_alt_Checked(object sender, RoutedEventArgs e)
		{
			is_select = "quickbar_dialog_alt";
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = quickbar_dialog_alt.Margin.Left + 5;
			_margin.Top = quickbar_dialog_alt.Margin.Top + 5;
			Change.Margin = _margin;
			Detail = false;
		}
		
		void Quickbar_dialog_extra_Checked(object sender, RoutedEventArgs e)
		{
			is_select = "quickbar_dialog_extra";
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = quickbar_dialog_extra.Margin.Left + 5;
			_margin.Top = quickbar_dialog_extra.Margin.Top + 5;
			Change.Margin = _margin;
			Detail = false;
		}
		
		void Quickbar_dialog_sep1_Checked(object sender, RoutedEventArgs e)
		{
			is_select = "quickbar_dialog_sep1";
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = quickbar_dialog_sep1.Margin.Left + 5;
			_margin.Top = quickbar_dialog_sep1.Margin.Top + 5;
			Change.Margin = _margin;
			Detail = false;
		}
		
		void Quickbar_dialog_sep2_Checked(object sender, RoutedEventArgs e)
		{
			is_select = "quickbar_dialog_sep2";
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = quickbar_dialog_sep2.Margin.Left + 5;
			_margin.Top = quickbar_dialog_sep2.Margin.Top + 5;
			Change.Margin = _margin;
			Detail = false;
		}
		
		void Quickbar_dialog_reset_shortcut()
		{
			quickbar_dialog_shortcut1.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut2.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut3.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut4.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut5.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut6.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut7.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut8.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut9.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut10.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut11.Margin = new Thickness(-1000,-1000,0,0);
			quickbar_dialog_shortcut12.Margin = new Thickness(-1000,-1000,0,0);
		}
		
		private void Window_Aion_Info()
		{
			try
			{
				if (!Is_Full)
				{
					RECT Rect = new RECT();
					if (GetWindowRect(AionProcess.whandle, ref Rect))
					{
						this.Width = Rect.Width - Rect.X - 16;
						this.Height = Rect.Height - Rect.Y - 38;
						this.Top = Rect.Y + 30;
						this.Left = Rect.X + 8;
					}
				}
				else
				{
					RECT Rect = new RECT();
					if (GetWindowRect(AionProcess.whandle, ref Rect))
					{
						this.Width = Rect.Width - Rect.X;
						this.Height = Rect.Height - Rect.Y;
						this.Top = Rect.Y;
						this.Left = Rect.X;
					}
				}
			}
			catch
			{
				this.Top = 0;
				this.Left = 0;
			}
		}
		
		void messageTimer_Tick(object sender, EventArgs e)
		{
			Update();
			Window_Aion_Info();
		}
		
		void Start_all()
		{
			long game = AionProcess.Modules.Game;
			ctrl_x = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_ctrl) + Offset_left);
			ctrl_y = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_ctrl) + Offset_top);
			ctrl_w = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_ctrl) + Offset_width);
			ctrl_h = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_ctrl) + Offset_height);
			for (int i = 0; i < 12; i++)
			{
				ctrl_shortcut_x[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut + i * Offset_size) + Offset_left);
				ctrl_shortcut_y[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut + i * Offset_size) + Offset_top);
			}
			
			alt_x = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_alt) + Offset_left);
			alt_y = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_alt) + Offset_top);
			alt_w = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_alt) + Offset_width);
			alt_h = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_alt) + Offset_height);
			for (int i = 0; i < 12; i++)
			{
				alt_shortcut_x[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut + i * Offset_size) + Offset_left);
				alt_shortcut_y[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut + i * Offset_size) + Offset_top);
			}
			
			extra_x = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_extra) + Offset_left);
			extra_y = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_extra) + Offset_top);
			extra_w = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_extra) + Offset_width);
			extra_h = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_extra) + Offset_height);
			for (int i = 0; i < 12; i++)
			{
				extra_shortcut_x[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut + i * Offset_size) + Offset_left);
				extra_shortcut_y[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut + i * Offset_size) + Offset_top);
			}
			
			sep1_x = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep1) + Offset_left);
			sep1_y = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep1) + Offset_top);
			sep1_w = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep1) + Offset_width);
			sep1_h = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep1) + Offset_height);
			for (int i = 0; i < 12; i++)
			{
				sep1_shortcut_x[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut + i * Offset_size) + Offset_left);
				sep1_shortcut_y[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut + i * Offset_size) + Offset_top);
			}
			
			sep2_x = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep2) + Offset_left);
			sep2_y = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep2) + Offset_top);
			sep2_w = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep2) + Offset_width);
			sep2_h = SplMemory.ReadDouble(SplMemory.ReadLong(game + Offset_sep2) + Offset_height);
			for (int i = 0; i < 12; i++)
			{
				sep2_shortcut_x[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut + i * Offset_size) + Offset_left);
				sep2_shortcut_y[i] = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut + i * Offset_size) + Offset_top);
			}
		}
		
		void Update()
		{
			long game = AionProcess.Modules.Game;
			for (int i = 0; i < 12; i++)
			{
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut + i * Offset_size) + Offset_left, ctrl_shortcut_x[i]);
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut + i * Offset_size) + Offset_top, ctrl_shortcut_y[i]);
			}
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_ctrl) + Offset_left, ctrl_x);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_ctrl) + Offset_top, ctrl_y);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_ctrl) + Offset_width, ctrl_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_ctrl) + Offset_height, ctrl_h);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_decale) + Offset_width, ctrl_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_decale) + Offset_height, ctrl_h);
			for (int i = 0; i < 12; i++)
			{
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut + i * Offset_size) + Offset_left, alt_shortcut_x[i]);
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut + i * Offset_size) + Offset_top, alt_shortcut_y[i]);
			}
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_alt) + Offset_left, alt_x);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_alt) + Offset_top, alt_y);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_alt) + Offset_width, alt_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_alt) + Offset_height, alt_h);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_decale) + Offset_width, alt_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_decale) + Offset_height, alt_h);
			for (int i = 0; i < 12; i++)
			{
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut + i * Offset_size) + Offset_left, extra_shortcut_x[i]);
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut + i * Offset_size) + Offset_top, extra_shortcut_y[i]);
			}
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_extra) + Offset_left, extra_x);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_extra) + Offset_top, extra_y);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_extra) + Offset_width, extra_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_extra) + Offset_height, extra_h);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_decale) + Offset_width, extra_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_decale) + Offset_height, extra_h);
			for (int i = 0; i < 12; i++)
			{
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut + i * Offset_size) + Offset_left, sep1_shortcut_x[i]);
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut + i * Offset_size) + Offset_top, sep1_shortcut_y[i]);
			}
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep1) + Offset_left, sep1_x);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep1) + Offset_top, sep1_y);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep1) + Offset_width, sep1_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep1) + Offset_height, sep1_h);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_decale) + Offset_width, sep1_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_decale) + Offset_height, sep1_h);
			for (int i = 0; i < 12; i++)
			{
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut + i * Offset_size) + Offset_left, sep2_shortcut_x[i]);
				SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut + i * Offset_size) + Offset_top, sep2_shortcut_y[i]);
			}
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep2) + Offset_left, sep2_x);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep2) + Offset_top, sep2_y);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep2) + Offset_width, sep2_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(game + Offset_sep2) + Offset_height, sep2_h);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_decale) + Offset_width, sep2_w);
			SplMemory.WriteMemory(SplMemory.ReadLong(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_decale) + Offset_height, sep2_h);
			
			System.Windows.Thickness _margin = new System.Windows.Thickness();
			_margin.Left = ctrl_x;
			_margin.Top = ctrl_y;
			quickbar_dialog_ctrl.Width = ctrl_w;
			quickbar_dialog_ctrl.Height = ctrl_h;
			quickbar_dialog_ctrl.Margin = _margin;
			
			_margin.Left = alt_x;
			_margin.Top = alt_y;
			quickbar_dialog_alt.Width = alt_w;
			quickbar_dialog_alt.Height = alt_h;
			quickbar_dialog_alt.Margin = _margin;
			
			_margin.Left = extra_x;
			_margin.Top = extra_y;
			quickbar_dialog_extra.Width = extra_w;
			quickbar_dialog_extra.Height = extra_h;
			quickbar_dialog_extra.Margin = _margin;
			
			_margin.Left = sep1_x;
			_margin.Top = sep1_y;
			quickbar_dialog_sep1.Width = sep1_w;
			quickbar_dialog_sep1.Height = sep1_h;
			quickbar_dialog_sep1.Margin = _margin;
			
			_margin.Left = sep2_x;
			_margin.Top = sep2_y;
			quickbar_dialog_sep2.Width = sep2_w;
			quickbar_dialog_sep2.Height = sep2_h;
			quickbar_dialog_sep2.Margin = _margin;
			
			long Offset = 0;
			double x = -1000;
			double y = -1000;
			if (is_select == "quickbar_dialog_ctrl")
			{
				Offset = Offset_ctrl;
				x = quickbar_dialog_ctrl.Margin.Left;
				y = quickbar_dialog_ctrl.Margin.Top;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				Offset = Offset_alt;
				x = quickbar_dialog_alt.Margin.Left;
				y = quickbar_dialog_alt.Margin.Top;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				Offset = Offset_extra;
				x = quickbar_dialog_extra.Margin.Left;
				y = quickbar_dialog_extra.Margin.Top;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				Offset = Offset_sep1;
				x = quickbar_dialog_sep1.Margin.Left;
				y = quickbar_dialog_sep1.Margin.Top;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				Offset = Offset_sep2;
				x = quickbar_dialog_sep2.Margin.Left;
				y = quickbar_dialog_sep2.Margin.Top;
			}
			if (Offset != 0 && Detail)
			{
				double w_skill = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut) + Offset_width);
				double h_skill = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut) + Offset_height);
				quickbar_dialog_shortcut1.Width = w_skill;
				quickbar_dialog_shortcut1.Height = h_skill;
				quickbar_dialog_shortcut2.Width = w_skill;
				quickbar_dialog_shortcut2.Height = h_skill;
				quickbar_dialog_shortcut3.Width = w_skill;
				quickbar_dialog_shortcut3.Height = h_skill;
				quickbar_dialog_shortcut4.Width = w_skill;
				quickbar_dialog_shortcut4.Height = h_skill;
				quickbar_dialog_shortcut5.Width = w_skill;
				quickbar_dialog_shortcut5.Height = h_skill;
				quickbar_dialog_shortcut6.Width = w_skill;
				quickbar_dialog_shortcut6.Height = h_skill;
				quickbar_dialog_shortcut7.Width = w_skill;
				quickbar_dialog_shortcut7.Height = h_skill;
				quickbar_dialog_shortcut8.Width = w_skill;
				quickbar_dialog_shortcut8.Height = h_skill;
				quickbar_dialog_shortcut9.Width = w_skill;
				quickbar_dialog_shortcut9.Height = h_skill;
				quickbar_dialog_shortcut10.Width = w_skill;
				quickbar_dialog_shortcut10.Height = h_skill;
				quickbar_dialog_shortcut11.Width = w_skill;
				quickbar_dialog_shortcut11.Height = h_skill;
				quickbar_dialog_shortcut12.Width = w_skill;
				quickbar_dialog_shortcut12.Height = h_skill;
				quickbar_dialog_shortcut1.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 0 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 0 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut2.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 1 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 1 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut3.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 2 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 2 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut4.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 3 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 3 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut5.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 4 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 4 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut6.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 5 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 5 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut7.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 6 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 6 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut8.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 7 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 7 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut9.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 8 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 8 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut10.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 9 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 9 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut11.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 10 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 10 * Offset_size) + Offset_top),0,0);
				quickbar_dialog_shortcut12.Margin = new Thickness(
					x + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 11 * Offset_size) + Offset_left),
					y + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut + 11 * Offset_size) + Offset_top),0,0);
				_margin.Left = -1000;
				_margin.Top = -1000;
				Change.Margin = _margin;
			}
			else
			{
				_margin.Left = x + 5;
				_margin.Top = y + 5;
				Change.Margin = _margin;
				Quickbar_dialog_reset_shortcut();
			}
		}
		
		private void Radio_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			// Get the Position of Window so that it will set margin from this window
			m_MouseX = e.GetPosition(this).X;
			m_MouseY = e.GetPosition(this).Y;
		}

		private void Radio_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// Capture the mouse for border
				System.Windows.Thickness _margin = new System.Windows.Thickness();
				int _tempX = Convert.ToInt32(e.GetPosition(this).X);
				int _tempY = Convert.ToInt32(e.GetPosition(this).Y);
				if (quickbar_dialog_ctrl.IsChecked == true && quickbar_dialog_ctrl.IsMouseOver)
					_margin = quickbar_dialog_ctrl.Margin;
				else if (quickbar_dialog_alt.IsChecked == true && quickbar_dialog_alt.IsMouseOver)
					_margin = quickbar_dialog_alt.Margin;
				else if (quickbar_dialog_extra.IsChecked == true && quickbar_dialog_extra.IsMouseOver)
					_margin = quickbar_dialog_extra.Margin;
				else if (quickbar_dialog_sep1.IsChecked == true && quickbar_dialog_sep1.IsMouseOver)
					_margin = quickbar_dialog_sep1.Margin;
				else if (quickbar_dialog_sep2.IsChecked == true && quickbar_dialog_sep2.IsMouseOver)
					_margin = quickbar_dialog_sep2.Margin;
				else if (quickbar_dialog_shortcut1.IsChecked == true && quickbar_dialog_shortcut1.IsMouseOver)
					_margin = quickbar_dialog_shortcut1.Margin;
				else if (quickbar_dialog_shortcut2.IsChecked == true && quickbar_dialog_shortcut2.IsMouseOver)
					_margin = quickbar_dialog_shortcut2.Margin;
				else if (quickbar_dialog_shortcut3.IsChecked == true && quickbar_dialog_shortcut3.IsMouseOver)
					_margin = quickbar_dialog_shortcut3.Margin;
				else if (quickbar_dialog_shortcut4.IsChecked == true && quickbar_dialog_shortcut4.IsMouseOver)
					_margin = quickbar_dialog_shortcut4.Margin;
				else if (quickbar_dialog_shortcut5.IsChecked == true && quickbar_dialog_shortcut5.IsMouseOver)
					_margin = quickbar_dialog_shortcut5.Margin;
				else if (quickbar_dialog_shortcut6.IsChecked == true && quickbar_dialog_shortcut6.IsMouseOver)
					_margin = quickbar_dialog_shortcut6.Margin;
				else if (quickbar_dialog_shortcut7.IsChecked == true && quickbar_dialog_shortcut7.IsMouseOver)
					_margin = quickbar_dialog_shortcut7.Margin;
				else if (quickbar_dialog_shortcut8.IsChecked == true && quickbar_dialog_shortcut8.IsMouseOver)
					_margin = quickbar_dialog_shortcut8.Margin;
				else if (quickbar_dialog_shortcut9.IsChecked == true && quickbar_dialog_shortcut9.IsMouseOver)
					_margin = quickbar_dialog_shortcut9.Margin;
				else if (quickbar_dialog_shortcut10.IsChecked == true && quickbar_dialog_shortcut10.IsMouseOver)
					_margin = quickbar_dialog_shortcut10.Margin;
				else if (quickbar_dialog_shortcut11.IsChecked == true && quickbar_dialog_shortcut11.IsMouseOver)
					_margin = quickbar_dialog_shortcut11.Margin;
				else if (quickbar_dialog_shortcut12.IsChecked == true && quickbar_dialog_shortcut12.IsMouseOver)
					_margin = quickbar_dialog_shortcut12.Margin;
				else
				{
					//MessageBox.Show("bug");
					return ;
				}
				// when While moving _tempX get greater than m_MouseX relative to usercontrol
				if (m_MouseX > _tempX)
				{
					// add the difference of both to Left
					_margin.Left += (_tempX - m_MouseX);
					// subtract the difference of both to Left
					_margin.Right -= (_tempX - m_MouseX);
				}
				else
				{
					_margin.Left -= (m_MouseX - _tempX);
					_margin.Right -= (_tempX - m_MouseX);
				}
				if (m_MouseY > _tempY)
				{
					_margin.Top += (_tempY - m_MouseY);
					_margin.Bottom -= (_tempY - m_MouseY);
				}
				else
				{
					_margin.Top -= (m_MouseY - _tempY);
					_margin.Bottom -= (_tempY - m_MouseY);
				}
				
				m_MouseX = _tempX;
				m_MouseY = _tempY;
				
				if (quickbar_dialog_ctrl.IsChecked == true)
				{
					quickbar_dialog_ctrl.Margin = _margin;
					ctrl_x = quickbar_dialog_ctrl.Margin.Left;
					ctrl_y = quickbar_dialog_ctrl.Margin.Top;
				}
				else if (quickbar_dialog_alt.IsChecked == true)
				{
					quickbar_dialog_alt.Margin = _margin;
					alt_x = quickbar_dialog_alt.Margin.Left;
					alt_y = quickbar_dialog_alt.Margin.Top;
				}
				else if (quickbar_dialog_extra.IsChecked == true)
				{
					quickbar_dialog_extra.Margin = _margin;
					extra_x = quickbar_dialog_extra.Margin.Left;
					extra_y = quickbar_dialog_extra.Margin.Top;
				}
				else if (quickbar_dialog_sep1.IsChecked == true)
				{
					quickbar_dialog_sep1.Margin = _margin;
					sep1_x = quickbar_dialog_sep1.Margin.Left;
					sep1_y = quickbar_dialog_sep1.Margin.Top;
				}
				else if (quickbar_dialog_sep2.IsChecked == true)
				{
					quickbar_dialog_sep2.Margin = _margin;
					sep2_x = quickbar_dialog_sep2.Margin.Left;
					sep2_y = quickbar_dialog_sep2.Margin.Top;
				}
				else if (quickbar_dialog_shortcut1.IsChecked == true)
				{
					quickbar_dialog_shortcut1.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[0] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[0] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[0] = _margin.Left - alt_x;
						alt_shortcut_y[0] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[0] = _margin.Left - extra_x;
						extra_shortcut_y[0] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[0] = _margin.Left - sep1_x;
						sep1_shortcut_y[0] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[0] = _margin.Left - sep2_x;
						sep2_shortcut_y[0] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut2.IsChecked == true)
				{
					quickbar_dialog_shortcut2.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[1] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[1] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[1] = _margin.Left - alt_x;
						alt_shortcut_y[1] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[1] = _margin.Left - extra_x;
						extra_shortcut_y[1] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[1] = _margin.Left - sep1_x;
						sep1_shortcut_y[1] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[1] = _margin.Left - sep2_x;
						sep2_shortcut_y[1] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut3.IsChecked == true)
				{
					quickbar_dialog_shortcut3.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[2] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[2] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[2] = _margin.Left - alt_x;
						alt_shortcut_y[2] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[2] = _margin.Left - extra_x;
						extra_shortcut_y[2] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[2] = _margin.Left - sep1_x;
						sep1_shortcut_y[2] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[2] = _margin.Left - sep2_x;
						sep2_shortcut_y[2] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut4.IsChecked == true)
				{
					quickbar_dialog_shortcut4.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[3] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[3] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[3] = _margin.Left - alt_x;
						alt_shortcut_y[3] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[3] = _margin.Left - extra_x;
						extra_shortcut_y[3] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[3] = _margin.Left - sep1_x;
						sep1_shortcut_y[3] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[3] = _margin.Left - sep2_x;
						sep2_shortcut_y[3] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut5.IsChecked == true)
				{
					quickbar_dialog_shortcut5.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[4] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[4] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[4] = _margin.Left - alt_x;
						alt_shortcut_y[4] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[4] = _margin.Left - extra_x;
						extra_shortcut_y[4] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[4] = _margin.Left - sep1_x;
						sep1_shortcut_y[4] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[4] = _margin.Left - sep2_x;
						sep2_shortcut_y[4] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut6.IsChecked == true)
				{
					quickbar_dialog_shortcut6.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[5] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[5] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[5] = _margin.Left - alt_x;
						alt_shortcut_y[5] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[5] = _margin.Left - extra_x;
						extra_shortcut_y[5] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[5] = _margin.Left - sep1_x;
						sep1_shortcut_y[5] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[5] = _margin.Left - sep2_x;
						sep2_shortcut_y[5] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut7.IsChecked == true)
				{
					quickbar_dialog_shortcut7.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[6] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[6] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[6] = _margin.Left - alt_x;
						alt_shortcut_y[6] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[6] = _margin.Left - extra_x;
						extra_shortcut_y[6] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[6] = _margin.Left - sep1_x;
						sep1_shortcut_y[6] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[6] = _margin.Left - sep2_x;
						sep2_shortcut_y[6] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut8.IsChecked == true)
				{
					quickbar_dialog_shortcut8.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[7] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[7] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[7] = _margin.Left - alt_x;
						alt_shortcut_y[7] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[7] = _margin.Left - extra_x;
						extra_shortcut_y[7] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[7] = _margin.Left - sep1_x;
						sep1_shortcut_y[7] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[7] = _margin.Left - sep2_x;
						sep2_shortcut_y[7] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut9.IsChecked == true)
				{
					quickbar_dialog_shortcut9.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[8] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[8] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[8] = _margin.Left - alt_x;
						alt_shortcut_y[8] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[8] = _margin.Left - extra_x;
						extra_shortcut_y[8] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[8] = _margin.Left - sep1_x;
						sep1_shortcut_y[8] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[8] = _margin.Left - sep2_x;
						sep2_shortcut_y[8] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut10.IsChecked == true)
				{
					quickbar_dialog_shortcut10.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[9] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[9] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[9] = _margin.Left - alt_x;
						alt_shortcut_y[9] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[9] = _margin.Left - extra_x;
						extra_shortcut_y[9] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[9] = _margin.Left - sep1_x;
						sep1_shortcut_y[9] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[9] = _margin.Left - sep2_x;
						sep2_shortcut_y[9] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut11.IsChecked == true)
				{
					quickbar_dialog_shortcut11.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[10] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[10] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[10] = _margin.Left - alt_x;
						alt_shortcut_y[10] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[10] = _margin.Left - extra_x;
						extra_shortcut_y[10] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[10] = _margin.Left - sep1_x;
						sep1_shortcut_y[10] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[10] = _margin.Left - sep2_x;
						sep2_shortcut_y[10] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
				else if (quickbar_dialog_shortcut12.IsChecked == true)
				{
					quickbar_dialog_shortcut12.Margin = _margin;
					long game = AionProcess.Modules.Game;
					if (is_select == "quickbar_dialog_ctrl")
					{
						ctrl_shortcut_x[11] = _margin.Left - ctrl_x;
						ctrl_shortcut_y[11] = _margin.Top - ctrl_y;
					}
					if (is_select == "quickbar_dialog_alt")
					{
						alt_shortcut_x[11] = _margin.Left - alt_x;
						alt_shortcut_y[11] = _margin.Top - alt_y;
					}
					if (is_select == "quickbar_dialog_extra")
					{
						extra_shortcut_x[11] = _margin.Left - extra_x;
						extra_shortcut_y[11] = _margin.Top - extra_y;
					}
					if (is_select == "quickbar_dialog_sep1")
					{
						sep1_shortcut_x[11] = _margin.Left - sep1_x;
						sep1_shortcut_y[11] = _margin.Top - sep1_y;
					}
					if (is_select == "quickbar_dialog_sep2")
					{
						sep2_shortcut_x[11] = _margin.Left - sep2_x;
						sep2_shortcut_y[11] = _margin.Top - sep2_y;
					}
					Calcule_Max_Rec();
				}
			}
		}
		
		void Calcule_Max_Rec()
		{
			double x_min = 0;
			double y_min = 0;
			double x_max = 0;
			double y_max = 0;
			double tmp = 0;
			long game = AionProcess.Modules.Game;
			if (is_select == "quickbar_dialog_ctrl")
			{
				x_max = ctrl_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_width);
				y_max = ctrl_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_height);
				x_min = ctrl_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_width);
				y_min = ctrl_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_height);
				for (int i = 0; i < 12; i++)
				{
					tmp = ctrl_shortcut_x[i];
					if (tmp < 0)
					{
						ctrl_x = ctrl_x + tmp;
						for (int j = 0; j < 12; j++)
						{
							ctrl_shortcut_x[j] = ctrl_shortcut_x[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < x_min)
						x_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_width) > x_max)
						x_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_width);
					tmp = ctrl_shortcut_y[i];
					if (tmp < 0)
					{
						ctrl_y = ctrl_y + tmp;
						for (int j = 0; j < 12; j++)
						{
							ctrl_shortcut_y[j] = ctrl_shortcut_y[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < y_min)
						y_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_height) > y_max)
						y_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_ctrl) + Offset_shortcut) + Offset_height);
				}
				if (x_min > 0)
				{
					ctrl_x = ctrl_x + x_min;
					for (int i = 0; i < 12; i++)
					{
						ctrl_shortcut_x[i] = ctrl_shortcut_x[i] - x_min;
					}
				}
				if (y_min > 0)
				{
					ctrl_y = ctrl_y + y_min;
					for (int i = 0; i < 12; i++)
					{
						ctrl_shortcut_y[i] = ctrl_shortcut_y[i] - y_min;
					}
				}
				ctrl_w = x_max - x_min + 2;
				ctrl_h = y_max - y_min + 2;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				x_max = alt_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_width);
				y_max = alt_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_height);
				x_min = alt_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_width);
				y_min = alt_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_height);
				for (int i = 0; i < 12; i++)
				{
					tmp = alt_shortcut_x[i];
					if (tmp < 0)
					{
						alt_x = alt_x + tmp;
						for (int j = 0; j < 12; j++)
						{
							alt_shortcut_x[j] = alt_shortcut_x[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < x_min)
						x_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_width) > x_max)
						x_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_width);
					tmp = alt_shortcut_y[i];
					if (tmp < 0)
					{
						alt_y = alt_y + tmp;
						for (int j = 0; j < 12; j++)
						{
							alt_shortcut_y[j] = alt_shortcut_y[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < y_min)
						y_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_height) > y_max)
						y_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_alt) + Offset_shortcut) + Offset_height);
				}
				if (x_min > 0)
				{
					alt_x = alt_x + x_min;
					for (int i = 0; i < 12; i++)
					{
						alt_shortcut_x[i] = alt_shortcut_x[i] - x_min;
					}
				}
				if (y_min > 0)
				{
					alt_y = alt_y + y_min;
					for (int i = 0; i < 12; i++)
					{
						alt_shortcut_y[i] = alt_shortcut_y[i] - y_min;
					}
				}
				alt_w = x_max - x_min + 2;
				alt_h = y_max - y_min + 2;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				x_max = extra_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_width);
				y_max = extra_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_height);
				x_min = extra_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_width);
				y_min = extra_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_height);
				for (int i = 0; i < 12; i++)
				{
					tmp = extra_shortcut_x[i];
					if (tmp < 0)
					{
						extra_x = extra_x + tmp;
						for (int j = 0; j < 12; j++)
						{
							extra_shortcut_x[j] = extra_shortcut_x[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < x_min)
						x_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_width) > x_max)
						x_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_width);
					tmp = extra_shortcut_y[i];
					if (tmp < 0)
					{
						extra_y = extra_y + tmp;
						for (int j = 0; j < 12; j++)
						{
							extra_shortcut_y[j] = extra_shortcut_y[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < y_min)
						y_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_height) > y_max)
						y_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_extra) + Offset_shortcut) + Offset_height);
				}
				if (x_min > 0)
				{
					extra_x = extra_x + x_min;
					for (int i = 0; i < 12; i++)
					{
						extra_shortcut_x[i] = extra_shortcut_x[i] - x_min;
					}
				}
				if (y_min > 0)
				{
					extra_y = extra_y + y_min;
					for (int i = 0; i < 12; i++)
					{
						extra_shortcut_y[i] = extra_shortcut_y[i] - y_min;
					}
				}
				extra_w = x_max - x_min + 2;
				extra_h = y_max - y_min + 2;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				x_max = sep1_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_width);
				y_max = sep1_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_height);
				x_min = sep1_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_width);
				y_min = sep1_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_height);
				for (int i = 0; i < 12; i++)
				{
					tmp = sep1_shortcut_x[i];
					if (tmp < 0)
					{
						sep1_x = sep1_x + tmp;
						for (int j = 0; j < 12; j++)
						{
							sep1_shortcut_x[j] = sep1_shortcut_x[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < x_min)
						x_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_width) > x_max)
						x_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_width);
					tmp = sep1_shortcut_y[i];
					if (tmp < 0)
					{
						sep1_y = sep1_y + tmp;
						for (int j = 0; j < 12; j++)
						{
							sep1_shortcut_y[j] = sep1_shortcut_y[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < y_min)
						y_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_height) > y_max)
						y_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep1) + Offset_shortcut) + Offset_height);
				}
				if (x_min > 0)
				{
					sep1_x = sep1_x + x_min;
					for (int i = 0; i < 12; i++)
					{
						sep1_shortcut_x[i] = sep1_shortcut_x[i] - x_min;
					}
				}
				if (y_min > 0)
				{
					sep1_y = sep1_y + y_min;
					for (int i = 0; i < 12; i++)
					{
						sep1_shortcut_y[i] = sep1_shortcut_y[i] - y_min;
					}
				}
				sep1_w = x_max - x_min + 2;
				sep1_h = y_max - y_min + 2;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				x_max = sep2_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_width);
				y_max = sep2_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_height);
				x_min = sep2_shortcut_x[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_width);
				y_min = sep2_shortcut_y[0] + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_height);
				for (int i = 0; i < 12; i++)
				{
					tmp = sep2_shortcut_x[i];
					if (tmp < 0)
					{
						sep2_x = sep2_x + tmp;
						for (int j = 0; j < 12; j++)
						{
							sep2_shortcut_x[j] = sep2_shortcut_x[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < x_min)
						x_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_width) > x_max)
						x_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_width);
					tmp = sep2_shortcut_y[i];
					if (tmp < 0)
					{
						sep2_y = sep2_y + tmp;
						for (int j = 0; j < 12; j++)
						{
							sep2_shortcut_y[j] = sep2_shortcut_y[j] - tmp;
						}
						Calcule_Max_Rec();
						return ;
					}
					if (tmp < y_min)
						y_min = tmp;
					if (tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_height) > y_max)
						y_max = tmp + SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset_sep2) + Offset_shortcut) + Offset_height);
				}
				if (x_min > 0)
				{
					sep2_x = sep2_x + x_min;
					for (int i = 0; i < 12; i++)
					{
						sep2_shortcut_x[i] = sep2_shortcut_x[i] - x_min;
					}
				}
				if (y_min > 0)
				{
					sep2_y = sep2_y + y_min;
					for (int i = 0; i < 12; i++)
					{
						sep2_shortcut_y[i] = sep2_shortcut_y[i] - y_min;
					}
				}
				sep2_w = x_max - x_min + 2;
				sep2_h = y_max - y_min + 2;
			}
		}
		
		private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
		{
			WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
			placement.length = Marshal.SizeOf(placement);
			GetWindowPlacement(hwnd, ref placement);
			return placement;
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowPlacement(
			IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWPLACEMENT
		{
			public int length;
			public int flags;
			public ShowWindowCommands showCmd;
			public System.Drawing.Point ptMinPosition;
			public System.Drawing.Point ptMaxPosition;
			public System.Drawing.Rectangle rcNormalPosition;
		}

		internal enum ShowWindowCommands : int
		{
			Hide = 0,
			Normal = 1,
			Minimized = 2,
			Maximized = 3,
		}
		
		int ChangeFormQuickBar(int Offset, int form_toggle)
		{
			long game = AionProcess.Modules.Game;
			double w_skill = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut) + Offset_width);
			double h_skill = SplMemory.ReadDouble(SplMemory.ReadLong(SplMemory.ReadLong(game + Offset) + Offset_shortcut) + Offset_height);
			
			double[] x_skill = new double[12];
			double[] y_skill = new double[12];
			double w_quick = 0;
			double h_quick = 0;
			int x = 0;
			int y = 0;
			
			if (form_toggle == 0)
			{
				x = 3;
				y = 4;
				
				form_toggle++;
			}
			else if (form_toggle == 1)
			{
				x = 4;
				y = 3;
				
				form_toggle++;
			}
			else if (form_toggle == 2)
			{
				x = 2;
				y = 6;
				
				form_toggle++;
			}
			else if (form_toggle == 3)
			{
				x = 6;
				y = 2;
				
				form_toggle++;
			}
			else if (form_toggle == 4)
			{
				x = 1;
				y = 12;
				
				form_toggle++;
			}
			else if (form_toggle == 5 || form_toggle == -1)
			{
				x = 12;
				y = 1;
				
				form_toggle = 0;
			}
			
			w_quick = w_skill * x;
			h_quick = h_skill * y;
			
			for (int j = 0; j < y; j++)
			{
				for (int i = 0; i < x; i++)
				{
					x_skill[i + (j * x)] = w_skill * i;
					y_skill[i + (j * x)] = h_skill * j;
				}
			}
			
			if (Offset == Offset_ctrl)
			{
				ctrl_w = w_quick;
				ctrl_h = h_quick;
				for (int i = 0; i < 12; i++)
				{
					ctrl_shortcut_x[i] = x_skill[i];
					ctrl_shortcut_y[i] = y_skill[i];
				}
			}
			if (Offset == Offset_alt)
			{
				alt_w = w_quick;
				alt_h = h_quick;
				for (int i = 0; i < 12; i++)
				{
					alt_shortcut_x[i] = x_skill[i];
					alt_shortcut_y[i] = y_skill[i];
				}
			}
			if (Offset == Offset_extra)
			{
				extra_w = w_quick;
				extra_h = h_quick;
				for (int i = 0; i < 12; i++)
				{
					extra_shortcut_x[i] = x_skill[i];
					extra_shortcut_y[i] = y_skill[i];
				}
			}
			if (Offset == Offset_sep1)
			{
				sep1_w = w_quick;
				sep1_h = h_quick;
				for (int i = 0; i < 12; i++)
				{
					sep1_shortcut_x[i] = x_skill[i];
					sep1_shortcut_y[i] = y_skill[i];
				}
			}
			if (Offset == Offset_sep2)
			{
				sep2_w = w_quick;
				sep2_h = h_quick;
				for (int i = 0; i < 12; i++)
				{
					sep2_shortcut_x[i] = x_skill[i];
					sep2_shortcut_y[i] = y_skill[i];
				}
			}
			
			return (form_toggle);
		}
		
		void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}
		
		void DetailItem_Click(object sender, RoutedEventArgs e)
		{
			Detail = !Detail;
		}
		
		void Quickbar_dialog_ctrl_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_x--;
				if (e.Key == Key.Right)
					ctrl_x++;
				if (e.Key == Key.Up)
					ctrl_y--;
				if (e.Key == Key.Down)
					ctrl_y++;
			}
		}
		
		void Quickbar_dialog_alt_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_x--;
				if (e.Key == Key.Right)
					alt_x++;
				if (e.Key == Key.Up)
					alt_y--;
				if (e.Key == Key.Down)
					alt_y++;
			}
		}
		
		void Quickbar_dialog_extra_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_x--;
				if (e.Key == Key.Right)
					extra_x++;
				if (e.Key == Key.Up)
					extra_y--;
				if (e.Key == Key.Down)
					extra_y++;
			}
		}
		
		void Quickbar_dialog_sep1_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_x--;
				if (e.Key == Key.Right)
					sep1_x++;
				if (e.Key == Key.Up)
					sep1_y--;
				if (e.Key == Key.Down)
					sep1_y++;
			}
		}
		
		void Quickbar_dialog_sep2_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_x--;
				if (e.Key == Key.Right)
					sep2_x++;
				if (e.Key == Key.Up)
					sep2_y--;
				if (e.Key == Key.Down)
					sep2_y++;
			}
		}
		
		void Quickbar_dialog_shortcut1_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[0]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[0]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[0]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[0]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[0]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[0]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[0]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[0]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[0]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[0]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[0]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[0]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[0]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[0]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[0]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[0]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[0]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[0]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[0]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[0]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut2_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[1]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[1]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[1]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[1]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[1]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[1]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[1]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[1]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[1]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[1]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[1]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[1]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[1]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[1]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[1]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[1]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[1]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[1]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[1]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[1]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut3_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[2]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[2]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[2]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[2]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[2]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[2]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[2]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[2]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[2]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[2]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[2]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[2]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[2]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[2]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[2]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[2]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[2]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[2]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[2]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[2]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut4_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[3]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[3]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[3]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[3]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[3]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[3]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[3]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[3]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[3]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[3]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[3]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[3]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[3]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[3]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[3]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[3]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[3]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[3]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[3]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[3]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut5_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[4]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[4]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[4]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[4]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[4]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[4]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[4]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[4]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[4]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[4]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[4]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[4]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[4]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[4]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[4]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[4]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[4]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[4]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[4]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[4]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut6_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[5]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[5]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[5]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[5]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[5]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[5]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[5]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[5]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[5]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[5]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[5]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[5]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[5]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[5]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[5]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[5]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[5]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[5]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[5]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[5]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut7_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[6]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[6]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[6]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[6]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[6]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[6]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[6]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[6]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[6]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[6]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[6]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[6]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[6]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[6]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[6]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[6]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[6]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[6]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[6]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[6]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut8_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[7]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[7]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[7]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[7]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[7]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[7]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[7]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[7]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[7]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[7]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[7]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[7]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[7]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[7]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[7]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[7]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[7]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[7]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[7]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[7]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut9_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[8]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[8]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[8]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[8]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[8]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[8]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[8]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[8]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[8]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[8]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[8]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[8]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[8]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[8]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[8]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[8]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[8]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[8]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[8]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[8]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut10_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[9]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[9]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[9]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[9]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[9]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[9]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[9]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[9]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[9]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[9]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[9]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[9]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[9]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[9]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[9]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[9]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[9]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[9]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[9]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[9]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut11_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[10]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[10]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[10]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[10]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[10]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[10]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[10]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[10]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[10]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[10]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[10]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[10]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[10]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[10]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[10]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[10]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[10]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[10]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[10]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[10]++;
			}
			Calcule_Max_Rec();
		}
		
		void Quickbar_dialog_shortcut12_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (is_select == "quickbar_dialog_ctrl")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					ctrl_shortcut_x[11]--;
				if (e.Key == Key.Right)
					ctrl_shortcut_x[11]++;
				if (e.Key == Key.Up)
					ctrl_shortcut_y[11]--;
				if (e.Key == Key.Down)
					ctrl_shortcut_y[11]++;
			}
			if (is_select == "quickbar_dialog_alt")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					alt_shortcut_x[11]--;
				if (e.Key == Key.Right)
					alt_shortcut_x[11]++;
				if (e.Key == Key.Up)
					alt_shortcut_y[11]--;
				if (e.Key == Key.Down)
					alt_shortcut_y[11]++;
			}
			if (is_select == "quickbar_dialog_extra")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					extra_shortcut_x[11]--;
				if (e.Key == Key.Right)
					extra_shortcut_x[11]++;
				if (e.Key == Key.Up)
					extra_shortcut_y[11]--;
				if (e.Key == Key.Down)
					extra_shortcut_y[11]++;
			}
			if (is_select == "quickbar_dialog_sep1")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep1_shortcut_x[11]--;
				if (e.Key == Key.Right)
					sep1_shortcut_x[11]++;
				if (e.Key == Key.Up)
					sep1_shortcut_y[11]--;
				if (e.Key == Key.Down)
					sep1_shortcut_y[11]++;
			}
			if (is_select == "quickbar_dialog_sep2")
			{
				e.Handled = true;
				if (e.Key == Key.Left)
					sep2_shortcut_x[11]--;
				if (e.Key == Key.Right)
					sep2_shortcut_x[11]++;
				if (e.Key == Key.Up)
					sep2_shortcut_y[11]--;
				if (e.Key == Key.Down)
					sep2_shortcut_y[11]++;
			}
			Calcule_Max_Rec();
		}
	}
}