using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Net;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Open_Source_GameEngine14
{
    public partial class Main_Window : Form
    {
        public Main_Window()
        {
            InitializeComponent();            
        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private static string _project_path;
        public string project_path
        {
            get
            {
                return _project_path;
            }
            set
            {
                _project_path = value;
            }
        }
        public static string externalUseFor_project_path
        {
            get
            {
                return _project_path;
            }
            set
            {

            }
        }

        private static string create_gameType_;
        public static string create_gameType
        {
            get
            {
                return create_gameType_;
            }
            set
            {
                create_gameType_ = value;
            }
        }
        class Global
        {
            public static string ProjectGUI_File_Path = _project_path + @"\projectGUI.OPG14";
            public static string Project_GameScripts_File_Path = Main_Window.externalUseFor_project_path + @"\Game_Scripts.s14c";
            public static string AppLanguage = null;
            public static bool MouseDown = false;
            //ScreenSceenGUI stuff
            public static int GUIObjectPositionX;
            public static int GUIObjectPositionY;
            public static int GUIObjectSizeX;
            public static int GUIObjectSizeY;
            public static string GUIObjectName;
            public static string GUIObjectColor;
            //ScreenSceenGUI Stuff (Text)
            public static string text_toDisplay;
            //ScreenSceenGUI Stuff (PictureBox)
            public static string picture_toDisplay;
        }

        private void Main_Window_Load(object sender, EventArgs e)
        {
            Start_or_Restart_Main_Windows();
        }

        public void Start_or_Restart_Main_Windows()
        {
            splitContainer1.Panel2.Controls.Clear();
            treeView1.Nodes.Clear();
            Global.ProjectGUI_File_Path = _project_path + @"\projectGUI.OPG14";
            //treeView1. = project_path;
            //ssageBox.Show(project_path);
            try
            {
                if (Directory.GetFiles(project_path, "*.OPG14").Length == 0)
                {
                    //MessageBox.Show("");
                    if (!File.Exists(project_path + @"\projectGUI.OPG14"))
                    {
                        using (FileStream fs = File.Create(Global.ProjectGUI_File_Path))
                        {
                            fs.Dispose();
                        }
                        File.WriteAllText(Global.ProjectGUI_File_Path, "Create.Panel(769, 64, 0, 445, Green)");
                    }
                    else
                    {
                        if (File.Exists(project_path + @"\projectGUI.OPG14"))
                        {
                            if (create_gameType == null)
                            {
                                //do Nothing
                            }
                            if (create_gameType == "2D Platformer")
                            {
                                File.WriteAllText(project_path + @"\projectGUI.OPG14", "Create.Panel(769, 64, 0, 445, Green) \n Create.Text(200, 200, 100, 100, Black,Your Text Here,text1)");
                            }
                            if (create_gameType == null)
                            {
                                //do Nothing
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("A SYSTEM IO ERROR OCCURED (Check Below for details)\n" + e.ToString(), "SYSTEM-IO_FOLDER_ERROR");
            }

            Load_Project_directory();
            //Load_Objects_list();
            LoadSceneGUI();
        }

        public void Load_Objects_list() //DO NOT USE THIS METHOD, IT WILL BRING UP AN ILLEGEL CHARACTOR EXCEPTION
        {
            string path = File.ReadAllText(Global.ProjectGUI_File_Path);
            string[] lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
            treeView1.Nodes.Clear();
            if (path.Contains("Create.Panel"))
            {
                int i = 1;
                foreach (string line in File.ReadLines(path))
                {
                    treeView1.Nodes.Add("Panel{0}", i.ToString());
                    i++;
                }
            }
        }

        public void Load_Project_directory()
        {
            string path = project_path;
            treeView1.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                {
                    currentNode.Nodes.Add(new TreeNode(file.Name));
                }
            }

            treeView1.Nodes.Add(node);

            //else
            //{
            //    MessageBox.Show("A SYSTEM IO ERROR OCCURED", "SYSTEM-IO_FOLDER_ERROR");
            //}
        }

        private Control activeControl;
        private Point previousPosition;
        public void LoadSceneGUI()
        {
            CustomProgressMessageBox messageBox = new CustomProgressMessageBox();
            messageBox.Message_Text_label = "Reloading Scene";
            messageBox.other_Message_Text_label = "Please have paitence, if it takes along time, then you can close this messagebox";
            messageBox.Show();
            string loadstuff = File.ReadAllText(Global.ProjectGUI_File_Path);
            string[] lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
            splitContainer1.Panel2.Controls.Clear();

            foreach (string line in lines)
            {
                if (line.StartsWith("Create.Panel"))
                {
                    string panel_create_command = line.ToString();
                    panel_create_command = panel_create_command.Replace("(", "");
                    panel_create_command = panel_create_command.Replace(")", "");
                    panel_create_command = panel_create_command.Replace("Create.Panel", "");
                    Console.WriteLine(panel_create_command);
                    List<string> list = new List<string>();
                    list = panel_create_command.Split(',').ToList();
                    int i = 1;
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                        if (i == 1)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 2)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 3)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 4)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 5)
                        {
                            Global.GUIObjectColor = l;
                            i++;
                        }
                        else if (i == 6)
                        {
                            Global.GUIObjectName = l;
                        }
                    }
                    Panel p = new Panel { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName;
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    //p.Paint += new PaintEventHandler(Panel_Paint);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    if (Global.GUIObjectColor == "Red")
                    {
                        p.BackColor = Color.Red;
                    }
                    else if (Global.GUIObjectColor == "Green")
                    {
                        p.BackColor = Color.Green;
                    }
                    else if (Global.GUIObjectColor == "Blue")
                    {
                        p.BackColor = Color.Blue;
                    }
                    else if (Global.GUIObjectColor == "White")
                    {
                        p.BackColor = Color.White;
                    }
                    else if (Global.GUIObjectColor == "Black")
                    {
                        p.BackColor = Color.Black;
                    }
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
                if (line.StartsWith("Create.Text"))
                {
                    string panel_create_command = line.ToString();
                    panel_create_command = panel_create_command.Replace("(", "");
                    panel_create_command = panel_create_command.Replace(")", "");
                    panel_create_command = panel_create_command.Replace("Create.Text", "");
                    Console.WriteLine(panel_create_command);
                    List<string> list = new List<string>();
                    list = panel_create_command.Split(',').ToList();
                    int i = 1;
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                        if (i == 1)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 2)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 3)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 4)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 5)
                        {
                            Global.GUIObjectColor = l;
                            i++;
                        }
                        else if (i == 6)
                        {
                            Global.text_toDisplay = l;
                            i++;
                        }
                        else if (i == 7)
                        {
                            Global.GUIObjectName = l;
                        }
                    }
                    Label p = new Label { Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName;
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    if (Global.GUIObjectColor == "Red")
                    {
                        p.ForeColor = Color.Red;
                    }
                    else if (Global.GUIObjectColor == "Green")
                    {
                        p.ForeColor = Color.Green;
                    }
                    else if (Global.GUIObjectColor == "Blue")
                    {
                        p.ForeColor = Color.Blue;
                    }
                    else if (Global.GUIObjectColor == "White")
                    {
                        p.ForeColor = Color.White;
                    }
                    else if (Global.GUIObjectColor == "Black")
                    {
                        p.ForeColor = Color.Black;
                    }
                    p.Text = Global.text_toDisplay;
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
                if (line.StartsWith("Create.Picturebox"))
                {
                    string panel_create_command = line.ToString();
                    panel_create_command = panel_create_command.Replace("(", "");
                    panel_create_command = panel_create_command.Replace(")", "");
                    panel_create_command = panel_create_command.Replace("Create.Picturebox", "");
                    Console.WriteLine(panel_create_command);
                    List<string> list = new List<string>();
                    list = panel_create_command.Split(',').ToList();
                    int i = 1;
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                        if (i == 1)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 2)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 3)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 4)
                        {
                            l.Replace(" ", "");
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                            i++;
                        }
                        else if (i == 5)
                        {
                            Global.GUIObjectColor = l;
                            i++;
                        }
                        else if (i == 6)
                        {
                            Global.picture_toDisplay = l;
                            i++;
                        }
                        else if (i == 7)
                        {
                            Global.GUIObjectName = l;
                        }
                    }
                    PictureBox p = new PictureBox { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName;
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    if (Global.GUIObjectColor == "Red")
                    {
                        p.BackColor = Color.Red;
                    }
                    else if (Global.GUIObjectColor == "Green")
                    {
                        p.BackColor = Color.Green;
                    }
                    else if (Global.GUIObjectColor == "Blue")
                    {
                        p.BackColor = Color.Blue;
                    }
                    else if (Global.GUIObjectColor == "White")
                    {
                        p.BackColor = Color.White;
                    }
                    else if (Global.GUIObjectColor == "Black")
                    {
                        p.BackColor = Color.Black;
                    }
                    Console.WriteLine(project_path + @"\" + Global.picture_toDisplay);
                    try
                    {
                        p.Image = Image.FromFile(project_path + @"\" + Global.picture_toDisplay);
                    }
                    catch
                    {
                        MessageBox.Show("Coulnd't Find the Image Specified","IMAGE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    p.BackgroundImageLayout = ImageLayout.Stretch;
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
            }
        }

        private void Panel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Global.MouseDown = true;
            activeControl = sender as Control;
            previousPosition = e.Location;
            Cursor = Cursors.SizeAll;
            LoadProperties(activeControl.Name);
        }
        private void Panel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Global.MouseDown = false;
            activeControl = null;
            ActiveControl = null;
            Cursor = Cursors.Default;
            string loadstuff = File.ReadAllText(Global.Project_GameScripts_File_Path);
            string[] lines = File.ReadAllLines(Global.Project_GameScripts_File_Path);
            /*foreach (string line in lines)
            {
                if (line.StartsWith(activeControl.Name))
                {
                    string eeeeffffgggg = line.ToString();
                    eeeeffffgggg.Substring(eeeeffffgggg.IndexOf("("));
                    eeeeffffgggg.Substring(eeeeffffgggg.IndexOf(","));
                    eeeeffffgggg.Substring(eeeeffffgggg.IndexOf(","));
                    int dyde = Convert.ToInt32(eeeeffffgggg.Substring(0, eeeeffffgggg.IndexOf(",")));
                    eeeeffffgggg.Substring(eeeeffffgggg.IndexOf(","));
                    int helpfiouvei = Convert.ToInt32(eeeeffffgggg.Substring(0, eeeeffffgggg.IndexOf(",")));
                }
            }*/
        }

        //private void Panel_Paint(object sender, PaintEventArgs e)
        //{
        //    activeControl = sender as Control;
        //    while (true)
        //    {
        //        if (Global.MouseDown == true)
        //        {
        //            ControlPaint.DrawBorder(e.Graphics, activeControl.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        //        }
        //        wait(500);
        //    }
        //}

        public void LoadProperties(string name)
        {
            foreach (Control ctrl in splitContainer1.Panel2.Controls)
            {
                if (ctrl.Name == name)
                {
                    this.NameofCTRL.Text = ctrl.Name;
                    imageNameTextBoxField.Text = null;
                    if (ctrl is Panel)
                    {                        
                        if (Global.AppLanguage == "English" || Global.AppLanguage == "Spanish")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "Panel";
                        }
                        if (Global.AppLanguage == "Hindi")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "पैनल";
                        }
                        if (Global.AppLanguage == "Arabic")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "لوحة";
                        }
                    }
                    else if (ctrl is Label)
                    {
                        if (Global.AppLanguage == "English")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "Label";
                        }
                        if (Global.AppLanguage == "Hindi")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "लेबल";
                        }
                        if (Global.AppLanguage == "Arabic")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "ملصق";
                        }
                        if (Global.AppLanguage == "Spanish")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "Etiqueta";
                        }
                    }
                    else if (ctrl is PictureBox)
                    {
                        //imageNameTextBoxField.Text = ctrl.Tag + string.Empty;
                        if (Global.AppLanguage == "English")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "PictureBox";
                        }
                        if (Global.AppLanguage == "Hindi")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "चित्र बॉक्स";
                        }
                        if (Global.AppLanguage == "Arabic")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "مربع صورة";
                        }
                        if (Global.AppLanguage == "Spanish")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "Cuadro de imagen";
                        }
                    }
                    else if (ctrl is TextBox)
                    {
                        if (Global.AppLanguage == "English")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "TextBox";
                        }
                        if (Global.AppLanguage == "Hindi")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "पाठ बॉक्स";
                        }
                        if (Global.AppLanguage == "Arabic")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "مربع الكتابة";
                        }
                    }
                    else if (ctrl is Button)
                    {
                        if (Global.AppLanguage == "English")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "Button";
                        }
                        if (Global.AppLanguage == "Hindi")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "बटन";
                        }
                        if (Global.AppLanguage == "Arabic")
                        {
                            this.TypeOFCTRL_TXTBOX.Text = "زر";
                        }
                    }
                    else if (ctrl is null)
                    {
                        this.TypeOFCTRL_TXTBOX.Text = "UNDEFINED";
                        MessageBox.Show("ALERT!, this object type is null and the engine has to delete it so no issue ocurre in the newer versions, will you delete it? if not, its ok, we will set it's type to unspecified, note that you cant ever use it unless it has a specified object type like 'panel', 'label', 'pictureBox' etc.","Object Type is null");
                    }

                }
            }
        }
        private void Panel_Hover(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }
        private void Panel_MouseLeave(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void Panel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
            {
                return;
            }
            var location = activeControl.Location;

            location.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
            activeControl.Location = location;

        }
        private void boxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string project_designer = Global.ProjectGUI_File_Path;
            string project_designer_text = File.ReadAllText(project_designer);
            // To write all of the text to the file
            string text = "\nCreate.Panel(400, 200, 100, 100, White)";        
            File.WriteAllText(project_designer, project_designer_text+text);
            LoadSceneGUI();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = folderBrowserDialog.SelectedPath;
                project_path = sSelectedPath;
                externalUseFor_project_path = sSelectedPath;
                Start_or_Restart_Main_Windows();
            }
        }

        private void Main_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunTime_Game_Window runTime_Game_Window = new RunTime_Game_Window();
            runTime_Game_Window.ShowDialog();
        }

        private void Main_Window_Shown(object sender, EventArgs e)
        {
            
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    sourceControl.BringToFront();
                }
            }           
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    sourceControl.SendToBack();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    splitContainer1.Panel2.Controls.Remove(sourceControl);
                }
            }
        }

        private int xResize_size_;
        public int xResize_size
        {
            get
            {
                return xResize_size_;
            }
            set
            {
                xResize_size_ = value;
            }
        }

        private int yResize_size_;
        public int yResize_size
        {
            get
            {
                return yResize_size_;
            }
            set
            {
                yResize_size_ = value;
            }
        }
        Control resizeControlControl;
        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    resizeControlControl = owner.SourceControl;
                }
            }
            MainWindow_ControlResizer _ControlResizer = new MainWindow_ControlResizer();
            _ControlResizer.object__ = resizeControlControl;
            _ControlResizer.xPos = resizeControlControl.Width;
            _ControlResizer.yPos = resizeControlControl.Height;
            _ControlResizer.ShowDialog();
            //_ControlResizer.FormClosed += new FormClosedEventHandler(resizerClosed);
        }

        public void resizerClosed(Control eeeeee)
        {
            MessageBox.Show("eee");
            System.Threading.Thread.Sleep(1000);
            resizeControlControl.Size = new Size(xResize_size, yResize_size);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxInfo aboutBox = new AboutBoxInfo();
            aboutBox.Show();
        }

        static char quotes = '"';
        string[] englishWriteToFile = //YES this is tedius but it gets the work done
        {
            "{\n  " + quotes + "Language" + quotes + ": " + quotes + "English" + quotes + "\n}"
        };
        string[] ArabicWriteToFile =
        {
            "{\n  " + quotes + "Language" + quotes + ": " + quotes + "Arabic" + quotes + "\n}"
        };
        string[] hindiWriteToFile =
        {
            "{\n  " + quotes + "Language" + quotes + ": " + quotes + "Hindi" + quotes + "\n}"
        };
        string[] spanishWriteToFile =
        {
            "{\n  " + quotes + "Language" + quotes + ": " + quotes + "Spanish" + quotes + "\n}"
        };
        string[] languagesSupported =
        {
            "English",
            "Arabic",
            "Hindi",
            "Spanish"
        };
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("====FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        File.WriteAllLines(filepathForLanguages, englishWriteToFile);
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error","Missing File");
            }
            this.Hide();
            wait(50);
            this.Show();
            this.Refresh();
        }

        private void arabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("====FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        File.WriteAllLines(filepathForLanguages, ArabicWriteToFile);
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
            this.Hide();
            wait(50);
            this.Show();
            this.Refresh();
        }

        private void HindiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("====FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        File.WriteAllLines(filepathForLanguages, hindiWriteToFile);
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
            this.Hide();
            wait(50);
            this.Show();
            this.Refresh();
        }

        private void SpanishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("====FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        File.WriteAllLines(filepathForLanguages, spanishWriteToFile);
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
            this.Hide();
            wait(50);
            this.Show();
            this.Refresh();
        }

        private void projectExplorerTitle_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gameEngineSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameEngineSettings engineSettings = new GameEngineSettings();
            engineSettings.ShowDialog();
        }

        private void Main_Window_Paint(object sender, PaintEventArgs e)
        {
            
        }
        public void windowTitleSet()
        {
            this.Text = "OSGE14 - " + project_path.ToString();
        }

        private void Main_Window_Activated(object sender, EventArgs e)
        {
            windowTitleSet();

            /*
             (っ◔◡◔)っ ♥ Languages \/
                You can add your own too if you want <3 ♥
                just copy the if statement and change language to whatever and then translate the words from English to what language you want
                if you want it as an option in the menu, you should see <link>
             */
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            bool isLanguageSupported = true;
            if (File.Exists(filepathForLanguages))
            {
                Console.WriteLine("======== LOADING LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        string theLine = line;
                        theLine = theLine.Replace($"{quotes}Language{quotes}: {quotes}", "");
                        theLine = theLine.Replace(quotes.ToString(), "");
                        theLine = theLine.Replace(" ", "");
                        Console.WriteLine(theLine);
                        if (theLine == "English")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "English";
                            projectExplorerTitle.Text = "Project Explorer";
                            propertiesTitleText.Text = "Properties";
                            runProjectBTN.Text = "> Run";
                            property_CTRLName.Text = "Name";
                            typeOfObjectTitleProperty.Text = "Type Of Object";
                        }
                        else if (theLine == "Hindi")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Hindi";
                            projectExplorerTitle.Text = "प्रोजेक्ट एक्सप्लोरर";
                            propertiesTitleText.Text = "गुण";
                            runProjectBTN.Text = "> भागो";
                            property_CTRLName.Text = "नाम";
                            typeOfObjectTitleProperty.Text = "वस्तु का प्रकार";
                        }
                        else if (theLine == "Arabic")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Arabic";
                            projectExplorerTitle.Text = "مستكشف المشروع";
                            propertiesTitleText.Text = "الخصائص";
                            runProjectBTN.Text = "> تشغيل";
                            property_CTRLName.Text = "اسم";
                            typeOfObjectTitleProperty.Text = "نوع الكائن";
                        }
                        else if (theLine == "Spanish")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Spanish";
                            projectExplorerTitle.Text = "Explorador de proyectos";
                            propertiesTitleText.Text = "Propiedades";
                            runProjectBTN.Text = "> Correr";
                            property_CTRLName.Text = "Nombre";
                            typeOfObjectTitleProperty.Text = "Tipo de objeto";
                        }
                        else if (theLine != "English" && theLine != "Hindi" && theLine != "Arabic" && theLine != "Spanish")
                        {
                            if (isLanguageSupported == true)
                            {
                                MessageBox.Show($"A Language Error Ocurred at\n line 41 in GameEngineSettings.cs\n The Error;\n language {theLine} doesnt have an if statement to tell what text should be displayed(in the language {theLine})");
                            }
                            isLanguageSupported = false;
                            Global.AppLanguage = null;
                            projectExplorerTitle.Text = "ÞřôJèçƭ Éжƥℓôřèř";
                            propertiesTitleText.Text = "Þřôƥèřƭïèƨ";
                            runProjectBTN.Text = "> Rúñ";
                            property_CTRLName.Text = "Ná₥è";
                            typeOfObjectTitleProperty.Text = "T¥ƥè Óƒ ÓβJèçƭ";
                        }
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
        }

        private void rightClickSceneObjectContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                }
            }
        }
    }
}
