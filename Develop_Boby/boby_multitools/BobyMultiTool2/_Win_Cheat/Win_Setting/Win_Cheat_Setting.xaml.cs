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
        public BuffCollection buff_collect;

        public Win_Cheat_Setting(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            in_Win_Main = tmp_in_Win_Main;

            if (Setting.Boby.Cheat.Acc_Distance != 0)
                sl_acc_dist.Value = Setting.Boby.Cheat.Acc_Distance;
            else
                sl_acc_dist_ValueChanged(null, null);

            if (Setting.Boby.Cheat.Sup_Distance != 0)
                sl_Sup_dist.Value = Setting.Boby.Cheat.Sup_Distance;
            else
                sl_Sup_dist_ValueChanged(null, null);

            hotkey_for_textbox(keys_NoGrav, Setting.Boby.CheatKey.modifierNoGrav, Setting.Boby.CheatKey.keyNoGrav);
            hotkey_for_textbox(keys_ZLock, Setting.Boby.CheatKey.modifierZLock, Setting.Boby.CheatKey.keyZLock);
            hotkey_for_textbox(keys_Actived, Setting.Boby.CheatKey.modifierToKey, Setting.Boby.CheatKey.keyToKey);
            hotkey_for_textbox(keys_Acc_For, Setting.Boby.CheatKey.modifierAccFor, Setting.Boby.CheatKey.keyAccFor);
            hotkey_for_textbox(keys_Acc_Up, Setting.Boby.CheatKey.modifierAccUp, Setting.Boby.CheatKey.keyAccUp);
            hotkey_for_textbox(keys_Acc_Down, Setting.Boby.CheatKey.modifierAccDown, Setting.Boby.CheatKey.keyAccDown);
            hotkey_for_textbox(keys_Sup_For, Setting.Boby.CheatKey.modifierSupFor, Setting.Boby.CheatKey.keySupFor);
            hotkey_for_textbox(keys_Sup_Up, Setting.Boby.CheatKey.modifierSupUp, Setting.Boby.CheatKey.keySupUp);
            hotkey_for_textbox(keys_Sup_Down, Setting.Boby.CheatKey.modifierSupDown, Setting.Boby.CheatKey.keySupDown);
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
            Setting.Boby.Cheat.Acc_Distance = (int)sl_acc_dist.Value;

            double txt = (sl_acc_dist.Value + 1) / 100d;
            lb_acc_dist.Content = txt.ToString("0.00m");
        }

        private void sl_Sup_dist_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Setting.Boby.Cheat.Sup_Distance = (int)sl_Sup_dist.Value;

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
                Setting.Boby.CheatKey.modifierNoGrav = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyNoGrav = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierNoGrav = 0;
                Setting.Boby.CheatKey.keyNoGrav = 0;
            }
        }

        private void keys_ZLock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_ZLock, key))
            {
                Setting.Boby.CheatKey.modifierZLock = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyZLock = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierZLock = 0;
                Setting.Boby.CheatKey.keyZLock = 0;
            }
        }

        private void keys_Actived_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Actived, key))
            {
                Setting.Boby.CheatKey.modifierToKey = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyToKey = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierToKey = 0;
                Setting.Boby.CheatKey.keyToKey = 0;
            }
        }

        private void keys_Acc_For_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_For, key))
            {
                Setting.Boby.CheatKey.modifierAccFor = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyAccFor = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierAccFor = 0;
                Setting.Boby.CheatKey.keyAccFor = 0;
            }
        }

        private void keys_Acc_Up_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_Up, key))
            {
                Setting.Boby.CheatKey.modifierAccUp = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyAccUp = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierAccUp = 0;
                Setting.Boby.CheatKey.keyAccUp = 0;
            }
        }

        private void keys_Acc_Down_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Acc_Down, key))
            {
                Setting.Boby.CheatKey.modifierAccDown = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keyAccDown = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierAccDown = 0;
                Setting.Boby.CheatKey.keyAccDown = 0;
            }
        }

        private void keys_Sup_For_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_For, key))
            {
                Setting.Boby.CheatKey.modifierSupFor = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keySupFor = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierSupFor = 0;
                Setting.Boby.CheatKey.keySupFor = 0;
            }
        }

        private void keys_Sup_Up_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_Up, key))
            {
                Setting.Boby.CheatKey.modifierSupUp = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keySupUp = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierSupUp = 0;
                Setting.Boby.CheatKey.keySupUp = 0;
            }
        }

        private void keys_Sup_Down_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (textbox_for_hotkey(keys_Sup_Down, key))
            {
                Setting.Boby.CheatKey.modifierSupDown = (int)Keyboard.Modifiers;
                Setting.Boby.CheatKey.keySupDown = (int)key;
            }
            else
            {
                Setting.Boby.CheatKey.modifierSupDown = 0;
                Setting.Boby.CheatKey.keySupDown = 0;
            }
        }
    }
}