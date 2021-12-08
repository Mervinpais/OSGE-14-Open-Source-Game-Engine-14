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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Create_a_New_Project _New_Project = new Create_a_New_Project();
            _New_Project.Show();
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            //base.OnVisibleChanged(e);
            this.Visible = false;
        }
    }
}
