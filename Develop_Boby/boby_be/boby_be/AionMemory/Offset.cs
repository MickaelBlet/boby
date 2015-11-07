using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;

namespace MemoryLib
{
    /// <summary>
    /// Description of Offset.
    /// </summary>
    public class Offset
    {
        public Offset()
        {
        }

        public static void GetWindowsOffset(int Game_Base, long Base)
        {
            Base_windows.newbase = new Hashtable();
            long i = Game_Base + Base;
            while (SplMemory.ReadLong(i) != 0
                   || SplMemory.ReadLong(i + 0x4) != 0
                   || SplMemory.ReadLong(i + 0x8) != 0
                   || SplMemory.ReadLong(i + 0xC) != 0
                   || SplMemory.ReadLong(i + 0x10) != 0
                   || SplMemory.ReadLong(i + 0x14) != 0
                   || SplMemory.ReadLong(i + 0x18) != 0)
            {
                if (SplMemory.ReadLong(i) != 0)
                {
                    var name = SplMemory.ReadChar(SplMemory.ReadLong(i) + (long)Offset.WinDlg.Name, 30);
                    if (!Regex.IsMatch(name, @"^[a-z0-9_]+$"))
                    {
                        var nameAddress = SplMemory.ReadLong(SplMemory.ReadLong(i) + (long)Offset.WinDlg.Name);
                        name = SplMemory.ReadChar(nameAddress, 30);
                    }
                    try
                    {
                        Base_windows.newbase.Add(name, i);
                    }
                    catch { }
                }
                i += 0x4;
            }
        }

