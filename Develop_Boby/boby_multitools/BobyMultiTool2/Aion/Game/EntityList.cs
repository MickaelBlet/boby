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
    public class EntityList
    {
        // locker
        public static object locker = new object();

        // private 
        private static Dictionary<long, Entity> pList = new Dictionary<long, Entity>();
        private static Dictionary<string, Entity> sList = new Dictionary<string, Entity>();

        // public
        public static Dictionary<long, Entity> uList = new Dictionary<long, Entity>(pList);
        public static Dictionary<string, Entity> List = new Dictionary<string, Entity>(sList);

        public Entity this[string name_or_id]
        {
            get
            {
                if (List.ContainsKey(name_or_id))
                {
                    System.Windows.MessageBox.Show(List[name_or_id].Distance2D.ToString() + "m");
                    return List[name_or_id];
                }
                else
                    return new Entity(0);
            }
        }

        public static Entity GetEntity(string name_or_id)
        {
            if (List.Count > 0 && List.ContainsKey(name_or_id))
                return List[name_or_id];
            else
                return new Entity(0);
        }

        public static List<Entity> GetEntity(eType type)
        {
            List<Entity> result = new List<Entity>();
            if (uList.Count > 0)
            {
                foreach(var entity in uList.Values)
                {
                    if (entity.Type == type)
                        result.Add(entity);
                }
            }
            return result;
        }

        DispatcherTimer messageTimer;

        public EntityList()
        {
            ;
        }

        public EntityList(int useless)
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
                ScanEntity();
            }
            catch { }
        }

        void ScanEntity()
        {
            if (Game.Pid == 0 || Game.Base == 0)
                return;
            Scan();
            uList = new Dictionary<long, Entity>(pList);
            sList = new Dictionary<string, Entity>();
            foreach (var list_entity in pList)
            {
                if (sList.ContainsKey(list_entity.Value.Id.ToString()))
                {
                    if (sList[list_entity.Value.Id.ToString()].Distance3D > list_entity.Value.Distance3D)
                        sList[list_entity.Value.Id.ToString()] = list_entity.Value;
                }
                else
                    sList.Add(list_entity.Value.Id.ToString(), list_entity.Value);
                if (sList.ContainsKey(list_entity.Value.Name))
                {
                    if (sList[list_entity.Value.Name].Distance3D > list_entity.Value.Distance3D)
                        sList[list_entity.Value.Name] = list_entity.Value;
                }
                else
                    sList.Add(list_entity.Value.Name, list_entity.Value);
            }
            List = new Dictionary<string, Entity>(sList);

            //Entity_To_View.messageTimer_Tick(null, null);

            /*foreach (var entity in List.Values)
            {
                if (entity.Name != "")
                    System.Windows.MessageBox.Show(entity.Name);
            }*/
        }

        private void Scan()
        {
            HashSet<long> save_node_entity = new HashSet<long>();
            HashSet<long> save_node = new HashSet<long>();
            List<long> entity_remove = new List<long>();

            long entity_map = SplMemory.ReadLong(Game.Base + Offset.EntityList.Map);
            long entityArray = SplMemory.ReadLong(entity_map + Offset.EntityList.Node);
            long firstEntity = SplMemory.ReadLong(entityArray);

            Node_Scan(firstEntity, ref save_node_entity, ref save_node);

            foreach (var entity in pList.Values)
            {
                if (save_node_entity.Contains(entity.Node))
                    entity.Update();
                else
                    entity_remove.Add(entity.Node);
            }
            foreach (var entityPtr in entity_remove)
                pList.Remove(entityPtr);
            foreach (var newEntityPtr in save_node_entity.Except(pList.Keys))
                pList[newEntityPtr] = new Entity(newEntityPtr);
        }

        void Node_Scan(long node, ref HashSet<long> save_node_entity, ref HashSet<long> save_node)
        {
            try
            {
                if (node == 0 || node == 0xCDCDCDCD || !save_node.Add(node))
                    return;
                long entity_node = SplMemory.ReadLong(node + 0xC);
                if (entity_node == 0 || entity_node == 0xCDCDCDCD || !save_node_entity.Add(entity_node))
                    return;
                Node_Scan(SplMemory.ReadLong(node), ref save_node_entity, ref save_node);
            }
            catch (Exception)
            {
                Console.WriteLine("Node_Scan: Entity");
                // uh oh, changing memory structure?
                // lets just back away slowly.
            }
        }
    }
}
