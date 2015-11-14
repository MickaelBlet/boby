using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MemoryLib;
using Windows_And_Process;

namespace Aion_Game
{
    public class Entity
    {
        protected long _node;
        protected long _node_location;
        protected long _node_status;

        // Common
        protected long _id;
        protected eType _type;
        protected string _name;
        protected string _info;
        protected string _guild;
        protected byte _lvl;
        protected eClass _class;

        // Stats
        protected byte _hp_percent;
        protected long _hp_max;
        protected long _hp;

        // Target
        protected long _target_id;

        // Info
        protected long _rank;

        // Cheat
        protected long _no_grav;
        protected long _no_skill;
        protected long _atk_speed;
        protected float _move_speed;

        // Vehicle
        protected long _vehicle_node;

        // State
        protected fAttitude _attitude;
        protected fMouseAction _mouse_action;
        protected fAction _action;
        protected byte _is_attackale;
        protected fStance _stance;

        // NPC
        protected long _id_object;
        protected fIdTypeNPC _id_type;
        protected fIdTypeQuest _id_type_quest;
        protected fIdProgressQuest _id_progress_quest;

        // Utility
        protected float _rot;
        protected float _z_collision;
        protected byte _weapon_style;
        protected byte _hide;

        // Location
        protected float _x;
        protected float _y;
        protected float _z;
        protected double _distance_2d;
        protected double _distance_3d;

        // For Entity
        protected bool _aggro;
        protected bool _group;
        protected bool _force;

        public long Node
        {
            get
            {
                return _node;
            }
        }
        public long NodeLocation
        {
            get
            {
                return _node_location;
            }
        }
        public long NodeStatus
        {
            get
            {
                return _node_status;
            }
        }

