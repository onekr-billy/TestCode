using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web.Security;
using Top.Api;
using Newtonsoft.Json.Linq;
using Top.Api.Util;
using TopCore.Enum;

namespace TopCore
{
    /// <summary>
    /// 淘宝配置
    /// </summary>
    public class TopConfigs
    {
        private ITopClient _topClient;

        #region 用户输入信息，变动
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey
        {
            get
            {
                var sData = MSession.Get("AppKey") as String;
                if (!string.IsNullOrWhiteSpace(sData))
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("AppKey", value); }
        }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret
        {
            get
            {
                var sData = MSession.Get("AppSecret") as String;
                if (!string.IsNullOrWhiteSpace(sData))
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("AppSecret", value); }
        }
        #endregion

        #region 淘宝 返回 数据

        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey
        {
            get
            {
                var sData = MSession.Get("SessionKey") as String;
                if (!string.IsNullOrWhiteSpace(sData))
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("SessionKey", value); }
        }

        /// <summary>
        /// 刷新授权Token
        /// </summary>
        public string RefreshToken
        {
            get
            {
                var sData = MSession.Get("RefreshToken") as String;
                if (!string.IsNullOrWhiteSpace(sData))
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("RefreshToken", value); }
        }

        /// <summary>
        /// top 回调 参数
        /// </summary>
        public NameValueCollection TopCallBackParams
        {
            get
            {
                var sData = MSession.Get("TopCallBackParams") as NameValueCollection;
                if (sData != null)
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("TopCallBackParams", value); }
        }

        /// <summary>
        /// 淘宝 回调 数据
        /// </summary>
        public NameValueCollection TopCallBackData
        {
            get
            {
                var sData = MSession.Get("TopCallBackData") as NameValueCollection;
                if (sData != null)
                    return sData;
                return null;
            }
            set { if (value != null) MSession.Set("TopCallBackData", value); }
        }

        #endregion

        #region 操作方法

        /// <summary>
        /// 刷新授权地址
        /// </summary>
        public string TopApiRefreshTokenUrl
        {
            get
            {
                const string url = "http://container.open.taobao.com/container/refresh?appkey={0}&refresh_token={1}&sessionkey={2}&sign={3}";
                var urlParams = new StringBuilder();
                var urlSign = new StringBuilder();
                var urlParamsDict = new Dictionary<string, string>
                                        {
                                            {"appkey", AppKey},
                                            {"refresh_token", RefreshToken},
                                            {"sessionkey", SessionKey}
                                        };
                
                foreach (var urlParamsItem in urlParamsDict)
                {
                    urlSign.Append(urlParamsItem.Key).Append(urlParamsItem.Value);
                }
                
                var hashPasswordForStoringInConfigFile = FormsAuthentication.HashPasswordForStoringInConfigFile(urlSign.ToString() + AppSecret, "MD5");
                var sign = (hashPasswordForStoringInConfigFile ?? "").ToUpper();

                return string.Format(url,
                    HttpUtility.UrlEncode(AppKey),
                    HttpUtility.UrlEncode(RefreshToken),
                    HttpUtility.UrlEncode(SessionKey),
                    HttpUtility.UrlEncode(sign));
            }
        }

        /// <summary>
        /// 淘宝 客户对象
        /// </summary>
        public ITopClient TopClient(ResultFormat resultFormat)
        {
            return _topClient ??
                   (_topClient = new DefaultTopClient(TopUtility.TopApiUrl, AppKey, AppSecret, resultFormat.ToString()));
        }

        #endregion
    }
}
