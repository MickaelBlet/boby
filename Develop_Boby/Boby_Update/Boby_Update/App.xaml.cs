using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Boby_Update
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            string update_name = "";
            string option = "";
            if (e.Args.Length < 1)
            {
                MessageBox.Show("You cant launch directly this application.", "Error");
                Environment.Exit(0);
            }
            else if (e.Args.Length == 1)
            {
                update_name = e.Args[0];
            }
            else if (e.Args.Length == 2)
            {
                update_name = e.Args[0];
                option = e.Args[1];
            }

            MainWindow mainWindow = new MainWindow(update_name, option);
            mainWindow.Show();
        }
    }
}
