using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication.Actions;
using System.IO;

namespace ConsoleApplication
{
    public static class ConsoleUtility
    {
        /// <summary>
        /// 命令工厂
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool CmdFactory(string[] args)
        {
            var isReset = false;
            if (args.Length > 0)
            {
                var actionName = args[0].ToUpper();
                switch (actionName)
                {
                    case "INIT":
                        {
                            isReset = true;
                        } break;
                    case "EXIT":
                        {
                            return false;
                        } break;
                    case "TAOBAO":
                        {
                            isReset = true;
                            TaoBaoConsole.GetInstance.DoExecute(args);
                        } break;
                    case "WEBSITE":
                        {
                            isReset = true;
                            WebSiteConsole.GetInstance.DoExecute(args);

                        } break;
                    case "MOBILEWEBSITE":
                        {
                            isReset = true;
                            MobileWebSiteConsole.GetInstance.DoExecute(args);
                        } break;
                    default:
                        {
                            OutPutMsg(MsgType.消息, "指令不存在！");
                            isReset = true;
                        }
                        break;
                }
                Reset();
            }
            else
            {
                OutPutMsg(MsgType.错误, "请输入参数，如果需要退出请输入 Exit + 回车！");
                Reset();
            }
            return isReset;
        }

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void OutPutMsg(MsgType msgType, string msg, params object[] args)
        {
            var msgTxt = string.Format(string.Concat("[", msgType.ToString(), "][", DateTime.Now, "]", msg), args);
            Console.Out.WriteLine(msgTxt);
            var logFile = string.Concat(msgType.ToString(), "_", DateTime.Now.ToString("yyyyMMddHHmm"), ".log");
            WriteLog(logFile, msgTxt);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void WriteLog(string fileName, string content)
        {
            using (var streamWrite = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                streamWrite.WriteLine(content);
                streamWrite.Flush();
                streamWrite.Close();
                streamWrite.Dispose();
            }
        }


        /// <summary>
        /// 复位到 入口状态
        /// </summary>
        public static void Reset()
        {
            var cmds = (Console.ReadLine() ?? "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            CmdFactory(cmds);
        }

        /// <summary>
        /// 参数格式化，如果返回true，则参数合法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Format(ref string[] args)
        {
            if (args != null && args.Length > 0)
                return true;
            else
                return false;
        }

    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        消息,
        调试,
        错误
    }
}
