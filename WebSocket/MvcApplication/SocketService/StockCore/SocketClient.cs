using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketService.StockCore
{
    public class SocketClient : AbsSocket
    {
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

        private IPAddress _ipAddress;
        private Socket _socketServer;
        private Thread _socketThread;

        public override void Start()
        {
            var ipHostInfo = Dns.Resolve("169.254.105.250");
            var ipAddress = ipHostInfo.AddressList[0];
            var remoteEp = new IPEndPoint(ipAddress, 8090);
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _socketServer.BeginConnect(remoteEp, ConnectCallback, _socketServer);
            ConnectDone.WaitOne();

            Send("abc");

            Receive(_socketServer);
            ReceiveDone.WaitOne();

            _socketServer.Shutdown(SocketShutdown.Both);
            _socketServer.Close();
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
            _socketServer.BeginSend(byteData, 0, byteData.Length, 0, SendAsyncCallback, _socketServer);

            SendDone.WaitOne();
        }

        public override void SendAsyncCallback(IAsyncResult ar)
        {

            // 从state对象中获取socket  
            var client = (Socket)ar.AsyncState;

            // 完成数据发送.  
            var bytesSent = client.EndSend(ar);

            // 指示数据已经发送完成，主线程继续.  
            SendDone.Set();
        }

        public override void ConnectCallback(IAsyncResult result)
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


        public override void AcceptAsyncCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override void ReceiveAsyncCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override void Send(Socket handler, string msg)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
