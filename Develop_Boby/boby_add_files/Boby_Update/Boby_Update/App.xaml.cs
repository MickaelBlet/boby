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
            //if (e.Args.Length < 1)
            //    Environment.Exit(0);
            //string update_name = e.Args[0];
            string update_name = "boby_be";
            string option = "nolaunch";

            MainWindow mainWindow = new MainWindow(update_name, option);
            mainWindow.Show();
        }
    }
}
