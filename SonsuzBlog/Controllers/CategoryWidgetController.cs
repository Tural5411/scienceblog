using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SonsuzBlog.Models;

namespace SonsuzBlog.Controllers
{
    public class CategoryWidgetController : Controller
    {
        Model1 db = new Model1();
        // GET: CategoryWidget


        [Route("Category/{KateqoriAd}-{id:int}")]
        public ActionResult Index(int id)
        {
            ViewBag.catImg = db.tbl_category.FirstOrDefault(x => x.CategoryId == id).tbl_sekil.Boyuk;
            ViewBag.catAd = db.tbl_category.FirstOrDefault(x => x.CategoryId == id).Ad ;
            return View(id); 
        }


        public ActionResult PostList(int id)
        {
            
            var data = db.tbl_post.OrderByDescending(x=>x.PostId).Where(x => x.CategoryId == id && x.QebulEdildi==true).ToList();
            return View("PostList",data); 
        }
        
        public ActionResult CategoryWidgetGetir()
        {
            return View(db.tbl_category.ToList());
        }
        public ActionResult CategoryWidgetNumber()
        {
            return View(db.tbl_category.OrderByDescending(x=>x.tbl_post.Count).Take(5).ToList());
        }

        
        public ActionResult PostListCat(int id)
        {
            
            var data = db.tbl_post.OrderByDescending(x=>x.PostId).Where(x => x.CategoryId == id && x.QebulEdildi==true).ToList();
            ViewBag.post = db.tbl_category.First();
            return View("PostListCat", data);
        }
        public  ActionResult BreakingNews()
        {
            return View(db.tbl_post.OrderByDescending(x => x.Tarixi).Take(3).ToList());
        }

        public ActionResult KategorySlider()
        {
            return View(db.tbl_category.ToList());
        }
    }
}