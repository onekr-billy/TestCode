using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Collections.Specialized;

namespace Ajax
{
    /// <summary>
    /// AjaxProxy 的摘要说明
    /// </summary>
    public class RequestProxy : IHttpHandler
    {
        private static Dictionary<string, string> apiUrlDict = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RequestProxy()
        {
            if (apiUrlDict == null)
            {
                apiUrlDict = new Dictionary<string, string>();
                apiUrlDict.Add("TEST1", "http://localhost:2524/Service.svc/WebGetT");
                apiUrlDict.Add("TEST2", "http://localhost:2524/Service.svc/GetData");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            var _api = context.Request["_api"] ?? "";
            var _url = context.Request["_url"] ?? apiUrlDict[_api.ToUpper()];
            var _method = context.Request["_type"] ?? "GET";
            var _data = context.Request["_data"] ?? "";
            var _contentType = context.Request["_contentType"] ?? "";
            var _callback = context.Request["callback"];
            var _resule = string.Empty;

            if (!string.IsNullOrEmpty(_url))
            {
                FillGetParams(ref _url, context);
                //throw new Exception(_url);
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(_url));
                webRequest.Timeout = 1000 * 60;
                webRequest.Method = _method.ToUpper();

                if (_method.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
                {
                    FillPostParams(ref _data, context, webRequest);
                    byte[] _params = Encoding.ASCII.GetBytes(_data);
                    webRequest.ContentLength = _params.Length;
                    if (!string.IsNullOrEmpty(_contentType))
                        webRequest.ContentType = _contentType;
                    else
                        webRequest.ContentType = "application/x-www-form-urlencoded";
                    using (Stream reqStream = webRequest.GetRequestStream())
                    {
                        reqStream.Write(_params, 0, _params.Length);
                    }
                }
                else if (_method.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
                {

                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Stream streamResponse = webResponse.GetResponseStream();
                        StreamReader streamRead = new StreamReader(streamResponse, System.Text.Encoding.UTF8);
                        _resule = streamRead.ReadToEnd();
                    }
                    else
                        _resule = "调用错误！";
                }
            }

            if (string.IsNullOrEmpty(_callback))
                context.Response.Write(string.Format("{0}", _resule));
            else
                context.Response.Write(string.Format("{0}({1});", _callback, _resule));
        }

        /// <summary>
        /// 填充 Get 参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void FillGetParams(ref string url, HttpContext context)
        {
            var _keys = context.Request.QueryString.AllKeys;
            var _keysLen = _keys.Length;
            var _appendParam = context.Request["_params"] ?? "";
            var _params = new List<string>();
            for (var i = 0; i < _keysLen; i++)
            {
                var _key = _keys[i];
                if (!_key.StartsWith("_") && !_key.Equals("callback", StringComparison.InvariantCultureIgnoreCase))
                    _params.Add(string.Format("{0}={1}", _key, context.Request.QueryString[_key]));
            }

            url = string.Format("{0}{1}?{2}",
                url.TrimEnd(new char[] { '?', '/', ' ' }),
                _appendParam,
                string.Join("&", _params.ToArray()));
        }

        /// <summary>
        /// 填充 Post 参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void FillPostParams(ref string data, HttpContext context, HttpWebRequest webRequest)
        {
            var _keys = context.Request.Form.AllKeys;
            var _keysLen = _keys.Length;
            var _params = new List<string>();
            if ((context.Request["_contentType"] ?? "").Equals("application/json", StringComparison.InvariantCulture))
            {
                
            }
            else
            {
                for (var i = 0; i < _keysLen; i++)
                {
                    var _key = _keys[i];
                    if (!_key.StartsWith("_") && !_key.Equals("callback", StringComparison.InvariantCultureIgnoreCase))
                        _params.Add(string.Format("{0}={1}", _key, context.Request.Form[_key]));
                }
                data = string.Join("&", _params.ToArray());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}