using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aion_Game
{
    public enum eServeur
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
        Hisui,
        Rookie1 = 111,
        Rookie2,
        Rookie3,
        Rookie4,
        Rookie5,
        Rookie6,
        Rookie7,
        Rookie8,
        Rookie9,
    }

    public enum eType
    {
        None = 0,
        Player = 1,
        User = 2,
        NPC = 3,
        GameObject = 4,
        Equipment = 5,
        Deprecated1 = 6,
        Projectile = 7,
        Door = 8,
        Gather = 9,
        Rope = 10,
        Vehicle = 11,
        Vessel = 12,
        BasicEntity = 13,
        Deprecated2 = 14,
        Fish = 15,
        Birds = 16,
        Bugs = 17,
        PlaceableObject = 18,
        Milestone = 19,
        ItemObject = 20,
        client_npc = 21,
        cooking = 22,
        weapon_craft = 23,
        armor_craft = 24,
        leatherwork = 25,
        tailoring = 26,
        handiwork = 27,
        alchemy = 28,
        carpentry = 29,
        Emblem = 30,
        AbyssDoor = 31,
        AbyssArtifacts = 32,
        AbyssShield = 33,
        Abyss_pvpeffect = 34,
        ExtendedEntity = 35,
        AbyssCarrier = 36,
        AbyssControlTower = 37,
        Milestone_Ndist = 38,
        AbyssCarrir_Submesh = 39,
        Chair = 40,
        DirectPortalEff = 41,
        FindHelper = 42,
        Opaque = 43,
        JumpTrigger = 44,
        Pet = 46,
        Mercenary = 47,
        ToyPet = 48,
        HousingBuilding = 49,
        Deprecated3 = 50,
        HousingInterior = 51,
        HousingBuildingIndoor = 52,
        Deprecated4 = 53,
        HousingVisualEffect = 54,
        HousingDoor = 55,
        NPCSearchBoard = 56,
        WorldEventEntity = 57,
        WindPath = 58,
        menuisier = 59,
        UiAxis = 60,
        MercenaryTroop = 61,
        TownObject = 62,
    }

    public enum eClass
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
        Songweaver,
        WTF
    }

    public enum eWeaponStyle
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

    public enum eRace
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

    public class fAttitude
    {
        public static fAttitude Passive = new fAttitude(0);
        public static fAttitude NoCombat = new fAttitude(2);
        public static fAttitude Hostile = new fAttitude(8);
        public static fAttitude NoFight = new fAttitude(10);
        public static fAttitude Friendly = new fAttitude(38);
        public static fAttitude Utility = new fAttitude(294);

        public static explicit operator fAttitude(int b)  // explicit byte to digit conversion operator
        {
            fAttitude d = new fAttitude(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fAttitude).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fAttitude p = obj as fAttitude;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fAttitude t1, fAttitude t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fAttitude t1, fAttitude t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fAttitude t1, fAttitude t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fAttitude t1, fAttitude t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fAttitude(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fStance
    {
        public static fStance Normal = new fStance(0);
        public static fStance Combat = new fStance(1);
        public static fStance Monture = new fStance(2);
        public static fStance Resting = new fStance(3);
        public static fStance Flying = new fStance(4);
        public static fStance FlyingCombat = new fStance(5);
        public static fStance Monture2 = new fStance(7);
        public static fStance Dead = new fStance(10);
        public static fStance Gliding = new fStance(-1);

        public static explicit operator fStance(int b)  // explicit byte to digit conversion operator
        {
            fStance d = new fStance(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fStance).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fStance p = obj as fStance;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fStance t1, fStance t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fStance t1, fStance t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fStance t1, fStance t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fStance t1, fStance t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fStance(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fMouseAction
    {
        public static fMouseAction None = new fMouseAction(0);
        public static fMouseAction Object = new fMouseAction(7);
        public static fMouseAction NotObject = new fMouseAction(8);
        public static fMouseAction Attackable = new fMouseAction(13);
        public static fMouseAction NotAttackable = new fMouseAction(14);
        public static fMouseAction Chat = new fMouseAction(15);
        public static fMouseAction Vendor = new fMouseAction(17);
        public static fMouseAction Low_Gatherable = new fMouseAction(22);
        public static fMouseAction Low_NotGatherable = new fMouseAction(23);
        public static fMouseAction Lootable = new fMouseAction(40);
        public static fMouseAction NotLootable = new fMouseAction(41);
        public static fMouseAction Gatherable = new fMouseAction(43);
        public static fMouseAction NotGatherable = new fMouseAction(44);

        public static explicit operator fMouseAction(int b)  // explicit byte to digit conversion operator
        {
            fMouseAction d = new fMouseAction(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fMouseAction).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fMouseAction p = obj as fMouseAction;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fMouseAction t1, fMouseAction t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fMouseAction t1, fMouseAction t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fMouseAction t1, fMouseAction t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fMouseAction t1, fMouseAction t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fMouseAction(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fAction
    {
        public static fAction None = new fAction(0);
        public static fAction Attack = new fAction(1);
        public static fAction Cast = new fAction(2);
        public static fAction Talking = new fAction(3);
        public static fAction Gather = new fAction(4);
        public static fAction Follow = new fAction(6);
        public static fAction MoveForward = new fAction(7);
        public static fAction MoveBackward = new fAction(8);
        public static fAction MoveForwardCombat = new fAction(9);
        public static fAction MoveBackwardCombat = new fAction(10);
        public static fAction ToggleCombat = new fAction(13);
        public static fAction RestSit = new fAction(14);
        public static fAction RestStand = new fAction(15);
        public static fAction Emote = new fAction(18);
        public static fAction FlightTakeOff = new fAction(19);
        public static fAction FlightLand = new fAction(20);
        public static fAction Loot = new fAction(21);
        public static fAction FaceTarget = new fAction(22);

        public static explicit operator fAction(int b)  // explicit byte to digit conversion operator
        {
            fAction d = new fAction(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fAction).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fAction p = obj as fAction;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fAction t1, fAction t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fAction t1, fAction t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fAction t1, fAction t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fAction t1, fAction t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fAction(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fIdTypeNPC
    {
        public static fIdTypeNPC LegionWarehouse = new fIdTypeNPC(350409);
        public static fIdTypeNPC StigmaMaster = new fIdTypeNPC(350410);
        public static fIdTypeNPC Warehouse = new fIdTypeNPC(350417);
        public static fIdTypeNPC Middleman = new fIdTypeNPC(350419);
        public static fIdTypeNPC FlightTeleport = new fIdTypeNPC(350420);
        public static fIdTypeNPC Teleport = new fIdTypeNPC(350423);
        public static fIdTypeNPC CoinVendorStart = new fIdTypeNPC(357632);
        public static fIdTypeNPC CoinVendorEnd = new fIdTypeNPC(357715);

        public static explicit operator fIdTypeNPC(int b)  // explicit byte to digit conversion operator
        {
            fIdTypeNPC d = new fIdTypeNPC(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fIdTypeNPC).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fIdTypeNPC p = obj as fIdTypeNPC;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fIdTypeNPC t1, fIdTypeNPC t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fIdTypeNPC t1, fIdTypeNPC t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fIdTypeNPC t1, fIdTypeNPC t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fIdTypeNPC t1, fIdTypeNPC t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fIdTypeNPC(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fIdTypeQuest
    {
        public static fIdTypeQuest Normal_Quest = new fIdTypeQuest(0);
        public static fIdTypeQuest Blue_Quest = new fIdTypeQuest(1);
        public static fIdTypeQuest Purple_Quest = new fIdTypeQuest(2);
        public static fIdTypeQuest Mission_Quest = new fIdTypeQuest(3);
        public static fIdTypeQuest Red_Quest = new fIdTypeQuest(4);

        public static explicit operator fIdTypeQuest(int b)  // explicit byte to digit conversion operator
        {
            fIdTypeQuest d = new fIdTypeQuest(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fIdTypeQuest).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fIdTypeQuest p = obj as fIdTypeQuest;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fIdTypeQuest t1, fIdTypeQuest t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fIdTypeQuest t1, fIdTypeQuest t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fIdTypeQuest t1, fIdTypeQuest t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fIdTypeQuest t1, fIdTypeQuest t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fIdTypeQuest(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }

    public class fIdProgressQuest
    {
        public static fIdProgressQuest Start_Quest = new fIdProgressQuest(3);
        public static fIdProgressQuest Inter_Quest = new fIdProgressQuest(7);
        public static fIdProgressQuest Final_Quest = new fIdProgressQuest(11);

        public static explicit operator fIdProgressQuest(int b)  // explicit byte to digit conversion operator
        {
            fIdProgressQuest d = new fIdProgressQuest(b);  // explicit conversion
            return d;
        }

        public override string ToString()
        {
            foreach (System.Reflection.FieldInfo field in typeof(fIdProgressQuest).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                if (field.GetValue(null).GetHashCode() == this.Value)
                    return field.Name;
            }
            return string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            fIdProgressQuest p = obj as fIdProgressQuest;
            if ((object)p == null)
                return false;

            return (this.Value == p.Value);
        }

        public static bool operator ==(fIdProgressQuest t1, fIdProgressQuest t2)
        {
            return t1.Value == t2.Value;
        }

        public static bool operator !=(fIdProgressQuest t1, fIdProgressQuest t2)
        {
            return t1.Value != t2.Value;
        }

        public static bool operator <=(fIdProgressQuest t1, fIdProgressQuest t2)
        {
            return t1.Value <= t2.Value;
        }

        public static bool operator >=(fIdProgressQuest t1, fIdProgressQuest t2)
        {
            return t1.Value >= t2.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public fIdProgressQuest(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }
}
