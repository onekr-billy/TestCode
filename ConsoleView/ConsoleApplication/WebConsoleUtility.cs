using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Specialized;

namespace ConsoleApplication
{
    public class WebConsoleManager : IDisposable
    {
        private static WebConsoleManager _obj;
        private static readonly Object LockObj = new object();

        private Process _process;
        private ProcessStartInfo _processStartInfo;
        private readonly List<ItemLog> _log = new List<ItemLog>();

        private WebConsoleManager()
        {
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static WebConsoleManager GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new WebConsoleManager();
                return _obj;
            }
        }

        /// <summary>
        /// 程序路径
        /// </summary>
        private string ConsolePath
        {
            get { return string.Concat(AppDomain.CurrentDomain.BaseDirectory, "bin\\", "ConsoleApplication.exe"); }
        }

        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <param name="processWindowStyle"> </param>
        /// <param name="msg"> </param>
        /// <param name="args"></param>
        public void StartConsole(ProcessWindowStyle processWindowStyle, out string msg, params string[] args)
        {
            Dispose();
            try
            {
                _processStartInfo = new ProcessStartInfo()
                                            {
                                                Arguments = args.Length > 0 ? string.Join(" ", args) : "",
                                                FileName = ConsolePath,
                                                RedirectStandardInput = true,
                                                RedirectStandardOutput = true,
                                                CreateNoWindow = false,
                                                UseShellExecute = false,
                                                WindowStyle = processWindowStyle
                                            };

                _process = new Process()
                               {
                                   StartInfo = _processStartInfo,
                                   EnableRaisingEvents = true
                               };

                _process.Exited += ConsoleExitedEvent;
                //_process.WaitForExit();
                _process.Start();

                var asyncLog = new Thread(() =>
                               {
                                   while (_process != null)
                                   {
                                       var reader = _process.StandardOutput;
                                       while (!reader.EndOfStream)
                                       {
                                           if (!string.IsNullOrEmpty(reader.ReadLine()))
                                           {
                                               _log.Add(new ItemLog
                                               {
                                                   Msg = reader.ReadLine(),
                                                   RunTime = DateTime.Now
                                               });
                                           }
                                       }
                                       //Thread.Sleep(500);
                                   }
                               });
                asyncLog.Start();
                msg = "服务已启动!";
            }
            catch(Exception ex)
            {
                msg = ex.ToString();
            }

        }

        /// <summary>
        /// 控制台停止
        /// </summary>
        public void ConsoleStop()
        {
            Dispose();
        }

        /// <summary>
        /// 输入命令
        /// </summary>
        /// <param name="args"></param>
        /// <param name="msg"> </param>
        public void ConsoleInput(string args, out string msg)
        {
            if (_process != null)
            {
                var write = _process.StandardInput;
                write.WriteLine(args);
                msg = "执行成功！";
            }
            else
            {
                msg = "服务没有启动！";
            }
        }

        /// <summary>
        /// 获取输出内容
        /// </summary>
        /// <returns></returns>
        public List<ItemLog> GetConsoleOuput()
        {
            var msg = new List<ItemLog>();
            _log.ForEach(msg.Add);
            _log.Clear();
            return msg;
        }

        /// <summary>
        /// 程序退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleExitedEvent(object sender, EventArgs e)
        {
            Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_process != null)
            {
                try
                {
                    //_process.Close();
                    _process.Kill();
                    _process.Dispose();
                }
                catch
                {
                }
                finally
                {
                    _process = null;
                    _processStartInfo = null;
                    _log.Clear();
                }
            }
        }
    }

    public class ItemLog
    {
        public string Msg { get; set; }
        public DateTime RunTime { get; set; }
    }

}
