using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SocketService.StockCore;

namespace SocketService
{
    public partial class ServerForm : Form
    {
        public delegate void ControlDelegate(string logTxt);

        public Thread SocketThread { get; set; }

        public ServerForm()
        {
            InitializeComponent();
            this.notifyIcon1.Text = "我是Socket服务啊！";
            this.notifyIcon1.Icon = new Icon(@"E:\Project\app.ico");
            this.notifyIcon1.Visible = true;
        }

        private void NotifyIconClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void BtnStartClick(object sender, EventArgs e)
        {
            SocketServer.GetInstance.Port = 8090;
            SocketServer.GetInstance.Run();
            /*
            SocketThread = new Thread(() =>
                           {
                               while (true)
                               {
                                   Thread.Sleep(1000);
                                   var log = string.Format("请求时间{0}\r\n", DateTime.Now.ToString());
                                   if (this.txt_log.InvokeRequired)
                                   {
                                       var delgateSetLog = new ControlDelegate(SetLog);
                                       this.Invoke(delgateSetLog, log);
                                   }
                                   else
                                   {
                                       SetLog(log);
                                   }
                               }

                           });
            SocketThread.IsBackground = true;
            SocketThread.Start();
             */
        }

        public void SetLog(string logTxt)
        {
            this.txt_log.Items.Add(logTxt);
        }

        private void BtnStopClick(object sender, EventArgs e)
        {
            if (SocketThread.ThreadState == ThreadState.Running || SocketThread.ThreadState == ThreadState.Background)
            {
                SocketThread.Abort();
            }
        }

        private void btnClientForm_Click(object sender, EventArgs e)
        {
            var clientForm = new ClientForm();
            clientForm.Show();
        }

    }
}
