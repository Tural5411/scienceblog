using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SonsuzBlog.Areas.AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        Model1 db = new Model1();      
        // GET: AdminPanel/Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult YazarAktivlesdir()
        {
            
            var data = db.tbl_users.OrderByDescending(x=>x.UserId).Where(x=>x.Yazar== true && x.QebulEdildi==false).ToList();
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult TesdiqEle(tbl_users istfd,int id)
        {
            tbl_users usr = db.tbl_users.FirstOrDefault(x =>x.UserId == id);
            usr.QebulEdildi = true;
            TempData["Info"] = "İstifadəçi uğurlu şəkildə müəllif oldu";
            db.SaveChanges();

            return RedirectToAction("YazarAktivlesdir");
        }

        public ActionResult MeqaleAktivlesdir()
        {
            var data = db.tbl_post.OrderByDescending(x=>x.PostId).Where(x => x.QebulEdildi == false).ToList();
            return View(data);
        }

        public ActionResult MeqaleTesdiqle(tbl_post post , int id)
        {
            tbl_post pst = db.tbl_post.FirstOrDefault(x => x.PostId==id);
            pst.QebulEdildi = true;
            TempData["InfoM"] = "Məqalə uğurlu şəkildə əlavə oldu";
            db.SaveChanges();

            return RedirectToAction("MeqaleAktivlesdir");
        }

        public ActionResult Istifadeciler()
        {
            return View(db.tbl_users.OrderByDescending(x=>x.UserId).Where(x=>x.QebulEdildi==true).ToList());
        }
        public ActionResult IstifadeciEdit(int id)
        {
            if (id != null)
            {
                var a = db.tbl_users.Where(x => x.UserId == id).SingleOrDefault();
                return View(a);
            }
            return HttpNotFound();
            
        }

        [HttpPost]
        public ActionResult IstifadeciEdit(int id ,tbl_users user,string Sifre,string Mail,string Login)
        {
            if (ModelState.IsValid)
            {
                var a = db.tbl_users.Where(x => x.UserId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(Sifre,"MD5");
                a.Mail = Mail;
                a.Login = Login;
                a.Yazar = user.Yazar;
                db.SaveChanges();
                RedirectToAction("Istifadeciler","Admin");
            }

            return View(user);
        }
        
    }


}