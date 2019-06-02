using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace VE_Game_Launcher
{
    public class Downloader
    {

        public readonly string Hostname, RootPath;
        private System.Net.WebClient webClient = new System.Net.WebClient();
        private List<JToken> artifacts;

        public List<JToken> Artifacts
        {
            get { return this.artifacts; }
        }

        public Downloader(string hostname, string path)
        {
            this.Hostname = hostname;
            this.RootPath = path;
        }


        public async void DownloadArtifactsList()
        {
            string data = await webClient.DownloadStringTaskAsync($"http://{this.Hostname}/artifacts");
            this.artifacts = JArray.Parse(data).ToList();
            if (!File.Exists($@"{this.RootPath}artifacts.json"))
            {
                this.WriteFile(data, "artifacts", "json");
            }
            else
            {
                JArray local_artifacts = JArray.Parse(File.ReadAllText($@"{this.RootPath}artifacts.json"));
                foreach (JObject obj in local_artifacts)
                {
                    JToken found = artifacts.Find(e => e["id"].Equals(obj["id"]));
                    string filePath = $"{this.RootPath}{obj["location"].ToString()}{obj["name"].ToString()}";
                    if (found is null)
                    {
                        File.Delete(filePath);
                    }
                    else if (found["version"].Equals(obj["version"]) && File.Exists(filePath))
                    {
                        artifacts.Remove(found);
                    }
                }
                if (artifacts.Count > 0)
                {
                    this.WriteFile(data, "artifacts", "json");
                }
            }
        }

        private void WriteFile(string data, string fileName, string extension)
        {
            using (StreamWriter writer = new StreamWriter($@"{this.RootPath}{fileName}.{extension}"))
            {
                writer.WriteLine(data);
            }
        }

        public void DownloadArtifact(string artifactId, string filePath, string fileName)
        {
            if (!Directory.Exists($"{this.RootPath}{filePath}"))
            {
                Directory.CreateDirectory($"{this.RootPath}{filePath}");
            }
            webClient.DownloadFile($"http://{this.Hostname}/artifact/{artifactId}", $"{this.RootPath}{filePath}{fileName}");
        }

        public void DownloadZipFile(string url, string fileName, string extractTo)
        {
            webClient.DownloadFile(url, $"{this.RootPath}{fileName}");
            ZipFile.ExtractToDirectory($"{this.RootPath}{fileName}", $"{this.RootPath}{extractTo}");
            File.Delete($"{this.RootPath}{fileName}");
        }

        public Int64 GetFileSize(string url)
        {
            webClient.OpenRead(url);
            return Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]);
        }

        public JArray GetSources()
        {
            return JArray.Parse(webClient.DownloadString($"http://{this.Hostname}/sources"));
        }

    }
}
