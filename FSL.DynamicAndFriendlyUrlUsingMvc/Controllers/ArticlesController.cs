using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSL.DynamicAndFriendlyUrlUsingMvc.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Index(string url, long id)
        {
            ViewBag.Url = url;
            ViewBag.Id = id;

            return View();
        }
    }
}