using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_Source_GameEngine14
{
    public partial class Main_Window : Form
    {
        public Main_Window()
        {
            InitializeComponent();
        }

        public void wait(int milliseconds) // Use this to wait and then do whatever, the System.Threading.Thread.Sleep is a bad practice since it freezes the whole window so dont do it
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

        private static string _project_path;
        public string project_path
        {
            get { return _project_path; }
            set { _project_path = value; }
        }
        public static string externalUseFor_project_path
        {
            get { return _project_path; }
            set { }
        }

        private string create_gameType_;
        public string create_gameType
        {
            get { return create_gameType_; }
            set { create_gameType_ = value; }
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

            //Project Saving Value(s)
            public static bool ProjectSaved = false;
        }

        readonly static char quotes = '"'; // this is here so if i wanted to use the '"' char the it would look like this """ which will Cause C# to have error since 2 qoutes are there but the only 3 chars of '"'

        private void Main_Window_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(project_path + @"\projectGUI.OPG14"))
                {
                    if (!File.Exists(project_path + @"\NotNeedingProjectGUI.OSGE14_SETTING_FILE"))
                    {
                        string[] textLines1 = { "Create.Panel(25, 268, 175, 300, Green, panel1)" };
                        File.WriteAllLines(Global.ProjectGUI_File_Path, textLines1);
                    }
                }
            }
            catch (Exception systemIOException_e)
            {
                MessageBox.Show("A SYSTEM IO ERROR OCCURED (Check Below for details)\n" + systemIOException_e.Message.ToString(), "SYSTEM-IO_FOLDER_ERROR");
            }
        }

        private void Main_Window_Shown(object sender, EventArgs e)
        {
            Start_or_Restart_Main_Windows();
        }

        public void Start_or_Restart_Main_Windows()
        {
            splitContainer1.Panel2.Controls.Clear();
            treeView1.Nodes.Clear();
            Global.ProjectGUI_File_Path = _project_path + @"\projectGUI.OPG14";

            Load_Project_directory();
            //Load_Objects_list();
            LoadSceneGUI();
            if (create_gameType == "3D Empty")
            {
                MessageBox.Show("Error; 3D Doesnt work Yet");
            }
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
                    //This is the Normal RGB + Black and White colors
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
                    //Other Colors
                    else if (Global.GUIObjectColor == "Orange")
                    {
                        p.BackColor = Color.Orange;
                    }
                    else if (Global.GUIObjectColor == "Yellow")
                    {
                        p.BackColor = Color.Yellow;
                    }
                    else if (Global.GUIObjectColor == "Cyan")
                    {
                        p.BackColor = Color.Cyan;
                    }
                    else if (Global.GUIObjectColor == "DarkBlue")
                    {
                        p.BackColor = Color.DarkBlue;
                    }
                    else if (Global.GUIObjectColor == "Brown")
                    {
                        p.BackColor = Color.Brown;
                    }
                    else
                    {
                        try
                        {
                            p.BackColor = Color.FromName(Global.GUIObjectColor);
                        }
                        catch
                        {
                            try
                            {
                                p.BackColor = Color.FromArgb(Convert.ToInt32(Global.GUIObjectColor));
                            }
                            catch (Exception ExceptionError2)
                            {
                                MessageBox.Show($"An Exception Error Occurred Related to the Color of the Object '{p.Name}', You Tried to Set the Color to a Color that doesnt exist (in the game engine) {Environment.NewLine} Here is the Complex info {ExceptionError2.Message}");
                            }
                        }
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
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        }
                        else if (i == 2)
                        {
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        }
                        else if (i == 3)
                        {
                            Global.GUIObjectColor = l;
                        }
                        else if (i == 4)
                        {
                            Global.text_toDisplay = l;
                        }
                        else if (i == 5)
                        {
                            Global.GUIObjectName = l;
                        }
                        i++;
                    }
                    Label p = new Label { Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.AutoSize = true;
                    p.Name = Global.GUIObjectName;
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    //This is the Normal RGB + Black and White colors
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
                    //Other Colors
                    else
                    {
                        try
                        {
                            p.ForeColor = Color.FromName(Global.GUIObjectColor);
                        }
                        catch
                        {
                            try
                            {
                                p.ForeColor = Color.FromArgb(Convert.ToInt32(Global.GUIObjectColor));
                            }
                            catch (Exception ExceptionError2)
                            {
                                MessageBox.Show($"An Exception Error Occurred Related to the Color of the Object '{p.Name}', You Tried to Set the Color to a Color that doesnt exist (in the game engine) {Environment.NewLine} Here is the Complex info {ExceptionError2.Message}");
                            }
                        }
                    }
                    p.Text = Global.text_toDisplay.Replace(@"\n", Environment.NewLine);
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
                            Global.picture_toDisplay = l.TrimStart();
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
                    //This is the Normal RGB + Black and White colors
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
                    //Other Colors
                    else
                    {
                        try
                        {
                            p.BackColor = Color.FromName(Global.GUIObjectColor);
                        }
                        catch
                        {
                            try
                            {
                                p.BackColor = Color.FromArgb(Convert.ToInt32(Global.GUIObjectColor));
                            }
                            catch (Exception ExceptionError2)
                            {
                                MessageBox.Show($"An Exception Error Occurred Related to the Color of the Object '{p.Name}', You Tried to Set the Color to a Color that doesnt exist (in the game engine) {Environment.NewLine} Here is the Complex info {ExceptionError2.Message}");
                            }
                        }
                    }
                    Console.WriteLine(project_path + @"\" + Global.picture_toDisplay);
                    try
                    {
                        p.BackgroundImage = Image.FromFile(project_path + @"\" + Global.picture_toDisplay);
                        p.BackgroundImage.Tag = project_path + @"\" + Global.picture_toDisplay;
                    }
                    catch
                    {
                        MessageBox.Show("Coulnd't Find the Image Specified", "IMAGE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    p.BackgroundImageLayout = ImageLayout.Stretch;
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
                if (line.StartsWith("Create.Button"))
                {
                    string panel_create_command = line.ToString();
                    panel_create_command = panel_create_command.Replace("(", "");
                    panel_create_command = panel_create_command.Replace(")", "");
                    panel_create_command = panel_create_command.Replace("Create.Button", "");
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
                            l.Trim();
                            Global.GUIObjectColor = l;
                            i++;
                        }
                        else if (i == 6)
                        {
                            l.Trim();
                            Global.text_toDisplay = l;
                            i++;
                        }
                        else if (i == 7)
                        {
                            l.Trim();
                            Global.GUIObjectName = l;
                        }
                    }
                    Button p = new Button { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName;
                    p.Text = Global.text_toDisplay.Trim();
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    //p.Paint += new PaintEventHandler(Panel_Paint);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    //This is the Normal RGB + Black and White colors
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
                    //Other Colors
                    else
                    {
                        try
                        {
                            p.BackColor = Color.FromName(Global.GUIObjectColor);
                        }
                        catch
                        {
                            try
                            {
                                p.BackColor = Color.FromArgb(Convert.ToInt32(Global.GUIObjectColor));
                            }
                            catch (Exception ExceptionError2)
                            {
                                MessageBox.Show($"An Exception Error Occurred Related to the Color of the Object '{p.Name}', You Tried to Set the Color to a Color that doesnt exist (in the game engine) {Environment.NewLine} Here is the Complex info {ExceptionError2.Message}");
                            }
                        }
                    }
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
                if (line.StartsWith("Create.Music"))
                {
                    string panel_create_command = line.ToString();
                    panel_create_command = panel_create_command.Replace("(", "");
                    panel_create_command = panel_create_command.Replace(")", "");
                    panel_create_command = panel_create_command.Replace("Create.Music", "");
                    Console.WriteLine(panel_create_command);
                    List<string> list = new List<string>();
                    list = panel_create_command.Split(',').ToList();
                    int i = 1;
                    string audio_toLoad = null;
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                        if (i == 1)
                        {
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        }
                        else if (i == 2)
                        {
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        }
                        else if (i == 3)
                        {
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                        }
                        else if (i == 4)
                        {
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                        }
                        else if (i == 5)
                        {
                            audio_toLoad = l;
                        }
                        else if (i == 6)
                        {
                            Global.GUIObjectName = l;
                        }
                        i++;
                    }
                    Panel p = new Panel { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    p.BackColor = Color.White;
                    p.Name = Global.GUIObjectName;
                    p.MouseDown += new MouseEventHandler(Panel_MouseDown);
                    p.MouseUp += new MouseEventHandler(Panel_MouseUp);
                    p.MouseMove += new MouseEventHandler(Panel_MouseMove);
                    p.ContextMenuStrip = rightClickSceneObjectContextMenuStrip;
                    Console.WriteLine(project_path + @"\" + audio_toLoad);
                    try
                    {
                        p.Tag = audio_toLoad;
                    }
                    catch
                    {
                        MessageBox.Show("Coulnd't Find the Audio Specified", "IMAGE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    splitContainer1.Panel2.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////

            string lastObject = null;
            List<string> liiist = new List<string>(lines.ToList());
            liiist.RemoveAll(x => string.IsNullOrEmpty(x));
            Console.WriteLine(String.Join(Environment.NewLine, liiist));
            lines = liiist.ToArray();
            
            foreach (string line in lines)
            {
                if (line != " " || line != Environment.NewLine || line != "" || line != null)
                {
                    try
                    {
                        if (line.IndexOf(',') != 0)
                        {
                            if (line.Substring(line.LastIndexOf(','), line.Length - line.LastIndexOf(',')).Trim() == lastObject)
                            {
                                this.Visible = false;
                                MessageBox.Show($"Sorry but an UNEXPECTED Error has occured, it seems that the Project has 2 items named {lastObject}, please PLEASE change the names, the Game Engine wont break but it will move both of the items which you might not want to do", "Error: 2 Items Have Same Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                wait(500);
                                Application.Exit();
                            }
                            lastObject = line.Substring(line.LastIndexOf(','), line.Length - line.LastIndexOf(',')).Trim();
                        }
                    }
                    catch (Exception argOutOfRangeException)
                    {
                        MessageBox.Show($"An Exception was excecuted as an error occurred, the last read message \n {argOutOfRangeException.Message}");
                    }
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
            Control ctrl = sender as Control;
            Global.MouseDown = false;
            Global.ProjectSaved = false;
            activeControl = null;
            ActiveControl = null;
            this.CtrlPosXTextBoxField.Text = ctrl.Location.X.ToString();
            this.CtrlPosYTextBoxField.Text = ctrl.Location.Y.ToString();
            this.CtrlSizeXTextBoxField.Text = ctrl.Size.Width.ToString();
            this.CtrlSizeYTextBoxField.Text = ctrl.Size.Height.ToString();
            Cursor = Cursors.Default;
            if (File.Exists(Global.Project_GameScripts_File_Path))
            {
                string loadstuff = File.ReadAllText(Global.Project_GameScripts_File_Path);
                string[] liness = File.ReadAllLines(Global.Project_GameScripts_File_Path);
            }
            else if (!File.Exists(Global.Project_GameScripts_File_Path))
            {
                if (!File.Exists(_project_path + @"\NotNeedingGameScripts.OSGE14_SETTING_FILE"))
                {
                    DialogResult d = MessageBox.Show("A BIG Error Occurred, There is no 'Game_Scripts.s14c' in the Project Folder, But Dont Worry, We Can Fix It by making one for you, please click 'Yes' to create it or 'No' to not make one", "A BIG Error Occurred", MessageBoxButtons.YesNo);
                    if (d == DialogResult.Yes)
                    {
                        string[] textLines1 = { " " };
                        File.WriteAllLines(Global.Project_GameScripts_File_Path, textLines1);
                        Load_Project_directory();
                    }
                    else if (d == DialogResult.No)
                    {
                        MessageBox.Show("Ok, Please note you will get this notification again and again and again unless you go to settings and disable 'Needing Game Scripts' under Personlisation", "Ok but..");
                    }
                }
            }
        }

        public void LoadProperties(string name)
        {
            foreach (Control ctrl in splitContainer1.Panel2.Controls)
            {
                if (ctrl.Name == name)
                {
                    this.NameofCTRL.Text = ctrl.Name;
                    imageNameTextBoxField.Text = null;
                    this.CtrlPosXTextBoxField.Text = ctrl.Location.X.ToString();
                    this.CtrlPosYTextBoxField.Text = ctrl.Location.Y.ToString();
                    this.CtrlSizeXTextBoxField.Text = ctrl.Size.Width.ToString();
                    this.CtrlSizeYTextBoxField.Text = ctrl.Size.Height.ToString();

                    if (ctrl.Tag != null)
                    {
                        if (this.CTRLPropertyAudioPlayer.URL != project_path + @"\" + ctrl.Tag)
                        {
                            this.CTRLPropertyAudioPlayer.URL = project_path + @"\" + ctrl.Tag;
                        }
                    }
                    else if (ctrl.Tag == null)
                    { this.CTRLPropertyAudioPlayer.URL = null; }

                    if (ctrl is Panel)
                    {
                        if (Global.AppLanguage == "English" || Global.AppLanguage == "Spanish")
                            this.TypeOFCTRL_TXTBOX.Text = "Panel";

                        if (Global.AppLanguage == "Hindi")
                            this.TypeOFCTRL_TXTBOX.Text = "पैनल";

                        if (Global.AppLanguage == "Arabic")
                            this.TypeOFCTRL_TXTBOX.Text = "لوحة";
                    }
                    else if (ctrl is Label)
                    {
                        if (Global.AppLanguage == "English")
                            this.TypeOFCTRL_TXTBOX.Text = "Label";

                        if (Global.AppLanguage == "Hindi")
                            this.TypeOFCTRL_TXTBOX.Text = "लेबल";

                        if (Global.AppLanguage == "Arabic")
                            this.TypeOFCTRL_TXTBOX.Text = "ملصق";

                        if (Global.AppLanguage == "Spanish")
                            this.TypeOFCTRL_TXTBOX.Text = "Etiqueta";
                    }
                    else if (ctrl is PictureBox)
                    {
                        //imageNameTextBoxField.Text = ctrl.Tag + string.Empty;
                        if (Global.AppLanguage == "English")
                            this.TypeOFCTRL_TXTBOX.Text = "PictureBox";

                        if (Global.AppLanguage == "Hindi")
                            this.TypeOFCTRL_TXTBOX.Text = "चित्र बॉक्स";

                        if (Global.AppLanguage == "Arabic")
                            this.TypeOFCTRL_TXTBOX.Text = "مربع صورة";

                        if (Global.AppLanguage == "Spanish")
                            this.TypeOFCTRL_TXTBOX.Text = "Cuadro de imagen";
                    }
                    else if (ctrl is TextBox)
                    {
                        if (Global.AppLanguage == "English")
                            this.TypeOFCTRL_TXTBOX.Text = "TextBox";

                        if (Global.AppLanguage == "Hindi")
                            this.TypeOFCTRL_TXTBOX.Text = "पाठ बॉक्स";

                        if (Global.AppLanguage == "Arabic")
                            this.TypeOFCTRL_TXTBOX.Text = "مربع الكتابة";
                    }
                    else if (ctrl is Button)
                    {
                        if (Global.AppLanguage == "English")
                            this.TypeOFCTRL_TXTBOX.Text = "Button";

                        if (Global.AppLanguage == "Hindi")
                            this.TypeOFCTRL_TXTBOX.Text = "बटन";

                        if (Global.AppLanguage == "Arabic")
                            this.TypeOFCTRL_TXTBOX.Text = "زر";
                    }
                    else if (ctrl is null)
                    {
                        this.TypeOFCTRL_TXTBOX.Text = "UNDEFINED";
                        MessageBox.Show("ALERT!, this object type is null and the engine has to delete it so no issue ocurre in the newer versions, will you delete it? if not, its ok, we will set it's type to unspecified, note that you cant ever use it unless it has a specified object type like 'panel', 'label', 'pictureBox' etc.", "Object Type is null");
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
            Panel panelToAdd = new Panel();
            panelToAdd.Name = "Box1";
            panelToAdd.Location = new Point(100,100);
            panelToAdd.Size = new Size(75, 75);
            panelToAdd.BackColor = Color.White;
            panelToAdd.BringToFront();
            splitContainer1.Panel2.Controls.Add(panelToAdd);
            panelToAdd.BringToFront();
            Save_ProjectData();
            LoadSceneGUI();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label textToAdd = new Label();
            textToAdd.Name = "Text1";
            textToAdd.Text = "This is some text";
            textToAdd.Location = new Point(100, 100);
            textToAdd.AutoSize = true;
            textToAdd.BringToFront();
            splitContainer1.Panel2.Controls.Add(textToAdd);
            textToAdd.BringToFront();
            Save_ProjectData();
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
            if (Global.ProjectSaved == false)
            {
                DialogResult dResult = MessageBox.Show("ALERT You Have Not Saved your project! Do You Want To Save Right Now Before Closing?", "Unsaved Project", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dResult == DialogResult.Yes)
                {
                    Global.ProjectSaved = true;
                    Save_ProjectData();
                }
            }
        }

        private void Main_Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save_ProjectData();
            RunTime_Game_Window runTime_Game_Window = new RunTime_Game_Window();
            runTime_Game_Window.ProjectDirIncase = project_path;
            runTime_Game_Window.ShowDialog();
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
                    string[] lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
                    string[] linesForFile = File.ReadAllLines(Global.ProjectGUI_File_Path);
                    int lineCount = 1;
                    int removeLinePos = 0;
                    foreach (string line in lines)
                    {
                        if (line.EndsWith($"{sourceControl})"))
                        {
                            removeLinePos = lineCount;
                        }
                        lineCount++;
                    }
                    Console.WriteLine(String.Join(Environment.NewLine, linesForFile) + " is the original");
                    List<string> linesForFile_list = linesForFile.ToList();
                    linesForFile_list.RemoveAt(removeLinePos);
                    Console.WriteLine(String.Join(Environment.NewLine, linesForFile_list) + " is the list version");
                    linesForFile = linesForFile_list.ToArray();
                    Console.WriteLine(String.Join(Environment.NewLine, linesForFile) + " is the result");
                    File.WriteAllLines(_project_path + @"\projectGUI.OPG14", linesForFile);
                    Console.WriteLine("Finished Editing File");
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
            if (resizeControlControl is Label)
            {
                MessageBox.Show("Alert; You CAN NOT resize the text object, due to size may be bigger than the text area size to cover the text", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MainWindow_ControlResizer _ControlResizer = new MainWindow_ControlResizer(this.LoadSceneGUI);
                _ControlResizer.object__ = resizeControlControl;
                _ControlResizer.xPos = resizeControlControl.Width;
                _ControlResizer.yPos = resizeControlControl.Height;
                _ControlResizer._project_path = project_path;
                _ControlResizer.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxInfo aboutBox = new AboutBoxInfo();
            aboutBox.Show();
        }

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
        string[] russionWriteToFile =
        {
            "{\n  " + quotes + "Language" + quotes + ": " + quotes + "Russian" + quotes + "\n}"
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
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
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
            this.Refresh();
        }

        private void RussianToolStripMenuItem_Click(object sender, EventArgs e)
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
                        File.WriteAllLines(filepathForLanguages, russionWriteToFile);
                    }
                }
            }
            else if (!File.Exists(filepathForLanguages))
            {
                Console.WriteLine("====DID NOT FOUND LANGUAGES SETTINGS =============");
                MessageBox.Show("LanguageSettings.json is missing or corrupted, please add/replace it to fix this error", "Missing File");
            }
            this.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gameEngineSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameEngineSettings engineSettings = new GameEngineSettings();
            engineSettings.projectPath = _project_path;
            engineSettings.ShowDialog();
        }

        public void windowTitleSet()
        {
            this.Text = "OSGE14 - " + project_path.ToString();
        }

        private void Main_Window_Activated(object sender, EventArgs e)
        {
            windowTitleSet();
            LoadSceneGUI();

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
                            runProjectBTN.Text = "▷ Run";
                            property_CTRLName.Text = "Name";
                            typeOfObjectTitleProperty.Text = "Type Of Object";
                            Ctrl_Pos_X_PropertyText.Text = "X Pos";
                            Ctrl_Pos_Y_PropertyText.Text = "Y Pos";
                            Ctrl_Size_X_PropertyText.Text = "X Size";
                            Ctrl_Size_Y_PropertyText.Text = "Y Size";
                        }
                        else if (theLine == "Hindi")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Hindi";
                            projectExplorerTitle.Text = "प्रोजेक्ट एक्सप्लोरर";
                            propertiesTitleText.Text = "गुण";
                            runProjectBTN.Text = "▷ भागो";
                            property_CTRLName.Text = "नाम";
                            typeOfObjectTitleProperty.Text = "वस्तु का प्रकार";
                            Ctrl_Pos_X_PropertyText.Text = "x स्थिति";
                            Ctrl_Pos_Y_PropertyText.Text = "y स्थिति";
                            Ctrl_Size_X_PropertyText.Text = "x आकार";
                            Ctrl_Size_Y_PropertyText.Text = "y आकार";
                        }
                        else if (theLine == "Arabic")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Arabic";
                            projectExplorerTitle.Text = "مستكشف المشروع";
                            propertiesTitleText.Text = "الخصائص";
                            runProjectBTN.Text = "▷ تشغيل";
                            property_CTRLName.Text = "اسم";
                            typeOfObjectTitleProperty.Text = "نوع الكائن";
                            Ctrl_Pos_X_PropertyText.Text = "موقع ص";
                            Ctrl_Pos_Y_PropertyText.Text = "موقف ص";
                            Ctrl_Size_X_PropertyText.Text = "حجم x";
                            Ctrl_Size_Y_PropertyText.Text = "حجم y";
                        }
                        else if (theLine == "Spanish")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Spanish";
                            projectExplorerTitle.Text = "Explorador de proyectos";
                            propertiesTitleText.Text = "Propiedades";
                            runProjectBTN.Text = "▷ Correr";
                            property_CTRLName.Text = "Nombre";
                            typeOfObjectTitleProperty.Text = "Tipo de objeto";
                            Ctrl_Pos_X_PropertyText.Text = "Pos X";
                            Ctrl_Pos_Y_PropertyText.Text = "Pos Y";
                            Ctrl_Size_X_PropertyText.Text = "X tamaño";
                            Ctrl_Size_Y_PropertyText.Text = "Y tamaño";
                        }
                        else if (theLine == "Russian")
                        {
                            isLanguageSupported = true;
                            Global.AppLanguage = "Russian";
                            projectExplorerTitle.Text = "Обозреватель проекта";
                            propertiesTitleText.Text = "характеристики";
                            runProjectBTN.Text = "▷ Запустить проект";
                            property_CTRLName.Text = "Имя";
                            typeOfObjectTitleProperty.Text = "Тип объекта";
                            Ctrl_Pos_X_PropertyText.Text = "X Pos";
                            Ctrl_Pos_Y_PropertyText.Text = "Y Pos";
                            Ctrl_Size_X_PropertyText.Text = "Размер X";
                            Ctrl_Size_Y_PropertyText.Text = "Размер Y";
                        }
                        else if (theLine != "English" && theLine != "Hindi" && theLine != "Arabic" && theLine != "Spanish" && theLine != "Russian")
                        {
                            if (isLanguageSupported == true)
                            {
                                MessageBox.Show($"A Language Error Ocurred at\n line ??? in GameEngineSettings.cs\n The Error;\n language {theLine} doesnt have an if statement to tell what text should be displayed(in the language {theLine})");
                            }
                            isLanguageSupported = false;
                            Global.AppLanguage = null;
                            projectExplorerTitle.Text = "ÞřôJèçƭ Éжƥℓôřèř";
                            propertiesTitleText.Text = "Þřôƥèřƭïèƨ";
                            runProjectBTN.Text = "▷ Rúñ";
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

        Control reNamingControl;
        private void reNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    reNamingControl = owner.SourceControl;
                }
            }
            ReNameOrNameObject renameOrNameObject = new ReNameOrNameObject();
            renameOrNameObject.ReNamingOrNaming = 2;
            renameOrNameObject.Object_ = reNamingControl;
            renameOrNameObject.ShowDialog(this);
        }

        public Control renamingControl
        {
            get { return reNamingControl; }
            set { }
        }

        public Control SceneGUIObjects
        {
            get { return this.splitContainer1.Panel2; }
            set { }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_ProjectData();
        }

        private void helpBTN_Click(object sender, EventArgs e)
        {
            HelpWithOSGE_Fourteen helpWithOSGE_Fourteen = new HelpWithOSGE_Fourteen();
            helpWithOSGE_Fourteen.ShowDialog();
        }

        public void Save_ProjectData()
        {
            string[] lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
            List<string> lines_list = new List<string>();
            foreach (Control control in this.splitContainer1.Panel2.Controls)
            {
                string backColorOfControl = control.BackColor.ToString().Replace("Color [", "").Replace("]", "").Trim();
                string foreColorOfControl = control.ForeColor.ToString().Replace("Color [", "").Replace("]", "").Trim();
                string ControlName = control.Name.Trim();
                Console.WriteLine(control.Location.X);
                Console.WriteLine(control.Location.Y);
                Console.WriteLine(control.Size.Width);
                Console.WriteLine(control.Size.Height);
                Console.WriteLine(control.BackColor.ToString().Replace("Color [", "").Replace("]", "").Trim());
                Console.WriteLine(control.Location.X);
                Console.WriteLine(control.Name);
                Console.WriteLine(control.Text);
                if (control is Panel)
                {
                    if (control.Tag != null) //? This if statement will/might get removed or at the very most, refactored..
                    {
                        lines_list.Add($"Create.Music({control.Location.X}, {control.Location.Y}, {control.Size.Width}, {control.Size.Height},{control.Tag}, {ControlName})");
                    }
                    else
                    {
                        lines_list.Add($"Create.Panel({control.Location.X}, {control.Location.Y}, {control.Size.Width}, {control.Size.Height}, {backColorOfControl}, {ControlName})");
                    }
                }
                else if (control is Label)
                {
                    lines_list.Add($"Create.Text({control.Location.X}, {control.Location.Y}, {foreColorOfControl}, {control.Text.Replace(Environment.NewLine, @"\n")}, {ControlName})");
                }
                else if (control is PictureBox)
                {
                    lines_list.Add($"Create.Picturebox({control.Location.X}, {control.Location.Y}, {control.Size.Width}, {control.Size.Height}, {backColorOfControl}, {control.BackgroundImage}, {ControlName})");
                }
                else if (control is Button)
                {
                    lines_list.Add($"Create.Button({control.Location.X}, {control.Location.Y}, {control.Size.Width}, {control.Size.Height}, {backColorOfControl}, {control.Text}, {ControlName})");
                }
            }
            Console.WriteLine(String.Join(Environment.NewLine, lines_list));
            //File.WriteAllText(_project_path + @"\projectGUI.OPG14", String.Empty);
            File.Delete(_project_path + @"\projectGUI.OPG14");
            if (!File.Exists(_project_path + @"\projectGUI.OPG14"))
            {
                Console.WriteLine("Deleted to old data files!");
            }
            File.WriteAllText(_project_path + @"\projectGUI.OPG14", String.Join(Environment.NewLine, lines_list));
            wait(1000);
            Console.WriteLine("Finished Saving new project data");
            LoadSceneGUI();
        }

        private void seePerformanceMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformanceMonitor performanceMonitor = new PerformanceMonitor();
            performanceMonitor.Show();
        }

        private void CTRLPropertyAudioPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            Console.WriteLine("the Media loaded is " + CTRLPropertyAudioPlayer.URL);
        }

        private void Main_Window_KeyDown(object sender, KeyEventArgs e) //Save Project
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                Save_ProjectData();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O) //Open a Project
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
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.E) //Settings
            {
                GameEngineSettings engineSettings = new GameEngineSettings();
                engineSettings.projectPath = _project_path;
                engineSettings.ShowDialog();
            }
        }
    }
}