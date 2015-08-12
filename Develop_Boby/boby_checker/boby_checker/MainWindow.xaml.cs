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
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Linq;
using System.Net;
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
		
		public MainWindow(string exe_name)
		{
			InitializeComponent();
			
			Config = new IniFile("./boby_checker.ini");
			tb_username.Text = Config.IniReadValue("boby", "u");
			tb_password.Password = Config.IniReadValue("boby", "pw");
			
			if (tb_username.Text != "" && tb_password.Password != "")
				cb_remember.IsChecked = true;
			
			if (tb_password.Password != "")
				pw_has_changed = false;
			else
				pw_has_changed = true;
			
			save_exe_name = exe_name;
			
			tb_username.Focus();
		}
		
		void Bt_login_Click(object sender, RoutedEventArgs e)
		{
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
			
			string r_key;
			
			try
			{
				using (WebClient Client = new WebClient())
				{
					Client.Proxy = null;
					r_key = Client.DownloadString(@"http://boby.pe.hu/getkey.php?p="+save_exe_name+"&u="+tb_username.Text+"&w="+hashedPwd);
				}
			}
			catch
			{
				MessageBox.Show("Connection server.", "Error");
				return ;
			}
					
			if (r_key == "expired")
			{
				MessageBox.Show("Your account has expired", "Error");
				return ;
			}
			
			if (r_key == "multi")
			{
				MessageBox.Show("Your cant launch other 2 boby_shulack, please wait 1 minute and try again.", "Error");
				return ;
			}
			
			if (r_key.Length != 128)
			{
				MessageBox.Show("Authentification failure", "Error");
				return ;
			}
			
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.WorkingDirectory = @".\";
				startInfo.Verb = "runas";
				startInfo.Arguments = r_key + " \"" + tb_username.Text + "\"";
				startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + save_exe_name + ".exe";
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(startInfo);
			}
			catch (Exception)
			{
				MessageBox.Show("Please launch in Admin. mode.", "Error");
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
	}
}