        // Common
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public eType Type
        {
            get
            {
                return _type;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SplMemory.WriteWchar(_node_status + Offset.Entity.Name, value, 60);
                _name = value;
            }
        }
        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                SplMemory.WriteWchar(_node_status + Offset.Entity.Info, value, 16);
                _info = value;
            }
        }
        public string Guild
        {
            get
            {
                return _guild;
            }
            set
            {
                SplMemory.WriteWchar(_node_status + Offset.Entity.Guild, value, 60);
                _guild = value;
            }
        }
        public byte Lvl
        {
            get
            {
                return _lvl;
            }
        }

        public eClass Class
        {
            get
            {
                return _class;
            }
        }

        public byte HpPercent
        {
            get
            {
                return _hp_percent;
            }
        }
        public long HpMax
        {
            get
            {
                return _hp_max;
            }
        }
        public long Hp
        {
            get
            {
                return _hp;
            }
        }
        public long TargetId
        {
            get
            {
                return _target_id;
            }
        }

        // Info
        public long Rank
        {
            get
            {
                return _rank;
            }
        }

        // Cheat
        public long NoGrav
        {
            get
            {
                long vehicle = VehicleNode;
                if (vehicle != 0)
                    return SplMemory.ReadLong(vehicle + Offset.Entity.No_Grav);
                return SplMemory.ReadLong(_node_status + Offset.Entity.No_Grav);
            }
            set
            {
                if (VehicleNode != 0)
                    SplMemory.WriteMemory(_vehicle_node + Offset.Entity.No_Grav, value);
                else
                    SplMemory.WriteMemory(_node_status + Offset.Entity.No_Grav, value);
                _no_grav = value;
            }
        }
        public long NoSkill
        {
            get
            {
                return _no_skill;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.No_Skill, value);
                _no_skill = value;
            }
        }
        public long AtkSpeed
        {
            get
            {
                return _atk_speed;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.Atk_Speed, value);
                _atk_speed = value;
            }
        }
        public float MoveSpeed
        {
            get
            {
                return _move_speed;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.Mov_Speed, value);
                _move_speed = value;
            }
        }

        // Vehicle
        public long VehicleNode
        {
            get
            {
                return SplMemory.ReadLong(_node_status + Offset.Entity.Vehicle);
            }
        }

        // State
        public fAttitude Attitude
        {
            get
            {
                return _attitude;
            }
        }
        public fMouseAction Mouse_Action
        {
            get
            {
                return _mouse_action;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.Mouse_Action, value.GetHashCode());
                _mouse_action = value;
            }
        }

        public fAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.Action, value.GetHashCode());
                _action = value;
            }
        }
        public byte IsAttackable
        {
            get
            {
                return _is_attackale;
            }
        }
        public fStance Stance
        {
            get
            {
                return _stance;
            }
            set
            {
                SplMemory.WriteMemory(_node_status + Offset.Entity.Stance, value.GetHashCode());
                _stance = value;
            }
        }

        public long IdObject
        {
            get
            {
                return _id_object;
            }
        }
        public fIdTypeNPC IdType
        {
            get
            {
                return _id_type;
            }
        }
        public fIdTypeQuest IdTypeQuest
        {
            get
            {
                return _id_type_quest;
            }
        }
        public fIdProgressQuest IdProgressQuest
        {
            get
            {
                return _id_progress_quest;
            }
        }

        public float Rot
        {
            get
            {
                return _rot;
            }
        }
        public float ZCollision
        {
            get
            {
                long vehicle = VehicleNode;
                if (vehicle != 0)
                    return SplMemory.ReadFloat(vehicle + Offset.Entity.Z_Colision);
                return SplMemory.ReadFloat(_node_status + Offset.Entity.Z_Colision);
            }
        }
        public byte WeaponStyle
        {
            get
            {
                return _weapon_style;
            }
        }

        public byte Hide
        {
            get
            {
                return _hide;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                long node;
                long vehicle = VehicleNode;
                if (vehicle != 0)
                    node = SplMemory.ReadLong(vehicle + Offset.Entity.Node);
                else
                    node = _node;
                long readLocNode = SplMemory.ReadLong(node + Offset.Entity.Position);
                if (readLocNode != 0 && readLocNode != 0xCDCDCDCD)
                {
                    SplMemory.WriteMemory(readLocNode + Offset.Entity.X, value);
                    _x = value;
                }
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                long node;
                long vehicle = VehicleNode;
                if (vehicle != 0)
                    node = SplMemory.ReadLong(vehicle + Offset.Entity.Node);
                else
                    node = _node;
                long readLocNode = SplMemory.ReadLong(node + Offset.Entity.Position);
                if (readLocNode != 0 && readLocNode != 0xCDCDCDCD)
                {
                    SplMemory.WriteMemory(readLocNode + Offset.Entity.Y, value);
                    _y = value;
                }
            }
        }
        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                long node;
                long vehicle = VehicleNode;
                if (vehicle != 0)
                    node = SplMemory.ReadLong(vehicle + Offset.Entity.Node);
                else
                    node = _node;
                long readLocNode = SplMemory.ReadLong(node + Offset.Entity.Position);
                if (readLocNode != 0 && readLocNode != 0xCDCDCDCD)
                {
                    SplMemory.WriteMemory(readLocNode + Offset.Entity.Z, value);
                    _z = value;
                }
            }
        }
        public double Distance2D
        {
            get
            {
                return _distance_2d;
            }
            set
            {
                _distance_2d = value;
            }
        }
        public double Distance3D
        {
            get
            {
                return _distance_3d;
            }
            set
            {
                _distance_3d = value;
            }
        }

        public bool Aggro
        {
            get
            {
                return _aggro;
            }
        }

        public bool Group
        {
            get
            {
                return _group;
            }
        }

        public bool Force
        {
            get
            {
                return _force;
            }
        }

        public Entity(long ptr)
        {
            DefaultValue();
            this._node = ptr;
            //Update();
        }

        public void DefaultValue()
        {
            _node = 0;
            _node_location = 0;
            _node_status = 0;

            // Common
            _id = 0;
            _type = 0;
            _name = string.Empty;
            _info = string.Empty;
            _guild = string.Empty;
            _lvl = 0;
            _class = 0;

            // Stats
            _hp_percent = 0;
            _hp_max = 0;
            _hp = 0;

            // Target
            _target_id = 0;

            // Info
            _rank = 0;

            // Cheat
            _no_grav = 0;
            _no_skill = 0;
            _atk_speed = 0;
            _move_speed = 0;

            // Vehicle
            _vehicle_node = 0;

            // State
            _attitude = (fAttitude)0;
            _mouse_action = (fMouseAction)0;
            _is_attackale = 0;
            _stance = (fStance)0;

            // NPC
            _id_object = 0;
            _id_type = (fIdTypeNPC)0;
            _id_type_quest = (fIdTypeQuest)0;
            _id_progress_quest = (fIdProgressQuest)0;

            // Utility
            _rot = 0;
            _z_collision = 0;
            _weapon_style = 0;

            // Location
            _x = 0;
            _y = 0;
            _z = 0;

            // Distance
            _distance_2d = 1000;
            _distance_3d = 1000;
        }

        public void Update()
        {
            try
            {
                if (_node == 0 || _node == 0xCDCDCDCD)
                    return;
                _node_status = SplMemory.ReadLong(_node + Offset.Entity.Status);
                if (_node_status == 0 || _node_status == 0xCDCDCDCD)
                    return;
                _node_location = SplMemory.ReadLong(_node + Offset.Entity.Position);
                if (_node_location == 0 || _node_location == 0xCDCDCDCD)
                    return;
                _hp_percent = SplMemory.ReadByte(_node_status + Offset.Entity.Hp_Percent);
                _lvl = SplMemory.ReadByte(_node_status + Offset.Entity.Lvl);
                _id = SplMemory.ReadLong(_node_status + Offset.Entity.Id);
                _type = (eType)SplMemory.ReadByte(_node + Offset.Entity.Type);
                _name = SplMemory.ReadWchar(_node_status + Offset.Entity.Name, 60);
                _x = SplMemory.ReadFloat(_node_location + Offset.Entity.X);
                _y = SplMemory.ReadFloat(_node_location + Offset.Entity.Y);
                _z = SplMemory.ReadFloat(_node_location + Offset.Entity.Z);
                if (_hp_percent > 100 || _lvl < 0 || _lvl > 80 || _type == 0u || _name.Length == 0)
                    return;

                // Position / Distance
                _distance_2d = CalculDistance2D();
                _distance_3d = CalculDistance3D();

                _mouse_action = (fMouseAction)SplMemory.ReadInt(_node_status + Offset.Entity.Mouse_Action);
                _id_object = SplMemory.ReadLong(_node_status + Offset.Entity.Type_Object);
                _info = SplMemory.ReadWchar(_node_status + Offset.Entity.Info, 30);

                if (IsNPC() || IsUser() || IsPlayer())
                {
                    _attitude = (fAttitude)SplMemory.ReadInt(_node_status + Offset.Entity.Attitude);
                    _hp = SplMemory.ReadLong(_node_status + Offset.Entity.Hp);
                    _hp_max = SplMemory.ReadLong(_node_status + Offset.Entity.Hp_Max);
                    _target_id = SplMemory.ReadLong(_node_status + Offset.Entity.TargetId);
                    _hide = SplMemory.ReadByte(_node_status + Offset.Entity.Hide);
                    _action = (fAction)SplMemory.ReadByte(_node_status + Offset.Entity.Action);
                    //_hide = SplMemory.ReadByte(_node_status + Offset.Status.Hide);
                }
                if (IsNPC() || IsUser())
                    _aggro = _target_id == Player.Id;
                if (IsUser() && IsFriendly())
                {
                    _group = Player.Group.Contains(Id);
                    _force = Player.Force.Contains(Id);
                }

                if (IsNPC())
                {
                    _id_type = (fIdTypeNPC)SplMemory.ReadInt(_node_status + Offset.Entity.Type_NPC);
                    _id_type_quest = (fIdTypeQuest)SplMemory.ReadInt(_node_status + Offset.Entity.Type_Quest);
                    _id_progress_quest = (fIdProgressQuest)SplMemory.ReadInt(_node_status + Offset.Entity.Type_Quest_Progress);
                    //_info = SplMemory.ReadWchar(_node_status + Offset.Status.Info, 30);
                }
                if (IsUser() || IsPlayer())
                {
                    _class = (eClass)SplMemory.ReadByte(_node_status + Offset.Entity.Class);
                    _stance = (fStance)SplMemory.ReadInt(_node_status + Offset.Entity.Stance);
                    _rank = CalculRank(SplMemory.ReadLong(_node_status + Offset.Entity.Rank));
                    _guild = SplMemory.ReadWchar(_node_status + Offset.Entity.Guild, 60);
                    _is_attackale = SplMemory.ReadByte(_node_status + Offset.Entity.IsAttackable);
                }
                else
                {
                    _guild = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
                    _rank = (long)-Distance3D;
                    _class = (eClass)(100 + (long)-Distance3D);
                }

                if (IsPlayer())
                {
                    Player.entity = this;
                    Player.GroupUpdate();
                    Player.ForceUpdate();
                    _weapon_style = SplMemory.ReadByte(_node_status + Offset.Entity.WeaponStyle);
                    _move_speed = SplMemory.ReadFloat(_node_status + Offset.Entity.Mov_Speed);
                    _atk_speed = SplMemory.ReadLong(_node_status + Offset.Entity.Atk_Speed);
                    _z_collision = SplMemory.ReadFloat(_node_status + Offset.Entity.Z_Colision);
                }

                //Console.WriteLine(_name + " " + _distance_2d + " " + _distance_3d);
            }
            catch { }
        }
        public bool IsPlayer()
        {
            return _type == eType.Player;
        }
        public bool IsUser()
        {
            return _type == eType.User;
        }
        public bool IsNPC()
        {
            return _type == eType.NPC || _type == eType.Pet || _type == eType.PlaceableObject;
        }
        public bool IsFriendly()
        {
            return _attitude == fAttitude.Friendly;
        }
        public bool IsHostile()
        {
            return _attitude != fAttitude.Friendly;
        }

        private double CalculDistance2D()
        {
            float x = Player.X - _x;
            float y = Player.Y - _y;
            return (Math.Sqrt(x * x + y * y));
        }
        private double CalculDistance3D()
        {
            float x = Player.X - _x;
            float y = Player.Y - _y;
            float z = Player.Z - _z;
            return (Math.Sqrt(x * x + y * y + z * z));
        }

        private long CalculRank(long rank)
        {
            long result = 0;
            switch (rank)
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
            if (this.Attitude != fAttitude.Friendly)
                result += 100;
            return result;
        }

        public void Select()
        {
            //Win.SetForegroundWindow(Aion_Process.Game.Whandle);
            //Send_Key.keybd_event(0x10, 0, 0, 0);
            //Send_Key.keybd_event(0x10, 0, 2, 0);

            (new System.Threading.Thread(() =>
            {
                try
                {
                    if (Node == 0 || NodeLocation == 0 || NodeStatus == 0)
                        return;
                    if (!Player.IsPlayer())
                        return;
                    if (Player.TargetId == Id)
                        return;

                    string select_name = "/Select " + _name;

                    float EntityX = X;
                    float EntityY = Y;
                    float EntityZ = Z;

                    int debug = 0;
                    while (debug < 20)
                    {
                        System.Threading.Thread.Sleep(10);
                        X = Player.X;
                        Y = Player.Y;
                        Z = Player.Z;
                        debug++;
                    }

                    Chat.Send(select_name);

                    debug = 0;
                    while (debug < 20)
                    {
                        System.Threading.Thread.Sleep(5);
                        X = Player.X;
                        Y = Player.Y;
                        Z = Player.Z;
                        debug++;
                    }

                    X = EntityX;
                    Y = EntityY;
                    Z = EntityZ;

                    if (Chat.GetValueChatIsOpen())
                        Send_Key.Send(Aion_Process.Game.Whandle, eWindowsVirtualKey.VK_RETURN);
                }
                catch { }
            })).Start();
        }
    }
}
