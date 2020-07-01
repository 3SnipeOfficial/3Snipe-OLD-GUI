using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using Nidhogg.rest;
using Nidhogg.data;
using System.Threading;
using System.Net.NetworkInformation;
using System.IO;
using System.Timers;
using System.Net.Sockets;
using System.Drawing.Text;

namespace _3Snipe_GUI
{
    public partial class Form1 : Form
    {
        public static WebBrowser browser;
        public static DateTime time;
        public static bool multithread = false;
        private static string accessToken = String.Empty;
        private static string password = String.Empty;
        private static string email = String.Empty;
        static string nameSniped = String.Empty;
        static string uuidOfUser = String.Empty;
        static int snipeAttempts = 20;
        static bool block = false;
        public static int pingCount = 8;
        public static readonly string versionCode = "1.2.1";
        public static List<string> workingProxies = new List<string>();
        public static int threads = 200;
        private static List<Thread> threadList = new List<Thread>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.AutoReset = false;
            t.Elapsed += checkPing;
            t.Enabled = true;
        }

        private void checkPing(object sender, ElapsedEventArgs e)
        {
            List<long> pingList = new List<long>();
            Ping p = new Ping();
            PingReply r;
            string s;
            s = "api.mojang.com";
            long i;
                r = p.Send(s);
            if (r.Status == IPStatus.Success)
            {
                i = r.RoundtripTime/2;
                label6.Text = i.ToString();
            }
            else
            {
                label6.Text = "Failed to ping.";
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browser = (WebBrowser)sender;
            var cookieString = WebUtility.HtmlDecode((string)browser.Document.Cookie);
            var cookieArray = cookieString.Split(';');
            foreach (var cookie in cookieArray)
            {
                if (cookie.Contains("bearer_token"))
                {
                    accessToken = cookie.Substring(14);
                    checkBox1.Checked = true;
                    System.Timers.Timer t = new System.Timers.Timer(1800000);
                    t.AutoReset = false;
                    t.Elapsed += bearerTokenRefresh;
                    t.Enabled = true;
                }
            }
        }
        private void bearerTokenRefresh(object sender, ElapsedEventArgs e)
        {
            browser.Refresh();
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                progressBar1.Visible = true;
                Task.Run(() => runSnipe());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            password = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            nameSniped = textBox2.Text;
        }

        public static RestResponse sendJsonReq(string baseurl, string endpoint, dynamic message, string bearerToken, Method method, string proxy)
        {
            var client = new RestClient(baseurl);
            var request = new RestRequest(endpoint, method);
            if (proxy != "")
                client.Proxy = new WebProxy(proxy);
            if (bearerToken != "")
            {
                request.AddHeader("Authorization", "Bearer " + bearerToken);
            }
            if (message.ToString() != "")
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message);
            }
            return (RestResponse)client.Execute(request);
        }
        public static RestResponse getName(string name, string timestamp)
        {
            var client = new RestClient("https://api.mojang.com/users/profiles/minecraft/");
            var request = new RestRequest(name + "?at=" + timestamp);
            request.AddQueryParameter("at", timestamp);
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            return (RestResponse)client.Execute(request);
        }
        public static long getPing(string url)
        {
            List<long> pingList = new List<long>();
            Ping p = new Ping();
            PingReply r;
            string s;
            s = url;
            int i;
            for (i = 0; i < pingCount; i++)
            {
                r = p.Send(s);
                if (r.Status == IPStatus.Success)
                {
                    pingList.Add(r.RoundtripTime);
                }
            }
            long h = 0;
            foreach (long l in pingList)
            {
                h += l;
            }
            return (long)(h / 8);
        }
        public RestResponse sendJsonReqThread(string baseurl, string endpoint, dynamic message, string bearerToken, Method method, string proxy)
        {
            var client = new RestClient(baseurl);
            var request = new RestRequest(endpoint, method);
            if (proxy != "")
                client.Proxy = new WebProxy("proxy");
            if (bearerToken != "")
            {
                request.AddHeader("Authorization", "Bearer " + bearerToken);
            }
            if (message.ToString() != "")
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message);
            }
            Console.WriteLine(client.BuildUri(request));
            return (RestResponse)client.Execute(request);
        }
        public static double getTimeOffset()
        {
            DateTime now = DateTime.Now;
            DateTime fromReq = DateTime.Now;
            fromReq = DateTime.Parse((string)JObject.Parse(sendJsonReq("http://worldtimeapi.org/api/", "ip", "", "", Method.GET, "").Content)["utc_datetime"]);
            try
            {
                double timeOffset = (now - fromReq).TotalMilliseconds;
                return timeOffset;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! " + e.Message);
            }


            return 0;
        }
        public static void runSnipe()
        {
            long dateTimeOffset = 0;
            string currentDir = Directory.GetCurrentDirectory();
            RestResponse response = new RestResponse();
            List<int> list = new List<int>();
            try
            {
                YggdrasilClient ygg = new YggdrasilClient();
                var yggSession = ygg.Login(new AccountCredentials(email, password));
                yggSession.AccessToken = accessToken;
                uuidOfUser = (string)JObject.Parse(sendJsonReq("https://api.mojang.com/users/profiles/minecraft/", yggSession.UserName, "", "", Method.GET, "").Content)["id"];
            } catch (Exception e)
            {
                MessageBox.Show("An error has occured. Check your account credentials.", "Error");
                Application.Exit();
            }
            try
            {
                dateTimeOffset = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) - 3283200;
                response = getName(nameSniped.ToLower(), dateTimeOffset.ToString());
                var oldOwnerUUID = (string)JObject.Parse((string)response.Content)["id"];
                response = sendJsonReq("https://api.mojang.com/user/profiles/", oldOwnerUUID + "/names", "", "", Method.GET, "");

                int i = 0;
                JArray responseString = JsonConvert.DeserializeObject<JArray>(response.Content);
                foreach (var payload in responseString)
                {
                    var stuff = (string)payload["name"];
                    if (stuff.ToLower() == nameSniped.ToLower())
                    {
                        list.Add(i);
                    }
                    i++;
                }
            } catch (Exception e)
            {
                MessageBox.Show("An error has occured. Check that the name isn't a reclaim or not avaliable and try again.", "Error");
                Application.Exit();
            }
            try
            {
                var time2 = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64((string)JArray.Parse(response.Content)[list[list.Count - 1] + 1]["changedToAt"]) + 3196800000).AddMilliseconds(0 - getPing("api.mojang.com"));
                time = time2.ToLocalTime().DateTime;
                if (multithread)
                {
                    
                    int i = 0;
                    Parallel.For(0, threads, (p) => timerThread(p));
                    System.Timers.Timer t = new System.Timers.Timer((time - DateTime.Now).TotalMilliseconds + 28000 - getPing("api.mojang.com"));
                    t.AutoReset = false;
                    t.Elapsed += checkForFail;
                    t.Enabled = true;
                } else
                {
                    System.Timers.Timer t = new System.Timers.Timer((time - DateTime.Now).TotalMilliseconds - 2000 - getPing("api.mojang.com"));
                    t.AutoReset = false;
                    t.Elapsed += TimerElapsed;
                    t.Enabled = true;
                }
            } catch (Exception e)
            {
                MessageBox.Show("An error has occured. Check that the name isn't a reclaim and try again.", "Error");
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            email = textBox3.Text;
        }
        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string message = $"Success! Set name to {nameSniped}.";
            RestResponse response = new RestResponse();
            string title = "Success!";
            int i = 0;
            var client = new RestClient("https://api.mojang.com/" + "user/profile/" + uuidOfUser);
            var request = new RestRequest("/name", Method.POST);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            if (message.ToString() != "")
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message);
            }
            if (!block)
            {
                for (i = 0; i < snipeAttempts; i++)
                {
                    response = (RestResponse)client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        MessageBox.Show(message, title);
                        System.Environment.Exit(0);
                    }
                    Thread.Sleep(100);
                }
                message = "Failed to snipe name...";
                title = "Failure...";
                MessageBox.Show(message, title);
                System.Environment.Exit(1);
            } else
            {
                for (i = 0; i < snipeAttempts; i++)
                {
                    response = sendJsonReq("https://api.mojang.com/user/profile/agent/minecraft/name", nameSniped, "", (string)(accessToken), Method.PUT, "");
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        MessageBox.Show(message, title);
                        System.Environment.Exit(0);
                    }
                    Thread.Sleep(100);
                }
                message = "Failed to block name...";
                title = "Failure...";
                MessageBox.Show(message, title);
                System.Environment.Exit(1);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            snipeAttempts = (int)numericUpDown1.Value;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            block = checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form2ref = new Form2();
            form2ref.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form4ref = new Form4();
            form4ref.ShowDialog();
        }
        private static void multiThreadSnipe(object sender, ElapsedEventArgs e, string proxy)
        {
            string message = $"Success! Set name to {nameSniped}.";
            RestResponse response = new RestResponse();
            string title = "Success!";
            int i = 0;
            if (!block)
            {
                for (i = 0; i < snipeAttempts; i++)
                {
                    response = sendJsonReq("https://api.mojang.com/" + "user/profile/" + uuidOfUser, "/name", new { name = nameSniped, password = password }, (string)(accessToken), Method.POST, proxy);
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        MessageBox.Show(message, title);
                        System.Environment.Exit(0);
                    }
                    Thread.Sleep(100);
                }
            }
            else
            {
                for (i = 0; i < snipeAttempts; i++)
                {
                    response = sendJsonReq("https://api.mojang.com/user/profile/agent/minecraft/name", nameSniped, "", (string)(accessToken), Method.PUT, proxy);
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        MessageBox.Show(message, title);
                        System.Environment.Exit(0);
                    }
                    Thread.Sleep(100);
                }
            }
            threads--;


        }
        private static void timerThread(int i)
        {
            int i1 = (int)i;
            int l = i1;
            Random rand = new Random();
            var proxyIndex = rand.Next(0, workingProxies.Count);
            var proxy = workingProxies.ElementAt(proxyIndex);
            workingProxies.RemoveAt(proxyIndex);
            System.Timers.Timer t = new System.Timers.Timer((time - DateTime.Now).TotalMilliseconds - ((snipeAttempts - 20) / 20 * 500) - getPing("api.mojang.com") + (l*5));
            t.AutoReset = false;
            t.Elapsed += (sender, e) => multiThreadSnipe(sender, e, proxy);
            t.Enabled = true;
        }
        private static void checkForFail(object sender, ElapsedEventArgs e)
        {
            string message = block ? "Failed to block name..." : "Failed to snipe name...";
            string title = "Failure...";
            MessageBox.Show(message, title);
            System.Environment.Exit(1);

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
