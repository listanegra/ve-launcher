using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VE_Game_Launcher
{
    class JavaCompiler
    {
        private readonly Downloader task;

        public JavaCompiler(Downloader task)
        {
            this.task = task;
        }

        public void ListSources()
        {
            using (StreamWriter writer = new StreamWriter($@"{task.RootPath}sources.txt"))
            {
                StringBuilder builder = new StringBuilder();
                foreach (JObject obj in task.GetSources())
                {
                    builder.Append('.').Append(obj["location"]).Append(obj["name"]).Append('\n');
                }
                writer.WriteLine(builder.ToString());
            }
        }

        public void CompileAndPackJar(string jarName)
        {
            ProcessStartInfo startInfo;
            try
            {
                startInfo = new ProcessStartInfo();
                startInfo.FileName = $@"{task.RootPath}runtime\jdk-12.0.1\bin\javac";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = @"-cp lib/\*;runtime/javafx-sdk-12.0.1/lib/\* -d output @sources.txt";
                using (var process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }

                startInfo = new ProcessStartInfo();
                startInfo.FileName = $@"{task.RootPath}runtime\jdk-12.0.1\bin\jar";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = $@"-cvfe {jarName}.jar ve/Launcher -C ./output .";
                using (var process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}");
            }
        }

        public void CleanTemporaryFiles()
        {
            File.Delete($@"{task.RootPath}sources.txt");
            Directory.Delete($@"{task.RootPath}output\", true);
        }

    }
}
