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
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private string title_text_;
        public string title_text
        {
            get
            {
                return label1.Text = title_text_;
            }
            set
            {
                title_text_ = value;
            }
        }

        private string message_text_;
        public string message_text
        {
            get
            {
                return label2.Text = message_text_;
            }
            set
            {
                message_text_ = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
