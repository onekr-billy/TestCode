
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopCore;
using TopCore.DataAccess;
using System.Data.SQLite;

namespace TopDemo.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var userName = "admin";
            var tc = new TopConfigs();
            if (string.IsNullOrWhiteSpace(tc.SessionKey))
            {
                using (var sqlDb = new SqliteHelper())
                {
                    const string queryTxt = @"select * from users where U_Name=@U_Name";
                    var userInfo = sqlDb.ExecuteList(queryTxt, new[]
                                                                    {
                                                                        new SQLiteParameter("@U_Name",userName)
                                                                    });
                    if (userInfo != null && userInfo.Count > 0)
                    {
                        var dr = userInfo[0];
                        tc.AppKey = dr["U_Top_AppKey"].ToString();
                        tc.AppSecret = dr["U_Top_AppSecret"].ToString();
                        tc.SessionKey = dr["U_Top_SessionKey"].ToString();
                        tc.TopCallBackData = HttpUtility.ParseQueryString(dr["U_Top_CallbackData"].ToString());
                    }
                }
            }
        }

    }
}
