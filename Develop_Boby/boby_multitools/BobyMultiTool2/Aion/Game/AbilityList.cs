using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aion_Process;
using MemoryLib;

namespace Aion_Game
{
    public class AbilityList
    {
        // private 
        private static Dictionary<long, Ability> pList = new Dictionary<long, Ability>();
        private static Dictionary<string, Ability> sList = new Dictionary<string, Ability>();
        private static Dictionary<string, Ability> sminList = new Dictionary<string, Ability>();
        private static Dictionary<int, Ability> iList = new Dictionary<int, Ability>();

        // public
        public static Dictionary<string, Ability> List = new Dictionary<string, Ability>(sList);
        public static Dictionary<string, Ability> MinList = new Dictionary<string, Ability>(sminList);
        public static Dictionary<int, Ability> IdList = new Dictionary<int, Ability>(iList);

        DispatcherTimer messageTimer;

        public static Ability GetAbility(string name)
        {
            if (List.ContainsKey(name))
                return List[name];
            else
                return new Ability(0);
        }

        public static Ability GetAbility(int id)
        {
            if (IdList.ContainsKey(id))
                return IdList[id];
            else
                return new Ability(0);
        }

        public AbilityList()
        {
            ;
        }
        public AbilityList(int useless)
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
                ScanAbility();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        void ScanAbility()
        {
            if (Game.Pid == 0 || Game.Base == 0 || !Player.IsPlayer())
                return;
            Scan();

            // string => maxLvl
            sList = new Dictionary<string, Ability>();
            sminList = new Dictionary<string, Ability>();
            foreach (var ability in pList.Values)
            {
                if (sList.ContainsKey(ability.Name))
                {
                    if (sList[ability.Name].Lvl < ability.Lvl)
                        sList[ability.Name] = ability;
                }
                else
                {
                    sList[ability.Name] = ability;
                }
                if (sminList.ContainsKey(ability.Name))
                {
                    if (sminList[ability.Name].Lvl > ability.Lvl)
                        sminList[ability.Name] = ability;
                }
                else
                {
                    sminList[ability.Name] = ability;
                }
            }

            // Lvl => maxLvl
            iList = new Dictionary<int, Ability>();
            foreach (var ability in pList.Values)
                iList[ability.Id] = sList[ability.Name];

            IdList = new Dictionary<int, Ability>(iList);
            MinList = new Dictionary<string, Ability>(sminList);
            List = new Dictionary<string, Ability>(sList);
        }

        private void Scan()
        {
            HashSet<long> save_node = new HashSet<long>();
            HashSet<long> save_node_ability = new HashSet<long>();
            List<long> to_remove = new List<long>();

            long map = SplMemory.ReadLong(Game.Base + Offset.Ability.Map);
            long node = SplMemory.ReadLong(map + Offset.Ability.Node);

            Node_Scan(node, ref save_node_ability, ref save_node);

            foreach (var ability in pList.Values)
            {
                if (save_node_ability.Contains(ability.Node))
                    ability.Update();
                else
                    to_remove.Add(ability.Node);
            }
            foreach (var abilityPtr in to_remove)
                pList.Remove(abilityPtr);
            foreach (var newAbilityPtr in save_node_ability.Except(pList.Keys))
                pList[newAbilityPtr] = new Ability(newAbilityPtr);
        }

        void Node_Scan(long node, ref HashSet<long> save_node_ability, ref HashSet<long> save_node)
        {
            try
            {
                if (node == 0 || node == 0xCDCDCDCD || !save_node.Add(node))
                    return;
                long ability_node = SplMemory.ReadLong(node + 0x10);
                if (!save_node_ability.Add(ability_node))
                    return;
                Node_Scan(SplMemory.ReadLong(node), ref save_node_ability, ref save_node);
                Node_Scan(SplMemory.ReadLong(node + 0x4), ref save_node_ability, ref save_node);
                Node_Scan(SplMemory.ReadLong(node + 0x8), ref save_node_ability, ref save_node);
            }
            catch (Exception)
            {
                Console.WriteLine("Node_Scan: Ability");
                // uh oh, changing memory structure?
                // lets just back away slowly.
            }
        }
    }
}
