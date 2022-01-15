
namespace Open_Source_GameEngine14
{
    partial class ResourceDownloader
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
            this.DownloadedAmountProgressBar = new System.Windows.Forms.ProgressBar();
            this.downloadingWhatItemText = new System.Windows.Forms.Label();
            this.downloadSpeedText = new System.Windows.Forms.Label();
            this.amountOfDataDownloadedText = new System.Windows.Forms.Label();
            this.downloadStatusText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DownloadedAmountProgressBar
            // 
            this.DownloadedAmountProgressBar.Location = new System.Drawing.Point(10, 82);
            this.DownloadedAmountProgressBar.Name = "DownloadedAmountProgressBar";
            this.DownloadedAmountProgressBar.Size = new System.Drawing.Size(695, 42);
            this.DownloadedAmountProgressBar.TabIndex = 0;
            // 
            // downloadingWhatItemText
            // 
            this.downloadingWhatItemText.AutoSize = true;
            this.downloadingWhatItemText.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingWhatItemText.Location = new System.Drawing.Point(12, 9);
            this.downloadingWhatItemText.Name = "downloadingWhatItemText";
            this.downloadingWhatItemText.Size = new System.Drawing.Size(228, 28);
            this.downloadingWhatItemText.TabIndex = 1;
            this.downloadingWhatItemText.Text = "Downloading {item}";
            // 
            // downloadSpeedText
            // 
            this.downloadSpeedText.AutoSize = true;
            this.downloadSpeedText.Enabled = false;
            this.downloadSpeedText.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold);
            this.downloadSpeedText.Location = new System.Drawing.Point(14, 50);
            this.downloadSpeedText.Name = "downloadSpeedText";
            this.downloadSpeedText.Size = new System.Drawing.Size(55, 21);
            this.downloadSpeedText.TabIndex = 2;
            this.downloadSpeedText.Text = "0 B/s";
            this.downloadSpeedText.Visible = false;
            // 
            // amountOfDataDownloadedText
            // 
            this.amountOfDataDownloadedText.AutoSize = true;
            this.amountOfDataDownloadedText.Font = new System.Drawing.Font("Cascadia Mono", 11F, System.Drawing.FontStyle.Bold);
            this.amountOfDataDownloadedText.Location = new System.Drawing.Point(165, 50);
            this.amountOfDataDownloadedText.Name = "amountOfDataDownloadedText";
            this.amountOfDataDownloadedText.Size = new System.Drawing.Size(216, 20);
            this.amountOfDataDownloadedText.TabIndex = 3;
            this.amountOfDataDownloadedText.Text = "0 of 0 Bytes downloaded";
            // 
            // downloadStatusText
            // 
            this.downloadStatusText.AutoSize = true;
            this.downloadStatusText.Font = new System.Drawing.Font("Cascadia Mono", 10F, System.Drawing.FontStyle.Bold);
            this.downloadStatusText.Location = new System.Drawing.Point(306, 18);
            this.downloadStatusText.Name = "downloadStatusText";
            this.downloadStatusText.Size = new System.Drawing.Size(128, 18);
            this.downloadStatusText.TabIndex = 4;
            this.downloadStatusText.Text = "Status: Unknown";
            // 
            // ResourceDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 134);
            this.Controls.Add(this.downloadStatusText);
            this.Controls.Add(this.amountOfDataDownloadedText);
            this.Controls.Add(this.downloadSpeedText);
            this.Controls.Add(this.downloadingWhatItemText);
            this.Controls.Add(this.DownloadedAmountProgressBar);
            this.Name = "ResourceDownloader";
            this.Text = "ResourceDownloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResourceDownloader_FormClosing);
            this.Shown += new System.EventHandler(this.ResourceDownloader_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar DownloadedAmountProgressBar;
        private System.Windows.Forms.Label downloadingWhatItemText;
        private System.Windows.Forms.Label downloadSpeedText;
        private System.Windows.Forms.Label amountOfDataDownloadedText;
        private System.Windows.Forms.Label downloadStatusText;
    }
}