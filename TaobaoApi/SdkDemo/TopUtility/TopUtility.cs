using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Top.Api;
using Top.Api.Request;
using TopCore.DataAccess;
using System.Data.SQLite;

namespace TopCore
{
    public class TopUtility
    {
        /// <summary>
        /// 处理 授权 回调
        /// </summary>
        public static bool AuthCallBack(bool isRedirect)
        {
            var result = false;
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                var urlParams = httpContext.Request.QueryString;
                var encoding = Encoding.UTF8;
                var topParameters = encoding.GetString(Convert.FromBase64String(urlParams.Get("top_parameters") ?? ""));
                var topParams = HttpUtility.ParseQueryString(topParameters);

                var tc = new TopConfigs
                {
                    SessionKey = urlParams.Get("top_session"),
                    TopCallBackData = urlParams,
                    TopCallBackParams = topParams,
                    RefreshToken = topParams.Get("refresh_token")
                };

                #region 更新数据库

                #region 原生方法

                using (var dbHelper = new SqliteHelper())
                {
                    var queryTxt = "select * from users where u_top_appkey=@appkey";
                    var sqlParameters = new[] { new SQLiteParameter("@appkey", tc.AppKey) };
                    var topInfo = dbHelper.ExecuteList(queryTxt, sqlParameters);
                    if (topInfo != null && topInfo.Count > 0)
                    {
                        queryTxt = "update users set U_Top_SessionKey=@SessionKey,U_LastUpdateTime=@LastupdateTime,U_Top_CallbackData=@U_Top_CallbackData where u_top_appkey=@appkey";
                        sqlParameters = new[]
                                            {
                                                new SQLiteParameter("@appkey", tc.AppKey),
                                                new SQLiteParameter("@SessionKey", tc.SessionKey),
                                                new SQLiteParameter("@U_Top_CallbackData", tc.TopCallBackData),
                                                new SQLiteParameter("@LastupdateTime", DateTime.Now)
                                            };
                        dbHelper.ExecuteNonQuery(queryTxt, sqlParameters);
                    }
                    else
                    {
                        queryTxt = @"insert into users(U_Guid,U_Name,U_Password,U_LastUpdateTime,U_Top_AppKey,U_Top_AppSecret,U_Top_SessionKey,U_Top_CallbackData) 
                                    values(@U_guid,@U_Name,@U_Password,@U_LastUpdateTime,@U_Top_AppKey,@U_Top_AppSecret,@U_Top_SessionKey,@U_Top_CallbackData); SELECT @@IDENTITY";
                        sqlParameters = new[]
                                            {
                                                new SQLiteParameter("@U_Guid", Guid.NewGuid().ToString("N")),
                                                new SQLiteParameter("@U_Name", "admin"),
                                                new SQLiteParameter("@U_Password", "admin888"),
                                                new SQLiteParameter("@U_LastUpdateTime", DateTime.Now),
                                                new SQLiteParameter("@U_Top_AppKey", tc.AppKey),
                                                new SQLiteParameter("@U_Top_AppSecret", tc.AppSecret),
                                                new SQLiteParameter("@U_Top_SessionKey", tc.SessionKey),
                                                new SQLiteParameter("@U_Top_CallbackData", tc.TopCallBackData)
                                            };
                        dbHelper.ExecuteScalar(queryTxt, sqlParameters);
                    }

                }

                #endregion

                #region Ef
                /*
                using (var topDb = new TopCore.DataAccess.TopEntities())
                {
                    var queryTxt = from a in topDb.Users
                                   where a.U_Top_AppKey == tc.AppKey && a.U_Top_AppSecret == tc.AppSecret
                                   select a;
                    var topInfo = queryTxt.First();
                    if (topInfo != null)
                    {
                        topInfo.U_Top_SessionKey = tc.SessionKey;
                        topInfo.U_LastUpdateTime = DateTime.Now;
                        topDb.SaveChanges();
                    }
                    else
                    {
                        var topEntity = new User
                        {
                            U_Name = "admin",
                            U_Password = "admin888",
                            U_LastUpdateTime = DateTime.Now,
                            U_Top_AppKey = tc.AppKey,
                            U_Top_AppSecret = tc.AppSecret,
                            U_Top_SessionKey = tc.SessionKey,
                            U_Top_CallbackData = tc.TopCallBackData.ToString()
                        };
                        topDb.AddToUsers(topEntity);
                    }
                }
                */
                #endregion

                #endregion

                result = true;
                if (isRedirect)
                    httpContext.Response.Redirect(TopApiMainUrl);
            }
            return result;
        }

        #region 清除登录状态

        public static void ClearSession()
        {
            MSession.RemoveAll();
        }

        #endregion

        #region 配置参数 不可变

        /// <summary>
        /// 是否沙箱环境
        /// </summary>
        public static bool IsSandbox
        {
            get
            {
                var result = false;
                Boolean.TryParse(MConfig.Get("IsSandbox") ?? "false", out result);
                return result;
            }
        }

        /// <summary>
        /// 授权地址
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string AuthUrl(string appKey)
        {
            if (IsSandbox)
                return String.Concat("http://container.api.tbsandbox.com/container?encode=utf-8&appkey=", appKey);
            else
                return String.Concat("http://container.api.taobao.com/container?encode=utf-8&appkey=", appKey);
        }

        /// <summary>
        /// Api 地址
        /// </summary>
        /// <returns></returns>
        public static string TopApiUrl
        {
            get
            {
                if (IsSandbox)
                    return "http://gw.api.tbsandbox.com/router/rest";
                else
                    return "http://gw.api.taobao.com/router/rest";
            }
        }

        /// <summary>
        /// 淘宝api 入口 授权后默认跳转的地址
        /// </summary>
        /// <returns></returns>
        public static string TopApiMainUrl
        {
            get { return MConfig.Get("TopApiMainUrl"); }
        }

        #endregion
    }
}
