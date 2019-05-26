using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json.Linq;

namespace VE_Game_Launcher
{
    public partial class MainForm : Form
    {

        public static readonly string JDK_URL = "https://download.java.net/java/GA/jdk12.0.1/69cfe15208a647278a19ef0990eea691/12/GPL/openjdk-12.0.1_windows-x64_bin.zip";
        public static readonly string JFX_URL = "https://download2.gluonhq.com/openjfx/12.0.1/openjfx-12.0.1_windows-x64_bin-sdk.zip";
        public readonly Downloader downloaderTask = new Downloader(Properties.Settings.Default.files_repository, AppDomain.CurrentDomain.BaseDirectory);
        private string progressStatus;

        public MainForm()
        {
            InitializeComponent();
            downloaderTask.DownloadArtifactsList();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            this.btnPlay.Enabled = false;
            this.progressBar.Maximum = downloaderTask.Artifacts.Count + 2;
            this.taskWorker.RunWorkerAsync();
        }

        private void TaskWorker_DoWork(object sender, DoWorkEventArgs args)
        {
            int current = 0;
            var backgroundWorker = sender as BackgroundWorker;
            foreach (JObject obj in downloaderTask.Artifacts)
            {
                this.progressStatus = $"Downloading {obj["name"]}";
                backgroundWorker.ReportProgress(current);
                downloaderTask.DownloadArtifact(obj["id"].ToString(), obj["location"].ToString(), obj["name"].ToString());
                backgroundWorker.ReportProgress(++current);
            }

            this.progressStatus = "Downloading the Java runtime";
            backgroundWorker.ReportProgress(current);
            if (!Directory.Exists($@"{downloaderTask.RootPath}runtime\jdk-12.0.1"))
            {
                downloaderTask.DownloadZipFile(JDK_URL, "openjdk-12.0.1_windows-x64_bin.zip", "runtime");
            }
            backgroundWorker.ReportProgress(++current);

            this.progressStatus = "Downloading JavaFX";
            backgroundWorker.ReportProgress(current);
            if (!Directory.Exists($@"{downloaderTask.RootPath}runtime\javafx-sdk-12.0.1"))
            {
                downloaderTask.DownloadZipFile(JFX_URL, "openjfx-12.0.1_windows-x64_bin-sdk.zip", "runtime");
            }  
            backgroundWorker.ReportProgress(++current);
        }

        private void TaskWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.CustomText = this.progressStatus;
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void TaskWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Directory.GetFiles(downloaderTask.RootPath, "*.jar").Length == 0 || downloaderTask.Artifacts.FindIndex(tk => tk["name"].ToString().Contains(".java")) != -1)
            {
                this.progressBar.CustomText = "Compiling the game";

                JavaCompiler compiler = new JavaCompiler(this.downloaderTask);
                compiler.ListSources();
                compiler.CompileAndPackJar("VehicularEpic");
                compiler.CleanTemporaryFiles();
            }
            
            this.progressBar.CustomText = "Starting game";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $@"{downloaderTask.RootPath}runtime\jdk-12.0.1\bin\javaw";
            startInfo.Arguments = @"-cp VehicularEpic.jar;lib/*;runtime/javafx-sdk-12.0.1/lib/* ve.Launcher";
            using (var process = Process.Start(startInfo))
            {
                this.Hide();
                process.WaitForExit();
            }

            this.btnPlay.Enabled = true;
            this.progressBar.CustomText = String.Empty;
            this.progressBar.Value = 0;
            this.Show();
        }
    }
}
