using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;


namespace MyPing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int DoWork()
        {
            string host = "1.1.1.1";
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1500;
            PingReply reply = pingSender.Send(host, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                int t = (int)reply.RoundtripTime;
                return t;
            }
            return 1500;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
            chartPing.Series["Ping"].Points.AddY(DoWork());
            if (chartPing.Series["Ping"].Points.Count > 50)
            {
                chartPing.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = chartPing.Series["Ping"].Points.Count - 50;
                chartPing.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 50;
            }
        }

        private void chartPing_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            chartPing.SaveImage("C:\\Users\\49667\\Pictures\\ping.png", System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            chartPing.Series["Ping"].Points.Clear();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
            Stop.Text = timer.Enabled ? "Stop" : "Start";
        }
    }
}
