using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemoryLib;
using Aion_Process;

namespace Aion_Game
{
    class DialogList
    {
        // private 
        private static Dictionary<string, Dialog> sList = new Dictionary<string, Dialog>();

        // public
        public static Dictionary<string, Dialog> List = new Dictionary<string, Dialog>(sList);

        DispatcherTimer messageTimer;

        public DialogList(int useless)
        {
            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            messageTimer.Start();
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ScanDialog();
            }
            catch { }
        }

        void ScanDialog()
        {
            if (Game.Pid == 0 || Game.Base == 0)
                return;
            Scan();
            List = new Dictionary<string, Dialog>(sList);
        }

        private void Scan()
        {
            HashSet<long> nodes = new HashSet<long>();

            long array = Offset.Dialog.Array;
            long size = Offset.Dialog.ArraySize;

            for (int i = 0; i < size; i++)
            {
                if (!nodes.Add(SplMemory.ReadLong(Game.Base + array + i * 4)))
                    continue;
            }

            foreach (var node in nodes)
            {
                if (node == 0)
                    continue;
                string name = "";
                Dialog dialog = new Dialog(node);

                sList[dialog.Name] = dialog;
            }
        }
    }
}
