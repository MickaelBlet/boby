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
        public static string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public static string User;
        public static string Offset;
        public DateTime SessionTime;
        void Checker()
        {
            if (!File.Exists(Path + @"\boby_checker.exe"))
            {
                new Win_Download("boby_checker.exe");
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = Path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = "boby_multitools";
                process.StartInfo.FileName = Path + @"\boby_checker.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
            }
            catch (Exception)
            {
                try
                {
                    File.Delete(Path + @"\boby_checker.exe");
                    new Win_Download("boby_checker.exe");
                    return;
                }
                catch (Exception)
                {
                }
            }
            Environment.Exit(0);
        }

        void Key(string key)
        {
            string offset = string.Empty;
            int tentative = 0;

            while (offset.Length == 0 && tentative < 5)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["k"] = key;

                        var response = client.UploadValues(@"http://bobytools.com/app/checkkey.php", values);
                        offset = Encoding.Default.GetString(response);
                    }
                }
                catch
                {
                    if (tentative == 4)
                    {
                        MessageBox.Show("Connection server.", "Error");
                        Environment.Exit(0);
                    }
                }
                tentative++;
            }
            if (offset.Length == 0)
            {
                MessageBox.Show("Connection server.", "Error");
                Environment.Exit(0);
            }

            char[] t = new char[1];
            t[0] = '|';
            string[] spl = offset.Split(t, 2);

            //MessageBox.Show(spl[0]);
            //MessageBox.Show(spl[1]);

            if (spl.Length == 2)
            {
                User = spl[0];
                Offset = spl[1];
            }
            else
                Environment.Exit(0);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length < 1)
            {
                Checker();
            }
            else
            {
                Setting.Load();
                Key(e.Args[0]);
                Win_Main start = new Win_Main();
            }
        }
    }
}