using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public abstract class AbsAction
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 命令参数
        /// </summary>
        public string[] ActionParams { get; set; }
        /// <summary>
        /// 命令历史记录
        /// </summary>
        public Dictionary<string, DateTime> CmdHistory { get; set; }

        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool DoExecute(string[] args);

    }

    

}
