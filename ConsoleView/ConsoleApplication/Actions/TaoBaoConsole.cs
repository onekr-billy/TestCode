using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication.Actions
{
    public class TaoBaoConsole : AbsAction
    {
        private static TaoBaoConsole _obj;
        private static readonly Object LockObj = new object();

        private TaoBaoConsole()
        { }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static TaoBaoConsole GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            return _obj = new TaoBaoConsole();
                return _obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool DoExecute(string[] args)
        {
            ConsoleUtility.OutPutMsg(MsgType.消息, "TaoBaoConsole");
            if (ConsoleUtility.Format(ref args))
            {
                ConsoleUtility.OutPutMsg(MsgType.消息, "成功"); 
                while(true)
                {
                    Thread.Sleep(1000);
                    ConsoleUtility.OutPutMsg(MsgType.消息, DateTime.Now.ToString());
                }
                return true;
            }
            else
            { ConsoleUtility.OutPutMsg(MsgType.消息, "失败"); return false; }
        }
    }
}
