using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using MemoryLib;

namespace Aion_Game
{
    public class Player
    {
        public static Entity entity = new Entity(0);
        public static List<long> Group = new List<long>();
        public static List<long> Force = new List<long>();

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
                return SplMemory.ReadUIint(entity.NodeStatus + Offset.Status.TargetId);
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
                return entity.AtkSpeed;
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
                return entity.MoveSpeed;
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
                return entity.VehicleNode;
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
    }
}
