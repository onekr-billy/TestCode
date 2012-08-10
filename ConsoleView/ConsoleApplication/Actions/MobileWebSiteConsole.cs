using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Actions
{
    public class MobileWebSiteConsole : AbsAction
    {
        private static MobileWebSiteConsole _obj;
        private static readonly Object LockObj = new object();

        private MobileWebSiteConsole()
        { }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static MobileWebSiteConsole GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            return _obj = new MobileWebSiteConsole();
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
            ConsoleUtility.OutPutMsg(MsgType.消息, "MobileWebSiteConsole");
            if (ConsoleUtility.Format(ref args))
            { ConsoleUtility.OutPutMsg(MsgType.消息, "成功"); return true; }
            else
            { ConsoleUtility.OutPutMsg(MsgType.消息, "失败"); return false; }
        }
    }
}
