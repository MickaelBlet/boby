using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices; // DllImport

using System.Threading;
using System.Threading.Tasks;

using NS_Aion_Game;
using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_Quick_Setting : Window
    {
        public IconCollection icon_collect;
        public IconSaveCollection icon_save_collect;
        public QuickCollection quick_collect;

        public static Win_Main in_Win_Main = null;

        public Hashtable File_img_png;
        public Hashtable File_img;
        public bool is_read_file = false;

        public Win_Quick_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            quick_collect = (QuickCollection)Resources["PersonQuickCollection"];

            tFile_PreviewMouseLeftButtonDown(null, null);

            string[] ssplit = in_Win_Main.in_Setting.in_Quick.File.Get_Value().Split('\\');
            tFile.SelectedItem = ssplit[ssplit.Length - 1];
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Quick tmp = dataGrid.SelectedItem as Quick;
                    tmp.TYPE = txb.Text;
                }
            }
            catch { }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Quick tmp = dataGrid.SelectedItem as Quick;
                    tmp.ID = int.Parse(txb.Text);
                }
            }
            catch { }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Quick tmp = dataGrid.SelectedItem as Quick;
                    tmp.TYPE = txb.Text;
                }
            }
            catch { }
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
                dataGrid.SelectedItem = dataGrid.Items[dataGrid.Items.Count - 1];
            }
        }

        private void moin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex >= 0)
                {
                    Quick tmp = dataGrid.SelectedItem as Quick;
                    quick_collect.Remove(tmp);
                }
            }
            catch (Exception)
            {

            }
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            quick_collect.Clear();
            quick_collect.Add(new Quick { ID = 0, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 1, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 2, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 3, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 4, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 5, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 6, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 7, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 8, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
            quick_collect.Add(new Quick { ID = 9, TYPE = "None", COMBO = new List<string>() { "None", "Hand", "Sword", "Dagger", "Mace", "Key", "Polearm", "Staff", "Greatsword", "Harp", "Orb", "Book", "Pistol", "Cannon", "Bow" } });
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\QuickFiles";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = " |*.xml"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;

                    quick_collect.Clear();

                    XmlSerializer serializer = new XmlSerializer(typeof(QuickCollection));
                    using (FileStream stream = new FileStream(filename, FileMode.Open))
                    {
                        IEnumerable<Quick> personData = (IEnumerable<Quick>)serializer.Deserialize(stream);

                        foreach (Quick p in personData)
                        {
                            quick_collect.Add(p);
                        }
                    }

                    in_Win_Main.in_Setting.in_Quick.File.Set_Value(filename);

                    tFile_PreviewMouseLeftButtonDown(null, null);

                    string[] ssplit = in_Win_Main.in_Setting.in_Quick.File.Get_Value().Split('\\');
                    tFile.SelectedItem = ssplit[ssplit.Length - 1];
                }
                catch
                {
                }
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\QuickFiles";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = " |*.xml"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;

                    XmlSerializer serializer = new XmlSerializer(typeof(QuickCollection));

                    using (FileStream stream = new FileStream(filename, FileMode.Create))
                    {
                        serializer.Serialize(stream, quick_collect);
                    }

                    in_Win_Main.in_Setting.in_Quick.File.Set_Value(filename);
                }
                catch
                {
                }
            }
        }

        bool InCombo(string filestronc)
        {
            for (int i = 0; i < tFile.Items.Count; i++)
            {
                if (filestronc == tFile.Items[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void tFile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\QuickFiles\";

                string[] files = Directory.GetFiles(appPath, "*.xml");

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filestronc = files[i].Remove(0, appPath.Length);
                        if (!InCombo(filestronc))
                            tFile.Items.Add(filestronc);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void tFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\QuickFiles\";
                string filename = tFile.SelectedValue.ToString();

                quick_collect.Clear();

                XmlSerializer serializer = new XmlSerializer(typeof(QuickCollection));

                using (FileStream stream = new FileStream(appPath + filename, FileMode.Open))
                {
                    IEnumerable<Quick> personData = (IEnumerable<Quick>)serializer.Deserialize(stream);

                    foreach (Quick p in personData)
                    {
                        quick_collect.Add(p);
                    }
                }

                in_Win_Main.in_Setting.in_Quick.File.Set_Value(appPath + filename);

                string[] ssplit = in_Win_Main.in_Setting.in_Quick.File.Get_Value().Split('\\');
                in_Win_Main.in_Win_Quick.tFile.SelectedItem = ssplit[ssplit.Length - 1];
            }
            catch
            { }
        }
    }

    public class Quick : System.ComponentModel.INotifyPropertyChanged
    {
        private int id;
        private string type;
        private List<string> combo;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public string TYPE
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged("TYPE");
            }
        }

        public List<string> COMBO
        {
            get { return combo; }
            set
            {
                combo = value;
                NotifyPropertyChanged("COMBO");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Serializable]
    public class QuickCollection : System.Collections.ObjectModel.ObservableCollection<Quick>
    {
    } 
}
