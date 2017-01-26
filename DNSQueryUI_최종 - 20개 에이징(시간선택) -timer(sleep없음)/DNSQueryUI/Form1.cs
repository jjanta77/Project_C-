using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using MetroFramework.Controls;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Timers;
using DnDns.Enums;
using DnDns.Query;
using DnDns.Records;

namespace DNSQueryUI
{
    public partial class Form1 : MetroForm
    {
        [DllImport("Encryption.dll")]
        public static extern int puts(string c);
        public Form1()
        {
            InitializeComponent();
        }

        bool sCheck = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            dnsqueryaging1();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            dnsqueryaging2();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            dnsqueryagingall();
        }
        //첫번째 도메인
        //Textbox의 도메인을 DNS 쿼리

        private void metroButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroLabel1.Text = "";
            try
            {
                /*
                IPAddress[] array = Dns.GetHostAddresses(metroTextBox1.Text);
                foreach (IPAddress ip in array)
                {
                    //Console.WriteLine(ip.ToString());
                    metroTextBox3.Text += ip.ToString() + Environment.NewLine;
                }
               */

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox1.Text);
                //DnsQueryRequest request = new DnsQueryRequest();
                //DnsQueryResponse response = request.Resolve(host, metroTextBox1.Text, NsType.A, NsClass.INET, ProtocolType.Udp);

                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    //foreach (IPAddress ips in host.AddressList)
                    //Console.WriteLine(ip.ToString());    
                    //metroLabel1.Text = ip.ToString().Replace(Environment.NewLine, ip.ToString()); //+ Environment.NewLine + ip.ToString();
                    metroLabel1.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }



                /*
                IPAddress[] addresslist = Dns.GetHostAddresses(metroTextBox1.Text);
                    foreach (IPAddress theaddress in addresslist)
                    {
                    //foreach (IPAddress ip in addresslist)
                    metroLabel1.Text = theaddress.ToString();// + Environment.NewLine + ip.ToString();                
                }*/
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ipconfig /flushdns 와 라벨 내용 삭제
        private void metroButton2_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroLabel1.Text = "";
            ServicePointManager.DnsRefreshTimeout = 0;

        }

        // DNS 쿼리 무한 반복 - 10초 딜레이
        private void metroButton3_Click(object sender, EventArgs e)
        {
            sCheck = true;
            timer1.Enabled = true;

            metroLabel1.Text = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel1.Text += ip.ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("만료 기간이 지났거나,네크워크 접속이 끊겼습니다.");
            }
            int myInt = 0;
            bool parsed = int.TryParse(metroTextBox23.Text, out myInt);

            if (parsed)
            {
                timer1.Interval = myInt;
            }
         }       
                     
        private void metroButton9_Click(object sender, EventArgs e) //stop
        {
            sCheck = false;
            timer1.Enabled = false;
        }
        
        
        private void dnsqueryaging1()
        {


            if (sCheck == true)
            {
                timer1.Enabled = false;
            }


          //  string s = metroTextBox23.Text;
          //  Convert.ToString(s.Length);
          //  int i = System.Convert.ToInt32(s);

            
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C ipconfig /flushdns";
                process.StartInfo = startInfo;
                process.Start();


            metroLabel1.Text = "";
                try
                {
                    IPHostEntry host;

                    host = Dns.GetHostEntry(metroTextBox1.Text);

                    foreach (IPAddress ip in host.AddressList)
                    {
                        Console.WriteLine();
                        metroLabel1.Text += Convert.ToString(ip) + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            // timer1.Interval = i;
            if (sCheck == true)
            {
                timer1.Enabled = true;
            }
        }
          


    // 2번째 도메인        

    private void metroButton6_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();


            metroLabel4.Text = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox2.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
        private void metroButton5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroLabel4.Text = "";
            ServicePointManager.DnsRefreshTimeout = 0;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            sCheck = true;
            timer2.Enabled = true;

            metroLabel4.Text = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox2.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("만료 기간이 지났거나,네크워크 접속이 끊겼습니다.");
            }

            int myInt = 0;
            bool parsed = int.TryParse(metroTextBox24.Text, out myInt);

            if (parsed)
            {
                timer2.Interval = myInt;
            }
            /*
            while (true)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C ipconfig /flushdns";
                process.StartInfo = startInfo;
                process.Start();

                metroLabel4.Text = "";
                try
                {
                    IPHostEntry host;

                    host = Dns.GetHostEntry(metroTextBox2.Text);

                    foreach (IPAddress ip in host.AddressList)
                    {
                        metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("오류가 발생 되었습니다.");
                    Console.WriteLine(ex.Message);
                }

                string s = metroTextBox24.Text;
                Convert.ToString(s.Length);
                int i = System.Convert.ToInt32(s);


                //TimeSpan duration = new TimeSpan(0, 0, 0, nVal);

                DateTime ThisMoment = DateTime.Now;
                TimeSpan duration = new TimeSpan(0, 0, 0, i);
                DateTime AfterWards = ThisMoment.Add(duration);
                while (AfterWards >= ThisMoment)
                {
                    System.Windows.Forms.Application.DoEvents();
                    ThisMoment = DateTime.Now;
                }

                //Task.Delay(10000);
                //System.Threading.Thread.Sleep(10000);               
            }*/
        }
        
        private void dnsqueryaging2()
        {


            if (sCheck == true)
            {
                timer2.Enabled = false;
            }


            //  string s = metroTextBox23.Text;
            //  Convert.ToString(s.Length);
            //  int i = System.Convert.ToInt32(s);


            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroLabel4.Text = "";
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox2.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // timer1.Interval = i;
            if (sCheck == true)
            {
                timer2.Enabled = true;
            }
        }
        
        private void metroButton10_Click(object sender, EventArgs e)
        {
            sCheck = false;
            timer2.Enabled = false;
        }


        private void metroButton7_Click(object sender, EventArgs e)
        {
            sCheck = true;
            timer3.Enabled = true;

            metroLabel1.Text = "";


            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox1.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel1.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // MessageBox.Show("만료 기간이 지났거나,네크워크 접속이 끊겼습니다.");
            }

            metroLabel4.Text = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox2.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               // MessageBox.Show("만료 기간이 지났거나,네크워크 접속이 끊겼습니다.");
            }

            int myInt = 0;
            bool parsed = int.TryParse(metroTextBox25.Text, out myInt);

            if (parsed)
            {
                timer3.Interval = myInt;
            }

           
            /*
                string s = metroTextBox25.Text;
                //Convert.ToString(s.Length);
                int i = System.Convert.ToInt32(s);


                //TimeSpan duration = new TimeSpan(0, 0, 0, nVal);

                DateTime ThisMoment = DateTime.Now;
                TimeSpan duration = new TimeSpan(0, 0, 0, i);
                DateTime AfterWards = ThisMoment.Add(duration);
                while (AfterWards >= ThisMoment)
                {
                    System.Windows.Forms.Application.DoEvents();
                    ThisMoment = DateTime.Now;
                }
                System.Threading.Thread.Sleep(100);
                //Task.Delay(10000);
                //System.Threading.Thread.Sleep(10000);
            }
            */
        }
        
        private void dnsqueryagingall()
        {
            if (sCheck == true)
            {
                timer3.Enabled = false;
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroLabel1.Text = "";
            metroLabel4.Text = "";
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox1.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel1.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Task.Delay(1000);
            //System.Threading.Thread.Sleep(100);

            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox2.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //3
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox3.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    //metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //4
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox4.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //5
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox5.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //6
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox6.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //7
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox7.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //8
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox8.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    //metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //9
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox9.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //10
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox10.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //11
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox11.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //12
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox12.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);

            //13
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox13.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    //metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //14
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox14.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //15
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox15.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //16
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox16.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //17
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox17.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //18
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox18.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    //metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //19
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox19.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //20
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox20.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //21
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox21.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);
            //22
            try
            {
                IPHostEntry host;

                host = Dns.GetHostEntry(metroTextBox22.Text);

                foreach (IPAddress ip in host.AddressList)
                {
                    Console.WriteLine();
                    // metroLabel4.Text += Convert.ToString(ip) + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //System.Threading.Thread.Sleep(100);

            if (sCheck == true)
            {
                timer3.Enabled = true;
            }
        }
        

        private void metroButton11_Click(object sender, EventArgs e)
        {
            sCheck = false;
            timer3.Enabled = false;
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {

            // Store all running process in the system
            Process[] runingProcess = Process.GetProcesses();
            for (int i = 0; i < runingProcess.Length; i++)
            {
                // compare equivalent process by their name
                if (runingProcess[i].ProcessName == "DNSQueryUI")
                {
                    // kill  running process
                    runingProcess[i].Kill();

                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] runingProcess = Process.GetProcesses();
            for (int i = 0; i < runingProcess.Length; i++)
            {
                // compare equivalent process by their name
                if (runingProcess[i].ProcessName == "DNSQueryUI")
                {
                    // kill  running process
                    runingProcess[i].Kill();

                }
            }
        }

        private void metroTextBox23_Click(object sender, EventArgs e)
        {
            metroTextBox23.Text = "";
        }

        private void metroTextBox24_Click(object sender, EventArgs e)
        {
            metroTextBox24.Text = "";
        }

        private void metroTextBox25_Click(object sender, EventArgs e)
        {
            metroTextBox25.Text = "";
        }

        //DNS 쿼리 및 IE로 콤보 박스의 URL로 이동
        private void metroButton12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/C ipconfig /flushdns";
           // process.StartInfo = startInfo;
            //process.Start();

            metroTextBox28.Text = "";
            //선택한 도메인 IP 출력
            try
            {

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox27.Text);
                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroTextBox28.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //IE 실행하여 선택한 도메인 이동
            try
            {
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
               // startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "/C ipconfig /flushdns";
                startInfo.FileName = "iexplore.exe";
                startInfo.Arguments = (metroTextBox27.Text);
                //startInfo.Arguments = (metroTextBox28.Text);

                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("인터넷 익스플로러 브라우저가 없습니다." + Environment.NewLine + "설정탭으로 이동하셔서 파이어폭스 브라우저를 설치 하세요", "브라우저 설치 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //DNS 쿼리 및 파폭으로 콤보 박스의 URL로 이동
        private void metroButton13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/C ipconfig /flushdns";
            //process.StartInfo = startInfo;
            //process.Start();

            metroTextBox28.Text = "";
            //선택한 도메인 IP 출력
            try
            {

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox27.Text);
                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroTextBox28.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //크롬 실행하여 선택한 도메인 이동
            try
            {
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "/C ipconfig /flushdns";
                startInfo.FileName = "firefox.exe";
                startInfo.Arguments = (metroTextBox27.Text);

                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("파이어폭스 브라우저가 없습니다." + Environment.NewLine + "설정탭으로 이동하셔서 파이어폭스 브라우저를 설치 하세요", "브라우저 설치 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //크롬으로 콤보박스에서 선택한 도메인으로 이동되는 동작, 크롬 없을 시 
        private void metroButton14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/C ipconfig /flushdns";
            //process.StartInfo = startInfo;
           // process.Start();

            metroTextBox28.Text = "";
            //선택한 도메인 IP 출력
            try
            {

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox27.Text);
                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroTextBox28.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //크롬 실행하여 선택한 도메인 이동
            try
            {
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = "/C ipconfig /flushdns";
                startInfo.FileName = "chrome.exe";
                startInfo.Arguments = (metroTextBox27.Text);

                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("크롬 브라우저가 없습니다." + Environment.NewLine + "설정탭으로 이동하셔서 파이어폭스 브라우저를 설치 하세요", "브라우저 설치 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        //TextBox의 도메인에 대한 Ping 동작 확인
        private void metroButton15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();


            startInfo.FileName = "CMD.exe";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            process.StandardInput.Write("ping " + (metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            Console.WriteLine("ping " + (metroTextBox27.Text) + Environment.NewLine);

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }


        //Tracert 동작 확인
        private void metroButton16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K tracert "+ (metroTextBox27.Text);
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            
            process.StandardInput.Write((metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();
            
            process.WaitForExit();
            process.Close();
        }

        //netstat 동작 확인
        private void metroButton17_Click(object sender, EventArgs e)
        {
            
            
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K netstat -bn";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            process.StandardInput.Write("netstat -bn " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();
            
            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();
            
            process.WaitForExit();
            process.Close(); 
            
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K ipconfig /flushdns";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            //process.StandardInput.Write("ipconfig /flushdns " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }

        private void metroButton19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/C ipconfig /flushdns";
            //process.StartInfo = startInfo;
            //process.Start();

            metroTextBox28.Text = "";
            try
            {

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox27.Text);
                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroTextBox28.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //레지스트리 Sreslover 내용 확인
        private void metroButton20_Click(object sender, EventArgs e)
        {
            /*
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            RegistryKey reg = Registry.LocalMachine;
            reg = Registry.LocalMachine.OpenSubKey(@"System\\CurrentControlSet\\Services");

            
            if (reg!=null)
            {
                Object val = reg.GetValue("ResolverVer");
                if (null !=val)
                {
                    metroTextBox28.Text = Convert.ToString("ResolverVer은 " + val + " 입니다.");
                    //MessageBox.Show(Convert.ToString(val));
;                }
            }
            

            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "regedit.exe" + "HKLM\\System\\CurrentControlSet\\Services\\sresolver";
            process.StartInfo = startInfo;
            process.Start();
            */

            
            //sresolver 레지스트 값을 텍스트 박스에 표현
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K reg query HKLM\\System\\CurrentControlSet\\Services\\sresolver /s";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            //process.StandardInput.Write("reg query HKLM\\System\\CurrentControlSet\\Services\\sresolver " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
         
        }
        //레지스트리 SecurePath 내용 확인
        private void metroButton16_Click_1(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K reg query HKLM\\System\\CurrentControlSet\\Services\\securepath /s";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            //process.StandardInput.Write("reg query HKLM\\System\\CurrentControlSet\\Services\\sresolver " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }
        //레지스트리 SecureEdge 내용 확인
        private void metroButton17_Click_1(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K reg query HKLM\\System\\CurrentControlSet\\Services\\EdgeFinder /s";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            //process.StandardInput.Write("reg query HKLM\\System\\CurrentControlSet\\Services\\sresolver " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }

        //후킹탭 텍스트 박스 글자수 세기
        private void metroTextBox28_TextChanged(object sender, EventArgs e)
        {
            countlabel1.Text = metroTextBox28.Text.Length.ToString();
        }

        //텍스트 박스 클릭시 빈칸 변경

        private void metroButton21_Click(object sender, EventArgs e)
        {
            //DirectoryInfo pathdi = new DirectoryInfo("C:\\Program Files (x86)\\securepath");
            DirectoryInfo edgedi = new DirectoryInfo("C:\\Program Files (x86)\\EdgeFinder");
            DirectoryInfo srdi = new DirectoryInfo("C:\\Program Files (x86)\\sresolver");
            /*
            try
            {
                if (pathdi.Exists)
                {
                    Process.Start("C:\\Program Files (x86)\\securepath");
                }
                else
                {
                    Process.Start("C:\\Program Files\\securepath");
                }
            }
            catch
            {
                MessageBox.Show("securepath폴더가 존재 하지 않습니다.", "SecurePath 설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
              */              

            try
            {
                if (edgedi.Exists)
                {
                    Process.Start("C:\\Program Files (x86)\\EdgeFinder");
                }
                else
                {
                    Process.Start("C:\\Program Files\\EdgeFinder");
                }               
            }
            catch
            {
                MessageBox.Show("EdgeFinder 존재 하지 않습니다.", "EdgeFinder 설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                if (srdi.Exists)
                {
                    Process.Start("C:\\Program Files (x86)\\sresolver");
                }
                else
                {
                    Process.Start("C:\\Program Files\\sresolver");
                }                
            }
            catch
            {
                MessageBox.Show("SResolver폴더가 존재 하지 않습니다.", "sresolver 설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        //services.msc 열기
        private void metroButton22_Click(object sender, EventArgs e)
        {
            Process.Start("services.msc");
        }
        //프로그램 추가 제거 열기
        private void metroButton23_Click(object sender, EventArgs e)
        {
            Process.Start("appwiz.cpl");
        }

        //SecurePath OEM 파일 다운로드
        private void metroButton26_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://hydra.uxcloud.net/Program/FileDownloadHandler.ashx?file=C:\\inetpub\\hydra.uxcloud.net\\Upload\\22b48389-8016-4b0c-82a5-704b36dae926&n=Edge_setup.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //SecurePath 파일다운로드
        private void metroButton27_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://hydra.uxcloud.net/Program/FileDownloadHandler.ashx?file=C:\\inetpub\\hydra.uxcloud.net\\Upload\\d2f79aa0-f6c7-4109-8045-ce9ceee79e5a&n=Securepath_0809.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //sresolver 다운로드
        private void metroButton36_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://hydra.uxcloud.net/Program/FileDownloadHandler.ashx?file=C:\\inetpub\\hydra.uxcloud.net\\Upload\021ea351-cdca-4304-a404-fc412adf41cb&n=sresolver_setup.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        //크롬 다운로드
        private void metroButton24_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B63A02678-F7D9-5470-D63A-5C240784E871%7D%26lang%3Den%26browser%3D3%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dfalse/update2/installers/ChromeStandaloneSetup.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //파이어폭스 다운로드
        private void metroButton25_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.mozilla.org/ko/firefox/new/?scene=2&utm_campaign=non-fx-button&utm_medium=referral&utm_source=addons.mozilla.org#download-fx");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //IE 11 64비트 다운로드
        private void metroButton28_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://download.microsoft.com/download/0/5/C/05CF28BA-0663-4B4F-BEAF-5C4A8AE9FE8F/IE11-Windows6.1-x64-ko-kr.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroButton29_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://download.microsoft.com/download/C/4/C/C4C088A7-7F88-4C45-9E63-169DD4D4FD50/IE11-Windows6.1-x86-ko-kr.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //와이어샤크 다운로드
        private void metroButton30_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://2.na.dl.wireshark.org/win64/Wireshark-win64-2.2.1.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroButton31_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://2.na.dl.wireshark.org/win32/Wireshark-win32-2.2.1.exe");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //.net 3.5 다운로드
        private void metroButton32_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.microsoft.com/ko-kr/download/confirmation.aspx?id=21");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //.net 4.0 다운로드
        private void metroButton33_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.microsoft.com/en-us/download/confirmation.aspx?id=17718");
            }
            catch
            {
                MessageBox.Show("파일이 존재 하지 않습니다.", "설치오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void metroButton34_Click(object sender, EventArgs e)
        {
            
            /*
            Console.WriteLine("Please enter your ISP's DNS server or prefered DNS server:");
            string dnsServer = Console.ReadLine();

            dnsServer = metroComboBox1.Text;


            Console.WriteLine("Enter a hostname:");
            string hostname = Console.ReadLine();

            DnsEx.DnsServers = new string[] { dnsServer };
            DnsEx dns = new DnsEx();
            DnsServerResponse response = null;
            //response = dns.Query(hostname, QTYPE.NS);
            response = dns.Query(hostname, QTYPE.metroComboBox2.Text);
            StringBuilder sb = new StringBuilder();

            // Alternative lookups
            // response = dns.Query(this.TextBoxQuery.Text, QTYPE.CNAME);
            // response = dns.Query(this.TextBoxQuery.Text, QTYPE.MX);

            foreach (object rec in response.Answers)
            {

                if (rec is NS_Record)
                {
                    sb.Append(((NS_Record)rec).NameServer + "\n");
                }
                else if (rec is CNAME_Record)
                {
                    sb.Append(((CNAME_Record)rec).Alias + "\n");
                }

                else if (rec is PTR_Record)
                {
                    sb.Append(((PTR_Record)rec).DomainName + "\n");
                }

                else if (rec is MX_Record)
                {
                    sb.Append(((MX_Record)rec).Host + " ");
                    sb.Append("(Preference: " + ((MX_Record)rec).Preference.ToString() + ") \n");
                }
            }

            // Show the results
            this.metroTextBox30.Text = sb.ToString();


            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ipconfig /flushdns";
            process.StartInfo = startInfo;
            process.Start();

            metroTextBox28.Text = "";
            try
            {

                IPHostEntry host;
                host = Dns.GetHostEntry(metroTextBox27.Text);
                //IPHostEntry ipentry = Dns.Resolve(metroTextBox1.Text);
                foreach (IPAddress ip in host.AddressList)
                {
                    metroTextBox28.Text += ip.ToString() + Environment.NewLine; // +ip.ToString();

                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("아래 내용을 확인 후 다시 시도해보세요" + Environment.NewLine + "1. 도메인 주소를 다시 확인 해보세요. (DNS쿼리의 도메인 주소가 잘못되었습니다.)" + Environment.NewLine + "2. 네트워크 접속을 확인해보세요. (만료 기간이 지났거나,네크워크 접속이 끊겼습니다.)", "후킹 IP 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Console.WriteLine("");
            //Console.WriteLine("Results:");
            //Console.WriteLine(sb.ToString());
            //Console.ReadLine();

            */
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            process.StartInfo = startInfo;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "nslookup.exe";
            startInfo.WorkingDirectory = @"C:\";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            
            process.Start();    //프로세스 시작
            process.StandardInput.Write("server " + (metroTextBox26.Text) + Environment.NewLine);
            process.StandardInput.Write("set type=" + (metroComboBox2.Text) + Environment.NewLine);
            process.StandardInput.Write((metroTextBox29.Text) + Environment.NewLine);//예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox30.Text = sb.ToString();

            process.WaitForExit();
            process.Close();

    
        }

        private void metroButton35_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K ipconfig /flushdns";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            //process.StandardInput.Write("ipconfig /flushdns " + Environment.NewLine);     //명령어를 입력
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox30.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }

        //NSLookup 탭의 텍스트 박스 글자수
        private void metroTextBox30_TextChanged(object sender, EventArgs e)
        {
            metroLabel21.Text = metroTextBox30.Text.Length.ToString();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            this.metroTextBox28.Text = DateTime.Now.ToString();
        }

        //net stop sresolverservice (DSRMService)- 윈도우 서비스에 들어 있는 항목
        private void metroButton37_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K net stop DSRMService ";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작

            //process.StandardInput.Write((metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }
        //net start sresolverservice (DSRMService)- 윈도우 서비스에 들어 있는 항목
        private void metroButton39_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K net start DSRMService ";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작

            //process.StandardInput.Write((metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }
        //net stop sresolver - sresolver의 서비스
        private void metroButton38_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K net stop Sresolver ";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작

            //process.StandardInput.Write((metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }
        //net start sresolver - sresolver의 서비스
        private void metroButton40_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/K net start Sresolver ";
            startInfo.WorkingDirectory = @"C:\";

            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작

            //process.StandardInput.Write((metroTextBox27.Text) + Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();                     //실행결과를 standard output으로 받아와 string값에 저장
            string error = process.StandardError.ReadToEnd();                        //오류유무를 standard output으로 받아와 string값에 저장
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[ Result Info ]\r\n");                                                  //출력
            sb.Append(result);
            sb.Append("\r\n\n");
            sb.Append("[ Error Info ]\r\n");
            sb.Append(error);

            this.metroTextBox28.Text = sb.ToString();

            process.WaitForExit();
            process.Close();
        }

        private void logviewer_Click(object sender, EventArgs e)
        {
            //string folderpath64  = @"C:\Program Files (x86)\Sresolver\log\";
            //string folderpath32 = @"C:\Program Files\Sresolver\log\";
;


            
            DirectoryInfo folderpath64 = new DirectoryInfo("C:\\Program Files (x86)\\sresolver\\log\\");
            DirectoryInfo folderpath32 = new DirectoryInfo("C:\\Program Files\\sresolver\\log\\");

            string filename = "DSRMService_"+DateTime.Today.ToString("yyyy_MM_dd")+".log";
                        
            //Process.Start(folderpath64+filename);

            try
            {
                if (folderpath64.Exists)
                {
                    Process.Start(folderpath64+filename);
                }
                else
                {
                    Process.Start(folderpath32+filename);
                }
            }
            catch
            {
                MessageBox.Show("로그파일이 존재 하지 않습니다.", "로그파일 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
 }


