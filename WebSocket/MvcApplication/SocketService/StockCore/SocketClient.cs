using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketService.StockCore
{
    public class SocketClient
    {
        private static readonly ManualResetEvent ConnectDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent SendDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        SocketClient()
        {
        }

        private static SocketClient _obj;
        private readonly static Object LockObj = new object();

        public static SocketClient GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new SocketClient();
                return _obj;
            }
        }

        private Socket socketClient { get; set; }

        public void Run()
        {
            var ipHostInfo = Dns.Resolve("169.254.105.250");
            var ipAddress = ipHostInfo.AddressList[0];
            var remoteEp = new IPEndPoint(ipAddress, 8090);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketClient.BeginConnect(remoteEp, ConnectCallback, socketClient);
            ConnectDone.WaitOne();

            Send("abc");

            Receive(socketClient);
            ReceiveDone.WaitOne();

            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
        }

        private void Receive(Socket socketClient)
        {
            // 构造容器state.  
            var state = new SocketState(socketClient);

            // 从远程目标接收数据.  
            socketClient.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReceiveCallback, state);
        }

        private void Send(string data)
        {
            // 格式转换.  
            var byteData = Encoding.ASCII.GetBytes(data);

            // 开始发送数据到远程设备.  
            socketClient.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, socketClient);

            SendDone.WaitOne();
        }

        private void SendCallback(IAsyncResult ar)
        {

            // 从state对象中获取socket  
            var client = (Socket)ar.AsyncState;

            // 完成数据发送.  
            var bytesSent = client.EndSend(ar);

            // 指示数据已经发送完成，主线程继续.  
            SendDone.Set();
        }

        private void ConnectCallback(IAsyncResult result)
        {
            // 从state对象获取socket.  
            var client = (Socket)result.AsyncState;
            // 完成连接.  
            client.EndConnect(result);

            // 连接已完成，主线程继续.  
            ConnectDone.Set();
        }

        public void ReceiveCallback(IAsyncResult result)
        {
            // 从输入参数异步state对象中获取state和socket对象  
            var state = (SocketState)result.AsyncState;
            var client = state.State;

            //从远程设备读取数据  
            var bytesRead = client.EndReceive(result);

            if (bytesRead > 0)
            {
                // 继续读取.  
                client.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReceiveCallback, state);
                var msg = Encoding.ASCII.GetString(state.Buffer, 0, state.BufferSize);

            }
            ReceiveDone.Set();
        }

    }
}
