using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Diagnostics;

namespace BobyMultitools
{
    public partial class Win_Download : Form
    {
        string save_file;

        public Win_Download(string file)
        {
            InitializeComponent();

            save_file = file;

            this.Text = "Download " + file;
            this.label1.Text = "0%";
            this.progressBar.Value = 0;

            this.Show();

            string origin_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            using (WebClient Client = new WebClient())
            {
                Client.Proxy = null;
                Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompletedLaunch);
                Client.DownloadFileAsync(new Uri("http://boby.pe.hu/files/download.php?file=" + file), origin_path + @"\" + file);
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            this.progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            this.label1.Text = Math.Truncate(percentage).ToString() + "%";
        }

        void client_DownloadFileCompletedLaunch(object sender, AsyncCompletedEventArgs e)
        {
            string origin_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message + ".", "Error");
                try
                {
                    File.Delete(origin_path + @"\" + save_file);
                }
                finally { }
                Environment.Exit(0);
            }

            try
            {
                Process process = new Process();
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = origin_path + @"\";
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = "boby_multitools";
                process.StartInfo.FileName = origin_path + @"\" + save_file;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Please run as administrator.", "Error");
            }
            Environment.Exit(0);
        }
    }
}
