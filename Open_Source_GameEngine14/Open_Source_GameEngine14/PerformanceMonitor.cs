using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Open_Source_GameEngine14
{
    public partial class PerformanceMonitor : Form
    {
        public PerformanceMonitor()
        {
            InitializeComponent();
        }

        bool isClosing = false;
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

        private void PerformanceMonitor_Load(object sender, EventArgs e)
        {
            wait(500); //Just a small delay to let the window load
            Process p = System.Diagnostics.Process.GetCurrentProcess();
            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", p.ProcessName);
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", p.ProcessName);
            while (isClosing == false)
            {
                wait(1000);
                double ram = ramCounter.NextValue();
                double cpu = cpuCounter.NextValue();
                cpuUseText.Text = "CPU Usage : " + Convert.ToInt32(cpu).ToString() + "%";
                ramUseText.Text = "RAM Usage : " + Convert.ToInt32(ram / 1024 / 1024).ToString() + "%";
            }
        }

        private void PerformanceMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
        }
    }
}
