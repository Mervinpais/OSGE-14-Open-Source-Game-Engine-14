
namespace Open_Source_GameEngine14
{
    partial class PerformanceMonitor
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
            this.cpuUseText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ramUseText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cpuUseText
            // 
            this.cpuUseText.AutoSize = true;
            this.cpuUseText.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuUseText.Location = new System.Drawing.Point(14, 106);
            this.cpuUseText.Name = "cpuUseText";
            this.cpuUseText.Size = new System.Drawing.Size(136, 21);
            this.cpuUseText.TabIndex = 0;
            this.cpuUseText.Text = "CPU Usage: NaN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Performance Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Mono Light", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 42);
            this.label3.TabIndex = 2;
            this.label3.Text = "This just shows performance details \r\nof the game engine, nothing fancy";
            // 
            // ramUseText
            // 
            this.ramUseText.AutoSize = true;
            this.ramUseText.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramUseText.Location = new System.Drawing.Point(14, 140);
            this.ramUseText.Name = "ramUseText";
            this.ramUseText.Size = new System.Drawing.Size(136, 21);
            this.ramUseText.TabIndex = 3;
            this.ramUseText.Text = "RAM Usage: NaN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Note: Results may not be correct sometimes";
            // 
            // PerformanceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 236);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ramUseText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cpuUseText);
            this.Name = "PerformanceMonitor";
            this.Text = "PerformanceMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerformanceMonitor_FormClosing);
            this.Shown += new System.EventHandler(this.PerformanceMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cpuUseText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ramUseText;
        private System.Windows.Forms.Label label1;
    }
}