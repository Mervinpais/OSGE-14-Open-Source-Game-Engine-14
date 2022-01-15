using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Open_Source_GameEngine14
{
    public partial class Create_a_New_Project : Form
    {
        public Create_a_New_Project()
        {
            InitializeComponent();
            updateCheckerPanel.Location = new Point(264, 210);
            LatestUpdateChecker();
        }
        class Global
        {
            public static string latestUpdateNum;
        }

        public static int crash_on_close = 1;
        //static char quotes = '"';
        //string e = quotes.ToString();
        public string searchForProjectFolder()
        {
            string path = null;
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath == null || folderBrowserDialog1.SelectedPath == @"C:\" || folderBrowserDialog1.SelectedPath == @"C:\Windows" || folderBrowserDialog1.SelectedPath == @"C:\Windows\System32")
                {
                    MessageBox.Show("Error; Please Choose a vaild path");
                }
                else
                {
                    path = folderBrowserDialog1.SelectedPath;
                }
            }
            if (path == null)
            {
                path = searchForProjectFolder();
            }
            else
            {
                return path;
            }
            return path;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string path = searchForProjectFolder();
            //======================================================================\\
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!Directory.Exists(folder + @"\.OSGE14_GameEngine_OtherData") && !File.Exists(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt"))
            {
                Directory.CreateDirectory(folder + @"\.OSGE14_GameEngine_OtherData");
                File.Create(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt");
            }
            else if (Directory.Exists(folder + @"\.OSGE14_GameEngine_OtherData") && !File.Exists(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt"))
            {
                File.Create(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt");
            }
            else
            {
                if (File.ReadAllText(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt") == null)
                {
                    File.WriteAllText(folder + @"\.OSGE14_GameEngine_OtherData\recentItem1.txt", path);
                }
            }
            //======================================================================\\
            Main_Window form1 = new Main_Window();
            form1.project_path = path;
            form1.Show();
            Console.WriteLine(path);
            crash_on_close = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = searchForProjectFolder();
            Main_Window form1 = new Main_Window();
            form1.project_path = path;
            form1.create_gameType = "2D Platformer";
            form1.Show();
            Console.WriteLine(path);
            crash_on_close = 0;
            this.Close();
        }

        private void Create_a_New_Project_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (crash_on_close == 1)
            {
                Application.Exit();
            }
            else
            {
                //this.Close();
            }
        }
        bool moving;
        Point offset;
        Point original;

        void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            TopPanel.Capture = true;
            offset = MousePosition;
            original = this.Location;
        }

        void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moving)
                return;

            int x = original.X + MousePosition.X - offset.X;
            int y = original.Y + MousePosition.Y - offset.Y;

            this.Location = new Point(x, y);
        }

        void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            TopPanel.Capture = false;
        }

        private void CloseWindowBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MaximiseWindowBTN_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MinimiseWindowBTN_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void EmptyThreeDCreateProject_Click(object sender, EventArgs e)
        {
            string path = null;
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath == null || folderBrowserDialog1.SelectedPath == @"C:\" || folderBrowserDialog1.SelectedPath == @"C:\Windows" || folderBrowserDialog1.SelectedPath == @"C:\Windows\System32")
                {
                    MessageBox.Show("Error; Please Choose a vaild path");
                    this.Close();
                    Application.Exit();
                }
                else
                {
                    path = folderBrowserDialog1.SelectedPath;
                }
            }
            Main_Window form1 = new Main_Window();
            form1.project_path = path;
            form1.create_gameType = "3D Empty";
            form1.Show();
            Console.WriteLine(path);
            crash_on_close = 0;
            this.Close();
        }

        private void EmptyThreeDCreateProject_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.EmptyThreeDCreateProject, "This Project Type has been discontinued");
        }

        private void CloseWindowBTN_MouseEnter(object sender, EventArgs e)
        {
            CloseWindowBTN.ForeColor = Color.White;
            CloseWindowBTN.BackColor = Color.Red;
        }

        private void CloseWindowBTN_MouseLeave(object sender, EventArgs e)
        {
            CloseWindowBTN.ForeColor = Color.Black;
            CloseWindowBTN.BackColor = Color.White;
        }

        private void Create_a_New_Project_Shown(object sender, EventArgs e)
        {
            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!Directory.Exists(folder + @"\.OSGE14_GameEngine_OtherData"))
            {
                Directory.CreateDirectory(folder + @"\.OSGE14_GameEngine_OtherData");
            }
        }

        private void HelpBTN_Click(object sender, EventArgs e)
        {
            if (updateCheckerPanel.Visible == false)
            {
                updateCheckerPanel.Visible = true;
                bool isAlreadyUpdated = LatestUpdateChecker();
                if (isAlreadyUpdated == true)
                {
                    notification_UpdateBTN.Enabled = false;
                }
                else if (isAlreadyUpdated == false)
                {
                    notification_UpdateBTN.Enabled = true;
                }
            }
            else
            {
                updateCheckerPanel.Visible = false;
            }
        }

        public bool LatestUpdateChecker() //True == you are already on latest update  //False == you need to update
        {
            string url = "https://pastebin.com/raw/H0LsycdY";
            WebClient webClient = new WebClient();
            string data = webClient.DownloadString(url);
            string efewh = null;
            try
            {
                string sourcedownloadURL = "https://pastebin.com/raw/H0LsycdY";
                string download_destinationpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\UpdateNumberVersion.txt";
                efewh = download_destinationpath;
                //downloadFile(sourcedownloadURL, download_destinationpath);
                using (var client = new WebClient())
                {
                    client.DownloadFile(sourcedownloadURL, download_destinationpath);
                }
            }
            catch (Exception http_errors)
            {
                Console.WriteLine(http_errors.ToString());
            }

            string htmlContent = File.ReadAllText(efewh);
            Console.WriteLine(File.ReadAllText(efewh));

            string LatestUpdateNum = htmlContent;
            Global.latestUpdateNum = LatestUpdateNum;
            bool isUpdated = false;

            string file = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\versionInfo.xml";
            XmlReader reader = XmlReader.Create(file);
            int? versionNumberMajor = null;
            int? versionNumberMinor = null;
            int? versionNumberPatch = null;
            while (reader.Read())
            {
                Console.WriteLine(reader.Name + " and value is " + reader.Value);
                if (reader.Name.ToString() == "Major")
                {
                    versionNumberMajor = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
                if (reader.Name.ToString() == "Minor")
                {
                    versionNumberMinor = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
                if (reader.Name.ToString() == "Patch")
                {
                    versionNumberPatch = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
            }
            versionPanel_Info.Text = $"Latest Version: {LatestUpdateNum}\nYour Version: v{versionNumberMajor}.{versionNumberMinor}.{versionNumberPatch}";
            versionNumber.Text = $"v{versionNumberMajor}.{versionNumberMinor}.{versionNumberPatch}";
            string[] latestUpdateVersionArray = LatestUpdateNum.Replace("v", "").Split('.');
            int UpdateItem_counter = 1;
            int? latestVersionMajor = null;
            int? latestVersionMinor = null;
            int? latestVersionPatch = null;
            foreach (string item in latestUpdateVersionArray)
            {
                if (UpdateItem_counter == 1)
                {
                    latestVersionMajor = Convert.ToInt32(item);
                }
                if (UpdateItem_counter == 2)
                {
                    latestVersionMinor = Convert.ToInt32(item);
                }
                if (UpdateItem_counter == 3)
                {
                    latestVersionPatch = Convert.ToInt32(item);
                }
                UpdateItem_counter++;
            }

            if (latestVersionMajor == versionNumberMajor)
            {
                if (latestVersionMinor == versionNumberMinor)
                {
                    if (latestVersionPatch == versionNumberPatch)
                    {
                        isUpdated = true;
                        updatePanel_Title.Text = "You Have The latest Updates";
                    }
                    else if (latestVersionPatch > versionNumberPatch)
                    {
                        isUpdated = false;
                        updatePanel_Title.Text = "Your Version is outdated";
                    }
                }
                else if (latestVersionMajor > versionNumberMajor)
                {
                    isUpdated = false;
                    updatePanel_Title.Text = "Your Version is outdated";
                }
            }
            else if (latestVersionMajor > versionNumberMajor)
            {
                isUpdated = false;
                updatePanel_Title.Text = "Your Version is outdated";
            }

            notification_UpdateBTN.BringToFront();

            Console.WriteLine(LatestUpdateNum);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\UpdateNumberVersion.txt");
            return isUpdated;
        }

        private void notification_UpdateBTN_Click(object sender, EventArgs e)
        {
            ResourceDownloader resourceDownloader = new ResourceDownloader();
            resourceDownloader.uwerfh = "https://github.com/Mervinpais/OSGE-14-Open-Source-Game-Engine-14/archive/refs/heads/main.zip";
            resourceDownloader.itemToDownloadName = "Update";
            //resourceDownloader.updateLatestVersion = Global.latestUpdateNum.ToString(); BUGGY
            resourceDownloader.ShowDialog();
        }
    }
}