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
using System.CodeDom.Compiler;
using Microsoft.CSharp;

using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;

using MemoryLib;
using _Threads;

namespace BobyMultitools
{
    /// <summary>
    /// Logique d'interaction pour Win_Buff.xaml
    /// </summary>
    public partial class Win_Script : Window
    {
        public Style Style_Play = null;
        public Style Style_Pause = null;
        public ControlTemplate Bg_Base = null;
        public ControlTemplate Bg_Play = null;

        public static ScriptCollection script_collect = null;
        public static TextBox Console = null;

        public static Win_Main in_Win_Main = null;
        public Win_DialogList in_Win_DialogList = null;
        public Win_AbilityList in_Win_AbilityList = null;
        public Win_TravelList in_Win_TravelList = null;
        public bool is_play = false;

        public Win_Script(Win_Main tmp_in_Win_Main)
        {
            InitializeComponent();

            Console = console;

            Style_Play = this.FindResource("Style_Buff_Play_Button") as Style;
            Style_Pause = this.FindResource("Style_Buff_Pause_Button") as Style;
            //Bg_Base = this.FindResource("BG_Win_buff") as ControlTemplate;
            //Bg_Play = this.FindResource("BG_Win_buff2") as ControlTemplate;

            in_Win_Main = tmp_in_Win_Main;
            in_Win_Main.in_Win_Script = this;

            this.Top = Setting.Boby.Scripts.Top;
            this.Left = Setting.Boby.Scripts.Left;

            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\";
                string[] files = Directory.GetFiles(appPath, "*.cs");
                List<string> listFile = new List<string>();

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filestronc = files[i].Remove(0, appPath.Length);
                        listFile.Add(filestronc);
                    }
                }

