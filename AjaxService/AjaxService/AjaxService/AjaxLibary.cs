using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxService
{
    public class AjaxLibary
    {
        public static HttpContext CurrentContext
        {
            get { return HttpContext.Current; }
        }

        [AjaxAttribute(EnableAjax = true)]
        public static string GetId(string id, string name)
        {
            return id + "-" + name;
        }

        [AjaxAttribute(EnableAjax = true)]
        public static string SaveId(string id)
        {
            return "哈哈" + id;
        }

    }
}