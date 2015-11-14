using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemoryLib
{
    class Ping
    {
        public static int tentative;
        public static DispatcherTimer messageTimer = null;

        public static void Online_Users_Sequence()
        {
            Thread T_Online = new Thread(Sequence_1);
            T_Online.SetApartmentState(ApartmentState.STA);
            T_Online.Start();
        }

        public static void Sequence_1()
        {
            if (messageTimer == null)
            {
                _Sequence_1(null, null);
                messageTimer = new DispatcherTimer();
                messageTimer.Tick += new EventHandler(_Sequence_1);
                messageTimer.Interval = new TimeSpan(0, 0, 20, 0, 0);
                messageTimer.Start();
                System.Windows.Threading.Dispatcher.Run();
            }
        }

        public static void _Sequence_1(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["k"] = BobyMultitools.App.User;

                    var response = client.UploadValues(@"http://boby.pe.hu/app/logs.php", values);
                }
                tentative = 0;
            }
            catch
            {
                tentative++;
                if (tentative < 6)
                    _Sequence_1(null, null);
            }
        }
    }
}
