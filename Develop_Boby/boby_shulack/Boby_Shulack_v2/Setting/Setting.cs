
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Globalization;
using System.IO.Compression;

using System.ComponentModel;

namespace Boby_Shulack
{
	/// <summary>
	/// Description of Setting.
	/// </summary>
	public class Setting
	{
        private static Win_Main in_Win_Main = null;

		public Cheat	in_Cheat	= null;
		public CheatKey	in_CheatKey	= null;
		public Entity	in_Entity	= null;
		public Radar	in_Radar	= null;
        public Buff		in_Buff		= null;
        public Quick	in_Quick	= null;

		private 		string		sConfigFile	= null;
		public static	XmlDocument	xmlDoc		= null;
		
		public static	Setting		in_Setting	= null;

        public Setting(Win_Main tmp_Win_Main)
		{
            in_Win_Main = tmp_Win_Main;
			sConfigFile	= Convert.ToString(Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location)) + "\\Boby_Multitools_Setting.xml";
			xmlDoc		= new XmlDocument();
			in_Cheat	= new Cheat();
			in_CheatKey = new CheatKey();
			in_Entity	= new Entity();
			in_Radar	= new Radar();
            in_Buff     = new Buff();
            in_Quick    = new Quick();
			in_Setting	= this;
			Load();
		}
		
		#region ### class ###
		public class Cheat
		{
			public Bool_To_XML	Show;
			public Int_To_XML	Left;
			public Int_To_XML	Top;
			
			public Int_To_XML	Attack_Speed;
			public Int_To_XML	Move_Speed;
			public Int_To_XML	Acc_Distance;
			public Int_To_XML	Sup_Distance;
			public Bool_To_XML	Safety;
			public Bool_To_XML	NoGrav;
			public Bool_To_XML	ZLock;
			public Bool_To_XML	Key;
			
			public Cheat()
			{
				string Where	= "/Boby/Cheat/";
				
				Show			= new Bool_To_XML(xmlDoc, Where, "Show", true);
				Left			= new Int_To_XML(xmlDoc, Where, "Left", 0);
				Top				= new Int_To_XML(xmlDoc, Where, "Top", 190);
				
				Attack_Speed	= new Int_To_XML(xmlDoc, Where, "Attack_Speed", 0);
				Move_Speed		= new Int_To_XML(xmlDoc, Where, "Move_Speed", 0);
				Acc_Distance	= new Int_To_XML(xmlDoc, Where, "Acc_Distance", 24);
				Sup_Distance	= new Int_To_XML(xmlDoc, Where, "Sup_Distance", 49);
				Safety			= new Bool_To_XML(xmlDoc, Where, "Safety", false);
				NoGrav			= new Bool_To_XML(xmlDoc, Where, "NoGrav", false);
				ZLock			= new Bool_To_XML(xmlDoc, Where, "ZLock", false);
				Key				= new Bool_To_XML(xmlDoc, Where, "Key", false);
			}
			
			public void Reset()
			{
				foreach (FieldInfo field in typeof(Cheat).GetFields())
				{
					object value_type = typeof(Cheat).GetField(field.Name).GetValue(in_Setting.in_Cheat);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Reset();
					}
				}
			}
			
