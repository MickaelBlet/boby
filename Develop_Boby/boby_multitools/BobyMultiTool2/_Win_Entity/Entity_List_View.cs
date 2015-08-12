using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Media.Effects;
using System.ComponentModel;
using System.Reflection;
using System.IO;

using MemoryLib;
using NS_Aion_Game;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_Entity
    {
        DispatcherTimer messageTimer;
        Hashtable File_img;
        int date = 0;

        public void Entity_List_View()
        {
            File_img = new Hashtable();

            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\RadarIcon\";

                string[] files = Directory.GetFiles(appPath, "*.png");

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filesreal = files[i];
                        BitmapImage source = new BitmapImage();
                        source.BeginInit();
                        source.UriSource = new Uri(filesreal);
                        source.EndInit();
                        if (source.Width > 16 || source.Height > 16)
                        {
                            source = new BitmapImage();
                            source.BeginInit();
                            source.UriSource = new Uri(filesreal);
                            source.DecodePixelHeight = 16;
                            source.DecodePixelWidth = 16;
                            source.EndInit();
                        }
                        File_img.Add(source.ToString(), source);
                    }
                }
            }
            catch (Exception)
            { }

            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            messageTimer.Start();
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            Entity_List_To_View_();
        }

        private void Entity_List_To_View_()
        {
            try
            {
                if (date != Thread_Entity.date)
                    date = Thread_Entity.date;
                else
                    return;

                int Count_Ally = 0;
                int Count_Ennemy = 0;
                int index = 0;

                IOrderedEnumerable<KeyValuePair<long, Entity>> sortedDict = null;
                if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Dst")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.DistanceReal ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Dst")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.DistanceReal descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Rnk")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Rank ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Rnk")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Rank descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Name")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Name ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Name")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Name descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Guild")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Guild ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Guild")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Guild descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Class")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Class ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Class")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Class descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Lvl")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Lvl ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Lvl")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.Lvl descending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Hp")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.HP_Percent ascending select entry);
                else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Hp")
                    sortedDict = (from entry in in_Win_Main.in_Thread_Entity.DicCopy orderby entry.Value.HP_Percent descending select entry);

                if (this.IsVisible == true)
                {
                    string ttbox = tb_where.Text.ToLower();

                    foreach (var entity in sortedDict)
                    {
                        Entity cust = entity.Value;
                        if (cust.Name != string.Empty)
                        {
                            if (cust.DistanceReal > -1 && cust.DistanceReal < 113 || cust._Image_Where != null)
                            {
                                if (cust.Type == EnumAion.eType.User && cust.Race == EnumAion.eAttitude.Friendly)
                                    Count_Ally++;
                                else if (cust.Type == EnumAion.eType.User && cust.Race != EnumAion.eAttitude.Friendly)
                                    Count_Ennemy++;
                                if (cust.Aggro && cust.Type != EnumAion.eType.Player && cust.HP > 0
                                    || cust.Type == EnumAion.eType.Gather && in_Win_Main.in_Setting.in_Entity.Gather.Get_Value()
                                    || cust.Type == EnumAion.eType.Pet && in_Win_Main.in_Setting.in_Entity.NPC.Get_Value()
                                    || cust.Type == EnumAion.eType.NPC && in_Win_Main.in_Setting.in_Entity.NPC.Get_Value()
                                    || cust.Type == EnumAion.eType.User && cust.Race == EnumAion.eAttitude.Friendly && in_Win_Main.in_Setting.in_Entity.Ally.Get_Value()
                                    || cust.Type == EnumAion.eType.User && cust.Race != EnumAion.eAttitude.Friendly && in_Win_Main.in_Setting.in_Entity.Hostile.Get_Value()
                                    || cust.Type == EnumAion.eType.PlaceableObject && in_Win_Main.in_Setting.in_Entity.NPC.Get_Value()
                                    || cust._Image_Where != null
                                    && cust.Type != EnumAion.eType.Player
                                    && (cust.Type == EnumAion.eType.Gather
                                    || cust.Type == EnumAion.eType.Pet
                                    || cust.Type == EnumAion.eType.NPC
                                    || cust.Type == EnumAion.eType.User
                                    || cust.Type == EnumAion.eType.PlaceableObject))
                                {
                                    EntityCollectionAdd(cust, index);
                                    index++;
                                }
                            }
                        }
                    }

                    for (int i = lv_Entity.Items.Count; i > index; i--)
                        lv_Entity.Items.Remove(lv_Entity.Items[index]);

                    Label_Count_Ally.Content = Count_Ally.ToString();
                    Label_Count_Ennemy.Content = Count_Ennemy.ToString();

                    lv_Entity.Items.Refresh();
                }
            }
            catch
            {
                lv_Entity.Items.Clear();
                lv_Entity.Items.Refresh();
            }
        }

        public void EntityCollectionAdd(Entity entity, int index)
        {
            Entity_View entity_view_tmp = new Entity_View();

            if (entity.Type == EnumAion.eType.Pet || entity.Type == EnumAion.eType.NPC || entity.Type == EnumAion.eType.PlaceableObject)
            {
                entity_view_tmp.Node = entity.PtrEntity.ToString();
                entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.HP = entity.HP_Percent.ToString() + "%";
                entity_view_tmp.Distance = entity.DistanceReal.ToString() + "m";
            }
            else if (entity.Type == EnumAion.eType.User)
            {
                if (entity.Rank == 100 || entity.Rank == 0)
                    return;
                entity_view_tmp.Node = entity.PtrEntity.ToString();
                if (entity.Rank > 100)
                {
                    int irank = entity.Rank - 100;
                    entity_view_tmp.Rank = (EnumAion.eRank)irank + "";
                }
                else
                    entity_view_tmp.Rank = ((EnumAion.eRank)entity.Rank) + "";
                if (entity.Buff == 1)
                    entity_view_tmp.Name = "#" + entity.Name.ToString();
                else
                    entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Guild = entity.Guild.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.Class = ((EnumAion.AionClasses)entity.Class).ToString();
                entity_view_tmp.HP = entity.HP_Percent.ToString() + "%";
                entity_view_tmp.Distance = entity.DistanceReal.ToString() + "m";
            }
            else if (entity.Type == EnumAion.eType.Gather)
            {
                entity_view_tmp.Node = entity.PtrEntity.ToString();
                entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.Distance = entity.DistanceReal.ToString() + "m";
            }

            entity_view_tmp = Entity_View_Grap(entity, entity_view_tmp);
            entity_view_tmp.Entity_Save = entity;

            if (entity._Image_Where != null && tb_where.Text != "" && entity.Nametolower.Contains(tb_where.Text.ToLower()))
            {
                if (entity.DistanceReal < 0)
                    entity_view_tmp.Distance = (-entity.DistanceReal).ToString() + "m";
                lv_Entity.Items.Insert(0, entity_view_tmp);
            }
            else if (entity.Aggro && entity.HP > 0 && entity.Type != EnumAion.eType.Gather)
            {
                lv_Entity.Items.Insert(0, entity_view_tmp);
            }
            else if (lv_Entity.Items.Count < index + 1)
            {
                lv_Entity.Items.Add(entity_view_tmp);
            }
            else
            {
                Entity_View tmp = (Entity_View)lv_Entity.Items[index];
                tmp.Copy(entity_view_tmp);
            }
        }

        public Entity_View Entity_View_Grap(Entity entity, Entity_View entity_view_tmp)
        {
            entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * 100 / 100;
            entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * 100 / 100;
            entity_view_tmp.graph_border = ToBrush("#00000000");
            if (entity.Aggro && entity.HP > 0)
            {
                entity_view_tmp.graph_border = ToBrush("#FFFED700");
            }
            if (entity.Type == EnumAion.eType.Gather)
            {
                if (entity._Icon != null && File_img.Contains(entity._Icon.IMG_PATH))
                {
                    entity_view_tmp.graph_img = (ImageSource)File_img[entity._Icon.IMG_PATH];
                }
                else if (entity._Image_Where != null)
                {
                    entity_view_tmp.graph_img = entity._Image_Where;
                }
                else if (entity._Image_Object != null)
                {
                    entity_view_tmp.graph_img = entity._Image_Object;
                }
                else if (entity._Image != null)
                {
                    entity_view_tmp.graph_img = entity._Image;
                }

                entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                entity_view_tmp.graph_forground = ToBrush("#FFDDDD00");
            }
            else if (entity.Type == EnumAion.eType.Pet || entity.Type == EnumAion.eType.NPC || entity.Type == EnumAion.eType.PlaceableObject)
            {
                entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * entity.HP_Percent / 100;
                entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * entity.HP_Percent / 100;

                if (entity._Icon != null && File_img.Contains(entity._Icon.IMG_PATH))
                {
                    entity_view_tmp.graph_img = (ImageSource)File_img[entity._Icon.IMG_PATH];
                }
                else if (entity._Image_Where != null)
                {
                    entity_view_tmp.graph_img = entity._Image_Where;
                }
                else if (entity._Image_Object != null)
                {
                    entity_view_tmp.graph_img = entity._Image_Object;
                }
                else if (entity._Image != null)
                {
                    entity_view_tmp.graph_img = entity._Image;
                }

                if (entity.Race == EnumAion.eAttitude.Friendly)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                    entity_view_tmp.graph_forground = ToBrush("#FF22FFFF");
                }
                else if (entity.Race == EnumAion.eAttitude.Hostile)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFAAAA");
                }
                else
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                    entity_view_tmp.graph_forground = ToBrush("#FF22FF22");
                }
            }
            else if (entity.Type == EnumAion.eType.User)
            {
                entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * entity.HP_Percent / 100;
                entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * entity.HP_Percent / 100;
                entity_view_tmp.graph_class = (ImageSource)Application.Current.FindResource("Class_Icon." + entity.Class);
                if (entity.Group)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8C006688);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFFFFF");
                }
                else if (entity.Force)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x4C00FFFF);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFFFFF");
                }
                else if (entity.Race == EnumAion.eAttitude.Friendly)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x2500FF41);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFFFFF");
                }
                else if (entity.Is_Attackable == 0)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8CFE0071);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFAAAA");
                }
                else
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8CFE0003);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFAAAA");
                }
            }
            else
            {
                entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                entity_view_tmp.graph_forground = ToBrush("#FFFFFFFF");
            }

            return entity_view_tmp;
        }

        public Color ToColor(uint argb)
        {
            return Color.FromArgb((byte)((argb & 0xff000000) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
        }

        public Brush ToBrush(string argb)
        {
            var bc = new BrushConverter();

            return (Brush)bc.ConvertFrom(argb);
        }
    }

    public class Entity_View
    {
        public string Node { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public string Guild { get; set; }
        public string Lvl { get; set; }
        public string Class { get; set; }
        public string HP { get; set; }
        public string Distance { get; set; }
        public string Race { get; set; }
        public Entity Entity_Save { get; set; }

        public int graph_HP_Meter { get; set; }
        public int graph_HP_Meter_inv { get; set; }

        public ImageSource graph_img { get; set; }
        public ImageSource graph_class { get; set; }

        public Brush graph_border { get; set; }

        public Brush graph_forground { get; set; }
        public SolidColorBrush graph_background { get; set; }

        public Entity_View()
        {
            this.graph_background = new SolidColorBrush();
        }

        public void Copy(Entity_View tmp)
        {
            Node = tmp.Node;
            Rank = tmp.Rank;
            Name = tmp.Name;
            Guild = tmp.Guild;
            Lvl = tmp.Lvl;
            Class = tmp.Class;
            HP = tmp.HP;
            Distance = tmp.Distance;
            Race = tmp.Race;
            Entity_Save = tmp.Entity_Save;
            graph_HP_Meter = tmp.graph_HP_Meter;
            graph_HP_Meter_inv = tmp.graph_HP_Meter_inv;
            graph_img = tmp.graph_img;
            graph_class = tmp.graph_class;
            graph_border = tmp.graph_border;
            graph_forground = tmp.graph_forground;
            graph_background = tmp.graph_background;
        }
    }
}
