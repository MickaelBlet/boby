/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/13/2014
 * Heure: 01:31
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
	/// Interaction logic for Win_Loot.xaml
	/// </summary>
	public partial class Win_Loot : Window
	{
		public LootCollection loot;
		public LootCollection2 loot2;
		
		public Win_Loot()
		{
			InitializeComponent();
			
			loot = (LootCollection)Resources["PersonCollection"];
			loot2 = (LootCollection2)Resources["PersonCollection2"];

			try
			{
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\";
				string filename = "Recycle_Loot.xml";
				
				loot.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LootCollection));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Loot> personData = (IEnumerable<Loot>)serializer.Deserialize(stream);

					foreach (Loot p in personData)
					{
						loot.Add(p);
					}
				}
			}
			catch
			{
				using(WebClient Client = new WebClient())
				{
					Client.Proxy = null;
					Client.DownloadFile(new Uri("http://www.aht.li/2292309/Recycle_Loot.xml"), @".\Recycle_Loot.xml");
				}
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\";
				string filename = "Recycle_Loot.xml";
				
				loot.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LootCollection));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Loot> personData = (IEnumerable<Loot>)serializer.Deserialize(stream);

					foreach (Loot p in personData)
					{
						loot.Add(p);
					}
				}
			}
			
			try
			{
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\";
				string filename = "Recycle_Loot_Full.xml";
				
				loot2.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LootCollection2));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Loot> personData = (IEnumerable<Loot>)serializer.Deserialize(stream);

					foreach (Loot p in personData)
					{
						loot2.Add(p);
					}
				}
			}
			catch
			{
				using(WebClient Client = new WebClient())
				{
					Client.Proxy = null;
					Client.DownloadFile(new Uri("http://www.aht.li/2318547/Recycle_Loot_Full.xml"), @".\Recycle_Loot_Full.xml");
				}
				string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"\\";
				string filename = "Recycle_Loot_Full.xml";
				
				loot2.Clear();
				
				XmlSerializer serializer = new XmlSerializer(typeof(LootCollection2));
				
				using (FileStream stream = new FileStream(appPath+filename, FileMode.Open))
				{
					IEnumerable<Loot> personData = (IEnumerable<Loot>)serializer.Deserialize(stream);

					foreach (Loot p in personData)
					{
						loot2.Add(p);
					}
				}
			}
		}
		
		public void Save_List()
		{
			string filename = "Recycle_Loot.xml";
			
			XmlSerializer serializer = new XmlSerializer(typeof(LootCollection));

			using (FileStream stream = new FileStream(filename, FileMode.Create))
			{
				serializer.Serialize(stream, loot);
			}
			
			filename = "Recycle_Loot_Full.xml";
			
			serializer = new XmlSerializer(typeof(LootCollection2));

			using (FileStream stream = new FileStream(filename, FileMode.Create))
			{
				serializer.Serialize(stream, loot2);
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
		
		void Select_Last2()
		{
			if (dataGrid2.Items.Count > 0)
			{
				var border = VisualTreeHelper.GetChild(dataGrid2, 0) as Decorator;
				if (border != null)
				{
					var scroll = border.Child as ScrollViewer;
					if (scroll != null) scroll.ScrollToEnd();
				}
				dataGrid2.SelectedItem = dataGrid2.Items[dataGrid2.Items.Count-1];
			}
		}
		
		void Button_Add(object sender, RoutedEventArgs e)
		{
			loot.Add(new Loot{Name = ""});
			Select_Last();
		}
		
		void Button_Del(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid.SelectedIndex >= 0)
				{
					Loot tmpbuff = dataGrid.SelectedItem as Loot;
					loot.Remove(tmpbuff);
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
					loot.Move(dataGrid.Items.IndexOf(dataGrid.SelectedItem), dataGrid.Items.IndexOf(dataGrid.SelectedItem)-1);
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
					loot.Move(dataGrid.Items.IndexOf(dataGrid.SelectedItem), dataGrid.Items.IndexOf(dataGrid.SelectedItem)+1);
			}
			catch (Exception)
			{
				
			}
		}
		
		void Listview_Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listview_Item.SelectedItem != null)
			{
				string name = listview_Item.SelectedItem.ToString();
				loot.Add(new Loot{Name = name});
				Select_Last();
			}
		}
		
		void Button_Add2(object sender, RoutedEventArgs e)
		{
			loot2.Add(new Loot{Name = ""});
			Select_Last2();
		}
		
		void Button_Del2(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid2.SelectedIndex >= 0)
				{
					Loot tmpbuff = dataGrid2.SelectedItem as Loot;
					loot2.Remove(tmpbuff);
				}
			}
			catch (Exception)
			{
				
			}
		}
		
		void Button_Up2(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid2.SelectedItem != null && dataGrid2.Items.IndexOf(dataGrid2.SelectedItem)>0)
					loot2.Move(dataGrid2.Items.IndexOf(dataGrid2.SelectedItem), dataGrid2.Items.IndexOf(dataGrid2.SelectedItem)-1);
			}
			catch (Exception)
			{
				
			}
		}
		
		void Button_Down2(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dataGrid2.SelectedItem != null && dataGrid2.Items.IndexOf(dataGrid2.SelectedItem)<dataGrid2.Items.Count-1)
					loot2.Move(dataGrid2.Items.IndexOf(dataGrid2.SelectedItem), dataGrid2.Items.IndexOf(dataGrid2.SelectedItem)+1);
			}
			catch (Exception)
			{
				
			}
		}
		
		void Listview_Item_MouseDoubleClick2(object sender, MouseButtonEventArgs e)
		{
			if (listview_Item2.SelectedItem != null)
			{
				string name = listview_Item2.SelectedItem.ToString();
				loot2.Add(new Loot{Name = name});
				Select_Last2();
			}
		}
	}
	
	public class Loot : INotifyPropertyChanged
	{
		private string name;

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

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Serializable]
	public class LootCollection : ObservableCollection<Loot>
	{
	}
	
	[Serializable]
	public class LootCollection2 : ObservableCollection<Loot>
	{
	}
}