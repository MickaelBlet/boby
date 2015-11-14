using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace BobyMultitools
{
    public static class Setting
    {
        private static string _config_file_name = "boby_multitools_config.xml";
        public static BobySave Boby = new BobySave();

        public static void Load()
        {
            try
            {
                string appPath = App.Path + @"\";
                string filename = _config_file_name;
                XmlSerializer serializer = new XmlSerializer(typeof(BobySave));
                using (FileStream stream = new FileStream(appPath + filename, FileMode.Open))
                {
                    BobySave fileData = (BobySave)serializer.Deserialize(stream);
                    Boby = fileData;
                }
            }
            catch {}
        }
        public static void Save()
        {
            try
            {
                string appPath = App.Path + @"\";
                string filename = _config_file_name;

                XmlSerializer serializer = new XmlSerializer(typeof(BobySave));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (FileStream stream = new FileStream(appPath + filename, FileMode.Create))
                {
                    serializer.Serialize(stream, Boby, ns);
                }
            }
            catch {}
        }

        [XmlRoot("Boby")]
        public class BobySave
        {
            public Cheat Cheat = new Cheat();
            public CheatKey CheatKey = new CheatKey();
            public Entity Entity = new Entity();
            public Radar Radar = new Radar();
            public Scripts Scripts = new Scripts();
        }

        public class Cheat
        {
            [XmlAttribute]
            public bool Show = true;
            [XmlAttribute]
            public double Left = 0;
            [XmlAttribute]
            public double Top = 190;

            public int Attack_Speed = 0;
            public int Move_Speed = 0;
            public double Acc_Distance = 24;
            public double Sup_Distance = 49;

            public bool Safety = false;
            public bool NoGrav = false;
            public bool ZLock = false;
            public bool Key = false;
        }

        public class CheatKey
        {
            public int keyNoGrav = 0;
            public int keyZLock = 0;
            public int keyToKey = 0;
            public int keyAccFor = 0;
            public int keyAccUp = 0;
            public int keyAccDown = 0;
            public int keySupFor = 0;
            public int keySupUp = 0;
            public int keySupDown = 0;

            public int modifierNoGrav = 0;
            public int modifierZLock = 0;
            public int modifierToKey = 0;
            public int modifierAccFor = 0;
            public int modifierAccUp = 0;
            public int modifierAccDown = 0;
            public int modifierSupFor = 0;
            public int modifierSupUp = 0;
            public int modifierSupDown = 0;
        }

        public class Entity
        {
            [XmlIgnore]
            public string Where = string.Empty;
            [XmlAttribute]
            public bool Show = true;
            [XmlAttribute]
            public double Left = 0;
            [XmlAttribute]
            public double Top = 0;
            [XmlAttribute]
            public double Height = 189;
            [XmlAttribute]
            public double Width = 409;

            public bool NPC = true;
            public bool Ally = true;
            public bool Hostile = true;
            public bool Gather = true;
            public string Order = "Rnk";
        }

        public class Radar
        {
            [XmlAttribute]
            public bool Show = true;
            [XmlAttribute]
            public double Left = 400;
            [XmlAttribute]
            public double Top = 0;

            public bool NPC = true;
            public bool Ally = true;
            public bool Hostile = true;
            public bool Gather = true;
            public bool Travel = true;

            public bool North = false;
            public bool BGon = false;
            public bool IconPlus = true;

            public double Zoom = 1.56;
            public double Size = 199;
            public double IconSize = 1;
        }

        public class Scripts
        {
            [XmlAttribute]
            public bool Show = false;
            [XmlAttribute]
            public double Left = 0;
            [XmlAttribute]
            public double Top = 300;

            public string TravelFiles = string.Empty;
        }
    }
}
