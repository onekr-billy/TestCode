using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopDemo.Models;
using TopCore;
using Top.Api;
using Top.Api.Response;
using System.Diagnostics;
using Jayrock.Json;
using Newtonsoft.Json.Linq;


namespace TopDemo.Controllers
{
    public class TopController : BaseController
    {
        //
        // GET: /Top/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auth(AuthModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.RememberMe)
                {
                    Response.Cookies.Add(new HttpCookie("top_appkey", model.AppKey));
                    Response.Cookies.Add(new HttpCookie("top_appsecret", model.AppSecret));
                    Response.Cookies.Add(new HttpCookie("top_rememberme", model.RememberMe.ToString(CultureInfo.InvariantCulture)));
                }

                var tc = new TopConfigs { AppKey = model.AppKey, AppSecret = model.AppSecret };

                var authUrl = TopCore.TopUtility.AuthUrl(model.AppKey);
                Response.Redirect(authUrl);
            }
            else
            {
                var appkey = Request.Cookies.Get("top_appkey") != null ? Request.Cookies.Get("top_appkey").Value : "";
                var appsecret = Request.Cookies.Get("top_appsecret") != null ? Request.Cookies.Get("top_appsecret").Value : "";
                var rememberme = Request.Cookies.Get("top_rememberme") != null ? Request.Cookies.Get("top_rememberme").Value : "";
                if (!string.IsNullOrEmpty(appkey) && !string.IsNullOrEmpty(appsecret) && !string.IsNullOrEmpty(rememberme))
                {
                    var remembermeBool = Boolean.Parse(rememberme);
                    ViewBag.AuthModel = new AuthModel
                                            {
                                                AppKey = appkey,
                                                AppSecret = appsecret,
                                                RememberMe = remembermeBool
                                            };
                }
            }
            if (ViewBag.AuthModel == null)
            {
                ViewBag.AuthModel = new AuthModel
                {
                    AppKey = "",
                    AppSecret = "",
                    RememberMe = false
                };
            }

            return View(ViewBag.AuthModel);
        }

        public ActionResult AuthLogOut()
        {
            TopCore.TopUtility.ClearSession();
            RedirectToAction("Auth", "Top");

            return View();
        }

        public ActionResult UserGet()
        {
            var tConfig = new TopConfigs();

            var request = new Top.Api.Request.UserGetRequest();
            request.Fields = "user_id,uid,nick,sex,buyer_credit,seller_credit,location,created,last_visit,birthday,type";
            var response = tConfig.TopClient(TopCore.Enum.ResultFormat.json).Execute(request, tConfig.SessionKey);

            JObject.Parse(response.Body);

            Response.Write(response.Body);

            return View();
        }

        public ActionResult RefreshToken()
        {
            var tConfig = new TopConfigs();
            Response.Redirect(tConfig.TopApiRefreshTokenUrl);

            return View();
        }

    }
}
