using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQueryMobileMvc.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            ViewBag.Market = DateTime.Now;
            return View();
        }

        public ActionResult Info()
        {
            ViewBag.Market = DateTime.Now;
            return View();
        }

    }
}
