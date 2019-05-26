namespace VE_Game_Launcher
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.webContent = new System.Windows.Forms.WebBrowser();
            this.btnPlay = new System.Windows.Forms.Button();
            this.taskWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new ProgressBarSample.TextProgressBar();
            this.SuspendLayout();
            // 
            // webContent
            // 
            this.webContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webContent.Location = new System.Drawing.Point(12, 12);
            this.webContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webContent.Name = "webContent";
            this.webContent.Size = new System.Drawing.Size(1240, 602);
            this.webContent.TabIndex = 0;
            this.webContent.Url = new System.Uri("https://ryan-albano.itch.io/the-vehicular-epic", System.UriKind.Absolute);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(12, 646);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(1240, 23);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Play Game";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // taskWorker
            // 
            this.taskWorker.WorkerReportsProgress = true;
            this.taskWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TaskWorker_DoWork);
            this.taskWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.TaskWorker_ProgressChanged);
            this.taskWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TaskWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.CustomText = "";
            this.progressBar.Location = new System.Drawing.Point(12, 620);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.SystemColors.Highlight;
            this.progressBar.Size = new System.Drawing.Size(1240, 20);
            this.progressBar.TabIndex = 3;
            this.progressBar.TextColor = System.Drawing.Color.Black;
            this.progressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressBar.VisualMode = ProgressBarSample.ProgressBarDisplayMode.CustomText;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.webContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Vehicular Epic Launcher 1.0 (by Vitor Mac)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webContent;
        private System.Windows.Forms.Button btnPlay;
        private ProgressBarSample.TextProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker taskWorker;
    }
}

