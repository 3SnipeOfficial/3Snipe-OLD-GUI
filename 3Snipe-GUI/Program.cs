using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace _3Snipe_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", "3Snipe-GUI.exe", null) == null)
            {
                string directory = Directory.GetCurrentDirectory() + @"\3Snipe-SetRegKeys.reg";
                Process regeditProcess = Process.Start("regedit.exe", "/s \"" + directory + "\"");
                regeditProcess.WaitForExit();
            }
            CheckForUpdates("repos/3SnipeTeam/3Snipe-CS/releases/latest");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static private void CheckForUpdates(string releasesEndpoint)
        {
            RestClient restClient = new RestClient("https://api.github.com/");
            RestRequest restRequest = new RestRequest(releasesEndpoint, Method.GET);
            RestResponse response = (RestResponse)restClient.Execute(restRequest);
            string currentVcode = (string)JObject.Parse(response.Content)["tag_name"];
            if (currentVcode != Form1.versionCode)
            {
                int i = 0;
                foreach (string str in currentVcode.Split('.'))
                {
                    if (Int32.Parse(str) > Int32.Parse(Form1.versionCode.Split('.')[i]))
                    {
                        Update3Snipe(releasesEndpoint);
                    }
                    i++;
                }
            }
        }
        static private void Update3Snipe(string releasesEndpoint)
        {
            DialogResult result = MessageBox.Show("A new version is avaliable. Update now?", "Update Avaliable", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                RestClient restClient = new RestClient("https://api.github.com/");
                RestRequest restRequest = new RestRequest(releasesEndpoint, Method.GET);
                RestResponse response = (RestResponse)restClient.Execute(restRequest);
                JArray assets = JObject.Parse(response.Content)["assets"] as JArray;
                using (var client = new WebClient())
                {
                    client.DownloadFile((string)((JObject)assets[0])["browser_download_url"], Path.GetTempPath() + @"\3Snipe-Setup.msi");
                }
                Process proc = new Process();
                proc.StartInfo.FileName = (Path.GetTempPath() + @"\3Snipe-Setup.msi");
                proc.Start();
                Environment.Exit(0);
            }
        }
    }
}
