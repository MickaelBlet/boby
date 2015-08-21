/*
 **
 **          :::      ::::::::
 **        :+:      :+:    :+:
 **      +:+ +:+         +:+
 **     +#+  +:+       +#+
 **   +#+#+#+#+#+   +#+
 **        #+#    #+#
 **       ###   ########
 **
 ** Crée par SharpDevelop.
 ** Date: 31/07/2015
 ** Heure: 08:28
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Reflection;

using Ini;

namespace boby_checker
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		IniFile			Config = null;
		static string	save_exe_name;
		bool			pw_has_changed = false;

        void CheckUpdate()
        {
            string origin_path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            /*
            if file exist
            */
            if (File.Exists(origin_path + @"\boby_update.exe"))
            {
                string fileVersion = AssemblyName.GetAssemblyName(origin_path + @"\boby_update.exe").Version.ToString();
                string check_version_web = "";

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_update.exe");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Connection server.", "Error");
                        Environment.Exit(0);
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
                {
                    MessageBox.Show("Connection server.", "Error");
                    Environment.Exit(0);
                }

                if (fileVersion != check_version_web)
                {
                    using (WebClient Client = new WebClient())
                    {
                        Client.Proxy = null;
                        Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), origin_path + @"\boby_update.exe");
                    }
                }
            }
            else
            {
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), origin_path + @"\boby_update.exe");
                }
            }

            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = save_exe_name + " nolaunch";
                process.StartInfo.FileName = origin_path + @"\boby_update.exe";
                process.Start();
                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Launch boby_update.exe", "Error");
                Environment.Exit(0);
            }
        }
        void CheckUpdateChild()
        {
            string origin_path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            /*
            if file exist
            */
            if (File.Exists(origin_path + @"\boby_update.exe"))
            {
                string fileVersion = AssemblyName.GetAssemblyName(origin_path + @"\boby_update.exe").Version.ToString();
                string check_version_web = "";

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_update.exe");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Connection server.", "Error");
                        Environment.Exit(0);
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
                {
                    MessageBox.Show("Connection server.", "Error");
                    Environment.Exit(0);
                }

                if (fileVersion != check_version_web)
                {
                    using (WebClient Client = new WebClient())
                    {
                        Client.Proxy = null;
                        Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), origin_path + @"\boby_update.exe");
                    }
                }
            }
            else
            {
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), origin_path + @"\boby_update.exe");
                }
            }

            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = save_exe_name + " nolaunch";
                process.StartInfo.FileName = origin_path + @"\boby_update.exe";
                process.Start();
                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Launch boby_update.exe", "Error");
                Environment.Exit(0);
            }
        }

        public MainWindow(string exe_name)
		{
            string origin_path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            InitializeComponent();

            /*
            Read ini file
            */
			Config = new IniFile(origin_path + @"\boby_checker.ini");
			tb_username.Text = Config.IniReadValue("boby", "u");
			tb_password.Password = Config.IniReadValue("boby", "pw");
			
			if (tb_username.Text != "" && tb_password.Password != "")
				cb_remember.IsChecked = true;

            if (tb_password.Password != "")
                pw_has_changed = false;
            else
                pw_has_changed = true;

            save_exe_name = exe_name;

            CheckUpdate();
            CheckUpdateChild();

            tb_username.Focus();
		}
		
		void Bt_login_Click(object sender, RoutedEventArgs e)
		{
            string origin_path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string saltAndPwd = tb_password.Password;
            string hashedPwd = string.Empty;
            if (pw_has_changed)
            {
                SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();

                byte[] sha512Bytes = System.Text.Encoding.Default.GetBytes(saltAndPwd);

                byte[] cryString = sha512.ComputeHash(sha512Bytes);

                hashedPwd = string.Empty;

                for (int i = 0; i < cryString.Length; i++)
                {
                    hashedPwd += cryString[i].ToString("x2");
                }
            }
            else
            {
                hashedPwd = saltAndPwd;
            }

            if (save_exe_name == "boby_shulack")
            {
                if (Process.GetProcessesByName("boby_shulack").Length > 1)
                {
                    MessageBox.Show("Your cant launch other 2 boby_shulack.", "Error");
                    return;
                }
            }

            /*
            Request
            */
            string r_key;
            			
			try
			{
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["p"] = save_exe_name;
                    values["u"] = tb_username.Text;
                    values["w"] = hashedPwd;

                    MessageBox.Show(values["p"].ToString() + " " + values["u"].ToString() + " " + values["w"].ToString());

                    var response = client.UploadValues(@"http://boby.pe.hu/getkey.php", values);
                    r_key = Encoding.Default.GetString(response);
                }
			}
			catch
			{
				MessageBox.Show("Connection server.", "Error");
				return ;
			}

            if (r_key.Length != 128)
            {
                MessageBox.Show(r_key, "Error");
                return;
            }
			
            // launch other program
			try
			{
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = r_key + " \"" + tb_username.Text + "\"";
                process.StartInfo.FileName = origin_path + @"\" + save_exe_name + ".exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
			}
			catch (Exception)
			{
				MessageBox.Show("Please run as administrator.", "Error");
				return ;
			}
			
			if (cb_remember.IsChecked == true)
			{
				Config.IniWriteValue("boby", "u", tb_username.Text);
				Config.IniWriteValue("boby", "pw", hashedPwd);
			}
			else
			{
				Config.IniWriteValue("boby", "u", "");
				Config.IniWriteValue("boby", "pw", "");
			}
			
			Environment.Exit(0);
		}
		
		void Bt_Close_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
   
        void Tb_password_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            pw_has_changed = true;
        }

        void Tb_username_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				Bt_login_Click(null, null);
		}

        private void rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}