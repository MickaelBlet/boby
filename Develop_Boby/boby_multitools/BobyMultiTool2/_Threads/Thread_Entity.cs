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

using MemoryLib;
using NS_Aion_Game;
using BobyMultitools;

namespace _Threads
{
	public class Thread_Entity
	{
        public Thread _thread = null;

		const int stop = -842150451; // 0xCDCDCDCD

        public static int date = 0;

        public static Player ePlayer;
		public static Win_Main in_Win_Main = null;
        public static Dictionary<string, ImageSource> dictionary = new Dictionary<string, ImageSource>();
        public static Dictionary<long, Entity> eList = new Dictionary<long, Entity>();
        public Dictionary<long, Entity> DicCopy = new Dictionary<long, Entity>(eList);
		HashSet<long> found = null;
		public ImageSource[] Image_File = null;
        public static string tWhere = "";

        DispatcherTimer messageTimer;

        public Thread_Entity()
        {
        }

        public Thread_Entity(Win_Main tmp_Win_Main)
		{
            in_Win_Main = tmp_Win_Main;
            in_Win_Main.in_Thread_Entity = this;

			ePlayer = new Player();
            new EnumAion.Gather();

            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            messageTimer.Start();
		}
		
		public static Player Return_Player()
		{
			return ePlayer;
		}

        void messageTimer_Tick(object sender, EventArgs e)
        {
            ThreadStartEntity();
        }

