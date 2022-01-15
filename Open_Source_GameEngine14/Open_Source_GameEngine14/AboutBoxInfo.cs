using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Open_Source_GameEngine14
{
    partial class AboutBoxInfo : Form
    {
        public AboutBoxInfo()
        {
            InitializeComponent();
        }

        private void AboutBoxInfo_Load(object sender, EventArgs e)
        {
            string file = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\versionInfo.xml";
            XmlReader reader = XmlReader.Create(file);
            int? versionNumberMajor = null;
            int? versionNumberMinor = null;
            int? versionNumberPatch = null;
            while (reader.Read())
            {
                Console.WriteLine(reader.Name + " and value is " + reader.Value);
                if (reader.Name.ToString() == "Major")
                {
                    versionNumberMajor = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
                if (reader.Name.ToString() == "Minor")
                {
                    versionNumberMinor = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
                if (reader.Name.ToString() == "Patch")
                {
                    versionNumberPatch = Convert.ToInt32(reader.Value.Replace("val=" + '"'.ToString(), "").Replace('"'.ToString(), "").Trim());
                }
            }
            this.labelVersion.Text = $"V{versionNumberMajor}.{versionNumberMinor}.{versionNumberPatch}";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