        public static void Loading(int Base)
        {
            WinDlg.Name = 0xC;
            GetWindowsOffset(Base, 0xF02610);
            Base_windows.IsOpen = 0x28;

            Game.Login = 0;
            Game.TimeDisconnect = 0xEB0810; 
            Game.x = 0xEFC16C;
            Game.y = Game.x + 0x4;
            Game.Where = 0xEF1A1C;

            Player.ID = 0xEABDD0;
            Player.Name = Player.ID + 0x4;
            Player.Guild = Player.Name + 0xB0;
            Player.Class = Player.Name + 0x3C;
            Player.Race = Player.Class - 0x4;
            Player.Lvl = Player.Name + 0xA4;
            Player.HP_Max = 0xEB5AA4;
            Player.HP = Player.HP_Max + 0x4;
            Player.MP_Max = Player.HP + 0x4;
            Player.MP = Player.MP_Max + 0x4;
            Player.ED_Max = Player.MP + 0x4;
            Player.ED = Player.ED_Max + 0x2;
            Player.Fly_Max = Player.ED + 0x2;
            Player.Fly_Time = Player.Fly_Max + 0x4;
            Player.IsFly = Player.Fly_Time + 0x4;
            Player.AtkSpeed = Player.IsFly + 0x18;
            Player.AtkSpeed = Player.AtkSpeed + 0xE0;
            Player.MoveSpeed = Player.AtkSpeed + 0x4;
            Game.TimeStamp = Player.MoveSpeed + 0x4;

            Player.IsMove = 0xEAB5A8;
            Player.IsJump = Player.IsMove + 0x2;
            Player.CamRotV = Player.IsMove + 0x14;
            Player.CamRotH = Player.CamRotV + 0x8;
            Player.FreeCamRotH = Player.CamRotH + 0xC;
            Player.FreeCamRot = Player.FreeCamRotH + 0x10;
            Player.X = Player.IsMove + 0x404;
            Player.Y = Player.X + 0x4;
            Player.Z = Player.Y + 0x4;

            Player.QuickBar = 0xE526E8;

            Entity.Is_Target = 0xA656E8;
            Entity.To_Target = Entity.Is_Target - 0x8;
            Entity.Status = 0x264;
            Entity.Loc = 0x140;
            Entity.Type = 0x14E;

            Status.Node = 0x4;
            Status.ID = 0x28;
            Status.ID_Type_NPC = 0xC0;
            Status.ID_Type_Quest = 0x1CC;
            Status.ID_Type_Quest_Type = 0x1D0;
            Status.ID_Object = 0x2C;
            Status.TargetId = 0x330;
            Status.Rot = 0x78C;
            Status.Name = 0x3E;
            Status.Guild = 0x108;
            Status.Lvl = 0x3A;
            Status.HP_Percent = 0x3C;
            Status.HP_Max = 0x12C8;
            Status.HP = Status.HP_Max + 0x4;
            Status.Rank = 0x105C;
            Status.Is_Attackable = 0x10B4;
            Status.Action = 0x1d4;
            Status.Link = 0x204;
            Status.Type = 0x1C;
            Status.WeaponStyle = 0x334;
            Status.Class = 0x21C;
            Status.Stance = 0x2A8;
            Status.State = 0x1588;
            Status.No_Grav = 0x890;
            Status.No_Skill = 0x1690;
            Status.AtkSpeed = 0x4d2;
            Status.MoveSpeed = 0x6a4;
            Status.BuffCount = 0xf5C;
            Status.BuffArray = 0xF64;
            Status.BuffSize = 0x12;

            Buff.ObjectId = 0x0;
            Buff.Id = 0x4;

            Loc.X = 0x64;
            Loc.Y = Loc.X + 0x4;
            Loc.Z = Loc.Y + 0x4;
            Loc.Zplan = 0x6DC;

            EntityList.Pointer = 0xEB60CC;
            EntityList.Array = 0x48;
            EntityList.Size = 0x58;
            EntityList.User = 0xD0;

            PartyList.Jump = 0x490;
            PartyList.Length = 0x7C;
            ForceList.Length = 0x24;

            ChatDlg.IsOpen = 0x28;
            ChatDlg.Jump = 0x4a0;
            ChatDlg.Length = 0x318;
            ChatDlg.Input = 0x328;

            Cube.Base_List = 0x4b0;
            Cube.ListItem = 0x384;
            Cube.ListX = 0x88;
            Cube.ListY = 0x90;
            Cube.ListW = 0x68;
            Cube.ListH = 0X70;
            Cube.ItemListW = 0x320;
            Cube.ItemListH = 0x328;
            Cube.ItemID = 0x88;
            Cube.ItemCD = 0x98;
            Cube.ItemName1 = 0x48;
            Cube.ItemName2 = 0x4;
            Cube.ItemName3 = 0x4;
            Cube.BaseCount = 0xE526A0;
            Cube.Curent = 0x8F8;
            Cube.Size = 0x908;

            Resurrect.Jump = 0x494;
            Resurrect.Bt_x = 0x88;
            Resurrect.Bt_y = 0x90;
            Resurrect.Bt_w = 0x98;
            Resurrect.Bt_h = 0xA0;

            Tmp_Win.Base = 0xF02CD8;
            Tmp_Win.Next = 0x4;
            Tmp_Win.Jump = 0x534;
            Tmp_Win.Bt_x = 0x98;
            Tmp_Win.Bt_y = 0xA0;
            Tmp_Win.Bt_w = 0xA8;
            Tmp_Win.Bt_h = 0xB0;

            Html.Jump = 0x484;
            Html.Bt_Count = 0x274;
            Html.Bt_Base = 0x270;
            Html.Bt_Jump = 0x8;
            Html.Bt_x = 0x98;
            Html.Bt_y = 0xA0;
            Html.Bt_w = 0xA8;
            Html.Bt_h = 0xB0;

            ChainsManager.ArrayStart2 = 0x490;
            ChainsManager.ArrayStart = 0x4C0;

            Chain.AbilityId = 0x3d8;
            Chain.IsElapsed = 0x428; // 0 si skill up
            Chain.Timeout = Chain.IsElapsed + 0x4; // 0 si skill up

            Pet.Loot = 0xEF63F8;

            AbilityList.Pointer = Cube.BaseCount;
            AbilityList.FirstItem = 0xB24;

            AbilityArrayItem.Pointer1 = 0x0;
            AbilityArrayItem.Pointer2 = 0x4;
            AbilityArrayItem.Pointer3 = 0x8;
            AbilityArrayItem.AbilityID = 0xC;
            AbilityArrayItem.AbilityPointer = 0x14;
            AbilityArrayItem.AbilityAcquired = 0x18;

            AbilityPointers.First = 0x0;
            AbilityPointers.Second = 0x14;
            AbilityPointers.Third = 0x4;
            AbilityPointers.Final = 0x8;

            Ability.ID = 0x8;
            Ability.Name = 0x1C;
            Ability.NameLength = 0x2C;
            Ability.LastUseTimestamp = 0x44;
            Ability.Cooldown = 0x38;
            Ability.CooldownEnd = 0x40;
            Ability.CastTime = 0x60;

            /*WinDlg.Name = 0xC;
            GetWindowsOffset(Base, 0xEFE170);
            Base_windows.IsOpen = 0x28;

            Game.Login = 0;
            Game.TimeDisconnect = 0xEACB88; 
            Game.x = 0xEF7CCC;
            Game.y = Game.x + 0x4;
            Game.Where = 0xEF1A1C;

            Player.ID = 0xEA8188;
            Player.Name = Player.ID + 0x4;
            Player.Guild = Player.Name + 0xB0;
            Player.Class = Player.Name + 0x3C;
            Player.Race = Player.Class - 0x4;
            Player.Lvl = Player.Name + 0xA4;
            Player.HP_Max = 0xEB1E14;
            Player.HP = Player.HP_Max + 0x4;
            Player.MP_Max = Player.HP + 0x4;
            Player.MP = Player.MP_Max + 0x4;
            Player.ED_Max = Player.MP + 0x4;
            Player.ED = Player.ED_Max + 0x2;
            Player.Fly_Max = Player.ED + 0x2;
            Player.Fly_Time = Player.Fly_Max + 0x4;
            Player.IsMove = 0xEA7960;
            Player.IsJump = Player.IsMove + 0x2;
            Player.CamRotV = Player.IsMove + 0x14;
            Player.CamRotH = Player.CamRotV + 0x8;
            Player.FreeCamRotH = Player.CamRotH + 0xC;
            Player.FreeCamRot = Player.FreeCamRotH + 0x10;
            Player.X = Player.IsMove + 0x404;
            Player.Y = Player.X + 0x4;
            Player.Z = Player.Y + 0x4;
            Player.IsFly = 0xE7A0E8;
            Player.AtkSpeed = 0xEB1F28;
            Player.MoveSpeed = Player.AtkSpeed + 0x4;
            Game.TimeStamp = Player.MoveSpeed + 0x4;

            Player.QuickBar = 0xE526E8;

            Entity.Is_Target = 0xA626E8;
            Entity.To_Target = Entity.Is_Target - 0x8;
            Entity.Status = 0x264;
            Entity.Loc = 0x140;
            Entity.Type = 0x14E;

            Status.Node = 0x4;
            Status.ID = 0x28;
            Status.ID_Type_NPC = 0xC0;
            Status.ID_Type_Quest = 0x1CC;
            Status.ID_Type_Quest_Type = 0x1D0;
            Status.ID_Object = 0x2C;
            Status.TargetId = 0x330;
            Status.Rot = 0x78C;
            Status.Name = 0x3E;
            Status.Guild = 0x108;
            Status.Lvl = 0x3A;
            Status.HP_Percent = 0x3C;
            Status.HP_Max = 0x12C8;
            Status.HP = Status.HP_Max + 0x4;
            Status.Rank = 0x105C;
            Status.Is_Attackable = 0x10B4;
            Status.Action = 0x1d4;
            Status.Link = 0x204;
            Status.Type = 0x1C;
            Status.WeaponStyle = 0x334;
            Status.Class = 0x21C;
            Status.Stance = 0x2A8;
            Status.State = 0x1588;
            Status.No_Grav = 0x890;
            Status.No_Skill = 0x1690;
            Status.AtkSpeed = 0x4d2;
            Status.MoveSpeed = 0x6a4;
            Status.BuffCount = 0xf5C;
            Status.BuffArray = 0xF64;
            Status.BuffSize = 0x12;

            Buff.ObjectId = 0x0;
            Buff.Id = 0x4;

            Loc.X = 0x64;
            Loc.Y = Loc.X + 0x4;
            Loc.Z = Loc.Y + 0x4;
            Loc.Zplan = 0x6DC;

            EntityList.Pointer = 0xEB2444;
            EntityList.Array = 0x48;
            EntityList.Size = 0x58;
            EntityList.User = 0xD0;

            PartyList.Jump = 0x490;
            PartyList.Length = 0x7C;
            ForceList.Length = 0x24;

            ChatDlg.IsOpen = 0x28;
            ChatDlg.Jump = 0x4a0;
            ChatDlg.Length = 0x318;
            ChatDlg.Input = 0x328;

            Cube.Base_List = 0x4b0;
            Cube.ListItem = 0x384;
            Cube.ListX = 0x88;
            Cube.ListY = 0x90;
            Cube.ListW = 0x68;
            Cube.ListH = 0X70;
            Cube.ItemListW = 0x320;
            Cube.ItemListH = 0x328;
            Cube.ItemID = 0x88;
            Cube.ItemCD = 0x98;
            Cube.ItemName1 = 0x48;
            Cube.ItemName2 = 0x4;
            Cube.ItemName3 = 0x4;
            Cube.BaseCount = 0xE526A0;
            Cube.Curent = 0x8F8;
            Cube.Size = 0x908;

            Resurrect.Jump = 0x494;
            Resurrect.Bt_x = 0x88;
            Resurrect.Bt_y = 0x90;
            Resurrect.Bt_w = 0x98;
            Resurrect.Bt_h = 0xA0;

            Tmp_Win.Base = 0xEFE838;
            Tmp_Win.Next = 0x4;
            Tmp_Win.Jump = 0x534;
            Tmp_Win.Bt_x = 0x98;
            Tmp_Win.Bt_y = 0xA0;
            Tmp_Win.Bt_w = 0xA8;
            Tmp_Win.Bt_h = 0xB0;

            Html.Jump = 0x484;
            Html.Bt_Count = 0x274;
            Html.Bt_Base = 0x270;
            Html.Bt_Jump = 0x8;
            Html.Bt_x = 0x98;
            Html.Bt_y = 0xA0;
            Html.Bt_w = 0xA8;
            Html.Bt_h = 0xB0;

            ChainsManager.ArrayStart2 = 0x490;
            ChainsManager.ArrayStart = 0x4C0;

            Chain.AbilityId = 0x3d8;
            Chain.IsElapsed = 0x428; // 0 si skill up
            Chain.Timeout = Chain.IsElapsed + 0x4; // 0 si skill up

            Pet.Loot = 0xEF1F58;

            AbilityList.Pointer = Cube.BaseCount;
            AbilityList.FirstItem = 0xB24;

            AbilityArrayItem.Pointer1 = 0x0;
            AbilityArrayItem.Pointer2 = 0x4;
            AbilityArrayItem.Pointer3 = 0x8;
            AbilityArrayItem.AbilityID = 0xC;
            AbilityArrayItem.AbilityPointer = 0x14;
            AbilityArrayItem.AbilityAcquired = 0x18;

            AbilityPointers.First = 0x0;
            AbilityPointers.Second = 0x14;
            AbilityPointers.Third = 0x4;
            AbilityPointers.Final = 0x8;

            Ability.ID = 0x8;
            Ability.Name = 0x1C;
            Ability.NameLength = 0x2C;
            Ability.LastUseTimestamp = 0x44;
            Ability.Cooldown = 0x38;
            Ability.CooldownEnd = 0x40;
            Ability.CastTime = 0x60;*/
        }

