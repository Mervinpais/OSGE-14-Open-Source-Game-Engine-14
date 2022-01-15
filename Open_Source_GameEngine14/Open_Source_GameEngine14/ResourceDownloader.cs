using System;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace Open_Source_GameEngine14
{
    public partial class ResourceDownloader : Form
    {
        public ResourceDownloader()
        {
            InitializeComponent();
        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private string uwerfh_;
        public string uwerfh
        {
            get
            {
                return uwerfh_;
            }
            set
            {
                uwerfh_ = value;
            }
        }

        private string itemToDownloadName_;
        public string itemToDownloadName
        {
            get
            {
                return itemToDownloadName_;
            }
            set
            {
                itemToDownloadName_ = value;
            }
        }

        private bool showDialog_ = true;
        public bool showDialog
        {
            get
            {
                return showDialog_;
            }
            set
            {
                showDialog_ = value;
            }
        }

        private string updateLatestVersion_;
        public string updateLatestVersion
        {
            get
            {
                return updateLatestVersion_;
            }
            set
            {
                updateLatestVersion_ = value;
            }
        }

        private bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        WebClient client = null;
        string download_destinationpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\DownloadedFiles";
        private void ResourceDownloader_Shown(object sender, EventArgs e)
        {
            if (showDialog_ == true)
            {
                this.Visible = true;
            }
            else if (showDialog_ == false)
            {
                this.Visible = false;
            }
            downloadingWhatItemText.Text = $"Downloading {itemToDownloadName}";
            bool isConnectedToInternet = CheckConnection(uwerfh);
            if (isConnectedToInternet == false)
            {
                downloadStatusText.Text = "ERROR: Unable to get a connection";
                return;
            }
            else if (isConnectedToInternet == true)
            {
                downloadStatusText.Text = "Status: Downloading File(s)";
            }
            wait(75);
            string sourcedownloadURI = null;
            try
            {
                sourcedownloadURI = uwerfh;

                if (client != null && client.IsBusy) // If the client is already downloading something we don't start a new download
                    return;

                if (client == null) // We only create a new client if we don't already have one
                {
                    client = new WebClient(); // Create a new client here
                    client.DownloadFileCompleted += client_DownloadFileCompleted;
                    client.DownloadProgressChanged += client_DownloadProgressChanged; // Add new event handler for updating the progress bar
                }
                int lineCounter = 1;
                foreach (string item_ in sourcedownloadURI.ToString().Split('/'))
                {
                    Console.WriteLine(item_);
                    lineCounter++;
                }
                int lineCounter2 = 1;
                string fileName = null;
                foreach (string item_ in sourcedownloadURI.ToString().Split('/'))
                {
                    Console.WriteLine(item_);
                    if (lineCounter2 == lineCounter - 1)
                    {
                        fileName = item_;
                    }
                    lineCounter2++;
                }
                Console.WriteLine(fileName);
                download_destinationpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + fileName;
                Console.WriteLine(download_destinationpath);
                client.DownloadFileAsync(new Uri(sourcedownloadURI), download_destinationpath);
            }
            catch (Exception http_errors)
            {
                Console.WriteLine(http_errors.ToString());
            }
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) // This is our new method!
        {
            wait(100);
            if (e.Error != null)
            {
                if (showDialog == true)
                {
                    if (itemToDownloadName_ == "Update")
                    {
                        MessageBox.Show($"New Update failed to download :(");
                    }
                    else
                    {
                        MessageBox.Show($"{uwerfh} has failed to download :(");
                    }
                }
            }
            else if (!e.Cancelled)
            {
                if (showDialog == true)
                {
                    if (itemToDownloadName_ == "Update")
                    {
                        MessageBox.Show($"New update was downloaded successfully");
                        MessageBox.Show($"Thank You for using this version of OSGE14,\n sadly, its time to move on to the newer version, if you want, you can still use this version, but for security options, move on. we, the game engine at this version, will remeber you :)");
                    }
                    else
                    {
                        MessageBox.Show($"{uwerfh} was downloaded successfully");
                    }
                }
            }

            if (download_destinationpath.EndsWith(".zip"))
            {
                if (Directory.Exists(download_destinationpath.Replace(".zip", "")))
                {
                    Directory.Delete(download_destinationpath.Replace(".zip", ""));
                }
                ZipFile.ExtractToDirectory(download_destinationpath, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                if (showDialog == true)
                {
                    MessageBox.Show("We have detected the file that is downloaded is a zip file, it has been upzipped for you", "We Unzipped you file for you", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //System.Diagnostics.Process.Start(download_destinationpath);
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) // NEW
        {
            DownloadedAmountProgressBar.Value = e.ProgressPercentage;
            if (e.BytesReceived < 1000)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived} of {e.TotalBytesToReceive} Bytes downloaded";
            else if (e.BytesReceived > 1000)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived / 1000} of {e.TotalBytesToReceive / 1000} KB (Kilobyte) downloaded";
            else if (e.BytesReceived > 1000000)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived / 1000000} of {e.TotalBytesToReceive / 1000000} MB (Megabyte) downloaded";
            else if (e.BytesReceived > 1073741824)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived / 1073741824} of {e.TotalBytesToReceive / 1073741824} GB (Gigabyte) downloaded";
            else if (e.BytesReceived > 1000000000000)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived / 1000000000000} of {e.TotalBytesToReceive / 1000000000000} TB (Terabyte) downloaded";
            else if (e.BytesReceived > 1000000000000000)
                amountOfDataDownloadedText.Text = $"{e.BytesReceived / 1000000000000000} of {e.TotalBytesToReceive / 1000000000000000} PB (Petabyte) downloaded";
        }

        private void ResourceDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
                client.Dispose(); // We have to delete our client manually when we close the window or whenever you want
        }
    }
}
