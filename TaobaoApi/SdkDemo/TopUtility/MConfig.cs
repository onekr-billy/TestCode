using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace TopCore
{
    public static class MConfig
    {
        static readonly NameValueCollection Config = ConfigurationManager.AppSettings;

        /// <summary>
        /// 获取配置文件 AppSettings 的 Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            var result = string.Empty;

            try
            {
                if (Config.AllKeys.Contains(key))
                    result = Config.Get(key);
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;
        }

    }
}
