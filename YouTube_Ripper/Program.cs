using System;
using System.IO;
using System.Net;
using System.Threading;

namespace YouTube_Ripper
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = "youtube-dl.exe";
            string filepath = path + "\\" + file;
            if (!File.Exists(filepath))
            {
                YouTubeDL();
            }
            else
            {
                YouTubeAudioDL();
            }
            YouTubeAudioDL();
            Console.ReadLine();
        }

        static void YouTubeAudioDL()
        {
            string link = null;
            while (link == null)
            {
                Console.Write("Enter youtube link: ");
                link = Console.ReadLine();
            }
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = "youtube-dl.exe";
            string filepath = path + "\\" + file;
            string command = " -x --audio-format mp3 " + link;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + filepath + command;
            process.StartInfo = startInfo;
            process.Start();
            Console.WriteLine("Downloading audio..." + Environment.NewLine);
            process.WaitForExit();
            Console.WriteLine("Download completed!" + Environment.NewLine);
            YouTubeAudioDL();
        }

        static void YouTubeDL()
        {
            Console.WriteLine("Downloading youtube-dl..." + Environment.NewLine);
            string url = "https://yt-dl.org/downloads/latest/youtube-dl.exe";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = "youtube-dl.exe";
            string filepath = path + "\\" + file;
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(url), filepath);
            }
            Console.ReadLine();
        }

        static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive + " bytes." + Environment.NewLine);
            Console.WriteLine("{0}% completed..." + Environment.NewLine, e.ProgressPercentage);
        }

        static void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download completed!" + Environment.NewLine);
            YouTubeAudioDL();
        }
    }
}
