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
    public partial class Win_Buff_Setting : Window
    {
        public IconCollection icon_collect;
        public IconSaveCollection icon_save_collect;
        public BuffCollection buff_collect;
        public PlayerBuffCollection player_buff_collect;
        public static Win_Main in_Win_Main = null;

        public Hashtable File_img_png;
        public Hashtable File_img;
        public bool is_read_file = false;

        public Win_Buff_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            buff_collect = (BuffCollection)Resources["PersonBuffCollection"];
            player_buff_collect = (PlayerBuffCollection)Resources["PersonPlayerBuffCollection"];

            File_img = new Hashtable();
            File_img_png = new Hashtable();

            new Thread(() =>
                {
                    try
                    {
                        string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BuffIcon\";

                        string[] files = Directory.GetFiles(appPath, "*.png");

                        if (files.Length > 0)
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                this.Dispatcher.Invoke((Action)(() =>
                                {
                                    string filesreal = files[i];
                                    BitmapImage source = new BitmapImage();
                                    source.BeginInit();
                                    source.UriSource = new Uri(filesreal);
                                    source.DecodePixelHeight = 40;
                                    source.DecodePixelWidth = 40;
                                    source.EndInit();

                                    string[] ssplitmp = filesreal.Split('\\');
                                    string files_without_ext = ssplitmp[ssplitmp.Length - 1].Remove(ssplitmp[ssplitmp.Length - 1].Length - 4, 4);


                                    File_img_png.Add(files_without_ext.ToLower(), source);
                                }
                                ));
                            }
                        }
                        this.Dispatcher.Invoke((Action)(() =>
                        {

                            string uri = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BuffIcon\client_skills.xml"; // your big XML file

                            foreach (var buff in XmlHelper.StreamBooks(uri))
                            {
                                string tmp = buff.File.ToLower();
                                if (tmp.Contains(".dds"))
                                    tmp = tmp.Remove(tmp.Length - 4, 4);
                                if (File_img_png.ContainsKey(tmp))
                                    File_img.Add(buff.Id, (BitmapImage)File_img_png[tmp]);
                            }
                            is_read_file = true;
                        }
                        ));
                    }
                    catch (Exception)
                    { }
                }).Start();

            tFile_PreviewMouseLeftButtonDown(null, null);

            string[] ssplit = in_Win_Main.in_Setting.in_Buff.File.Get_Value().Split('\\');
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
                    Buff tmp = dataGrid.SelectedItem as Buff;
                    tmp.COMMAND = txb.Text;
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
                    Buff tmp = dataGrid.SelectedItem as Buff;
                    tmp.ID = int.Parse(txb.Text);
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
                    Buff tmp = dataGrid.SelectedItem as Buff;
                    buff_collect.Remove(tmp);
                }
            }
            catch (Exception)
            {

            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            buff_collect.Add(new Buff { ID = 0, COMMAND = "", IMG = null });
            Select_Last();
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            buff_collect.Clear();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\BuffFiles";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = " |*.xml"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;

                    buff_collect.Clear();

                    XmlSerializer serializer = new XmlSerializer(typeof(BuffCollection));
                    using (FileStream stream = new FileStream(filename, FileMode.Open))
                    {
                        IEnumerable<Buff> personData = (IEnumerable<Buff>)serializer.Deserialize(stream);

                        foreach (Buff p in personData)
                        {
                            buff_collect.Add(p);
                        }
                    }

                    in_Win_Main.in_Setting.in_Buff.File.Set_Value(filename);

                    tFile_PreviewMouseLeftButtonDown(null, null);

                    string[] ssplit = in_Win_Main.in_Setting.in_Buff.File.Get_Value().Split('\\');
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
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\BuffFiles";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = " |*.xml"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;

                    XmlSerializer serializer = new XmlSerializer(typeof(BuffCollection));

                    using (FileStream stream = new FileStream(filename, FileMode.Create))
                    {
                        serializer.Serialize(stream, buff_collect);
                    }

                    in_Win_Main.in_Setting.in_Buff.File.Set_Value(filename);
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
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BuffFiles\";

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
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BuffFiles\";
                string filename = tFile.SelectedValue.ToString();

                buff_collect.Clear();

                XmlSerializer serializer = new XmlSerializer(typeof(BuffCollection));

                using (FileStream stream = new FileStream(appPath + filename, FileMode.Open))
                {
                    IEnumerable<Buff> personData = (IEnumerable<Buff>)serializer.Deserialize(stream);

                    foreach (Buff p in personData)
                    {
                        buff_collect.Add(p);
                    }
                }

                in_Win_Main.in_Setting.in_Buff.File.Set_Value(appPath + filename);

                string[] ssplit = in_Win_Main.in_Setting.in_Buff.File.Get_Value().Split('\\');
                in_Win_Main.in_Win_Buff.tFile.SelectedItem = ssplit[ssplit.Length - 1];
            }
            catch
            { }
        }

        private void btnMoveFDAup(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Grid grd = (Grid)btn.Parent;
            Label lb = (Label)grd.Children[0];
            buff_collect.Add(new Buff { ID = int.Parse(lb.Content.ToString()), COMMAND = "", IMG = null });
            Select_Last();
        }
    }

    public class Buff : System.ComponentModel.INotifyPropertyChanged
    {
        private int id;
        private string command;
        private ImageSource img;

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

        public string COMMAND
        {
            get { return command; }
            set
            {
                command = value;
                NotifyPropertyChanged("COMMAND");
            }
        }

        public ImageSource IMG
        {
            get { return img; }
            set
            {
                img = value;
                NotifyPropertyChanged("IMG");
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
    public class BuffCollection : System.Collections.ObjectModel.ObservableCollection<Buff>
    {
    }

    [Serializable]
    public class PlayerBuffCollection : System.Collections.ObjectModel.ObservableCollection<Buff>
    {
    }

    public class skill_base_client
    {
        public string Id { get; set; }
        public string File { get; set; }
    }

    public static class XmlHelper
    {
        public static IEnumerable<skill_base_client> StreamBooks(string uri)
        {
            using (XmlReader reader = XmlReader.Create(uri))
            {
                string id = null;
                string file = null;

                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name == "skill_base_client")
                    {
                        while (reader.Read())
                        {
                            if (reader.Name == "id")
                            {
                                id = reader.ReadString();
                                break;
                            }
                        }
                        while (reader.Read())
                        {
                            if (reader.Name == "skillicon_name")
                            {
                                file = reader.ReadString();
                                break;
                            }
                        }
                        yield return new skill_base_client() { Id = id, File = file };
                    }
                }
            }
        }
    }
}
