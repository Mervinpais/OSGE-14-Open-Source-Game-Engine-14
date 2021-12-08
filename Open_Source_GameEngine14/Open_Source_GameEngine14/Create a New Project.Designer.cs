
namespace Open_Source_GameEngine14
{
    partial class Create_a_New_Project
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.RecentItem1BTN = new System.Windows.Forms.LinkLabel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.CloseWindowBTN = new System.Windows.Forms.Button();
            this.MaximiseWindowBTN = new System.Windows.Forms.Button();
            this.MinimiseWindowBTN = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 235);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(17, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 160);
            this.label1.TabIndex = 3;
            this.label1.Text = "Create a new project\r\n\r\n\r\n\r\nor\r\n\r\n\r\nOpen a Project";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(51, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "Demo Platformer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RecentItem1BTN
            // 
            this.RecentItem1BTN.AutoSize = true;
            this.RecentItem1BTN.Location = new System.Drawing.Point(51, 126);
            this.RecentItem1BTN.Name = "RecentItem1BTN";
            this.RecentItem1BTN.Size = new System.Drawing.Size(86, 13);
            this.RecentItem1BTN.TabIndex = 5;
            this.RecentItem1BTN.TabStop = true;
            this.RecentItem1BTN.Text = "<Recent_Item1>";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TopPanel.Controls.Add(this.MinimiseWindowBTN);
            this.TopPanel.Controls.Add(this.MaximiseWindowBTN);
            this.TopPanel.Controls.Add(this.CloseWindowBTN);
            this.TopPanel.Location = new System.Drawing.Point(-1, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(525, 37);
            this.TopPanel.TabIndex = 6;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // CloseWindowBTN
            // 
            this.CloseWindowBTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseWindowBTN.Location = new System.Drawing.Point(488, 5);
            this.CloseWindowBTN.Name = "CloseWindowBTN";
            this.CloseWindowBTN.Size = new System.Drawing.Size(34, 29);
            this.CloseWindowBTN.TabIndex = 0;
            this.CloseWindowBTN.Text = "X";
            this.CloseWindowBTN.UseVisualStyleBackColor = true;
            this.CloseWindowBTN.Click += new System.EventHandler(this.CloseWindowBTN_Click);
            // 
            // MaximiseWindowBTN
            // 
            this.MaximiseWindowBTN.Enabled = false;
            this.MaximiseWindowBTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximiseWindowBTN.Location = new System.Drawing.Point(448, 5);
            this.MaximiseWindowBTN.Name = "MaximiseWindowBTN";
            this.MaximiseWindowBTN.Size = new System.Drawing.Size(34, 29);
            this.MaximiseWindowBTN.TabIndex = 1;
            this.MaximiseWindowBTN.Text = "❏";
            this.MaximiseWindowBTN.UseVisualStyleBackColor = true;
            this.MaximiseWindowBTN.Click += new System.EventHandler(this.MaximiseWindowBTN_Click);
            // 
            // MinimiseWindowBTN
            // 
            this.MinimiseWindowBTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimiseWindowBTN.Location = new System.Drawing.Point(408, 5);
            this.MinimiseWindowBTN.Name = "MinimiseWindowBTN";
            this.MinimiseWindowBTN.Size = new System.Drawing.Size(34, 29);
            this.MinimiseWindowBTN.TabIndex = 2;
            this.MinimiseWindowBTN.Text = "-";
            this.MinimiseWindowBTN.UseVisualStyleBackColor = true;
            this.MinimiseWindowBTN.Click += new System.EventHandler(this.MinimiseWindowBTN_Click);
            // 
            // Create_a_New_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 277);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.RecentItem1BTN);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Create_a_New_Project";
            this.Text = "Create_a_New_Project";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Create_a_New_Project_FormClosed);
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel RecentItem1BTN;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button CloseWindowBTN;
        private System.Windows.Forms.Button MaximiseWindowBTN;
        private System.Windows.Forms.Button MinimiseWindowBTN;
    }
}