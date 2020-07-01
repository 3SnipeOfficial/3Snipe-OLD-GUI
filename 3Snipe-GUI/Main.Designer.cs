namespace _3Snipe_GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.controlPanel = new System.Windows.Forms.TabPage();
            this.snipeMode = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.name = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.sniperThreadCounter = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.sniperThreadsSlider = new System.Windows.Forms.TrackBar();
            this.colorSelector = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.snipeMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sniperThreadsSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.controlPanel);
            this.tabControl1.Location = new System.Drawing.Point(-2, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 319);
            this.tabControl1.TabIndex = 1;
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.snipeMode);
            this.controlPanel.Controls.Add(this.name);
            this.controlPanel.Controls.Add(this.textBox1);
            this.controlPanel.Controls.Add(this.password);
            this.controlPanel.Controls.Add(this.email);
            this.controlPanel.Controls.Add(this.button2);
            this.controlPanel.Controls.Add(this.sniperThreadCounter);
            this.controlPanel.Controls.Add(this.textBox3);
            this.controlPanel.Controls.Add(this.sniperThreadsSlider);
            this.controlPanel.Location = new System.Drawing.Point(4, 22);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Padding = new System.Windows.Forms.Padding(3);
            this.controlPanel.Size = new System.Drawing.Size(614, 293);
            this.controlPanel.TabIndex = 0;
            this.controlPanel.Text = "Sniping Control";
            this.controlPanel.UseVisualStyleBackColor = true;
            // 
            // snipeMode
            // 
            this.snipeMode.Controls.Add(this.textBox2);
            this.snipeMode.Controls.Add(this.checkBox1);
            this.snipeMode.Controls.Add(this.radioButton2);
            this.snipeMode.Controls.Add(this.radioButton1);
            this.snipeMode.Location = new System.Drawing.Point(243, 6);
            this.snipeMode.Name = "snipeMode";
            this.snipeMode.Size = new System.Drawing.Size(187, 156);
            this.snipeMode.TabIndex = 9;
            this.snipeMode.Validating += new System.ComponentModel.CancelEventHandler(this.validateSettings);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(0, 83);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(187, 73);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Info\r\nSniping: Sets a paid account\'s name.\r\nBlock: Blocks anyone from getting a n" +
    "ame.\r\nTurbo: Attempt every 24 hours.";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(4, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Turbo?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(4, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(52, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Block";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(52, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Snipe";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(6, 58);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(136, 20);
            this.name.TabIndex = 8;
            this.name.UseSystemPasswordChar = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(148, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(89, 75);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Email\r\n\r\nPassword\r\n\r\nYoink This Name";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(6, 32);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(136, 20);
            this.password.TabIndex = 6;
            this.password.UseSystemPasswordChar = true;
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(6, 5);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(136, 20);
            this.email.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 28);
            this.button2.TabIndex = 4;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sniperThreadCounter
            // 
            this.sniperThreadCounter.Location = new System.Drawing.Point(521, 8);
            this.sniperThreadCounter.Margin = new System.Windows.Forms.Padding(0);
            this.sniperThreadCounter.Name = "sniperThreadCounter";
            this.sniperThreadCounter.Size = new System.Drawing.Size(29, 20);
            this.sniperThreadCounter.TabIndex = 3;
            this.sniperThreadCounter.Text = "20";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(550, 8);
            this.textBox3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(45, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "Threads";
            // 
            // sniperThreadsSlider
            // 
            this.sniperThreadsSlider.LargeChange = 10;
            this.sniperThreadsSlider.Location = new System.Drawing.Point(550, 32);
            this.sniperThreadsSlider.Maximum = 100;
            this.sniperThreadsSlider.Minimum = 20;
            this.sniperThreadsSlider.Name = "sniperThreadsSlider";
            this.sniperThreadsSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sniperThreadsSlider.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sniperThreadsSlider.Size = new System.Drawing.Size(45, 249);
            this.sniperThreadsSlider.TabIndex = 0;
            this.sniperThreadsSlider.TabStop = false;
            this.sniperThreadsSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sniperThreadsSlider.Value = 20;
            this.sniperThreadsSlider.Scroll += new System.EventHandler(this.sniperThreadsSlider_Scroll);
            // 
            // colorSelector
            // 
            this.colorSelector.AnyColor = true;
            this.colorSelector.SolidColorOnly = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(616, 317);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "3Snipe";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.snipeMode.ResumeLayout(false);
            this.snipeMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sniperThreadsSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage controlPanel;
        private System.Windows.Forms.ColorDialog colorSelector;
        private System.Windows.Forms.TextBox sniperThreadCounter;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TrackBar sniperThreadsSlider;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Panel snipeMode;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}