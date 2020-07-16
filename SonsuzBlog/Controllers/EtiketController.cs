using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SonsuzBlog.Controllers
{
    public class EtiketController : Controller
    {
        Model1 db = new Model1();
        // GET: Etiket
        [Route("Etiket/{Ad}-{id=int}")]
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public ActionResult PostList(int id)
        {
            var data = db.tbl_post.OrderByDescending(x=>x.PostId).Where(x => x.tbl_etiket.Any(me => me.EtiketId == id));
            return View("PostList", data);
        }
    }
}