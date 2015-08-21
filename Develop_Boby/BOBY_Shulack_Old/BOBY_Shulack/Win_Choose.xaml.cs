/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 10/02/2014
 * Heure: 16:43
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Interop;
using System.Windows.Controls.Primitives;

using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices; // DllImport

using AddProcess;
using MemoryLib;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

using System.Net.Mail;

using System.ComponentModel;

using System.Reflection;

using _Threads;

namespace BOBY_Shulack
{
    /// <summary>
    /// Interaction logic for Win_Choose.xaml
    /// </summary>
    public partial class Win_Choose : Window
    {
        #region DllImport
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        //Initialization
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hwnd, bool enable);
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public const int BM_CLICK = 0x00F5;
        #endregion

        public Settings ini_Settings = null;

        public DownloadBox DownloadWindows = null;
        public Win_Shulack ini_Win_Shulack = null;
        public Win_Choose ini_Win_Choose = null;
        public Win_Loot ini_Win_Loot = null;
        public Win_Skills ini_Win_Skills = null;
        public Win_Viewer ini_Win_Viewer = null;
        public Win_Status ini_Win_Status = null;
        public Ability_List ini_Ability_List = null;
        public Html ini_Html = null;

        public Boucle_Status ini_Boucle_Status = null;
        public Boucle_Skills ini_Boucle_Skills = null;
        public Boucle_Delete ini_Boucle_Delete = null;
        public Boucle_Bateau ini_Boucle_Bateau = null;

        public Thread_Entity in_threadentity = null;

        DispatcherTimer messageTimer;

        public int Pid = 0;

        public bool hide = false;

