using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;

namespace _3Snipe_GUI
{
    public partial class Main : Form
    {
        private static int mode;
        /* 0 = snipe, no turbo
         * 1 = snipe, turbo
         * 2 = block, no turbo
         * 3 = block, turbo
         */ 
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void sniperThreadsSlider_Scroll(object sender, EventArgs e)
        {
            this.sniperThreadCounter.Text = this.sniperThreadsSlider.Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private static object authorize(string email, string password)
        {
            WebRequest request = WebRequest.Create("https://authserver.mojang.com/authenticate");
            request.Method = "POST";
            byte[] jsonBytes = Encoding.UTF8.GetBytes($"{{'captchaSupported':'none', 'email': '{email}', 'password', '{password}'}}");
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(jsonBytes, 0, jsonBytes.Length);
            dataStream.Close();
            Stream responseStream = request.GetResponse().GetResponseStream();
            int contentLen = (int)request.GetResponse().ContentLength;
            byte[] responseBytes = { };
            responseStream.Read(responseBytes, 0, contentLen);
            JObject response = JObject.Parse(Encoding.UTF8.GetString(responseBytes));
            responseStream.Close();
            string accessToken = (string)response["accessToken"];
            string clientToken = (string)response["clientToken"];
            return new { accessToken = accessToken, clientToken = clientToken };
        }
        private void button2_Click(object sender, EventArgs e) //start sniper
        {
            var email = this.email.Text;
            var password = this.password.Text;
            var name = this.name.Text;
            dynamic authorization = authorize(email, password);
            var accessToken = authorization.accessToken;
            var dropTime = getDropTime(name);
            object info = new {
                accessToken = accessToken,
                name = name,
                password = password,
                dropTime = dropTime
            };
            
        }
        private static DateTime getDropTime(string name)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://api.mojang.com/users/profiles/minecraft/" + name + $"?at={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}");
                request.Method = "GET";
                Stream dataStream = request.GetRequestStream();
                dataStream.Close();
                var responseBytes = new byte[] { };
                Stream responseStream = request.GetResponse().GetResponseStream();
                responseStream.Read(responseBytes, 0, (int)request.GetResponse().ContentLength);
                dynamic response = JObject.Parse(Encoding.UTF8.GetString(responseBytes));
                responseStream.Close();
                string id = (string)response["id"];
                request = WebRequest.Create($"https://api.mojang.com/user/profiles/{id}/names");
                dataStream = request.GetRequestStream();
                dataStream.Close();
                responseBytes = new byte[] { };
                responseStream = request.GetResponse().GetResponseStream();
                responseStream.Read(responseBytes, 0, (int)request.GetResponse().ContentLength);
                response = JArray.Parse(Encoding.UTF8.GetString(responseBytes));
                responseStream.Close();
                int lastInstanceOf = response.Length - 1;
                for (int i = 0; i < response.Length; i++)
                {
                    if (response[i]["name"] == name.ToLower())
                        lastInstanceOf = i;
                }
                int droptime = (int)response[lastInstanceOf + 1]["changedToAt"];
                return DateTimeOffset.FromUnixTimeMilliseconds(droptime + 3196800000).ToLocalTime().DateTime;
            } catch
            {
                return DateTime.Now;
            }
        }
        private static void snipeName(dynamic info)
        {
            string accessToken = info.accessToken;
            string name = info.name;
            string password = info.password;
            DateTime dropTime = DateTimeOffset.FromUnixTimeMilliseconds(info.droptime).ToLocalTime().DateTime;
            
        }

        private void validateSettings(object sender, CancelEventArgs e)
        {
            mode = 0;
            if (this.radioButton1.Checked)
            { }
            if (this.radioButton2.Checked)
                mode += 2;
            if (this.checkBox1.Checked)
                mode++;
        }
    }
}
