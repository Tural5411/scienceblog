using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SonsuzBlog.Models;
namespace SonsuzBlog.Controllers
{
    public class MiniSliderWidgetController : Controller
    {
        Model1 db = new Model1();
        // GET: MiniSliderWidget
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MiniSliderWidgetGetir()
        {
            return View(db.tbl_post.OrderBy(x => x.Tarixi).Take(3).ToList());
        }
    }
}