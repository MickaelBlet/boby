using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobyMultitools
{
    public class Boby
    {
        public static Win_Main Win_Main;
        public static Win_Radar Win_Radar;
        public static Win_Entity Win_Entity;
        public static Win_Cheat Win_Cheat;
        public static Win_Script Win_Script;

        public Boby()
        {
            Win_Main = new Win_Main();
        }
    }
}
