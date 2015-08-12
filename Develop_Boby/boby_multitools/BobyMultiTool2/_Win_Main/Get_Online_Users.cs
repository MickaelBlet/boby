using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices; // DllImport

using MemoryLib;
using NS_Aion_Game;
using _Threads;
using NS_Windows_And_Process;

namespace BobyMultitools
{
    public partial class Win_Main
    {
    	int				tentative;
        DispatcherTimer messageTimer_Online_Users;

        private void Online_Users_Sequence()
        {
            Thread T_Online = new Thread(Sequence_1);
            T_Online.SetApartmentState(ApartmentState.STA);
            T_Online.Start();
        }

        private void Sequence_1()
        {
            _Sequence_1(null, null);
            messageTimer_Online_Users = new DispatcherTimer();
            messageTimer_Online_Users.Tick += new EventHandler(_Sequence_1);
            messageTimer_Online_Users.Interval = new TimeSpan(0, 0, 4, 0, 0);
            messageTimer_Online_Users.Start();
            System.Windows.Threading.Dispatcher.Run();
        }

        void _Sequence_1(object sender, EventArgs e)
        {
            try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadString(@"http://boby.pe.hu/listing.php");
                    in_Win_Main.Dispatcher.Invoke((Action)(() =>
                    {
                        l_Users.Content = "Online Users: " + Client.DownloadString(@"http://boby.pe.hu/count_online.php");
                    }));
                }
                tentative = 0;
            }
            catch {
            	tentative++;
            	if (tentative > 5)
            	{
            		in_Win_Main.Dispatcher.Invoke((Action)(() =>
                    {
            			MessageBox.Show("Server not Found", "Error");
            			Environment.Exit(0);
            		}));
            	}
            }
        }
    }
}