        public class Base_windows
        {
            public static Hashtable newbase = null;
            public static long IsOpen = 0;
        }

        public class Game
        {
            public static long Lang = 0;
            public static long Login = 0;
            public static long Where = 0;
            public static long FocusInfo = 0;
            public static long TimeStamp = 0;
            public static long TimeDisconnect = 0;
            public static long x = 0;
            public static long y = 0;
            public static long TESTAbyPt = 0xE526A0;
            public static long TESTAbyFirst = 0;
        }

        public class Player
        {
            public static long ID = 0;
            public static long Name = 0;
            public static long Guild = 0;
            public static long Class = 0;
            public static long Race = 0;
            public static long Lvl = 0;
            public static long HP_Max = 0;
            public static long HP = 0;
            public static long MP_Max = 0;
            public static long MP = 0;
            public static long ED_Max = 0;
            public static long ED = 0;
            public static long Fly_Max = 0;
            public static long Fly_Time = 0;
            public static long CamRotV = 0;
            public static long CamRotH = 0;
            public static long FreeCamRot = 0;
            public static long FreeCamRotH = 0;
            public static long ID_Zone = 0;
            public static long X = 0;
            public static long Y = 0;
            public static long Z = 0;
            public static long CDFly = 0;
            public static long IsFly = 0;
            public static long IsMove = 0;
            public static long IsJump = 0;
            public static long AtkSpeed = 0;
            public static long MoveSpeed = 0;
            public static long QuickBar = 0;
        }

        public class PlayerCast
        {
            public static long EnchanteBase = 0;
            public static long SkillBase = 0;
            public static long IsOpen = 0;
        }

        public class Entity
        {
            public static long Is_Target = 0;
            public static long To_Target = 0;
            public static long Status = 0;
            public static long Loc = 0;
            public static long Type = 0;
        }

        public class Status
        {
            public static long Node = 0;
            public static long ID = 0;
            public static long ID_Type_NPC = 0;
            public static long ID_Type_Quest = 0;
            public static long ID_Type_Quest_Type = 0;
            public static long ID_Object = 0;
            public static long TargetId = 0;
            public static long Rot = 0;
            public static long Name = 0;
            public static long Guild = 0;
            public static long Lvl = 0;
            public static long HP_Percent = 0;
            public static long HP_Max = 0;
            public static long HP = 0;
            public static long Rank = 0;
            public static long Is_Attackable = 0;
            public static long Action = 0;
            public static long Link = 0;
            public static long Opacity = 0;
            public static long Type = 0;
            public static long WeaponStyle = 0;
            public static long Class = 0;
            public static long Stance = 0;
            public static long State = 0;
            public static long No_Grav = 0;
            public static long No_Skill = 0;
            public static long AtkSpeed = 0;
            public static long MoveSpeed = 0;
            public static long BuffCount = 0;
            public static long BuffArray = 0;
            public static long BuffSize = 0;
        }

        public class Buff
        {
            public static long ObjectId = 0;
            public static long Id = 0;
        }

        public class Loc
        {
            public static long X = 0;
            public static long Y = 0;
            public static long Z = 0;
            public static long Zplan = 0;
        }

        public class EntityList
        {
            public static long Pointer = 0;
            public static long Array = 0;
            public static long Size = 0;
            public static long User = 0;
        }

        public class PartyList
        {
            public static long Base = 0;
            public static long Jump = 0;
            public static long Length = 0;
        }

        public class ForceList
        {
            public static long Length = 0;
        }

        public class ChatDlg
        {
            public static long IsOpen = 0;
            public static long Jump = 0;
            public static long Length = 0;
            public static long Input = 0;
        }

        public class WinDlg
        {
            public static long Name = 0;
        }

        public class Cube
        {
            public static long Base_List = 0;
            public static long ListItem = 0;
            public static long ListX = 0;
            public static long ListY = 0;
            public static long ListW = 0;
            public static long ListH = 0;
            public static long ItemListW = 0;
            public static long ItemListH = 0;
            public static long ItemID = 0;
            public static long ItemCD = 0;
            public static long ItemName1 = 0;
            public static long ItemName2 = 0;
            public static long ItemName3 = 0;
            public static long BaseCount = 0;
            public static long Curent = 0;
            public static long Size = 0;
        }

        public class Resurrect
        {
            public static long Jump = 0;
            public static long Bt_x = 0;
            public static long Bt_y = 0;
            public static long Bt_w = 0;
            public static long Bt_h = 0;
        }

