using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

using System.Runtime.InteropServices;

using MemoryLib;
using Aion_Game;

namespace BobyMultitools
{
    public partial class Win_Entity : Window
    {
        public Win_Main in_Win_Main = null;
        public Win_Entity_Popup in_Win_Entity_Popup = null;

        public Style style_List_Header = null;
        public Style style_List_Header_Green = null;
        public Style style_List_Header_Red = null;

        public bool select_is_ready = true;

        public Win_Entity(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            style_List_Header = (Style)Application.Current.FindResource("List_Header");
            style_List_Header_Green = (Style)Application.Current.FindResource("List_Header_Green");
            style_List_Header_Red = (Style)Application.Current.FindResource("List_Header_Red");

            this.SourceInitialized += new EventHandler(InitializeWindowSource);

            in_Win_Main = tmp_in_Win_Main;
            in_Win_Main.in_Win_Entity = this;

            refresh_style_button();

            in_Win_Entity_Popup = new Win_Entity_Popup();
            in_Win_Entity_Popup.Show();
            in_Win_Entity_Popup.Left = -1000;
            in_Win_Entity_Popup.Top = -1000;

            this.Left = Setting.Boby.Entity.Left;
            this.Top = Setting.Boby.Entity.Top;
            this.Height = Setting.Boby.Entity.Height;
            this.Width = Setting.Boby.Entity.Width;

            this.Check_NPC.IsChecked = Setting.Boby.Entity.NPC;
            this.Check_Ally.IsChecked = Setting.Boby.Entity.Ally;
            this.Check_Ennemy.IsChecked = Setting.Boby.Entity.Hostile;
            this.Check_Gather.IsChecked = Setting.Boby.Entity.Gather;

            if (Setting.Boby.Entity.Show)
                this.Show();

            Entity_List_View();
        }

        #region _Event gui_
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            in_Win_Main.Full_Close();
        }

        private void Bt_Minimise_Click(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Minimized;
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ContextMenu contextMenu = button.ContextMenu;
            contextMenu.Visibility = Visibility.Visible;
            contextMenu.IsOpen = true;
        }

