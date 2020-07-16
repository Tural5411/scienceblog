using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SonsuzBlog.Models;

namespace SonsuzBlog.Controllers
{
    public class PopularWidgetController : Controller
    {
        Model1 db = new Model1();
        // GET: PopularWidget
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PopularWidgetGetir()
        {
            return View(db.tbl_post.OrderByDescending(x => x.Baxis).Take(3).ToList());
        }
    }
}