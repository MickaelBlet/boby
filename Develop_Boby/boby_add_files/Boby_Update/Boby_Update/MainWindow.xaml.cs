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
        static string       in_option                     = "";

		public MainWindow()
		{
			InitializeComponent();
		}

		public MainWindow(string update_name, string option)
		{
            string origin_path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            InitializeComponent();

			in_Win_Main = this;

			Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;

			img_ShugoLoading.Style = null;
			img_ShugoLoading.Style = Style_ShugoLoading;

            in_update_name = update_name;
            in_option = option;

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
                    if (MessageBox.Show("Close the other " + update_name + " application?", "Warning Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        Environment.Exit(0);
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
                if (option == "nolaunch")
                    Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                else
                    Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompletedLaunch);
                Client.DownloadFileAsync(new Uri("http://boby.pe.hu/files/download.php?file=" + update_name + ".exe"), origin_path + @"\" + update_name + ".exe");
            }

           // CheckUpdate();
        }

		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			double bytesIn = double.Parse(e.BytesReceived.ToString());
			double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
			double percentage = bytesIn / totalBytes * 100;
			this.PBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            if (this.PBar.Value * 2 < 120)
			    in_Win_Main.img_ShugoLoading.Height = this.PBar.Value * 2;
		}

		void client_DownloadFileCompletedLaunch(object sender, AsyncCompletedEventArgs e)
		{
            string origin_path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            try
			{
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = in_option;
                process.StartInfo.FileName = origin_path + @"\" + in_update_name + ".exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
			}
			catch (Exception)
			{
                MessageBox.Show("Please run as administrator.", "Error");
            }
			this.Close();
			Environment.Exit(0);
		}

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
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
				i += 1;
				if (i > 100)
					i = 0;
				in_Win_Main.Dispatcher.Invoke((Action)(() =>
				                                       {
                                                           if (i * 2 < 120)
                                                               in_Win_Main.img_ShugoLoading.Height = i * 2;
				                                       	in_Win_Main.PBar.Value = i;
				                                       }));
				Thread.Sleep(50);
			}
		}
	}
}
