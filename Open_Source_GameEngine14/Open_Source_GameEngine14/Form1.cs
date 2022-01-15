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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Hide();
        }

        class UnableToFix_or_ReAddFiles : Exception
        {
            public UnableToFix_or_ReAddFiles()
            {
                MessageBox.Show("ERROR; UNABLE TO RE-ADD FILES WITH FILE RESTORE, PLEASE PLEASE REINSTALL THE ENGINE\n OR MANUALLY RESTORE THE FILES YOURSELF :(");
            }
        }

        public class AutoClosingMessageBox //https://stackoverflow.com/questions/23692127/auto-close-message-box is the stackoverflow question from where i got the code (THe Answer)
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Create_a_New_Project _New_Project = new Create_a_New_Project();
            _New_Project.Show();

            bool fileVerify1 = false;
            bool fileVerify2 = false;
            bool fileVerify3 = false;
            //Tests
            if (File.Exists("RecentItemsList.json"))
            {
                Console.WriteLine("File Verify 1 complete");
                fileVerify1 = true;
            }
            else
            {
                Console.Beep();
                fileVerify1 = false;
            }

            if (File.Exists("LanguageSettings.json"))
            {
                Console.WriteLine("File Verify 2 complete");
                fileVerify2 = true;
            }
            else
            {
                Console.Beep();
                fileVerify2 = false;
            }

            if (File.Exists("ColorThemeSettings.json"))
            {
                Console.WriteLine("File Verify 3 complete");
                fileVerify3 = true;
            }
            else
            {
                Console.Beep();
                fileVerify3 = false;
            }

            if (fileVerify1 == true && fileVerify2 == true && fileVerify3 == true)
            {
                Console.WriteLine("All Files Needed are found :)");
            }
            else
            {
                Console.WriteLine("Not All Files Needed are found :(");
                Console.Beep();
                Console.Beep();

                char quotes = '"';
                if (MessageBox.Show("ALERT; The OSGE14 Game Engine has found missing files that are... well, Missing, Do you want" +
                    " to *TRY and add those files back \n\n *Complete File restore is not guaranteed.", "Uh oh", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\RecentItemsList.json", "{" + $"\n  {quotes}Recent Item 1{quotes}: null\n   {quotes}Recent Item 2{quotes}: null\n   {quotes}Recent Item 3{quotes}: null\n" + "}");
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\LanguageSettings.json", "{" + $"\n  {quotes}Recent Item 1{quotes}: {quotes}English{quotes}\n" + "}");
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\ColorThemeSettings.json", "{" + $"\n  {quotes}Theme{quotes}: {quotes}Dark{quotes}\n" + "}");
                    AutoClosingMessageBox.Show("Added Files, Please wait until this messagebox closes (DONT CLICK THE OK BUTTON)", "Re-Adding files: Part 2", 1000);
                    if (File.Exists("RecentItemsList.json"))
                    {
                        Console.WriteLine("File Verify 1 complete");
                        fileVerify1 = true;
                    }
                    else
                    {
                        fileVerify1 = false; Console.Beep();
                    }

                    if (File.Exists("LanguageSettings.json"))
                    {
                        Console.WriteLine("File Verify 2 complete");
                        fileVerify2 = true;
                    }
                    else
                    {
                        fileVerify2 = false; Console.Beep();
                    }

                    if (File.Exists("ColorThemeSettings.json"))
                    {
                        Console.WriteLine("File Verify 3 complete");
                        fileVerify3 = true;
                    }
                    else
                    {
                        fileVerify3 = false; Console.Beep();
                    }
                    if (fileVerify1 == true && fileVerify2 == true && fileVerify3 == true)
                    {
                        Console.WriteLine("All Files Needed and Files that were re-added are Found :)");
                    }
                    else
                    {
                        throw new UnableToFix_or_ReAddFiles();
                    }
                }
            }

        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            //base.OnVisibleChanged(e);
            this.Visible = false;
        }
    }
}