		void ThreadStartEntity()
		{
            if (Aion_Game.pid != 0 && Aion_Game.Modules.Game != 0)
            {
                //Thread.Sleep(100);
                try
                {
                    tWhere = SplMemory.ReadWchar(Aion_Game.Modules.Game + Offset.Game.Where, 60);
                    ePlayer.Update();
                    Update();
                    DicCopy = new Dictionary<long, Entity>(eList);
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

            long entityMap = SplMemory.ReadLong(Aion_Game.Modules.Game + (long)Offset.EntityList.Pointer);
			long entityArray = SplMemory.ReadLong(entityMap + (long)Offset.EntityList.Array);

			TraverseNode(SplMemory.ReadLong(entityArray));
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
				eList[newEntityPtr] = new Entity(newEntityPtr);

            found.Clear();
            found = null;
		}
		
		void TraverseNode(long nodePtr)
		{
			try
			{
				long entityPtr = SplMemory.ReadLong(nodePtr + 12);
				if (entityPtr == 0 || entityPtr == stop || !found.Add(entityPtr))
					return;
                entityPtr = SplMemory.ReadLong(nodePtr + 4);

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
	
	public class Player
	{
		public float	X;
		public float	Y;
		public float	Z;
		public int		ID;
        public Entity   Entity_Player;
		public int[]	ID_Group = new int[24];
		public int[]	ID_Force = new int[24];
		
		public Player()
		{
			//this.Update();
		}

		public void Update()
		{
            long Modules = Aion_Game.Modules.Game;
            try
            {
                this.X = SplMemory.ReadFloat(Modules + Offset.Player.X);
                this.Y = SplMemory.ReadFloat(Modules + Offset.Player.Y);
                this.Z = SplMemory.ReadFloat(Modules + Offset.Player.Z);
                this.ID = SplMemory.ReadInt(Modules + Offset.Player.ID);

                long GroupEntityBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["party_dialog"]) + Offset.PartyList.Jump;
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        this.ID_Group[i] = SplMemory.ReadInt(GroupEntityBase + i * (long)Offset.PartyList.Length);
                    }
                    catch
                    {
                        this.ID_Group[i] = 0;
                    }
                }
             }
            catch
            { }
            try
            {
                long ForceEntityBase = SplMemory.ReadLong((long)Offset.Base_windows.newbase["force_dialog"]) + Offset.PartyList.Jump + 0x8;
                for (int i = 0; i < 24; i++)
                {
                    try
                    {
                        this.ID_Force[i] = SplMemory.ReadInt(ForceEntityBase + i / 6 * 0x8 + i * Offset.ForceList.Length);
                    }
                    catch
                    {
                        this.ID_Force[i] = 0;
                    }
                }
            }
            catch
            { }
		}
	}

	public class Entity
	{
		const int stop = -842150451;
		
		public long 		PtrEntity;
		
		public int			ID;
		public int			ID_Object;
		public int			ID_Type_NPC;
		public int			ID_Type_Quest;
		public int			ID_Type_Quest_Type;
		public int			Rank;
		public int			TargetID;
		public string		Name;
		public string		Guild;
		public string		Nametolower;
		public int			Lvl;
		public int			HP_Percent;
		public int			HP;
		public int			Class;
        public long			Link;
		public int			Type;
		public int			Stance;
		public int			Action;
		public int			DistanceReal;
		public int			Distance;
		public bool			Aggro;
		public bool			Group;
		public bool			Force;
		public int			Is_Attackable;
		public Icon     	_Icon;
        public ImageSource	_Image;
        public ImageSource	_Image_Object;
        public ImageSource	_Image_Where;

		public int			Buff;
		
		public float		X;
		public float		Y;
		public float		Z;
		
		public int			Race;

        public long		_PtrEntity;

        private string  tWhere;
		
		public Entity()
		{
		}
		
		public Entity(long Ptr)
		{
			this.PtrEntity = Ptr;
			SetZero();
			this.Update();
		}
		
		public void Update()
		{
			if (PtrEntity != 0 && this._PtrEntity != stop)
			{
				try
				{
					Player ePlayer = Thread_Entity.Return_Player();

                    tWhere = Thread_Entity.tWhere;

					long EntityLoc = SplMemory.ReadLong(PtrEntity + Offset.Entity.Loc);
					this.X = SplMemory.ReadFloat(EntityLoc + Offset.Loc.X);
					this.Y = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Y);
					this.Z = SplMemory.ReadFloat(EntityLoc + Offset.Loc.Z);
					
					GetDistance(ePlayer);

					this._PtrEntity = SplMemory.ReadLong(PtrEntity + Offset.Entity.Status);
					if (this._PtrEntity != 0 && this._PtrEntity != stop)
					{
						this.ID 			= SplMemory.ReadInt(_PtrEntity + Offset.Status.ID);
						this.ID_Object		= SplMemory.ReadInt(_PtrEntity + Offset.Status.ID_Object);
						this.Name 			= SplMemory.ReadWchar(_PtrEntity + Offset.Status.Name, 60);
						this.Lvl 			= SplMemory.ReadByte(_PtrEntity + Offset.Status.Lvl);
						this.HP 			= SplMemory.ReadInt(_PtrEntity + Offset.Status.HP);
						this.HP_Percent		= SplMemory.ReadByte(_PtrEntity + Offset.Status.HP_Percent);
						this.Type			= SplMemory.ReadByte(PtrEntity + Offset.Entity.Type);
						this.Stance			= SplMemory.ReadByte(_PtrEntity + Offset.Status.Stance);
						this.Action			= SplMemory.ReadByte(_PtrEntity + Offset.Status.Action);
						this.Race			= SplMemory.ReadByte(_PtrEntity + Offset.Status.Type);
						this.Is_Attackable	= SplMemory.ReadInt(_PtrEntity + Offset.Status.Is_Attackable);
						this.TargetID 		= SplMemory.ReadInt(_PtrEntity + Offset.Status.TargetId);
                        if (this.Type == EnumAion.eType.User)
                        {
                            this.Guild = SplMemory.ReadWchar(_PtrEntity + Offset.Status.Guild, 60);
                            this.Rank = checkRank(SplMemory.ReadInt(_PtrEntity + Offset.Status.Rank));
                            this.Class = SplMemory.ReadByte(_PtrEntity + Offset.Status.Class);
                        }
                        else
                        {
                            this.Guild = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
                            this.Rank = -this.DistanceReal;
                            this.Class = 100 + this.DistanceReal;
                        }
                        if (this.Type == EnumAion.eType.Gather && EnumAion.Gather.gather.ContainsKey(this.ID_Object))
                        {
                            this.Lvl = (int)EnumAion.Gather.gather[this.ID_Object];
                        }
						Buff_Fufu();
						this.Update2();
						this.Update_img();
					}

                    if (this.Type == EnumAion.eType.Player)
                    {
                        ePlayer.Entity_Player = this;
                    }
				}
				catch
				{
					SetZero();
				}
			}
			else
				SetZero();
		}
		
		public void Update2()
		{
			Player ePlayer = Thread_Entity.Return_Player();
			
			this.Nametolower	= this.Name.ToLower();
            this.Link           = SplMemory.ReadLong(_PtrEntity + Offset.Status.Link);
			this.Aggro 			= checkAggro(ePlayer);
			if (this.Type == EnumAion.eType.User && this.Race == EnumAion.eAttitude.Friendly)
			{
				this.Group		= checkGroup(ePlayer);
				if (!this.Group)
					this.Force	= checkForce(ePlayer);
			}
			if (this.Type == EnumAion.eType.NPC && this.Race == EnumAion.eAttitude.Friendly)
			{
				this.ID_Type_NPC	= SplMemory.ReadInt(_PtrEntity + Offset.Status.ID_Type_NPC);
				this.ID_Type_Quest	= SplMemory.ReadInt(_PtrEntity + Offset.Status.ID_Type_Quest);
				this.ID_Type_Quest_Type	= SplMemory.ReadInt(_PtrEntity + Offset.Status.ID_Type_Quest_Type);
			}
            this._Icon = Check_If_PNG();
		}
		
		public void Update_img()
		{
            this._Image_Object = null;
            this._Image_Where = null;
			this._Image = null;

			if (this.Type == EnumAion.eType.Gather)
			{
                if (this.DistanceReal < 113 && Thread_Entity.in_Win_Main.in_Setting.in_Entity.Where != "" && this.Nametolower.Contains(Thread_Entity.in_Win_Main.in_Setting.in_Entity.Where))
                {
                    this._Image_Where = (ImageSource)Application.Current.FindResource("Where.Where_Yellow");
                    this.DistanceReal = -this.DistanceReal;
                    this.Rank = -this.Rank;
                    this.Class = -this.Class;
                }
                else if (tWhere != "" && this.Nametolower.Contains(tWhere.ToLower()))
                {
                    this._Image_Where = (ImageSource)Application.Current.FindResource("Where.Where");
                }
                else if (this.Action == EnumAion.eAction.Gatherable || this.Action == EnumAion.eAction.Low_Gatherable)
                {
                    if (this.ID_Object >= 401001 && this.ID_Object <= 401077)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ether");
                    else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Gather");
                }
                else if (this.Action == EnumAion.eAction.NotGatherable || this.Action == EnumAion.eAction.Low_NotGatherable)
                {
                    if (this.ID_Object >= 401001 && this.ID_Object <= 401077)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ether_Not");
                    else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Gather_Not");
                }
			}
			else if (this.HP != 0 && this.Stance != EnumAion.eStance.Dead)
			{
                if (this.DistanceReal < 113 && Thread_Entity.in_Win_Main.in_Setting.in_Entity.Where != "" && this.Nametolower.Contains(Thread_Entity.in_Win_Main.in_Setting.in_Entity.Where))
                {
                    this._Image_Where = (ImageSource)Application.Current.FindResource("Where.Where_Yellow");
                    this.DistanceReal = -this.DistanceReal;
                    if (this.Type != EnumAion.eType.User)
                    {
                        this.Rank = -this.Rank;
                        this.Class = -this.Class;
                    }
                }
                else if (tWhere != "" && this.Nametolower.Contains(tWhere.ToLower()))
                {
                    this._Image_Where = (ImageSource)Application.Current.FindResource("Where.Where");
                }
				if ((this.Type == EnumAion.eType.Pet ||this.Type == EnumAion.eType.NPC || this.Type == EnumAion.eType.PlaceableObject))
				{
                    if ((this.ID_Object == 832855 || this.ID_Object == 701388 || this.ID_Object == 215420) && Thread_Entity.in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value())
                        this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Chest_Green");
					else if ((this.ID_Object == 832856 || this.ID_Object == 701389 || this.ID_Object == 215421) && Thread_Entity.in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value())
                        this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Chest_Blue");
                    else if ((this.ID_Object == 832857 || this.ID_Object == 701390 || this.ID_Object == 215422) && Thread_Entity.in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value())
                        this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Chest_Gold");
					else if (this.Race == EnumAion.eAttitude.Friendly && (this.ID_Object == 700000 || this.ID_Object == 701250 || this.ID_Object == 700079 || this.ID_Object == 701271) && Thread_Entity.in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value())
                        this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Letter");
					else if (this.Action == EnumAion.eAction.Object)
                        this._Image_Object = (ImageSource)Application.Current.FindResource("Entity.NPC_O");
					else if ((this.Race != EnumAion.eAttitude.Passive
					          && this.Race != EnumAion.eAttitude.Friendly && this.Race != EnumAion.eAttitude.NoCombat
					          && this.Type != EnumAion.eType.PlaceableObject
					          && this.Action != EnumAion.eAction.NotObject)
					         || (this.Type == EnumAion.eType.Pet && this.Action == EnumAion.eAction.Attackable))
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.NPC_E");
					else if (this.Race == EnumAion.eAttitude.Passive
					         || this.Race == EnumAion.eAttitude.NoCombat
					         || (this.Type == EnumAion.eType.PlaceableObject && this.Action == EnumAion.eAction.Object))
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.NPC_N");
					else if (this.Race == EnumAion.eAttitude.Friendly)
					{
						if (Thread_Entity.in_Win_Main.in_Setting.in_Radar.IconPlus.Get_Value())
						{
                            if (this.ID_Type_Quest == EnumAion.eTypeQuest.Start_Quest)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_" + this.ID_Type_Quest_Type);
                            else if (this.ID_Type_Quest == EnumAion.eTypeQuest.Inter_Quest)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_" + this.ID_Type_Quest_Type);
                            else if (this.ID_Type_Quest == EnumAion.eTypeQuest.Final_Quest)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_" + this.ID_Type_Quest_Type);
							else if (this.Action == EnumAion.eAction.Vendor)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Merchant");
							else if (this.ID_Type_NPC == (int)EnumAion.eTypeNPC.Negociant)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Negociant");
							else if (this.ID_Type_NPC == (int)EnumAion.eTypeNPC.Entrepo || this.ID_Type_NPC == (int)EnumAion.eTypeNPC.LegionEntrepo)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Chest");
							else if (this.ID_Type_NPC == (int)EnumAion.eTypeNPC.Teleport)
                                this._Image_Object = (ImageSource)Application.Current.FindResource("Special_Icon.Teleport");
							else
                                this._Image = (ImageSource)Application.Current.FindResource("Entity.NPC_A");
						}
						else
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.NPC_A");
					}
				}
				else if (this.Type == EnumAion.eType.User && this.Race == EnumAion.eAttitude.Friendly && (this.Group))
				{
					if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Group_Down");
					else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Group_Up");
					else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Group");
				}
				else if (this.Type == EnumAion.eType.User && this.Race == EnumAion.eAttitude.Friendly && (this.Force))
				{
					if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Force_Down");
					else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Force_Up");
					else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Force");
				}
				else if (this.Type == EnumAion.eType.User && this.Race == EnumAion.eAttitude.Friendly)
				{
					if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ally_Down");
					else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ally_Up");
					else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ally");
				}
				else if (this.Type == EnumAion.eType.User && this.Race != EnumAion.eAttitude.Friendly)
				{
					if (this.Buff == 1)
					{
						if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Down");
						else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Up");
						else
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu");
					}
					else if (this.Is_Attackable == 0)
					{
						if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Down");
						else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Up");
						else
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe");
					}
					else
					{
						if ((this.Z + 10) < Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Down");
						else if ((this.Z - 10) > Thread_Entity.ePlayer.Z)
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Up");
						else
                            this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy");
					}
				}
			}
			else if (this.HP == 0 || this.Stance == EnumAion.eStance.Dead)
			{
				if (this.Type == EnumAion.eType.NPC)
				{
					if (this.Action == EnumAion.eAction.Lootable)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.NPC_Loot");
					else
						this._Image = null;
				}
				else if (this.Type == EnumAion.eType.User && this.Race == EnumAion.eAttitude.Friendly)
				{
					if (this.Group)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Group_Dead");
					else if (this.Force)
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Force_Dead");
					else
                        this._Image = (ImageSource)Application.Current.FindResource("Entity.Ally_Dead");
				}
				else if (this.Type == EnumAion.eType.User &&
				         this.Race != EnumAion.eAttitude.Friendly)
                    this._Image = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Dead");
			}
		}
		
		private void SetZero()
		{
			this.ID					= 0;
			this.ID_Object			= 0;
			this.ID_Type_NPC		= 0;
			this.ID_Type_Quest		= 0;
			this.ID_Type_Quest_Type	= 0;
			this.Rank				= 0;
			this.TargetID			= 0;
			this.Name				= "";
			this.Guild				= "";
			this.Nametolower		= "";
			this.Lvl				= 0;
			this.HP_Percent			= 0;
			this.HP					= 0;
			this.Class				= 0;
			this.Link				= 0;
			this.Type				= 0;
			this.Stance				= 0;
			this.Action				= 0;
			this.DistanceReal		= 0;
			this.Distance			= 0;
			this.Aggro				= false;
			this.Group				= false;
			this.Force				= false;
			this.Is_Attackable		= 0;
            this._Icon              = null;
			this._Image				= null;
			this._Image_Object		= null;
			this.Buff				= 0;
			this.X					= 0;
			this.Y					= 0;
			this.Z					= 0;
			this.Race				= 0;
			this._PtrEntity			= 0;
		}

		private void GetDistance(Player ePlayer)
		{
			double part1 = (this.X - ePlayer.X) * (this.X - ePlayer.X);
			double part2 = (this.Y - ePlayer.Y) * (this.Y - ePlayer.Y);
			double part3 = (this.Z - ePlayer.Z) * (this.Z - ePlayer.Z);

			double result1 = part1 + part2;
			double result2 = part1 + part2 + part3;
			
			this.Distance = (int)Math.Sqrt(result1);
			this.DistanceReal = (int)Math.Sqrt(result2);
		}

		private bool checkAggro(Player ePlayer)
		{
			if (this.TargetID == ePlayer.ID)
				return true;
			else
				return false;
		}

		private bool checkGroup(Player ePlayer)
		{
			if (ePlayer.ID_Group.Contains(this.ID))
				return true;
			return false;
		}

		private bool checkForce(Player ePlayer)
		{
			if (ePlayer.ID_Force.Contains(this.ID))
				return true;
			return false;
		}

		private int checkRank(int rank)
		{
			int result = 0;
			switch(rank)
			{
				case 901215:
				case 901233:
					result = 1;
					break;
				case 901216:
				case 901234:
					result = 2;
					break;
				case 901217:
				case 901235:
					result = 3;
					break;
				case 901218:
				case 901236:
					result = 4;
					break;
				case 901219:
				case 901237:
					result = 5;
					break;
				case 901220:
				case 901238:
					result = 6;
					break;
				case 901221:
				case 901239:
					result = 7;
					break;
				case 901222:
				case 901240:
					result = 8;
					break;
				case 901223:
				case 901241:
					result = 9;
					break;
				case 901224:
				case 901242:
					result = 10;
					break;
				case 901225:
				case 901243:
					result = 11;
					break;
				case 901226:
				case 901244:
					result = 12;
					break;
				case 901227:
				case 901245:
					result = 13;
					break;
				case 901228:
				case 901246:
					result = 14;
					break;
				case 901229:
				case 901247:
					result = 15;
					break;
				case 901230:
				case 901248:
					result = 16;
					break;
				case 901231:
				case 901249:
					result = 17;
					break;
				case 901232:
				case 901250:
					result = 18;
					break;
			}
			if (this.Race != EnumAion.eAttitude.Friendly)
				result += 100;
			return result;
		}

		Icon Check_If_PNG()
		{
			if ((this.HP != 0 || this.Type == EnumAion.eType.Gather) && this.Stance != EnumAion.eStance.Dead)
			{
                foreach (var key in Thread_Entity.in_Win_Main.in_Win_Radar.in_Win_Radar_Setting.icon_collect)
                {
                    if (key.Name != string.Empty
                        && key.IMG_SRC != null
                        && this.Nametolower.Contains(key.Name.ToLower()))
                        return (key);
                }
			}
			return null;
		}

		private void Buff_Fufu()
		{
			if ((this.Class == (int)EnumAion.AionClasses.Assassin || this.Class == (int)EnumAion.AionClasses.Ranger) &&
			    this.Type == EnumAion.eType.User &&
			    this.Race != EnumAion.eAttitude.Friendly)
			{
				for (int i = 0; i < SplMemory.ReadInt(this._PtrEntity + Offset.Status.BuffCount); i++)
				{
					int Id_Buff = SplMemory.ReadInt(SplMemory.ReadLong(SplMemory.ReadLong(_PtrEntity + Offset.Status.BuffArray) + i * 0x4) + 0x4);
					if (Id_Buff == 559 ||
					    Id_Buff == 582 ||
					    Id_Buff == 851 ||
					    Id_Buff == 948 ||
					    Id_Buff == 2735)
					{
						this.Buff = 1;
						return;
					}
				}
			}
			this.Buff = 0;
		}
	}
}