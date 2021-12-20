
namespace Open_Source_GameEngine14
{
    partial class MainWindow_ControlResizer
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
            this.label1 = new System.Windows.Forms.Label();
            this.x_pos_textbox = new System.Windows.Forms.TextBox();
            this.y_pos_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PreviewBox = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.previewBoxSize = new System.Windows.Forms.Panel();
            this.Ok_BTN = new System.Windows.Forms.Button();
            this.PreviewBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(40, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "X Size";
            // 
            // x_pos_textbox
            // 
            this.x_pos_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x_pos_textbox.Location = new System.Drawing.Point(129, 50);
            this.x_pos_textbox.Name = "x_pos_textbox";
            this.x_pos_textbox.Size = new System.Drawing.Size(100, 26);
            this.x_pos_textbox.TabIndex = 1;
            this.x_pos_textbox.TextChanged += new System.EventHandler(this.x_pos_textbox_TextChanged);
            this.x_pos_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.x_pos_textbox_KeyPress);
            // 
            // y_pos_textbox
            // 
            this.y_pos_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.y_pos_textbox.Location = new System.Drawing.Point(129, 109);
            this.y_pos_textbox.Name = "y_pos_textbox";
            this.y_pos_textbox.Size = new System.Drawing.Size(100, 26);
            this.y_pos_textbox.TabIndex = 2;
            this.y_pos_textbox.TextChanged += new System.EventHandler(this.y_pos_textbox_TextChanged);
            this.y_pos_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.y_pos_textbox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y Size";
            // 
            // PreviewBox
            // 
            this.PreviewBox.Controls.Add(this.previewBoxSize);
            this.PreviewBox.Location = new System.Drawing.Point(252, 44);
            this.PreviewBox.Name = "PreviewBox";
            this.PreviewBox.Size = new System.Drawing.Size(213, 163);
            this.PreviewBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Preview";
            // 
            // previewBoxSize
            // 
            this.previewBoxSize.BackColor = System.Drawing.SystemColors.Desktop;
            this.previewBoxSize.Location = new System.Drawing.Point(10, 6);
            this.previewBoxSize.Name = "previewBoxSize";
            this.previewBoxSize.Size = new System.Drawing.Size(96, 44);
            this.previewBoxSize.TabIndex = 0;
            // 
            // Ok_BTN
            // 
            this.Ok_BTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ok_BTN.Location = new System.Drawing.Point(48, 186);
            this.Ok_BTN.Name = "Ok_BTN";
            this.Ok_BTN.Size = new System.Drawing.Size(74, 38);
            this.Ok_BTN.TabIndex = 6;
            this.Ok_BTN.Text = "Ok";
            this.Ok_BTN.UseVisualStyleBackColor = true;
            this.Ok_BTN.Click += new System.EventHandler(this.Ok_BTN_Click);
            // 
            // MainWindow_ControlResizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 236);
            this.Controls.Add(this.Ok_BTN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PreviewBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.y_pos_textbox);
            this.Controls.Add(this.x_pos_textbox);
            this.Controls.Add(this.label1);
            this.Name = "MainWindow_ControlResizer";
            this.Text = "MainWindow_ControlResizer";
            this.Shown += new System.EventHandler(this.MainWindow_ControlResizer_Shown);
            this.PreviewBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox x_pos_textbox;
        private System.Windows.Forms.TextBox y_pos_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PreviewBox;
        private System.Windows.Forms.Panel previewBoxSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Ok_BTN;
    }
}