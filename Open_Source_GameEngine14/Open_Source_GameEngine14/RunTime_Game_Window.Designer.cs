
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // game_window
            // 
            this.game_window.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.game_window.Location = new System.Drawing.Point(21, 62);
            this.game_window.Name = "game_window";
            this.game_window.Size = new System.Drawing.Size(905, 505);
            this.game_window.TabIndex = 0;
            // 
            // Start_btn
            // 
            this.Start_btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_btn.Location = new System.Drawing.Point(21, 13);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(95, 43);
            this.Start_btn.TabIndex = 1;
            this.Start_btn.Text = "Start 🏳";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // stop_btn
            // 
            this.stop_btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_btn.Location = new System.Drawing.Point(122, 12);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(95, 43);
            this.stop_btn.TabIndex = 2;
            this.stop_btn.Text = "Stop ⛔";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.debug_info_text);
            this.panel1.Location = new System.Drawing.Point(562, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 44);
            this.panel1.TabIndex = 4;
            // 
            // debug_info_text
            // 
            this.debug_info_text.AutoSize = true;
            this.debug_info_text.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug_info_text.ForeColor = System.Drawing.SystemColors.ControlText;
            this.debug_info_text.Location = new System.Drawing.Point(17, 13);
            this.debug_info_text.Name = "debug_info_text";
            this.debug_info_text.Size = new System.Drawing.Size(171, 19);
            this.debug_info_text.TabIndex = 0;
            this.debug_info_text.Text = "Compiling Simulation";
            // 
            // RunTime_Game_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 579);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stop_btn);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.game_window);
            this.Name = "RunTime_Game_Window";
            this.Text = "RunTime_Game_Window";
            this.Shown += new System.EventHandler(this.RunTime_Game_Window_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel game_window;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label debug_info_text;
    }
}