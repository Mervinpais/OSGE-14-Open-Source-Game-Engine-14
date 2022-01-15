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

namespace Open_Source_GameEngine14
{
    public partial class ReNameOrNameObject : Form
    {
        public ReNameOrNameObject()
        {
            InitializeComponent();
        }

        private Main_Window mainForm = new Main_Window();
        public ReNameOrNameObject(Form callingForm)
        {
            mainForm = callingForm as Main_Window;
            InitializeComponent();
        }


        private int ReNamingOrNaming_;
        public int ReNamingOrNaming //1 = Name, 2 = Rename
        {
            get
            {
                return ReNamingOrNaming_;
            }
            set
            {
                ReNamingOrNaming_ = value;
            }
        }

        private Control Object__;
        public Control Object_
        {
            get
            {
                return Object__;
            }
            set
            {
                Object__ = value;
            }
        }

        private static string _project_path_;
        public static string _project_path
        {
            get
            {
                return _project_path_;
            }
            set
            {
                _project_path_ = value;
            }
        }

        class Global
        {
            public static string ProjectGUI_File_Path = _project_path + @"\projectGUI.OPG14";
            public static string Project_GameScripts_File_Path = Main_Window.externalUseFor_project_path + @"\Game_Scripts.s14c";
        }

        private void ReNameOrNameObject_Load(object sender, EventArgs e)
        {
            if (ReNamingOrNaming == 1)
            {
                this.Text = "Object Naming";
                TitleText.Text = "Name Your Object";
            }
            else if (ReNamingOrNaming == 2)
            {
                this.Text = "Object Renaming";
                TitleText.Text = "Rename Your Object";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Object__.Name);
            Object__.Name = textBox1.Text.Trim(); //Trimming is needed because you cant have spaces in control names :/
            foreach (Control ctrl in this.mainForm.SceneGUIObjects.Controls)
            {
                if (ctrl.Name == Object__.Name)
                {
                    this.mainForm.renamingControl.Name = Object__.Name;
                }
            }
            this.Close();
        }
    }
}
