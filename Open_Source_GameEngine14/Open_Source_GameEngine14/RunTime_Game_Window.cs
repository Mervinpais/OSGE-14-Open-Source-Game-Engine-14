using System;
using System.Collections;
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
using System.Text.RegularExpressions;

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
            public static string ProjectGUI_File_Path = Main_Window.externalUseFor_project_path + @"\projectGUI.OPG14";
            public static string Project_GameScripts_File_Path = Main_Window.externalUseFor_project_path + @"\Game_Scripts.s14c";
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
            public static string project_dir = Main_Window.externalUseFor_project_path;
            //if Statement stuff
            public static bool IfStatementConditionsAreMet = false;
        }

        public class ProjectPathError : Exception
        {
            public ProjectPathError(string message) : base($"The path '{message}' doesnt seem to be accessable to the program, please make sure that the path exists :(") { }
            public ProjectPathError() { }
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

        private string ProjectDirIncase_;
        public string ProjectDirIncase
        {
            get { return ProjectDirIncase_; }
            set { ProjectDirIncase_ = value; }
        }

        public void WriteToGameLogger(string text, string typeOfMessage)
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

        public void LoadSceneGUI()
        {
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
                        //Console.WriteLine(l);
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
                    this.game_window.Controls.Add(p); // nothing would show
                    /*Label id_for_control = new Label();
                    id_for_control.Name = "id";
                    id_for_control.Text = panel_create_command.Substring(panel_create_command.IndexOf('"'), panel_create_command.LastIndexOf('"'));
                    p.Controls.Add(id_for_control);*/
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
                        //Console.WriteLine(l);
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
                    this.game_window.Controls.Add(p); // nothing would show
                    messageBox.did_task_finish = "true";
                }
            }
            Load_GameScripts(Global.Project_GameScripts_File_Path, null,true);
        }

        public void Load_GameScripts(string path_file, string[] stringTextArray, bool isFilePath)
        {
            //string loadstuff = File.ReadAllText(Global.Project_GameScripts_File_Path);
            if (isFilePath == true && path_file != null && stringTextArray == null)
            {
                string[] lines = null;
                lines = File.ReadAllLines(path_file);
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

                            WriteToGameLogger($"Loading Script {mainScriptName}", "info");
                            Console.WriteLine("loading script {0}...", mainScriptName);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    //wait(Conve0rt.ToInt32(panel_create_command) * 1000);
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == mainScriptName + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + mainScriptName + ".s14c", null, true);
                                                line.Replace(line, "");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                            }
                                        }
                                    }
                                }
                                Console.WriteLine("finished loading script {0}", mainScriptName + ".s14c");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while loading script :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.StartsWith("string"))
                    {
                        string stringReplaced = line.Replace("string", "");
                        stringReplaced = stringReplaced.Replace(" ", "");
                        string fileName = null;
                        if (Global.project_dir != null)
                        {
                            fileName = Global.project_dir + @"\" + stringReplaced + ".json";
                        }
                        else
                        {
                            throw new ProjectPathError(Global.project_dir);
                        }
                        try
                        {
                            // Check if file already exists. If yes, delete it.     
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }

                            // Create a new file     
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                char qoutes = '"';
                                //sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                                sw.WriteLine("{");
                                sw.WriteLine(qoutes + "data" + qoutes + ": null");
                                sw.WriteLine("}");
                            }

                            // Write file contents on console.     
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
                        if (line.StartsWith($"{Path.GetFileName(file.Replace(".json", ""))}.edit"))
                        {
                            string stringReplaced = line.Replace("edit", "");
                            stringReplaced = stringReplaced.Replace("=", "");
                            stringReplaced = stringReplaced.Replace(" ", "");
                            string fileName = null;
                            char[] textLeArray = stringReplaced.ToCharArray();
                            int dotCharPos = 0;
                            if (Global.project_dir != null)
                            {
                                for (int i = 1; i < stringReplaced.Length; i++)
                                {
                                    if (textLeArray[i].Equals('.'))
                                    {
                                        dotCharPos = i;
                                        break;
                                    }
                                }
                                fileName = Global.project_dir + @"\" + stringReplaced.Substring(0, dotCharPos) + ".json";
                            }
                            else
                            {
                                throw new ProjectPathError(Global.project_dir);
                            }
                            try
                            {
                                string stringNewData = stringReplaced.Substring(dotCharPos, stringReplaced.Length - dotCharPos);
                                // Check if file already exists. If yes, delete it.     
                                if (File.Exists(fileName))
                                {
                                    File.Delete(fileName);
                                }

                                // Create a new file     
                                using (StreamWriter sw = File.CreateText(fileName))
                                {
                                    char qoutes = '"';
                                    //sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                                    Write("Editing String");
                                    sw.WriteLine("{");
                                    sw.WriteLine($"{qoutes} data {qoutes} : {stringNewData.Remove(0, 1)}");
                                    sw.WriteLine("}");
                                }

                                // Write file contents on console.     
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
                    }
                    if (line.StartsWith("if")) //Messy Code :/
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
                        for (int i = 0; i < cooool.Length; i++)//simple explaination for beginner C# coders, this whole section uses array's
                        {
                            if (cooool[i] == "if") //the cooool[i] means "For each word splited from space, word splited = i and cooool[i] means the word in the array" so in this case, it means, "word in array called cooool"
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
                            if (if_Line_Pos < then_Line_Pos && if_Found == true && end_Found == true)//if the line char position of "if" keyword is lesser than the "then" keyword, then this is an if statment
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
                            else if (if_Line_Pos > then_Line_Pos || if_Line_Pos == then_Line_Pos)//else if the "if" keyword is after "then" keyword, there is to try and handle the isssue
                            {
                                try
                                {
                                    //An Exception im tryig to fix
                                    Write("====IMPORTANT========");
                                    Write(if_Line_Pos.ToString() + " & " + cooool[i]);
                                    Write("");
                                    Write(if_Line_Pos.ToString() + " & " + countsdfud);
                                    Write(countsdfud.ToString());
                                    Write("");
                                    var newArray2 = cooool[if_Line_Pos + 1];
                                    if (newArray2 == "then")
                                    {
                                        end_Found = true;
                                    }
                                    else
                                    {
                                        Write(newArray2 + " is inside the if statement");
                                    }

                                    string[] keywords = { "then", "or", "else" };
                                    if (if_Found == true && end_Found == false)
                                    {
                                        Write("ERROR - NO 'THEN' IN IF STATMENT FOUND");
                                    }

                                    if (if_Found == false && end_Found == true)
                                    {
                                        Write("ERROR - NO 'IF' IN IF STATMENT FOUND");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Write("Operation FAILED, " + e.ToString());
                                }
                            }
                        }
                    }

                    if (line.Contains("Clone"))
                    {
                        string cloneCommand = line.ToString();
                        cloneCommand = cloneCommand.Replace("(", "");
                        cloneCommand = cloneCommand.Replace(")", "");
                        cloneCommand = cloneCommand.Replace("Clone", "");
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
                                            /*if (ctrl is Panel)
                                            {*/
                                            Control NewControl = new Control(ctrl, ctrl.Name + "_clone");
                                            NewControl.Location = new Point(NewControl.Location.X + 10, NewControl.Location.Y + 10);
                                            //}
                                        }
                                    }
                                }
                                Console.WriteLine("finished cloning");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("wait"))
                    {
                        string wait_Command = line.ToString();
                        wait_Command = wait_Command.Replace("(", "");
                        wait_Command = wait_Command.Replace(")", "");
                        wait_Command = wait_Command.Replace("wait", "");
                        if (!wait_Command.EndsWith(" ") || !wait_Command.EndsWith(",") || !wait_Command.EndsWith("."))
                        {
                            Console.WriteLine(wait_Command);

                            Console.WriteLine("waiting...");

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    wait(Convert.ToInt32(wait_Command) * 1000);
                                }
                                //Console.WriteLine("finished waiting");
                                line.Replace(line, "");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("OnkeyPress"))
                    {
                        string keyPressCommands = line.ToString();
                        keyPressCommands = keyPressCommands.Replace("(", "");
                        keyPressCommands = keyPressCommands.Replace(")", "");
                        keyPressCommands = keyPressCommands.Replace("OnkeyPress", "");
                        if (!keyPressCommands.EndsWith(" ") || !keyPressCommands.EndsWith(",") || !keyPressCommands.EndsWith("."))
                        {
                            Console.WriteLine(keyPressCommands);

                            WriteToGameLogger("Waiting For Keypress", "info");
                            Write("Waiting FOr UserKeypress");

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    //wait(Convert.ToInt32(panel_create_command) * 1000);
                                    
                                }
                                //Console.WriteLine("finished waiting");
                                line.Replace(line, "");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("Set color"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set color", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("set color to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string Color_get = bruhhh.Substring(1);

                                    Console.WriteLine("color was set to {0}", Color_get);
                                    Console.WriteLine("color set successfully");

                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            if (Color_get == " Red")
                                            {
                                                ctrl.BackColor = Color.Red;
                                            }
                                            else if (Color_get == " Green")
                                            {
                                                ctrl.BackColor = Color.Green;
                                            }
                                            else if (Color_get == " Blue")
                                            {
                                                ctrl.BackColor = Color.Blue;
                                            }
                                            else if (Color_get == " White")
                                            {
                                                ctrl.BackColor = Color.White;
                                            }
                                            else if (Color_get == " Black")
                                            {
                                                ctrl.BackColor = Color.Black;
                                            }
                                            else if (Color_get == " Cyan")
                                            {
                                                ctrl.BackColor = Color.Cyan;
                                            }
                                        }
                                        //Console.WriteLine("finished changing color object");
                                        line.Replace(line, "");
                                        WriteToGameLogger("Finished Change Color Of Object", "info");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while changing color {0} :(", ctrl.Name);
                                    }
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
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Move by", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

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
                                            wait(0001);
                                        }
                                        //Console.WriteLine("finished moving object");
                                        line.Replace(line, "");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Couldn't Excute Move to Command because of error");
                                    WriteToGameLogger("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(","err");
                                    MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                                }
                                break;
                            }
                        }
                    }
                    if (line.Contains(".Set Y"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set Y", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

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
                                            wait(0001);
                                        }
                                        line.Replace(line, "");
                                        //Console.WriteLine("finished moving object");
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
                    if (line.Contains("Set X"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set X", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

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
                                                ctrl.Location = new Point(X_pos_Move_to, ctrl.Location.Y);
                                            }
                                            wait(0050);
                                        }
                                        //Console.WriteLine("finished moving object's X");
                                        line.Replace(line, "");
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
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Move To", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

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
                                            wait(0001);
                                        }
                                        //Console.WriteLine("finished moving object");
                                        line.Replace(line, "");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
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
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Physics", "");
                        Console.WriteLine(panel_create_command);
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("iufheruifjfefhu {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            //Console.WriteLine(ctrl);
                            //MessageBox.Show(ctrl.Name);
                            MessageBox.Show("Alert!, Physics for the game engine is still being worked on, so please report any bugs to mervinpaismakeswindows14@gmail.com");
                            if (ctrl.Name == _object)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (panel_create_command.EndsWith("true"))
                                {
                                    Console.WriteLine("physics = true");
                                    while (ctrl.Location.Y < 425)
                                    {
                                        Console.WriteLine(ctrl.Location.Y + "is the Pos of " + ctrl.Name);
                                        ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 2);
                                        wait(0001);
                                    }
                                    //Console.WriteLine("finished loading physics");
                                    line.Replace(line, "");
                                }
                                else if (panel_create_command.EndsWith("false"))
                                {
                                    Console.WriteLine("physics = false");
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
                string[] lines = stringTextArray;
                int lineCounter = 1;
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    Global.line_count++;
                    if (line.EndsWith("();"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("(", "");
                        panel_create_command = panel_create_command.Replace(")", "");
                        panel_create_command = panel_create_command.Replace(";", "");
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                        {
                            Console.WriteLine(panel_create_command);

                            Console.WriteLine("loading script {0}...", panel_create_command);

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == panel_create_command + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + panel_create_command + ".s14c", null, true);
                                                line.Replace(line, "");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("An Error Occured while reading the file given for detecting scripts :(");
                                                Global.game_stopped = true;
                                                debug_info_text.ForeColor = SystemColors.ControlDark;
                                                Console.WriteLine("Stopped Simulation For Safety of scripts and files");
                                                debug_info_text.Text = "Stopped Simulation For Safety of scripts and files";
                                            }
                                        }
                                    }
                                }
                                Console.WriteLine("finished loading script {0}", panel_create_command + ".s14c");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while loading script :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.StartsWith("string"))
                    {
                        string stringReplaced = line.Replace("string", "");
                        stringReplaced = stringReplaced.Replace(" ", "");
                        string fileName = null;
                        if (Global.project_dir != null)
                        {
                            fileName = Global.project_dir + @"\" + stringReplaced + ".json";
                        }
                        else
                        {
                            throw new ProjectPathError(Global.project_dir);
                        }
                        try
                        {
                            // Check if file already exists. If yes, delete it.     
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }

                            // Create a new file     
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                char qoutes = '"';
                                sw.WriteLine("{");
                                sw.WriteLine(qoutes + "data" + qoutes + ": null");
                                sw.WriteLine("}");
                            }

                            // Write file contents on console.     
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
                        if (line.StartsWith($"{Path.GetFileName(file.Replace(".json", ""))}.edit"))
                        {
                            string stringReplaced = line.Replace("edit", "");
                            stringReplaced = stringReplaced.Replace("=", "");
                            stringReplaced = stringReplaced.Replace(" ", "");
                            string fileName = null;
                            char[] textLeArray = stringReplaced.ToCharArray();
                            int dotCharPos = 0;
                            if (Global.project_dir != null)
                            {
                                for (int i = 1; i < stringReplaced.Length; i++)
                                {
                                    if (textLeArray[i].Equals('.'))
                                    {
                                        dotCharPos = i;
                                        break;
                                    }
                                }
                                fileName = Global.project_dir + @"\" + stringReplaced.Substring(0, dotCharPos) + ".json";
                            }
                            else
                            {
                                throw new ProjectPathError(Global.project_dir);
                            }
                            try
                            {
                                string stringNewData = stringReplaced.Substring(dotCharPos, stringReplaced.Length - dotCharPos);
                                // Check if file already exists. If yes, delete it.     
                                if (File.Exists(fileName))
                                {
                                    File.Delete(fileName);
                                }

                                // Create a new file     
                                using (StreamWriter sw = File.CreateText(fileName))
                                {
                                    char qoutes = '"';
                                    Write("Editing String");
                                    sw.WriteLine("{");
                                    sw.WriteLine($"{qoutes} data {qoutes} : {stringNewData.Remove(0, 1)}");
                                    sw.WriteLine("}");
                                }

                                // Write file contents on console.     
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
                    }
                    if (line.StartsWith("if")) //Messy Code :/
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
                        for (int i = 0; i < cooool.Length; i++)//simple explaination for beginner C# coders, this whole section uses array's
                        {
                            if (cooool[i] == "if") //the cooool[i] means "For each word splited from space, word splited = i and cooool[i] means the word in the array" so in this case, it means, "word in array called cooool"
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

                            if (if_Line_Pos < then_Line_Pos && if_Found == true && end_Found == true)//if the line char position of "if" keyword is lesser than the "then" keyword, then this is an if statment
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
                            else if (if_Line_Pos > then_Line_Pos || if_Line_Pos == then_Line_Pos)//else if the "if" keyword is after "then" keyword, there is to try and handle the isssue
                            {
                                try
                                {
                                    //An Exception im trying to fix
                                    Write("====IMPORTANT========");
                                    Write(if_Line_Pos.ToString() + " & " + cooool[i]);
                                    Write("");
                                    Write(if_Line_Pos.ToString() + " & " + countsdfud);
                                    Write(countsdfud.ToString());
                                    Write("");
                                    var newArray2 = cooool[if_Line_Pos + 1];
                                    if (newArray2 == "then")
                                    {
                                        end_Found = true;
                                    }
                                    else
                                    {
                                        Write(newArray2 + " is inside the if statement");
                                    }

                                    string[] keywords = { "then", "or", "else" };
                                    if (if_Found == true && end_Found == false)
                                    {
                                        Write("ERROR - NO 'THEN' IN IF STATMENT FOUND");
                                    }

                                    if (if_Found == false && end_Found == true)
                                    {
                                        Write("ERROR - NO 'IF' IN IF STATMENT FOUND");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Write("Operation FAILED, " + e.ToString());
                                }
                            }
                        }
                    }

                    if (line.Contains("Clone"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("(", "");
                        panel_create_command = panel_create_command.Replace(")", "");
                        panel_create_command = panel_create_command.Replace("Clone", "");
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                        {
                            Console.WriteLine(panel_create_command);

                            Console.WriteLine("cloning...");

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    foreach (Control ctrl in game_window.Controls)
                                    {
                                        if (ctrl.Name == panel_create_command)
                                        {
                                            /*if (ctrl is Panel)
                                            {*/
                                            Control NewControl = new Control(ctrl, ctrl.Name + "_clone");
                                            NewControl.Location = new Point(NewControl.Location.X + 10, NewControl.Location.Y + 10);
                                            //}
                                        }
                                    }
                                }
                                Console.WriteLine("finished cloning");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("wait"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("(", "");
                        panel_create_command = panel_create_command.Replace(")", "");
                        panel_create_command = panel_create_command.Replace("wait", "");
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                        {
                            Console.WriteLine(panel_create_command);

                            Console.WriteLine("waiting...");

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    wait(Convert.ToInt32(panel_create_command) * 1000);
                                }
                                //Console.WriteLine("finished waiting");
                                line.Replace(line, "");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("OnkeyPress"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("(", "");
                        panel_create_command = panel_create_command.Replace(")", "");
                        panel_create_command = panel_create_command.Replace("OnkeyPress", "");
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                        {
                            Console.WriteLine(panel_create_command);

                            Console.WriteLine("Geting scripts...");

                            try
                            {
                                if (Global.game_stopped == false)
                                {
                                    //wait(Convert.ToInt32(panel_create_command) * 1000);

                                }
                                //Console.WriteLine("finished waiting");
                                line.Replace(line, "");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while waiting :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Move to Command because of error");
                            MessageBox.Show("Couldn't Excute Move to Command because of invaild Char(charactor) at the end of your command :(");
                        }
                    }
                    if (line.Contains("Set color"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set color", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("set color to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

                                    string Color_get = bruhhh.Substring(1);

                                    Console.WriteLine("color was set to {0}", Color_get);
                                    Console.WriteLine("color set successfully");

                                    try
                                    {
                                        if (Global.game_stopped == false)
                                        {
                                            if (Color_get == " Red")
                                            {
                                                ctrl.BackColor = Color.Red;
                                            }
                                            else if (Color_get == " Green")
                                            {
                                                ctrl.BackColor = Color.Green;
                                            }
                                            else if (Color_get == " Blue")
                                            {
                                                ctrl.BackColor = Color.Blue;
                                            }
                                            else if (Color_get == " White")
                                            {
                                                ctrl.BackColor = Color.White;
                                            }
                                            else if (Color_get == " Black")
                                            {
                                                ctrl.BackColor = Color.Black;
                                            }
                                            else if (Color_get == " Cyan")
                                            {
                                                ctrl.BackColor = Color.Cyan;
                                            }
                                        }
                                        //Console.WriteLine("finished changing color object");
                                        line.Replace(line, "");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while changing color {0} :(", ctrl.Name);
                                    }
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
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Move by", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

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
                                            wait(0001);
                                        }
                                        //Console.WriteLine("finished moving object");
                                        line.Replace(line, "");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
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
                    if (line.Contains(".Set Y"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set Y", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

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
                                            wait(0001);
                                        }
                                        line.Replace(line, "");
                                        //Console.WriteLine("finished moving object");
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
                    if (line.Contains("Set X"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Set X", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

                                    Console.WriteLine(bruhhh.Substring(1));

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
                                                ctrl.Location = new Point(X_pos_Move_to, ctrl.Location.Y);
                                            }
                                            wait(0050);
                                        }
                                        //Console.WriteLine("finished moving object's X");
                                        line.Replace(line, "");
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
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("=", "");
                        panel_create_command = panel_create_command.Replace("Move To", "");
                        string _object = null;
                        _object = panel_create_command.Substring(0, panel_create_command.IndexOf('.'));
                        Console.WriteLine("move to object's name is {0}", _object);
                        foreach (Control ctrl in game_window.Controls)
                        {
                            if (ctrl.Name == _object) //i had to indent each line so its easier to read the code your welcome :)
                            {
                                Console.WriteLine("object with name found({0})", ctrl.Name);
                                if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                                {
                                    Console.WriteLine(panel_create_command);

                                    string bruhhh = panel_create_command.Replace(_object + ".", "");

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
                                            wait(0001);
                                        }
                                        //Console.WriteLine("finished moving object");
                                        line.Replace(line, "");
                                    }
                                    catch
                                    {
                                        MessageBox.Show("An Error Occured while moving {0} :(", ctrl.Name);
                                    }
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
                    lineCounter++;
                }
            }
        }

        private void RunTime_Game_Window_Shown(object sender, EventArgs e)
        {
            this.debug_info_text.Text = "...";
            debug_info_text.ForeColor = SystemColors.ControlDark;
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Global.game_stopped = false;
            try
            {
                debug_info_text.ForeColor = SystemColors.ControlText;
                this.debug_info_text.Text = "Compiling Game";
                Game_Logger.Text = null;
                //WriteToGameLogger("GAMEENGINE - GAME STARTED", "info");
                this.game_window.Controls.Clear();
                wait(1500);
                this.debug_info_text.Text = "Finished Compiling Game :D";
                LoadSceneGUI();
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

        public void if_statement_Handle(string path, int line_where_if_is_on)//if statement handler was supposed to be the name but eh, whatever
        {
            Write("\n"); Write("\n");
            string[] file_lines = File.ReadAllLines(path);
            List<string> file_lines_list = new List<string>(file_lines);
            //Step 1: Remove the top part of the code that isnt neccesary for the if statement
            foreach (string text in file_lines_list)
            {
                Write(text);
            }
            Write(line_where_if_is_on.ToString() + "is the line where if is int");
            Write("\n");
            file_lines_list.RemoveRange(0, line_where_if_is_on); //Main part of removing
            foreach (string text in file_lines_list)
            {
                Write(text);
            }

            //Step 2: Remove the lower, unneccesary part of the code
            foreach (string text in file_lines_list)
            {
                Write(text);
            }
            List<string> lowerpartAfterIf = new List<string>();
            Write("\n"); Write("\n");
            Write(file_lines_list.Count.ToString() + "is the file_lines_list Count");
            Write("\n");
            Write(file_lines_list.IndexOf("end") + "is the index of end Count");
            int lineCounterr = 1;
            int end_keyword_is_on_line_number = 0;//since we dont know the line count "end" is on yet,i will just set it to zero
            foreach (string text in file_lines_list)
            {
                Write(text);
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
            Write("FInished Job 1");
            foreach (string text in file_lines_list)
            {
                Write(text + " is result");
            }
            Write("\n"); Write("\n"); Write("\n");
            ////////////////////////////////Now, The handling if statement part and what the "if" is itself////////////////////////////////
            List<string> theMainIfAbout = file_lines_list;
            //int lineCounterrr = 1;
            //bool foundThenKeyword = false;
            //foreach (string text in theMainIfAbout)
            //{
            //    Write(text);
            //    if (foundThenKeyword == true)
            //    {
            //        string eee = text;
            //        theMainIfAbout2.Remove(eee.ToString());
            //    }
            //    if (text.EndsWith("then"))
            //    {
            //        foundThenKeyword = true;
            //    }
            //    lineCounterrr++;
            //}
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
            var variableToFind = Regex.Replace(result.Split()[1], @"[^=0-9a-zA-Z\ ]+", "");//For the newbies to c#, basically we will remove all other words after a space and then we get the word and thats basically it
            Write(variableToFind  + " is the if statement contents");
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
                    Write(file + " is a file");
                    if (file.EndsWith($"{variableToFind.ToString().Substring(0, equalsCharPos)}.json"))
                    {
                        fileFound = true;
                        Write($"{variableToFind} is the if statement whole");
                        string settingValue = variableToFind.Substring(equalsCharPos, variableToFind.Length - equalsCharPos);
                        settingValue = settingValue.Replace("=", "");
                        Write(settingValue + " is the settings val");
                        string theVariable = variableToFind.Substring(0, equalsCharPos);
                        Write(theVariable + " is the variable");
                        string theOperator = variableToFind.Substring(equalsCharPos, equalsCharPos);
                        theOperator = Regex.Replace(variableToFind.Split()[0], @"[^=!*+/]+", "");
                        Write(theOperator + " is the operator");
                        if (theOperator == "=")//if the "if" statement is asking about "if x = y then" where x is the variable and y is the value
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
                                string[] theIfStatement = ifStatmentAndContents.ToArray();
                                theIfStatement = theIfStatement.Skip(1).ToArray();
                                theIfStatement = theIfStatement.Skip(theIfStatement.Length).ToArray();
                                Write(String.Join(Environment.NewLine, theIfStatement) + " | is the script thats gonna load");
                                Load_GameScripts(null, theIfStatement, false);
                                Load_GameScripts(null, lowerpartAfterIf.ToArray(), false);
                            }
                            else
                            {
                                //Do nothing
                                Global.IfStatementConditionsAreMet = true;
                                List<string> FullIfStatementPlusContents = new List<string>(ifStatmentAndContents);
                                FullIfStatementPlusContents.RemoveRange(1, FullIfStatementPlusContents.Count - 1);
                                string[] TheIfstatementArray = FullIfStatementPlusContents.ToArray();
                                string theFirstIFstatementPartOnly = String.Join(Environment.NewLine, TheIfstatementArray);//this is the best solution to convert an array to a string for it(only if the array has 1 item)
                                Write("if x = y then statement didnt match, continuing reading game script");
                                WriteToGameLogger($"The if statement '{theFirstIFstatementPartOnly}' has been skiped due to its condition(s) not met, the Condition(s) is/are '{theVariable}'", "info");
                                Write(String.Join(Environment.NewLine, lowerpartAfterIf.ToArray()) + " | is the script thats gonna load");
                                Load_GameScripts(null, lowerpartAfterIf.ToArray(), false);
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
                    WriteToGameLogger("Uh oh, the game engine is now at an unsafe state, the engine will stop the project/simulation to prevent it to cause more errors", "err");
                    MessageBox.Show("Uh oh, the game engine is now at an unsafe state, the engine will stop the project/simulation to prevent it to cause more errors");
                    this.Close();
                }
            }
            else if (Directory.GetFiles(Global.project_dir, "*.json").Length == 0)
            {
                MessageBox.Show("User Code Defined Error;\n An Error Occured with Your code, This Error happened BECAUSE;\n\n There are no variables that can be found in your project :/");
            }
        }
    }
}