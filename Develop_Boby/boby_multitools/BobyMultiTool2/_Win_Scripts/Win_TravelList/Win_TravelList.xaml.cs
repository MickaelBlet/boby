using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices; // DllImport
using System.Text.RegularExpressions;

using System.Threading;
using System.Threading.Tasks;

using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_TravelList : Window
    {
        public static TravelCollection travelCollection;
        public Win_TravelList in_DialogList;
        public Style Style_ShugoLoading = null;
        public bool isRecord = false;

        DispatcherTimer messageTimerRecord;
        DispatcherTimer messageTimerRun;

        public Win_TravelList()
        {
            InitializeComponent();
            Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;
            in_DialogList = this;
            travelCollection = (TravelCollection)Resources["TravelCollection"];

            // TIMER
            messageTimerRecord = new DispatcherTimer();
            messageTimerRecord.Tick += new EventHandler(messageTimer_Tick);
            messageTimerRecord.Interval = new TimeSpan(0, 0, 0, 0, 500);

            messageTimerRun = new DispatcherTimer();
            messageTimerRun.Tick += new EventHandler(messageTimerRun_Tick);
            messageTimerRun.Interval = new TimeSpan(0, 0, 0, 0, 100);

            tFile.Items.Add("");
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            Travel last = travelCollection.Last<Travel>();
            if (Aion_Game.Player.GetDistance(last.X, last.Y, last.Z) > 3)
            {
                travelCollection.Add(new Travel { NAME = travelCollection.Count.ToString(), X = Aion_Game.Player.X, Y = Aion_Game.Player.Y, Z = Aion_Game.Player.Z, FLY = Aion_Game.Player.IsFly, TYPE = cDefaultType.Text, COMMENT = "" });
                dgTravel.SelectedIndex = dgTravel.Items.Count - 1;
                dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
            }
        }

        void messageTimerRun_Tick(object sender, EventArgs e)
        {
            if (Aion_Game.Player.Action == (Aion_Game.fAction)0)
                messageTimerRun.Stop();
        }

        public void Update()
        {
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Style = Style_ShugoLoading;
            img_ShugoLoading.Visibility = Visibility.Visible;
            pgLoading.Value = 0;
            pgLoading.Visibility = Visibility.Visible;
           
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Visibility = Visibility.Hidden;
            pgLoading.Visibility = Visibility.Hidden;
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void bt_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBox_Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_X_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = sender as TextBox;
                if (txb.IsSelectionActive == true)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    string txt = txb.Text.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    tmp.X = Convert.ToSingle(txt);
                }
            }
            catch { }
        }

        private void TextBox_Y_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = sender as TextBox;
                if (txb.IsSelectionActive == true)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    string txt = txb.Text.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    tmp.Y = Convert.ToSingle(txt);
                }
            }
            catch { }
        }
        private void TextBox_Z_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txb = sender as TextBox;
                if (txb.IsSelectionActive == true)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    string txt = txb.Text.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    tmp.Z = Convert.ToSingle(txt);
                }
            }
            catch { }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            try
            {
                if (txb != null)
                {
                    if (txb.Text == string.Empty)
                        txb.Text = "0";
                    string txt = txb.Text.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    txb.Text = Convert.ToSingle(txt).ToString("F2").Replace(",", ".");
                }
            }
            catch
            {
                if (txb != null)
                {
                    txb.Text = "0.00";
                }
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            travelCollection.Add(new Travel { NAME = travelCollection.Count.ToString(), X = Aion_Game.Player.X, Y = Aion_Game.Player.Y, Z = Aion_Game.Player.Z, FLY = Aion_Game.Player.IsFly, TYPE = cDefaultType.Text, COMMENT = "" });
            dgTravel.SelectedIndex = dgTravel.Items.Count - 1;
            dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
        }
        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTravel.SelectedIndex >= 0)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    travelCollection.Remove(tmp);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTravel.SelectedIndex > 0)
                {
                    travelCollection.Move(dgTravel.SelectedIndex-1, dgTravel.SelectedIndex);
                    dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTravel.SelectedIndex < dgTravel.Items.Count - 1)
                {
                    travelCollection.Move(dgTravel.SelectedIndex, dgTravel.SelectedIndex + 1);
                    dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTravel.SelectedIndex >= 0)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    Aion_Game.Player.MoveTo(tmp);
                }
            }
            catch { }
        }

        private void btecord_Click(object sender, RoutedEventArgs e)
        {
            isRecord = !isRecord;
            if (isRecord)
            {
                travelCollection.Add(new Travel { NAME = travelCollection.Count.ToString(), X = Aion_Game.Player.X, Y = Aion_Game.Player.Y, Z = Aion_Game.Player.Z, FLY = Aion_Game.Player.IsFly, TYPE = cDefaultType.Text, COMMENT = "" });
                dgTravel.SelectedIndex = dgTravel.Items.Count - 1;
                dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
                messageTimerRecord.Start();
                btRecord.Content = "Stop";
            }
            else
            {
                messageTimerRecord.Stop();
                btRecord.Content = "Record";
            }
        }

        private void btFullRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (dgTravel.SelectedIndex < dgTravel.Items.Count - 1)
                {
                    if (dgTravel.SelectedIndex >= 0)
                    {
                        Travel tmp = dgTravel.SelectedItem as Travel;
                        Aion_Game.Player.MoveTo(tmp);
                        messageTimerRun.Start();
                    }
                    while (messageTimerRun.IsEnabled)
                    {
                        btFullRun.Content = "OnRun";
                        System.Windows.Forms.Application.DoEvents();
                        Thread.Sleep(20);
                    }
                    btFullRun.Content = "Next";
                    dgTravel.SelectedIndex++;
                    dgTravel.ScrollIntoView(dgTravel.Items[dgTravel.SelectedIndex]);
                }
                if (dgTravel.SelectedIndex >= 0)
                {
                    Travel tmp = dgTravel.SelectedItem as Travel;
                    Aion_Game.Player.MoveTo(tmp);
                }
                while (messageTimerRun.IsEnabled)
                {
                    btFullRun.Content = "OnRun";
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(20);
                }
                if (cTravelPath.Name == "Circular")
                {
                    dgTravel.SelectedIndex = 0;
                    btFullRun_Click(null, null);
                }
                else
                {
                    travelCollection.Reverse();
                    btFullRun_Click(null, null);
                }
            }
            catch {}
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            travelCollection.Clear();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\travels";
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = " |*.xml"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    string filename = dlg.FileName;
                    travelCollection.TravelPath = cTravelPath.Text;

                    TravelSave travel_save = new TravelSave();
                    travel_save.TravelPath = travelCollection.TravelPath;
                    travel_save.Travels = travelCollection.ToList<Travel>();

                    XmlSerializer serializer = new XmlSerializer(typeof(TravelSave));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    using (FileStream stream = new FileStream(filename, FileMode.Create))
                    {
                        serializer.Serialize(stream, travel_save, ns);
                    }
                }
                catch
                {
                }
            }
        }

        bool InCombo(string filestronc)
        {
            for (int i = 0; i < tFile.Items.Count; i++)
                if (filestronc == tFile.Items[i].ToString())
                    return true;
            return false;
        }
        private void tFile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\travels\";
                string[] files = Directory.GetFiles(appPath, "*.xml");
                for (int i = 0; i < files.Length; i++)
                {
                    string filestronc = files[i].Remove(0, appPath.Length);
                    if (!InCombo(filestronc))
                        tFile.Items.Add(filestronc);
                }
            }
            catch { }
        }

        private void tFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\travels\";
                string filename = tFile.SelectedValue.ToString();
                travelCollection.Clear();
                XmlSerializer serializer = new XmlSerializer(typeof(TravelSave));
                using (FileStream stream = new FileStream(appPath + filename, FileMode.Open))
                {
                    TravelSave travelData = (TravelSave)serializer.Deserialize(stream);
                    cTravelPath.Text = travelData.TravelPath;
                    foreach (Travel p in travelData.Travels)
                        travelCollection.Add(p);
                }
            }
            catch
            { }
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
    }

    public class Travel
    {
        private string _name;
        private float _x;
        private float _y;
        private float _z;
        private bool _fly;
        private string _type;
        private string _comment;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public string NAME
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("NAME");
            }
        }
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyPropertyChanged("X");
            }
        }

        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                NotifyPropertyChanged("Y");
            }
        }

        public float Z
        {
            get { return _z; }
            set
            {
                _z = value;
                NotifyPropertyChanged("Z");
            }
        }

        public bool FLY
        {
            get { return _fly; }
            set
            {
                _fly = value;
                NotifyPropertyChanged("FLY");
            }
        }
        public string TYPE
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged("TYPE");
            }
        }

        public string COMMENT
        {
            get { return _comment; }
            set
            {
                _comment = value;
                NotifyPropertyChanged("COMMENT");
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

    public class TravelCollection : System.Collections.ObjectModel.ObservableCollection<Travel>
    {
        private string _travel_path = "Circular";
        [XmlAttribute]
        public string TravelPath
        {
            get
            {
                return _travel_path;
            }
            set
            {
                _travel_path = value;
            }
        }
    }

    [Serializable]
    [XmlRoot("TravelList")]
    public class TravelSave
    {
        public string TravelPath = "Circular";
        public List<Travel> Travels = new List<Travel>();
    }
}
