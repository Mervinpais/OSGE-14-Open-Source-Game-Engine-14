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
    public partial class MainWindow_ControlResizer : Form
    {
        public MainWindow_ControlResizer()
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

        private void Ok_BTN_Click(object sender, EventArgs e)
        {
            object__.Size = new Size(previewBoxSize.Width, previewBoxSize.Height);
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
    }
}
