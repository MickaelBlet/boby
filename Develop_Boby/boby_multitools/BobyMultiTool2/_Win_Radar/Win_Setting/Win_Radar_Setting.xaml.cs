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

using System.Threading;
using System.Threading.Tasks;

using NS_Aion_Game;
using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_Radar_Setting : Window
    {
        public Win_Main in_Win_Main = null;
        public Hashtable Image_File_Mini = null;
        public Hashtable Image_File_Real = null;

        public IconCollection icon_collect;
        public IconSaveCollection icon_save_collect;
        public BuffCollection buff_collect;

        public Win_Radar_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            Image img = new Image();

            ImageSource[] Image_File = null;

            Image_File_Mini = new Hashtable();
            Image_File_Real = new Hashtable();
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\RadarIcon\";

                string[] files = Directory.GetFiles(appPath, "*.png");

                if (files.Length > 0)
                {
                    Image_File = new ImageSource[files.Length];
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filesreal = files[i];
                        BitmapImage source = new BitmapImage();
                        source.BeginInit();
                        source.UriSource = new Uri(filesreal);
                        source.EndInit();
                        if (source.Width > 20 || source.Height > 20)
                        {
                            source = new BitmapImage();
                            source.BeginInit();
                            source.UriSource = new Uri(filesreal);
                            source.DecodePixelHeight = 20;
                            source.DecodePixelWidth = 20;
                            source.EndInit();
                        }
                        Image_File[i] = source;
                        Image_File_Mini.Add(source.ToString(), source);
                        Image_File_Real.Add(source.ToString(), new BitmapImage(new Uri(filesreal)));
                    }
                }
            }
            catch (Exception)
            { }

            lb_Items.ItemsSource = Image_File;

            icon_collect = (IconCollection)Resources["PersonCollection"];

            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Icon_list.xml";

                if (File.Exists(filePath))
                {
                    icon_collect.Clear();

                    XmlSerializer serializer = new XmlSerializer(typeof(IconSaveCollection));

                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        IEnumerable<Icon_Save> personData = (IEnumerable<Icon_Save>)serializer.Deserialize(stream);

                        foreach (Icon_Save p in personData)
                        {
                            icon_collect.Add(new Icon { IMG_PATH = p.IMG_PATH, Name = p.Name, SCALE = p.SCALE, AGGRO_SCALE = p.AGGRO_SCALE });
                        }
                    }

                    foreach (Icon p in icon_collect)
                    {
                        try
                        {
                            string r = p.IMG_PATH;
                            p.IMG_SRC_MINI = (ImageSource)Image_File_Mini[r];
                            p.IMG_SRC = (ImageSource)Image_File_Real[r];
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }

            sl_zoom.Value = (int)((in_Win_Main.in_Setting.in_Radar.Zoom.Get_Value() * 100 - 150) / 2f);
            lb_zoom.Content = sl_zoom.Value.ToString() + " %";

            sl_icon_size.Value = (int)((in_Win_Main.in_Setting.in_Radar.Size.Get_Value() * 100 - 91));
            lb_icon_size.Content = sl_icon_size.Value.ToString() + " %";

            sl_size.Value = (int)((in_Win_Main.in_Setting.in_Radar.Radar_Width.Get_Value() - 199) / 2f);
            lb_size.Content = sl_size.Value.ToString() + " %";
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.CancelEdit();
            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Icon_list.xml";

                icon_save_collect = new IconSaveCollection();

                foreach (Icon p in icon_collect)
                    icon_save_collect.Add(new Icon_Save { IMG_PATH = p.IMG_PATH, Name = p.Name, SCALE = p.SCALE, AGGRO_SCALE = p.AGGRO_SCALE });

                XmlSerializer serializer = new XmlSerializer(typeof(IconSaveCollection));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, icon_save_collect);
                }
            }
            catch
            {
            }
            this.Hide();
        }

        private void Rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMoveFDAup(object sender, RoutedEventArgs e)
        {
            Button btn = ((Button)sender);
            if (dataGrid.SelectedItem != null)
            {
                Icon tmp = dataGrid.SelectedItem as Icon;
                tmp.IMG_SRC = Image_File_Real[btn.Content.ToString()] as ImageSource;
                tmp.IMG_SRC_MINI = Image_File_Mini[btn.Content.ToString()] as ImageSource;
                tmp.IMG_PATH = btn.Content.ToString();
            }
        }

        private void scale_plus_Click(object sender, RoutedEventArgs e)
        {
            Icon tmp = dataGrid.SelectedItem as Icon;
            tmp.SCALE = tmp.SCALE + 0.1f;
        }

        private void scale_moin_Click(object sender, RoutedEventArgs e)
        {
            Icon tmp = dataGrid.SelectedItem as Icon;
            if (tmp.SCALE > 0)
                tmp.SCALE = tmp.SCALE - 0.1f;
        }

        private void aggro_scale_plus_Click(object sender, RoutedEventArgs e)
        {
            Icon tmp = dataGrid.SelectedItem as Icon;
            tmp.AGGRO_SCALE = tmp.AGGRO_SCALE + 1f;
        }

        private void aggro_scale_moin_Click(object sender, RoutedEventArgs e)
        {
            Icon tmp = dataGrid.SelectedItem as Icon;
            if (tmp.AGGRO_SCALE > 0)
                tmp.AGGRO_SCALE = tmp.AGGRO_SCALE - 1f;
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            icon_collect.Add(new Icon { IMG_SRC = null, IMG_PATH = "", Name = "", SCALE = 1, AGGRO_SCALE = 0 });
            Select_Last();
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
                    Icon tmp = dataGrid.SelectedItem as Icon;
                    icon_collect.Remove(tmp);
                }
            }
            catch (Exception)
            {

            }
        }

        private void target_Click(object sender, RoutedEventArgs e)
        {
            Icon icon = new Icon { IMG_SRC = null, Name = "", SCALE = 1, AGGRO_SCALE = 0 };
            icon.Name = Get_Target_Name();
            icon_collect.Add(icon);
            Select_Last();
        }

        private string Get_Target_Name()
        {
            string name = "";
            int ID = 0;

            foreach (var entity in in_Win_Main.in_Thread_Entity.DicCopy.Values)
            {
                if (entity.Type == EnumAion.eType.Player)
                {
                    ID = entity.TargetID;
                    break ;
                }
            }
            foreach (var entity in in_Win_Main.in_Thread_Entity.DicCopy.Values)
            {
                if (entity.ID == ID)
                {
                    name = entity.Name;
                    break;
                }
            }

            return name;
        }

        private void TextBox_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Icon tmp = dataGrid.SelectedItem as Icon;
                    tmp.Name = txb.Text;
                }
            }
            catch { }
        }

        private void TextBox_scale_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Icon tmp = dataGrid.SelectedItem as Icon;
                    string txt = txb.Text.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    tmp.SCALE = Convert.ToSingle(txt);
                }
            }
            catch { }
        }

        private void TextBox_aggro_scale_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = ((TextBox)sender);
                if (txb.IsSelectionActive == true)
                {
                    Icon tmp = dataGrid.SelectedItem as Icon;
                    tmp.AGGRO_SCALE = Convert.ToSingle(txb.Text);
                }
            }
            catch { }
        }

        private void sl_zoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Radar.Zoom.Set_Value((float)(sl_zoom.Value * 2 + 150) / 100f);
            lb_zoom.Content = sl_zoom.Value.ToString() + " %";
        }

        private void sl_icon_size_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Radar.Size.Set_Value((float)(sl_icon_size.Value * 1 + 91) / 100f);
            lb_icon_size.Content = sl_icon_size.Value.ToString() + " %";
        }

        private void sl_size_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Radar.Radar_Width.Set_Value((int)((sl_size.Value * 2 + 199)));
            in_Win_Main.in_Setting.in_Radar.Radar_Height.Set_Value((int)((sl_size.Value * 2 + 199)));
            lb_size.Content = sl_size.Value.ToString() + " %";

            in_Win_Main.in_Win_Radar.Width = in_Win_Main.in_Setting.in_Radar.Radar_Width.Get_Value();
            in_Win_Main.in_Win_Radar.Height = in_Win_Main.in_Setting.in_Radar.Radar_Height.Get_Value();
            in_Win_Main.in_Win_Radar.Canvas_Radar.Width = in_Win_Main.in_Setting.in_Radar.Radar_Width.Get_Value();
            in_Win_Main.in_Win_Radar.Canvas_Radar.Height = in_Win_Main.in_Setting.in_Radar.Radar_Height.Get_Value();
            in_Win_Main.in_Win_Radar.BG.Width = in_Win_Main.in_Setting.in_Radar.Radar_Width.Get_Value();
            in_Win_Main.in_Win_Radar.BG.Height = in_Win_Main.in_Setting.in_Radar.Radar_Height.Get_Value();
            in_Win_Main.in_Win_Radar.View.Width = in_Win_Main.in_Setting.in_Radar.Radar_Width.Get_Value();
            in_Win_Main.in_Win_Radar.View.Height = in_Win_Main.in_Setting.in_Radar.Radar_Height.Get_Value();
            in_Win_Main.in_Win_Radar.BG_Opa.Viewport = new Rect(0, 0, in_Win_Main.in_Win_Radar.Canvas_Radar.Width, in_Win_Main.in_Win_Radar.Canvas_Radar.Height);
            in_Win_Main.in_Win_Radar.BG_Mask.Viewport = new Rect(0, 0, in_Win_Main.in_Win_Radar.Canvas_Radar.Width, in_Win_Main.in_Win_Radar.Canvas_Radar.Height);

            in_Win_Main.in_Win_Radar.button_Setting.Margin = new Thickness(0, 0, sl_size.Value / 2.5 + 30, sl_size.Value / 2.5 + 10);
        }
    }

    public class Icon : System.ComponentModel.INotifyPropertyChanged
    {
        private ImageSource img_src_mini;
        private ImageSource img_src;
        private string Image_path;
        private string name;
        private float scale;
        private float aggro_scale;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public ImageSource IMG_SRC_MINI
        {
            get { return img_src_mini; }
            set
            {
                img_src_mini = value;
                NotifyPropertyChanged("IMG_SRC_MINI");
            }
        }

        public ImageSource IMG_SRC
        {
            get { return img_src; }
            set
            {
                img_src = value;
                NotifyPropertyChanged("IMG_SRC");
            }
        }

        public string IMG_PATH
        {
            get { return Image_path; }
            set
            {
                Image_path = value;
                NotifyPropertyChanged("IMG_PATH");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public float SCALE
        {
            get { return scale; }
            set
            {
                scale = value;
                NotifyPropertyChanged("SCALE");
            }
        }

        public float AGGRO_SCALE
        {
            get { return aggro_scale; }
            set
            {
                aggro_scale = value;
                NotifyPropertyChanged("AGGRO_SCALE");
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

    public class Icon_Save
    {
        private string Image_path;
        private string name;
        private float scale;
        private float aggro_scale;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public string IMG_PATH
        {
            get { return Image_path; }
            set
            {
                Image_path = value;
                NotifyPropertyChanged("IMG_PATH");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public float SCALE
        {
            get { return scale; }
            set
            {
                scale = value;
                NotifyPropertyChanged("SCALE");
            }
        }

        public float AGGRO_SCALE
        {
            get { return aggro_scale; }
            set
            {
                aggro_scale = value;
                NotifyPropertyChanged("AGGRO_SCALE");
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
    public class IconSaveCollection : System.Collections.ObjectModel.ObservableCollection<Icon_Save>
    {
    }

    [Serializable]
    public class IconCollection : System.Collections.ObjectModel.ObservableCollection<Icon>
    {
    }
}