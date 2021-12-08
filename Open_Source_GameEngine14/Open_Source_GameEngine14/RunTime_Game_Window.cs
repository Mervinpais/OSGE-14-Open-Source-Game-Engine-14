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
            public static string project_dir = null;
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
            Load_GameScripts(Global.Project_GameScripts_File_Path, true);
        }

        public static void Write(string text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch
            {
                Console.WriteLine("An Error occured while sending text");
            }
        }

        public void Load_GameScripts(string path_file, bool isFilePath)
        {
            //string loadstuff = File.ReadAllText(Global.Project_GameScripts_File_Path);
            string[] lines = null;
            if (isFilePath == true)
            {
                lines = File.ReadAllLines(path_file);
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
                                    //wait(Conve0rt.ToInt32(panel_create_command) * 1000);
                                    string pathh = Main_Window.externalUseFor_project_path;
                                    DirectoryInfo directoryInfo = new DirectoryInfo(pathh);
                                    Console.WriteLine(pathh);
                                    foreach (var file in directoryInfo.GetFiles())
                                    {
                                        if (file.Name == panel_create_command + ".s14c")
                                        {
                                            try
                                            {
                                                Load_GameScripts(pathh + @"\" + panel_create_command + ".s14c", true);
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
                    if (line.StartsWith("if"))
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
                        int result = 0;
                        int result2 = 0;
                        bool if_Found = false;
                        bool end_Found = false;
                        int countsdfud = 0;
                        for (int i = 0; i < cooool.Length; i++)
                        {
                            if (cooool[i] == "if")
                            {
                                result = 0;
                                if_Found = true;
                                Write("if found at " + i);
                            }

                            if (cooool[i] == "end")
                            {
                                result2 = i;
                                end_Found = true;
                                Write("end found at " + i);
                            }

                            ///////////////
                            if (result < result2 && if_Found == true && end_Found == true)
                            {
                                Write("If statment Found :D");
                                line.Replace("if", "");
                                line.Replace("end", "");
                                Load_GameScripts(line, false);
                            }
                            else if (result > result2 || result == result2)
                            {
                                try
                                {
                                    //An Exception im tryig to fix
                                    Write("====IMPORTANT========");
                                    Write(result.ToString() + " & " + cooool[i]);
                                    Write("");
                                    Write(result.ToString() + " & " + countsdfud);
                                    Write(countsdfud.ToString());
                                    Write("");
                                    var newArray2 = cooool[result + 1];
                                    if (newArray2 == "end")
                                    {
                                        end_Found = true;
                                    }
                                    else
                                    {
                                        Write(newArray2 + " is inside the if statement");
                                    }

                                    string[] keywords = { "then", "or", "else" };
                                    if (if_Found == true && end_Found == false && !keywords.All(newArray2.Contains))
                                    {
                                        Write("ERROR - NO 'END' IN IF STATMENT FOUND");
                                    }

                                    if (if_Found == false && end_Found == true && !keywords.All(newArray2.Contains))
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

                    //if (line.StartsWith("if"))
                    //{
                    //    string theLine = line;
                    //    int lineIfWasFoundOn = Global.line_count;
                    //    string if_loadStuff = File.ReadAllText(Global.ProjectGUI_File_Path);
                    //    string[] if_lines = File.ReadAllLines(Global.ProjectGUI_File_Path);
                    //    if_lines = if_lines.SkipWhile(x => x != theLine).ToArray();
                    //    foreach (string if_line in if_lines)
                    //    {
                    //        if (if_line.StartsWith("end"))
                    //        {
                    //            if_lines = if_lines.SkipWhile(x => x != "end").ToArray();
                    //        }
                    //    }
                    //    Load_GameScripts(if_lines.ToString(), false);
                    //}
                    /*if (line.StartsWith("if"))
                    {
                        string panel_create_command = line.ToString();
                        string if_index_loc = File.ReadAllText(path_file);
                        panel_create_command = panel_create_command.Replace("if", "");

                        //Console.WriteLine("object with name found({0})", ctrl.Name);
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith(";"))
                        {
                            Console.WriteLine(panel_create_command);

                            try
                            {
                                Console.WriteLine("finished moving object");
                            }
                            catch
                            {
                                MessageBox.Show("An Error Occured while moving 's Y :(");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldn't Excute Set Y Command because of error");
                            MessageBox.Show("Couldn't Excute Set Y Command because of invaild Char(charactor) at the end of your command :(");
                        }
                        break;
                    }*/
                    if (line.Contains("Clone"))
                    {
                        string panel_create_command = line.ToString();
                        panel_create_command = panel_create_command.Replace("(", "");
                        panel_create_command = panel_create_command.Replace(")", "");
                        panel_create_command = panel_create_command.Replace("Clone", "");
                        if (!panel_create_command.EndsWith(" ") || !panel_create_command.EndsWith(",") || !panel_create_command.EndsWith("."))
                        {
                            Console.WriteLine(panel_create_command);

                            Console.WriteLine("waiting...");

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
                                Console.WriteLine("finished waiting");
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
                        panel_create_command = panel_create_command.Replace("wait", "");
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
                }
            }
            else
            {

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
                this.game_window.Controls.Clear();
                wait(1500);
                this.debug_info_text.Text = "Finished compiling game :D";
                LoadSceneGUI();
            }
            catch (Exception exceptionError)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox();
                customMessageBox.title_text = "AN EXCEPTION ERROR OCCURED WHILE LOADING SCENE GUI ╯︿╰";
                MessageBox.Show("AN EXCEPTION ERROR OCCURED WHILE LOADING SCENE GUI  ╯︿╰");
                this.debug_info_text.Text = "Exception while compiling :(";
                customMessageBox.title_text = exceptionError.Message;
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            Global.game_stopped = true;
            debug_info_text.ForeColor = SystemColors.ControlDark;
        }
    }
}