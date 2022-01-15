using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace Open_Source_GameEngine14
{
    public partial class RunTime_Game_Window : Form
    {
        public RunTime_Game_Window()
        {
            InitializeComponent();
        }

        class Global
        {
            //Main GameEngine Stuff (Helps in running the engine and just general stuff)
            public static string project_dir = Main_Window.externalUseFor_project_path;
            public static string ProjectGUI_File_Path = project_dir + @"\projectGUI.OPG14";
            public static string Project_GameScripts_File_Path = project_dir + @"\Game_Scripts.s14c";
            public static bool game_stopped = false;
            
            //ScreenSceenGUI stuff (All)
            public static int GUIObjectPositionX;
            public static int GUIObjectPositionY;
            public static int GUIObjectSizeX;
            public static int GUIObjectSizeY;
            public static string GUIObjectName;
            public static string GUIObjectColor;
            
            //ScreenSceenGUI Stuff (Text)
            public static string text_toDisplay;
            public static int line_count;

            //if Statement stuff
            public static bool IfStatementConditionsAreMet = false;

            //math statement stuff
            public static int? mathAnswer = 0;
        }

        public class ProjectPathError : Exception
        {
            public ProjectPathError(string message) : base($"The path '{message}' doesnt seem to be accessable to the program, please make sure that the path exists :(") { }
            public ProjectPathError() { }
        }
        public void wait(int milliseconds)
        {
            var timer1 = new Timer();
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

        private string ProjectDirIncase_;
        public string ProjectDirIncase
        {
            get { return ProjectDirIncase_; }
            set { ProjectDirIncase_ = value; }
        }

        public void WriteToGameLogger(string text, string typeOfMessage = null)
        {
            Game_Logger.ForeColor = Color.White;
            if (typeOfMessage == "info" || typeOfMessage == null || typeOfMessage == "information" || typeOfMessage == "i" || typeOfMessage == "INFO" || typeOfMessage == "I" || typeOfMessage == "INFORMATION")
            {
                Game_Logger.Text = $"{Game_Logger.Text}\n Info: {text}";
            }
            else if (typeOfMessage == "warn" || typeOfMessage == "Warning" || typeOfMessage == "WARNING" || typeOfMessage == "W" || typeOfMessage == "w" || typeOfMessage == "Warn" || typeOfMessage == "WARN")
            {
                Game_Logger.Text = $"{Game_Logger.Text}\n WARNING: {text}";
            }
            else if (typeOfMessage == "error" || typeOfMessage == "Error" || typeOfMessage == "ERROR" || typeOfMessage == "E" || typeOfMessage == "e" || typeOfMessage == "Err" || typeOfMessage == "ERR" || typeOfMessage == "err")
            {
                Game_Logger.Text = $"{Game_Logger.Text}\n ERROR: {text}";
            }
        }

        public static void Write(string text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch
            {
                Console.WriteLine("An Error occured while sending text to console");
            }
        }


        /*            
                                         ████████╗██╗  ██╗███████╗   ██████╗ █████╗ ███████╗███╗  ██╗███████╗
                                         ╚══██╔══╝██║  ██║██╔════╝  ██╔════╝██╔══██╗██╔════╝████╗ ██║██╔════╝
                                            ██║   ███████║█████╗    ╚█████╗ ██║  ╚═╝█████╗  ██╔██╗██║█████╗
                                            ██║   ██╔══██║██╔══╝     ╚═══██╗██║  ██╗██╔══╝  ██║╚████║██╔══╝
                                            ██║   ██║  ██║███████╗  ██████╔╝╚█████╔╝███████╗██║ ╚███║███████╗
                                            ╚═╝   ╚═╝  ╚═╝╚══════╝  ╚═════╝  ╚════╝ ╚══════╝╚═╝  ╚══╝╚══════╝
                                                  ██╗      █████╗  █████╗ ██████╗ ███████╗██████╗
                                                  ██║     ██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗
                                                  ██║     ██║  ██║███████║██║  ██║█████╗  ██████╔╝
                                                  ██║     ██║  ██║██╔══██║██║  ██║██╔══╝  ██╔══██╗
                                                  ███████╗╚█████╔╝██║  ██║██████╔╝███████╗██║  ██║
                                                  ╚══════╝ ╚════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝
         */
        /// <summary>
        /// Method <c>LoadSceneGUI()</c> Loads The Main Scene on Runtime, this is the Main fundimental part of the runtime window because without it, the gameScripts wouldnt even work
        /// <example>LoadSceneGUI() //thats literally it to loading scene gui, no parameters are needed</example>
        /// </summary>
        ///
        public void LoadSceneGUI()
        {
            Write("==============Runtime-Game-Window-Code-below==================");
            CustomProgressMessageBox messageBox = new CustomProgressMessageBox();
            messageBox.Message_Text_label = "Reloading Scene";
            messageBox.other_Message_Text_label = "Please have paitence, if it takes along time, then you can close this messagebox";
            messageBox.Show();
            string loadstuff = File.ReadAllText(Global.ProjectGUI_File_Path);
            string[] lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
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
                        l.Trim();
                        if (i == 1)
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        else if (i == 2)
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        else if (i == 3)
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                        else if (i == 4)
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                        else if (i == 5)
                            Global.GUIObjectColor = l;
                        else if (i == 6)
                            Global.GUIObjectName = l;
                        i++;
                    }
                    Panel p = new Panel { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName.Trim();
                    if (Global.GUIObjectColor == "Red")
                        p.BackColor = Color.Red;
                    else if (Global.GUIObjectColor == "Green")
                        p.BackColor = Color.Green;
                    else if (Global.GUIObjectColor == "Blue")
                        p.BackColor = Color.Blue;
                    else if (Global.GUIObjectColor == "White")
                        p.BackColor = Color.White;
                    else if (Global.GUIObjectColor == "Black")
                        p.BackColor = Color.Black;
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
                    this.game_window.Controls.Add(p);
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
                        l.Trim();
                        if (i == 1)
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        else if (i == 2)
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        else if (i == 3)
                            Global.GUIObjectColor = l;
                        else if (i == 4)
                            Global.text_toDisplay = l;
                        else if (i == 5)
                            Global.GUIObjectName = l;
                        i++;
                    }
                    Label p = new Label { Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    p.AutoSize = true;
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName.Trim();
                    if (Global.GUIObjectColor == "Red")
                        p.ForeColor = Color.Red;
                    else if (Global.GUIObjectColor == "Green")
                        p.ForeColor = Color.Green;
                    else if (Global.GUIObjectColor == "Blue")
                        p.ForeColor = Color.Blue;
                    else if (Global.GUIObjectColor == "White")
                        p.ForeColor = Color.White;
                    else if (Global.GUIObjectColor == "Black")
                        p.ForeColor = Color.Black;
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
                    this.game_window.Controls.Add(p);
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
                    string image_toLoad = null;
                    int i = 1;
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                        if (i == 1)
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        else if (i == 2)
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        else if (i == 3)
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                        else if (i == 4)
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                        else if (i == 5)
                            Global.GUIObjectColor = l;
                        else if (i == 6)
                            image_toLoad = l;
                        else if (i == 7)
                            Global.GUIObjectName = l;
                        i++;
                    }
                    Console.WriteLine(image_toLoad);
                    PictureBox p = new PictureBox { Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY), Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName.Trim();
                    p.BackgroundImageLayout = ImageLayout.Stretch;
                    try
                    {
                        p.BackgroundImage = Image.FromFile(Global.project_dir + @"\" + image_toLoad.TrimStart());
                    }
                    catch
                    {
                        p.BackgroundImage = p.ErrorImage;
                    }
                    if (Global.GUIObjectColor == "Red")
                        p.BackColor = Color.Red;
                    else if (Global.GUIObjectColor == "Green")
                        p.BackColor = Color.Green;
                    else if (Global.GUIObjectColor == "Blue")
                        p.BackColor = Color.Blue;
                    else if (Global.GUIObjectColor == "White")
                        p.BackColor = Color.White;
                    else if (Global.GUIObjectColor == "Black")
                        p.BackColor = Color.Black;
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
                    this.game_window.Controls.Add(p);
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
                        l.Trim();
                        if (i == 1)
                            Global.GUIObjectPositionX = Convert.ToInt32(l);
                        else if (i == 2)
                            Global.GUIObjectPositionY = Convert.ToInt32(l);
                        else if (i == 3)
                            Global.GUIObjectSizeX = Convert.ToInt32(l);
                        else if (i == 4)
                            Global.GUIObjectSizeY = Convert.ToInt32(l);
                        else if (i == 5)
                            Global.GUIObjectColor = l;
                        else if (i == 6)
                            Global.text_toDisplay = l;
                        else if (i == 7)
                            Global.GUIObjectName = l;
                        i++;
                    }
                    Button p = new Button { Size = new Size(Global.GUIObjectSizeX, Global.GUIObjectSizeY), Location = new Point(Global.GUIObjectPositionX, Global.GUIObjectPositionY) };
                    Global.GUIObjectColor = Global.GUIObjectColor.Replace(" ", "");
                    p.Name = Global.GUIObjectName.Trim();
                    p.Text = Global.text_toDisplay;
                    //This is the Normal RGB + Black and White colors
                    if (Global.GUIObjectColor == "Red")
                        p.BackColor = Color.Red;
                    else if (Global.GUIObjectColor == "Green")
                        p.BackColor = Color.Green;
                    else if (Global.GUIObjectColor == "Blue")
                        p.BackColor = Color.Blue;
                    else if (Global.GUIObjectColor == "White")
                        p.BackColor = Color.White;
                    else if (Global.GUIObjectColor == "Black")
                        p.BackColor = Color.Black;
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
                    this.game_window.Controls.Add(p);
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
                    Console.WriteLine(Global.project_dir + @"\" + audio_toLoad);
                    try
                    {
                        p.Tag = Global.project_dir + @"\" + audio_toLoad;
                        Write(p.Tag.ToString());
                        SoundPlayer soundPlayer = new SoundPlayer();
                        Write("after");
                        soundPlayer.Tag = Global.GUIObjectName.Trim();
                    }
                    catch
                    {
                        MessageBox.Show("Coulnd't Find the Audio Specified", "IMAGE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    game_window.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
            }
        }

        /*
                        ████████╗██╗  ██╗███████╗    █████╗  █████╗ ███╗   ███╗██████╗ ██╗██╗     ███████╗██████╗
                        ╚══██╔══╝██║  ██║██╔════╝   ██╔══██╗██╔══██╗████╗ ████║██╔══██╗██║██║     ██╔════╝██╔══██╗
                           ██║   ███████║█████╗     ██║  ╚═╝██║  ██║██╔████╔██║██████╔╝██║██║     █████╗  ██████╔╝
                           ██║   ██╔══██║██╔══╝     ██║  ██╗██║  ██║██║╚██╔╝██║██╔═══╝ ██║██║     ██╔══╝  ██╔══██╗
                           ██║   ██║  ██║███████╗   ╚█████╔╝╚█████╔╝██║ ╚═╝ ██║██║     ██║███████╗███████╗██║  ██║
                           ╚═╝   ╚═╝  ╚═╝╚══════╝    ╚════╝  ╚════╝ ╚═╝     ╚═╝╚═╝     ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝
         */
        public void Load_GameScripts(string path_file, string[] stringTextArray, bool isFilePath)
        {
            if (isFilePath == true && path_file != null && stringTextArray == null)
            {
                string[] lines = null;
                lines = File.ReadAllLines(path_file/*.Trim()*/);
                int lineCounter = 1;
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    Global.line_count++;
                    if (line.EndsWith("();"))
                    {
                        string mainScriptName = line.ToString();
                        mainScriptName = mainScriptName.Replace("(", "");
                        mainScriptName = mainScriptName.Replace(")", "");
                        mainScriptName = mainScriptName.Replace(";", "");
                        if (!mainScriptName.EndsWith(" ") || !mainScriptName.EndsWith(",") || !mainScriptName.EndsWith("."))
                        {
                            Console.WriteLine(mainScriptName);

                            WriteToGameLogger($"Loading Script {mainScriptName}");
                            Console.WriteLine("loading script {0}...", mainScriptName);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{mainScriptName}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == mainScriptName + ".s14c")
                                        {
                                            try
                                            { Load_GameScripts(pathh + @"\" + mainScriptName + ".s14c", null, true); }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");
                                            }
                                        }
                                    }
                                }
                                WriteToGameLogger($"Finished Loading script '{mainScriptName}'");
                                Console.WriteLine("finished loading script {0}", mainScriptName + ".s14c");
                            }
                            catch
                            {
                                WriteToGameLogger($"Error While Trying to load Script '{mainScriptName}'", "err");
                                MessageBox.Show("An Error Occured while loading script :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Load Script because of error");
                            WriteToGameLogger("Couldn't Load Script because of invaild Char(Charactor) at the end of your command", "err");
                        }
                    }
                    if (line.StartsWith("Log(") || line.StartsWith("log(") || line.StartsWith("LOG("))
                    {
                        char qoutes = '"';
                        string TheLine = line;
                        if (line.StartsWith("Log("))
                            TheLine = TheLine.Replace("Log(", "");
                        if (line.StartsWith("log("))
                            TheLine = TheLine.Replace("log(", "");
                        if (line.StartsWith("LOG("))
                            TheLine = TheLine.Replace("LOG(", "");
                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                        {
                            Write(TheLine.ToString() + $" was logged to game engine");
                            WriteToGameLogger(TheLine.ToString() + "?");
                        }
                        else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                        {
                            foreach (string file in Directory.GetFiles(Global.project_dir))
                            {
                                if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                {
                                    string[] variableData = File.ReadAllLines(file);
                                    List<string> variableData_List = new List<string>(variableData);
                                    variableData_List.RemoveAt(2);
                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                    varData = varData.Replace("{", "");
                                    varData = varData.Trim();
                                    varData = varData.Replace("data", "");
                                    varData = varData.Replace(":", "");
                                    varData = varData.Replace(qoutes.ToString(), "");
                                    varData = varData.Trim();
                                    Write(varData.ToString() + $" was logged to game engine");
                                    WriteToGameLogger(varData.ToString() + "?");
                                }
                            }
                        }
                    }
                    if (line.StartsWith("Input(") || line.StartsWith("input(") || line.StartsWith("INPUT("))
                    {
                        bool enterKeyHit = false;
                        char qoutes = '"';
                        string TheLine = line;
                        if (line.StartsWith("Input("))
                            TheLine = TheLine.Replace("Input(", "");
                        if (line.StartsWith("input("))
                            TheLine = TheLine.Replace("input(", "");
                        if (line.StartsWith("INPUT("))
                            TheLine = TheLine.Replace("INPUT(", "");
                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                        {
                            Write(TheLine.ToString());
                            WriteToGameLogger("Asked; " + TheLine.ToString());
                        }
                        else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                        {
                            foreach (string file in Directory.GetFiles(Global.project_dir))
                            {
                                if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                {
                                    string[] variableData = File.ReadAllLines(file);
                                    List<string> variableData_List = new List<string>(variableData);
                                    variableData_List.RemoveAt(2);
                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                    varData = varData.Replace("{", "");
                                    varData = varData.Trim();
                                    varData = varData.Replace("data", "");
                                    varData = varData.Replace(":", "");
                                    varData = varData.Replace(qoutes.ToString(), "");
                                    varData = varData.Trim();
                                    Write(varData.ToString());
                                    WriteToGameLogger("Asked; " + varData.ToString());
                                }
                            }
                        }
                        inputTextbox.ReadOnly = false;
                        inputTextbox.Text = string.Empty;
                        inputTextbox.Visible = true;
                        void onEnterKeyPressed(object sender, KeyPressEventArgs keyPressEventArgs_e)
                        {
                            if (keyPressEventArgs_e.KeyChar == (char)Keys.Enter)
                            {
                                //Since we dont have to send any info to a variable, we just discard it
                                enterKeyHit = true;
                                inputTextbox.ReadOnly = true;
                                inputTextbox.Text = string.Empty;
                                inputTextbox.Visible = false;
                            }
                        }
                        inputTextbox.KeyPress += new KeyPressEventHandler(onEnterKeyPressed);
                        while (enterKeyHit == false)
                        {
                            wait(500);
                        }
                    }
                    if (line.StartsWith("random(") || line.StartsWith("Random(") || line.StartsWith("RANDOM("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("random("))
                            TheLine = TheLine.Replace("random(", "");
                        if (line.StartsWith("Random("))
                            TheLine = TheLine.Replace("Random(", "");
                        if (line.StartsWith("RANDOM("))
                            TheLine = TheLine.Replace("RANDOM(", "");
                        TheLine = TheLine.Replace(")", "");
                        TheLine = TheLine.Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;
                        int number_1 = 0;
                        int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                            {
                                number_1 = Convert.ToInt32(element);
                            }
                            if (counter == 2)
                            {
                                number_2 = Convert.ToInt32(element);
                            }
                            counter++;
                        }
                        Random rd = new Random();
                        int rand_num = rd.Next(number_1, number_2);
                        Global.mathAnswer = rand_num;
                        Write(rand_num.ToString() + $" is result of a random number generation of numbers {number_1} and {number_2}");
                        WriteToGameLogger(rand_num.ToString() + $" is result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Add(") || line.StartsWith("add(") || line.StartsWith("ADD("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Add("))
                            TheLine = TheLine.Replace("Add(", "");
                        if (line.StartsWith("add("))
                            TheLine = TheLine.Replace("add(", "");
                        if (line.StartsWith("ADD("))
                            TheLine = TheLine.Replace("ADD(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;    int number_1 = 0;   int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);
                            counter++;
                        }
                        Math_statements("Add", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is sum(result) of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is sum(result) of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Minus(") || line.StartsWith("Subtract(") || line.StartsWith("minus(") ||
                        line.StartsWith("subtract(") || line.StartsWith("MINUS(") || line.StartsWith("SUBTRACT("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Minus("))
                            TheLine = TheLine.Replace("Minus(", "");
                        if (line.StartsWith("Subtract("))
                            TheLine = TheLine.Replace("Subtract(", "");
                        if (line.StartsWith("minus("))
                            TheLine = TheLine.Replace("minus(", "");
                        if (line.StartsWith("subtract("))
                            TheLine = TheLine.Replace("subtract(", "");
                        if (line.StartsWith("MINUS("))
                            TheLine = TheLine.Replace("MINUS(", "");
                        if (line.StartsWith("SUBTRACT("))
                            TheLine = TheLine.Replace("SUBTRACT(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;    int number_1 = 0;   int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Minus", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the difference(result) of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the difference(result) of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Multiply(") || line.StartsWith("multiply(") || line.StartsWith("MULTIPLY("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Multiply("))
                            TheLine = TheLine.Replace("Multiply(", "");
                        if (line.StartsWith("multiply("))
                            TheLine = TheLine.Replace("multiply(", "");
                        if (line.StartsWith("MULTIPLY("))
                            TheLine = TheLine.Replace("MULTIPLY(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;    int number_1 = 0;   int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Multiply", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Divide(") || line.StartsWith("divide(") || line.StartsWith("DIVIDE("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Divide("))
                            TheLine = TheLine.Replace("Divide(", "");
                        if (line.StartsWith("divide("))
                            TheLine = TheLine.Replace("divide(", "");
                        if (line.StartsWith("DIVIDE("))
                            TheLine = TheLine.Replace("DIVIDE(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;    int number_1 = 0;   int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Divide", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Sqrt(") || line.StartsWith("sqrt(") || line.StartsWith("SQRT("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Sqrt("))
                            TheLine = TheLine.Replace("Sqrt(", "");
                        if (line.StartsWith("sqrt("))
                            TheLine = TheLine.Replace("sqrt(", "");
                        if (line.StartsWith("SQRT("))
                            TheLine = TheLine.Replace("SQRT(", "");
                        TheLine = TheLine.Replace(")", "");
                        TheLine = TheLine.Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Sqrt", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Cos(") || line.StartsWith("cos(") || line.StartsWith("COS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Cos("))
                            TheLine = TheLine.Replace("Cos(", "");
                        if (line.StartsWith("cos("))
                            TheLine = TheLine.Replace("cos(", "");
                        if (line.StartsWith("COS("))
                            TheLine = TheLine.Replace("COS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Cos", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Tan(") || line.StartsWith("tan(") || line.StartsWith("TAN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Tan("))
                            TheLine = TheLine.Replace("Tan(", "");
                        if (line.StartsWith("tan("))
                            TheLine = TheLine.Replace("tan(", "");
                        if (line.StartsWith("TAN("))
                            TheLine = TheLine.Replace("TAN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Tan", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Abs(") || line.StartsWith("abs(") || line.StartsWith("ABS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Abs("))
                            TheLine = TheLine.Replace("Abs(", "");
                        if (line.StartsWith("abs("))
                            TheLine = TheLine.Replace("abs(", "");
                        if (line.StartsWith("ABS("))
                            TheLine = TheLine.Replace("ABS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Abs", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Acos(") || line.StartsWith("acos(") || line.StartsWith("ACOS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Acos("))
                            TheLine = TheLine.Replace("Acos(", "");
                        if (line.StartsWith("acos("))
                            TheLine = TheLine.Replace("acos(", "");
                        if (line.StartsWith("ACOS("))
                            TheLine = TheLine.Replace("ACOS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Acos", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Asin(") || line.StartsWith("asin(") || line.StartsWith("ASIN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Asin("))
                            TheLine = TheLine.Replace("Asin(", "");
                        if (line.StartsWith("asin("))
                            TheLine = TheLine.Replace("asin(", "");
                        if (line.StartsWith("ASIN("))
                            TheLine = TheLine.Replace("ASIN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Asin", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Atan(") || line.StartsWith("atan(") || line.StartsWith("ATAN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Atan("))
                            TheLine = TheLine.Replace("Atan(", "");
                        if (line.StartsWith("atan("))
                            TheLine = TheLine.Replace("atan(", "");
                        if (line.StartsWith("ATAN("))
                            TheLine = TheLine.Replace("ATAN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Atan", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("MLog(") || line.StartsWith("mlog(") || line.StartsWith("MLOG("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("MLog("))
                            TheLine = TheLine.Replace("MLog(", "");
                        if (line.StartsWith("mlog("))
                            TheLine = TheLine.Replace("mlog(", "");
                        if (line.StartsWith("MLOG("))
                            TheLine = TheLine.Replace("MLOG(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("MLog", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Cosh(") || line.StartsWith("cosh(") || line.StartsWith("COSH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Cosh("))
                            TheLine = TheLine.Replace("Cosh(", "");
                        if (line.StartsWith("cosh("))
                            TheLine = TheLine.Replace("cosh(", "");
                        if (line.StartsWith("COSH("))
                            TheLine = TheLine.Replace("COSH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Cosh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Sinh(") || line.StartsWith("sinh(") || line.StartsWith("SINH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Sinh("))
                            TheLine = TheLine.Replace("Sinh(", "");
                        if (line.StartsWith("sinh("))
                            TheLine = TheLine.Replace("sinh(", "");
                        if (line.StartsWith("SINH("))
                            TheLine = TheLine.Replace("SINH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Sinh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Tanh(") || line.StartsWith("tanh(") || line.StartsWith("TANH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Tanh("))
                            TheLine = TheLine.Replace("Tanh(", "");
                        if (line.StartsWith("tanh("))
                            TheLine = TheLine.Replace("tanh(", "");
                        if (line.StartsWith("TANH("))
                            TheLine = TheLine.Replace("TANH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Tanh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("var"))
                    {
                        string varReplaced = line.Replace("var", "");
                        varReplaced = varReplaced.TrimStart();
                        string fileName = null;

                        if (Global.project_dir != null)
                            fileName = Global.project_dir + @"\" + varReplaced + ".json";
                        else
                            throw new ProjectPathError(Global.project_dir);

                        try
                        {
                            if (File.Exists(fileName))
                                File.Delete(fileName);

                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                char qoutes = '"';
                                sw.WriteLine("{");
                                sw.WriteLine(qoutes + "data" + qoutes + ": null");
                                sw.WriteLine("}");
                            }

                            using (StreamReader sr = File.OpenText(fileName))
                            {
                                string s = "";
                                while ((s = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(s);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex.ToString());
                        }
                    }
                    foreach (string file in Directory.GetFiles(Global.project_dir))
                    {
                        if (line.StartsWith($"{Path.GetFileName(file.Replace(".json", ""))} ="))
                        {
                            string varReplaced = line.Substring(line.LastIndexOf(@"\") + 1, line.Length);
                            Write(varReplaced);
                            char[] fullLine = varReplaced.ToCharArray();
                            int equalSignPos = 0;
                            int counnter = 0;
                            foreach (char char_ in fullLine)
                            {
                                if (char_ == '=')
                                {
                                    equalSignPos = counnter;
                                    break;
                                }
                                counnter++;
                            }
                            string aftrEqualsPt = varReplaced.Substring(equalSignPos + 1); Write(aftrEqualsPt);
                            string beforEqualsPt = varReplaced.Substring(0, equalSignPos + 1); Write(beforEqualsPt);
                            beforEqualsPt = beforEqualsPt.Replace("=", "");
                            beforEqualsPt = beforEqualsPt.TrimEnd();
                            varReplaced = varReplaced.TrimStart('"');
                            string fileName = null;
                            char[] textLeArray = varReplaced.ToCharArray();
                            if (Global.project_dir != null)
                            {
                                fileName = Global.project_dir + @"\" + Path.GetFileName(beforEqualsPt) + ".json";
                                Write(fileName + " is the file name");
                            }
                            else
                            {
                                throw new ProjectPathError(Global.project_dir);
                            }
                            try
                            {
                                aftrEqualsPt = aftrEqualsPt.Trim();
                                aftrEqualsPt = aftrEqualsPt.Replace('.'.ToString(), "");
                                Write(aftrEqualsPt + " is the orignal");
                                char qoutes = '"';
                                varReplaced = varReplaced.Replace(".", "");
                                if (aftrEqualsPt.StartsWith("add(") || aftrEqualsPt.StartsWith("Add(") || aftrEqualsPt.StartsWith("ADD("))
                                {

                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("add("))
                                        finding_both_ints = finding_both_ints.Replace("add", "");
                                    if (aftrEqualsPt.StartsWith("Add("))
                                        finding_both_ints = finding_both_ints.Replace("Add", "");
                                    if (aftrEqualsPt.StartsWith("ADD("))
                                        finding_both_ints = finding_both_ints.Replace("ADD", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Add", number1, number2);
                                    Write(Global.mathAnswer + " is the result");


                                    string[] fileDOTWrite = {
                                        "{",
                                            $"{qoutes}data{qoutes} : {qoutes}{Global.mathAnswer.ToString()}{qoutes}",
                                        "}"
                                    };
                                    File.WriteAllLines(fileName, fileDOTWrite);
                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {qoutes}{Global.mathAnswer.ToString()}{qoutes}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("minus(") || aftrEqualsPt.StartsWith("Minus(") || aftrEqualsPt.StartsWith("MINUS(") || aftrEqualsPt.StartsWith("subtract(") || aftrEqualsPt.StartsWith("Subtract(") || aftrEqualsPt.StartsWith("SUBTRACT("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("minus("))
                                        finding_both_ints = finding_both_ints.Replace("minus", "");
                                    if (aftrEqualsPt.StartsWith("Minus("))
                                        finding_both_ints = finding_both_ints.Replace("Minus", "");
                                    if (aftrEqualsPt.StartsWith("MINUS("))
                                        finding_both_ints = finding_both_ints.Replace("MINUS", "");
                                    if (aftrEqualsPt.StartsWith("subtract("))
                                        finding_both_ints = finding_both_ints.Replace("subtract", "");
                                    if (aftrEqualsPt.StartsWith("Subtract("))
                                        finding_both_ints = finding_both_ints.Replace("Subtract", "");
                                    if (aftrEqualsPt.StartsWith("SUBTRACT("))
                                        finding_both_ints = finding_both_ints.Replace("SUBTRACT", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Subtract", number1, number2);
                                    Write(Global.mathAnswer + " is the result");

                                    if (File.Exists(fileName))
                                    {
                                        File.Delete(fileName);
                                    }


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("multiply(") || aftrEqualsPt.StartsWith("Multiply(") || aftrEqualsPt.StartsWith("MULTIPLY("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("multiply", "");
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("Multiply", "");
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("MULTIPLY", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Multiply", number1, number2);
                                    Write(Global.mathAnswer + " is the result");
                                    if (File.Exists(fileName))
                                    {
                                        File.Delete(fileName);
                                    }


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("divide(") || aftrEqualsPt.StartsWith("Divide(") || aftrEqualsPt.StartsWith("DIVIDE("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("divide("))
                                        finding_both_ints = finding_both_ints.Replace("divide", "");
                                    if (aftrEqualsPt.StartsWith("Divide("))
                                        finding_both_ints = finding_both_ints.Replace("Divide", "");
                                    if (aftrEqualsPt.StartsWith("DIVIDE("))
                                        finding_both_ints = finding_both_ints.Replace("DIVIDE", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Divide", number1, number2);
                                    Write(Global.mathAnswer + " is the result");


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("random(") || aftrEqualsPt.StartsWith("Random(") || aftrEqualsPt.StartsWith("RANDOM("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("random("))
                                        finding_both_ints = finding_both_ints.Replace("random", "");
                                    if (aftrEqualsPt.StartsWith("Random("))
                                        finding_both_ints = finding_both_ints.Replace("Random", "");
                                    if (aftrEqualsPt.StartsWith("RANDOM("))
                                        finding_both_ints = finding_both_ints.Replace("RANDOM", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Random rd = new Random();
                                    int stringNewData_int = rd.Next(number1, number2);
                                    Write(stringNewData_int + " is the result");


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {stringNewData_int.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("Input(") || aftrEqualsPt.StartsWith("input(") || aftrEqualsPt.StartsWith("INPUT("))
                                {
                                    bool enterKeyWasHit = false;
                                    string TheLine = aftrEqualsPt;
                                    if (aftrEqualsPt.StartsWith("Input("))
                                        TheLine = TheLine.Replace("Input(", "");
                                    if (aftrEqualsPt.StartsWith("input("))
                                        TheLine = TheLine.Replace("input(", "");
                                    if (aftrEqualsPt.StartsWith("INPUT("))
                                        TheLine = TheLine.Replace("INPUT(", "");
                                    TheLine = TheLine.Replace(")", "");

                                    if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                                    {
                                        Write(TheLine.ToString());
                                        WriteToGameLogger("Asked; " + TheLine.ToString());
                                    }
                                    else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                                    {
                                        foreach (string file_toAskQuestionData in Directory.GetFiles(Global.project_dir))
                                        {
                                            if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                            {
                                                string[] variableData = File.ReadAllLines(file);
                                                List<string> variableData_List = new List<string>(variableData);
                                                variableData_List.RemoveAt(2);
                                                string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                                varData = varData.Replace("{", "");
                                                varData = varData.Trim();
                                                varData = varData.Replace("data", "");
                                                varData = varData.Replace(":", "");
                                                varData = varData.Replace(qoutes.ToString(), "");
                                                varData = varData.Trim();
                                                Write(varData.ToString());
                                                WriteToGameLogger("Asked; " + varData.ToString());
                                            }
                                        }
                                    }
                                    inputTextbox.ReadOnly = false;
                                    inputTextbox.Text = string.Empty;
                                    inputTextbox.Visible = true;
                                    inputTextbox.Focus();
                                    void onEnterKeyPressed(object sender, KeyPressEventArgs keyPressEventArgs_e)
                                    {
                                        if (keyPressEventArgs_e.KeyChar == (char)Keys.Enter)
                                        {
                                            string[] textToWrite =
                                            {
                                                "{",
                                                $"{qoutes}data{qoutes} : {aftrEqualsPt}",
                                                "}"
                                            };

                                            File.WriteAllLines(fileName, textToWrite);
                                            wait(250);
                                            enterKeyWasHit = true;
                                            inputTextbox.ReadOnly = true;
                                            inputTextbox.Text = string.Empty;
                                            inputTextbox.Visible = false;
                                        }
                                    }
                                    inputTextbox.KeyPress += new KeyPressEventHandler(onEnterKeyPressed);
                                    while (enterKeyWasHit == false)
                                    {
                                        wait(750);
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith(qoutes.ToString()) && aftrEqualsPt.EndsWith(qoutes.ToString()))
                                {

                                    aftrEqualsPt.Replace(qoutes.ToString(), "");
                                    string[] textToWrite =
                                    {
                                        "{",
                                        $"{qoutes}data{qoutes} : {aftrEqualsPt}",
                                        "}"
                                    };

                                    File.WriteAllLines(fileName, textToWrite);

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (!aftrEqualsPt.StartsWith(qoutes.ToString()) && !aftrEqualsPt.EndsWith(qoutes.ToString()))
                                {
                                    aftrEqualsPt = aftrEqualsPt.Trim();
                                    bool isAnInt = aftrEqualsPt.All(char.IsDigit);
                                    if (isAnInt == false)
                                    {
                                        if (aftrEqualsPt == "true" || aftrEqualsPt == "false")
                                        {
                                            if (aftrEqualsPt == "true")
                                            {
                                                using (StreamWriter sw = File.CreateText(fileName))
                                                {
                                                    Write("Editing String");
                                                    sw.WriteLine("{");
                                                    sw.WriteLine($"{qoutes}data{qoutes} : true");
                                                    sw.WriteLine("}");
                                                }

                                                using (StreamReader sr = File.OpenText(fileName))
                                                {
                                                    string s = "";
                                                    while ((s = sr.ReadLine()) != null)
                                                    {
                                                        Console.WriteLine(s);
                                                    }
                                                }
                                            }
                                            if (aftrEqualsPt == "false")
                                            {
                                                using (StreamWriter sw = File.CreateText(fileName))
                                                {
                                                    Write("Editing String");
                                                    sw.WriteLine("{");
                                                    sw.WriteLine($"{qoutes}data{qoutes} : false");
                                                    sw.WriteLine("}");
                                                }

                                                using (StreamReader sr = File.OpenText(fileName))
                                                {
                                                    string s = "";
                                                    while ((s = sr.ReadLine()) != null)
                                                    {
                                                        Console.WriteLine(s);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Write("Finding files for string data");
                                            foreach (string var_file in Directory.GetFiles(Global.project_dir))
                                            {
                                                Write(Path.GetFileName(var_file));
                                                if (aftrEqualsPt.StartsWith(Path.GetFileName(var_file.Replace(".json", "").Trim())))
                                                {
                                                    Write("Found file!");
                                                    string[] variableData = File.ReadAllLines(var_file);
                                                    List<string> variableData_List = new List<string>(variableData);
                                                    variableData_List.RemoveAt(2);
                                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                                    varData = varData.Replace("{", "");
                                                    varData = varData.Trim();
                                                    varData = varData.Replace("data", "");
                                                    varData = varData.Replace(":", "");
                                                    varData = varData.Replace(qoutes.ToString(), "");
                                                    varData = varData.Trim();
                                                    Write(varData);
                                                    using (StreamWriter sw = File.CreateText(fileName))
                                                    {
                                                        Write($"Editing String ({fileName})");
                                                        sw.WriteLine("{");
                                                        sw.WriteLine($"{qoutes}data{qoutes} : {qoutes}{varData}{qoutes}");
                                                        sw.WriteLine("}");
                                                    }

                                                    using (StreamReader sr = File.OpenText(fileName))
                                                    {
                                                        string s = "";
                                                        while ((s = sr.ReadLine()) != null)
                                                        {
                                                            Console.WriteLine(s);
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else if (isAnInt == true)
                                    {
                                        aftrEqualsPt = aftrEqualsPt.Trim();
                                        using (StreamWriter sw = File.CreateText(fileName))
                                        {
                                            Write("Editing String");
                                            sw.WriteLine("{");
                                            sw.WriteLine($"{qoutes}data{qoutes} : {aftrEqualsPt}");
                                            sw.WriteLine("}");
                                        }

                                        using (StreamReader sr = File.OpenText(fileName))
                                        {
                                            string s = "";
                                            while ((s = sr.ReadLine()) != null)
                                            {
                                                Console.WriteLine(s);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                Console.WriteLine(Ex.ToString());
                            }
                        }
                    }
                    if (line.StartsWith("if") && line.EndsWith("then")) //Messy Code :/
                    {
                        string if_lines = line;
                        string[] cooool = if_lines.Split(' ');
                        for (int i = 0; i < cooool.Length; i++)
                        {
                            if (cooool[i] == "if")
                            {
                                string eee = if_lines.Substring(i, if_lines.Length);
                                cooool = eee.Split(' ');
                            }
                        }
                        int if_Line_Pos = 0;
                        int then_Line_Pos = 0;
                        bool if_Found = false;
                        bool end_Found = false;
                        int countsdfud = 0;
                        for (int i = 0; i < cooool.Length; i++)
                        {
                            if (cooool[i] == "if")
                            {
                                if_Line_Pos = 0;
                                if_Found = true;
                                Write("if found at " + i);
                            }

                            if (cooool[i] == "then")
                            {
                                then_Line_Pos = i;
                                end_Found = true;
                                Write("then found at " + i);
                            }

                            ///////////////
                            if (if_Line_Pos <= then_Line_Pos && if_Found == true && end_Found == true)
                            {
                                Write("If statment Found :D");
                                line.Replace("if", "");
                                line.Replace("then", "");
                                if_statement_Handle(path_file, lineCounter - 1);
                                if (Global.IfStatementConditionsAreMet == false)
                                {
                                    //Do nothing :/
                                }
                                else if (Global.IfStatementConditionsAreMet == true)
                                {
                                    return;// do note that in the if statement handler, it will continue the main script after the if statement script is done so there is no issue :)
                                }
                            }
                            else if (if_Line_Pos > then_Line_Pos)//else if the "if" keyword is after "then" keyword, there is to try and handle the isssue
                            {
                                try
                                {
                                    //An Exception im trying to fix
                                    Write("=======IMPORTANT========");
                                    Write(if_Line_Pos.ToString() + " & " + cooool[i] + "\n" + if_Line_Pos.ToString() + " & " + countsdfud);
                                    var newArray2 = cooool[if_Line_Pos + 1];
                                    if (newArray2 == "then") { end_Found = true; }
                                    else { Write(newArray2 + " is inside the if statement"); }

                                    string[] keywords = { "then", "or", "else" };
                                    if (if_Found == true && end_Found == false) { Write("ERROR - NO 'THEN' IN IF STATMENT FOUND"); }
                                    if (if_Found == false && end_Found == true) { Write("ERROR - NO 'IF' IN IF STATMENT FOUND"); }
                                }
                                catch (Exception e) { Write("Operation FAILED, " + e.ToString()); }
                            }
                        }
                    }

                    foreach (Control buttonCTRL in game_window.Controls)
                    {
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseClick("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseClick("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseClick(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void onMouseClickCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.Click += new EventHandler(onMouseClickCUSTOM);
                            //WriteToGameLogger($"Loading Script {buttonOnClickFunctionLine}");
                            //Console.WriteLine("loading script {0}...", buttonOnClickFunctionLine);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseDoubleClick("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseDoubleClick("))
                            { buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseDoubleClick(", ""); }
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void onMouseDoubleClickCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.DoubleClick += new EventHandler(onMouseDoubleClickCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseEnter("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseEnter("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseEnter(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void OnMouseEnterCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.MouseEnter += new EventHandler(OnMouseEnterCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseLeave("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseLeave("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseLeave(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void OnMouseEnterCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.MouseLeave += new EventHandler(OnMouseEnterCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                    }
                    if (line.Contains("Clone(") || line.Contains("clone("))
                    {
                        string cloneCommand = line.ToString();
                        if (cloneCommand.StartsWith("Clone("))
                            cloneCommand = cloneCommand.Replace("Clone(", "");
                        if (cloneCommand.StartsWith("clone("))
                            cloneCommand = cloneCommand.Replace("clone(", "");
                        cloneCommand = cloneCommand.Replace(")", "");
                        if (!cloneCommand.EndsWith(" ") || !cloneCommand.EndsWith(",") || !cloneCommand.EndsWith("."))
                        {
                            Console.WriteLine(cloneCommand);
                            Console.WriteLine("cloning...");
                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    foreach (Control ctrl in game_window.Controls)
                                    {
                                        if (ctrl.Name == cloneCommand)
                                        {
                                            Control NewControl = new Control(ctrl, ctrl.Name + "_clone");
                                            NewControl.Location = new Point(NewControl.Location.X + 10, NewControl.Location.Y + 10);
                                        }
                                    }
                                }
                                Console.WriteLine("finished cloning");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while cloning :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Cloning Command because of error");
                            MessageBox.Show("Couldn't Excute Cloning Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("wait("))
                    {
                        string wait_Command = line.ToString();
                        wait_Command = wait_Command.Replace("wait(", "");
                        wait_Command = wait_Command.Replace(")", "");
                        if (!wait_Command.EndsWith(" ") || !wait_Command.EndsWith(",") || !wait_Command.EndsWith("."))
                        {
                            Console.WriteLine(wait_Command);
                            try { if (Global.game_stopped == false) { wait(Convert.ToInt32(wait_Command) * 1000); } }
                            catch { MessageBox.Show("An Error Occured while waiting :("); }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Wait Command because of error");
                            MessageBox.Show("Couldn't Excute Wait Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.StartsWith("Remove(") || line.StartsWith("remove(") || line.StartsWith("REMOVE("))
                    {
                        char qoutes = '"';
                        string TheLine = line;

                        if (line.StartsWith("Remove(")) { TheLine = TheLine.Replace("Remove(", ""); }
                        if (line.StartsWith("remove(")) { TheLine = TheLine.Replace("remove(", ""); }
                        if (line.StartsWith("REMOVE(")) { TheLine = TheLine.Replace("REMOVE(", ""); }

                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.Contains(qoutes.ToString())) { TheLine = TheLine.Replace(qoutes.ToString(), ""); }
                        if (TheLine == "All") { game_window.Controls.Clear(); }
                        else
                        {
                            var ControlToRemove = game_window.Controls.Find(TheLine, true).FirstOrDefault();
                            if (ControlToRemove != null) { game_window.Controls.Remove(ControlToRemove); }
                            else { WriteToGameLogger($"Error; Unable To Remove Object Named '{TheLine}' because The Object Was Not Found", "err"); }
                        }
                    }
                    foreach (Control control in this.game_window.Controls)
                    {
                        if (line.Contains($"{control.Name}.Enabled"))
                        {
                            string enabled_Command = line.ToString();
                            enabled_Command = enabled_Command.Replace("=", "");
                            enabled_Command = enabled_Command.Replace(".Enabled", "");
                            enabled_Command = enabled_Command.Replace(control.Name, "");
                            enabled_Command = enabled_Command.Trim();
                            if (!enabled_Command.EndsWith(" ") || !enabled_Command.EndsWith(",") || !enabled_Command.EndsWith("."))
                            {
                                Console.WriteLine($"setting {control.Name} to {enabled_Command}...");
                                try
                                {
                                    if (Global.game_stopped == false)
                                    {
                                        if (enabled_Command == "true") { control.Enabled = true; }
                                        else if (enabled_Command == "false") { control.Enabled = false; }
                                    }
                                }
                                catch { MessageBox.Show("An Error Occured while waiting :("); }
                            }
                            else
                            {
                                Console.WriteLine("Couldn't Excute Move to Command because of error");
                                MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                            }
                        }
                    }
                    if (line.Contains("OnkeyPress"))
                    {
                        string keyPressCommands = line.ToString();
                        keyPressCommands = keyPressCommands.Replace(")", "");
                        keyPressCommands = keyPressCommands.Replace("OnkeyPress(", "");
                        string[] OnPressKeyarray = keyPressCommands.Split(',');
                        int itemNum = 1;
                        string keyToPress = null;
                        string methodToLoadAfterKeyPressed = null;
                        foreach (string item in OnPressKeyarray)
                        {
                            if (itemNum == 1)
                            {
                                keyToPress = item;
                            }
                            if (itemNum == 2)
                            {
                                methodToLoadAfterKeyPressed = item;
                            }
                            itemNum++;
                        }
                        if (!keyPressCommands.EndsWith(" ") || !keyPressCommands.EndsWith(",") || !keyPressCommands.EndsWith("."))
                        {
                            Console.WriteLine(keyPressCommands);

                            WriteToGameLogger("Waiting For Keypress");
                            Write("Waiting FOr UserKeypress");

                            try
                            {
                                this.game_window.Focus();
                                KeyEventHandler handler = (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                                this.game_window.KeyDown += handler;
                                //The below code has been removed since i cant manage to remove the event handler from the game_window.KeyDown Event
                                //this.game_window.KeyDown -= (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                                //this.game_window.KeyDown += (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                            }
                            catch { MessageBox.Show("An Error Occured while waiting :("); }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains(".Set color") || line.Contains(".Set Color") || line.Contains(".set color"))
                    {
                        string SetColor_Command = line.ToString();
                        SetColor_Command = SetColor_Command.Replace("=", "");
                        if (line.Contains(".Set color")) { SetColor_Command = SetColor_Command.Replace("Set color", ""); }
                        if (line.Contains(".Set Color")) { SetColor_Command = SetColor_Command.Replace("Set Color", ""); }
                        if (line.Contains(".set color")) { SetColor_Command = SetColor_Command.Replace("set color", ""); }
                        string _object = null;
                        _object = SetColor_Command.Substring(0, SetColor_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("set color to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetColor_Command.EndsWith(" ") || !SetColor_Command.EndsWith(",") || !SetColor_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetColor_Command);
                                    string bruhhh = SetColor_Command.Replace(_object + ".", "");
                                    string Color_get = bruhhh.Substring(1);
                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            if (Color_get == " Red") { ctrl.BackColor = Color.Red; }
                                            else if (Color_get == " Green") { ctrl.BackColor = Color.Green; }
                                            else if (Color_get == " Blue") { ctrl.BackColor = Color.Blue; }
                                            else if (Color_get == " White") { ctrl.BackColor = Color.White; }
                                            else if (Color_get == " Black") { ctrl.BackColor = Color.Black; }
                                            else if (Color_get == " Cyan") { ctrl.BackColor = Color.Cyan; }
                                        }
                                        Console.WriteLine("color was set to {0}", Color_get);
                                        WriteToGameLogger("Finished Change Color Of Object");
                                    }
                                    catch { MessageBox.Show("An Error Occured while changing color {0} :(", ctrl.Name); }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute set color Command because of error");
                                    MessageBox.Show("Couldn't Excute set color Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Move by"))
                    {
                        string MoveBy_Command = line.ToString();
                        MoveBy_Command = MoveBy_Command.Replace("=", "");
                        if (line.Contains(".Move by")) { MoveBy_Command = MoveBy_Command.Replace("Move by", ""); }
                        if (line.Contains(".move by")) { MoveBy_Command = MoveBy_Command.Replace("move by", ""); }
                        if (line.Contains(".Move By")) { MoveBy_Command = MoveBy_Command.Replace("Move By", ""); }
                        if (line.Contains(".MOVE BY")) { MoveBy_Command = MoveBy_Command.Replace("MOVE BY", ""); }
                        string _object = null;
                        _object = MoveBy_Command.Substring(0, MoveBy_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!MoveBy_Command.EndsWith(" ") || !MoveBy_Command.EndsWith(",") || !MoveBy_Command.EndsWith("."))
                                {
                                    Console.WriteLine(MoveBy_Command);
                                    string bruhhh = MoveBy_Command.Replace(_object + ".", "");
                                    string X_pos_get = bruhhh.Substring(1);
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y);
                                                if (ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Move to Command because of error");
                                    WriteToGameLogger("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(", "err");
                                    MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Set Y"))
                    {
                        string SetY_Command = line.ToString();
                        SetY_Command = SetY_Command.Replace("=", "");
                        if (line.Contains(".Set Y"))
                            SetY_Command = SetY_Command.Replace("Set Y", "");
                        if (line.Contains(".set Y"))
                            SetY_Command = SetY_Command.Replace("set Y", "");
                        if (line.Contains(".set y"))
                            SetY_Command = SetY_Command.Replace("set y", "");
                        if (line.Contains(".SET y"))
                            SetY_Command = SetY_Command.Replace("SET Y", "");
                        string _object = null;
                        _object = SetY_Command.Substring(0, SetY_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetY_Command.EndsWith(" ") || !SetY_Command.EndsWith(",") || !SetY_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetY_Command);

                                    string bruhhh = SetY_Command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string Y_pos_get = bruhhh.Substring(1);
                                    int Y_pos_Move_to = Convert.ToInt32(Y_pos_get);

                                    Console.WriteLine("Go to Y {0}", Y_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.Y != Y_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, Y_pos_Move_to);
                                                if (ctrl.Location.Y < Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, Y_pos_Move_to);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's Y :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set Y Command because of error");
                                    MessageBox.Show("Couldn't Excute Set Y Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Set X"))
                    {
                        string SetX_Command = line.ToString();
                        SetX_Command = SetX_Command.Replace("=", "");
                        if (line.Contains(".Set X"))
                            SetX_Command = SetX_Command.Replace("Set X", "");
                        if (line.Contains(".set X"))
                            SetX_Command = SetX_Command.Replace("set X", "");
                        if (line.Contains(".set x"))
                            SetX_Command = SetX_Command.Replace("set x", "");
                        if (line.Contains(".SET X"))
                            SetX_Command = SetX_Command.Replace("SET X", "");
                        string _object = null;
                        _object = SetX_Command.Substring(0, SetX_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetX_Command.EndsWith(" ") || !SetX_Command.EndsWith(",") || !SetX_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetX_Command);

                                    string xPos = SetX_Command.Replace(_object + ".", "");

                                    Console.WriteLine(xPos.Substring(1));

                                    string X_pos_get = xPos.Substring(1);
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                ctrl.Location = new Point(X_pos_Move_to, ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's X position :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Change Image"))
                    {
                        string changeImage_Command = line.ToString();
                        changeImage_Command = changeImage_Command.Replace("=", "");
                        changeImage_Command = changeImage_Command.Replace("Change Image", "");
                        string _object = null;
                        _object = changeImage_Command.Substring(0, changeImage_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!changeImage_Command.EndsWith(" ") || !changeImage_Command.EndsWith(",") || !changeImage_Command.EndsWith("."))
                                {
                                    Console.WriteLine(changeImage_Command);

                                    string bruhhh = changeImage_Command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string X_pos_get = bruhhh.Substring(1);


                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            ctrl.BackgroundImage = Image.FromFile(Global.project_dir + @"\" + X_pos_get.TrimStart());
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's X position :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Move To"))
                    {
                        string moveToLine = line.ToString();
                        moveToLine = moveToLine.Replace("=", "");
                        if (line.Contains(".Move to"))
                            moveToLine = moveToLine.Replace("Move to", "");
                        if (line.Contains(".Move To"))
                            moveToLine = moveToLine.Replace("Move To", "");
                        if (line.Contains(".move to"))
                            moveToLine = moveToLine.Replace("move to", "");
                        string _object = null;
                        _object = moveToLine.Substring(0, moveToLine.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!moveToLine.EndsWith(" ") || !moveToLine.EndsWith(",") || !moveToLine.EndsWith("."))
                                {
                                    Console.WriteLine(moveToLine);

                                    string bruhhh = moveToLine.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1, bruhhh.IndexOf(',')));

                                    string X_pos_get = bruhhh.Substring(1, bruhhh.IndexOf(','));
                                    X_pos_get = X_pos_get.Replace(",", "");
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("ehyufehyfu {0}", X_pos_Move_to.ToString());
                                    Console.WriteLine("ehyufehyfu {0}", bruhhh.ToString());
                                    Console.WriteLine(bruhhh.Substring(bruhhh.IndexOf(',')));

                                    string Y_pos_get = bruhhh.Substring(bruhhh.IndexOf(','));
                                    Y_pos_get = Y_pos_get.Replace(",", "");
                                    int Y_pos_Move_to = Convert.ToInt32(Y_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("Go to Y {0}", Y_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to && ctrl.Location.Y != Y_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.Y > Y_pos_Move_to && ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y + 1);
                                                if (ctrl.Location.Y < Y_pos_Move_to && ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y - 1);
                                                if (ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y);
                                                if (ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y);
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 1);
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 1);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch { MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name); }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Move to Command because of error");
                                    MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains("Physics = "))
                    {
                        string physics_command = line.ToString();
                        physics_command = physics_command.Replace("=", "");
                        physics_command = physics_command.Replace("Physics", "");
                        Console.WriteLine(physics_command);
                        string _object = null;
                        _object = physics_command.Substring(0, physics_command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("iufheruifjfefhu {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            MessageBox.Show("Alert!, Physics for the game engine is still being worked on, so please report any bugs to mervinpaismakeswindows14@gmail.com");
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (physics_command.EndsWith("true"))
                                {
                                    Console.WriteLine("physics = true");
                                    while (ctrl.Location.Y < 425)
                                    {
                                        Console.WriteLine(ctrl.Location.Y + "is the Pos of " + ctrl.Name);
                                        ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 2);
                                    }
                                }
                                else if (physics_command.EndsWith("false")) { Console.WriteLine("physics = false"); }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Play("))
                    {
                        string playSoundCommand = line.ToString();
                        if (line.Contains(".Play("))
                            playSoundCommand = playSoundCommand.Replace("Play(", "");
                        if (line.Contains(".PLAY("))
                            playSoundCommand = playSoundCommand.Replace("PLAY(", "");
                        if (line.Contains(".play("))
                            playSoundCommand = playSoundCommand.Replace("play(", "");
                        string _object = null;
                        _object = playSoundCommand.Substring(0, playSoundCommand.IndexOf('.'));
                        _object.Trim();
                        string song = playSoundCommand.Substring(playSoundCommand.IndexOf('.'));
                        song = song.Remove(0, 1);
                        song = song.Replace(")", "");
                        Console.WriteLine("move to object's name is {0}", _object);
                        Write(song);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            Write(ctrl.Name);
                            if (ctrl.Name.Trim() == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!playSoundCommand.EndsWith(" ") || !playSoundCommand.EndsWith(",") || !playSoundCommand.EndsWith("."))
                                {
                                    Console.WriteLine(playSoundCommand);

                                    string bruhhh = playSoundCommand.Replace(_object + ".", "");

                                    //Console.WriteLine(bruhhh.Substring(1));

                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            SoundPlayer soundPlr = new SoundPlayer();
                                            Write("Loading Sound!");
                                            soundPlr.SoundLocation = song;
                                            soundPlr.Play();
                                        }
                                    }
                                    catch (Exception eeeeee)
                                    {
                                        MessageBox.Show($"An Error Occured while moving\n {eeeeee.Message}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    lineCounter++;
                }
            }
            else if (path_file == null && stringTextArray != null && isFilePath == false)
            {
                string[] lines = null;
                lines = File.ReadAllLines(path_file/*.Trim()*/);
                int lineCounter = 1;
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    Global.line_count++;
                    if (line.EndsWith("();"))
                    {
                        string mainScriptName = line.ToString();
                        mainScriptName = mainScriptName.Replace("(", "");
                        mainScriptName = mainScriptName.Replace(")", "");
                        mainScriptName = mainScriptName.Replace(";", "");
                        if (!mainScriptName.EndsWith(" ") || !mainScriptName.EndsWith(",") || !mainScriptName.EndsWith("."))
                        {
                            Console.WriteLine(mainScriptName);

                            WriteToGameLogger($"Loading Script {mainScriptName}");
                            Console.WriteLine("loading script {0}...", mainScriptName);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{mainScriptName}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == mainScriptName + ".s14c")
                                        {
                                            try
                                            { Load_GameScripts(pathh + @"\" + mainScriptName + ".s14c", null, true); }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");
                                            }
                                        }
                                    }
                                }
                                WriteToGameLogger($"Finished Loading script '{mainScriptName}'");
                                Console.WriteLine("finished loading script {0}", mainScriptName + ".s14c");
                            }
                            catch
                            {
                                WriteToGameLogger($"Error While Trying to load Script '{mainScriptName}'", "err");
                                MessageBox.Show("An Error Occured while loading script :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Load Script because of error");
                            WriteToGameLogger("Couldn't Load Script because of invaild Char(Charactor) at the end of your command", "err");
                        }
                    }
                    if (line.StartsWith("Log(") || line.StartsWith("log(") || line.StartsWith("LOG("))
                    {
                        char qoutes = '"';
                        string TheLine = line;
                        if (line.StartsWith("Log("))
                            TheLine = TheLine.Replace("Log(", "");
                        if (line.StartsWith("log("))
                            TheLine = TheLine.Replace("log(", "");
                        if (line.StartsWith("LOG("))
                            TheLine = TheLine.Replace("LOG(", "");
                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                        {
                            Write(TheLine.ToString() + $" was logged to game engine");
                            WriteToGameLogger(TheLine.ToString() + "?");
                        }
                        else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                        {
                            foreach (string file in Directory.GetFiles(Global.project_dir))
                            {
                                if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                {
                                    string[] variableData = File.ReadAllLines(file);
                                    List<string> variableData_List = new List<string>(variableData);
                                    variableData_List.RemoveAt(2);
                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                    varData = varData.Replace("{", "");
                                    varData = varData.Trim();
                                    varData = varData.Replace("data", "");
                                    varData = varData.Replace(":", "");
                                    varData = varData.Replace(qoutes.ToString(), "");
                                    varData = varData.Trim();
                                    Write(varData.ToString() + $" was logged to game engine");
                                    WriteToGameLogger(varData.ToString() + "?");
                                }
                            }
                        }
                    }
                    if (line.StartsWith("Input(") || line.StartsWith("input(") || line.StartsWith("INPUT("))
                    {
                        bool enterKeyHit = false;
                        char qoutes = '"';
                        string TheLine = line;
                        if (line.StartsWith("Input("))
                            TheLine = TheLine.Replace("Input(", "");
                        if (line.StartsWith("input("))
                            TheLine = TheLine.Replace("input(", "");
                        if (line.StartsWith("INPUT("))
                            TheLine = TheLine.Replace("INPUT(", "");
                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                        {
                            Write(TheLine.ToString());
                            WriteToGameLogger("Asked; " + TheLine.ToString());
                        }
                        else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                        {
                            foreach (string file in Directory.GetFiles(Global.project_dir))
                            {
                                if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                {
                                    string[] variableData = File.ReadAllLines(file);
                                    List<string> variableData_List = new List<string>(variableData);
                                    variableData_List.RemoveAt(2);
                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                    varData = varData.Replace("{", "");
                                    varData = varData.Trim();
                                    varData = varData.Replace("data", "");
                                    varData = varData.Replace(":", "");
                                    varData = varData.Replace(qoutes.ToString(), "");
                                    varData = varData.Trim();
                                    Write(varData.ToString());
                                    WriteToGameLogger("Asked; " + varData.ToString());
                                }
                            }
                        }
                        inputTextbox.ReadOnly = false;
                        inputTextbox.Text = string.Empty;
                        inputTextbox.Visible = true;
                        void onEnterKeyPressed(object sender, KeyPressEventArgs keyPressEventArgs_e)
                        {
                            if (keyPressEventArgs_e.KeyChar == (char)Keys.Enter)
                            {
                                //Since we dont have to send any info to a variable, we just discard it
                                enterKeyHit = true;
                                inputTextbox.ReadOnly = true;
                                inputTextbox.Text = string.Empty;
                                inputTextbox.Visible = false;
                            }
                        }
                        inputTextbox.KeyPress += new KeyPressEventHandler(onEnterKeyPressed);
                        while (enterKeyHit == false)
                        {
                            wait(500);
                        }
                    }
                    if (line.StartsWith("random(") || line.StartsWith("Random(") || line.StartsWith("RANDOM("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("random("))
                            TheLine = TheLine.Replace("random(", "");
                        if (line.StartsWith("Random("))
                            TheLine = TheLine.Replace("Random(", "");
                        if (line.StartsWith("RANDOM("))
                            TheLine = TheLine.Replace("RANDOM(", "");
                        TheLine = TheLine.Replace(")", "");
                        TheLine = TheLine.Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1;
                        int number_1 = 0;
                        int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                            {
                                number_1 = Convert.ToInt32(element);
                            }
                            if (counter == 2)
                            {
                                number_2 = Convert.ToInt32(element);
                            }
                            counter++;
                        }
                        Random rd = new Random();
                        int rand_num = rd.Next(number_1, number_2);
                        Global.mathAnswer = rand_num;
                        Write(rand_num.ToString() + $" is result of a random number generation of numbers {number_1} and {number_2}");
                        WriteToGameLogger(rand_num.ToString() + $" is result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Add(") || line.StartsWith("add(") || line.StartsWith("ADD("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Add("))
                            TheLine = TheLine.Replace("Add(", "");
                        if (line.StartsWith("add("))
                            TheLine = TheLine.Replace("add(", "");
                        if (line.StartsWith("ADD("))
                            TheLine = TheLine.Replace("ADD(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1; int number_1 = 0; int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);
                            counter++;
                        }
                        Math_statements("Add", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is sum(result) of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is sum(result) of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Minus(") || line.StartsWith("Subtract(") || line.StartsWith("minus(") ||
                        line.StartsWith("subtract(") || line.StartsWith("MINUS(") || line.StartsWith("SUBTRACT("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Minus("))
                            TheLine = TheLine.Replace("Minus(", "");
                        if (line.StartsWith("Subtract("))
                            TheLine = TheLine.Replace("Subtract(", "");
                        if (line.StartsWith("minus("))
                            TheLine = TheLine.Replace("minus(", "");
                        if (line.StartsWith("subtract("))
                            TheLine = TheLine.Replace("subtract(", "");
                        if (line.StartsWith("MINUS("))
                            TheLine = TheLine.Replace("MINUS(", "");
                        if (line.StartsWith("SUBTRACT("))
                            TheLine = TheLine.Replace("SUBTRACT(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1; int number_1 = 0; int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Minus", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the difference(result) of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the difference(result) of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Multiply(") || line.StartsWith("multiply(") || line.StartsWith("MULTIPLY("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Multiply("))
                            TheLine = TheLine.Replace("Multiply(", "");
                        if (line.StartsWith("multiply("))
                            TheLine = TheLine.Replace("multiply(", "");
                        if (line.StartsWith("MULTIPLY("))
                            TheLine = TheLine.Replace("MULTIPLY(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1; int number_1 = 0; int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Multiply", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Divide(") || line.StartsWith("divide(") || line.StartsWith("DIVIDE("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Divide("))
                            TheLine = TheLine.Replace("Divide(", "");
                        if (line.StartsWith("divide("))
                            TheLine = TheLine.Replace("divide(", "");
                        if (line.StartsWith("DIVIDE("))
                            TheLine = TheLine.Replace("DIVIDE(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string[] TheLineArray = TheLine.Split(',').ToArray();
                        int counter = 1; int number_1 = 0; int number_2 = 0;
                        foreach (string element in TheLineArray)
                        {
                            if (counter == 1)
                                number_1 = Convert.ToInt32(element);
                            if (counter == 2)
                                number_2 = Convert.ToInt32(element);

                            counter++;
                        }
                        Math_statements("Divide", number_1, number_2);
                        Write(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                        WriteToGameLogger(Global.mathAnswer.ToString() + $" is the Result of numbers {number_1} and {number_2}");
                    }
                    if (line.StartsWith("Sqrt(") || line.StartsWith("sqrt(") || line.StartsWith("SQRT("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Sqrt("))
                            TheLine = TheLine.Replace("Sqrt(", "");
                        if (line.StartsWith("sqrt("))
                            TheLine = TheLine.Replace("sqrt(", "");
                        if (line.StartsWith("SQRT("))
                            TheLine = TheLine.Replace("SQRT(", "");
                        TheLine = TheLine.Replace(")", "");
                        TheLine = TheLine.Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Sqrt", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Cos(") || line.StartsWith("cos(") || line.StartsWith("COS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Cos("))
                            TheLine = TheLine.Replace("Cos(", "");
                        if (line.StartsWith("cos("))
                            TheLine = TheLine.Replace("cos(", "");
                        if (line.StartsWith("COS("))
                            TheLine = TheLine.Replace("COS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Cos", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Tan(") || line.StartsWith("tan(") || line.StartsWith("TAN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Tan("))
                            TheLine = TheLine.Replace("Tan(", "");
                        if (line.StartsWith("tan("))
                            TheLine = TheLine.Replace("tan(", "");
                        if (line.StartsWith("TAN("))
                            TheLine = TheLine.Replace("TAN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Tan", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Abs(") || line.StartsWith("abs(") || line.StartsWith("ABS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Abs("))
                            TheLine = TheLine.Replace("Abs(", "");
                        if (line.StartsWith("abs("))
                            TheLine = TheLine.Replace("abs(", "");
                        if (line.StartsWith("ABS("))
                            TheLine = TheLine.Replace("ABS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Abs", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Acos(") || line.StartsWith("acos(") || line.StartsWith("ACOS("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Acos("))
                            TheLine = TheLine.Replace("Acos(", "");
                        if (line.StartsWith("acos("))
                            TheLine = TheLine.Replace("acos(", "");
                        if (line.StartsWith("ACOS("))
                            TheLine = TheLine.Replace("ACOS(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Acos", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Asin(") || line.StartsWith("asin(") || line.StartsWith("ASIN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Asin("))
                            TheLine = TheLine.Replace("Asin(", "");
                        if (line.StartsWith("asin("))
                            TheLine = TheLine.Replace("asin(", "");
                        if (line.StartsWith("ASIN("))
                            TheLine = TheLine.Replace("ASIN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Asin", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Atan(") || line.StartsWith("atan(") || line.StartsWith("ATAN("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Atan("))
                            TheLine = TheLine.Replace("Atan(", "");
                        if (line.StartsWith("atan("))
                            TheLine = TheLine.Replace("atan(", "");
                        if (line.StartsWith("ATAN("))
                            TheLine = TheLine.Replace("ATAN(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Atan", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("MLog(") || line.StartsWith("mlog(") || line.StartsWith("MLOG("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("MLog("))
                            TheLine = TheLine.Replace("MLog(", "");
                        if (line.StartsWith("mlog("))
                            TheLine = TheLine.Replace("mlog(", "");
                        if (line.StartsWith("MLOG("))
                            TheLine = TheLine.Replace("MLOG(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("MLog", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Cosh(") || line.StartsWith("cosh(") || line.StartsWith("COSH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Cosh("))
                            TheLine = TheLine.Replace("Cosh(", "");
                        if (line.StartsWith("cosh("))
                            TheLine = TheLine.Replace("cosh(", "");
                        if (line.StartsWith("COSH("))
                            TheLine = TheLine.Replace("COSH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Cosh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Sinh(") || line.StartsWith("sinh(") || line.StartsWith("SINH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Sinh("))
                            TheLine = TheLine.Replace("Sinh(", "");
                        if (line.StartsWith("sinh("))
                            TheLine = TheLine.Replace("sinh(", "");
                        if (line.StartsWith("SINH("))
                            TheLine = TheLine.Replace("SINH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Sinh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("Tanh(") || line.StartsWith("tanh(") || line.StartsWith("TANH("))
                    {
                        string TheLine = line;
                        if (line.StartsWith("Tanh("))
                            TheLine = TheLine.Replace("Tanh(", "");
                        if (line.StartsWith("tanh("))
                            TheLine = TheLine.Replace("tanh(", "");
                        if (line.StartsWith("TANH("))
                            TheLine = TheLine.Replace("TANH(", "");
                        TheLine = TheLine.Replace(")", "").Trim();
                        string TheLineArray = TheLine.Trim().ToString();
                        int number_1 = Convert.ToInt32(TheLineArray);
                        Math_statements("Tanh", number_1, 0);
                        Write($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                        WriteToGameLogger($"{Global.mathAnswer.ToString()} is result of number {number_1}");
                    }
                    if (line.StartsWith("var"))
                    {
                        string varReplaced = line.Replace("var", "");
                        varReplaced = varReplaced.TrimStart();
                        string fileName = null;

                        if (Global.project_dir != null)
                            fileName = Global.project_dir + @"\" + varReplaced + ".json";
                        else
                            throw new ProjectPathError(Global.project_dir);

                        try
                        {
                            if (File.Exists(fileName))
                                File.Delete(fileName);

                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                char qoutes = '"';
                                sw.WriteLine("{");
                                sw.WriteLine(qoutes + "data" + qoutes + ": null");
                                sw.WriteLine("}");
                            }

                            using (StreamReader sr = File.OpenText(fileName))
                            {
                                string s = "";
                                while ((s = sr.ReadLine()) != null)
                                {
                                    Console.WriteLine(s);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine(Ex.ToString());
                        }
                    }
                    foreach (string file in Directory.GetFiles(Global.project_dir))
                    {
                        if (line.StartsWith($"{Path.GetFileName(file.Replace(".json", ""))} ="))
                        {
                            string varReplaced = line.Substring(line.LastIndexOf(@"\") + 1, line.Length);
                            Write(varReplaced);
                            char[] fullLine = varReplaced.ToCharArray();
                            int equalSignPos = 0;
                            int counnter = 0;
                            foreach (char char_ in fullLine)
                            {
                                if (char_ == '=')
                                {
                                    equalSignPos = counnter;
                                    break;
                                }
                                counnter++;
                            }
                            string aftrEqualsPt = varReplaced.Substring(equalSignPos + 1); Write(aftrEqualsPt);
                            string beforEqualsPt = varReplaced.Substring(0, equalSignPos + 1); Write(beforEqualsPt);
                            beforEqualsPt = beforEqualsPt.Replace("=", "");
                            beforEqualsPt = beforEqualsPt.TrimEnd();
                            varReplaced = varReplaced.TrimStart('"');
                            string fileName = null;
                            char[] textLeArray = varReplaced.ToCharArray();
                            if (Global.project_dir != null)
                            {
                                fileName = Global.project_dir + @"\" + Path.GetFileName(beforEqualsPt) + ".json";
                                Write(fileName + " is the file name");
                            }
                            else
                            {
                                throw new ProjectPathError(Global.project_dir);
                            }
                            try
                            {
                                aftrEqualsPt = aftrEqualsPt.Trim();
                                aftrEqualsPt = aftrEqualsPt.Replace('.'.ToString(), "");
                                Write(aftrEqualsPt + " is the orignal");
                                char qoutes = '"';
                                varReplaced = varReplaced.Replace(".", "");
                                if (aftrEqualsPt.StartsWith("add(") || aftrEqualsPt.StartsWith("Add(") || aftrEqualsPt.StartsWith("ADD("))
                                {

                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("add("))
                                        finding_both_ints = finding_both_ints.Replace("add", "");
                                    if (aftrEqualsPt.StartsWith("Add("))
                                        finding_both_ints = finding_both_ints.Replace("Add", "");
                                    if (aftrEqualsPt.StartsWith("ADD("))
                                        finding_both_ints = finding_both_ints.Replace("ADD", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Add", number1, number2);
                                    Write(Global.mathAnswer + " is the result");


                                    string[] fileDOTWrite = {
                                        "{",
                                            $"{qoutes}data{qoutes} : {qoutes}{Global.mathAnswer.ToString()}{qoutes}",
                                        "}"
                                    };
                                    File.WriteAllLines(fileName, fileDOTWrite);
                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {qoutes}{Global.mathAnswer.ToString()}{qoutes}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("minus(") || aftrEqualsPt.StartsWith("Minus(") || aftrEqualsPt.StartsWith("MINUS(") || aftrEqualsPt.StartsWith("subtract(") || aftrEqualsPt.StartsWith("Subtract(") || aftrEqualsPt.StartsWith("SUBTRACT("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("minus("))
                                        finding_both_ints = finding_both_ints.Replace("minus", "");
                                    if (aftrEqualsPt.StartsWith("Minus("))
                                        finding_both_ints = finding_both_ints.Replace("Minus", "");
                                    if (aftrEqualsPt.StartsWith("MINUS("))
                                        finding_both_ints = finding_both_ints.Replace("MINUS", "");
                                    if (aftrEqualsPt.StartsWith("subtract("))
                                        finding_both_ints = finding_both_ints.Replace("subtract", "");
                                    if (aftrEqualsPt.StartsWith("Subtract("))
                                        finding_both_ints = finding_both_ints.Replace("Subtract", "");
                                    if (aftrEqualsPt.StartsWith("SUBTRACT("))
                                        finding_both_ints = finding_both_ints.Replace("SUBTRACT", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Subtract", number1, number2);
                                    Write(Global.mathAnswer + " is the result");

                                    if (File.Exists(fileName))
                                    {
                                        File.Delete(fileName);
                                    }


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("multiply(") || aftrEqualsPt.StartsWith("Multiply(") || aftrEqualsPt.StartsWith("MULTIPLY("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("multiply", "");
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("Multiply", "");
                                    if (aftrEqualsPt.StartsWith("multiply("))
                                        finding_both_ints = finding_both_ints.Replace("MULTIPLY", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Multiply", number1, number2);
                                    Write(Global.mathAnswer + " is the result");
                                    if (File.Exists(fileName))
                                    {
                                        File.Delete(fileName);
                                    }


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("divide(") || aftrEqualsPt.StartsWith("Divide(") || aftrEqualsPt.StartsWith("DIVIDE("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("divide("))
                                        finding_both_ints = finding_both_ints.Replace("divide", "");
                                    if (aftrEqualsPt.StartsWith("Divide("))
                                        finding_both_ints = finding_both_ints.Replace("Divide", "");
                                    if (aftrEqualsPt.StartsWith("DIVIDE("))
                                        finding_both_ints = finding_both_ints.Replace("DIVIDE", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Math_statements("Divide", number1, number2);
                                    Write(Global.mathAnswer + " is the result");


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {Global.mathAnswer.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("random(") || aftrEqualsPt.StartsWith("Random(") || aftrEqualsPt.StartsWith("RANDOM("))
                                {
                                    string finding_both_ints = aftrEqualsPt.Replace(".", "");
                                    Write(finding_both_ints);
                                    if (aftrEqualsPt.StartsWith("random("))
                                        finding_both_ints = finding_both_ints.Replace("random", "");
                                    if (aftrEqualsPt.StartsWith("Random("))
                                        finding_both_ints = finding_both_ints.Replace("Random", "");
                                    if (aftrEqualsPt.StartsWith("RANDOM("))
                                        finding_both_ints = finding_both_ints.Replace("RANDOM", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace(")", "");
                                    Write(finding_both_ints);
                                    finding_both_ints = finding_both_ints.Replace("(", "");
                                    Write(finding_both_ints);
                                    int number1 = Convert.ToInt32(finding_both_ints.Split(',')[0]);
                                    int number2 = Convert.ToInt32(finding_both_ints.Split(',')[1]);
                                    Random rd = new Random();
                                    int stringNewData_int = rd.Next(number1, number2);
                                    Write(stringNewData_int + " is the result");


                                    using (StreamWriter sw = File.CreateText(fileName))
                                    {
                                        Write("Editing String");
                                        sw.WriteLine("{");
                                        sw.WriteLine($"{qoutes}data{qoutes} : {stringNewData_int.ToString()}");
                                        sw.WriteLine("}");
                                    }

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith("Input(") || aftrEqualsPt.StartsWith("input(") || aftrEqualsPt.StartsWith("INPUT("))
                                {
                                    bool enterKeyWasHit = false;
                                    string TheLine = aftrEqualsPt;
                                    if (aftrEqualsPt.StartsWith("Input("))
                                        TheLine = TheLine.Replace("Input(", "");
                                    if (aftrEqualsPt.StartsWith("input("))
                                        TheLine = TheLine.Replace("input(", "");
                                    if (aftrEqualsPt.StartsWith("INPUT("))
                                        TheLine = TheLine.Replace("INPUT(", "");
                                    TheLine = TheLine.Replace(")", "");

                                    if (TheLine.StartsWith(qoutes.ToString()) && TheLine.StartsWith(qoutes.ToString()))
                                    {
                                        Write(TheLine.ToString());
                                        WriteToGameLogger("Asked; " + TheLine.ToString());
                                    }
                                    else if (!TheLine.StartsWith(qoutes.ToString()) && !TheLine.StartsWith(qoutes.ToString()))
                                    {
                                        foreach (string file_toAskQuestionData in Directory.GetFiles(Global.project_dir))
                                        {
                                            if (Path.GetFileName(file.Replace(".json", "")) == TheLine)
                                            {
                                                string[] variableData = File.ReadAllLines(file);
                                                List<string> variableData_List = new List<string>(variableData);
                                                variableData_List.RemoveAt(2);
                                                string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                                varData = varData.Replace("{", "");
                                                varData = varData.Trim();
                                                varData = varData.Replace("data", "");
                                                varData = varData.Replace(":", "");
                                                varData = varData.Replace(qoutes.ToString(), "");
                                                varData = varData.Trim();
                                                Write(varData.ToString());
                                                WriteToGameLogger("Asked; " + varData.ToString());
                                            }
                                        }
                                    }
                                    inputTextbox.ReadOnly = false;
                                    inputTextbox.Text = string.Empty;
                                    inputTextbox.Visible = true;
                                    inputTextbox.Focus();
                                    void onEnterKeyPressed(object sender, KeyPressEventArgs keyPressEventArgs_e)
                                    {
                                        if (keyPressEventArgs_e.KeyChar == (char)Keys.Enter)
                                        {
                                            string[] textToWrite =
                                            {
                                                "{",
                                                $"{qoutes}data{qoutes} : {aftrEqualsPt}",
                                                "}"
                                            };

                                            File.WriteAllLines(fileName, textToWrite);
                                            wait(250);
                                            enterKeyWasHit = true;
                                            inputTextbox.ReadOnly = true;
                                            inputTextbox.Text = string.Empty;
                                            inputTextbox.Visible = false;
                                        }
                                    }
                                    inputTextbox.KeyPress += new KeyPressEventHandler(onEnterKeyPressed);
                                    while (enterKeyWasHit == false)
                                    {
                                        wait(750);
                                    }
                                }
                                else if (aftrEqualsPt.StartsWith(qoutes.ToString()) && aftrEqualsPt.EndsWith(qoutes.ToString()))
                                {

                                    aftrEqualsPt.Replace(qoutes.ToString(), "");
                                    string[] textToWrite =
                                    {
                                        "{",
                                        $"{qoutes}data{qoutes} : {aftrEqualsPt}",
                                        "}"
                                    };

                                    File.WriteAllLines(fileName, textToWrite);

                                    using (StreamReader sr = File.OpenText(fileName))
                                    {
                                        string s = "";
                                        while ((s = sr.ReadLine()) != null)
                                        {
                                            Console.WriteLine(s);
                                        }
                                    }
                                }
                                else if (!aftrEqualsPt.StartsWith(qoutes.ToString()) && !aftrEqualsPt.EndsWith(qoutes.ToString()))
                                {
                                    aftrEqualsPt = aftrEqualsPt.Trim();
                                    bool isAnInt = aftrEqualsPt.All(char.IsDigit);
                                    if (isAnInt == false)
                                    {
                                        if (aftrEqualsPt == "true" || aftrEqualsPt == "false")
                                        {
                                            if (aftrEqualsPt == "true")
                                            {
                                                using (StreamWriter sw = File.CreateText(fileName))
                                                {
                                                    Write("Editing String");
                                                    sw.WriteLine("{");
                                                    sw.WriteLine($"{qoutes}data{qoutes} : true");
                                                    sw.WriteLine("}");
                                                }

                                                using (StreamReader sr = File.OpenText(fileName))
                                                {
                                                    string s = "";
                                                    while ((s = sr.ReadLine()) != null)
                                                    {
                                                        Console.WriteLine(s);
                                                    }
                                                }
                                            }
                                            if (aftrEqualsPt == "false")
                                            {
                                                using (StreamWriter sw = File.CreateText(fileName))
                                                {
                                                    Write("Editing String");
                                                    sw.WriteLine("{");
                                                    sw.WriteLine($"{qoutes}data{qoutes} : false");
                                                    sw.WriteLine("}");
                                                }

                                                using (StreamReader sr = File.OpenText(fileName))
                                                {
                                                    string s = "";
                                                    while ((s = sr.ReadLine()) != null)
                                                    {
                                                        Console.WriteLine(s);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Write("Finding files for string data");
                                            foreach (string var_file in Directory.GetFiles(Global.project_dir))
                                            {
                                                Write(Path.GetFileName(var_file));
                                                if (aftrEqualsPt.StartsWith(Path.GetFileName(var_file.Replace(".json", "").Trim())))
                                                {
                                                    Write("Found file!");
                                                    string[] variableData = File.ReadAllLines(var_file);
                                                    List<string> variableData_List = new List<string>(variableData);
                                                    variableData_List.RemoveAt(2);
                                                    string varData = String.Join(Environment.NewLine, variableData_List.ToArray());
                                                    varData = varData.Replace("{", "");
                                                    varData = varData.Trim();
                                                    varData = varData.Replace("data", "");
                                                    varData = varData.Replace(":", "");
                                                    varData = varData.Replace(qoutes.ToString(), "");
                                                    varData = varData.Trim();
                                                    Write(varData);
                                                    using (StreamWriter sw = File.CreateText(fileName))
                                                    {
                                                        Write($"Editing String ({fileName})");
                                                        sw.WriteLine("{");
                                                        sw.WriteLine($"{qoutes}data{qoutes} : {qoutes}{varData}{qoutes}");
                                                        sw.WriteLine("}");
                                                    }

                                                    using (StreamReader sr = File.OpenText(fileName))
                                                    {
                                                        string s = "";
                                                        while ((s = sr.ReadLine()) != null)
                                                        {
                                                            Console.WriteLine(s);
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else if (isAnInt == true)
                                    {
                                        aftrEqualsPt = aftrEqualsPt.Trim();
                                        using (StreamWriter sw = File.CreateText(fileName))
                                        {
                                            Write("Editing String");
                                            sw.WriteLine("{");
                                            sw.WriteLine($"{qoutes}data{qoutes} : {aftrEqualsPt}");
                                            sw.WriteLine("}");
                                        }

                                        using (StreamReader sr = File.OpenText(fileName))
                                        {
                                            string s = "";
                                            while ((s = sr.ReadLine()) != null)
                                            {
                                                Console.WriteLine(s);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                Console.WriteLine(Ex.ToString());
                            }
                        }
                    }
                    if (line.StartsWith("if") && line.EndsWith("then")) //Messy Code :/
                    {
                        string if_lines = line;
                        string[] cooool = if_lines.Split(' ');
                        for (int i = 0; i < cooool.Length; i++)
                        {
                            if (cooool[i] == "if")
                            {
                                string eee = if_lines.Substring(i, if_lines.Length);
                                cooool = eee.Split(' ');
                            }
                        }
                        int if_Line_Pos = 0;
                        int then_Line_Pos = 0;
                        bool if_Found = false;
                        bool end_Found = false;
                        int countsdfud = 0;
                        for (int i = 0; i < cooool.Length; i++)
                        {
                            if (cooool[i] == "if")
                            {
                                if_Line_Pos = 0;
                                if_Found = true;
                                Write("if found at " + i);
                            }

                            if (cooool[i] == "then")
                            {
                                then_Line_Pos = i;
                                end_Found = true;
                                Write("then found at " + i);
                            }

                            ///////////////
                            if (if_Line_Pos <= then_Line_Pos && if_Found == true && end_Found == true)
                            {
                                Write("If statment Found :D");
                                line.Replace("if", "");
                                line.Replace("then", "");
                                if_statement_Handle(path_file, lineCounter - 1);
                                if (Global.IfStatementConditionsAreMet == false)
                                {
                                    //Do nothing :/
                                }
                                else if (Global.IfStatementConditionsAreMet == true)
                                {
                                    return;// do note that in the if statement handler, it will continue the main script after the if statement script is done so there is no issue :)
                                }
                            }
                            else if (if_Line_Pos > then_Line_Pos)//else if the "if" keyword is after "then" keyword, there is to try and handle the isssue
                            {
                                try
                                {
                                    //An Exception im trying to fix
                                    Write("=======IMPORTANT========");
                                    Write(if_Line_Pos.ToString() + " & " + cooool[i] + "\n" + if_Line_Pos.ToString() + " & " + countsdfud);
                                    var newArray2 = cooool[if_Line_Pos + 1];
                                    if (newArray2 == "then") { end_Found = true; }
                                    else { Write(newArray2 + " is inside the if statement"); }

                                    string[] keywords = { "then", "or", "else" };
                                    if (if_Found == true && end_Found == false) { Write("ERROR - NO 'THEN' IN IF STATMENT FOUND"); }
                                    if (if_Found == false && end_Found == true) { Write("ERROR - NO 'IF' IN IF STATMENT FOUND"); }
                                }
                                catch (Exception e) { Write("Operation FAILED, " + e.ToString()); }
                            }
                        }
                    }

                    foreach (Control buttonCTRL in game_window.Controls)
                    {
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseClick("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseClick("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseClick(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void onMouseClickCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.Click += new EventHandler(onMouseClickCUSTOM);
                            //WriteToGameLogger($"Loading Script {buttonOnClickFunctionLine}");
                            //Console.WriteLine("loading script {0}...", buttonOnClickFunctionLine);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseDoubleClick("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseDoubleClick("))
                            { buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseDoubleClick(", ""); }
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void onMouseDoubleClickCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.DoubleClick += new EventHandler(onMouseDoubleClickCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseEnter("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseEnter("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseEnter(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void OnMouseEnterCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.MouseEnter += new EventHandler(OnMouseEnterCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                        if (line.StartsWith($"{buttonCTRL.Name}.OnMouseLeave("))
                        {
                            string buttonOnClickFunctionLine = line.ToString();
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace($"{buttonCTRL.Name}.", "");
                            if (buttonOnClickFunctionLine.StartsWith("OnMouseLeave("))
                                buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace("OnMouseLeave(", "");
                            buttonOnClickFunctionLine = buttonOnClickFunctionLine.Replace(")", "");
                            string methodToLoad = buttonOnClickFunctionLine;

                            void OnMouseEnterCUSTOM(object sender, EventArgs e)
                            {
                                Load_GameScripts(Global.project_dir + @"\" + methodToLoad + ".s14c", null, true);
                            }

                            buttonCTRL.MouseLeave += new EventHandler(OnMouseEnterCUSTOM);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    WriteToGameLogger($"Loading script '{buttonOnClickFunctionLine}'");
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == buttonOnClickFunctionLine + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + buttonOnClickFunctionLine + ".s14c", null, true);
                                                WriteToGameLogger($"Finished Loading script '{buttonOnClickFunctionLine}'");
                                                Console.WriteLine("finished loading script {0}", buttonOnClickFunctionLine + ".s14c");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                WriteToGameLogger("Stopped Game/Project/Simulation for the safty of Scripts and Files", "warn");

                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show($"An Error Occurred while loading script {buttonOnClickFunctionLine}");
                            }
                        }
                    }
                    if (line.Contains("Clone(") || line.Contains("clone("))
                    {
                        string cloneCommand = line.ToString();
                        if (cloneCommand.StartsWith("Clone("))
                            cloneCommand = cloneCommand.Replace("Clone(", "");
                        if (cloneCommand.StartsWith("clone("))
                            cloneCommand = cloneCommand.Replace("clone(", "");
                        cloneCommand = cloneCommand.Replace(")", "");
                        if (!cloneCommand.EndsWith(" ") || !cloneCommand.EndsWith(",") || !cloneCommand.EndsWith("."))
                        {
                            Console.WriteLine(cloneCommand);
                            Console.WriteLine("cloning...");
                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    foreach (Control ctrl in game_window.Controls)
                                    {
                                        if (ctrl.Name == cloneCommand)
                                        {
                                            Control NewControl = new Control(ctrl, ctrl.Name + "_clone");
                                            NewControl.Location = new Point(NewControl.Location.X + 10, NewControl.Location.Y + 10);
                                        }
                                    }
                                }
                                Console.WriteLine("finished cloning");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while cloning :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Cloning Command because of error");
                            MessageBox.Show("Couldn't Excute Cloning Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("wait("))
                    {
                        string wait_Command = line.ToString();
                        wait_Command = wait_Command.Replace("wait(", "");
                        wait_Command = wait_Command.Replace(")", "");
                        if (!wait_Command.EndsWith(" ") || !wait_Command.EndsWith(",") || !wait_Command.EndsWith("."))
                        {
                            Console.WriteLine(wait_Command);
                            try { if (Global.game_stopped == false) { wait(Convert.ToInt32(wait_Command) * 1000); } }
                            catch { MessageBox.Show("An Error Occured while waiting :("); }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Wait Command because of error");
                            MessageBox.Show("Couldn't Excute Wait Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.StartsWith("Remove(") || line.StartsWith("remove(") || line.StartsWith("REMOVE("))
                    {
                        char qoutes = '"';
                        string TheLine = line;

                        if (line.StartsWith("Remove(")) { TheLine = TheLine.Replace("Remove(", ""); }
                        if (line.StartsWith("remove(")) { TheLine = TheLine.Replace("remove(", ""); }
                        if (line.StartsWith("REMOVE(")) { TheLine = TheLine.Replace("REMOVE(", ""); }

                        TheLine = TheLine.Replace(")", "");
                        if (TheLine.Contains(qoutes.ToString())) { TheLine = TheLine.Replace(qoutes.ToString(), ""); }
                        if (TheLine == "All") { game_window.Controls.Clear(); }
                        else
                        {
                            var ControlToRemove = game_window.Controls.Find(TheLine, true).FirstOrDefault();
                            if (ControlToRemove != null) { game_window.Controls.Remove(ControlToRemove); }
                            else { WriteToGameLogger($"Error; Unable To Remove Object Named '{TheLine}' because The Object Was Not Found", "err"); }
                        }
                    }
                    foreach (Control control in this.game_window.Controls)
                    {
                        if (line.Contains($"{control.Name}.Enabled"))
                        {
                            string enabled_Command = line.ToString();
                            enabled_Command = enabled_Command.Replace("=", "");
                            enabled_Command = enabled_Command.Replace(".Enabled", "");
                            enabled_Command = enabled_Command.Replace(control.Name, "");
                            enabled_Command = enabled_Command.Trim();
                            if (!enabled_Command.EndsWith(" ") || !enabled_Command.EndsWith(",") || !enabled_Command.EndsWith("."))
                            {
                                Console.WriteLine($"setting {control.Name} to {enabled_Command}...");
                                try
                                {
                                    if (Global.game_stopped == false)
                                    {
                                        if (enabled_Command == "true") { control.Enabled = true; }
                                        else if (enabled_Command == "false") { control.Enabled = false; }
                                    }
                                }
                                catch { MessageBox.Show("An Error Occured while waiting :("); }
                            }
                            else
                            {
                                Console.WriteLine("Couldn't Excute Move to Command because of error");
                                MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                            }
                        }
                    }
                    if (line.Contains("OnkeyPress"))
                    {
                        string keyPressCommands = line.ToString();
                        keyPressCommands = keyPressCommands.Replace(")", "");
                        keyPressCommands = keyPressCommands.Replace("OnkeyPress(", "");
                        string[] OnPressKeyarray = keyPressCommands.Split(',');
                        int itemNum = 1;
                        string keyToPress = null;
                        string methodToLoadAfterKeyPressed = null;
                        foreach (string item in OnPressKeyarray)
                        {
                            if (itemNum == 1)
                            {
                                keyToPress = item;
                            }
                            if (itemNum == 2)
                            {
                                methodToLoadAfterKeyPressed = item;
                            }
                            itemNum++;
                        }
                        if (!keyPressCommands.EndsWith(" ") || !keyPressCommands.EndsWith(",") || !keyPressCommands.EndsWith("."))
                        {
                            Console.WriteLine(keyPressCommands);

                            WriteToGameLogger("Waiting For Keypress");
                            Write("Waiting FOr UserKeypress");

                            try
                            {
                                this.game_window.Focus();
                                KeyEventHandler handler = (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                                this.game_window.KeyDown += handler;
                                //The below code has been removed since i cant manage to remove the event handler from the game_window.KeyDown Event
                                //this.game_window.KeyDown -= (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                                //this.game_window.KeyDown += (sender2, e2) => CustomFormKeyPressDetect(sender2, e2, keyToPress, methodToLoadAfterKeyPressed);
                            }
                            catch { MessageBox.Show("An Error Occured while waiting :("); }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains(".Set color") || line.Contains(".Set Color") || line.Contains(".set color"))
                    {
                        string SetColor_Command = line.ToString();
                        SetColor_Command = SetColor_Command.Replace("=", "");
                        if (line.Contains(".Set color")) { SetColor_Command = SetColor_Command.Replace("Set color", ""); }
                        if (line.Contains(".Set Color")) { SetColor_Command = SetColor_Command.Replace("Set Color", ""); }
                        if (line.Contains(".set color")) { SetColor_Command = SetColor_Command.Replace("set color", ""); }
                        string _object = null;
                        _object = SetColor_Command.Substring(0, SetColor_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("set color to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetColor_Command.EndsWith(" ") || !SetColor_Command.EndsWith(",") || !SetColor_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetColor_Command);
                                    string bruhhh = SetColor_Command.Replace(_object + ".", "");
                                    string Color_get = bruhhh.Substring(1);
                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            if (Color_get == " Red") { ctrl.BackColor = Color.Red; }
                                            else if (Color_get == " Green") { ctrl.BackColor = Color.Green; }
                                            else if (Color_get == " Blue") { ctrl.BackColor = Color.Blue; }
                                            else if (Color_get == " White") { ctrl.BackColor = Color.White; }
                                            else if (Color_get == " Black") { ctrl.BackColor = Color.Black; }
                                            else if (Color_get == " Cyan") { ctrl.BackColor = Color.Cyan; }
                                        }
                                        Console.WriteLine("color was set to {0}", Color_get);
                                        WriteToGameLogger("Finished Change Color Of Object");
                                    }
                                    catch { MessageBox.Show("An Error Occured while changing color {0} :(", ctrl.Name); }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute set color Command because of error");
                                    MessageBox.Show("Couldn't Excute set color Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Move by"))
                    {
                        string MoveBy_Command = line.ToString();
                        MoveBy_Command = MoveBy_Command.Replace("=", "");
                        if (line.Contains(".Move by")) { MoveBy_Command = MoveBy_Command.Replace("Move by", ""); }
                        if (line.Contains(".move by")) { MoveBy_Command = MoveBy_Command.Replace("move by", ""); }
                        if (line.Contains(".Move By")) { MoveBy_Command = MoveBy_Command.Replace("Move By", ""); }
                        if (line.Contains(".MOVE BY")) { MoveBy_Command = MoveBy_Command.Replace("MOVE BY", ""); }
                        string _object = null;
                        _object = MoveBy_Command.Substring(0, MoveBy_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!MoveBy_Command.EndsWith(" ") || !MoveBy_Command.EndsWith(",") || !MoveBy_Command.EndsWith("."))
                                {
                                    Console.WriteLine(MoveBy_Command);
                                    string bruhhh = MoveBy_Command.Replace(_object + ".", "");
                                    string X_pos_get = bruhhh.Substring(1);
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y);
                                                if (ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Move to Command because of error");
                                    WriteToGameLogger("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(", "err");
                                    MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Set Y"))
                    {
                        string SetY_Command = line.ToString();
                        SetY_Command = SetY_Command.Replace("=", "");
                        if (line.Contains(".Set Y"))
                            SetY_Command = SetY_Command.Replace("Set Y", "");
                        if (line.Contains(".set Y"))
                            SetY_Command = SetY_Command.Replace("set Y", "");
                        if (line.Contains(".set y"))
                            SetY_Command = SetY_Command.Replace("set y", "");
                        if (line.Contains(".SET y"))
                            SetY_Command = SetY_Command.Replace("SET Y", "");
                        string _object = null;
                        _object = SetY_Command.Substring(0, SetY_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetY_Command.EndsWith(" ") || !SetY_Command.EndsWith(",") || !SetY_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetY_Command);

                                    string bruhhh = SetY_Command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string Y_pos_get = bruhhh.Substring(1);
                                    int Y_pos_Move_to = Convert.ToInt32(Y_pos_get);

                                    Console.WriteLine("Go to Y {0}", Y_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.Y != Y_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, Y_pos_Move_to);
                                                if (ctrl.Location.Y < Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, Y_pos_Move_to);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's Y :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set Y Command because of error");
                                    MessageBox.Show("Couldn't Excute Set Y Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Set X"))
                    {
                        string SetX_Command = line.ToString();
                        SetX_Command = SetX_Command.Replace("=", "");
                        if (line.Contains(".Set X"))
                            SetX_Command = SetX_Command.Replace("Set X", "");
                        if (line.Contains(".set X"))
                            SetX_Command = SetX_Command.Replace("set X", "");
                        if (line.Contains(".set x"))
                            SetX_Command = SetX_Command.Replace("set x", "");
                        if (line.Contains(".SET X"))
                            SetX_Command = SetX_Command.Replace("SET X", "");
                        string _object = null;
                        _object = SetX_Command.Substring(0, SetX_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!SetX_Command.EndsWith(" ") || !SetX_Command.EndsWith(",") || !SetX_Command.EndsWith("."))
                                {
                                    Console.WriteLine(SetX_Command);

                                    string xPos = SetX_Command.Replace(_object + ".", "");

                                    Console.WriteLine(xPos.Substring(1));

                                    string X_pos_get = xPos.Substring(1);
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                ctrl.Location = new Point(X_pos_Move_to, ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's X position :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Change Image"))
                    {
                        string changeImage_Command = line.ToString();
                        changeImage_Command = changeImage_Command.Replace("=", "");
                        changeImage_Command = changeImage_Command.Replace("Change Image", "");
                        string _object = null;
                        _object = changeImage_Command.Substring(0, changeImage_Command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!changeImage_Command.EndsWith(" ") || !changeImage_Command.EndsWith(",") || !changeImage_Command.EndsWith("."))
                                {
                                    Console.WriteLine(changeImage_Command);

                                    string bruhhh = changeImage_Command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string X_pos_get = bruhhh.Substring(1);


                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            ctrl.BackgroundImage = Image.FromFile(Global.project_dir + @"\" + X_pos_get.TrimStart());
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} 's X position :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Move To"))
                    {
                        string moveToLine = line.ToString();
                        moveToLine = moveToLine.Replace("=", "");
                        if (line.Contains(".Move to"))
                            moveToLine = moveToLine.Replace("Move to", "");
                        if (line.Contains(".Move To"))
                            moveToLine = moveToLine.Replace("Move To", "");
                        if (line.Contains(".move to"))
                            moveToLine = moveToLine.Replace("move to", "");
                        string _object = null;
                        _object = moveToLine.Substring(0, moveToLine.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!moveToLine.EndsWith(" ") || !moveToLine.EndsWith(",") || !moveToLine.EndsWith("."))
                                {
                                    Console.WriteLine(moveToLine);

                                    string bruhhh = moveToLine.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1, bruhhh.IndexOf(',')));

                                    string X_pos_get = bruhhh.Substring(1, bruhhh.IndexOf(','));
                                    X_pos_get = X_pos_get.Replace(",", "");
                                    int X_pos_Move_to = Convert.ToInt32(X_pos_get);

                                    Console.WriteLine("ehyufehyfu {0}", X_pos_Move_to.ToString());
                                    Console.WriteLine("ehyufehyfu {0}", bruhhh.ToString());
                                    Console.WriteLine(bruhhh.Substring(bruhhh.IndexOf(',')));

                                    string Y_pos_get = bruhhh.Substring(bruhhh.IndexOf(','));
                                    Y_pos_get = Y_pos_get.Replace(",", "");
                                    int Y_pos_Move_to = Convert.ToInt32(Y_pos_get);

                                    Console.WriteLine("Go to X {0}", X_pos_Move_to);
                                    Console.WriteLine("Go to Y {0}", Y_pos_Move_to);
                                    Console.WriteLine("moving object = true");

                                    try
                                    {
                                        while (ctrl.Location.X != X_pos_Move_to && ctrl.Location.Y != Y_pos_Move_to)
                                        {
                                            if (Global.game_stopped == false)
                                            {
                                                if (ctrl.Location.Y > Y_pos_Move_to && ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y + 1);
                                                if (ctrl.Location.Y < Y_pos_Move_to && ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y - 1);
                                                if (ctrl.Location.X > X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X - 1, ctrl.Location.Y);
                                                if (ctrl.Location.X < X_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X + 1, ctrl.Location.Y);
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 1);
                                                if (ctrl.Location.Y > Y_pos_Move_to) ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 1);
                                                Console.WriteLine("ctrl x pos = {0}", ctrl.Location.X);
                                                Console.WriteLine("ctrl y pos = {0}", ctrl.Location.Y);
                                            }
                                        }
                                    }
                                    catch { MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name); }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Move to Command because of error");
                                    MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains("Physics = "))
                    {
                        string physics_command = line.ToString();
                        physics_command = physics_command.Replace("=", "");
                        physics_command = physics_command.Replace("Physics", "");
                        Console.WriteLine(physics_command);
                        string _object = null;
                        _object = physics_command.Substring(0, physics_command.IndexOf('.'));
                        _object.Trim();
                        Console.WriteLine("iufheruifjfefhu {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            MessageBox.Show("Alert!, Physics for the game engine is still being worked on, so please report any bugs to mervinpaismakeswindows14@gmail.com");
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (physics_command.EndsWith("true"))
                                {
                                    Console.WriteLine("physics = true");
                                    while (ctrl.Location.Y < 425)
                                    {
                                        Console.WriteLine(ctrl.Location.Y + "is the Pos of " + ctrl.Name);
                                        ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 2);
                                    }
                                }
                                else if (physics_command.EndsWith("false")) { Console.WriteLine("physics = false"); }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Play("))
                    {
                        string playSoundCommand = line.ToString();
                        if (line.Contains(".Play("))
                            playSoundCommand = playSoundCommand.Replace("Play(", "");
                        if (line.Contains(".PLAY("))
                            playSoundCommand = playSoundCommand.Replace("PLAY(", "");
                        if (line.Contains(".play("))
                            playSoundCommand = playSoundCommand.Replace("play(", "");
                        string _object = null;
                        _object = playSoundCommand.Substring(0, playSoundCommand.IndexOf('.'));
                        _object.Trim();
                        string song = playSoundCommand.Substring(playSoundCommand.IndexOf('.'));
                        song = song.Remove(0, 1);
                        song = song.Replace(")", "");
                        Console.WriteLine("move to object's name is {0}", _object);
                        Write(song);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            Write(ctrl.Name);
                            if (ctrl.Name.Trim() == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!playSoundCommand.EndsWith(" ") || !playSoundCommand.EndsWith(",") || !playSoundCommand.EndsWith("."))
                                {
                                    Console.WriteLine(playSoundCommand);

                                    string bruhhh = playSoundCommand.Replace(_object + ".", "");

                                    //Console.WriteLine(bruhhh.Substring(1));

                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            SoundPlayer soundPlr = new SoundPlayer();
                                            Write("Loading Sound!");
                                            soundPlr.SoundLocation = song;
                                            soundPlr.Play();
                                        }
                                    }
                                    catch (Exception eeeeee)
                                    {
                                        MessageBox.Show($"An Error Occured while moving\n {eeeeee.Message}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Set X Command because of error");
                                    MessageBox.Show("Couldn't Excute Set X Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    lineCounter++;
                }
            }
        }

        private void RunTime_Game_Window_Shown(object sender, EventArgs e)
        {
            this.debug_info_text.Text = "...";
            debug_info_text.ForeColor = SystemColors.ControlDark;
        }

        public void RemoveKeyDownEvents()
        {
            FieldInfo f1 = typeof(Control).GetField("EventKeyDown",
                BindingFlags.Static | BindingFlags.NonPublic);

            object obj = f1.GetValue(game_window);
            PropertyInfo pi = game_window.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(game_window, null);
            list.RemoveHandler(obj, list[obj]);
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Global.game_stopped = false;
            try
            {
                debug_info_text.ForeColor = SystemColors.ControlText;
                this.debug_info_text.Text = "Compiling Game";
                Game_Logger.Text = null;
                this.game_window.Controls.Clear();
                RemoveKeyDownEvents();
                wait(150);
                this.debug_info_text.Text = "Finished Compiling Game :D";
                LoadSceneGUI();
                wait(25);
                Load_GameScripts(Global.Project_GameScripts_File_Path, null, true);
            }
            catch (Exception exceptionError)
            {
                MessageBox.Show("AN EXCEPTION ERROR OCCURED, SEE BELOW:\n\n" + exceptionError.Message.ToString() + "\n\n This Error was caused by your code, if you checked your code and everything seems correct, contact mervinpaismakeswindows14@gmail.com to contact Mervin(creator of the game engine) to fix this bug \n\n Thanks for reading");
                WriteToGameLogger($"GAMEENGINE - An Exception Error Occurred while running game, the Exception is; \n\n{exceptionError}\n\n {exceptionError.Message}", "err");
                this.debug_info_text.Text = "Exception while compiling :(";
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            Global.game_stopped = true;
            debug_info_text.ForeColor = SystemColors.ControlDark;
        }

        private void RunTime_Game_Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == (char)Keys.Enter)
            {
                Type thisType = this.GetType();
                MethodInfo theMethod = thisType.GetMethod(e.KeyChar + "KeyPressAction");
                theMethod.Invoke(this, null);
            }*/
            //CustomFormKeyPressDetect(e.KeyChar);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///         ALERT: Below this Warning should ONLY be CUSTOM methods, Methods which are special since they may server an important function to the statements
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void if_statement_Handle(string path, int line_where_if_is_on)
        {
            string[] file_lines = File.ReadAllLines(path);
            List<string> file_lines_list = new List<string>(file_lines);

            //Step 1: Remove the top part of the code that isnt neccesary for the if statement
            Write(line_where_if_is_on.ToString() + "is the line where if is int");
            Write("\n");
            file_lines_list.RemoveRange(0, line_where_if_is_on); //Main part of removing

            //Step 2: Remove the lower, unneccesary part of the code
            List<string> lowerpartAfterIf = new List<string>();
            Write("\n"); Write("\n");
            Write(file_lines_list.Count.ToString() + "is the file_lines_list Count");
            Write("\n");
            Write(file_lines_list.IndexOf("end") + "is the index of end Count");
            int lineCounterr = 1;
            int end_keyword_is_on_line_number = 0;//since we dont know the line count "end" is on yet,i will just set it to zero
            int else_keyword_on_line_number = 0;
            foreach (string text in file_lines_list)
            {
                if (text == "else")
                {
                    else_keyword_on_line_number = lineCounterr;
                    lowerpartAfterIf = new List<string>(file_lines_list);
                    lowerpartAfterIf.RemoveRange(0, else_keyword_on_line_number);
                    Write($"line count of 'else' is {else_keyword_on_line_number}");
                }
                if (text == "end")
                {
                    end_keyword_is_on_line_number = lineCounterr;
                    lowerpartAfterIf = new List<string>(file_lines_list);
                    lowerpartAfterIf.RemoveRange(0, end_keyword_is_on_line_number);
                    Write($"line count of 'end' is {end_keyword_is_on_line_number}");
                }
                lineCounterr++;
            }
            Write("\n");
            List<string> ifStatmentAndContents = new List<string>(file_lines_list);
            file_lines_list.RemoveRange(end_keyword_is_on_line_number, file_lines_list.Count - end_keyword_is_on_line_number); //Main part of removing
            Write("Finished Job 1");
            ////////////////////////////////Now, The handling if statement part and what the "if" is itself////////////////////////////////
            List<string> theMainIfAbout = file_lines_list;
            Write("Spliting");
            theMainIfAbout.RemoveRange(1, theMainIfAbout.Count - 1);
            Write("Finished");
            foreach (string text in theMainIfAbout)
            {
                Write(text);
            }
            string result = string.Join(" ", theMainIfAbout);
            /////////////////////////////////Now We cant finally, just finally, check what if we are talking about in our statement/////////////////////////////
            ///First, variables. in the engine, we can set variables that can have a string or int value. do note that even if 
            ///two variables have the same name, if one have a different capitlization, in windows and Mac os, the file system 
            ///can understand they are different, and can sort them so it ok (sorry for the long explaintion)
            Write($"{Global.project_dir} is project dir");
            var variableToFind = Regex.Replace(result.Split()[1], @"[^=0-9a-zA-Z\ ]+", "");
            Write(variableToFind + " is the if statement contents");
            if (Directory.GetFiles(Global.project_dir, "*.json").Length != 0)
            {
                char[] textLeArray = variableToFind.ToCharArray();
                int equalsCharPos = 0;
                for (int i = 1; i < variableToFind.Length; i++)
                {
                    if (textLeArray[i].Equals('='))
                    {
                        equalsCharPos = i;
                        break;
                    }
                }
                bool fileFound = false;
                foreach (string file in Directory.GetFiles(Global.project_dir, "*.json"))
                {
                    if (file.EndsWith($"{variableToFind.ToString().Substring(0, equalsCharPos)}.json"))
                    {
                        fileFound = true;
                        Write($"{variableToFind} is the if statement whole");
                        string settingValue = variableToFind.Substring(equalsCharPos, variableToFind.Length - equalsCharPos);
                        settingValue = settingValue.Replace("==", "");
                        Write(settingValue + " is the settings val");
                        string theVariable = variableToFind.Substring(0, equalsCharPos);
                        Write(theVariable + " is the variable");
                        Write(variableToFind);
                        Write(equalsCharPos.ToString());
                        int theOperator_int = variableToFind.IndexOf("==");

                        string theOperator = variableToFind[theOperator_int].ToString() + variableToFind[theOperator_int + 1].ToString();
                        Write(theOperator + " is the operator");
                        if (theOperator == "==")//if the "if" statement is asking about "if x == y then" where x is the variable and y is the value
                        {
                            string str = File.ReadAllText(file);
                            Write(str);
                            char quotes = '"';
                            str = str.Replace("{", "");
                            str = str.Replace("}", "");
                            str = str.Replace($"{quotes}", "");
                            str = str.Replace($":", "");
                            str = str.Replace($"data", "");
                            str = str.Replace(" ", "");
                            str = str.Trim();
                            Write(str + " is a str"); Write(settingValue + " is a setting val");
                            if (settingValue == str)
                            {
                                Global.IfStatementConditionsAreMet = false;
                                for (int i = 0; i < ifStatmentAndContents.Count; i++)
                                {
                                    ifStatmentAndContents[i] = ifStatmentAndContents[i].Trim();
                                }
                                string[] theIfStatement = ifStatmentAndContents.ToArray();
                                theIfStatement = theIfStatement.Skip(1).ToArray();
                                theIfStatement = theIfStatement.Skip(theIfStatement.Length).ToArray();
                                Write(String.Join(Environment.NewLine, theIfStatement) + " | is the script thats gonna load");
                                Load_GameScripts(null, theIfStatement, false);
                                Load_GameScripts(null, lowerpartAfterIf.ToArray(), false);
                            }
                            else
                            {
                                if (else_keyword_on_line_number != 0) { }
                                else if (else_keyword_on_line_number == 0)
                                {
                                    Global.IfStatementConditionsAreMet = true;
                                    List<string> FullIfStatementPlusContents = new List<string>(ifStatmentAndContents);
                                    FullIfStatementPlusContents.RemoveRange(1, FullIfStatementPlusContents.Count - 1);
                                    string[] TheIfstatementArray = FullIfStatementPlusContents.ToArray();
                                    string theFirstIFstatementPartOnly = String.Join(Environment.NewLine, TheIfstatementArray);//this is the best solution to convert an array to a string for it(only if the array has 1 item)
                                    Write("if x = y then statement didnt match, continuing reading game script");
                                    WriteToGameLogger($"The if statement '{theFirstIFstatementPartOnly}' has been skiped due to its condition(s) not met, the Condition(s) is/are '{theVariable}'");
                                    Write(String.Join(Environment.NewLine, lowerpartAfterIf.ToArray()) + "| is the script thats gonna load");
                                    Load_GameScripts(null, lowerpartAfterIf.ToArray(), false);
                                }
                            }
                        }
                        break;
                    }
                    /*else if (!file.EndsWith($"{variableToFind.ToString()}.json") && file.EndsWith($".json"))
                    {
                        MessageBox.Show("User Code Defined Error;\n An Error Occured with Your code, This Error happened BECAUSE;\n\n A Variable in an If Statement was not found!, make sure you have your spelling of the variable correct(note: variable name checking is case sensitive!!!) \n \n please fix these error to run your program successfully :/");
                    }*/
                }
                if (fileFound == false)
                {
                    WriteToGameLogger($"looks like the variable in an if statement was not found,", "err");
                    WriteToGameLogger("Uh oh, the game runtime engine is now at an unsafe state, the engine will stop the project/simulation to prevent it to cause more errors", "err");
                    MessageBox.Show("Uh oh, the game runtime engine is now at an unsafe state, the engine will stop the project/simulation to prevent it to cause more errors");
                    this.Close();
                }
            }
            else if (Directory.GetFiles(Global.project_dir, "*.json").Length == 0)
            {
                MessageBox.Show("User Code Defined Error;\n An Error Occured with Your code, This Error happened BECAUSE;\n\n There are no variables that can be found in your project :/");
            }
        }
        public void Math_statements(string operator_, int num1, int num2)
        {
            if (operator_ == "add" || operator_ == "Add" || operator_ == "ADD" || operator_ == "+" || operator_ == "plus" || operator_ == "Plus" || operator_ == "PLUS")
            {
                var Result = num1 + num2;
                Global.mathAnswer = Result;
            }
            else if (operator_ == "subtract" || operator_ == "Subtract" || operator_ == "SUBTRACT" || operator_ == "-" || operator_ == "minus" || operator_ == "Minus" || operator_ == "MINUS")
            {
                var Result = num1 - num2;
                Global.mathAnswer = Result;
            }
            else if (operator_ == "multiply" || operator_ == "Multiply" || operator_ == "MULTIPLY" || operator_ == "*")
            {
                var Result = num1 * num2;
                Global.mathAnswer = Result;
            }
            else if (operator_ == "divide" || operator_ == "Divide" || operator_ == "DIVIDE" || operator_ == "/" || operator_ == "split" || operator_ == "Split" || operator_ == "SPLIT")
            {
                var Result = 0;
                string DivideByZeroErrorHandler = "GAME SCRIPTS ERROR; You cannot divide by 0 as this would either equal to 'Error' or 'inf'(also known as infinity)";
                if (num2 != 0)
                {
                    Result = num1 / num2;
                }
                else
                {
                    MessageBox.Show(DivideByZeroErrorHandler);
                }
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Sin" || operator_ == "sin" || operator_ == "SIN")
            {
                var Result = (int)Math.Sin(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Cos" || operator_ == "cos" || operator_ == "COS")
            {
                var Result = (int)Math.Cos(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Tan" || operator_ == "tan" || operator_ == "TAN")
            {
                var Result = (int)Math.Tan(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Abs" || operator_ == "abs" || operator_ == "ABS")
            {
                var Result = (int)Math.Abs(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Acos" || operator_ == "acos" || operator_ == "ACOS")
            {
                var Result = (int)Math.Acos(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Asin" || operator_ == "asin" || operator_ == "ASIN")
            {
                var Result = (int)Math.Asin(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Atan" || operator_ == "atan" || operator_ == "ATAN")
            {
                var Result = (int)Math.Atan(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Atan2" || operator_ == "atan2" || operator_ == "ATAN2")
            {
                var Result = (int)Math.Atan2(num1, num2);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "MLog" || operator_ == "mlog" || operator_ == "MLOG")
            {
                var Result = (int)Math.Log(num1, num2);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Cosh" || operator_ == "cosh" || operator_ == "COSH" || operator_ == "CosH")
            {
                var Result = (int)Math.Cosh(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Sinh" || operator_ == "sinh" || operator_ == "SINH" || operator_ == "SinH")
            {
                var Result = (int)Math.Sinh(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Tanh" || operator_ == "tanh" || operator_ == "TANH" || operator_ == "TanH")
            {
                var Result = (int)Math.Tanh(num1);
                Global.mathAnswer = Result;
            }
            else if (operator_ == "Sqrt" || operator_ == "sqrt" || operator_ == "SQRT")
            {
                var Result = (int)Math.Sqrt(num1);
                Global.mathAnswer = Result;
            }
            else
            {
                Global.mathAnswer = 0;
            }
        }
        public void CustomFormKeyPressDetect(object sender, KeyEventArgs keyPressArgs_e, string keyWhenPressed_string, string methodToLoadAfterKeyPress)
        {
            //Write(keyWhenPressed_string);
            if (keyPressArgs_e.KeyCode == (Keys)Enum.Parse(typeof(Keys), keyWhenPressed_string, true))
            {
                Load_GameScripts(Global.project_dir + @"\" + methodToLoadAfterKeyPress + ".s14c", null,true);
            }
        }
    }
}