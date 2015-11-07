using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Specialized;

namespace BobyMultitools
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public string User;
        public string Offset;
        public DateTime SessionTime;
        void Checker()
        {
            string origin_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            if (!File.Exists(origin_path + @"\boby_checker.exe"))
            {
                Win_Download down = new Win_Download("boby_checker.exe");
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = "boby_multitools";
                process.StartInfo.FileName = origin_path + @"\boby_checker.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
            }
            catch (Exception)
            {
                try
                {
                    File.Delete(origin_path + @"\boby_checker.exe");
                    Win_Download down = new Win_Download("boby_checker.exe");
                    return;
                }
                catch (Exception)
                {
                }
            }
            Environment.Exit(0);
        }

        string Key(string key, string user)
        {
            string offset = string.Empty;

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["u"] = user;
                    values["k"] = key;

                    var response = client.UploadValues(@"http://boby.pe.hu/app/checkkey.php", values);
                    offset = Encoding.Default.GetString(response);
                }
            }
            catch
            {
                MessageBox.Show("Connection server.", "Error");
                Environment.Exit(0);
            }

            if (offset.Length == 0)
            {
                MessageBox.Show("Connection server.", "Error");
                Environment.Exit(0);
            }

            return offset;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
           /* if (e.Args.Length < 2)
            {
                Checker();
            }
            else
            {*/
               // Offset = Key(e.Args[0], e.Args[1]);
               // User = e.Args[1];
                Win_Main start = new Win_Main();
            //}
        }
    }
}