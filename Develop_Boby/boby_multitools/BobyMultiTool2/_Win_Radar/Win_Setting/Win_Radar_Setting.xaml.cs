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

using MemoryLib;
using Aion_Game;

namespace BobyMultitools
{
    public partial class Win_Radar_Setting : Window
    {
        public Win_Main in_Win_Main = null;

        public static IconCollection icon_collect;
        public BuffCollection buff_collect;
        public CroppedBitmap[] FileImg;
        public static Hashtable Img_to_id = null;

        public Win_Radar_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            ImageSource GraphicChar = (ImageSource)Application.Current.FindResource("GraphicChar");

            int maxImage = 234;
            FileImg = new CroppedBitmap[maxImage];

            for (int y = 0; y < 17; y++)
            {
                for (int x = 0; x < 14; x++)
                {
                    if (y * 14 + x >= maxImage)
                        break;
                    CroppedBitmap img = new CroppedBitmap((BitmapSource)GraphicChar, new Int32Rect(x*16+x+1, y*16+y+1, 16, 16));
                    FileImg[y * 14 + x] = img;
                }
            }

            Img_to_id = new Hashtable();

            lb_Items.ItemsSource = FileImg;

            int id = 0;
            foreach (var img in FileImg)
            {
                //MessageBox.Show(img);
                if (Img_to_id.ContainsKey(img))
                {
                    id++;
                    continue;
                }
                Img_to_id.Add(img, id);
                id++;
            }

            icon_collect = (IconCollection)Resources["PersonCollection"];

            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_multitools_icons.xml";

                if (File.Exists(filePath))
                {
                    icon_collect.Clear();

                    XmlSerializer serializer = new XmlSerializer(typeof(IconCollection));

                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        IEnumerable<Icon> personData = (IEnumerable<Icon>)serializer.Deserialize(stream);

                        foreach (var p in personData)
                        {
                            icon_collect.Add(new Icon { ID = p.ID, NAME = p.NAME, SCALE = p.SCALE, AGGRO_SCALE = p.AGGRO_SCALE });
                        }
                    }

                    foreach (Icon p in icon_collect)
                    {
                        try
                        {
                            p.IMG_SRC = FileImg[p.ID];
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }

            tZoom.Text = Setting.Boby.Radar.Zoom.ToString("F2");
            tSize.Text = Setting.Boby.Radar.Size.ToString("F2");
            tIcon.Text = Setting.Boby.Radar.IconSize.ToString("F2");
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.CancelEdit();
            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_multitools_icons.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(IconCollection));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, icon_collect, ns);
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
                tmp.ID = (int)Img_to_id[btn.Content];
                tmp.IMG_SRC = FileImg[tmp.ID];
            }
        }

        private void scale_plus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Icon tmp = dataGrid.CurrentItem as Icon;
                tmp.SCALE = tmp.SCALE + 0.1f;
            }
        }

        private void scale_moin_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Icon tmp = dataGrid.CurrentItem as Icon;
                if (tmp.SCALE > 0)
                    tmp.SCALE = tmp.SCALE - 0.1f;
            }
        }

        private void aggro_scale_plus_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Icon tmp = dataGrid.CurrentItem as Icon;
                tmp.AGGRO_SCALE = tmp.AGGRO_SCALE + 1f;
            }
        }

        private void aggro_scale_moin_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Icon tmp = dataGrid.CurrentItem as Icon;
                if (tmp.AGGRO_SCALE > 0)
                    tmp.AGGRO_SCALE = tmp.AGGRO_SCALE - 1f;
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            icon_collect.Add(new Icon { IMG_SRC = FileImg[0], ID = 0, NAME = "", SCALE = 1, AGGRO_SCALE = 0 });
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
            Icon icon = new Icon { IMG_SRC = FileImg[0], ID = 0, NAME = "", SCALE = 1, AGGRO_SCALE = 0 };
            icon.NAME = Get_Target_Name();
            icon_collect.Add(icon);
            Select_Last();
        }

        private string Get_Target_Name()
        {
            string name = "";
            long ID = 0;

            foreach (var entity in EntityList.List.Values)
            {
                if (entity.Type == eType.Player)
                {
                    ID = entity.TargetId;
                    break;
                }
            }

            if (ID != 0)
            {
                foreach (var entity in EntityList.List.Values)
                {
                    if (entity.Id == ID)
                    {
                        name = entity.Name;
                        break;
                    }
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
                    tmp.NAME = txb.Text;
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

        private void btDownZoom_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Zoom -= 0.02;
            tZoom.Text = Setting.Boby.Radar.Zoom.ToString("F2");
        }

        private void btUpZoom_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Zoom += 0.02;
            tZoom.Text = Setting.Boby.Radar.Zoom.ToString("F2");
        }

        private void btDownSize_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Size -= 1;
            tSize.Text = Setting.Boby.Radar.Size.ToString("F2");
        }

        private void btUpSize_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.Size += 1;
            tSize.Text = Setting.Boby.Radar.Size.ToString("F2");
        }

        private void btDownIcon_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.IconSize -= 0.1;
            tIcon.Text = Setting.Boby.Radar.IconSize.ToString("F2");
        }

        private void btUpIcon_Click(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Radar.IconSize += 0.1;
            tIcon.Text = Setting.Boby.Radar.IconSize.ToString("F2");
        }

        private void tZoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Setting.Boby.Radar.Zoom = double.Parse(tZoom.Text);
            }
            catch { }
        }
        private void tSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Setting.Boby.Radar.Size = double.Parse(tSize.Text);
                in_Win_Main.in_Win_Radar.Width = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.Height = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.Canvas_Radar.Width = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.Canvas_Radar.Height = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.BG.Width = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.BG.Height = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.View.Width = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.View.Height = Setting.Boby.Radar.Size;
                in_Win_Main.in_Win_Radar.BG_Opa.Viewport = new Rect(0, 0, in_Win_Main.in_Win_Radar.Canvas_Radar.Width, in_Win_Main.in_Win_Radar.Canvas_Radar.Height);
                in_Win_Main.in_Win_Radar.BG_Mask.Viewport = new Rect(0, 0, in_Win_Main.in_Win_Radar.Canvas_Radar.Width, in_Win_Main.in_Win_Radar.Canvas_Radar.Height);
                in_Win_Main.in_Win_Radar.button_Setting.Margin = new Thickness(Setting.Boby.Radar.Size * 0.64824120603, Setting.Boby.Radar.Size * 0.64824120603, 0, 0);
            }
            catch { }
        }

        private void tIcon_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Setting.Boby.Radar.IconSize = double.Parse(tIcon.Text);
            }
            catch { }
        }
    }

    public class Icon : System.ComponentModel.INotifyPropertyChanged
    {
        private ImageSource img_src;
        private int id;
        private string name;
        private float scale;
        private float aggro_scale;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public ImageSource IMG_SRC
        {
            get { return img_src; }
            set
            {
                img_src = value;
                NotifyPropertyChanged("IMG_SRC");
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public string NAME
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("NAME");
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
    [XmlRoot("Icons")]
    public class IconCollection : System.Collections.ObjectModel.ObservableCollection<Icon>
    {
    }
}