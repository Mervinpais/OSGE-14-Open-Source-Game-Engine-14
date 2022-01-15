
namespace Open_Source_GameEngine14
{
    partial class HelpWithOSGE_Fourteen
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
            this.helpGroupBox = new System.Windows.Forms.GroupBox();
            this.CodingStuffHelpBTN = new System.Windows.Forms.Button();
            this.usingTheEngineHelpBTN = new System.Windows.Forms.Button();
            this.TitleText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // helpGroupBox
            // 
            this.helpGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpGroupBox.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpGroupBox.Location = new System.Drawing.Point(12, 145);
            this.helpGroupBox.Name = "helpGroupBox";
            this.helpGroupBox.Size = new System.Drawing.Size(818, 407);
            this.helpGroupBox.TabIndex = 0;
            this.helpGroupBox.TabStop = false;
            this.helpGroupBox.Text = "<Group Name>";
            // 
            // CodingStuffHelpBTN
            // 
            this.CodingStuffHelpBTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodingStuffHelpBTN.Location = new System.Drawing.Point(32, 77);
            this.CodingStuffHelpBTN.Name = "CodingStuffHelpBTN";
            this.CodingStuffHelpBTN.Size = new System.Drawing.Size(186, 48);
            this.CodingStuffHelpBTN.TabIndex = 1;
            this.CodingStuffHelpBTN.Text = "Coding and Programing Stuff";
            this.CodingStuffHelpBTN.UseVisualStyleBackColor = true;
            this.CodingStuffHelpBTN.Click += new System.EventHandler(this.CodingStuffHelpBTN_Click);
            // 
            // usingTheEngineHelpBTN
            // 
            this.usingTheEngineHelpBTN.Enabled = false;
            this.usingTheEngineHelpBTN.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usingTheEngineHelpBTN.Location = new System.Drawing.Point(582, 77);
            this.usingTheEngineHelpBTN.Name = "usingTheEngineHelpBTN";
            this.usingTheEngineHelpBTN.Size = new System.Drawing.Size(186, 48);
            this.usingTheEngineHelpBTN.TabIndex = 2;
            this.usingTheEngineHelpBTN.Text = "Navigating/Using The Engine";
            this.usingTheEngineHelpBTN.UseVisualStyleBackColor = true;
            // 
            // TitleText
            // 
            this.TitleText.AutoSize = true;
            this.TitleText.Font = new System.Drawing.Font("Segoe UI Variable Text", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleText.Location = new System.Drawing.Point(240, 25);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(319, 32);
            this.TitleText.TabIndex = 1;
            this.TitleText.Text = "Choose The Help You need";
            this.TitleText.Click += new System.EventHandler(this.TitleText_Click);
            // 
            // HelpWithOSGE_Fourteen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 564);
            this.Controls.Add(this.TitleText);
            this.Controls.Add(this.usingTheEngineHelpBTN);
            this.Controls.Add(this.CodingStuffHelpBTN);
            this.Controls.Add(this.helpGroupBox);
            this.Name = "HelpWithOSGE_Fourteen";
            this.Text = "HelpWithOSGE_Fourteen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox helpGroupBox;
        private System.Windows.Forms.Button CodingStuffHelpBTN;
        private System.Windows.Forms.Button usingTheEngineHelpBTN;
        private System.Windows.Forms.Label TitleText;
    }
}