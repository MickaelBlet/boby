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

            this.Left = in_Win_Main.in_Setting.in_Entity.Left.Get_Value();
            this.Top = in_Win_Main.in_Setting.in_Entity.Top.Get_Value();
            this.Height = in_Win_Main.in_Setting.in_Entity.Height.Get_Value();
            this.Width = in_Win_Main.in_Setting.in_Entity.Width.Get_Value();

            this.Check_NPC.IsChecked = in_Win_Main.in_Setting.in_Entity.NPC.Get_Value();
            this.Check_Ally.IsChecked = in_Win_Main.in_Setting.in_Entity.Ally.Get_Value();
            this.Check_Ennemy.IsChecked = in_Win_Main.in_Setting.in_Entity.Hostile.Get_Value();
            this.Check_Gather.IsChecked = in_Win_Main.in_Setting.in_Entity.Gather.Get_Value();

            if (in_Win_Main.in_Setting.in_Entity.Show.Get_Value())
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
            in_Win_Main.in_Setting.in_Entity.NPC.Set_Value(true);
        }

        void Check_NPC_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.NPC.Set_Value(false);
        }

        void Check_Ally_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Ally.Set_Value(true);
        }

        void Check_Ally_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Ally.Set_Value(false);
        }

        void Check_Ennemy_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Hostile.Set_Value(true);
        }

        void Check_Ennemy_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Hostile.Set_Value(false);
        }

        void Check_Gather_Checked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Gather.Set_Value(true);
        }

        void Check_Gather_Unchecked(object sender, RoutedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Gather.Set_Value(false);
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
                    Thread Select = new Thread(() => SlashSelect(in_Win_Entity_Popup.entityPtr));
                    Select.Start();
                }
            }
        }

        private void tb_where_TextChanged(object sender, TextChangedEventArgs e)
        {
            in_Win_Main.in_Setting.in_Entity.Where = tb_where.Text;
        }

        #endregion

        void SlashSelect(long entityPtr)
        {
            //Character.Select(entityPtr);
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
            in_Win_Main.in_Setting.in_Entity.Height.Set_Value((int)this.Height);
            in_Win_Main.in_Setting.in_Entity.Width.Set_Value((int)this.Width);
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
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Rnk")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Rnk");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Rnk");
            refresh_style_button();
        }

        private void bt_Name_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Name")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Name");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Name");
            refresh_style_button();
        }

        private void bt_Guild_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Guild")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Guild");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Guild");
            refresh_style_button();
        }

        private void bt_Lvl_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Lvl")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Lvl");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Lvl");
            refresh_style_button();
        }

        private void bt_Class_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Class")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Class");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Class");
            refresh_style_button();
        }

        private void bt_Hp_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Hp")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Hp");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Hp");
            refresh_style_button();
        }

        private void bt_Dst_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Dst")
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("/Dst");
            else
                in_Win_Main.in_Setting.in_Entity.Order.Set_Value("Dst");
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
            if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Rnk")
                bt_Rnk.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Rnk")
                bt_Rnk.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Name")
                bt_Name.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Name")
                bt_Name.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Guild")
                bt_Guild.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Guild")
                bt_Guild.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Lvl")
                bt_Lvl.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Lvl")
                bt_Lvl.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Class")
                bt_Class.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Class")
                bt_Class.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Hp")
                bt_Hp.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Hp")
                bt_Hp.Style = style_List_Header_Red;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "Dst")
                bt_Dst.Style = style_List_Header_Green;
            else if (in_Win_Main.in_Setting.in_Entity.Order.Get_Value() == "/Dst")
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
            in_Win_Main.in_Setting.in_Entity.Left.Set_Value((int)this.Left);
            in_Win_Main.in_Setting.in_Entity.Top.Set_Value((int)this.Top);
        }
    }
}