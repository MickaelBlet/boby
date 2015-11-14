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
    public class DialogList
    {
        // private 
        private static Dictionary<string, Dialog> sList = new Dictionary<string, Dialog>();

        // public
        public static Dictionary<string, Dialog> List = new Dictionary<string, Dialog>(sList);

        DispatcherTimer messageTimer;

        public static Dialog GetDialog(string name)
        {
            if (!name.Contains('>'))
            {
                if (List.ContainsKey(name))
                    return List[name];
                else
                    return new Dialog(0);
            }

            string[] tree = name.Split('>');
            if (!List.ContainsKey(tree[0]))
                return new Dialog(0);
            Dialog dlg = List[tree[0]];
            dlg.Update();
            for (int i = 1; i < tree.Length; i++)
            {
                dlg.UpdateChild();
                if (!dlg.Childs.ContainsKey(tree[i]))
                    return new Dialog(0);
                dlg = dlg.Childs[tree[i]];
            }
            return dlg;
        }

        public DialogList()
        {
            ;
        }
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
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        void ScanDialog()
        {
            if (Game.Pid == 0 || Game.Base == 0 || !Player.IsPlayer())
                return;
            Scan();
            List = new Dictionary<string, Dialog>(sList);
        }

        private void Scan()
        {
            HashSet<long> nodes = new HashSet<long>();

            long array = Offset.Dialog.Array;
            long size = Offset.Dialog.ArraySize / 0x4;

            for (int i = 0; i < size; i++)
                if (!nodes.Add(SplMemory.ReadLong(Game.Base + array + i * 4)))
                    continue;

            sList = new Dictionary<string, Dialog>();
            int index = 0;
            foreach (var node in nodes)
            {
                if (node == 0 || node == 0xFFFFFFFF)
                    continue;
                var validate = SplMemory.ReadChar(node + Offset.Dialog.Validate, 8);
                if (validate != "Vera" && validate != "MYRIADPR")
                    continue;
                //System.Windows.MessageBox.Show(node.ToString("X"));
                Dialog dialog = new Dialog(node, index);
                sList[dialog.Name] = dialog;
                index++;
            }
        }
    }
}
