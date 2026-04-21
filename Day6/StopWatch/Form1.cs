using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int times, timemin, timehr;
        bool isActive;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetTime();
            isActive = false;
            timer1.Start();   // start timer (runs in background)
        }

        private void button1_Click(object sender, EventArgs e) // Start
        {
            isActive = true;
        }

        private void button2_Click(object sender, EventArgs e) // Stop
        {
            isActive = false;
        }

        private void button3_Click(object sender, EventArgs e) // Reset
        {
            isActive = false;
            ResetTime();
            DrawTime();
        }

        private void ResetTime()
        {
            times = 0;
            timemin = 0;
            timehr = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                times++;

                if (times >= 60)
                {
                    timemin++;
                    times = 0;

                    if (timemin >= 60)
                    {
                        timehr++;
                        timemin = 0;
                    }
                }
            }

            DrawTime();
        }

        private void DrawTime()
        {
            // Hours | Minutes | Seconds
            richTextBox1.Text = timehr.ToString("00");
            richTextBox2.Text = timemin.ToString("00");
            richTextBox3.Text = times.ToString("00");
        }
    }
}