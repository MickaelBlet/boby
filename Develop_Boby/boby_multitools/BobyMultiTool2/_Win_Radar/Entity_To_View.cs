using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using MemoryLib;
using Aion_Process;
using Aion_Game;

namespace Aion_Game
{
    class Entity_To_View
    {
        private static Dictionary<long, View_Entity> sView = new Dictionary<long, View_Entity>();

        public static Dictionary<long, View_Entity> View = new Dictionary<long, View_Entity>(sView);

        public static DispatcherTimer messageTimer;

        public static bool Icon_Plus = false;
        public static string Where_In_Game = string.Empty;
        public static string Where_In_Entity = string.Empty;

        public static bool Radar_NPC = false;
        public static bool Radar_Ally = false;
        public static bool Radar_Enemy = false;
        public static bool Radar_Gather = false;

        public static bool Entity_NPC = false;
        public static bool Entity_Ally = false;
        public static bool Entity_Enemy = false;
        public static bool Entity_Gather = false;

        public static void Start()
        {
            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            messageTimer.Start();
        }

        public static void messageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ScanEntity();
            }
            catch { }
        }

        public static void ScanEntity()
        {
            if (Game.Pid == 0 || Game.Base == 0)
                return;

            Icon_Plus = BobyMultitools.Setting.in_Setting.in_Radar.IconPlus.Get_Value();
            Where_In_Entity = View_Entity.RemoveDiacritics(BobyMultitools.Setting.in_Setting.in_Entity.Where.ToLower());
            Where_In_Game = View_Entity.RemoveDiacritics(SplMemory.ReadWchar(Game.Base + Offset.Game.Where, 60).ToLower());

            Radar_NPC = BobyMultitools.Setting.in_Setting.in_Radar.NPC.Get_Value();
            Radar_Ally = BobyMultitools.Setting.in_Setting.in_Radar.Ally.Get_Value();
            Radar_Enemy = BobyMultitools.Setting.in_Setting.in_Radar.Hostile.Get_Value();
            Radar_Gather = BobyMultitools.Setting.in_Setting.in_Radar.Gather.Get_Value();

            Entity_NPC = BobyMultitools.Setting.in_Setting.in_Entity.NPC.Get_Value();
            Entity_Ally = BobyMultitools.Setting.in_Setting.in_Entity.Ally.Get_Value();
            Entity_Enemy = BobyMultitools.Setting.in_Setting.in_Entity.Hostile.Get_Value();
            Entity_Gather = BobyMultitools.Setting.in_Setting.in_Entity.Gather.Get_Value();

            sView = new Dictionary<long, View_Entity>();
            foreach (var entity in Aion_Game.EntityList.uList.Values)
                sView[entity.Node] = new View_Entity(entity);
            View = new Dictionary<long, View_Entity>(sView);
        }
    }

    class Resources_Entity
    {
        public static class Where
        {
            public static ImageSource where = (ImageSource)Application.Current.FindResource("Where.Where");
            public static ImageSource where_yellow = (ImageSource)Application.Current.FindResource("Where.Where_Yellow");
        }

        public static class Entity
        {
            public static ImageSource Ether = (ImageSource)Application.Current.FindResource("Entity.Ether");
            public static ImageSource Gather = (ImageSource)Application.Current.FindResource("Entity.Gather");
            public static ImageSource Ether_Not = (ImageSource)Application.Current.FindResource("Entity.Ether_Not");
            public static ImageSource Gather_Not = (ImageSource)Application.Current.FindResource("Entity.Gather_Not");

            public static ImageSource NPC_Object = (ImageSource)Application.Current.FindResource("Entity.NPC_O");
            public static ImageSource NPC_Neutral = (ImageSource)Application.Current.FindResource("Entity.NPC_N");
            public static ImageSource NPC_Enemy = (ImageSource)Application.Current.FindResource("Entity.NPC_E");
            public static ImageSource NPC_Ally = (ImageSource)Application.Current.FindResource("Entity.NPC_A");
            public static ImageSource NPC_Loot = (ImageSource)Application.Current.FindResource("Entity.NPC_Loot");
            public static ImageSource NPC_None = (ImageSource)Application.Current.FindResource("Entity.NPC_NO");

            public static ImageSource Ally = (ImageSource)Application.Current.FindResource("Entity.Ally");
            public static ImageSource Ally_Up = (ImageSource)Application.Current.FindResource("Entity.Ally_Up");
            public static ImageSource Ally_Down = (ImageSource)Application.Current.FindResource("Entity.Ally_Down");
            public static ImageSource Ally_Dead = (ImageSource)Application.Current.FindResource("Entity.Ally_Dead");
            public static ImageSource Group = (ImageSource)Application.Current.FindResource("Entity.Group");
            public static ImageSource Group_Up = (ImageSource)Application.Current.FindResource("Entity.Group_Up");
            public static ImageSource Group_Down = (ImageSource)Application.Current.FindResource("Entity.Group_Down");
            public static ImageSource Group_Dead = (ImageSource)Application.Current.FindResource("Entity.Group_Dead");
            public static ImageSource Force = (ImageSource)Application.Current.FindResource("Entity.Force");
            public static ImageSource Force_Up = (ImageSource)Application.Current.FindResource("Entity.Force_Up");
            public static ImageSource Force_Down = (ImageSource)Application.Current.FindResource("Entity.Force_Down");
            public static ImageSource Force_Dead = (ImageSource)Application.Current.FindResource("Entity.Force_Dead");
            public static ImageSource Ennemy = (ImageSource)Application.Current.FindResource("Entity.Ennemy");
            public static ImageSource Ennemy_Up = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Up");
            public static ImageSource Ennemy_Down = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Down");
            public static ImageSource Ennemy_Dead = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Dead");
            public static ImageSource Ennemy_Fufu = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu");
            public static ImageSource Ennemy_Fufu_Up = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Up");
            public static ImageSource Ennemy_Fufu_Down = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Down");
            public static ImageSource Ennemy_Transfo = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Transfo");
            public static ImageSource Ennemy_Transfo_Up = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Transfo_Up");
            public static ImageSource Ennemy_Transfo_Down = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Transfo_Down");
            public static ImageSource Ennemy_Safe = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe");
            public static ImageSource Ennemy_Safe_Up = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Up");
            public static ImageSource Ennemy_Safe_Down = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Down");
        }

        public static class Special_Icon
        {
            public static ImageSource Negociant = (ImageSource)Application.Current.FindResource("Special_Icon.Negociant");
            public static ImageSource Merchant = (ImageSource)Application.Current.FindResource("Special_Icon.Merchant");
            public static ImageSource Teleport = (ImageSource)Application.Current.FindResource("Special_Icon.Teleport");
            public static ImageSource Chest = (ImageSource)Application.Current.FindResource("Special_Icon.Chest");
            public static ImageSource Letter = (ImageSource)Application.Current.FindResource("Special_Icon.Letter");
            public static ImageSource Stigma = (ImageSource)Application.Current.FindResource("Special_Icon.Stigma");
            public static ImageSource Coins = (ImageSource)Application.Current.FindResource("Special_Icon.Coins");
            public static ImageSource Table = (ImageSource)Application.Current.FindResource("Special_Icon.Table");
            public static ImageSource House = (ImageSource)Application.Current.FindResource("Special_Icon.House");

            public static ImageSource Quest_Start_0 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_0");
            public static ImageSource Quest_Inter_0 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_0");
            public static ImageSource Quest_Final_0 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_0");
            public static ImageSource Quest_Start_1 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_1");
            public static ImageSource Quest_Inter_1 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_1");
            public static ImageSource Quest_Final_1 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_1");
            public static ImageSource Quest_Start_2 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_2");
            public static ImageSource Quest_Inter_2 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_2");
            public static ImageSource Quest_Final_2 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_2");
            public static ImageSource Quest_Start_3 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_3");
            public static ImageSource Quest_Inter_3 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_3");
            public static ImageSource Quest_Final_3 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_3");
            public static ImageSource Quest_Start_4 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Start_4");
            public static ImageSource Quest_Inter_4 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Inter_4");
            public static ImageSource Quest_Final_4 = (ImageSource)Application.Current.FindResource("Special_Icon.Quest_Final_4");
        }
    }

    public class View_Entity
    {
        // image
        public ImageSource img = null;
        public int radar_img_index = 0;

        // entity
        public Entity entity = null;

        // other info
        public string Name_To_Lower = string.Empty;
        public long Distance = 0;
        public long Rank = 0;
        public long Lvl = 0;
        public long Class = 0;
        public long Hp = 0;

        public bool in_radar = false;
        public bool in_entity = false;

        public View_Entity(Entity entity)
        {
            Name_To_Lower = RemoveDiacritics(entity.Name.ToLower());
            Distance = (long)entity.Distance3D;
            Rank = (long)entity.Rank;
            Lvl = (long)entity.Lvl;
            Class = (long)entity.Class;
            Hp = (long)entity.Hp;
            this.entity = entity;

            if (Distance > 112)
                return;

            if (Entity_To_View.Where_In_Entity != "" && Name_To_Lower.Contains(Entity_To_View.Where_In_Entity))
            {
                this.img = Resources_Entity.Where.where_yellow;
                this.radar_img_index = 8;
                this.Distance = -Distance;
                this.Rank = -Rank;
                this.Lvl = -Lvl;
                this.Class = -Class;
                this.Hp = -Hp;
                this.in_radar = true;
                this.in_entity = true;
                return;
            }
            if (Entity_To_View.Where_In_Game != "" && Name_To_Lower.Contains(Entity_To_View.Where_In_Game))
            {
                this.img = Resources_Entity.Where.where;
                this.radar_img_index = 8;
                this.in_radar = true;
                this.in_entity = true;
                return;
            }
            if (entity.Type == eType.Gather)
            {
                if (entity.Action == fAction.Gatherable || entity.Action == fAction.Low_Gatherable)
                {
                    if (entity.IdObject >= 401001 && entity.IdObject <= 401077)
                        this.img = Resources_Entity.Entity.Ether;
                    else
                        this.img = Resources_Entity.Entity.Gather;
                }
                else if (entity.Action == fAction.NotGatherable || entity.Action == fAction.Low_NotGatherable)
                {
                    if (entity.IdObject >= 401001 && entity.IdObject <= 401077)
                        this.img = Resources_Entity.Entity.Ether_Not;
                    else
                        this.img = Resources_Entity.Entity.Gather_Not;
                }
                if (this.img != null)
                {
                    this.radar_img_index = 0;
                    this.in_radar = Entity_To_View.Radar_Gather;
                    this.in_entity = Entity_To_View.Entity_Gather;
                }
                return;
            }
            if (entity.Type == eType.Pet || entity.Type == eType.NPC || entity.Type == eType.PlaceableObject)
            {
                if (entity.HpPercent != 0 && entity.Stance != fStance.Dead)
                {
                    if (entity.Attitude == fAttitude.Friendly)
                    {
                        if (entity.IdObject == 700000
                            || entity.IdObject == 700079
                            || entity.IdObject == 701250
                            || entity.IdObject == 701271
                            || entity.IdObject == 701458
                            || entity.IdObject == 701459)
                            this.img = Resources_Entity.Special_Icon.Letter;
                        else if (entity.IdType == fIdTypeNPC.Middleman)
                            this.img = Resources_Entity.Special_Icon.Negociant;
                        else if (entity.IdType == fIdTypeNPC.Warehouse || entity.IdType == fIdTypeNPC.LegionWarehouse)
                            this.img = Resources_Entity.Special_Icon.Chest;
                        else if (entity.IdType == fIdTypeNPC.Teleport)
                            this.img = Resources_Entity.Special_Icon.Teleport;
                        else if (entity.IdType == fIdTypeNPC.StigmaMaster)
                            this.img = Resources_Entity.Special_Icon.Stigma;
                        else if (entity.IdType >= fIdTypeNPC.CoinVendorStart && entity.IdType <= fIdTypeNPC.CoinVendorEnd)
                            this.img = Resources_Entity.Special_Icon.Coins;
                        else if (entity.Action == fAction.Vendor)
                            this.img = Resources_Entity.Special_Icon.Merchant;
                        if (entity.IdProgressQuest != (fIdProgressQuest)0)
                        {
                            if (entity.IdTypeQuest == fIdTypeQuest.Red_Quest)
                            {
                                if (entity.IdProgressQuest == fIdProgressQuest.Start_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Start_4;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Inter_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Inter_4;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Final_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Final_4;
                            }
                            else if (entity.IdTypeQuest == fIdTypeQuest.Mission_Quest)
                            {
                                if (entity.IdProgressQuest == fIdProgressQuest.Start_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Start_3;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Inter_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Inter_3;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Final_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Final_3;
                            }
                            else if (entity.IdTypeQuest == fIdTypeQuest.Purple_Quest)
                            {
                                if (entity.IdProgressQuest == fIdProgressQuest.Start_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Start_2;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Inter_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Inter_2;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Final_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Final_2;
                            }
                            else if (entity.IdTypeQuest == fIdTypeQuest.Blue_Quest)
                            {
                                if (entity.IdProgressQuest == fIdProgressQuest.Start_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Start_1;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Inter_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Inter_1;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Final_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Final_1;
                            }
                            else if (entity.IdTypeQuest == fIdTypeQuest.Normal_Quest)
                            {
                                if (entity.IdProgressQuest == fIdProgressQuest.Start_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Start_0;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Inter_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Inter_0;
                                else if (entity.IdProgressQuest == fIdProgressQuest.Final_Quest)
                                    this.img = Resources_Entity.Special_Icon.Quest_Final_0;
                            }
                        }
                        if (this.img != null && Entity_To_View.Icon_Plus)
                        {
                            this.radar_img_index = 7;
                        }
                        else
                        {
                            this.img = Resources_Entity.Entity.NPC_Ally;
                            this.radar_img_index = 1;
                        }
                    }
                    else if (entity.Action == fAction.Object)
                    {
                        this.img = Resources_Entity.Entity.NPC_Object;
                        this.radar_img_index = 3;
                    }
                    else if ((entity.Attitude != fAttitude.Passive
                            && entity.Attitude != fAttitude.Friendly
                            && entity.Attitude != fAttitude.NoCombat
                            && entity.Type != eType.PlaceableObject
                            && entity.Action != fAction.NotObject)
                        || (entity.Type == eType.Pet && entity.Action == fAction.Attackable))
                    {
                        this.img = Resources_Entity.Entity.NPC_Enemy;
                        this.radar_img_index = 2;
                    }
                    else if ((entity.Attitude == fAttitude.Passive
                            || entity.Attitude == fAttitude.NoCombat)
                        || (entity.Type == eType.PlaceableObject
                            && entity.Action == fAction.Object))
                    {
                        this.img = Resources_Entity.Entity.NPC_Neutral;
                        this.radar_img_index = 1;
                    }
                    if (this.img == null)
                    {
                        this.img = Resources_Entity.Entity.NPC_Neutral;
                        this.radar_img_index = 1;
                    }
                    this.in_radar = Entity_To_View.Radar_NPC;
                    this.in_entity = Entity_To_View.Entity_NPC;
                }
                else // !if (this.Hp != 0 && entity.Stance != fStance.Dead)
                {
                    if (entity.Action == fAction.Lootable)
                        this.img = Resources_Entity.Entity.NPC_Loot;
                    else
                        this.img = null;
                    this.radar_img_index = 3;
                    this.in_radar = Entity_To_View.Radar_NPC;
                    this.in_entity = Entity_To_View.Entity_NPC;
                }
            }
            else if (entity.Type == eType.User && entity.Attitude == fAttitude.Friendly && entity.Group)
            {
                if (entity.HpPercent != 0 && entity.Stance != fStance.Dead)
                {
                    if ((entity.Z + 10) < Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Group_Down");
                    else if ((entity.Z - 10) > Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Group_Up");
                    else
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Group");
                }
                else
                    this.img = (ImageSource)Application.Current.FindResource("Entity.Group_Dead");
                this.radar_img_index = 8;
                this.in_radar = true;
                this.in_entity = true;
            }
            else if (entity.Type == eType.User && entity.Attitude == fAttitude.Friendly && entity.Force)
            {
                if (entity.HpPercent != 0 && entity.Stance != fStance.Dead)
                {
                    if ((entity.Z + 10) < Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Force_Down");
                    else if ((entity.Z - 10) > Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Force_Up");
                    else
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Force");
                }
                else
                    this.img = (ImageSource)Application.Current.FindResource("Entity.Force_Dead");
                this.radar_img_index = 8;
                this.in_radar = true;
                this.in_entity = true;
            }
            else if (entity.Type == eType.User && entity.Attitude == fAttitude.Friendly)
            {
                if (entity.HpPercent != 0 && entity.Stance != fStance.Dead)
                {
                    if ((entity.Z + 10) < Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Ally_Down");
                    else if ((entity.Z - 10) > Player.Z)
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Ally_Up");
                    else
                        this.img = (ImageSource)Application.Current.FindResource("Entity.Ally");
                }
                else
                    this.img = (ImageSource)Application.Current.FindResource("Entity.Ally_Dead");
                this.radar_img_index = 5;
                this.in_radar = Entity_To_View.Radar_Ally;
                this.in_entity = Entity_To_View.Entity_Ally;
            }
            else if (entity.Type == eType.User && entity.Attitude != fAttitude.Friendly)
            {
                if (entity.HpPercent != 0 && entity.Stance != fStance.Dead)
                {
                    /*if (entity.Hide == 1)
                    {
                        if ((entity.Z + 10) < Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Down");
                        else if ((entity.Z - 10) > Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu_Up");
                        else
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Fufu");
                    }
                    else */
                    if (entity.IsAttackable == 0)
                    {
                        if ((entity.Z + 10) < Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Down");
                        else if ((entity.Z - 10) > Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe_Up");
                        else
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Safe");
                    }
                    else
                    {
                        if ((entity.Z + 10) < Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Down");
                        else if ((entity.Z - 10) > Player.Z)
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Up");
                        else
                            this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy");
                    }
                }
                else
                    this.img = (ImageSource)Application.Current.FindResource("Entity.Ennemy_Dead");
                this.radar_img_index = 6;
                this.in_radar = Entity_To_View.Radar_Enemy;
                this.in_entity = Entity_To_View.Entity_Enemy;
            }
        }

        public static string RemoveDiacritics(string name)
        {
            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(name);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }
    }
}
