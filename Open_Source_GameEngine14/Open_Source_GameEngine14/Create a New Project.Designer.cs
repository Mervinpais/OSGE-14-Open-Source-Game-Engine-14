
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
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browserProjectsBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.createNew_DemoPlatformer = new System.Windows.Forms.Button();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.MinimiseWindowBTN = new System.Windows.Forms.Button();
            this.MaximiseWindowBTN = new System.Windows.Forms.Button();
            this.CloseWindowBTN = new System.Windows.Forms.Button();
            this.EmptyThreeDCreateProject = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HelpBTN = new System.Windows.Forms.Button();
            this.updateCheckerPanel = new System.Windows.Forms.Panel();
            this.versionPanel_Info = new System.Windows.Forms.Label();
            this.notification_UpdateBTN = new System.Windows.Forms.Button();
            this.updatePanel_Title = new System.Windows.Forms.Label();
            this.versionNumber = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.updateCheckerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Gray;
            this.textBox1.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(51, 199);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 30);
            this.textBox1.TabIndex = 0;
            // 
            // browserProjectsBTN
            // 
            this.browserProjectsBTN.BackColor = System.Drawing.Color.White;
            this.browserProjectsBTN.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browserProjectsBTN.ForeColor = System.Drawing.Color.Black;
            this.browserProjectsBTN.Location = new System.Drawing.Point(343, 199);
            this.browserProjectsBTN.Name = "browserProjectsBTN";
            this.browserProjectsBTN.Size = new System.Drawing.Size(98, 30);
            this.browserProjectsBTN.TabIndex = 2;
            this.browserProjectsBTN.Text = "Browse";
            this.browserProjectsBTN.UseVisualStyleBackColor = false;
            this.browserProjectsBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Create a new project";
            // 
            // createNew_DemoPlatformer
            // 
            this.createNew_DemoPlatformer.BackColor = System.Drawing.Color.White;
            this.createNew_DemoPlatformer.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNew_DemoPlatformer.ForeColor = System.Drawing.Color.Black;
            this.createNew_DemoPlatformer.Location = new System.Drawing.Point(50, 87);
            this.createNew_DemoPlatformer.Name = "createNew_DemoPlatformer";
            this.createNew_DemoPlatformer.Size = new System.Drawing.Size(170, 37);
            this.createNew_DemoPlatformer.TabIndex = 4;
            this.createNew_DemoPlatformer.Text = "Demo Platformer";
            this.createNew_DemoPlatformer.UseVisualStyleBackColor = false;
            this.createNew_DemoPlatformer.Click += new System.EventHandler(this.button2_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TopPanel.Controls.Add(this.label3);
            this.TopPanel.Controls.Add(this.MinimiseWindowBTN);
            this.TopPanel.Controls.Add(this.MaximiseWindowBTN);
            this.TopPanel.Controls.Add(this.CloseWindowBTN);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(524, 40);
            this.TopPanel.TabIndex = 6;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Create or Open a Project";
            // 
            // MinimiseWindowBTN
            // 
            this.MinimiseWindowBTN.BackColor = System.Drawing.Color.White;
            this.MinimiseWindowBTN.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimiseWindowBTN.Location = new System.Drawing.Point(388, 5);
            this.MinimiseWindowBTN.Name = "MinimiseWindowBTN";
            this.MinimiseWindowBTN.Size = new System.Drawing.Size(35, 29);
            this.MinimiseWindowBTN.TabIndex = 2;
            this.MinimiseWindowBTN.Text = "-";
            this.MinimiseWindowBTN.UseVisualStyleBackColor = false;
            this.MinimiseWindowBTN.Click += new System.EventHandler(this.MinimiseWindowBTN_Click);
            // 
            // MaximiseWindowBTN
            // 
            this.MaximiseWindowBTN.BackColor = System.Drawing.Color.White;
            this.MaximiseWindowBTN.Enabled = false;
            this.MaximiseWindowBTN.Font = new System.Drawing.Font("Cascadia Mono", 11F, System.Drawing.FontStyle.Bold);
            this.MaximiseWindowBTN.Location = new System.Drawing.Point(423, 5);
            this.MaximiseWindowBTN.Name = "MaximiseWindowBTN";
            this.MaximiseWindowBTN.Size = new System.Drawing.Size(36, 29);
            this.MaximiseWindowBTN.TabIndex = 1;
            this.MaximiseWindowBTN.Text = "❏";
            this.MaximiseWindowBTN.UseVisualStyleBackColor = false;
            this.MaximiseWindowBTN.Click += new System.EventHandler(this.MaximiseWindowBTN_Click);
            // 
            // CloseWindowBTN
            // 
            this.CloseWindowBTN.BackColor = System.Drawing.Color.White;
            this.CloseWindowBTN.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseWindowBTN.Location = new System.Drawing.Point(459, 5);
            this.CloseWindowBTN.Name = "CloseWindowBTN";
            this.CloseWindowBTN.Size = new System.Drawing.Size(63, 29);
            this.CloseWindowBTN.TabIndex = 0;
            this.CloseWindowBTN.Text = "X";
            this.CloseWindowBTN.UseVisualStyleBackColor = false;
            this.CloseWindowBTN.Click += new System.EventHandler(this.CloseWindowBTN_Click);
            this.CloseWindowBTN.MouseEnter += new System.EventHandler(this.CloseWindowBTN_MouseEnter);
            this.CloseWindowBTN.MouseLeave += new System.EventHandler(this.CloseWindowBTN_MouseLeave);
            // 
            // EmptyThreeDCreateProject
            // 
            this.EmptyThreeDCreateProject.BackColor = System.Drawing.Color.White;
            this.EmptyThreeDCreateProject.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmptyThreeDCreateProject.ForeColor = System.Drawing.Color.Black;
            this.EmptyThreeDCreateProject.Location = new System.Drawing.Point(364, 87);
            this.EmptyThreeDCreateProject.Name = "EmptyThreeDCreateProject";
            this.EmptyThreeDCreateProject.Size = new System.Drawing.Size(151, 49);
            this.EmptyThreeDCreateProject.TabIndex = 7;
            this.EmptyThreeDCreateProject.Text = "3D Empty (Not Working)";
            this.EmptyThreeDCreateProject.UseVisualStyleBackColor = false;
            this.EmptyThreeDCreateProject.Click += new System.EventHandler(this.EmptyThreeDCreateProject_Click);
            this.EmptyThreeDCreateProject.MouseHover += new System.EventHandler(this.EmptyThreeDCreateProject_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "Open a Project";
            // 
            // HelpBTN
            // 
            this.HelpBTN.BackColor = System.Drawing.Color.White;
            this.HelpBTN.Font = new System.Drawing.Font("Cascadia Mono", 17F, System.Drawing.FontStyle.Bold);
            this.HelpBTN.Location = new System.Drawing.Point(475, 299);
            this.HelpBTN.Name = "HelpBTN";
            this.HelpBTN.Size = new System.Drawing.Size(47, 41);
            this.HelpBTN.TabIndex = 9;
            this.HelpBTN.Text = "🔁";
            this.HelpBTN.UseVisualStyleBackColor = false;
            this.HelpBTN.Click += new System.EventHandler(this.HelpBTN_Click);
            // 
            // updateCheckerPanel
            // 
            this.updateCheckerPanel.BackColor = System.Drawing.Color.FloralWhite;
            this.updateCheckerPanel.Controls.Add(this.versionPanel_Info);
            this.updateCheckerPanel.Controls.Add(this.notification_UpdateBTN);
            this.updateCheckerPanel.Controls.Add(this.updatePanel_Title);
            this.updateCheckerPanel.ForeColor = System.Drawing.Color.Black;
            this.updateCheckerPanel.Location = new System.Drawing.Point(264, 210);
            this.updateCheckerPanel.Name = "updateCheckerPanel";
            this.updateCheckerPanel.Size = new System.Drawing.Size(256, 88);
            this.updateCheckerPanel.TabIndex = 10;
            this.updateCheckerPanel.Visible = false;
            // 
            // versionPanel_Info
            // 
            this.versionPanel_Info.AutoSize = true;
            this.versionPanel_Info.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 8.5F, System.Drawing.FontStyle.Bold);
            this.versionPanel_Info.Location = new System.Drawing.Point(4, 29);
            this.versionPanel_Info.Name = "versionPanel_Info";
            this.versionPanel_Info.Size = new System.Drawing.Size(113, 32);
            this.versionPanel_Info.TabIndex = 2;
            this.versionPanel_Info.Text = "Latest Version:\r\nYour Version:";
            // 
            // notification_UpdateBTN
            // 
            this.notification_UpdateBTN.Font = new System.Drawing.Font("Cascadia Mono SemiLight", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notification_UpdateBTN.Location = new System.Drawing.Point(169, 55);
            this.notification_UpdateBTN.Name = "notification_UpdateBTN";
            this.notification_UpdateBTN.Size = new System.Drawing.Size(81, 28);
            this.notification_UpdateBTN.TabIndex = 1;
            this.notification_UpdateBTN.Text = "Update";
            this.notification_UpdateBTN.UseVisualStyleBackColor = true;
            this.notification_UpdateBTN.Click += new System.EventHandler(this.notification_UpdateBTN_Click);
            // 
            // updatePanel_Title
            // 
            this.updatePanel_Title.AutoSize = true;
            this.updatePanel_Title.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatePanel_Title.Location = new System.Drawing.Point(3, 9);
            this.updatePanel_Title.Name = "updatePanel_Title";
            this.updatePanel_Title.Size = new System.Drawing.Size(224, 17);
            this.updatePanel_Title.TabIndex = 0;
            this.updatePanel_Title.Text = "You Have The latest Updates";
            // 
            // versionNumber
            // 
            this.versionNumber.AutoSize = true;
            this.versionNumber.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.versionNumber.ForeColor = System.Drawing.Color.White;
            this.versionNumber.Location = new System.Drawing.Point(12, 319);
            this.versionNumber.Name = "versionNumber";
            this.versionNumber.Size = new System.Drawing.Size(64, 21);
            this.versionNumber.TabIndex = 3;
            this.versionNumber.Text = "v0.0.7";
            // 
            // Create_a_New_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(524, 343);
            this.Controls.Add(this.versionNumber);
            this.Controls.Add(this.updateCheckerPanel);
            this.Controls.Add(this.HelpBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmptyThreeDCreateProject);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.createNew_DemoPlatformer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browserProjectsBTN);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Create_a_New_Project";
            this.Text = "Create_a_New_Project";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Create_a_New_Project_FormClosed);
            this.Shown += new System.EventHandler(this.Create_a_New_Project_Shown);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.updateCheckerPanel.ResumeLayout(false);
            this.updateCheckerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button browserProjectsBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createNew_DemoPlatformer;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button CloseWindowBTN;
        private System.Windows.Forms.Button MaximiseWindowBTN;
        private System.Windows.Forms.Button MinimiseWindowBTN;
        private System.Windows.Forms.Button EmptyThreeDCreateProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button HelpBTN;
        private System.Windows.Forms.Panel updateCheckerPanel;
        private System.Windows.Forms.Label updatePanel_Title;
        private System.Windows.Forms.Button notification_UpdateBTN;
        private System.Windows.Forms.Label versionPanel_Info;
        private System.Windows.Forms.Label versionNumber;
    }
}