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
using _Threads;

namespace BobyMultitools
{
    public partial class Win_Cheat_Setting : Window
    {
        public static Win_Main in_Win_Main = null;
        public Hashtable Image_File_Mini = null;
        public Hashtable Image_File_Real = null;

        public IconCollection icon_collect;
        public IconSaveCollection icon_save_collect;
        public BuffCollection buff_collect;

        public Win_Cheat_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            if (in_Win_Main.in_Setting.in_Cheat.Acc_Distance.Get_Value() != 0)
                sl_acc_dist.Value = in_Win_Main.in_Setting.in_Cheat.Acc_Distance.Get_Value();
            else
                sl_acc_dist_ValueChanged(null, null);

            if (in_Win_Main.in_Setting.in_Cheat.Sup_Distance.Get_Value() != 0)
                sl_Sup_dist.Value = in_Win_Main.in_Setting.in_Cheat.Sup_Distance.Get_Value();
            else
                sl_Sup_dist_ValueChanged(null, null);

            hotkey_for_textbox(keys_NoGrav, in_Win_Main.in_Setting.in_CheatKey.modifierNoGrav.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyNoGrav.Get_Value());
            hotkey_for_textbox(keys_ZLock, in_Win_Main.in_Setting.in_CheatKey.modifierZLock.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyZLock.Get_Value());
            hotkey_for_textbox(keys_Actived, in_Win_Main.in_Setting.in_CheatKey.modifierToKey.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyToKey.Get_Value());
            hotkey_for_textbox(keys_Acc_For, in_Win_Main.in_Setting.in_CheatKey.modifierAccFor.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyAccFor.Get_Value());
            hotkey_for_textbox(keys_Acc_Up, in_Win_Main.in_Setting.in_CheatKey.modifierAccUp.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyAccUp.Get_Value());
            hotkey_for_textbox(keys_Acc_Down, in_Win_Main.in_Setting.in_CheatKey.modifierAccDown.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keyAccDown.Get_Value());
            hotkey_for_textbox(keys_Sup_For, in_Win_Main.in_Setting.in_CheatKey.modifierSupFor.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keySupFor.Get_Value());
            hotkey_for_textbox(keys_Sup_Up, in_Win_Main.in_Setting.in_CheatKey.modifierSupUp.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keySupUp.Get_Value());
            hotkey_for_textbox(keys_Sup_Down, in_Win_Main.in_Setting.in_CheatKey.modifierSupDown.Get_Value(), in_Win_Main.in_Setting.in_CheatKey.keySupDown.Get_Value());
        }

        private void hotkey_for_textbox(object textBoxZlock, object p1, object p2)
        {
            throw new NotImplementedException();
        }

        private void rt_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void sl_acc_dist_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Cheat.Acc_Distance.Set_Value((int)sl_acc_dist.Value);

            double txt = (sl_acc_dist.Value + 1) / 100d;
            lb_acc_dist.Content = txt.ToString("0.00m");
        }

        private void sl_Sup_dist_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            in_Win_Main.in_Setting.in_Cheat.Sup_Distance.Set_Value((int)sl_Sup_dist.Value);

            double txt = (sl_Sup_dist.Value + 1) / 10d;
            lb_Sup_dist.Content = txt.ToString("0.00m");
        }

        void hotkey_for_textbox(TextBox textbox, int modifiers, int key)
        {
            // Fetch the actual shortcut key.
            string text_for_textbox = "";

            switch (modifiers)
            {
                case 1://alt
                    text_for_textbox = "Alt+";
                    break;
                case 2://ctrl
                    text_for_textbox = "Ctrl+";
                    break;
                case 3://alt+ctrl
                    text_for_textbox = "Ctrl+Alt+";
                    break;
                case 4://shift
                    text_for_textbox = "Shift+";
                    break;
                case 5://alt+shift
                    text_for_textbox = "Shift+Alt+";
                    break;
                case 6://ctrl+shift
                    text_for_textbox = "Ctrl+Shift+";
                    break;
                case 7://alt+ctrl+shift
                    text_for_textbox = "Ctrl+Shift+Alt+";
                    break;
            }

            textbox.Text = text_for_textbox + ((Key)key).ToString();
        }

        bool textbox_for_hotkey(TextBox textbox, Key key)
        {
            // Fetch the actual shortcut key.
            if (!((int)Keyboard.Modifiers == 0 && key == Key.Back))
            {
                // Ignore modifier keys.
                if (key == Key.LeftShift || key == Key.RightShift
                    || key == Key.LeftCtrl || key == Key.RightCtrl
                    || key == Key.LeftAlt || key == Key.RightAlt
                    || key == Key.LWin || key == Key.RWin)
                {
                    textbox.Text = "None";
                    return false;
                }

                // Build the shortcut key name.
                StringBuilder shortcutText = new StringBuilder();
                if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                {
                    shortcutText.Append("Ctrl+");
                }
                if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                {
                    shortcutText.Append("Shift+");
                }
                if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
                {
                    shortcutText.Append("Alt+");
                }
                shortcutText.Append(key.ToString());

                textbox.Text = shortcutText.ToString();
                return true;
            }
            else
            {
                textbox.Text = "None";
                return false;
            }
        }

        private void keys_NoGrav_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_NoGrav, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierNoGrav.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyNoGrav.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierNoGrav.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyNoGrav.Set_Value(0);
            }
        }

        private void keys_ZLock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_ZLock, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierZLock.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyZLock.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierZLock.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyZLock.Set_Value(0);
            }
        }

        private void keys_Actived_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Actived, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierToKey.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyToKey.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierToKey.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyToKey.Set_Value(0);
            }
        }

        private void keys_Acc_For_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_For, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccFor.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyAccFor.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccFor.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyAccFor.Set_Value(0);
            }
        }

        private void keys_Acc_Up_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_Up, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccUp.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyAccUp.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccUp.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyAccUp.Set_Value(0);
            }
        }

        private void keys_Acc_Down_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_Down, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccDown.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keyAccDown.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierAccDown.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keyAccDown.Set_Value(0);
            }
        }

        private void keys_Sup_For_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_For, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupFor.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keySupFor.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupFor.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keySupFor.Set_Value(0);
            }
        }

        private void keys_Sup_Up_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_Up, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupUp.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keySupUp.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupUp.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keySupUp.Set_Value(0);
            }
        }

        private void keys_Sup_Down_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_Down, key))
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupDown.Set_Value((int)Keyboard.Modifiers);
                in_Win_Main.in_Setting.in_CheatKey.keySupDown.Set_Value((int)key);
            }
            else
            {
                in_Win_Main.in_Setting.in_CheatKey.modifierSupDown.Set_Value(0);
                in_Win_Main.in_Setting.in_CheatKey.keySupDown.Set_Value(0);
            }
        }
    }
}