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
using System.Globalization;

using MemoryLib;
using Aion_Game;

namespace BobyMultitools
{
    public partial class Win_Entity
    {
        DispatcherTimer messageTimer;
        CroppedBitmap[] FileImg;
        int date = 0;

        public void Entity_List_View()
        {
            ImageSource GraphicChar = (ImageSource)Application.Current.FindResource("GraphicChar");

            int maxImage = 234;
            FileImg = new CroppedBitmap[maxImage];

            for (int y = 0; y < 17; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    if (y * 14 + x >= maxImage)
                        break;
                    CroppedBitmap img = new CroppedBitmap((BitmapSource)GraphicChar, new Int32Rect(x * 16 + x + 1, y * 16 + y + 1, 16, 16));
                    FileImg[y * 14 + x] = img;
                }
            }

            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
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
                int Count_Ally = 0;
                int Count_Ennemy = 0;
                int index = 0;

                IOrderedEnumerable<KeyValuePair<long, View_Entity>> sortedDict = null;
                if (Setting.Boby.Entity.Order == "Dst")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Distance ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Dst")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Distance descending select entry;
                else if (Setting.Boby.Entity.Order == "/Rnk")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Rank ascending select entry;
                else if (Setting.Boby.Entity.Order == "Rnk")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Rank descending select entry;
                else if (Setting.Boby.Entity.Order == "Name")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Name_To_Lower ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Name")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Name_To_Lower descending select entry;
                else if (Setting.Boby.Entity.Order == "Guild")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.Guild ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Guild")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.Guild descending select entry;
                else if (Setting.Boby.Entity.Order == "Class")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Class ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Class")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.Class descending select entry;
                else if (Setting.Boby.Entity.Order == "Lvl")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.Lvl ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Lvl")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.Lvl descending select entry;
                else if (Setting.Boby.Entity.Order == "Hp")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.HpPercent ascending select entry;
                else if (Setting.Boby.Entity.Order == "/Hp")
                    sortedDict = from entry in Entity_To_View.View orderby entry.Value.entity.HpPercent descending select entry;

                if (this.IsVisible == true)
                {
                    string ttbox = tb_where.Text.ToLower();

                    foreach (var entry in sortedDict)
                    {
                        Entity entity = entry.Value.entity;
                        if (entity.Name != string.Empty && entity.Distance3D > -1 && entity.Distance3D < 113)
                        {
                            if (entity.Type == eType.User)
                            {
                                if (entity.Attitude == fAttitude.Friendly)
                                    Count_Ally++;
                                else
                                    Count_Ennemy++;
                            }
                            if ((entity.Aggro && entity.Type != eType.Player && entity.HpPercent > 0)
                                || (entry.Value.in_entity && entity.Type != eType.Player))
                            {
                                EntityCollectionAdd(entry.Value, index);
                                index++;
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
            catch (Exception ex)
            {
                lv_Entity.Items.Clear();
                lv_Entity.Items.Refresh();
                Console.WriteLine(ex.ToString());
            }
        }

        public void EntityCollectionAdd(View_Entity entry, int index)
        {
            Entity entity = entry.entity;
            Entity_View entity_view_tmp = new Entity_View();

            if (entity.Type == eType.Pet || entity.Type == eType.NPC || entity.Type == eType.PlaceableObject)
            {
                entity_view_tmp.Node = entity.Node.ToString();
                entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.HP = entity.HpPercent.ToString() + "%";
                entity_view_tmp.Distance = ((int)entity.Distance2D).ToString() + "m";
            }
            else if (entity.Type == eType.User)
            {
                if (entity.Rank == 100 || entity.Rank == 0)
                    return;
                entity_view_tmp.Node = entity.Node.ToString();
                if (entity.Rank > 100)
                {
                    long irank = entity.Rank - 100;
                    entity_view_tmp.Rank = (eRank)irank + "";
                }
                else
                    entity_view_tmp.Rank = (eRank)entity.Rank + "";
                //if (entity.Buff == 1)
                //    entity_view_tmp.Name = "#" + entity.Name.ToString();
                //else
                entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Guild = entity.Guild.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.Class = ((eClass)entity.Class).ToString();
                entity_view_tmp.HP = entity.HpPercent.ToString() + "%";
                entity_view_tmp.Distance = ((int)entity.Distance2D).ToString() + "m";
            }
            else if (entity.Type == eType.Gather)
            {
                entity_view_tmp.Node = entity.Node.ToString();
                entity_view_tmp.Name = entity.Name.ToString();
                entity_view_tmp.Lvl = entity.Lvl.ToString();
                entity_view_tmp.Distance = ((int)entity.Distance2D).ToString() + "m";
            }
            else
            {
                return;
            }

            entity_view_tmp = Entity_View_Graph(entry, entity_view_tmp);
            entity_view_tmp.Entity_Save = entity;

            if (entry.radar_img_index == 8)
            {
                if (entry.Distance < 0)
                    entity_view_tmp.Distance = (-entry.Distance).ToString() + "m";
                lv_Entity.Items.Insert(0, entity_view_tmp);
            }
            else if (entity.Aggro && entity.Hp > 0 && entity.Type != eType.Gather)
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

        public Entity_View Entity_View_Graph(View_Entity entry, Entity_View entity_view_tmp)
        {
            Entity entity = entry.entity;
            entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * 100 / 100;
            entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * 100 / 100;
            entity_view_tmp.graph_border = ToBrush("#00000000");
            if (entity.Aggro && entity.Hp > 0)
            {
                entity_view_tmp.graph_border = ToBrush("#FFFED700");
            }
            if (entity.Type == eType.Gather)
            {
                if (entry.img != null)
                    entity_view_tmp.graph_img = entry.img;

                entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                entity_view_tmp.graph_forground = ToBrush("#FFDDDD00");
            }
            else if (entity.Type != eType.User && entity.Type != eType.Player)
            {
                entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * entity.HpPercent / 100;
                entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * entity.HpPercent / 100;

                /*if (entity._Icon != null && File_img.Contains(entity._Icon.IMG_PATH))
                {
                    entity_view_tmp.graph_img = (ImageSource)File_img[entity._Icon.IMG_PATH];
                }*/
                if (entry.icon != null)
                    entity_view_tmp.graph_img = FileImg[entry.icon.ID];
                else if (entry.img != null)
                    entity_view_tmp.graph_img = entry.img;

                if (entity.Attitude == fAttitude.Friendly)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x8F858585);
                    entity_view_tmp.graph_forground = ToBrush("#FF22FFFF");
                }
                else if (entity.Attitude == fAttitude.Hostile)
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
            else if (entity.Type == eType.User)
            {
                entity_view_tmp.graph_HP_Meter_inv = ((int)lv_Entity.Width - 10) - ((int)lv_Entity.Width - 10) * entity.HpPercent / 100;
                entity_view_tmp.graph_HP_Meter = ((int)lv_Entity.Width - 10) * entity.HpPercent / 100;
                entity_view_tmp.graph_class = (ImageSource)Application.Current.FindResource("Class_Icon." + (int)entity.Class);
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
                else if (entity.Attitude == fAttitude.Friendly)
                {
                    entity_view_tmp.graph_background.Color = ToColor(0x2500FF41);
                    entity_view_tmp.graph_forground = ToBrush("#FFFFFFFF");
                }
                else if (entity.IsAttackable == 0)
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
