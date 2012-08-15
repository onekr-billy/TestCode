using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketService.StockCore
{
    public class SocketServer
    {
        private static SocketServer _obj;
        private static readonly Object LockObj = new object();

        SocketServer()
        {
        }

        public static SocketServer GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new SocketServer();
                return _obj;
            }
        }

        private IPAddress _ipAddress;
        private Socket _socketServer;
        private Thread _socketThread;

        public int Port { get; set; }
        public string Server { get; private set; }

        private readonly ManualResetEvent _acceptDone = new ManualResetEvent(false);

        /// <summary>
        /// 开始运行
        /// </summary>
        public void Run()
        {
            if (Port > 0)
            {
                if(_socketServer!=null)
                {
                    _socketServer.Disconnect(true);
                }
                var opHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                _ipAddress = (from a in opHostEntry.AddressList where a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork select a).First();
                if (_ipAddress != null)
                {
                    var ipe = new IPEndPoint(_ipAddress, Port);
                    _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _socketServer.Bind(ipe);
                    _socketServer.Listen(10);

                    _socketThread = new Thread(() =>
                                                   {
                                                       while (true)
                                                       {
                                                           _acceptDone.Reset();
                                                           _socketServer.BeginAccept(AcceptAsyncCallback, _socketServer);
                                                           _acceptDone.WaitOne();
                                                       }
                                                   });
                    _socketThread.Start();

                }
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="result"></param>
        public void AcceptAsyncCallback(IAsyncResult result)
        {
            _acceptDone.Set();

            var listener = (Socket)result.AsyncState;
            var callbackSocket = listener.EndAccept(result);
            var socketState = new SocketState(callbackSocket);

            SocketError errorCode;
            callbackSocket.BeginReceive(socketState.Buffer, 0, socketState.BufferSize, SocketFlags.None, out errorCode,
                                        ReceiveAsyncCallback, socketState);

            if (errorCode != SocketError.Success && errorCode != SocketError.IOPending)
            {

            }

        }

        /// <summary>
        /// 收到数据
        /// </summary>
        /// <param name="result"></param>
        public void ReceiveAsyncCallback(IAsyncResult result)
        {
            var socketState = (SocketState)result.AsyncState;
            var handler = socketState.State;
            var bytesRead = handler.EndReceive(result);
            if (bytesRead > 0)
            {
                if (handler.Available > 0 || handler.Connected)
                {
                    var content = Encoding.ASCII.GetString(socketState.Buffer, 0, bytesRead);
                    socketState.MsgList.Add(
                        new ItemMsg
                        {
                            Created = DateTime.Now,
                            Content = content
                        });

                    //SocketError errorCode;
                    //handler.BeginReceive(socketState.Buffer, 0, socketState.BufferSize, SocketFlags.None, out errorCode,
                    //                     ReceiveAsyncCallback, handler);

                    Send(handler, content);

                }
                else
                {
                    handler.Close();
                }
            }
        }

        public void Send(Socket handler, string msg)
        {
            // 消息格式转换.
            var byteData = Encoding.ASCII.GetBytes(msg);
            // 开始发送数据给远程目标.
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, handler);
        }

        public void SendCallback(IAsyncResult result)
        {
            var handler = (Socket)result.AsyncState;
            var bytesSended = handler.EndSend(result);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }


    }
}
