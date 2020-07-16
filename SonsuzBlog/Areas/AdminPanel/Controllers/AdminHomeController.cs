using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SonsuzBlog.Areas.AdminPanel.Controllers
{
    public class AdminHomeController : Controller
    {
        Model1 db = new Model1();
        [Authorize(Roles ="Admin,Yazar")]
        // GET: AdminPanel/AdminHome
        public ActionResult Index()
        {
            ViewBag.bildiris = db.tbl_users.Where(x => x.Yazar == true && x.QebulEdildi == false).Count();
            return View();
        }
    }
}