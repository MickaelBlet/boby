using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BobyMultitools;

namespace Aion_Game
{
    class Boby
    {
        // Windows
        //public static Win_Main Win_Main;
        //public static Win_Entity Win_Entity;
        //public static Win_Radar Win_Radar;
        //public static Win_Cheat Win_Cheat;
        //public static Win_Buff Win_Buff;

        // Variable
        public static EntityList EntityList;
        //public static ForceList ForceList;
        //public static DialogList DialogList;
        //public static AbilityList AbilityList;

        public Boby()
        {
            EntityList = new EntityList();
            //Win_Main = new Win_Main();
            //Win_Entity = new Win_Entity();
        }
    }
}
