using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQueryMobileMvc
{
    public class WebPageUtility
    {
        /// <summary>
        /// 获取页面Id
        /// </summary>
        /// <returns></returns>
        public static string GetPageId(object objPageId)
        {
            var pageId = objPageId is String ? objPageId.ToString() : "";
            if (!string.IsNullOrEmpty(pageId))
                return pageId;
            else
            {
                var httpContext = HttpContext.Current;
                var url = httpContext.Request.Url.PathAndQuery.Trim('/').Replace('/', '_');
                return url + "_Page";
            }
        }
    }
}