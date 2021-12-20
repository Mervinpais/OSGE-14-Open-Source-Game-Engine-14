
namespace Open_Source_GameEngine14
{
    partial class GameEngineSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameEngineSettings));
            this.settingsTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LanguageSettingsTitle = new System.Windows.Forms.Label();
            this.LanguageSettingsDescription = new System.Windows.Forms.Label();
            this.ColorModeTitle = new System.Windows.Forms.Label();
            this.DarkModeOptionBTN = new System.Windows.Forms.RadioButton();
            this.LightModeOptionBTN = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsTitle
            // 
            this.settingsTitle.AutoSize = true;
            this.settingsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsTitle.Location = new System.Drawing.Point(302, 8);
            this.settingsTitle.Name = "settingsTitle";
            this.settingsTitle.Size = new System.Drawing.Size(90, 26);
            this.settingsTitle.TabIndex = 0;
            this.settingsTitle.Text = "Settings";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(433, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 262);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // LanguageSettingsTitle
            // 
            this.LanguageSettingsTitle.AutoSize = true;
            this.LanguageSettingsTitle.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LanguageSettingsTitle.Location = new System.Drawing.Point(29, 60);
            this.LanguageSettingsTitle.Name = "LanguageSettingsTitle";
            this.LanguageSettingsTitle.Size = new System.Drawing.Size(124, 27);
            this.LanguageSettingsTitle.TabIndex = 3;
            this.LanguageSettingsTitle.Text = "Language";
            // 
            // LanguageSettingsDescription
            // 
            this.LanguageSettingsDescription.AutoSize = true;
            this.LanguageSettingsDescription.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LanguageSettingsDescription.Location = new System.Drawing.Point(30, 98);
            this.LanguageSettingsDescription.Name = "LanguageSettingsDescription";
            this.LanguageSettingsDescription.Size = new System.Drawing.Size(516, 21);
            this.LanguageSettingsDescription.TabIndex = 4;
            this.LanguageSettingsDescription.Text = "Go to Help > Language and choose your language";
            // 
            // ColorModeTitle
            // 
            this.ColorModeTitle.AutoSize = true;
            this.ColorModeTitle.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorModeTitle.Location = new System.Drawing.Point(29, 142);
            this.ColorModeTitle.Name = "ColorModeTitle";
            this.ColorModeTitle.Size = new System.Drawing.Size(152, 27);
            this.ColorModeTitle.TabIndex = 5;
            this.ColorModeTitle.Text = "Color Mode";
            // 
            // DarkModeOptionBTN
            // 
            this.DarkModeOptionBTN.AutoSize = true;
            this.DarkModeOptionBTN.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DarkModeOptionBTN.Location = new System.Drawing.Point(34, 191);
            this.DarkModeOptionBTN.Name = "DarkModeOptionBTN";
            this.DarkModeOptionBTN.Size = new System.Drawing.Size(90, 20);
            this.DarkModeOptionBTN.TabIndex = 6;
            this.DarkModeOptionBTN.TabStop = true;
            this.DarkModeOptionBTN.Text = "DarkMode";
            this.DarkModeOptionBTN.UseVisualStyleBackColor = true;
            // 
            // LightModeOptionBTN
            // 
            this.LightModeOptionBTN.AutoSize = true;
            this.LightModeOptionBTN.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LightModeOptionBTN.Location = new System.Drawing.Point(34, 217);
            this.LightModeOptionBTN.Name = "LightModeOptionBTN";
            this.LightModeOptionBTN.Size = new System.Drawing.Size(98, 20);
            this.LightModeOptionBTN.TabIndex = 7;
            this.LightModeOptionBTN.TabStop = true;
            this.LightModeOptionBTN.Text = "LightMode";
            this.LightModeOptionBTN.UseVisualStyleBackColor = true;
            // 
            // GameEngineSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.Controls.Add(this.LightModeOptionBTN);
            this.Controls.Add(this.DarkModeOptionBTN);
            this.Controls.Add(this.ColorModeTitle);
            this.Controls.Add(this.LanguageSettingsDescription);
            this.Controls.Add(this.LanguageSettingsTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.settingsTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GameEngineSettings";
            this.Text = "GameEngineSettings";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameEngineSettings_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label settingsTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LanguageSettingsTitle;
        private System.Windows.Forms.Label LanguageSettingsDescription;
        private System.Windows.Forms.Label ColorModeTitle;
        private System.Windows.Forms.RadioButton DarkModeOptionBTN;
        private System.Windows.Forms.RadioButton LightModeOptionBTN;
    }
}