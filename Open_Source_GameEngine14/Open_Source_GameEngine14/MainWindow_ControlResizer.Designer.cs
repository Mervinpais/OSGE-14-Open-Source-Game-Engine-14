
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
            this.xPosTitleText = new System.Windows.Forms.Label();
            this.x_pos_textbox = new System.Windows.Forms.TextBox();
            this.y_pos_textbox = new System.Windows.Forms.TextBox();
            this.yPosTitleText = new System.Windows.Forms.Label();
            this.PreviewBox = new System.Windows.Forms.Panel();
            this.previewBoxSize = new System.Windows.Forms.Panel();
            this.previewTitleText = new System.Windows.Forms.Label();
            this.Ok_BTN = new System.Windows.Forms.Button();
            this.PreviewBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // xPosTitleText
            // 
            this.xPosTitleText.AutoSize = true;
            this.xPosTitleText.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.xPosTitleText.Location = new System.Drawing.Point(40, 44);
            this.xPosTitleText.Name = "xPosTitleText";
            this.xPosTitleText.Size = new System.Drawing.Size(83, 31);
            this.xPosTitleText.TabIndex = 0;
            this.xPosTitleText.Text = "X Size";
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
            // yPosTitleText
            // 
            this.yPosTitleText.AutoSize = true;
            this.yPosTitleText.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold);
            this.yPosTitleText.Location = new System.Drawing.Point(40, 103);
            this.yPosTitleText.Name = "yPosTitleText";
            this.yPosTitleText.Size = new System.Drawing.Size(82, 31);
            this.yPosTitleText.TabIndex = 3;
            this.yPosTitleText.Text = "Y Size";
            // 
            // PreviewBox
            // 
            this.PreviewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewBox.Controls.Add(this.previewBoxSize);
            this.PreviewBox.Location = new System.Drawing.Point(263, 44);
            this.PreviewBox.Name = "PreviewBox";
            this.PreviewBox.Size = new System.Drawing.Size(242, 163);
            this.PreviewBox.TabIndex = 4;
            // 
            // previewBoxSize
            // 
            this.previewBoxSize.BackColor = System.Drawing.SystemColors.Desktop;
            this.previewBoxSize.Location = new System.Drawing.Point(10, 6);
            this.previewBoxSize.Name = "previewBoxSize";
            this.previewBoxSize.Size = new System.Drawing.Size(96, 44);
            this.previewBoxSize.TabIndex = 0;
            // 
            // previewTitleText
            // 
            this.previewTitleText.AutoSize = true;
            this.previewTitleText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewTitleText.Location = new System.Drawing.Point(265, 19);
            this.previewTitleText.Name = "previewTitleText";
            this.previewTitleText.Size = new System.Drawing.Size(74, 22);
            this.previewTitleText.TabIndex = 5;
            this.previewTitleText.Text = "Preview";
            // 
            // Ok_BTN
            // 
            this.Ok_BTN.AutoSize = true;
            this.Ok_BTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ok_BTN.Location = new System.Drawing.Point(26, 182);
            this.Ok_BTN.Name = "Ok_BTN";
            this.Ok_BTN.Size = new System.Drawing.Size(96, 42);
            this.Ok_BTN.TabIndex = 6;
            this.Ok_BTN.Text = "Ok";
            this.Ok_BTN.UseVisualStyleBackColor = true;
            this.Ok_BTN.Click += new System.EventHandler(this.Ok_BTN_Click);
            // 
            // MainWindow_ControlResizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 236);
            this.Controls.Add(this.Ok_BTN);
            this.Controls.Add(this.previewTitleText);
            this.Controls.Add(this.PreviewBox);
            this.Controls.Add(this.yPosTitleText);
            this.Controls.Add(this.y_pos_textbox);
            this.Controls.Add(this.x_pos_textbox);
            this.Controls.Add(this.xPosTitleText);
            this.Name = "MainWindow_ControlResizer";
            this.Text = "MainWindow_ControlResizer";
            this.Shown += new System.EventHandler(this.MainWindow_ControlResizer_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_ControlResizer_Paint);
            this.PreviewBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xPosTitleText;
        private System.Windows.Forms.TextBox x_pos_textbox;
        private System.Windows.Forms.TextBox y_pos_textbox;
        private System.Windows.Forms.Label yPosTitleText;
        private System.Windows.Forms.Panel PreviewBox;
        private System.Windows.Forms.Panel previewBoxSize;
        private System.Windows.Forms.Label previewTitleText;
        private System.Windows.Forms.Button Ok_BTN;
    }
}