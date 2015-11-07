using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace boby_checker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length != 1)
            {
                MessageBox.Show("You cant launch directly this application.", "Error");
                Environment.Exit(0);
            }
            string exe_name = e.Args[0];
            //string exe_name = "boby_multitools";

            MainWindow mainWindow = new MainWindow(exe_name);
            mainWindow.Show();
        }
	}
}