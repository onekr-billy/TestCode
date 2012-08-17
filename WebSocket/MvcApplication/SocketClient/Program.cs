using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using SocketService.StockCore;
using System.Threading;

namespace SocketClient
{
    public class Program
    {
        private static ManualResetEvent startDone = new ManualResetEvent(false);

        [STAThread]
        public static void Main(string[] args)
        {

            Console.Read();

        }


    }
}
