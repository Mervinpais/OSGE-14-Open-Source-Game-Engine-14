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
    public partial class MainWindow_ControlResizer : Form
    {
        private readonly Action LoadSceneGUIFromMainForm;
        public MainWindow_ControlResizer(Action LoadSceneGUIFromMainForm_Action)
        {
            InitializeComponent();
            LoadSceneGUIFromMainForm = LoadSceneGUIFromMainForm_Action;
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

        private string _project_path_;
        public string _project_path
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

        private void Ok_BTN_Click(object sender, EventArgs e)
        {
            object__.Size = new Size(previewBoxSize.Width, previewBoxSize.Height);
            Control ctrl = object__;
            Cursor = Cursors.Default;
            string[] lines = File.ReadAllLines(_project_path + @"\projectGUI.OPG14");
            string[] linesForFile = File.ReadAllLines(_project_path + @"\projectGUI.OPG14");
            int lineCount = 0;
            int removeLinePos = 0;
            foreach (string line in lines)
            {
                if (line.EndsWith($"{ctrl.Name})"))
                {
                    removeLinePos = lineCount;
                }
                lineCount++;
            }
            Console.WriteLine(String.Join(Environment.NewLine, linesForFile) + " is the original");
            List<string> list = new List<string>(linesForFile);
            //////////////////////////////////////////
            int objectXPos = ctrl.Location.X;
            int objectYPos = ctrl.Location.Y;
            int objectXSize = ctrl.Size.Width;
            int objectYSize = ctrl.Size.Height;
            string objectColor = ctrl.BackColor.ToString().Replace("Color [", "").Replace("]", "").Trim();
            string objectName = ctrl.Name.Trim();
            list.RemoveAt(removeLinePos);
            list.Add($"Create.Panel({objectXPos}, {objectYPos}, {objectXSize}, {objectYSize}, {objectColor}, {objectName})");
            linesForFile = list.ToArray();
            Console.WriteLine(String.Join(Environment.NewLine, linesForFile) + " is the result");
            File.WriteAllLines(_project_path + @"\projectGUI.OPG14", linesForFile);
            Console.WriteLine("Finished Editing File");
            wait(75);
            LoadSceneGUIFromMainForm();
            this.Close();
        }

        private Control object___;
        public Control object__
        {
            get 
            {
                return object___; 
            }
            set 
            {
                object___ = value;
            }
        }

        private int xPos_;
        public int xPos
        {
            get
            {
                return xPos_;
            }
            set
            {
                xPos_ = value;
            }
        }

        private int yPos_;
        public int yPos
        {
            get
            {
                return yPos_;
            }
            set
            {
                yPos_ = value;
            }
        }

        private void MainWindow_ControlResizer_Shown(object sender, EventArgs e)
        {
            //x_pos_textbox.Text
            wait(50);
            previewBoxSize.Size = new Size(xPos, yPos);
        }

        private void y_pos_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void x_pos_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void y_pos_textbox_TextChanged(object sender, EventArgs e)
        {
            wait(250);
            if (y_pos_textbox.Text != "")
            {
                previewBoxSize.Size = new Size(previewBoxSize.Width, Convert.ToInt32(y_pos_textbox.Text));
            }
            else
            {
                previewBoxSize.Size = new Size(previewBoxSize.Width, 1);
            }
        }

        private void x_pos_textbox_TextChanged(object sender, EventArgs e)
        {
            wait(250);
            if (x_pos_textbox.Text != "")
            {
               previewBoxSize.Size = new Size(Convert.ToInt32(x_pos_textbox.Text), previewBoxSize.Height);
            }
            else
            {
                previewBoxSize.Size = new Size(1, previewBoxSize.Height);
            }
        }

        bool isSupportedLanguage = false;
        private void MainWindow_ControlResizer_Paint(object sender, PaintEventArgs e)
        {
            char quotes = '"';
            string filepathForLanguages = Environment.CurrentDirectory + @"\LanguageSettings.json";
            //Console.WriteLine(filepathForLanguages);
            if (File.Exists(filepathForLanguages))
            {
                //Console.WriteLine("======= FOUND LANGUAGES SETTINGS =============");
                string[] lines = File.ReadAllLines(filepathForLanguages);
                foreach (string line in lines)
                {
                    if (line.StartsWith("  " + quotes + "Language" + quotes))
                    {
                        string theLine = line;
                        theLine = theLine.Replace($"{quotes}Language{quotes}: {quotes}", "");
                        theLine = theLine.Replace(quotes.ToString(), "");
                        theLine = theLine.Replace(" ", "");
                        //Console.WriteLine(theLine);
                        if (theLine == "English")
                        {
                            isSupportedLanguage = true;
                            previewTitleText.Text = "Preview";
                            Ok_BTN.Text = "Ok";
                        }
                        else if (theLine == "Hindi")
                        {
                            isSupportedLanguage = true;
                            previewTitleText.Text = "पूर्वावलोकन";
                            Ok_BTN.Text = "ठीक";

                        }
                        else if (theLine == "Arabic")
                        {
                            isSupportedLanguage = true;
                            previewTitleText.Text = "معاينة";
                            Ok_BTN.Text = "موافق";
                        }
                        else if (theLine == "Spanish")
                        {
                            isSupportedLanguage = true;
                            previewTitleText.Text = "Avance";
                            Ok_BTN.Text = "Ok";
                        }
                        else if (theLine == "Russian")
                        {
                            isSupportedLanguage = true;
                            previewTitleText.Text = "Предварительный просмотр";
                            Ok_BTN.Text = "В порядкеВ порядке";
                        }
                        else if (theLine != "English" && theLine != "Hindi" && theLine != "Arabic" && theLine != "Spanish" && theLine != "Russian")
                        {
                            if (isSupportedLanguage == true)
                            {
                                MessageBox.Show($"A Language Error Ocurred at\n line ??? in MainWindow_ControlResizer.cs\n The Error;\n language {theLine} doesnt have an if statement to tell what text should be displayed(in the language {theLine})");
                            }
                            isSupportedLanguage = false;
                            previewTitleText.Text = "Preview";
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
    }
}
