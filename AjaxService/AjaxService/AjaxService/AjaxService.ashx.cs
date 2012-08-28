using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Collections.Specialized;

namespace AjaxService
{
    /// <summary>
    /// AjaxService 的摘要说明
    /// </summary>
    public class AjaxService : IHttpHandler
    {
        HttpContext _httpContext;

        public void ProcessRequest(HttpContext context)
        {
            _httpContext = context;
            var result = "";

            MethodInfo methodInfo;
            MethodListInit();
            MethodList.TryGetValue(CurrentActionName, out methodInfo);
            if (methodInfo != null && CurrentActionParams != null && CurrentActionParams.Length > 0)
            {
                if (methodInfo.GetParameters().Count() == CurrentActionParams.Length)
                    result = methodInfo.Invoke(null, CurrentActionParams) as String;
            }
            if (result != null) _httpContext.Response.Write(result);
        }

        #region 公用属性 方法

        /// <summary>
        /// 请求命令名称
        /// </summary>
        public string CurrentActionName
        {
            get
            {
                return string.IsNullOrEmpty(_httpContext.Request["act"])
                           ? null
                           : _httpContext.Request["act"];
            }
        }

        /// <summary>
        /// 请求参数列表
        /// </summary>
        public object[] CurrentActionParams
        {
            get
            {
                var args = string.IsNullOrEmpty(_httpContext.Request["args"])
                         ? null
                         : _httpContext.Request["args"];
                return (args ?? "").Split(new char[] { ',' });
            }
        }

        /// <summary>
        /// 所有方法列表
        /// </summary>
        public Dictionary<string, MethodInfo> MethodList
        {
            get;
            set;
        }

        /// <summary>
        /// 遍历方法初始化
        /// </summary>
        public void MethodListInit()
        {
            MethodList = new Dictionary<string, MethodInfo>();

            var type = typeof(AjaxLibary);
            var methods = type.GetMethods();
            foreach (var methodInfo in methods)
            {
                foreach (var customAttributeData in methodInfo.GetCustomAttributesData())
                {
                    if (customAttributeData.NamedArguments != null)
                        foreach (var customAttributeNamedArgument in customAttributeData.NamedArguments)
                        {
                            if (customAttributeNamedArgument.MemberInfo.Name.Equals("EnableAjax", StringComparison.InvariantCulture))
                            {
                                if ((bool)customAttributeNamedArgument.TypedValue.Value)
                                    MethodList.Add(methodInfo.Name, methodInfo);
                            }
                        }
                }
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}