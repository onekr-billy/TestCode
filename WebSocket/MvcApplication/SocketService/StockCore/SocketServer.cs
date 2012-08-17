using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketService.StockCore
{
    public class SocketServer : AbsSocket
    {
        SocketServer()
        {
        }

        private static SocketServer _obj;
        private static readonly Object LockObj = new object();

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

        /// <summary>
        /// 开始运行
        /// </summary>
        public override void Start()
        {
            if (Port > 0)
            {
                if(SocketObj!=null)
                {
                    SocketObj.Disconnect(true);
                }
                var opHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                IpAddress = (from a in opHostEntry.AddressList where a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork select a).First();
                if (IpAddress != null)
                {
                    var ipe = new IPEndPoint(IpAddress, Port);
                    SocketObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    SocketObj.Bind(ipe);
                    SocketObj.Listen(100);

                    SocketThread = new Thread(() =>
                                                   {
                                                       while (true)
                                                       {
                                                           AccepDone.Reset();
                                                           SocketObj.BeginAccept(AcceptAsyncCallback, SocketObj);
                                                           AccepDone.WaitOne();
                                                       }
                                                   });
                    SocketThread.Start();

                }
            }
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="result"></param>
        public override void AcceptAsyncCallback(IAsyncResult result)
        {
            AccepDone.Set();

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
        public override void ReceiveAsyncCallback(IAsyncResult result)
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

        /// <summary>
        /// 开始发送
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="msg"></param>
        public override void Send(Socket handler, string msg)
        {
            // 消息格式转换.
            var byteData = Encoding.ASCII.GetBytes(msg);
            // 开始发送数据给远程目标.
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendAsyncCallback, handler);
        }

        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="result"></param>
        public override void SendAsyncCallback(IAsyncResult result)
        {
            var handler = (Socket)result.AsyncState;
            var bytesSended = handler.EndSend(result);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }


        public override void ConnectCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
