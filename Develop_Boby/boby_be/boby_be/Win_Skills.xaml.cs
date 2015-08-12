/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/13/2014
 * Heure: 13:01
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using System.Net;

using System.Threading;
using System.Threading.Tasks;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Interaction logic for Win_Skills.xaml
	/// </summary>
	public partial class Win_Skills : Window
	{
		public SkillsCollection skills;
		
		public Win_Skills()
		{
			InitializeComponent();
			
			comboboxColumn.ItemsSource  = new List<string>() { "OnFight", "OffFight", "AllTime", "Item" };
			
			skills = (SkillsCollection)Resources["PersonCollection"];

			try
			{
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\Skills_List\\";
				string filename = (EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) + ".xml";
				
				skills.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(SkillsCollection));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Skills> personData = (IEnumerable<Skills>)serializer.Deserialize(stream);

					foreach (Skills p in personData)
					{
						skills.Add(p);
					}
				}
			}
			catch
			{
				if (""+(EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) == "Cleric")
				{
					using(WebClient Client = new WebClient())
					{
						Client.Proxy = null;
						Client.DownloadFile(new Uri("http://www.aht.li/2297181/Cleric.xml"),@"Skills_List\Cleric.xml");
					}
				}
				else if (""+(EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) == "Chanter")
				{
					using(WebClient Client = new WebClient())
					{
						Client.Proxy = null;
						Client.DownloadFile(new Uri("http://www.aht.li/2297182/Chanter.xml"),@"Skills_List\Chanter.xml");
					}
				}
				else if (""+(EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) == "Gunner")
				{
					using(WebClient Client = new WebClient())
					{
						Client.Proxy = null;
						Client.DownloadFile(new Uri("http://www.aht.li/2297183/Gunner.xml"),@"Skills_List\Gunner.xml");
					}
				}
				else
				{
					skills.Add(new Skills{Name = "Seau sans fond", HP = 32, MP = 32, Type = "Item"});
					skills.Add(new Skills{Name = "Panacée de mana excellente", HP = 0, MP = 0, Type = "Item"});
					skills.Add(new Skills{Name = "Elixir de mana excellent", HP = 0, MP = 0, Type = "Item"});
					skills.Add(new Skills{Name = "Panacée de vie excellente", HP = 0, MP = 0, Type = "Item"});
					skills.Add(new Skills{Name = "Elixir de vie excellent", HP = 0, MP = 0, Type = "Item"});
					Save_List();
				}
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\Skills_List\\";
				string filename = (EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) + ".xml";
				
				skills.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(SkillsCollection));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Skills> personData = (IEnumerable<Skills>)serializer.Deserialize(stream);

					foreach (Skills p in personData)
					{
						skills.Add(p);
					}
				}
			}
		}
		
		public void Save_List()
		{
			string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\Skills_List\\";
			string filename = (EnumAion.AionClasses)SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Class) + ".xml";
			
			XmlSerializer serializer = new XmlSerializer(typeof(SkillsCollection));

			using (FileStream stream = new FileStream(appPath+filename, FileMode.Create))
			{
				serializer.Serialize(stream, skills);
			}
		}
		
		void Select_Last()
		{
			if (dataGrid.Items.Count > 0)
			{
				var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
				if (border != null)
				{
					var scroll = border.Child as ScrollViewer;
					if (scroll != null) scroll.ScrollToEnd();
				}
				dataGrid.SelectedItem = dataGrid.Items[dataGrid.Items.Count-1];
			}
		}
		
		void Button_Add(object sender, RoutedEventArgs e)
		{
			skills.Add(new Skills{Name = ""});
			Select_Last();
		}
		
		void Button_Del(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid.SelectedIndex >= 0)
				{
					Skills tmpbuff = dataGrid.SelectedItem as Skills;
					skills.Remove(tmpbuff);
				}
			}
			catch (Exception)
			{
				
			}
		}
		
		void Button_Up(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid.SelectedItem != null && dataGrid.Items.IndexOf(dataGrid.SelectedItem)>0)
					skills.Move(dataGrid.Items.IndexOf(dataGrid.SelectedItem), dataGrid.Items.IndexOf(dataGrid.SelectedItem)-1);
			}
			catch (Exception)
			{
				
			}
		}
		
		void Button_Down(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid.SelectedItem != null && dataGrid.Items.IndexOf(dataGrid.SelectedItem)<dataGrid.Items.Count-1)
					skills.Move(dataGrid.Items.IndexOf(dataGrid.SelectedItem), dataGrid.Items.IndexOf(dataGrid.SelectedItem)+1);
			}
			catch (Exception)
			{
				
			}
		}
		
		void Listview_Skill_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listview_Skill.SelectedItem != null)
			{
				string name = listview_Skill.SelectedItem.ToString();
				skills.Add(new Skills{Name = name});
				Select_Last();
			}
		}
		
		void Listview_Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listview_Item.SelectedItem != null)
			{
				string name = listview_Item.SelectedItem.ToString();
				skills.Add(new Skills{Name = name});
				Select_Last();
			}
		}
	}
	
	public class Skills : INotifyPropertyChanged
	{
		private string name;
		private int hp;
		private int mp;
		private string type;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyPropertyChanged("Name");
			}
		}
		
		public int HP
		{
			get { return hp; }
			set
			{
				hp = value;
				NotifyPropertyChanged("HP");
			}
		}
		
		public int MP
		{
			get { return mp; }
			set
			{
				mp = value;
				NotifyPropertyChanged("MP");
			}
		}
		
		public string Type
		{
			get { return type; }
			set
			{
				type = value;
				NotifyPropertyChanged("Type");
			}
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Serializable]
	public class SkillsCollection : ObservableCollection<Skills>
	{
	}
}