			public void Load()
			{
				foreach (FieldInfo field in typeof(Cheat).GetFields())
				{
					object value_type = typeof(Cheat).GetField(field.Name).GetValue(in_Setting.in_Cheat);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Load();
					}
				}
			}

			public void Save()
			{
				foreach (FieldInfo field in typeof(Cheat).GetFields())
				{
					object value_type = typeof(Cheat).GetField(field.Name).GetValue(in_Setting.in_Cheat);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Save();
					}
				}
			}
		}
		
		public class CheatKey
		{
			public Int_To_XML	keyNoGrav;
			public Int_To_XML	keyZLock;
			public Int_To_XML	keyToKey;
			public Int_To_XML	keyAccFor;
			public Int_To_XML	keyAccUp;
			public Int_To_XML	keyAccDown;
			public Int_To_XML	keySupFor;
			public Int_To_XML	keySupUp;
			public Int_To_XML	keySupDown;
			public Int_To_XML	modifierNoGrav;
			public Int_To_XML	modifierZLock;
			public Int_To_XML	modifierToKey;
			public Int_To_XML	modifierAccFor;
			public Int_To_XML	modifierAccUp;
			public Int_To_XML	modifierAccDown;
			public Int_To_XML	modifierSupFor;
			public Int_To_XML	modifierSupUp;
			public Int_To_XML	modifierSupDown;
			
			public CheatKey()
			{
				string Where	= "/Boby/CheatKey/";
				
				keyNoGrav		= new Int_To_XML(xmlDoc, Where, "keyNoGrav", 0);
				keyZLock		= new Int_To_XML(xmlDoc, Where, "keyZLock", 0);
				keyToKey		= new Int_To_XML(xmlDoc, Where, "keyToKey", 0);
				keyAccFor		= new Int_To_XML(xmlDoc, Where, "keyAccFor", 0);
				keyAccUp		= new Int_To_XML(xmlDoc, Where, "keyAccUp", 0);
				keyAccDown		= new Int_To_XML(xmlDoc, Where, "keyAccDown", 0);
				keySupFor		= new Int_To_XML(xmlDoc, Where, "keySupFor", 0);
				keySupUp		= new Int_To_XML(xmlDoc, Where, "keySupUp", 0);
				keySupDown		= new Int_To_XML(xmlDoc, Where, "keySupDown", 0);
				modifierNoGrav	= new Int_To_XML(xmlDoc, Where, "modifierNoGrav", 0);
				modifierZLock	= new Int_To_XML(xmlDoc, Where, "modifierZLock", 0);
				modifierToKey	= new Int_To_XML(xmlDoc, Where, "modifierToKey", 0);
				modifierAccFor	= new Int_To_XML(xmlDoc, Where, "modifierAccFor", 0);
				modifierAccUp	= new Int_To_XML(xmlDoc, Where, "modifierAccUp", 0);
				modifierAccDown	= new Int_To_XML(xmlDoc, Where, "modifierAccDown", 0);
				modifierSupFor	= new Int_To_XML(xmlDoc, Where, "modifierSupFor", 0);
				modifierSupUp	= new Int_To_XML(xmlDoc, Where, "modifierSupUp", 0);
				modifierSupDown	= new Int_To_XML(xmlDoc, Where, "modifierSupDown", 0);
			}
			
			public void Reset()
			{
				foreach (FieldInfo field in typeof(CheatKey).GetFields())
				{
					object value_type = typeof(CheatKey).GetField(field.Name).GetValue(in_Setting.in_CheatKey);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Reset();
					}
				}
			}
			
			public void Load()
			{
				foreach (FieldInfo field in typeof(CheatKey).GetFields())
				{
					object value_type = typeof(CheatKey).GetField(field.Name).GetValue(in_Setting.in_CheatKey);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Load();
					}
				}
			}

			public void Save()
			{
				foreach (FieldInfo field in typeof(CheatKey).GetFields())
				{
					object value_type = typeof(CheatKey).GetField(field.Name).GetValue(in_Setting.in_CheatKey);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Save();
					}
				}
			}
		}
		
		public class Entity
		{
            public string           Where = "";

			public Bool_To_XML		Show;
			public Int_To_XML		Left;
            public Int_To_XML		Top;
            public Int_To_XML		Height;
            public Int_To_XML		Width;
			
			public Bool_To_XML		NPC;
			public Bool_To_XML		Ally;
			public Bool_To_XML		Hostile;
			public Bool_To_XML		Gather;
			public String_To_XML	Order;
			
			public Entity()
			{
				string Where	= "/Boby/Entity/";
				
				Show	= new Bool_To_XML(xmlDoc, Where, "Show", true);
				Left	= new Int_To_XML(xmlDoc, Where, "Left", 0);
				Top		= new Int_To_XML(xmlDoc, Where, "Top", 0);
                Height = new Int_To_XML(xmlDoc, Where, "Height", 189);
                Width = new Int_To_XML(xmlDoc, Where, "Width", 409);
				
				NPC		= new Bool_To_XML(xmlDoc, Where, "NPC", true);
				Ally	= new Bool_To_XML(xmlDoc, Where, "Ally", true);
				Hostile	= new Bool_To_XML(xmlDoc, Where, "Hostile", true);
				Gather	= new Bool_To_XML(xmlDoc, Where, "Gather", true);
				Order	= new String_To_XML(xmlDoc, Where, "Order", "Rnk");
			}

			public void Reset()
			{
				foreach (FieldInfo field in typeof(Entity).GetFields())
				{
					object value_type = typeof(Entity).GetField(field.Name).GetValue(in_Setting.in_Entity);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Reset();
					}
				}
			}
			
			public void Load()
			{
				foreach (FieldInfo field in typeof(Entity).GetFields())
				{
					object value_type = typeof(Entity).GetField(field.Name).GetValue(in_Setting.in_Entity);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Load();
					}
				}
			}

			public void Save()
			{
				foreach (FieldInfo field in typeof(Entity).GetFields())
				{
					object value_type = typeof(Entity).GetField(field.Name).GetValue(in_Setting.in_Entity);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Save();
					}
				}
			}
		}
		
		public class Radar
		{
			public Bool_To_XML		Show;
			public Int_To_XML		Left;
			public Int_To_XML		Top;
			
			public Bool_To_XML		Nord;
			public Bool_To_XML		NPC;
			public Bool_To_XML		Ally;
			public Bool_To_XML		Hostile;
			public Bool_To_XML		Gather;
			
			public Bool_To_XML		BGon;
			public Bool_To_XML		Mapon;
			public Bool_To_XML		Overlay;
			public Bool_To_XML		IconPlus;
			
			public Float_To_XML		Zoom;
			public Float_To_XML		Size;
			public Int_To_XML		Width;
			public Int_To_XML		Height;
			
			public Float_To_XML		Radar_X;
			public Float_To_XML		Radar_Y;
			public Int_To_XML		Radar_Width;
			public Int_To_XML		Radar_Height;
			
			public Radar()
			{
				string Where	= "/Boby/Radar/";
				
				Show		= new Bool_To_XML(xmlDoc, Where, "Show", true);
				Left		= new Int_To_XML(xmlDoc, Where, "Left", 400);
				Top			= new Int_To_XML(xmlDoc, Where, "Top", 0);
				
				Nord		= new Bool_To_XML(xmlDoc, Where, "Nord", false);
				NPC			= new Bool_To_XML(xmlDoc, Where, "NPC", true);
				Ally		= new Bool_To_XML(xmlDoc, Where, "Ally", true);
				Hostile		= new Bool_To_XML(xmlDoc, Where, "Hostile", true);
				Gather		= new Bool_To_XML(xmlDoc, Where, "Gather", true);

                BGon        = new Bool_To_XML(xmlDoc, Where, "BGon", false);
				Mapon		= new Bool_To_XML(xmlDoc, Where, "Mapon", false);
				Overlay		= new Bool_To_XML(xmlDoc, Where, "Overlay", false);
				IconPlus	= new Bool_To_XML(xmlDoc, Where, "IconPlus", true);
				
				Zoom		= new Float_To_XML(xmlDoc, Where, "Zoom", 1.80f);
				Size		= new Float_To_XML(xmlDoc, Where, "Size", 1.00f);
				Width		= new Int_To_XML(xmlDoc, Where, "Width", 199);
				Height		= new Int_To_XML(xmlDoc, Where, "Height", 199);
				
				Radar_X		= new Float_To_XML(xmlDoc, Where, "Radar_X", 8f);
				Radar_Y		= new Float_To_XML(xmlDoc, Where, "Radar_Y", 7f);
				Radar_Width	= new Int_To_XML(xmlDoc, Where, "Radar_Width", 175);
				Radar_Height= new Int_To_XML(xmlDoc, Where, "Radar_Height", 175);
			}

			public void Reset()
			{
				foreach (FieldInfo field in typeof(Radar).GetFields())
				{
					object value_type = typeof(Radar).GetField(field.Name).GetValue(in_Setting.in_Radar);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Reset();
					}
				}
			}
			
			public void Load()
			{
				foreach (FieldInfo field in typeof(Radar).GetFields())
				{
					object value_type = typeof(Radar).GetField(field.Name).GetValue(in_Setting.in_Radar);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Load();
					}
				}
			}

			public void Save()
			{
				foreach (FieldInfo field in typeof(Radar).GetFields())
				{
					object value_type = typeof(Radar).GetField(field.Name).GetValue(in_Setting.in_Radar);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Save();
					}
				}
			}
		}
		
		public class Buff
		{
			public Bool_To_XML		Show;
			public Int_To_XML		Left;
			public Int_To_XML		Top;
			
			public String_To_XML	File;
			
			public Buff()
			{
				string Where	= "/Boby/Buff/";
				
				Show	= new Bool_To_XML(xmlDoc, Where, "Show", true);
				Left	= new Int_To_XML(xmlDoc, Where, "Left", 0);
				Top		= new Int_To_XML(xmlDoc, Where, "Top", 300);
				
				File	= new String_To_XML(xmlDoc, Where, "File", "");
			}

			public void Reset()
			{
				foreach (FieldInfo field in typeof(Buff).GetFields())
				{
					object value_type = typeof(Buff).GetField(field.Name).GetValue(in_Setting.in_Buff);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Reset();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Reset();
					}
				}
			}
			
			public void Load()
			{
				foreach (FieldInfo field in typeof(Buff).GetFields())
				{
					object value_type = typeof(Buff).GetField(field.Name).GetValue(in_Setting.in_Buff);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Load();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Load();
					}
				}
			}

			public void Save()
			{
				foreach (FieldInfo field in typeof(Buff).GetFields())
				{
					object value_type = typeof(Buff).GetField(field.Name).GetValue(in_Setting.in_Buff);
					if (value_type.GetType() == typeof(Bool_To_XML))
					{
						var tmp = (Bool_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Int_To_XML))
					{
						var tmp = (Int_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(Float_To_XML))
					{
						var tmp = (Float_To_XML)value_type;
						tmp.Save();
					}
					if (value_type.GetType() == typeof(String_To_XML))
					{
						var tmp = (String_To_XML)value_type;
						tmp.Save();
					}
				}
			}
		}

        public class Quick
        {
            public Bool_To_XML		Show;
            public Int_To_XML		Left;
            public Int_To_XML		Top;

            public String_To_XML	File;

            public Quick()
            {
                string Where	= "/Boby/Quick/";

                Show = new Bool_To_XML(xmlDoc, Where, "Show", true);
                Left = new Int_To_XML(xmlDoc, Where, "Left", 0);
                Top = new Int_To_XML(xmlDoc, Where, "Top", 370);

                File = new String_To_XML(xmlDoc, Where, "File", "");
            }

            public void Reset()
            {
                foreach (FieldInfo field in typeof(Quick).GetFields())
                {
                    object value_type = typeof(Quick).GetField(field.Name).GetValue(in_Setting.in_Quick);
                    if (value_type.GetType() == typeof(Bool_To_XML))
                    {
                        var tmp = (Bool_To_XML)value_type;
                        tmp.Reset();
                    }
                    if (value_type.GetType() == typeof(Int_To_XML))
                    {
                        var tmp = (Int_To_XML)value_type;
                        tmp.Reset();
                    }
                    if (value_type.GetType() == typeof(Float_To_XML))
                    {
                        var tmp = (Float_To_XML)value_type;
                        tmp.Reset();
                    }
                    if (value_type.GetType() == typeof(String_To_XML))
                    {
                        var tmp = (String_To_XML)value_type;
                        tmp.Reset();
                    }
                }
            }

            public void Load()
            {
                foreach (FieldInfo field in typeof(Quick).GetFields())
                {
                    object value_type = typeof(Quick).GetField(field.Name).GetValue(in_Setting.in_Quick);
                    if (value_type.GetType() == typeof(Bool_To_XML))
                    {
                        var tmp = (Bool_To_XML)value_type;
                        tmp.Load();
                    }
                    if (value_type.GetType() == typeof(Int_To_XML))
                    {
                        var tmp = (Int_To_XML)value_type;
                        tmp.Load();
                    }
                    if (value_type.GetType() == typeof(Float_To_XML))
                    {
                        var tmp = (Float_To_XML)value_type;
                        tmp.Load();
                    }
                    if (value_type.GetType() == typeof(String_To_XML))
                    {
                        var tmp = (String_To_XML)value_type;
                        tmp.Load();
                    }
                }
            }

            public void Save()
            {
                foreach (FieldInfo field in typeof(Quick).GetFields())
                {
                    object value_type = typeof(Quick).GetField(field.Name).GetValue(in_Setting.in_Quick);
                    if (value_type.GetType() == typeof(Bool_To_XML))
                    {
                        var tmp = (Bool_To_XML)value_type;
                        tmp.Save();
                    }
                    if (value_type.GetType() == typeof(Int_To_XML))
                    {
                        var tmp = (Int_To_XML)value_type;
                        tmp.Save();
                    }
                    if (value_type.GetType() == typeof(Float_To_XML))
                    {
                        var tmp = (Float_To_XML)value_type;
                        tmp.Save();
                    }
                    if (value_type.GetType() == typeof(String_To_XML))
                    {
                        var tmp = (String_To_XML)value_type;
                        tmp.Save();
                    }
                }
            }
        }
		#endregion

		private void OpenXML()
		{
            if (!(File.Exists(sConfigFile)))
            {
                this.MakeXML();

                client_DownloadBase();
            }
            else
                xmlDoc.Load(sConfigFile);
		}
		
		private XmlText Check_Type_To_XmlText(object value_type)
		{
			if (value_type.GetType() == typeof(Bool_To_XML))
			{
				var tmp = (Bool_To_XML)value_type;
				return(xmlDoc.CreateTextNode(tmp.Get_Value().ToString()));
			}
			if (value_type.GetType() == typeof(Int_To_XML))
			{
				var tmp = (Int_To_XML)value_type;
				return(xmlDoc.CreateTextNode(tmp.Get_Value().ToString()));
			}
			if (value_type.GetType() == typeof(Float_To_XML))
			{
				var tmp = (Float_To_XML)value_type;
				return(xmlDoc.CreateTextNode(tmp.Get_Value().ToString()));
			}
			if (value_type.GetType() == typeof(String_To_XML))
			{
				var tmp = (String_To_XML)value_type;
				return(xmlDoc.CreateTextNode(tmp.Get_Value().ToString()));
			}
			return(null);
		}
		
		private void MakeXML()
		{
			XmlElement Root;
			XmlElement New;
			XmlText value;
			//if file is not found, create a new xml file
			XmlTextWriter xmlWriter = new XmlTextWriter(sConfigFile, System.Text.Encoding.UTF8);
			xmlWriter.Formatting = Formatting.Indented;
			xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
			xmlWriter.WriteStartElement("Boby");
			xmlWriter.Close();
			xmlDoc.Load(sConfigFile);
			Root = xmlDoc.DocumentElement;

			New = xmlDoc.CreateElement("Cheat");
			Root.AppendChild(New);
			
			foreach (FieldInfo field in typeof(Cheat).GetFields())
			{
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(Cheat).GetField(field.Name).GetValue(in_Cheat);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
			}
			
			New = xmlDoc.CreateElement("CheatKey");
			Root.AppendChild(New);
			
			foreach (FieldInfo field in typeof(CheatKey).GetFields())
			{
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(CheatKey).GetField(field.Name).GetValue(in_CheatKey);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
			}
			
			New = xmlDoc.CreateElement("Entity");
			Root.AppendChild(New);
			
			foreach (FieldInfo field in typeof(Entity).GetFields())
			{
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(Entity).GetField(field.Name).GetValue(in_Entity);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
			}
			
			New = xmlDoc.CreateElement("Radar");
			Root.AppendChild(New);
			
			foreach (FieldInfo field in typeof(Radar).GetFields())
			{
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(Radar).GetField(field.Name).GetValue(in_Radar);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
			}
			
			New = xmlDoc.CreateElement("Buff");
			Root.AppendChild(New);
			
			foreach (FieldInfo field in typeof(Buff).GetFields())
			{
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(Buff).GetField(field.Name).GetValue(in_Buff);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
			}

            New = xmlDoc.CreateElement("Quick");
            Root.AppendChild(New);

            foreach (FieldInfo field in typeof(Quick).GetFields())
            {
                try
                {
                    New = xmlDoc.CreateElement(field.Name);
                    object value_type = typeof(Quick).GetField(field.Name).GetValue(in_Quick);
                    value = Check_Type_To_XmlText(value_type);
                    Root.LastChild.AppendChild(New);
                    Root.LastChild.LastChild.AppendChild(value);
                }
                catch { }
            }

			this.CloseXML();
		}
		
		private void CloseXML()
		{
			xmlDoc.Save(sConfigFile);
		}
		
		public void Reset()
		{
			this.in_Cheat.Reset();
			this.in_CheatKey.Reset();
			this.in_Entity.Reset();
			this.in_Radar.Reset();
			this.in_Buff.Reset();
            this.in_Quick.Reset();
        }

		public void Load()
		{
			try
			{
				this.OpenXML();
				this.in_Cheat.Load();
				this.in_CheatKey.Load();
				this.in_Entity.Load();
				this.in_Radar.Load();
				this.in_Buff.Load();
                this.in_Quick.Load();
            }
			catch (Exception)
			{
				this.Reset();
				File.Delete(this.sConfigFile);
                this.OpenXML();
				this.Load();

                client_DownloadBase();

			}
		}

		public void Save()
		{
			this.OpenXML();
			this.in_Cheat.Save();
			this.in_CheatKey.Save();
			this.in_Entity.Save();
			this.in_Radar.Save();
            this.in_Buff.Save();
            this.in_Quick.Save();
			this.CloseXML();
		}

        void client_DownloadBase()
        {
            try
            {
                in_Win_Main.pg_ShugoLoading.Opacity = 1;
                System.Windows.MessageBox.Show(in_Win_Main, "Download base config auto (25Mo)", "Info");

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    Client.DownloadFileAsync(new Uri("http://aion-add-file.url.ph/" + "Boby_Multitools_Install.zip"), @".\Boby_Multitools_Install.zip");
                }
            }
            catch { }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            in_Win_Main.pg_ShugoLoading.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            in_Win_Main.pg_ShugoLoading.Value = 100;

            using (ZipArchive modFile = ZipFile.Open(@".\Boby_Multitools_Install.zip", ZipArchiveMode.Read))
            {
                ZipArchiveExtensions.ExtractToDirectory(modFile, ".", true);
            }

            File.Delete(@".\Boby_Multitools_Install.zip");

            in_Win_Main.pg_ShugoLoading.Opacity = 0;
        }        
	}

    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
