namespace YouTubeDownloadApp
{
    partial class YouTubeDownload
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cmbQuality = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblVideoTitle = new System.Windows.Forms.Label();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(28, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(336, 50);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "YouTube Download";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUrl.Location = new System.Drawing.Point(32, 100);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(128, 25);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "YouTube URL";
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUrl.Location = new System.Drawing.Point(37, 132);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(652, 32);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "https://www.youtube.com/watch?v=...";
            // 
            // btnFetch
            // 
            this.btnFetch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFetch.Location = new System.Drawing.Point(706, 128);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(126, 40);
            this.btnFetch.TabIndex = 3;
            this.btnFetch.Text = "Fetch";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblDuration);
            this.grpInfo.Controls.Add(this.lblChannel);
            this.grpInfo.Controls.Add(this.lblVideoTitle);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpInfo.Location = new System.Drawing.Point(37, 192);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(795, 132);
            this.grpInfo.TabIndex = 4;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Video Information";
            // 
            // lblVideoTitle
            // 
            this.lblVideoTitle.AutoEllipsis = true;
            this.lblVideoTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVideoTitle.Location = new System.Drawing.Point(18, 31);
            this.lblVideoTitle.Name = "lblVideoTitle";
            this.lblVideoTitle.Size = new System.Drawing.Size(755, 25);
            this.lblVideoTitle.TabIndex = 0;
            this.lblVideoTitle.Text = "Title: -";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoEllipsis = true;
            this.lblChannel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChannel.Location = new System.Drawing.Point(18, 62);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(755, 25);
            this.lblChannel.TabIndex = 1;
            this.lblChannel.Text = "Channel: -";
            // 
            // lblDuration
            // 
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDuration.Location = new System.Drawing.Point(18, 93);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(755, 25);
            this.lblDuration.TabIndex = 2;
            this.lblDuration.Text = "Duration: -";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuality.Location = new System.Drawing.Point(32, 352);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(145, 25);
            this.lblQuality.TabIndex = 5;
            this.lblQuality.Text = "Video Quality";
            // 
            // cmbQuality
            // 
            this.cmbQuality.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbQuality.FormattingEnabled = true;
            this.cmbQuality.Location = new System.Drawing.Point(37, 386);
            this.cmbQuality.Name = "cmbQuality";
            this.cmbQuality.Size = new System.Drawing.Size(795, 33);
            this.cmbQuality.TabIndex = 6;
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDownload.Location = new System.Drawing.Point(37, 451);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(795, 53);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "Download Selected Quality";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(37, 535);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(795, 28);
            this.progressBar.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(33, 578);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(799, 43);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Enter a YouTube URL and click Fetch.";
            // 
            // YouTubeDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 647);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.cmbQuality);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.btnFetch);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "YouTubeDownload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Download";
            this.grpInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblVideoTitle;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.ComboBox cmbQuality;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}
