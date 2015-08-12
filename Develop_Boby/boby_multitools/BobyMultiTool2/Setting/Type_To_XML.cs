using System;
using System.IO;
using System.Xml;
using System.Globalization;

namespace BobyMultitools
{
	public class Bool_To_XML
	{
		public bool			Value;
		public bool			ini_Value;
		public string		Name;
		public string		Where;
		private XmlDocument	xmlDoc;
		
		public Bool_To_XML(XmlDocument xml_doc, string xml_path, string xml_name, bool xml_value)
		{
			this.xmlDoc		= xml_doc;
			this.Where		= xml_path;
			this.Name		= xml_name;
			this.Value		= xml_value;
			this.ini_Value	= xml_value;
		}
		
		public void Reset()
		{
			this.Value	= this.ini_Value;
		}
		
		public void Set_Value(bool Value)
		{
			this.Value	= Value;
		}
		
		public bool Get_Value()
		{
			return (this.Value);
		}
		
		public XmlNode Get_Node()
		{
			string		Path	= this.Where + this.Name;
			XmlNode		node	= this.xmlDoc.SelectSingleNode(Path);
			return (node);
		}
		
		public void Load()
		{
			this.Value = Convert.ToBoolean(Get_Node().InnerText);
		}
		
		public void Save()
		{
			Get_Node().InnerText = this.Value.ToString();
		}
	}
	
	public class Int_To_XML
	{
		public int			Value;
		public int			ini_Value;
		public string		Name;
		public string		Where;
		private XmlDocument	xmlDoc;
		
		public Int_To_XML(XmlDocument xml_doc, string xml_path, string xml_name, int xml_value)
		{
			this.xmlDoc		= xml_doc;
			this.Where		= xml_path;
			this.Name		= xml_name;
			this.Value		= xml_value;
			this.ini_Value	= xml_value;
		}
		
		public void Reset()
		{
			this.Value	= this.ini_Value;
		}
		
		public void Set_Value(int Value)
		{
			this.Value	= Value;
		}
		
		public int Get_Value()
		{
			return (this.Value);
		}
		
		public XmlNode Get_Node()
		{
			string		Path	= this.Where + this.Name;
			XmlNode		node	= this.xmlDoc.SelectSingleNode(Path);
			return (node);
		}
		
		public void Load()
		{
			this.Value = Convert.ToInt32(Get_Node().InnerText);
		}
		
		public void Save()
		{
			Get_Node().InnerText = this.Value.ToString();
		}
	}
	
	public class Float_To_XML
	{
		public float		Value		= 0;
		public float		ini_Value	= 0;
		public string		Name		= "";
		public string		Where		= "";
		private XmlDocument	xmlDoc		= null;
		
		public Float_To_XML(XmlDocument xml_doc, string xml_path, string xml_name, float xml_value)
		{
			this.xmlDoc		= xml_doc;
			this.Where		= xml_path;
			this.Name		= xml_name;
			this.Value		= xml_value;
			this.ini_Value	= xml_value;;
		}
		
		public void Reset()
		{
			this.Value	= this.ini_Value;
		}
		
		public void Set_Value(float Value)
		{
			this.Value	= Value;
		}
		
		public float Get_Value()
		{
			return (this.Value);
		}
		
		public XmlNode Get_Node()
		{
			string		Path	= this.Where + this.Name;
			XmlNode		node	= this.xmlDoc.SelectSingleNode(Path);
			return (node);
		}
		
		public void Load()
		{
			this.Value = Convert.ToSingle(Get_Node().InnerText.Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
		}
		
		public void Save()
		{
			Get_Node().InnerText = this.Value.ToString();
		}
	}
	
	public class String_To_XML
	{
		public string		Value		= "";
		public string		ini_Value	= "";
		public string		Name		= "";
		public string		Where		= "";
		private XmlDocument	xmlDoc		= null;
		
		public String_To_XML(XmlDocument xml_doc, string xml_path, string xml_name, string xml_value)
		{
			this.xmlDoc		= xml_doc;
			this.Where		= xml_path;
			this.Name		= xml_name;
			this.Value		= xml_value;
			this.ini_Value	= xml_value;
		}
		
		public void Reset()
		{
			this.Value	= this.ini_Value;
		}
		
		public void Set_Value(string Value)
		{
			this.Value = Value;
		}
		
		public string Get_Value()
		{
			return (this.Value);
		}
		
		public XmlNode Get_Node()
		{
			string		Path	= this.Where + this.Name;
			XmlNode		node	= this.xmlDoc.SelectSingleNode(Path);
			return (node);
		}
		
		public void Load()
		{
			this.Value = Get_Node().InnerText;
		}
		
		public void Save()
		{
			Get_Node().InnerText = this.Value.ToString();
		}
	}
}
