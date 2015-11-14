using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using Aion_Process;
using MemoryLib;
using Aion_Game;

namespace BobyMultitools
{
    /// <summary>
    /// Interaction logic for Win_Radar_Popup.xaml
    /// </summary>
    public partial class Win_Entity_Popup : Window
    {
        public Entity entity;
        public long entityPtr = 0;
        public string entityName = "";

        private ImageSource Altitude_Down = null;
        private ImageSource Altitude_Equal = null;
        private ImageSource Altitude_Up = null;

        public Win_Entity_Popup()
        {
            InitializeComponent();

            Altitude_Down = (ImageSource)FindResource("Sign_Popup.Altitude_Down");
            Altitude_Equal = (ImageSource)FindResource("Sign_Popup.Altitude_Equal");
            Altitude_Up = (ImageSource)FindResource("Sign_Popup.Altitude_Up");
        }

        public void PopupContent(Entity entityparam)
        {
            entity = entityparam;
            BrushConverter bc = new BrushConverter();

            if (entity.Type == Aion_Game.eType.Gather)
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FFDDDD00");
            else if ((entity.Type == Aion_Game.eType.Pet || entity.Type == Aion_Game.eType.NPC || entity.Type == Aion_Game.eType.PlaceableObject) && entity.Attitude == Aion_Game.fAttitude.Friendly)
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FF22FFFF");
            else if ((entity.Type == Aion_Game.eType.Pet || entity.Type == Aion_Game.eType.NPC || entity.Type == Aion_Game.eType.PlaceableObject) && entity.Attitude == Aion_Game.fAttitude.Hostile)
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FFFFAAAA");
            else if ((entity.Type == Aion_Game.eType.Pet || entity.Type == Aion_Game.eType.NPC || entity.Type == Aion_Game.eType.PlaceableObject) && (entity.Attitude == Aion_Game.fAttitude.Passive || entity.Attitude == Aion_Game.fAttitude.NoCombat))
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FF22FF22");
            else if ((entity.Type == Aion_Game.eType.Pet || entity.Type == Aion_Game.eType.NPC || entity.Type == Aion_Game.eType.PlaceableObject))
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FF22FF22");
            else if (entity.Type == Aion_Game.eType.User && entity.Attitude == Aion_Game.fAttitude.Friendly)
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FFFFFFFF");
            else if (entity.Type == Aion_Game.eType.User && entity.Attitude != Aion_Game.fAttitude.Friendly)
                this.tbName.Foreground = (Brush)bc.ConvertFrom("#FFFFAAAA");
            if ((entity.Z + 10) < Aion_Game.Player.Z)
                this.Altitude.Source = Altitude_Down;
            else if ((entity.Z - 10) > Aion_Game.Player.Z)
                this.Altitude.Source = Altitude_Up;
            else
                this.Altitude.Source = Altitude_Equal;
            if (entity.Type == Aion_Game.eType.User)
            {
                if (entity.Guild.Length > 0)
                {
                    this.tbGuild.Text = "<" + entity.Guild + ">";
                    this.tbGuild.Height = Double.NaN;
                }
                else
                    this.tbGuild.Height = 0;
                this.tbLvl.Text = "Lvl " + entity.Lvl + "	" + (Aion_Game.eClass)entity.Class;
                this.tbDistance.Text = Get_Distance_Real(entity) + "m";
            }
            else
            {
                this.tbGuild.Height = 0;
                this.tbLvl.Text = "Lvl " + entity.Lvl;
                this.tbDistance.Text = Get_Distance_Real(entity) + "m";
            }
            this.tbName.Text = entity.Name;
            if (entity.Type == Aion_Game.eType.Gather)
            {
                this.tbName.Text = entity.Name + " (" + entity.Lvl + "p)";
                this.prgHP.Height = 0;
                this.tbHP.Height = 0;
                this.tbHPbg.Height = 0;
                this.tbHP.Text = "";
                this.tbHPbg.Text = "";
                this.tbLvl.Height = 0;
            }
            else
            {
                this.prgHP.Height = 7;
                this.tbHP.Height = 9;
                this.tbHPbg.Height = 9;
                if (entity.Hp != entity.HpPercent)
                {
                    this.tbHP.Text = String.Format("{0:### ### ### ###}", entity.Hp).Trim();
                    this.tbHPbg.Text = String.Format("{0:### ### ### ###}", entity.Hp).Trim();
                }
                else
                {
                    this.tbHP.Text = "";
                    this.tbHPbg.Text = "";
                }
                this.tbLvl.Height = Double.NaN;
                this.prgHP.Value = entity.HpPercent;
            }
            this.entityPtr = entity.Node;
            this.entityName = entity.Name;
            /*if (entity.Aggro)
                this.border_bg.BorderBrush = Brushes.Yellow;
            else*/
            this.border_bg.BorderBrush = Brushes.Gray;
        }

        private int Get_Distance_Real(Entity entity)
        {
            float PlayerX = SplMemory.ReadFloat(Game.Base + Offset.Player.X);
            float PlayerY = SplMemory.ReadFloat(Game.Base + Offset.Player.Y);
            float PlayerZ = SplMemory.ReadFloat(Game.Base + Offset.Player.Z);

            double part1 = (entity.X - PlayerX) * (entity.X - PlayerX);
            double part2 = (entity.Y - PlayerY) * (entity.Y - PlayerY);
            double part3 = (entity.Z - PlayerZ) * (entity.Z - PlayerZ);

            double result = part1 + part2 + part3;

            return((int)Math.Sqrt(result));
        }
    }
}