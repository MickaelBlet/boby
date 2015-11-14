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
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices; // DllImport

using System.Threading;
using System.Threading.Tasks;

using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    public partial class Win_AbilityList : Window
    {
        private bool atClear = false;
        public Win_AbilityList in_SkillList;
        public Style Style_ShugoLoading = null;

        public Hashtable File_img;
        public Hashtable File_source;

        public Win_AbilityList()
        {
            InitializeComponent();
            Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;
            in_SkillList = this;

            File_img = new Hashtable();
            File_source = new Hashtable();
            /*string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\skills\";

            string[] files = Directory.GetFiles(appPath, "*.png");
            Hashtable File_img_png = new Hashtable();

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
            }*/

            string uri = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\skills\boby_client_skills.xml"; // your big XML file

            foreach (var buff in XmlHelper.StreamBooks(uri))
            {
                File_img.Add(buff.Id, buff.File);
            }
        }

        public void Update()
        {
            string dir_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            lbSkills.Opacity = 0;
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Style = Style_ShugoLoading;
            img_ShugoLoading.Visibility = Visibility.Visible;
            pgLoading.Value = 0;
            pgLoading.Visibility = Visibility.Visible;
            lbSkills.Items.Clear();

            IOrderedEnumerable<KeyValuePair<string, Aion_Game.Ability>> sortedDict = null;
            sortedDict = from entry in Aion_Game.AbilityList.MinList orderby entry.Value.Name ascending select entry;

            double percentUnit = 100d / Aion_Game.AbilityList.MinList.Count;

            foreach (var item in sortedDict)
            {
                Aion_Game.Ability ability = item.Value;
                if (ability.Name == "")
                    continue;
                ImageSource img = null;
                if (File_img.ContainsKey(ability.Id.ToString()))
                {
                    string img_name = File_img[ability.Id.ToString()].ToString();
                    if (File_source.ContainsKey(img_name))
                    {
                        img = (ImageSource)File_source[img_name];
                    }
                    else if (File.Exists(dir_path + @"\skills\" + img_name + ".png"))
                    {
                        BitmapImage logo = new BitmapImage();
                        logo.BeginInit();
                        logo.UriSource = new Uri(dir_path + @"\skills\" + img_name + ".png");
                        logo.EndInit();
                        img = (ImageSource)logo;
                        File_source.Add(img_name, img);
                    }
                }
                lbSkills.Items.Add(new Ability_View { Image = img, Id = ability.Id, Name = ability.Name });
                pgLoading.Value = pgLoading.Value + percentUnit;
                System.Windows.Forms.Application.DoEvents();
            }
            lbSkills.Opacity = 1;
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

        private void btClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (lbSkills.SelectedItem != null)
            {
                Ability_View ab = (Ability_View)lbSkills.SelectedItem;
                Clipboard.SetText("AbilityList.GetAbility(" + ab.Id + ") //" + ab.Name);
            }
        }

        private void lbSkills_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lbSkills.SelectedItem != null)
            {
                Ability_View ab = (Ability_View)lbSkills.SelectedItem;
                Aion_Game.Chat.Send("/skill " + ab.Name);
            }
        }
    }
    public class Ability_View
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lvl { get; set; }
        public ImageSource Image { get; set; }

        public ImageSource graph_ability { get; set; }

        public Ability_View()
        {
        }
    }
}
