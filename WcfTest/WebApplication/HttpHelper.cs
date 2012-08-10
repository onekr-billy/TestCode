using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Framework.Tools
{
    /// <summary>
    /// http 操作类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetParam<T>(string paramName, params T[] def)
        {
            string _val = HttpContext.Current.Request[paramName];
            return CvHelper.To<T>(_val, def);
        }

        /// <summary>
        /// 获取数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static T[] GetParams<T>(string paramName)
        {
            string _val = HttpContext.Current.Request[paramName];
            return CvHelper.ToArray<T>(_val);
        }

    }
}
