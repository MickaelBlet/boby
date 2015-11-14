using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Threading;

using MemoryLib;

namespace Aion_Game
{
    public class Player
    {
        public static Entity entity = new Entity(0);
        public static List<long> Group = new List<long>();
        public static List<long> Force = new List<long>();

        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static float x_destination = float.NaN;
        public static float y_destination = float.NaN;
        public static float z_destination = float.NaN;
        public static void IniTimer()
        {
            dispatcherTimer.Tick += new EventHandler(messageTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        public static long Node
        {
            get
            {
                return entity.Node;
            }
        }
        public static long NodeLocation
        {
            get
            {
                return entity.NodeLocation;
            }
        }
        public static long NodeStatus
        {
            get
            {
                return entity.NodeStatus;
            }
        }

        // Common
        public static long Id
        {
            get
            {
                return entity.Id;
            }
        }
        public static eType Type
        {
            get
            {
                return entity.Type;
            }
        }
        public static string Name
        {
            get
            {
                return entity.Name;
            }
            set
            {
                entity.Name = value;
            }
        }
        public static string Info
        {
            get
            {
                return entity.Info;
            }
            set
            {
                entity.Info = value;
            }
        }
        public static string Guild
        {
            get
            {
                return entity.Guild;
            }
            set
            {
                entity.Guild = value;
            }
        }
        public static byte Lvl
        {
            get
            {
                return entity.Lvl;
            }
        }

        public static eClass Class
        {
            get
            {
                return entity.Class;
            }
        }

        public static byte HpPercent
        {
            get
            {
                return entity.HpPercent;
            }
        }
        public static long HpMax
        {
            get
            {
                return entity.HpMax;
            }
        }
        public static long Hp
        {
            get
            {
                return entity.Hp;
            }
        }
        public static long TargetId
        {
            get
            {
                return SplMemory.ReadUIint(entity.NodeStatus + Offset.Entity.TargetId);
            }
        }

        // Info
        public static long Rank
        {
            get
            {
                return entity.Rank;
            }
        }

        // Cheat
        public static long NoGrav
        {
            get
            {
                return entity.NoGrav;
            }
            set
            {
                entity.NoGrav = value;
            }
        }
        public static long NoSkill
        {
            get
            {
                return entity.NoSkill;
            }
            set
            {
                entity.NoSkill = value;
            }
        }
        public static long AtkSpeed
        {
            get
            {
                return SplMemory.ReadLong(Aion_Process.Game.Base + Offset.Player.Atk_Speed);
            }
            set
            {
                entity.AtkSpeed = value;
            }
        }
        public static float MoveSpeed
        {
            get
            {
                return SplMemory.ReadFloat(Aion_Process.Game.Base + Offset.Player.Mov_Speed);
            }
            set
            {
                entity.MoveSpeed = value;
            }
        }

        // Vehicle
        public static long VehicleNode
        {
            get
            {
                return SplMemory.ReadLong(entity.NodeStatus + Offset.Entity.Vehicle);
            }
        }

        // State
        public static fAttitude Attitude
        {
            get
            {
                return entity.Attitude;
            }
        }
        public static fMouseAction Mouse_Action
        {
            get
            {
                return entity.Mouse_Action;
            }
            set
            {
                entity.Mouse_Action = value;
            }
        }
        public static fAction Action
        {
            get
            {
                return entity.Action;
            }
            set
            {
                entity.Action = value;
            }
        }
        public static byte IsAttackable
        {
            get
            {
                return entity.IsAttackable;
            }
        }
        public static fStance Stance
        {
            get
            {
                return entity.Stance;
            }
            set
            {
                entity.Stance = value;
            }
        }

        public static long IdObject
        {
            get
            {
                return entity.IdObject;
            }
        }

        public static float Rot
        {
            get
            {
                return entity.Rot;
            }
        }
        public static float ZCollision
        {
            get
            {
                return entity.ZCollision;
            }
        }
        public static byte WeaponStyle
        {
            get
            {
                return entity.WeaponStyle;
            }
        }

        public static float X
        {
            get
            {
                return entity.X;
            }
            set
            {
                entity.X = value;
            }
        }
        public static float Y
        {
            get
            {
                return entity.Y;
            }
            set
            {
                entity.Y = value;
            }
        }
        public static float Z
        {
            get
            {
                return entity.Z;
            }
            set
            {
                entity.Z = value;
            }
        }

        public static bool IsPlayer()
        {
            return (eType)SplMemory.ReadByte(entity.Node + Offset.Entity.Type) == eType.Player;
        }

        public static void Update()
        {
            entity.Update();
        }

        public static void GroupUpdate()
        {

        }
        public static void ForceUpdate()
        {

        }

        // Player Only

        public static bool IsFly
        {
            get
            {
                return SplMemory.ReadByte(Aion_Process.Game.Base + Offset.Player.IsFly) == 1;
            }
        }

        public static double GetDistance(float x, float y, float z)
        {
            double px = (x - Player.X);
            double py = (y - Player.Y);
            double pz = (z - Player.Z);
            return Math.Sqrt(px * px + py * py + pz * pz);
        }
        public static void MoveTo(Entity entity)
        {
            while (GetDistance(entity.X, entity.Y, entity.Z) > 2)
            {
                float Diff_X = entity.X - Player.X;
                float Diff_Y = entity.Y - Player.Y;
                float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

                if (Diff_X > 0 && Diff_Y > 0)
                    Angle_X = (180 - Angle_X);
                else if (Diff_Y > 0 && Diff_X < 0)
                    Angle_X = (-180 - Angle_X);
                else if (Diff_X < 0 && Diff_Y < 0)
                    Angle_X = (0 - Angle_X);
                else if (Diff_Y < 0 && Diff_X > 0)
                    Angle_X = (0 - Angle_X);
                if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = 360 - Math.Abs(Angle_X);

                SplMemory.WriteMemory(Aion_Process.Game.Base + (long)Offset.Player.CamRot_H, Angle_X);
                SplMemory.WriteMemory(Aion_Process.Game.Base + (long)Offset.Player.IsMove, 4);
            }
        }
        public static void MoveTo(float x, float y, float z)
        {
            while (GetDistance(x, y, z) > 2)
            {
                float Diff_X = x - Player.X;
                float Diff_Y = y - Player.Y;
                float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

                if (Diff_X > 0 && Diff_Y > 0)
                    Angle_X = (180 - Angle_X);
                else if (Diff_Y > 0 && Diff_X < 0)
                    Angle_X = (-180 - Angle_X);
                else if (Diff_X < 0 && Diff_Y < 0)
                    Angle_X = (0 - Angle_X);
                else if (Diff_Y < 0 && Diff_X > 0)
                    Angle_X = (0 - Angle_X);
                if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = 360 - Math.Abs(Angle_X);

                SplMemory.WriteMemory(Aion_Process.Game.Base + (long)Offset.Player.CamRot_H, Angle_X);
                SplMemory.WriteMemory(Aion_Process.Game.Base + (long)Offset.Player.IsMove, 4);
                System.Threading.Thread.Sleep(10);
            }
        }

        public static void MoveTo(BobyMultitools.Travel travel)
        {
            x_destination = travel.X;
            y_destination = travel.Y;
            z_destination = travel.Z;

            if (!dispatcherTimer.IsEnabled)
                dispatcherTimer.Start();

            /*if (GetDistance(travel.X, travel.Y, travel.Z) > 2)
            while (GetDistance(travel.X, travel.Y, travel.Z) > 2)
            {
                float Diff_X = travel.X - Player.X;
                float Diff_Y = travel.Y - Player.Y;
                float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

                if (Diff_X > 0 && Diff_Y > 0)
                    Angle_X = (180 - Angle_X);
                else if (Diff_Y > 0 && Diff_X < 0)
                    Angle_X = (-180 - Angle_X);
                else if (Diff_X < 0 && Diff_Y < 0)
                    Angle_X = (0 - Angle_X);
                else if (Diff_Y < 0 && Diff_X > 0)
                    Angle_X = (0 - Angle_X);
                if (Angle_X < 0 && Angle_X > -180)
                    Angle_X = 360 - Math.Abs(Angle_X);

                if (!float.IsNaN(Angle_X))
                    SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Player.CamRot_H, Angle_X);
                SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Player.IsMove, 4);
                System.Threading.Thread.Sleep(20);
                Player.Update();
            }*/
        }

        public static void messageTimer_Tick(object sender, EventArgs e)
        {
            if (GetDistance(x_destination, y_destination, z_destination) < 2)
            {
                x_destination = float.NaN;
                y_destination = float.NaN;
                z_destination = float.NaN;
                Action = fAction.None;
                dispatcherTimer.Stop();
                return;
            }

            if (Action != fAction.MoveForward)
                Action = fAction.MoveForward;

            float Diff_X = x_destination - Player.X;
            float Diff_Y = y_destination - Player.Y;
            float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

            if (Diff_X > 0 && Diff_Y > 0)
                Angle_X = (180 - Angle_X);
            else if (Diff_Y > 0 && Diff_X < 0)
                Angle_X = (-180 - Angle_X);
            else if (Diff_X < 0 && Diff_Y < 0)
                Angle_X = (0 - Angle_X);
            else if (Diff_Y < 0 && Diff_X > 0)
                Angle_X = (0 - Angle_X);
            if (Angle_X < 0 && Angle_X > -180)
                Angle_X = 360 - Math.Abs(Angle_X);

            if (!float.IsNaN(Angle_X))
                SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Player.CamRot_H, Angle_X);

            Player.Update();
            //SplMemory.WriteMemory(Aion_Process.Game.Base + Offset.Player.IsMove, 4);
        }
    }
}
