using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SonsuzBlog.Controllers
{
    public class LatestTrendingController : Controller
    {
        Model1 db = new Model1();
        // GET: LatestTrending
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LatestTrendingGetir()
        {
            ViewBag.Latest = db.tbl_post.Where(x=>x.QebulEdildi==true).OrderByDescending(x => x.Tarixi).Take(9).ToList();
            ViewBag.Trending = db.tbl_post.Where(x => x.QebulEdildi == true).OrderByDescending(x => x.Baxis).Take(9).ToList();
            return View();
        }
    }
}