                Script.combo = listFile;

            }
            catch
            {
                Script.combo = null;
            }

            script_collect = (ScriptCollection)Resources["ScriptCollection"];

            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_multitools_scripts.xml";

                if (File.Exists(filePath))
                {
                    script_collect.Clear();
                    XmlSerializer serializer = new XmlSerializer(typeof(ScriptCollection));
                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        IEnumerable<Script> fileData = (IEnumerable<Script>)serializer.Deserialize(stream);
                        foreach (var item in fileData)
                            script_collect.Add(item);
                    }
                }
            }
            catch (Exception)
            { }

            if (script_collect.Count == 0)
            {
                script_collect.Add(new Script { FILE = "" });
                script_collect.Add(new Script { FILE = "" });
                script_collect.Add(new Script { FILE = "" });
            }

            if (Setting.Boby.Scripts.Show)
                this.Show();

            in_Win_DialogList = new Win_DialogList();
            in_Win_AbilityList = new Win_AbilityList();
            in_Win_TravelList = new Win_TravelList();
            //in_Win_Buff_Setting = new Win_Buff_Setting(in_Win_Main);

            //Buff_Sequence();
        }

        private void bt_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_multitools_scripts.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(ScriptCollection));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, script_collect, ns);
                }
            }
            catch
            {
            }
            in_Win_Main.Full_Close();
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
            Setting.Boby.Scripts.Left = this.Left;
            Setting.Boby.Scripts.Top = this.Top;
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_DialogList.IsVisible)
            {
                in_Win_DialogList.Hide();
            }
            else
            {
                in_Win_DialogList.Show();
                in_Win_DialogList.Update();
            }
        }

        private void tFile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\scripts\";

                string[] files = Directory.GetFiles(appPath, "*.cs");
                List<string> list_files = new List<string>();

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filestronc = files[i].Remove(0, appPath.Length);
                        list_files.Add(filestronc);
                    }
                }

                foreach (var item in script_collect)
                    item.COMBO = list_files;
            }
            catch (Exception)
            { }
        }

        private void bt_Play_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = (FrameworkElement)e.OriginalSource;
                Script script = (Script)item.DataContext;
                if (script.PLAY)
                {
                    script.COMBO_ENABLE = true;
                    script.PLAY = false;
                    //bt_Play.Style = Style_Play;
                    //Aion_Game.EntityList list = new Aion_Game.EntityList();
                    //list["Esprit du sol ruiné"].Select();
                    //BG.Template = Bg_Base;
                }
                else
                {
                    script.COMBO_ENABLE = false;
                    script.PLAY = true;
                    //bt_Play.Style = Style_Pause;
                    //System.Windows.Forms.Application.DoEvents();
                }
            }
            catch
            {
                console.AppendText("Error");
            }
        }

        private void bt_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = (FrameworkElement)e.OriginalSource;
                Script script = (Script)item.DataContext;
                if (script.FILE != "")
                {
                    Editor edit = new Editor(script.FILE);
                }
            }
            catch
            {
                console.AppendText("Error");
            }
        }

        private void add_script_Click(object sender, RoutedEventArgs e)
        {
            script_collect.Add(new Script { FILE = "" });
        }

        private void remove_script_Click(object sender, RoutedEventArgs e)
        {
            if (script_collect.Count > 0)
            {
                script_collect[script_collect.Count - 1].PLAY = false;
                script_collect.RemoveAt(script_collect.Count - 1);
            }
        }

        private void ComboBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void bt_Ability_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_AbilityList.IsVisible)
            {
                in_Win_AbilityList.Hide();
            }
            else
            {
                in_Win_AbilityList.Show();
                in_Win_AbilityList.Update();
            }
        }

        private void bt_Travel_Click(object sender, RoutedEventArgs e)
        {
            if (in_Win_TravelList.IsVisible)
            {
                in_Win_TravelList.Hide();
            }
            else
            {
                in_Win_TravelList.Show();
                in_Win_TravelList.Update();
            }
        }
    }

    [Serializable]
    [XmlRoot("ScriptFiles")]
    public class ScriptCollection : System.Collections.ObjectModel.ObservableCollection<Script>
    {
    }

    [XmlType(TypeName = "ScriptFile")]
    public class Script : System.ComponentModel.INotifyPropertyChanged
    {
        [XmlIgnore]
        public static List<string> combo = null;
        private string file = "";
        private bool play = false;
        private bool combo_enable = true;
        private DispatcherTimer timer = null;
        private BobyScript.IBobyScript Interface = null;
        private BobyScript.BobyScript Class = null;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public Script()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(_OnRun);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        void _OnRun(object sender, EventArgs e)
        {
            if (Class.run == false)
            {
                COMBO_ENABLE = true;
                PLAY = false;
            }
            else if (Aion_Game.Player.IsPlayer())
                Interface.OnRun();
        }

        [XmlAttribute("Name")]
        public string FILE
        {
            get { return file; }
            set
            {
                file = value;
                NotifyPropertyChanged("FILE");
            }
        }

        [XmlIgnore]
        public bool PLAY
        {
            get { return play; }
            set
            {
                play = value;
                Play();
                NotifyPropertyChanged("PLAY");
            }
        }

        [XmlIgnore]
        public bool COMBO_ENABLE
        {
            get { return combo_enable; }
            set
            {
                combo_enable = value;
                NotifyPropertyChanged("COMBO_ENABLE");
            }
        }

        [XmlIgnore]
        public List<string> COMBO
        {
            get { return combo; }
            set
            {
                combo = value;
                NotifyPropertyChanged("COMBO");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private void Play()
        {
            if (play == true && File.Exists(@"scripts\" + FILE))
            {
                CompilerResults result = LoadScript(@"scripts\" + FILE);
                if (result.Errors.HasErrors)
                {
                    StringBuilder errors = new StringBuilder();
                    string filename = FILE;
                    foreach (CompilerError err in result.Errors)
                    {
                        errors.Append(string.Format("\r\n{0} ({1},{2}): {3}",
                                    filename, err.Line, err.Column,
                                   err.ErrorText));
                    }
                    string str = "Error Loading: " + errors.ToString() + "\r\n";

                    Win_Script.Console.AppendText(str);
                    Win_Script.Console.ScrollToEnd();
                }
                else
                {
                    try
                    {
                        GetPlugins(result.CompiledAssembly, ref Interface, ref Class);
                        if (Interface != null && Class != null)
                        {
                            Class.run = true;
                            Interface.OnPlay();
                            timer.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        Win_Script.Console.AppendText(ex.ToString());
                        Win_Script.Console.ScrollToEnd();
                    }
                }
            }
            else
            {
                if (timer != null)
                    timer.Stop();
                if (Interface != null)
                {
                    Interface.OnStop();
                    Interface = null;
                }
            }
        }
        private CompilerResults LoadScript(string filepath)
        {
            string language = CSharpCodeProvider.GetLanguageFromExtension(Path.GetExtension(filepath));
            CodeDomProvider codeDomProvider = CSharpCodeProvider.CreateProvider(language);
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.IncludeDebugInformation = false;

            //compilerParams.ReferencedAssemblies.Add("BobyScript.dll");
            compilerParams.ReferencedAssemblies.Add(Path.GetFullPath(System.Reflection.Assembly.GetEntryAssembly().Location));
            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Drawing.dll");
            compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            return codeDomProvider.CompileAssemblyFromFile(compilerParams, filepath);
        }

        private void GetPlugins(System.Reflection.Assembly assembly, ref BobyScript.IBobyScript Interface, ref BobyScript.BobyScript Class)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (((IList<Type>)type.GetInterfaces()).Contains(typeof(BobyScript.IBobyScript)) && type.IsSubclassOf(typeof(BobyScript.BobyScript)))
                {
                    var script = Activator.CreateInstance(type);
                    Interface = (BobyScript.IBobyScript)script;
                    Class = (BobyScript.BobyScript)script;
                }
            }
        }
    }
}