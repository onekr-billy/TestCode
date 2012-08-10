using System.Web;
using Framework.Tools;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using WebApplication.ServiceReference;
using System.ServiceModel;
using System;

namespace WebApplication
{
    /// <summary>
    /// WebApi 的摘要说明
    /// </summary>
    public class WebApi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var _method = HttpHelper.GetParam<string>("method");
            var _callback = HttpHelper.GetParam<string>("callback");
            MethodInfo methodInfo = this.GetType().GetMethod(_method);
            try
            {
                var result = methodInfo.Invoke(this, null).ToString();
                if (!string.IsNullOrEmpty(_callback))
                    HttpContext.Current.Response.Write(string.Format("{0}({1});", _callback, result));
                else
                    HttpContext.Current.Response.Write(result);
            }
            catch (Exception ex)
            {
                ex.Source = string.Format("{0} -> {1}", "调用 Ajax 出现异常", ex);
                throw ex;
            }
        }

        public string ToJson<T>(T item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T JsonTo<T>(string str) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                return serializer.ReadObject(ms) as T;
            }
        }

        public string GetData()
        {
            object result;
            var val = HttpHelper.GetParam<string>("str");

            // 1
            //result = WcfChannelFactory.ExecuteMetod<IService1>("http://localhost:2524/Service.svc/web", "GetData", val);

            // 2
            using (var sc = new Service1Client())
            {
                sc.Open();
                result = sc.GetData(val);
                
                sc.Close();
            }

            return ToJson(result);
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