using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SonsuzBlog.Models;
namespace SonsuzBlog.Controllers
{
    public class SliderController : Controller
    {
        Model1 db = new Model1();
        // GET: Slider
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SliderGetir()
        {
            return View(db.tbl_post.OrderByDescending(x=>x.Beyenme).Take(5).ToList());
        }
    }
}