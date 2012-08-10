using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Actions
{
    public class WebSiteConsole : AbsAction
    {
        private static WebSiteConsole _obj;
        private static readonly Object LockObj = new object();

        private WebSiteConsole()
        { }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static WebSiteConsole GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            return _obj = new WebSiteConsole();
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
            ConsoleUtility.OutPutMsg(MsgType.消息, "WebSiteConsole");
            if (ConsoleUtility.Format(ref args))
            { ConsoleUtility.OutPutMsg(MsgType.消息, "成功"); return true; }
            else
            { ConsoleUtility.OutPutMsg(MsgType.消息, "失败"); return false; }
        }
    }
}
