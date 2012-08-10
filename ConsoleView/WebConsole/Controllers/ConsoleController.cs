using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ConsoleApplication;
using System.Diagnostics;

namespace WebConsole.Controllers
{
    public class ConsoleController : Controller
    {
        //
        // GET: /Console/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsoleInput(string args)
        {
            string msg;
            WebConsoleManager.GetInstance.ConsoleInput(args, out msg);
            ViewData.Add("msg", msg);
            return View();
        }

        [HttpGet]
        public ActionResult ConsoleOutput()
        {
            var msgSbr = new StringBuilder();
            var msgList = WebConsoleManager.GetInstance.GetConsoleOuput();
            foreach (var itemLog in msgList)
            {
                msgSbr.AppendFormat("{0}\r\n", itemLog.Msg);
            }
            ViewData["Output"] = msgSbr;
            return View();
        }

        [HttpGet]
        public ActionResult ConsoleAction(string act)
        {
            var result = string.Empty;
            var msg = string.Empty;
            if (!string.IsNullOrEmpty(act))
            {
                try
                {
                    switch (act.ToUpper())
                    {
                        case "START":
                            WebConsoleManager.GetInstance.StartConsole(ProcessWindowStyle.Normal, out msg);
                            result = "成功启动";
                            break;
                        case "STOP":
                            WebConsoleManager.GetInstance.ConsoleStop();
                            result = "成功停止";
                            break;
                    }
                }
                catch
                {
                }
                finally
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "命令不存在！";
                    }
                }
            }
            ViewData.Add("msg", msg);
            return View();
        }

    }
}