        private void Rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void Check_NPC_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.NPC = true;
        }

        void Check_NPC_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.NPC = false;
        }

        void Check_Ally_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Ally = true;
        }

        void Check_Ally_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Ally = false;
        }

        void Check_Ennemy_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Hostile = true;
        }

        void Check_Ennemy_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Hostile = false;
        }

        void Check_Gather_Checked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Gather = true;
        }

        void Check_Gather_Unchecked(object sender, RoutedEventArgs e)
        {
            Setting.Boby.Entity.Gather = false;
        }

        void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (in_Win_Entity_Popup.IsVisible)
            {
                if (select_is_ready)
                {
                    select_is_ready = false;
                    in_Win_Entity_Popup.Left = -1000;
                    in_Win_Entity_Popup.Top = -1000;
                    Thread Select = new Thread(() => SlashSelect(in_Win_Entity_Popup.entity));
                    Select.Start();
                }
            }
        }

        private void tb_where_TextChanged(object sender, TextChangedEventArgs e)
        {
            Setting.Boby.Entity.Where = tb_where.Text;
        }

        #endregion

        void SlashSelect(Aion_Game.Entity entity)
        {
            entity.Select();
            select_is_ready = true;
        }

        void GridViewRowPresenter_MouseLeave(object sender, MouseEventArgs e)
        {
            in_Win_Entity_Popup.Left = -1000;
            in_Win_Entity_Popup.Top = -1000;
        }

        void GridViewRowPresenter_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Grid grid = (Grid)sender;
                GridViewRowPresenter lvItem = (GridViewRowPresenter)grid.Children[6];
                in_Win_Entity_Popup.PopupContent(((Entity_View)lvItem.Content).Entity_Save);
                in_Win_Entity_Popup.Left = GetMousePosition().X - in_Win_Entity_Popup.Width - 2;
                in_Win_Entity_Popup.Top = GetMousePosition().Y - in_Win_Entity_Popup.Height - 2;
                in_Win_Entity_Popup.Topmost = false;
                in_Win_Entity_Popup.Topmost = true;
            }
            catch { }
        }

        void GridViewRowPresenter_MouseMove(object sender, MouseEventArgs e)
        {
            in_Win_Entity_Popup.Left = GetMousePosition().X - in_Win_Entity_Popup.Width - 2;
            in_Win_Entity_Popup.Top = GetMousePosition().Y - in_Win_Entity_Popup.Height - 2;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource hwndSource;

        private void InitializeWindowSource(object sender, EventArgs e)
        {
            hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
        }

        private Dictionary<ResizeDirection, Cursor> cursors = new Dictionary<ResizeDirection, Cursor>
		{
			{ResizeDirection.Top, Cursors.SizeNS},
			{ResizeDirection.Bottom, Cursors.SizeNS},
			{ResizeDirection.Left, Cursors.SizeWE},
			{ResizeDirection.Right, Cursors.SizeWE},
			{ResizeDirection.TopLeft, Cursors.SizeNWSE},
			{ResizeDirection.BottomRight, Cursors.SizeNWSE},
			{ResizeDirection.TopRight, Cursors.SizeNESW},
			{ResizeDirection.BottomLeft, Cursors.SizeNESW}
		};

        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(hwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        protected void ResizeIfPressed(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = GetDirectionFromName(element.Name);

            this.Cursor = cursors[direction];

            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        private static ResizeDirection GetDirectionFromName(string name)
        {
            //Hack - Assumes the drag handels are all named *DragHandle
            if (name == "BG_Count_Ally"
                || name == "BG_Count_Ennemy"
                || name == "Label_Count_Ally"
                || name == "Label_Count_Ennemy")
            {
                return (ResizeDirection)Enum.Parse(typeof(ResizeDirection), "Bottom");
            }
            else if (name == "Resize_all")
            {
                return (ResizeDirection)Enum.Parse(typeof(ResizeDirection), "BottomRight");
            }
            return (ResizeDirection)0;
        }

        protected void ResetCursor(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;

                //Hack - only reset cursors if the orginal source isn't a draghandle
                if (element != null && !element.Name.Contains("DragHandle"))
                    this.Cursor = Cursors.Arrow;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lv_Entity.Height = this.Height - 64;
            lv_Entity.Width = this.Width - 4;
            BG_Count_Ally.Width = (this.Width - 20) / 2;
            BG_Count_Ennemy.Width = (this.Width - 20) / 2;
            AutoSizeColumns();
            Setting.Boby.Entity.Width = this.Width;
            Setting.Boby.Entity.Height = this.Height;
        }

        public void AutoSizeColumns()
        {
            GridView gv = lv_Entity.View as GridView;
            if (gv != null)
            {
                if (this.Width - 238 > 121)
                {
                    gv.Columns[1].Width = (this.Width - 220) / 2;
                    gv.Columns[2].Width = (this.Width - 220) / 2 + 8;
                    bt_Name.Width = (this.Width - 220) / 2 + 2;
                    bt_Guild.Width = (this.Width - 220) / 2;
                }
                else
                {
                    gv.Columns[1].Width = this.Width - 220 + 8;
                    gv.Columns[2].Width = 0;
                    bt_Name.Width = this.Width - 220 + 2;
                    bt_Guild.Width = 0;
                }
            }
        }

        private void bt_Rnk_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Rnk")
                Setting.Boby.Entity.Order = "/Rnk";
            else
                Setting.Boby.Entity.Order = "Rnk";
            refresh_style_button();
        }

        private void bt_Name_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Name")
                Setting.Boby.Entity.Order = "/Name";
            else
                Setting.Boby.Entity.Order = "Name";
            refresh_style_button();
        }

        private void bt_Guild_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Guild")
                Setting.Boby.Entity.Order = "/Guild";
            else
                Setting.Boby.Entity.Order = "Guild";
            refresh_style_button();
        }

        private void bt_Lvl_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Lvl")
                Setting.Boby.Entity.Order = "/Lvl";
            else
                Setting.Boby.Entity.Order = "Lvl";
            refresh_style_button();
        }

        private void bt_Class_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Class")
                Setting.Boby.Entity.Order = "/Class";
            else
                Setting.Boby.Entity.Order = "Class";
            refresh_style_button();
        }

        private void bt_Hp_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Hp")
                Setting.Boby.Entity.Order = "/Hp";
            else
                Setting.Boby.Entity.Order = "Hp";
            refresh_style_button();
        }

        private void bt_Dst_Click(object sender, RoutedEventArgs e)
        {
            if (Setting.Boby.Entity.Order == "Dst")
                Setting.Boby.Entity.Order = "/Dst";
            else
                Setting.Boby.Entity.Order = "Dst";
            refresh_style_button();
        }

        private void bt_up_Click(object sender, RoutedEventArgs e)
        {
            if (lv_Entity.Items.Count > 0)
                lv_Entity.ScrollIntoView(lv_Entity.Items[0]);
        }

        private void refresh_style_button()
        {
            bt_Rnk.Style = style_List_Header;
            bt_Name.Style = style_List_Header;
            bt_Guild.Style = style_List_Header;
            bt_Lvl.Style = style_List_Header;
            bt_Class.Style = style_List_Header;
            bt_Hp.Style = style_List_Header;
            bt_Dst.Style = style_List_Header;
            if (Setting.Boby.Entity.Order == "/Rnk")
                bt_Rnk.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "Rnk")
                bt_Rnk.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Name")
                bt_Name.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Name")
                bt_Name.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Guild")
                bt_Guild.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Guild")
                bt_Guild.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Lvl")
                bt_Lvl.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Lvl")
                bt_Lvl.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Class")
                bt_Class.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Class")
                bt_Class.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Hp")
                bt_Hp.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Hp")
                bt_Hp.Style = style_List_Header_Red;
            else if (Setting.Boby.Entity.Order == "Dst")
                bt_Dst.Style = style_List_Header_Green;
            else if (Setting.Boby.Entity.Order == "/Dst")
                bt_Dst.Style = style_List_Header_Red;
        }

        private void bt_where_target_Click(object sender, RoutedEventArgs e)
        {
            tb_where.Text = Get_Target_Name();
        }

        private void bt_clear_target_Click(object sender, RoutedEventArgs e)
        {
            tb_where.Text = "";
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

        private void Window_StateChanged(object sender, EventArgs e)
        {
			if (this.WindowState == WindowState.Normal)
				this.ShowInTaskbar = false;
			else
				this.ShowInTaskbar = true;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top < 1)
                this.Top = 0;
            if (this.Left < 1)
                this.Left = 0;
            if (this.Top + this.Height + 1 > SystemParameters.VirtualScreenHeight)
                this.Top = SystemParameters.VirtualScreenHeight - this.Height;
            if (this.Left + this.Width + 1 > SystemParameters.VirtualScreenWidth)
                this.Left = SystemParameters.VirtualScreenWidth - this.Width;
            Setting.Boby.Entity.Left = this.Left;
            Setting.Boby.Entity.Top = this.Top;
        }
    }
}