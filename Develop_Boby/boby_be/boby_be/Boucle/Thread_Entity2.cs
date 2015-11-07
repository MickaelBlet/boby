using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

using AddProcess;
using MemoryLib;

namespace _Threads
{
	public class Thread_Entity2
	{
        public Thread _thread = null;

		const int stop = -842150451; // 0xCDCDCDCD

        public static int date = 0;

        public static Dictionary<string, ImageSource> dictionary = new Dictionary<string, ImageSource>();
        public static Dictionary<long, Entity2> eList = new Dictionary<long, Entity2>();
        public Dictionary<long, Entity2> DicCopy = new Dictionary<long, Entity2>(eList);
		HashSet<long> found = null;
		public ImageSource[] Image_File = null;
        public static string tWhere = "";

        DispatcherTimer messageTimer;

        public Thread_Entity2()
        {
            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            messageTimer.Start();
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            ThreadStartEntity();
        }

		void ThreadStartEntity()
		{
            if (AionProcess.pid2 != 0 && AionProcess.Modules2.Game != 0)
            {
                //Thread.Sleep(100);
                try
                {
                    Update();
                    DicCopy = new Dictionary<long, Entity2>(eList);
                    date = DateTime.Now.Millisecond;
                    //if (in_Win_Main.in_Win_Entity != null)
                    //     in_Win_Main.in_Win_Entity.Entity_List_View();
                }
                catch
                { }
            }
		}
		
		public void Update()
		{
			found = new HashSet<long>();

            long entityMap = SplMemory2.ReadLong(AionProcess.Modules2.Game + (long)Offset.EntityList.Pointer);
			long entityArray = SplMemory2.ReadLong(entityMap + (long)Offset.EntityList.Array);

			TraverseNode(SplMemory2.ReadLong(entityArray));
			List<long> entitiesToRemove = new List<long>();
			foreach (var entity in eList.Values)
			{
                if (found.Contains(entity.PtrEntity))
                {
                    if (entity.ID != 0 || entity.Name != "" || entity.Type == EnumAion.eType.Vehicle)
                        entity.Update();
                    else
                        entitiesToRemove.Add(entity.PtrEntity);
                }
                else
                    entitiesToRemove.Add(entity.PtrEntity);
			}
            foreach (var entityPtr in entitiesToRemove)
                eList.Remove(entityPtr);

			foreach (var newEntityPtr in found.Except(eList.Keys))
				eList[newEntityPtr] = new Entity2(newEntityPtr);

            found.Clear();
            found = null;
		}
		
		void TraverseNode(long nodePtr)
		{
			try
			{
				long entityPtr = SplMemory2.ReadLong(nodePtr + 12);
				if (entityPtr == 0 || entityPtr == stop || !found.Add(entityPtr))
					return;
                entityPtr = SplMemory2.ReadLong(nodePtr + 0);

                if (entityPtr != 0 && entityPtr != stop)
				{
                    TraverseNode(entityPtr);
				}
			}
			catch (Exception)
			{
				// uh oh, changing memory structure?
				// lets just back away slowly.
			}
		}
	}

    public class Entity2
    {
        const int stop = -842150451;

        public long PtrEntity;

        public int ID;
        public int ID_Object;
        public int ID_Type_NPC;
        public int ID_Type_Quest;
        public int ID_Type_Quest_Type;
        public int Rank;
        public int TargetID;
        public string Name;
        public string Guild;
        public string Nametolower;
        public int Lvl;
        public int HP_Percent;
        public int HP;
        public int Class;
        public long Link;
        public int Type;
        public int Stance;
        public int Action;
        public int DistanceReal;
        public int Distance;
        public bool Aggro;
        public bool Group;
        public bool Force;
        public int Is_Attackable;
        public ImageSource _Image;
        public ImageSource _Image_Object;
        public ImageSource _Image_Where;

        public int Buff;

        public float X;
        public float Y;
        public float Z;

        public int Race;

        public long _PtrEntity;

        private string tWhere;

        public Entity2()
        {
        }

        public Entity2(long Ptr)
        {
            this.PtrEntity = Ptr;
            this.Type = 0;
            this.Update();
        }

        public void Update()
        {
            this._PtrEntity = SplMemory2.ReadLong(PtrEntity + Offset.Entity.Status);
            if (PtrEntity != 0 && this._PtrEntity != stop)
            {
                try
                {
                    this.Type = SplMemory2.ReadByte(PtrEntity + Offset.Entity.Type);
                    this.Name = SplMemory2.ReadWchar(_PtrEntity + Offset.Status.Name, 60);
                }
                catch
                {
                    this.Type = 0;
                }
            }
            else
                this.Type = 0;
        }
    }
}