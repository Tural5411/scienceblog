using SonsuzBlog.Models;
using SonsuzBlog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SonsuzBlog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        Model1 db = new Model1();
        // GET: Post
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Post/{baslig}-{id:int}")]
        public ActionResult PostDetail(int id)
        {
            PostVm VM = new PostVm();
            //tbl_post.SkipWhile(x => x != NextOfThisValue).Skip(1).DefaultIfEmpty(YourList[0]).FirstOrDefault();
            ViewBag.Istifadeci = Session["Istifadeci"];
            VM.postsingle = db.tbl_post.FirstOrDefault(x => x.PostId == id);
            return View(VM);
        }

        [AllowAnonymous]
        public ActionResult RelatedPost(int? id)
        {
            PostVm VM = new PostVm();
            if (id != null)
            {
                VM.posts = db.tbl_post.Where(x => x.CategoryId == id && x.QebulEdildi==true).Take(3).ToList();
                
            }
            return PartialView(VM);
        }

        [AllowAnonymous]
        public ActionResult RelatedTag(int? id)
        {
            PostVm VM = new PostVm();
            if (id != null)
            {
                VM.posts = db.tbl_post.Where(x=>x.QebulEdildi==true).Where(x => x.tbl_etiket.Any(xm => xm.EtiketId == id)).Take(3).ToList();
            }
            return PartialView(VM);
        }

        //[AllowAnonymous]
        //public ActionResult EvvelkiPost()
        //{
        //    var oncekimakalem = db.tbl_post.OrderByDescending(a => a.PostId).FirstOrDefault(a => a.PostId < a.PostId);
        //    Proje.Models.Veritabanı sonrakimakalem = veriler.Makales.FirstOrDefault(a => a.MakaleID > Model.MakaleID);
        //}
        [AllowAnonymous]
        public ActionResult CommentYaz(tbl_comment Comment)
        {
            Comment.Tarixi = DateTime.Now;
            //Comment.Baslig = "";
            //Comment.Aktiv = false;
            db.tbl_comment.Add(Comment);
            db.SaveChanges();
            return RedirectToAction("PostDetail", new { id = Comment.PostId });
        }
        [AllowAnonymous]
        public string PostBeyen(int id)
        {
            tbl_post pst = db.tbl_post.FirstOrDefault(x => x.PostId == id);
            pst.Beyenme++;
            db.SaveChanges();
            return pst.Beyenme.ToString();
        }
       
        [AllowAnonymous]
        public void Baxildi(int id)
        {
            tbl_post pst = db.tbl_post.FirstOrDefault(x => x.PostId == id);
            pst.Baxis++;
            db.SaveChanges();

        }

    }
}