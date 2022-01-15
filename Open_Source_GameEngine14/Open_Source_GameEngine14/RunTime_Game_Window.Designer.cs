
namespace Open_Source_GameEngine14
{
    partial class RunTime_Game_Window
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
            this.game_window = new System.Windows.Forms.Panel();
            this.Start_btn = new System.Windows.Forms.Button();
            this.stop_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.debug_info_text = new System.Windows.Forms.Label();
            this.Game_Logger = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inputTextbox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // game_window
            // 
            this.game_window.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.game_window.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.game_window.Location = new System.Drawing.Point(3, 3);
            this.game_window.Name = "game_window";
            this.game_window.Size = new System.Drawing.Size(1202, 472);
            this.game_window.TabIndex = 0;
            // 
            // Start_btn
            // 
            this.Start_btn.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_btn.Location = new System.Drawing.Point(12, 13);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(95, 42);
            this.Start_btn.TabIndex = 1;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // stop_btn
            // 
            this.stop_btn.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_btn.Location = new System.Drawing.Point(113, 12);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(95, 43);
            this.stop_btn.TabIndex = 2;
            this.stop_btn.Text = "Stop ⛔";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.debug_info_text);
            this.panel1.Location = new System.Drawing.Point(826, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 44);
            this.panel1.TabIndex = 4;
            // 
            // debug_info_text
            // 
            this.debug_info_text.AutoSize = true;
            this.debug_info_text.Font = new System.Drawing.Font("Cascadia Mono", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug_info_text.ForeColor = System.Drawing.SystemColors.ControlText;
            this.debug_info_text.Location = new System.Drawing.Point(17, 13);
            this.debug_info_text.Name = "debug_info_text";
            this.debug_info_text.Size = new System.Drawing.Size(189, 20);
            this.debug_info_text.TabIndex = 0;
            this.debug_info_text.Text = "Compiling Simulation";
            // 
            // Game_Logger
            // 
            this.Game_Logger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Game_Logger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Game_Logger.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.Game_Logger.ForeColor = System.Drawing.Color.White;
            this.Game_Logger.Location = new System.Drawing.Point(0, 3);
            this.Game_Logger.Name = "Game_Logger";
            this.Game_Logger.ReadOnly = true;
            this.Game_Logger.Size = new System.Drawing.Size(1202, 261);
            this.Game_Logger.TabIndex = 5;
            this.Game_Logger.Text = "";
            this.Game_Logger.ZoomFactor = 1.5F;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.game_window);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inputTextbox);
            this.splitContainer1.Panel2.Controls.Add(this.Game_Logger);
            this.splitContainer1.Size = new System.Drawing.Size(1202, 730);
            this.splitContainer1.SplitterDistance = 474;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 6;
            // 
            // inputTextbox
            // 
            this.inputTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.inputTextbox.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextbox.ForeColor = System.Drawing.Color.Gray;
            this.inputTextbox.Location = new System.Drawing.Point(5, 204);
            this.inputTextbox.Multiline = true;
            this.inputTextbox.Name = "inputTextbox";
            this.inputTextbox.ReadOnly = true;
            this.inputTextbox.Size = new System.Drawing.Size(1190, 51);
            this.inputTextbox.TabIndex = 6;
            this.inputTextbox.Text = "Waiting for input request...";
            // 
            // RunTime_Game_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stop_btn);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "RunTime_Game_Window";
            this.Text = "RunTime_Game_Window";
            this.Shown += new System.EventHandler(this.RunTime_Game_Window_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RunTime_Game_Window_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel game_window;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label debug_info_text;
        private System.Windows.Forms.RichTextBox Game_Logger;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox inputTextbox;
    }
}