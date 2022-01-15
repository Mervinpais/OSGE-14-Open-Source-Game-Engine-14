using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_Source_GameEngine14
{
    public partial class HelpWithOSGE_Fourteen : Form
    {
        public HelpWithOSGE_Fourteen()
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

        private void CodingStuffHelpBTN_Click(object sender, EventArgs e)
        {
            this.helpGroupBox.Controls.Clear();
            Label helpTitle = new Label();
            helpTitle.Text = "Help With Programming";
            helpTitle.AutoSize = true;
            helpTitle.Location = new Point(15, 30);
            Label helpText = new Label();
            helpText.Text = "Here you can learn how to program stuff\n\n first here are the basic statements;\n-Log(Text)\n-Add(num1,num2)\n -Subtract(num1,num2) OR Minus(num1,num2)\n-Multiply(num1,num2)\n-Divide(num1,num2)\n\n these are the basic statements you can try out\n Check below for more info";
            helpText.AutoSize = true;
            helpText.Location = new Point(15, 60);
            LinkLabel helpLinkText = new LinkLabel();
            helpLinkText.Text = "https://mervinpais.github.io/OSGE-14-Open-Source-Game-Engine-14/";
            helpLinkText.AutoSize = true;
            helpLinkText.Location = new Point(20, 300);
            this.helpGroupBox.Controls.Add(helpTitle);
            this.helpGroupBox.Controls.Add(helpText);
            this.helpGroupBox.Controls.Add(helpLinkText);
        }

        private void TitleText_Click(object sender, EventArgs e)
        {
            while (TitleText.Location.Y < 40)
            {
                TitleText.Location = new Point(TitleText.Location.X, TitleText.Location.Y + 1);
                wait(10);
            }
            TitleText.Text = @"Click any button dude, im not a button";
            wait(1200);
            while (TitleText.Location.Y > 10)
            {
                TitleText.Location = new Point(TitleText.Location.X, TitleText.Location.Y - 1);
                wait(10);
            }
            TitleText.Text = "Choose The Help You need";
            wait(1000);
        }
    }
}
