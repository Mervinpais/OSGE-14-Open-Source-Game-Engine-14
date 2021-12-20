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
    public partial class CustomProgressMessageBox : Form
    {
        public CustomProgressMessageBox()
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

        private string Message_Text_label_;
        public string Message_Text_label
        {
            get
            {
                return label1.Text = Message_Text_label_;
            }
            set
            {
                Message_Text_label_ = value;
            }
        }

        private string other_Message_Text_label_;
        public string other_Message_Text_label
        {
            get
            {
                return label2.Text = other_Message_Text_label_;
            }
            set
            {
                other_Message_Text_label_ = value;
            }
        }

        private string did_task_finish_;
        public string did_task_finish
        {
            get
            {
                this.Close();
                return did_task_finish_;
            }
            set
            {
                did_task_finish_ = value;
            }
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Style = ProgressBarStyle.Marquee;

            while (did_task_finish_ == "true")
            {
                progressBar1.Increment(1);
            }
            wait(1000);
            this.Close();
        }
    }
}
