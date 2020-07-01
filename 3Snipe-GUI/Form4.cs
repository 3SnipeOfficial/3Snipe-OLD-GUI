using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Snipe_GUI
{
    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();
            numericUpDown1.Value = Form1.pingCount;
            checkBox1.Checked = Form1.multithread;
            numericUpDown2.Value = Form1.threads;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Form1.pingCount = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form3ref = new Form3();
            form3ref.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.multithread = checkBox1.Checked;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Form1.threads = (int)numericUpDown2.Value;
        }
    }
}
