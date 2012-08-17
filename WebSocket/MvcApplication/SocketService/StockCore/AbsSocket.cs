using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketService.StockCore
{
    public abstract class AbsSocket
    {
        /// <summary>
        /// 连接标记
        /// </summary>
        protected static readonly ManualResetEvent ConnectDone = new ManualResetEvent(false);
        /// <summary>
        /// 发送标记
        /// </summary>
        protected static readonly ManualResetEvent SendDone = new ManualResetEvent(false);
        /// <summary>
        /// 接受标记
        /// </summary>
        protected static readonly ManualResetEvent AccepDone = new ManualResetEvent(false);
        /// <summary>
        /// 收到标记
        /// </summary>
        protected static readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; protected set; }
        /// <summary>
        /// ip 地址 对象
        /// </summary>
        protected IPAddress IpAddress { get; set; }
        /// <summary>
        /// socket 对象
        /// </summary>
        protected Socket SocketObj { get; set; }
        /// <summary>
        /// socket 线程
        /// </summary>
        protected Thread SocketThread { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        protected AbsSocket()
        {

        }

        /// <summary>
        /// 启动 
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// 停止
        /// </summary>
        public abstract void Stop();
        /// <summary>
        /// 连接回调函数
        /// </summary>
        /// <param name="result"></param>
        public abstract void ConnectCallback(IAsyncResult result);
        /// <summary>
        /// 接受信息回调函数
        /// </summary>
        /// <param name="result"></param>
        public abstract void AcceptAsyncCallback(IAsyncResult result);
        /// <summary>
        /// 收到信息回调函数
        /// </summary>
        /// <param name="result"></param>
        public abstract void ReceiveAsyncCallback(IAsyncResult result);
        /// <summary>
        /// 开始发送信息
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="msg"></param>
        public abstract void Send(Socket handler, string msg);
        /// <summary>
        /// 发送信息回调函数
        /// </summary>
        /// <param name="result"></param>
        public abstract void SendAsyncCallback(IAsyncResult result);
    }
}
