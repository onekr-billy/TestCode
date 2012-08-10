using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WcfHostConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            WcfService.Open();

            Console.WriteLine((CommunicationState)WcfService.Host.State);
            Console.ReadLine();
            WcfService.Close();
        }
    }
}
