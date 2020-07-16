using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SonsuzBlog.Controllers
{
    public class MuellifController : Controller
    {
        Model1 db = new Model1();
        // GET: Muellif
        
        [Route("Muellif/{Ad}-{id=int}")]
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public ActionResult PostList(int id)
        {
            var data = db.tbl_post.OrderByDescending(x=>x.PostId).Where(x => x.MuellifId == id && x.QebulEdildi==true);
            return View("PostList", data);
        }
    }
}