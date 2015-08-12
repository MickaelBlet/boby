/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 22/06/2013
 * Heure: 15:26
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.IO;
using System.Xml;
using System.Globalization;

using AddProcess;

namespace BOBY_Shulack
{

	public class Settings
	{
		public Main inMain = null;
		//public Options[] inOptions = { null, null, null };
		private string sConfigFile = null;
		private static XmlDocument xmlDoc = null;
		private static XmlNode node = null;
		private static string xPath = null;
		public bool bOn = false;

		public Settings()
		{
			sConfigFile = Convert.ToString(Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location)) + "\\config.xml";
			xmlDoc = new XmlDocument();
			inMain = new Main();
			Load();
		}

		public class Main
		{
			public int posxViewer = 0;
			public int posyViewer = 0;

			public void Load()
			{
				#region Load Main

				xPath = "/Boby/Config/Main" + "/posxViewer";
				node = xmlDoc.SelectSingleNode(xPath);
				this.posxViewer = Convert.ToInt32(node.InnerText);

				xPath = "/Boby/Config/Main" + "/posyViewer";
				node = xmlDoc.SelectSingleNode(xPath);
				this.posyViewer = Convert.ToInt32(node.InnerText);

				#endregion
			}

			public void Save()
			{
				#region Save Main
				
				xPath = "/Boby/Config/Main" + "/posxViewer";
				node = xmlDoc.SelectSingleNode(xPath);
				node.InnerText = this.posxViewer.ToString();

				xPath = "/Boby/Config/Main" + "/posyViewer";
				node = xmlDoc.SelectSingleNode(xPath);
				node.InnerText = this.posyViewer.ToString();

				#endregion
			}
		}

		private void OpenXML()
		{
			if (!(File.Exists(sConfigFile)))
				MakeXML();
			else
				xmlDoc.Load(sConfigFile);
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
			#region Make <Config>
			New = xmlDoc.CreateElement("Config");
			Root.AppendChild(New);
			#region Make <Main>
			New = xmlDoc.CreateElement("Main");
			Root.LastChild.AppendChild(New);

			New = xmlDoc.CreateElement("posxViewer");
			value = xmlDoc.CreateTextNode(inMain.posxViewer.ToString());
			Root.LastChild.LastChild.AppendChild(New);
			Root.LastChild.LastChild.LastChild.AppendChild(value);
			
			New = xmlDoc.CreateElement("posyViewer");
			value = xmlDoc.CreateTextNode(inMain.posyViewer.ToString());
			Root.LastChild.LastChild.AppendChild(New);
			Root.LastChild.LastChild.LastChild.AppendChild(value);

			#endregion
			#endregion
			CloseXML();
		}

		private void CloseXML()
		{
			xmlDoc.Save(sConfigFile);
		}

		public void Load()
		{
			try
			{
				OpenXML();
				this.inMain.Load();
			}
			catch (Exception)
			{
				CloseXML();
				File.Delete(Convert.ToString(Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location)) + "\\config.xml");
				OpenXML();
			}
		}

		public void Save()
		{
			OpenXML();
			this.inMain.Save();
			CloseXML();
		}
	}
}