        public class Html
        {
            public static long Jump = 0;
            public static long Bt_Count = 0;
            public static long Bt_Base = 0;
            public static long Bt_Jump = 0;
            public static long Bt_x = 0;
            public static long Bt_y = 0;
            public static long Bt_w = 0;
            public static long Bt_h = 0;
        }

        public class Tmp_Win
        {
            public static long Base = 0;
            public static long Next = 0;
            public static long Jump = 0;
            public static long Bt_x = 0;
            public static long Bt_y = 0;
            public static long Bt_w = 0;
            public static long Bt_h = 0;
        }

        public class ChainsManager
        {
            public static long ArrayStart2 = 0;
            public static long ArrayStart = 0;
        }

        public class Chain
        {
            public static long AbilityId = 0;
            public static long Timeout = 0;
            public static long IsElapsed = 0;
        }

        public class Pet
        {
            public static long Loot = 0;
        }

        public class AbilityList
        {
            public static long Pointer = 0;
            public static long FirstItem = 0;
        }

        public class AbilityArrayItem
        {
            public static long Pointer1 = 0;
            public static long Pointer2 = 0;
            public static long Pointer3 = 0;
            public static long AbilityID = 0;
            public static long AbilityPointer = 0;
            public static long AbilityAcquired = 0;
        }

        public class AbilityPointers
        {
            public static long First = 0;
            public static long Second = 0;
            public static long Third = 0;
            public static long Final = 0;
        }

        public class Ability
        {
            public static long ID = 0;
            public static long Name = 0;
            public static long NameLength = 0;
            public static long LastUseTimestamp = 0;
            public static long Cooldown = 0;
            public static long CooldownEnd = 0;
            public static long CastTime = 0;
        }
    }

    public class EnumAion
    {

        public enum AionServeur
        {
            Spatalos = 31,
            Telemachus,
            Castor,
            Gorgos = 35,
            Kromede,
            Thor,
            Votan,
            Balder,
            Urtem,
            Vidar,
            Suthran,
            Vehalla,
            Nexus,
            Calindi,
            Anuhart,
            Alquima,
            Curatus,
            Barus,
            Miren,
            Zubaba,
            Kalabar,
            Lakhara,
            Marchutan,
            Tribunus,
            Sif,
            Freyr,
            Jebal,
            Hisui
        }

        public enum AionZone
        {
            ldf4a = 600020000,
            ldf4b = 600030000,
            ldf5a = 600050000,
            ldf5b = 600060000,
            LC1 = 110010000,
            DC1 = 120010000,
            df4 = 220070000,
            lf4 = 210050000
        }

        public enum AionClasses
        {
            Warrior = 0,
            Gladiator,
            Templar,
            Scout,
            Assassin,
            Ranger,
            Mage,
            Sorcerer,
            Spiritmaster,
            Priest,
            Cleric,
            Chanter,
            Ingenior,
            Aethertech,
            Gunner,
            Artist,
            Songweaver
        }

        public enum AionWeaponStyle
        {
            None = -1,
            Hand = 0,
            Sword,
            Dagger,
            Mace,
            Key,
            Polearm,
            Staff,
            Greatsword,
            Harp,
            Orb,
            Book,
            Pistol,
            Cannon,
            Bow
        }

        public enum AionRace
        {
            Elyos = 0,
            Asmo
        }

        public enum eRank
        {
            r9 = 1,
            r8,
            r7,
            r6,
            r5,
            r4,
            r3,
            r2,
            r1,
            o1,
            o2,
            o3,
            o4,
            o5,
            g,
            gg,
            c,
            G
        }

        public struct eType
        {
            public const int None = 0;
            public const int Player = 1;
            public const int User = 2;
            public const int NPC = 3;
            public const int GameObject = 4;
            public const int Equipment = 5;
            public const int Deprecated1 = 6;
            public const int Projectile = 7;
            public const int Door = 8;
            public const int Gather = 9;
            public const int Rope = 10;
            public const int Vehicle = 11;
            public const int Vessel = 12;
            public const int BasicEntity = 13;
            public const int Deprecated2 = 14;
            public const int Fish = 15;
            public const int Birds = 16;
            public const int Bugs = 17;
            public const int PlaceableObject = 18;
            public const int Milestone = 19;
            public const int ItemObject = 20;
            public const int client_npc = 21;
            public const int cooking = 22;
            public const int weapon_craft = 23;
            public const int armor_craft = 24;
            public const int leatherwork = 25;
            public const int tailoring = 26;
            public const int handiwork = 27;
            public const int alchemy = 28;
            public const int carpentry = 29;
            public const int Emblem = 30;
            public const int AbyssDoor = 31;
            public const int AbyssArtifacts = 32;
            public const int AbyssShield = 33;
            public const int Abyss_pvpeffect = 34;
            public const int ExtendedEntity = 35;
            public const int AbyssCarrier = 36;
            public const int AbyssControlTower = 37;
            public const int Milestone_Ndist = 38;
            public const int AbyssCarrier_Submesh = 39;
            public const int Chair = 40;
            public const int DirectPortalEff = 41;
            public const int FindHelper = 42;
            public const int Opaque = 43;
            public const int JumpTrigger = 44;
            public const int Pet = 46;
            public const int Mercenary = 47;
            public const int ToyPet = 48;
            public const int HousingBuilding = 49;
            public const int Deprecated3 = 50;
            public const int HousingInterior = 51;
            public const int HousingBuildingIndoor = 52;
            public const int Deprecated4 = 53;
            public const int HousingVisualEffect = 54;
            public const int HousingDoor = 55;
            public const int NPCSearchBoard = 56;
            public const int WorldEventEntity = 57;
            public const int WindPath = 58;
            public const int menuisier = 59;
            public const int UiAxis = 60;
            public const int MercenaryTroop = 61;
            public const int TownObject = 62;
        }

        public struct eAttitude
        {
            public const int Passive = 0;
            public const int NoCombat = 2;
            public const int Hostile = 8;
            public const int NoFight = 10;
            public const int Friendly = 38;
            public const int Utility = 294;
        }

        public struct eAction
        {
            public const int None = 0;
            public const int Object = 7;
            public const int NotObject = 8;
            public const int Attackable = 13;
            public const int NotAttackable = 14;
            public const int Chat = 15;
            public const int Vendor = 17;
            public const int Low_Gatherable = 22;
            public const int Low_NotGatherable = 23;
            public const int Lootable = 40;
            public const int NotLootable = 41;
            public const int Gatherable = 43;
            public const int NotGatherable = 44;
        }

        public struct eStance
        {
            public const int Normal = 0;
            public const int Combat = 1;
            public const int Monture = 2;
            public const int Resting = 3;
            public const int Flying = 4;
            public const int FlyingCombat = 5;
            public const int Monture2 = 7;
            public const int Dead = 10;
        }

