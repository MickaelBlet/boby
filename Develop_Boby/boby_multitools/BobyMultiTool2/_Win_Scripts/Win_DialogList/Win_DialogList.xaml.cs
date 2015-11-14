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
using System.Windows.Threading;
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
    public partial class Win_DialogList : Window
    {
        private bool atClear = false;
        public Win_DialogList in_DialogList;
        public Style Style_ShugoLoading = null;

        DispatcherTimer dispatcherTimer;

        public Win_DialogList()
        {
            InitializeComponent();
            Style_ShugoLoading = this.FindResource("Style_ShugoLoading") as Style;
            in_DialogList = this;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(_Sequence);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        void _Sequence(object sender, EventArgs e)
        {
            if (!this.IsVisible)
                return;
            TreeViewItem item = (TreeViewItem)tvDialog.SelectedItem;
            if (item == null)
                return;
            item.Items.Clear();
            ariane.Text = GetFullPath(item);
            Aion_Game.Dialog dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
            item.Header = dlg.Name + " (" + dlg.Left.ToString("N0") + ";" + dlg.Top.ToString("N0") + ")";
            dlg.UpdateChild();
            foreach (var child in dlg.Childs.Values)
                item.Items.Add(creatTree(child));
            btShow.Content = dlg.Open ? "Hide" : "Show";
            btEnable.Content = dlg.Enable ? "Disable" : "Enable";
            btClick.IsEnabled = dlg.Enable && dlg.Open;
        }

        public void Update()
        {
            tvDialog.Opacity = 0;
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Style = Style_ShugoLoading;
            img_ShugoLoading.Visibility = Visibility.Visible;
            pgLoading.Value = 0;
            pgLoading.Visibility = Visibility.Visible;
            atClear = true;
            tvDialog.Items.Clear();
            atClear = false;

            IOrderedEnumerable<KeyValuePair<string, Aion_Game.Dialog>> sortedDict = null;
            sortedDict = from entry in Aion_Game.DialogList.List orderby entry.Value.Name ascending select entry;

            double percentUnit = 100d / Aion_Game.DialogList.List.Count;

            foreach (var dialog_Item in sortedDict)
            {
                Aion_Game.Dialog dialog = (Aion_Game.Dialog)dialog_Item.Value;
                TreeViewItem treeItem = creatTree(dialog);
                tvDialog.Items.Add(treeItem);
                pgLoading.Value = pgLoading.Value + percentUnit;
                System.Windows.Forms.Application.DoEvents();
            }
            tvDialog.Opacity = 1;
            img_ShugoLoading.Style = null;
            img_ShugoLoading.Visibility = Visibility.Hidden;
            pgLoading.Visibility = Visibility.Hidden;
        }

        private TreeViewItem creatTree(Aion_Game.Dialog dlg)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = dlg.Name;
            dlg.UpdateChild();
            if (dlg.Childs.Count > 0)
                treeItem.Items.Add(null);
            return treeItem;
        }
        private TreeViewItem creatTreeWhitoutChild(Aion_Game.Dialog dlg)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = dlg.Name;
            return treeItem;
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void bt_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void tvDialog_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            if (!atClear)
            {
                if (e.OldValue != null)
                {
                    TreeViewItem itemold = (TreeViewItem)e.OldValue;
                    itemold.Header = itemold.Header.ToString().Split('(')[0].Trim();
                }
                TreeViewItem item = (TreeViewItem)e.NewValue;
                item.Items.Clear();
                ariane.Text = GetFullPath((TreeViewItem)e.NewValue);
                Aion_Game.Dialog dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
                item.Header = dlg.Name + " (" + dlg.Left.ToString("N0") + ";" + dlg.Top.ToString("N0") + ")";
                dlg.UpdateChild();
                foreach (var child in dlg.Childs.Values)
                    item.Items.Add(creatTree(child));
                btShow.Content = dlg.Open ? "Hide" : "Show";
                btEnable.Content = dlg.Enable ? "Disable" : "Enable";
                btClick.IsEnabled = dlg.Enable && dlg.Open;
            }
            else
                ariane.Text = string.Empty;
        }

        static TreeViewItem GetParentItem(TreeViewItem item)
        {
            for (var i = VisualTreeHelper.GetParent(item); i != null; i = VisualTreeHelper.GetParent(i))
                if (i is TreeViewItem)
                    return (TreeViewItem)i;

            return null;
        }

        public string GetFullPath(TreeViewItem node)
        {
            var result = Convert.ToString(node.Header).Split('(')[0].Trim();
            for (var i = GetParentItem(node); i != null; i = GetParentItem(i))
                result = i.Header + ">" + result;

            return result;
        }

        private void btClick_Click(object sender, RoutedEventArgs e)
        {
            Aion_Game.Dialog dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
            dlg.Click();
            dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
            btShow.Content = dlg.Open ? "Hide" : "Show";
            btEnable.Content = dlg.Enable ? "Disable" : "Enable";
            btClick.IsEnabled = dlg.Enable && dlg.Open;
        }

        private void btShow_Click(object sender, RoutedEventArgs e)
        {
            Aion_Game.Dialog dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
            dlg.Open = !dlg.Open;
            btShow.Content = dlg.Open ? "Hide" : "Show";
            btClick.IsEnabled = dlg.Enable && dlg.Open;
        }

        private void btEnable_Click(object sender, RoutedEventArgs e)
        {
            Aion_Game.Dialog dlg = Aion_Game.DialogList.GetDialog(ariane.Text);
            dlg.Enable = !dlg.Enable;
            btEnable.Content = dlg.Enable ? "Disable" : "Enable";
            btClick.IsEnabled = dlg.Enable && dlg.Open;
        }

        private void btClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("DialogList.GetDialog(\"" + ariane.Text + "\")");
        }
    }
}
