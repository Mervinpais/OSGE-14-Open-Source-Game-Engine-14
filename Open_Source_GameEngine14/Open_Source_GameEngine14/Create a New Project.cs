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
using Newtonsoft.Json;
using System.Text.Json;

namespace Open_Source_GameEngine14
{
    public partial class Create_a_New_Project : Form
    {
        public Create_a_New_Project()
        {
            InitializeComponent();
            CheckTheItemList();
        }

        class Global
        {
            public static string Name1 = null;
            public static string Name2 = null;
            public static string Name3 = null;
        }
        public static int crash_on_close = 1;
        //static char quotes = '"';
        //string e = quotes.ToString();
        public void CheckTheItemList()
        {
            string filepathForLanguages = Environment.CurrentDirectory + @"\RecentItemsList.txt";
            string[] fhuiefhui = File.ReadAllLines(filepathForLanguages);
            Array.ForEach(fhuiefhui, Console.WriteLine);
            foreach (string line in fhuiefhui)
            {
                if (line.StartsWith("1."))
                {
                    string cfeiufui = line.Replace("1.", "");
                    Global.Name1 = cfeiufui;
                }
                if (line.StartsWith("2."))
                {
                    string cfeiufui = line.Replace("2.", "");
                    Global.Name2 = cfeiufui;
                }
                if (line.StartsWith("3."))
                {
                    string cfeiufui = line.Replace("3.", "");
                    Global.Name3 = cfeiufui;
                }
            }
            /*Console.WriteLine(Global.Name1);
            Console.WriteLine(Global.Name2);
            Console.WriteLine(Global.Name3);*/
        }
        string RecentWriteToFile1 = "1." + Global.Name1 + "\n2." + Global.Name2 + "\n3." + Global.Name3;
        private void button2_Click(object sender, EventArgs e)
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
            //======================================================================\\
            string filepathForLanguages = Environment.CurrentDirectory + @"\RecentItemsList.txt";
            Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====FOUND Recent Items =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("1."))
                    {
                        Console.WriteLine("Writing to file");
                        if (Global.Name2 != null || Global.Name3 != null)
                        {
                            Global.Name1 = path;
                        }
                        if (Global.Name2 == null || Global.Name3 == null)
                        {
                            Global.Name1 = path;
                        }
                        if (Global.Name2 == null || Global.Name3 != null)
                        {
                            Global.Name1 = path;
                        }
                        if (Global.Name2 != null || Global.Name3 == null)
                        {
                            Global.Name1 = path;
                        }
                        File.WriteAllText(filepathForLanguages, RecentWriteToFile1);
                    }
                    if (line.StartsWith("2."))
                    {
                        Console.WriteLine("Writing to file");
                        if (Global.Name1 != null || Global.Name3 != null)
                        {
                            Global.Name2 = path;
                        }
                        if (Global.Name1 == null || Global.Name3 == null)
                        {
                            Global.Name2 = path;
                        }
                        if (Global.Name1 != null || Global.Name3 == null)
                        {
                            Global.Name2 = path;
                        }
                        if (Global.Name1 == null || Global.Name3 != null)
                        {
                            Global.Name2 = path;
                        }
                        File.WriteAllText(filepathForLanguages, RecentWriteToFile1);
                    }
                    if (line.StartsWith("3."))
                    {
                        Console.WriteLine("Writing to file");
                        if (Global.Name1 != null || Global.Name2 != null)
                        {
                            Global.Name3 = path;
                        }
                        if (Global.Name1 == null || Global.Name2 == null)
                        {
                            Global.Name3 = path;
                        }
                        if (Global.Name1 != null || Global.Name2 == null)
                        {
                            Global.Name3 = path;
                        }
                        if (Global.Name1 == null || Global.Name2 != null)
                        {
                            Global.Name3 = path;
                        }
                        File.WriteAllText(filepathForLanguages, RecentWriteToFile1);
                    }
                    if (Global.Name1 != null)
                    {
                        RecentItem1BTN.Text = path;
                        Global.Name1 = path;
                    }
                    File.WriteAllText(filepathForLanguages, "1." + Global.Name1 + "\n2." + Global.Name2 + "\n3." + Global.Name3);
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND Recent items =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
            Main_Window form1 = new Main_Window();
            form1.project_path = path;
            form1.Show();
            Console.WriteLine(path);
            crash_on_close = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
            form1.Show();
            Main_Window.create_gameType = "2D Platformer";
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
    }
}
