using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Net;
using System.Threading;

namespace _3Snipe_GUI
{
    public partial class Form3 : Form
    {
        private static List<Thread> threadList = new List<Thread>();
        public static int proxyInt = 0;
        public Form3()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => checkProxies(textBox1.Lines));
        }
        private void checkProxies(string[] lines)
        {
            int i = 0;
            proxyInt = lines.Length;
            Parallel.ForEach(lines, new ParallelOptions { MaxDegreeOfParallelism = 1000 },
            l =>
            {
            SoketConnect(l); 
            });
            this.Close();
        }
        public void SoketConnect(string proxy2)
        {
            var proxy1 = proxy2;
            var proxy = proxy1;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://google.com");
            request.Proxy = new WebProxy(proxy);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            request.Timeout = 10000;
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (Exception)
            {
                Interlocked.Decrement(ref proxyInt);
                this.Text = $"Checking Proxies | {proxyInt} Left | {Form1.workingProxies.Count} Working";
                return;
            }
            Form1.workingProxies.Add(proxy);
            Interlocked.Decrement(ref proxyInt);
            this.Text = $"Checking Proxies | {proxyInt} Left | {Form1.workingProxies.Count} Working";
            return;
            
        }
    }
}
