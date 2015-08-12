using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Boby_Update
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow   in_Win_Main                   = null;
		public Style        Style_ShugoLoading            = null;
		public string       in_update_name                = "";

		public MainWindow()
		{
			InitializeComponent();
		}

		public MainWindow(string update_name)
		{
			InitializeComponent();

			in_Win_Main = this;

			Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;

			img_ShugoLoading.Style = null;
			img_ShugoLoading.Style = Style_ShugoLoading;

			in_update_name = update_name;

			string fileVersion = "";
			
			if (System.IO.File.Exists(@".\" + update_name + ".exe"))
			{
				fileVersion = AssemblyName.GetAssemblyName(@"./" + update_name + ".exe").Version.ToString();

				string check_version_web = "";
				
				using (WebClient Client = new WebClient())
				{
					Client.Proxy = null;
					try
					{
						check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + update_name + ".exe");
					}
					catch (Exception)
					{
					}
				}
				
				if (check_version_web == "..." || check_version_web == "")
				{
					foreach (Process proc in Process.GetProcessesByName(update_name))
						proc.Kill();
					Environment.Exit(0);
				}
				
				if (fileVersion == check_version_web)
				{
					Environment.Exit(0);
				}
				
				if (Process.GetProcessesByName(update_name).Length > 1)
				{
					if (MessageBox.Show("Close the other " + update_name + " Application?", "Warning Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
					{
						Environment.Exit(0);
					}
				}
				
				while (System.IO.File.Exists(@".\" + update_name + ".exe"))
				{
					try
					{
						foreach (Process proc in Process.GetProcessesByName(update_name))
							proc.Kill();
						System.IO.File.Delete(@".\" + update_name + ".exe");
					}
					catch (Exception)
					{
						Thread.Sleep(100);
					}
				}
			}
			else
			{
				foreach (Process proc in Process.GetProcessesByName(update_name))
					proc.Kill();
			}

			using (WebClient Client = new WebClient())
			{
				Client.Proxy = null;
				Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				Client.DownloadFileAsync(new Uri("http://boby.pe.hu/files/download.php?file=" + update_name + ".exe"), update_name + ".exe");
			}

			CheckUpdate();
		}

		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			double bytesIn = double.Parse(e.BytesReceived.ToString());
			double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
			double percentage = bytesIn / totalBytes * 100;
			this.PBar.Value = int.Parse(Math.Truncate(percentage).ToString());
			in_Win_Main.img_ShugoLoading.Height = this.PBar.Value * 2;
		}

		void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.WorkingDirectory = @".\";
				startInfo.Verb = "runas";
				startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + in_update_name + ".exe";
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(startInfo);
			}
			catch (Exception)
			{
				MessageBox.Show("Essayez de lancer le programme en tant qu'Administrateur.", "Erreur de lancement");
			}
			this.Close();
			Environment.Exit(0);
		}

		void CheckUpdate()
		{
			Thread tmp_thread = new Thread(Thread_CheckUpdate);
			tmp_thread.Start();
		}

		void Thread_CheckUpdate()
		{
			int i = 0;
			while (true)
			{
				i += 100;
				if (i > 100)
					i = 0;
				in_Win_Main.Dispatcher.Invoke((Action)(() =>
				                                       {
				                                       	if (i * 2 <= 120)
				                                       		in_Win_Main.img_ShugoLoading.Height = i * 2;
				                                       	in_Win_Main.PBar.Value = i;
				                                       }));
				Thread.Sleep(1000);
			}
		}
	}
}