        void CheckUpdate()
        {
            if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe"))
            {
                string fileVersion = AssemblyName.GetAssemblyName(@"./" + "boby_update" + ".exe").Version.ToString();
                string check_version_web = "";

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_update" + ".exe");
                    }
                    catch (Exception)
                    {
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
				{
					MessageBox.Show("Connection server.", "Error");
            		Environment.Exit(0);
                }
                
                if (fileVersion != check_version_web)
                {
                    using (WebClient Client = new WebClient())
                    {
                        Client.Proxy = null;
                        Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), @".\boby_update.exe");
                    }
                }
            }
            if (!File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe"))
            {
            	string check_version_web = "";
            	
            	using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_update" + ".exe");
                    }
                    catch (Exception)
                    {
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
				{
					MessageBox.Show("Connection server.", "Error");
            		Environment.Exit(0);
                }
            	
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_update.exe"), @".\boby_update.exe");
                }
            }
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = @".\";
                startInfo.Verb = "runas";
                startInfo.Arguments = "boby_shulack";
                startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_update.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
                while (Process.GetProcessesByName("boby_update").Any() == false)
                    ;
                while (Process.GetProcessesByName("boby_update").Any() == true)
                    ;
            }
            catch
            {
                //in_Win_Main.Hide();
                //MessageBox.Show(in_Win_Main, "boby_update.exe not found.", "Error");
                //Environment.Exit(0);
            }
        }
        
        void CheckChecker()
        {
            if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_checker.exe"))
            {
                string fileVersion = AssemblyName.GetAssemblyName(@"./" + "boby_checker" + ".exe").Version.ToString();
                string check_version_web = "";

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_checker" + ".exe");
                    }
                    catch (Exception)
                    {
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
				{
					MessageBox.Show("Connection server.", "Error");
            		Environment.Exit(0);
                }
                
                if (fileVersion != check_version_web)
                {
                    using (WebClient Client = new WebClient())
                    {
                        Client.Proxy = null;
                        Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_checker.exe"), @".\boby_checker.exe");
                    }
                }
            }
            if (!File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_checker.exe"))
            {
            	string check_version_web = "";
            	
            	using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        check_version_web = Client.DownloadString("http://boby.pe.hu/files/get_version.php?file=" + "boby_checker" + ".exe");
                    }
                    catch (Exception)
                    {
                    }
                }

                if (check_version_web == "..." || check_version_web == "")
				{
					MessageBox.Show("Connection server.", "Error");
            		Environment.Exit(0);
                }
            	
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    Client.DownloadFile(new Uri("http://boby.pe.hu/files/download.php?file=" + "boby_checker.exe"), @".\boby_checker.exe");
                }
            }
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = @".\";
                startInfo.Verb = "runas";
                startInfo.Arguments = "boby_shulack";
                startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\boby_checker.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
            }
            catch
            {
                //in_Win_Main.Hide();
                //MessageBox.Show(in_Win_Main, "boby_update.exe not found.", "Error");
                //Environment.Exit(0);
            }
        }
        
        void CheckValidKey(string _key)
        {
        	string r_key = "";
	        
	        try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    r_key = Client.DownloadString(@"http://boby.pe.hu/checkkey.php?k="+_key);
                }
	        }
	        catch
	        {
	        	MessageBox.Show("Connection server.", "Error");
            	Environment.Exit(0);
	        }
	        
	        for (int i = 0; i < 32; i++)
	        {
	        	if (r_key[i] != '&')
            		Environment.Exit(0);	        		
	        }
        }

        public Win_Choose()
        {
            InitializeComponent();
            
            ini_Win_Choose = this;

            if (!File.Exists(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location) + ".vshost.exe"))
            {
                CheckUpdate();
            }
            Listing.List();
            if (Environment.GetCommandLineArgs().Length == 1)
            {
            	CheckChecker();
            	Environment.Exit(0);
            }
            CheckValidKey(Environment.GetCommandLineArgs()[1]);
            this.Show();
            
            Online_Users_Sequence();

            if (!Directory.Exists("Skills_List"))
                Directory.CreateDirectory("Skills_List");

            UpdateText("debug ?\nhttps://docs.google.com/document/d/1J2LkcPVLWdIk9gXSKL9K2qCUmftxR8CXu9rOcBuqYSo");

            if (!File.Exists(".\\zap") && SearchBOBY() < 2)
            {
                string version_exe = "";

                if ("BOBY_Shulack_NEW.exe" == Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location))
                {
                    bool copy = true;

                    while (System.IO.File.Exists(@".\BOBY_Shulack.exe"))
                    {
                        try
                        {
                            System.IO.File.Delete(@".\BOBY_Shulack.exe");
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(100);
                        }
                    }

                    while (copy)
                    {
                        try
                        {
                            System.IO.File.Copy(@".\BOBY_Shulack_NEW.exe", @".\BOBY_Shulack.exe", true);
                            copy = false;
                        }
                        catch (Exception)
                        {
                            copy = true;
                            Thread.Sleep(100);
                        }
                    }

                    while (!System.IO.File.Exists(@".\BOBY_Shulack.exe"))
                    {
                        Thread.Sleep(100);
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.WorkingDirectory = @".\";
                    startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BOBY_Shulack.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(startInfo);
                    Environment.Exit(0);
                }
                else
                {
                    foreach (Process proc in Process.GetProcessesByName("BOBY_Shulack_NEW.exe"))
                    {
                        proc.Kill();
                    }
                }

                while (System.IO.File.Exists(@".\BOBY_Shulack_NEW.exe"))
                {
                    try
                    {
                        System.IO.File.Delete(@".\BOBY_Shulack_NEW.exe");
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                    }
                }

                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    try
                    {
                        version_exe = Client.DownloadString("http://addfile.x10host.com/Files/BOBY_Shulack_Version.txt");
                    }
                    catch (Exception)
                    {

                    }
                }

                //var versionInfo = FileVersionInfo.GetVersionInfo(Path.GetFullPath(System.Reflection.Assembly.GetEntryAssembly().Location));

                //if (version_exe != "" && version_exe != versionInfo.ProductVersion.ToString())
                //{
                //    if (MessageBox.Show(this, "Do you want to update this application?",
                //                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                //    {
                //        //ni.Visible = false;
                //        this.Hide();
                //        DownloadWindows = new DownloadBox();
                //        DownloadWindows.Show();
                //        using(WebClient Client = new WebClient())
                //        {
                //            Client.Proxy = null;
                //            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                //            Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                //            Client.DownloadFileAsync(new Uri("http://addfile.x10host.com/Files/BOBY_Shulack.exe"), @".\BOBY_Shulack_NEW.exe");
                //        }
                //        return;
                //    }
                //}
            }

            ini_Settings = new Settings();

            ini_Win_Loot = new Win_Loot();
            ini_Win_Shulack = new Win_Shulack();
            ini_Win_Viewer = new Win_Viewer();
            ini_Win_Status = new Win_Status();
            ini_Ability_List = new Ability_List();
            ini_Html = new Html();

            //new List();

            messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            SearchGame();
        }

        void win_Closing(object sender, EventArgs e)
        {
            FullClose();
        }

        void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            FullClose();
        }

        public void FullClose()
        {
            //ini_Settings.Save();
            if (MessageBox.Show(this, "Do you want to close this application?",
                                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                //ni.Visible = false;
                Environment.Exit(0);
            }
            else
            {
                // Do not close the window
            }
        }

        void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            DownloadWindows.PBar.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadWindows.Close();
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = @".\";
                startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @".\BOBY_Shulack_NEW.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
            }
            catch (Exception)
            {
                MessageBox.Show("Essayez de lancer le programme en tant qu'Administrateur.", "Erreur de lancement");
            }
            Environment.Exit(0);
        }

        public int SearchBOBY()
        {
            int result = 0;
            Process[] pid = Process.GetProcessesByName("BOBY_Shulack");
            for (int i = 0; i < pid.Length; i++)
            {
                result++;
            }
            pid = Process.GetProcessesByName("BOBY_Shulack_NEW");
            for (int i = 0; i < pid.Length; i++)
            {
                result--;
            }
            return result;
        }

        public void SearchGame()
        {
            this.Pid = 0;
            listBox_Game.Items.Clear();
            string Name = "";
            Process[] pid = Process.GetProcessesByName("aion.bin");

            if (pid.Length == 0)
            {
                //ini_Settings.Save();
                MessageBox.Show("Not Found", "aion.bin");
                //ni.Visible = false;
                Environment.Exit(0);
            }

            for (int i = 0; i < pid.Length; i++)
            {
                try
                {
                    AionProcess.Open(pid[i].Id);
                    Offset.Loading(AionProcess.Modules.Game);
                    Name = SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30);
                    if (Name == string.Empty)
                    {
                        Name = "(Login/Select. Character)";
                    }
                    else
                    {
                        Name = Name + " (" + SplMemory.ReadByte(AionProcess.Modules.Game + (long)Offset.Player.Lvl) + ")";
                    }

                    listBox_Game.Items.Add(pid[i].Id + ": " + Name);
                }
                catch { }
                Name = "";
            }

            listBox_Game.SelectedItem = listBox_Game.Items[0];

            //if (pid.Length == 1)
            //{
            //    string pid_box = listBox_Game.SelectedItem.ToString();
            //    string[] words = pid_box.Split(':');
            //    pid_box = words[0];
            //    this.Pid = Convert.ToInt32(pid_box);
            //    StartAll();
            //}
        }

        void AbyTest()
        {
            bool test = false;
            while (!test)
            {
                try
                {
                    ini_Ability_List.UpdateAbilities();
                    if (ini_Ability_List.skillfound == 1 && MessageBox.Show(this, "?",
                                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                        test = true;
                    else
                    {
                        Offset.Game.TESTAbyFirst += 0x4;
                        if (Offset.Game.TESTAbyFirst > 40000)
                        {
                            Offset.Game.TESTAbyPt += 0x4;
                            Offset.Game.TESTAbyFirst = 0;
                            Thread.Sleep(100);
                        }
                        ini_Win_Shulack.Player_Name.Text = Offset.Game.TESTAbyFirst.ToString("X");
                        ini_Win_Shulack.tbLog1.Text = Offset.Game.TESTAbyPt.ToString("X");
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                catch (Exception)
                {
                    Offset.Game.TESTAbyFirst += 0x4;
                    if (Offset.Game.TESTAbyFirst > 40000)
                    {
                        Offset.Game.TESTAbyPt += 0x4;
                        Offset.Game.TESTAbyFirst = 0;
                        Thread.Sleep(100);
                    }
                    ini_Win_Shulack.Player_Name.Text = Offset.Game.TESTAbyFirst.ToString("X");
                    ini_Win_Shulack.tbLog1.Text = Offset.Game.TESTAbyPt.ToString("X");
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            MessageBox.Show(Offset.Game.TESTAbyFirst.ToString("X"));
            MessageBox.Show(Offset.Game.TESTAbyPt.ToString("X"));
        }

        void StartAll()
        {
            if (Pid != 0 && AionProcess.Open(Pid))
            {
                Offset.Loading(AionProcess.Modules.Game);
                this.WindowState = WindowState.Minimized;
                this.Hide();
                ini_Win_Skills = new Win_Skills();
                ini_Win_Shulack.Show();
                //AbyTest();
                ini_Ability_List.UpdateAbilities();
                Find_Best_Skill();
                ini_Boucle_Status = new Boucle_Status();
                ini_Boucle_Skills = new Boucle_Skills();
                ini_Boucle_Delete = new Boucle_Delete();
                ini_Boucle_Bateau = new Boucle_Bateau();
                in_threadentity = new Thread_Entity();
                /*if (ini_Settings.inMain.shoCheat)
                {
                    ini_Win_Cheat.Show();
                    if (ini_Cheat_Boucle == null)
                        ini_Cheat_Boucle = new Cheat_Boucle();
                }
                if (ini_Settings.inMain.shoBuff)
                {
                    ini_Win_Buff.Show();
                    if (ini_Buff_Boucle == null)
                        ini_Buff_Boucle = new Buff_Boucle();
                }
                if (ini_Settings.inMain.shoRadar)
                    ini_Win_Radar.Show();
                if (ini_Settings.inMain.shoEntity)
                    ini_Win_Entity.Show();
                this.hide = false;
                if (ini_Entity_Boucle == null)
                    ini_Entity_Boucle = new Entity_Boucle();*/
                Thread.Sleep(1000);
                messageTimer.Start();
            }
            else
            {
                //ini_Settings.Save();
                MessageBox.Show("Not Found", "aion.bin");
                //ni.Visible = false;
                Environment.Exit(0);
            }
        }

        public bool ProcessIDOn(int id)
        {
            Process[] pid = Process.GetProcessesByName("aion.bin");
            for (int i = 0; i < pid.Length; i++)
            {
                if (id == pid[i].Id)
                    return true;
            }
            return false;
        }

        private bool WindowsIs()
        {
            bool isactive = false;
            if (AionProcess.whandle != IntPtr.Zero)
            {
                IntPtr MWhandle = AionProcess.whandle;
                IntPtr CWhandle = GetForegroundWindow();
                if (MWhandle == CWhandle)
                    isactive = true;
            }
            return isactive;
        }

        private bool IsGoodWindows()
        {
            uint processId;
            GetWindowThreadProcessId(AionProcess.whandle, out processId);
            if (processId == this.Pid)
                return true;
            else
                return false;
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            DlgAionIni();
        }

        void listBox_Game_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Up)
            {
                if (listBox_Game.SelectedIndex - 1 >= 0)
                    listBox_Game.SelectedItem = listBox_Game.Items[listBox_Game.SelectedIndex - 1];
            }
            if (e.Key == Key.Down)
            {
                if (listBox_Game.SelectedIndex + 1 < listBox_Game.Items.Count)
                    listBox_Game.SelectedItem = listBox_Game.Items[listBox_Game.SelectedIndex + 1];
            }
            if (e.Key == Key.Enter)
            {
                if (listBox_Game.SelectedItem != null)
                {
                    try
                    {
                        string pid = listBox_Game.SelectedItem.ToString();
                        string[] words = pid.Split(':');
                        pid = words[0];
                        this.Pid = Convert.ToInt32(pid);
                        StartAll();
                    }
                    catch { }
                }
            }
        }

        private void UpdateText(string message)
        {
            tbLog.Text = message;
            tbLog.ScrollToEnd();
        }

        void listBox_Game_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBox_Game.SelectedItem != null)
            {
                string pid = listBox_Game.SelectedItem.ToString();
                string[] words = pid.Split(':');
                pid = words[0];
                this.Pid = Convert.ToInt32(pid);
                StartAll();
            }
        }

        void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Game.SelectedItem != null)
            {
                string pid = listBox_Game.SelectedItem.ToString();
                string[] words = pid.Split(':');
                pid = words[0];
                this.Pid = Convert.ToInt32(pid);
                StartAll();
            }
        }

        public void HideAION()
        {
            IntPtr wHandle = IntPtr.Zero;
            Process[] procs = Process.GetProcesses();

            foreach (Process proc in procs)
            {
                if (proc.Id == AionProcess.pid)
                {
                    wHandle = proc.MainWindowHandle;
                    break;
                }
            }

            if (wHandle == IntPtr.Zero)
            {
                ShowWindow(AionProcess.whandle, SW_SHOW);
            }
            else
            {
                ShowWindow(AionProcess.whandle, SW_HIDE);
            }
        }

        public void SendEnter()
        {
            (
                new Thread(() =>
                           {
                               SendMessage(AionProcess.whandle, 0x0100, (IntPtr)0x0D, IntPtr.Zero);
                           }
                          )
            ).Start();
        }

        public void SendEscape()
        {
            (
                new Thread(() =>
                           {
                               SendMessage(AionProcess.whandle, 0x0100, (IntPtr)0x1B, IntPtr.Zero);
                           }
                          )
            ).Start();
        }

        public void SendClick()
        {
            PostMessage(AionProcess.whandle, 0x0201, (IntPtr)0x0001, IntPtr.Zero);
            PostMessage(AionProcess.whandle, 0x0202, (IntPtr)0x0001, IntPtr.Zero);
        }

        public void SendLeftMousseDown()
        {
            PostMessage(AionProcess.whandle, 0x0201, (IntPtr)0x0001, IntPtr.Zero);
        }

        public void SendLeftMousseUp(int x, int y)
        {
            PostMessage(AionProcess.whandle, 0x0200, (IntPtr)0x0000, (IntPtr)MakeLParam(x, y));
            PostMessage(AionProcess.whandle, 0x0202, (IntPtr)0x0001, IntPtr.Zero);
        }

        public void SendMousseMove(int x, int y)
        {
            PostMessage(AionProcess.whandle, 0x0200, (IntPtr)0x0000, (IntPtr)MakeLParam(x, y));
        }

        private int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord * 0x10000) | (LoWord & 0xFFFF));
        }

        public void SendCtrlV()
        {
            for (int i = 0; i < 256; i++){
             	SendMessage(AionProcess.whandle, 0x0102, (IntPtr)0x42, IntPtr.Zero);
        	}
        }

        void SendReturn()
        {
            SendMessage(AionProcess.whandle, 0x100, (IntPtr)0x08, IntPtr.Zero);
        }

        public void DlgAionIni()
        {
            try
            {
                if (GetValue_Chat_Input_Ini() < 255)
                {
                	if (GetValue_Chat_Is_Open() == 7)
                	{
                    	SendCtrlV();
                    }
                    if (GetValue_Chat_Is_Open() == 6)
                    {
                    	SendEnter();
                    }
                }
                else
                {
                	if (GetValue_Chat_Is_Open() == 7)
                	{
                		long adressChat = GetAddress_Chat_For_Text();
	                    if (adressChat != 0)
	                    {
		                    SplMemory.WriteWchar(adressChat, "", 255);
                        	SendEnter();
	                    }
                	}
                	if (GetValue_Chat_Is_Open() == 6)
                	{
                		return ;
                	}
                }
            }
            catch { }
        }

        public void DlgAion(string Text)
        {
            try
            {
                if (Text != "" && GetValue_Chat_Input_Ini() >= 255)
                {
                    SplMemory.WriteWchar(GetAddress_Chat_For_Text(), Text, Text.Length);
                    SplMemory.WriteWchar(GetAddress_Chat_For_Text() + Text.Length * 2, "\0", Text.Length);
                    if (GetValue_Chat_Is_Open() == 7)
                    {
                        Console.WriteLine(Text);
                        SendEnter();
                    }
                    else
                    {
                        Console.WriteLine(Text);
                        SendEnter();
                        SendEnter();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Bug" + Text);
            }
        }

        short GetValue_Chat_Input_Ini()
        {
            return SplMemory.ReadShort(
                SplMemory.ReadLong(
                    SplMemory.ReadLong(
                        (long)Offset.Base_windows.newbase["chat_input_dialog"]) + (long)Offset.ChatDlg.Jump) + (long)Offset.ChatDlg.Length);
        }

        public byte GetValue_Chat_Is_Open()
        {
            return SplMemory.ReadByte(
                SplMemory.ReadLong(
                    (long)Offset.Base_windows.newbase["chat_input_dialog"]) + (long)Offset.ChatDlg.IsOpen);
        }

        long GetAddress_Chat_For_Text()
        {
            return SplMemory.ReadLong(
                SplMemory.ReadLong(
                    SplMemory.ReadLong(
                        (long)Offset.Base_windows.newbase["chat_input_dialog"]) + (long)Offset.ChatDlg.Jump) + (long)Offset.ChatDlg.Input);
        }

        public void Find_Best_Skill()
        {
            Dictionary<long, Ability> LAbilities = null;
            List<string> LAbilityName = null;
            LAbilities = new Dictionary<long, Ability>();
            LAbilityName = new List<string>();
            if (ini_Ability_List.Abilities != null)
            {
                for (int i = 15; i > 0; i--)
                {
                    foreach (var keyValue in ini_Ability_List.Abilities)
                    {
                        if (keyValue.Value.Name.EndsWith(" " + (EnumAion.roman_numerals)i)
                            && !LAbilityName.Contains(keyValue.Value.Name.Replace(" " + (EnumAion.roman_numerals)i, ""))
                            && !LAbilities.ContainsKey(keyValue.Key))
                        {
                            LAbilities.Add(keyValue.Key, (Ability)keyValue.Value);
                            LAbilityName.Add(keyValue.Value.Name.Replace(" " + (EnumAion.roman_numerals)i, ""));
                        }
                    }

                }
            }

            ini_Ability_List.Abilities = LAbilities;
            ini_Win_Skills.listview_Skill.ItemsSource = LAbilityName.OrderBy(q => q).ToList();
        }

        public void Set_List_Skill_Item(List<string> list)
        {
            list = list.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            ini_Win_Skills.listview_Item.ItemsSource = list;
            ini_Win_Loot.listview_Item.ItemsSource = list;
            ini_Win_Loot.listview_Item2.ItemsSource = list;
        }

        public void PlayerMovePointAndJump(int x, int y)
        {
            float PlayerX = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
            float PlayerY = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

            double part1 = Math.Pow((x - PlayerX), 2);
            double part2 = Math.Pow((y - PlayerY), 2);
            double result1 = part1 + part2;
            int Dist = (int)Math.Sqrt(result1);
            DateTime TimeJump = DateTime.Now.AddSeconds(5);
            while (Dist > 2
                   && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0)
            {
                if (TimeJump < DateTime.Now)
                    SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsJump, (byte)1);
                PlayerX = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
                PlayerY = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

                part1 = Math.Pow((x - PlayerX), 2);
                part2 = Math.Pow((y - PlayerY), 2);
                result1 = part1 + part2;
                Dist = (int)Math.Sqrt(result1);

                float Diff_X = x - PlayerX;
                float Diff_Y = y - PlayerY;
                float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

                if (Diff_X > 0 && Diff_Y > 0)
                {
                    Angle_X = (180 - Angle_X);
                }
                else if (Diff_Y > 0 && Diff_X < 0)
                {
                    Angle_X = (-180 - Angle_X);
                }
                else if (Diff_X < 0 && Diff_Y < 0)
                {
                    Angle_X = (0 - Angle_X);
                }
                else if (Diff_Y < 0 && Diff_X > 0)
                {
                    Angle_X = (0 - Angle_X);
                }

                if (Angle_X < 0 && Angle_X > -180)
                {
                    Angle_X = 360 - Math.Abs(Angle_X);
                }

                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.CamRotH, Angle_X);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove, 4);
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(10);
            }
        }

        public void PlayerMovePoint(int x, int y)
        {
            float PlayerX = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
            float PlayerY = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

            double part1 = Math.Pow((x - PlayerX), 2);
            double part2 = Math.Pow((y - PlayerY), 2);
            double result1 = part1 + part2;
            int Dist = (int)Math.Sqrt(result1);
            while (Dist > 2
                   && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0)
            {
                PlayerX = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
                PlayerY = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

                part1 = Math.Pow((x - PlayerX), 2);
                part2 = Math.Pow((y - PlayerY), 2);
                result1 = part1 + part2;
                Dist = (int)Math.Sqrt(result1);

                float Diff_X = x - PlayerX;
                float Diff_Y = y - PlayerY;
                float Angle_X = (float)(Math.Atan(Diff_X / Diff_Y) * 180 / 3.14);

                if (Diff_X > 0 && Diff_Y > 0)
                {
                    Angle_X = (180 - Angle_X);
                }
                else if (Diff_Y > 0 && Diff_X < 0)
                {
                    Angle_X = (-180 - Angle_X);
                }
                else if (Diff_X < 0 && Diff_Y < 0)
                {
                    Angle_X = (0 - Angle_X);
                }
                else if (Diff_Y < 0 && Diff_X > 0)
                {
                    Angle_X = (0 - Angle_X);
                }

                if (Angle_X < 0 && Angle_X > -180)
                {
                    Angle_X = 360 - Math.Abs(Angle_X);
                }

                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.CamRotH, Angle_X);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.IsMove, 4);
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(10);
            }
        }

        public void Recharge()
        {
            long PlayerPtr = FindPlayerPtr();
            DlgAion("/Select " + SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30));
            PlayerMovePointAndJump(626, 348);
            PlayerMovePointAndJump(619, 347);
            PlayerMovePoint(608, 348);
            do
            {
                DlgAion("/Select Recharger");
                Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
            } while (Get_Name_Target() != "Recharger");
            do
            {
                DlgAion("/Select Recharger");
                Thread.Sleep(400);
                DlgAion("/Attack");
                Thread.Sleep(400);
                System.Windows.Forms.Application.DoEvents();
                ini_Boucle_Skills.ListBuff(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status));
            } while (!ini_Boucle_Skills.Buff_Is_Active(19520));
            DlgAion("/Select " + SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30));
            PlayerMovePointAndJump(619, 347);
            PlayerMovePointAndJump(621, 338);
        }

        public void Start_Place()
        {
            float PlayerX = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X);
            float PlayerY = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Y);

            if (PlayerY > 350 && PlayerX > 625)
                PlayerMovePointAndJump(636, 337);
            else if (PlayerY < 328 && PlayerX < 615)
            {
                PlayerMovePointAndJump(610, 320);
                PlayerMovePointAndJump(610, 335);
            }
            else if (PlayerX > 604 && PlayerX < 617 && PlayerY > 343 && PlayerY < 354)
                PlayerMovePointAndJump(626, 348);
            PlayerMovePointAndJump(621, 338);
        }

        public void Start_Assist()
        {
            DlgAion("/anon");
            float Z = 0;

            long PlayerPtr = FindPlayerPtr();

            if (PlayerPtr != 0)
            {
                PlayerMovePoint(557, 536);
                do
                {
                    do
                    {
                        SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status) + (long)Offset.Status.No_Grav, 5);
                        Z = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Z);
                        SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Loc) + (long)Offset.Loc.X, SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X) - 0.3f);
                        SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Loc) + (long)Offset.Loc.Z, Z - 0.3f);
                        Thread.Sleep(100);
                        SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Loc) + (long)Offset.Loc.X, SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.X) + 0.3f);
                        SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Loc) + (long)Offset.Loc.Z, Z - 0.3f);
                        Thread.Sleep(100);
                        System.Windows.Forms.Application.DoEvents();
                    } while (592f < Z && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                    PlayerMovePoint(565, 530);
                    Z = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Z);
                    PlayerMovePoint(557, 536);
                    Z = SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Z);
                } while (592f < Z && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                PlayerMovePoint(700, 451);
                SplMemory.WriteMemory(AionProcess.Modules.Game + (long)Offset.Player.CamRotV, -85.0f);
                do
                {
                    SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status) + (long)Offset.Status.No_Grav, 5);
                    DlgAion("/Select Commandes de l'élévateur");
                    Thread.Sleep(200);
                    System.Windows.Forms.Application.DoEvents();
                } while (Get_Name_Target() != "Commandes de l'élévateur" && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                do
                {
                    SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status) + (long)Offset.Status.No_Grav, 5);
                    DlgAion("/Attack");
                    Thread.Sleep(700);
                    System.Windows.Forms.Application.DoEvents();
                } while (!Target_LookMe() && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                Thread.Sleep(2000);
                do
                {
                    SplMemory.WriteMemory(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status) + (long)Offset.Status.No_Grav, 5);
                    ini_Html.Clic_Bt(1);
                    Thread.Sleep(700);
                    System.Windows.Forms.Application.DoEvents();
                } while (Target_LookMe() && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                DlgAion("/Select " + SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30));
                do
                {
                    Thread.Sleep(700);
                    System.Windows.Forms.Application.DoEvents();
                } while (SplMemory.ReadFloat(AionProcess.Modules.Game + (long)Offset.Player.Z) < 600f);
                PlayerMovePoint(683, 460);
                PlayerMovePoint(687, 467);
                do
                {
                    DlgAion("/Select Coffre de fournitures d'Hariken");
                    Thread.Sleep(50);
                    System.Windows.Forms.Application.DoEvents();
                } while (Get_Name_Target() != "Coffre de fournitures d'Hariken" && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                PlayerPtr = FindPlayerPtr();
                do
                {
                    DlgAion("/Attack");
                    Thread.Sleep(700);
                    System.Windows.Forms.Application.DoEvents();
                } while (!Target_LookMe() && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                do
                {
                    ini_Html.Clic_Bt(1);
                    Thread.Sleep(700);
                    System.Windows.Forms.Application.DoEvents();
                } while (Target_LookMe() && SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.HP) > 0);
                DlgAion("/Select " + SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30));
                PlayerPtr = FindPlayerPtr();
                PlayerMovePoint(685, 459);
                PlayerMovePoint(661, 468);
                PlayerMovePoint(636, 424);
                PlayerMovePoint(635, 353);
                PlayerMovePoint(622, 353);
                PlayerMovePoint(620, 347);
                PlayerMovePoint(608, 348);
                do
                {
                    DlgAion("/Select Recharger");
                    Thread.Sleep(100);
                    System.Windows.Forms.Application.DoEvents();
                } while (Get_Name_Target() != "Recharger");
                do
                {
                    DlgAion("/Select Recharger");
                    Thread.Sleep(400);
                    DlgAion("/Attack");
                    Thread.Sleep(400);
                    System.Windows.Forms.Application.DoEvents();
                    ini_Boucle_Skills.ListBuff(SplMemory.ReadLong(PlayerPtr + (long)Offset.Entity.Status));
                } while (!ini_Boucle_Skills.Buff_Is_Active(19520));
                DlgAion("/Select " + SplMemory.ReadWchar(AionProcess.Modules.Game + (long)Offset.Player.Name, 30));
                PlayerMovePointAndJump(619, 347);
                PlayerMovePointAndJump(621, 338);
            }
        }

        private string Get_Name_Target()
        {
            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 1)
            {
                long tmp = SplMemory.ReadLong(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Entity.To_Target) + (long)Offset.Entity.Status);
                if (tmp != 0 && tmp != 0xCDCDCDCD)
                    return (SplMemory.ReadWchar(tmp + (long)Offset.Status.Name, 60));
            }
            return "";
        }

        private bool Target_LookMe()
        {
            if (SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Entity.Is_Target) == 1)
            {
                if (SplMemory.ReadInt(SplMemory.ReadLong(SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Entity.To_Target) + (long)Offset.Entity.Status) + (long)Offset.Status.TargetId)
                    == SplMemory.ReadInt(AionProcess.Modules.Game + (long)Offset.Player.ID))
                    return true;
            }
            return false;
        }

        private long FindPlayerPtr()
        {
            long entityMap = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.EntityList.Pointer);
            long entityArray = SplMemory.ReadLong(entityMap + (long)Offset.EntityList.Array);

            return TraverseNodePlayer(SplMemory.ReadLong(entityArray));
        }

        private long TraverseNodePlayer(long nodePtr)
        {
            try
            {
                long entityPtr = SplMemory.ReadLong(nodePtr + 12);
                if (SplMemory.ReadByte(entityPtr + (long)Offset.Entity.Type) == EnumAion.eType.Player)
                    return entityPtr;
                long rightPtr = SplMemory.ReadLong(nodePtr + 4);
                if (rightPtr != 0 && rightPtr != 0xCDCDCDCD)
                {
                    return (TraverseNodePlayer(rightPtr));
                }
            }
            catch (Exception)
            {
                // uh oh, changing memory structure?
                // lets just back away slowly.
            }
            return 0;
        }
    }
}