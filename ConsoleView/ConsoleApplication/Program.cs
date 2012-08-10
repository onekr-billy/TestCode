using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication.Actions;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtility.CmdFactory(new[] { "init" });
        }
    }
}