        public enum eTypeNPC
        {
            LegionEntrepo = 350409,
            Entrepo = 350417,
            Negociant = 350419,
            Teleport = 350423
        }

        public struct eTypeQuest
        {
            public const int Start_Quest = 3;
            public const int Inter_Quest = 7;
            public const int Final_Quest = 11;
        }

        public enum roman_numerals
        {
            I = 1,
            II,
            III,
            IV,
            V,
            VI,
            VII,
            VIII,
            IX,
            X,
            XI,
            XII,
            XIII,
            XIV,
            XV,
            XVI,
            XVII,
            XVIII,
            XIX,
            XX
        }

        public class Gather
        {
            public static Hashtable gather = new Hashtable();
            public Gather()
            {
                gather.Add(400001, 20);
                gather.Add(400002, 55);
                gather.Add(400003, 75);
                gather.Add(400004, 120);
                gather.Add(400005, 180);
                gather.Add(400006, 180);
                gather.Add(400007, 240);
                gather.Add(400008, 280);
                gather.Add(400009, 330);
                gather.Add(400010, 375);
                gather.Add(400011, 75);
                gather.Add(400012, 280);
                gather.Add(400013, 330);
                gather.Add(400014, 375);
                gather.Add(400015, 375);
                gather.Add(400016, 410);
                gather.Add(400017, 455);
                gather.Add(400018, 485);
                gather.Add(400019, 410);
                gather.Add(400020, 455);
                gather.Add(400021, 455);
                gather.Add(400022, 485);
                gather.Add(400023, 499);
                gather.Add(400051, 20);
                gather.Add(400052, 55);
                gather.Add(400053, 75);
                gather.Add(400054, 140);
                gather.Add(400055, 190);
                gather.Add(400056, 230);
                gather.Add(400057, 290);
                gather.Add(400058, 290);
                gather.Add(400059, 335);
                gather.Add(400060, 375);
                gather.Add(400061, 75);
                gather.Add(400062, 140);
                gather.Add(400063, 290);
                gather.Add(400064, 335);
                gather.Add(400065, 375);
                gather.Add(400066, 375);
                gather.Add(400067, 425);
                gather.Add(400068, 440);
                gather.Add(400069, 490);
                gather.Add(400070, 375);
                gather.Add(400071, 425);
                gather.Add(400072, 440);
                gather.Add(400073, 490);
                gather.Add(400101, 35);
                gather.Add(400102, 160);
                gather.Add(400103, 210);
                gather.Add(400104, 320);
                gather.Add(400105, 160);
                gather.Add(400106, 320);
                gather.Add(400107, 370);
                gather.Add(400108, 320);
                gather.Add(400109, 460);
                gather.Add(400110, 460);
                gather.Add(400111, 440);
                gather.Add(400112, 499);
                gather.Add(400151, 35);
                gather.Add(400152, 160);
                gather.Add(400153, 210);
                gather.Add(400154, 320);
                gather.Add(400155, 160);
                gather.Add(400156, 320);
                gather.Add(400157, 320);
                gather.Add(400158, 460);
                gather.Add(400159, 460);
                gather.Add(400160, 460);
                gather.Add(400161, 460);
                gather.Add(400201, 15);
                gather.Add(400202, 15);
                gather.Add(400203, 15);
                gather.Add(400204, 100);
                gather.Add(400205, 100);
                gather.Add(400206, 200);
                gather.Add(400207, 200);
                gather.Add(400208, 300);
                gather.Add(400209, 100);
                gather.Add(400210, 300);
                gather.Add(400211, 360);
                gather.Add(400212, 300);
                gather.Add(400213, 400);
                gather.Add(400214, 499);
                gather.Add(400215, 499);
                gather.Add(400251, 15);
                gather.Add(400252, 15);
                gather.Add(400253, 15);
                gather.Add(400254, 100);
                gather.Add(400255, 100);
                gather.Add(400256, 200);
                gather.Add(400257, 200);
                gather.Add(400258, 300);
                gather.Add(400259, 100);
                gather.Add(400260, 300);
                gather.Add(400261, 400);
                gather.Add(400262, 400);
                gather.Add(400263, 400);
                gather.Add(400264, 499);
                gather.Add(400301, 40);
                gather.Add(400302, 80);
                gather.Add(400303, 60);
                gather.Add(400304, 145);
                gather.Add(400305, 155);
                gather.Add(400306, 185);
                gather.Add(400307, 215);
                gather.Add(400308, 245);
                gather.Add(400309, 280);
                gather.Add(400310, 315);
                gather.Add(400311, 350);
                gather.Add(400312, 380);
                gather.Add(400313, 145);
                gather.Add(400314, 155);
                gather.Add(400315, 185);
                gather.Add(400316, 315);
                gather.Add(400317, 315);
                gather.Add(400318, 350);
                gather.Add(400319, 350);
                gather.Add(400320, 380);
                gather.Add(400321, 380);
                gather.Add(400322, 380);
                gather.Add(400323, 450);
                gather.Add(400324, 420);
                gather.Add(400325, 495);
                gather.Add(400326, 499);
                gather.Add(400327, 499);
                gather.Add(400351, 40);
                gather.Add(400352, 80);
                gather.Add(400353, 60);
                gather.Add(400354, 145);
                gather.Add(400355, 155);
                gather.Add(400356, 185);
                gather.Add(400357, 215);
                gather.Add(400358, 245);
                gather.Add(400359, 280);
                gather.Add(400360, 315);
                gather.Add(400361, 350);
                gather.Add(400362, 380);
                gather.Add(400363, 145);
                gather.Add(400364, 155);
                gather.Add(400365, 185);
                gather.Add(400366, 315);
                gather.Add(400367, 350);
                gather.Add(400368, 350);
                gather.Add(400369, 380);
                gather.Add(400370, 380);
                gather.Add(400371, 380);
                gather.Add(400372, 450);
                gather.Add(400373, 420);
                gather.Add(400374, 495);
                gather.Add(400375, 450);
                gather.Add(400376, 420);
                gather.Add(400377, 450);
                gather.Add(400378, 420);
                gather.Add(400379, 495);
                gather.Add(400401, 30);
                gather.Add(400402, 30);
                gather.Add(400403, 140);
                gather.Add(400404, 140);
                gather.Add(400405, 140);
                gather.Add(400406, 250);
                gather.Add(400407, 250);
                gather.Add(400408, 250);
                gather.Add(400409, 250);
                gather.Add(400410, 345);
                gather.Add(400411, 345);
                gather.Add(400412, 140);
                gather.Add(400413, 345);
                gather.Add(400414, 480);
                gather.Add(400415, 480);
                gather.Add(400416, 480);
                gather.Add(400417, 499);
                gather.Add(400418, 499);
                gather.Add(400451, 30);
                gather.Add(400452, 30);
                gather.Add(400453, 110);
                gather.Add(400454, 110);
                gather.Add(400455, 250);
                gather.Add(400456, 250);
                gather.Add(400457, 340);
                gather.Add(400458, 110);
                gather.Add(400459, 340);
                gather.Add(400460, 410);
                gather.Add(400461, 410);
                gather.Add(400462, 410);
                gather.Add(400463, 410);
                gather.Add(400464, 410);
                gather.Add(400465, 499);
                gather.Add(400501, 25);
                gather.Add(400502, 25);
                gather.Add(400503, 150);
                gather.Add(400504, 150);
                gather.Add(400505, 150);
                gather.Add(400506, 150);
                gather.Add(400507, 235);
                gather.Add(400508, 235);
                gather.Add(400509, 335);
                gather.Add(400510, 150);
                gather.Add(400511, 335);
                gather.Add(400512, 415);
                gather.Add(400513, 410);
                gather.Add(400514, 415);
                gather.Add(400515, 415);
                gather.Add(400516, 499);
                gather.Add(400551, 25);
                gather.Add(400552, 25);
                gather.Add(400553, 120);
                gather.Add(400554, 120);
                gather.Add(400555, 260);
                gather.Add(400556, 260);
                gather.Add(400557, 260);
                gather.Add(400558, 330);
                gather.Add(400559, 330);
                gather.Add(400560, 120);
                gather.Add(400561, 330);
                gather.Add(400562, 470);
                gather.Add(400563, 470);
                gather.Add(400564, 470);
                gather.Add(400601, 1);
                gather.Add(400602, 1);
                gather.Add(400603, 1);
                gather.Add(400604, 45);
                gather.Add(400605, 45);
                gather.Add(400606, 105);
                gather.Add(400607, 105);
                gather.Add(400608, 170);
                gather.Add(400609, 225);
                gather.Add(400610, 270);
                gather.Add(400611, 310);
                gather.Add(400612, 310);
                gather.Add(400613, 365);
                gather.Add(400614, 105);
                gather.Add(400615, 270);
                gather.Add(400616, 310);
                gather.Add(400617, 365);
                gather.Add(400618, 430);
                gather.Add(400619, 470);
                gather.Add(400620, 430);
                gather.Add(400621, 470);
                gather.Add(400622, 499);
                gather.Add(400623, 499);
                gather.Add(400651, 1);
                gather.Add(400652, 1);
                gather.Add(400653, 45);
                gather.Add(400654, 45);
                gather.Add(400655, 130);
                gather.Add(400656, 170);
                gather.Add(400657, 220);
                gather.Add(400658, 270);
                gather.Add(400659, 305);
                gather.Add(400660, 365);
                gather.Add(400661, 130);
                gather.Add(400662, 305);
                gather.Add(400663, 365);
                gather.Add(400664, 445);
                gather.Add(400665, 485);
                gather.Add(400666, 445);
                gather.Add(400667, 485);
                gather.Add(400668, 485);
                gather.Add(400701, 10);
                gather.Add(400702, 10);
                gather.Add(400703, 50);
                gather.Add(400704, 50);
                gather.Add(400705, 90);
                gather.Add(400706, 110);
                gather.Add(400707, 165);
                gather.Add(400708, 220);
                gather.Add(400709, 260);
                gather.Add(400710, 325);
                gather.Add(400711, 355);
                gather.Add(400712, 385);
                gather.Add(400713, 90);
                gather.Add(400714, 325);
                gather.Add(400715, 355);
                gather.Add(400716, 355);
                gather.Add(400717, 385);
                gather.Add(400718, 385);
                gather.Add(400719, 435);
                gather.Add(400720, 490);
                gather.Add(400721, 420);
                gather.Add(400722, 435);
                gather.Add(400723, 490);
                gather.Add(400724, 499);
                gather.Add(400725, 499);
                gather.Add(400751, 10);
                gather.Add(400752, 10);
                gather.Add(400753, 50);
                gather.Add(400754, 50);
                gather.Add(400755, 90);
                gather.Add(400756, 135);
                gather.Add(400757, 180);
                gather.Add(400758, 240);
                gather.Add(400759, 275);
                gather.Add(400760, 275);
                gather.Add(400761, 325);
                gather.Add(400762, 355);
                gather.Add(400763, 390);
                gather.Add(400764, 135);
                gather.Add(400765, 275);
                gather.Add(400766, 355);
                gather.Add(400767, 355);
                gather.Add(400768, 390);
                gather.Add(400769, 430);
                gather.Add(400770, 480);
                gather.Add(400771, 390);
                gather.Add(400772, 430);
                gather.Add(400773, 480);
                gather.Add(400801, 5);
                gather.Add(400802, 65);
                gather.Add(400803, 65);
                gather.Add(400804, 95);
                gather.Add(400805, 95);
                gather.Add(400806, 130);
                gather.Add(400807, 190);
                gather.Add(400808, 230);
                gather.Add(400809, 265);
                gather.Add(400810, 290);
                gather.Add(400811, 445);
                gather.Add(400812, 495);
                gather.Add(400813, 430);
                gather.Add(400814, 445);
                gather.Add(400815, 495);
                gather.Add(400816, 499);
                gather.Add(400851, 5);
                gather.Add(400852, 65);
                gather.Add(400853, 65);
                gather.Add(400854, 95);
                gather.Add(400855, 115);
                gather.Add(400856, 150);
                gather.Add(400857, 255);
                gather.Add(400858, 310);
                gather.Add(400859, 345);
                gather.Add(400860, 310);
                gather.Add(400861, 345);
                gather.Add(400862, 345);
                gather.Add(400863, 345);
                gather.Add(400864, 430);
                gather.Add(400865, 480);
                gather.Add(400866, 430);
                gather.Add(400867, 480);
                gather.Add(400901, 1);
                gather.Add(400902, 85);
                gather.Add(400903, 305);
                gather.Add(400904, 305);
                gather.Add(400905, 440);
                gather.Add(400906, 475);
                gather.Add(400907, 440);
                gather.Add(400908, 475);
                gather.Add(400909, 499);
                gather.Add(400910, 499);
                gather.Add(400951, 1);
                gather.Add(400952, 85);
                gather.Add(400953, 280);
                gather.Add(400954, 85);
                gather.Add(400955, 415);
                gather.Add(400956, 465);
                gather.Add(400957, 415);
                gather.Add(400958, 465);
                gather.Add(401001, 1);
                gather.Add(401002, 1);
                gather.Add(401003, 25);
                gather.Add(401004, 50);
                gather.Add(401005, 75);
                gather.Add(401006, 75);
                gather.Add(401007, 100);
                gather.Add(401008, 100);
                gather.Add(401009, 100);
                gather.Add(401010, 125);
                gather.Add(401011, 150);
                gather.Add(401012, 175);
                gather.Add(401013, 200);
                gather.Add(401014, 200);
                gather.Add(401015, 225);
                gather.Add(401016, 250);
                gather.Add(401017, 275);
                gather.Add(401018, 300);
                gather.Add(401019, 300);
                gather.Add(401020, 325);
                gather.Add(401021, 350);
                gather.Add(401022, 375);
                gather.Add(401023, 125);
                gather.Add(401024, 150);
                gather.Add(401025, 175);
                gather.Add(401026, 225);
                gather.Add(401027, 250);
                gather.Add(401028, 275);
                gather.Add(401029, 325);
                gather.Add(401030, 350);
                gather.Add(401031, 375);
                gather.Add(401032, 225);
                gather.Add(401033, 250);
                gather.Add(401034, 275);
                gather.Add(401035, 325);
                gather.Add(401036, 399);
                gather.Add(401037, 400);
                gather.Add(401038, 425);
                gather.Add(401039, 450);
                gather.Add(401040, 475);
                gather.Add(401041, 400);
                gather.Add(401042, 425);
                gather.Add(401043, 450);
                gather.Add(401044, 375);
                gather.Add(401045, 499);
                gather.Add(401051, 1);
                gather.Add(401052, 1);
                gather.Add(401053, 25);
                gather.Add(401054, 50);
                gather.Add(401055, 75);
                gather.Add(401056, 75);
                gather.Add(401057, 100);
                gather.Add(401058, 100);
                gather.Add(401059, 100);
                gather.Add(401060, 125);
                gather.Add(401061, 150);
                gather.Add(401062, 175);
                gather.Add(401063, 200);
                gather.Add(401064, 200);
                gather.Add(401065, 225);
                gather.Add(401066, 250);
                gather.Add(401067, 275);
                gather.Add(401068, 300);
                gather.Add(401069, 300);
                gather.Add(401070, 325);
                gather.Add(401071, 350);
                gather.Add(401072, 375);
                gather.Add(401073, 399);
                gather.Add(401074, 400);
                gather.Add(401075, 425);
                gather.Add(401076, 450);
                gather.Add(401077, 475);
                gather.Add(401078, 375);
                gather.Add(401079, 400);
                gather.Add(401080, 425);
                gather.Add(401081, 450);
                gather.Add(401082, 475);
                gather.Add(401083, 400);
                gather.Add(401084, 425);
                gather.Add(401085, 450);
                gather.Add(401086, 475);
                gather.Add(401087, 400);
                gather.Add(401101, 100);
                gather.Add(401102, 210);
                gather.Add(401103, 155);
                gather.Add(401104, 235);
                gather.Add(401105, 220);
                gather.Add(401106, 250);
                gather.Add(401107, 225);
                gather.Add(401108, 240);
                gather.Add(401109, 315);
                gather.Add(401110, 380);
                gather.Add(401111, 300);
                gather.Add(401112, 399);
                gather.Add(401113, 400);
                gather.Add(401114, 450);
                gather.Add(401115, 495);
                gather.Add(401116, 480);
                gather.Add(401117, 415);
                gather.Add(401118, 499);
                gather.Add(401119, 499);
                gather.Add(401151, 130);
                gather.Add(401152, 315);
                gather.Add(401153, 400);
                gather.Add(401154, 450);
                gather.Add(401155, 495);
                gather.Add(401156, 410);
                gather.Add(401157, 470);
                gather.Add(401158, 400);
                gather.Add(401159, 400);
                gather.Add(401160, 480);
                gather.Add(401161, 410);
                gather.Add(401162, 400);
                gather.Add(401163, 450);
                gather.Add(401164, 495);
                gather.Add(401165, 420);
                gather.Add(401166, 400);
                gather.Add(401167, 499);
                gather.Add(402001, 400);
                gather.Add(402002, 400);
                gather.Add(402101, 400);
                gather.Add(402102, 400);
                gather.Add(403000, 1);
                gather.Add(403001, 1);
                gather.Add(403002, 1);
                gather.Add(403003, 40);
                gather.Add(403004, 40);
                gather.Add(403005, 40);
                gather.Add(403006, 80);
                gather.Add(403007, 80);
                gather.Add(403008, 80);
                gather.Add(403009, 120);
                gather.Add(403010, 120);
                gather.Add(403011, 120);
                gather.Add(403012, 160);
                gather.Add(403013, 160);
                gather.Add(403014, 160);
                gather.Add(403015, 200);
                gather.Add(403016, 200);
                gather.Add(403017, 200);
                gather.Add(403018, 240);
                gather.Add(403019, 240);
                gather.Add(403020, 240);
                gather.Add(403021, 280);
                gather.Add(403022, 280);
                gather.Add(403023, 280);
                gather.Add(403500, 1);
                gather.Add(403501, 1);
                gather.Add(403502, 1);
                gather.Add(403503, 40);
                gather.Add(403504, 40);
                gather.Add(403505, 40);
                gather.Add(403506, 80);
                gather.Add(403507, 80);
                gather.Add(403508, 80);
                gather.Add(403509, 120);
                gather.Add(403510, 120);
                gather.Add(403511, 120);
                gather.Add(403512, 160);
                gather.Add(403513, 160);
                gather.Add(403514, 160);
                gather.Add(403515, 200);
                gather.Add(403516, 200);
                gather.Add(403517, 200);
                gather.Add(403518, 240);
                gather.Add(403519, 240);
                gather.Add(403520, 240);
                gather.Add(403521, 280);
                gather.Add(403522, 280);
                gather.Add(403523, 280);
                gather.Add(405000, 350);
                gather.Add(405001, 350);
                gather.Add(408001, 400);
                gather.Add(408002, 400);
                gather.Add(409000, 320);
                gather.Add(409001, 300);
                gather.Add(409002, 300);
                gather.Add(409003, 485);
                gather.Add(409004, 490);
                gather.Add(409005, 460);
                gather.Add(409006, 400);
                gather.Add(409007, 450);
                gather.Add(409008, 420);
                gather.Add(409009, 495);
                gather.Add(409010, 435);
                gather.Add(409011, 430);
                gather.Add(409012, 475);
                gather.Add(409013, 350);
                gather.Add(409014, 375);
                gather.Add(409015, 315);
                gather.Add(409016, 350);
                gather.Add(409017, 380);
                gather.Add(409018, 400);
                gather.Add(409019, 400);
                gather.Add(409020, 400);
                gather.Add(409021, 400);
                gather.Add(409022, 375);
                gather.Add(409023, 375);
                gather.Add(409024, 365);
                gather.Add(409025, 365);
                gather.Add(409026, 385);
                gather.Add(409027, 390);
                gather.Add(409028, 415);
                gather.Add(409029, 465);
                gather.Add(409030, 440);
                gather.Add(409031, 475);
                gather.Add(409032, 430);
                gather.Add(409033, 480);
                gather.Add(409034, 445);
                gather.Add(409035, 495);
                gather.Add(409036, 480);
                gather.Add(409037, 490);
                gather.Add(409038, 445);
                gather.Add(409039, 485);
                gather.Add(409040, 430);
                gather.Add(409041, 470);
                gather.Add(409042, 470);
                gather.Add(409043, 415);
                gather.Add(409044, 410);
                gather.Add(409045, 480);
                gather.Add(409046, 410);
                gather.Add(409047, 455);
                gather.Add(409048, 425);
                gather.Add(409049, 440);
                gather.Add(409050, 399);
                gather.Add(409051, 430);
                gather.Add(409052, 480);
                gather.Add(409053, 465);
                gather.Add(409054, 500);
                gather.Add(409055, 500);
                gather.Add(409056, 440);
                gather.Add(409057, 410);
                gather.Add(409058, 455);
                gather.Add(409059, 455);
                gather.Add(409060, 485);
                gather.Add(409061, 425);
                gather.Add(409062, 440);
                gather.Add(409063, 490);
                gather.Add(409064, 415);
                gather.Add(409065, 415);
                gather.Add(409066, 470);
                gather.Add(409067, 470);
                gather.Add(409068, 430);
                gather.Add(409069, 470);
                gather.Add(409070, 445);
                gather.Add(409071, 485);
                gather.Add(409072, 410);
                gather.Add(409073, 455);
                gather.Add(409075, 485);
                gather.Add(409076, 425);
                gather.Add(409077, 440);
                gather.Add(409078, 490);
                gather.Add(409079, 460);
                gather.Add(409081, 400);
                gather.Add(409083, 450);
                gather.Add(409084, 420);
                gather.Add(409087, 495);
                gather.Add(409088, 480);
                gather.Add(409090, 410);
                gather.Add(409092, 415);
                gather.Add(409094, 470);
                gather.Add(409096, 430);
                gather.Add(409097, 470);
                gather.Add(409098, 445);
                gather.Add(409099, 485);
                gather.Add(409101, 435);
                gather.Add(409102, 490);
                gather.Add(409103, 430);
                gather.Add(409104, 480);
                gather.Add(409105, 445);
                gather.Add(409106, 495);
                gather.Add(409107, 430);
                gather.Add(409108, 480);
                gather.Add(409109, 440);
                gather.Add(409110, 475);
                gather.Add(409111, 415);
                gather.Add(409112, 465);
                gather.Add(409113, 400);
                gather.Add(409114, 400);
                gather.Add(409115, 400);
                gather.Add(409116, 1);
                gather.Add(409117, 1);
                gather.Add(409118, 1);
                gather.Add(409119, 40);
                gather.Add(409120, 40);
                gather.Add(409121, 40);
                gather.Add(409122, 80);
                gather.Add(409123, 80);
                gather.Add(409124, 80);
                gather.Add(409125, 120);
                gather.Add(409126, 120);
                gather.Add(409127, 120);
                gather.Add(409128, 160);
                gather.Add(409129, 160);
                gather.Add(409130, 160);
                gather.Add(409131, 200);
                gather.Add(409132, 200);
                gather.Add(409133, 200);
                gather.Add(409134, 240);
                gather.Add(409135, 240);
                gather.Add(409136, 240);
                gather.Add(409137, 280);
                gather.Add(409138, 280);
                gather.Add(409139, 280);
                gather.Add(409140, 1);
                gather.Add(409141, 1);
                gather.Add(409142, 1);
                gather.Add(409143, 40);
                gather.Add(409144, 40);
                gather.Add(409145, 40);
                gather.Add(409146, 80);
                gather.Add(409147, 80);
                gather.Add(409148, 80);
                gather.Add(409149, 120);
                gather.Add(409150, 120);
                gather.Add(409151, 120);
                gather.Add(409152, 160);
                gather.Add(409153, 160);
                gather.Add(409154, 160);
                gather.Add(409155, 200);
                gather.Add(409156, 200);
                gather.Add(409157, 200);
                gather.Add(409158, 240);
                gather.Add(409159, 240);
                gather.Add(409160, 240);
                gather.Add(409161, 280);
                gather.Add(409162, 280);
                gather.Add(409163, 280);
                gather.Add(409164, 40);
                gather.Add(409165, 45);
                gather.Add(409166, 50);
                gather.Add(409167, 55);
                gather.Add(409168, 60);
                gather.Add(409169, 65);
                gather.Add(409170, 75);
                gather.Add(409171, 80);
                gather.Add(409172, 85);
                gather.Add(409173, 90);
                gather.Add(409174, 95);
                gather.Add(409175, 100);
                gather.Add(409176, 105);
                gather.Add(409177, 110);
                gather.Add(409178, 120);
                gather.Add(409179, 130);
                gather.Add(409180, 140);
                gather.Add(409181, 145);
                gather.Add(409182, 150);
                gather.Add(409183, 155);
                gather.Add(409184, 160);
                gather.Add(409185, 165);
                gather.Add(409186, 170);
                gather.Add(409187, 180);
                gather.Add(409188, 185);
                gather.Add(409189, 190);
                gather.Add(409190, 200);
                gather.Add(409191, 210);
                gather.Add(409192, 215);
                gather.Add(409193, 220);
                gather.Add(409194, 225);
                gather.Add(409195, 230);
                gather.Add(409196, 235);
                gather.Add(409197, 240);
                gather.Add(409198, 245);
                gather.Add(409199, 250);
                gather.Add(409200, 260);
                gather.Add(409201, 265);
                gather.Add(409202, 270);
                gather.Add(409203, 280);
                gather.Add(409204, 290);
                gather.Add(409205, 300);
                gather.Add(409206, 50);
                gather.Add(409207, 75);
                gather.Add(409208, 100);
                gather.Add(409209, 125);
                gather.Add(409210, 150);
                gather.Add(409211, 175);
                gather.Add(409212, 200);
                gather.Add(409213, 225);
                gather.Add(409214, 250);
                gather.Add(409215, 275);
                gather.Add(409216, 300);
                gather.Add(409217, 499);
                gather.Add(409218, 499);
                gather.Add(409219, 499);
                gather.Add(409220, 499);
                gather.Add(409221, 499);
                gather.Add(409222, 499);
                gather.Add(409223, 499);
                gather.Add(409224, 499);
                gather.Add(409225, 499);
                gather.Add(409226, 499);
                gather.Add(409227, 499);
                gather.Add(409228, 499);
                gather.Add(409229, 499);
                gather.Add(409230, 499);
                gather.Add(409231, 499);
                gather.Add(409232, 499);
                gather.Add(409233, 499);
                gather.Add(409234, 499);
            }
        }
    }
}
