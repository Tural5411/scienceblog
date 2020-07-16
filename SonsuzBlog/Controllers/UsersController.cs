using SonsuzBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace SonsuzBlog.Controllers
{
    public class UsersController : Controller
    {
        Model1 db = new Model1();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(tbl_users usr)
        {
            string rol = ValidateUser(usr.Login, Crypto.Hash(usr.Sifre,"MD5"));
            if (!string.IsNullOrEmpty(rol))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    usr.Login,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    true,
                    rol,
                    FormsAuthentication.FormsCookiePath);

                HttpCookie cuki = new HttpCookie(FormsAuthentication.FormsCookieName);
                if (ticket.IsPersistent)
                {
                    cuki.Expires = ticket.Expiration;
                }

                Response.Cookies.Add(cuki);
                //Session["rol"] = rol;
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(usr.Login, true));
                FormsAuthentication.RedirectFromLoginPage(usr.Login, true);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Giris");
        }

        string ValidateUser(string ua, string pwd)
        {
            tbl_users user = db.tbl_users.FirstOrDefault(x => x.Login == ua && x.Sifre == pwd);

            if (user != null)
                return user.Ad;
            else
            {
                TempData["UserLogin"] = "Şifrə yanlışdır";
                return "";
            }
        }

        public ActionResult Cixis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RememberMe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RememberMe(string email)
        {
            var mail = db.tbl_users.Where(x => x.Mail == email).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();

                tbl_users user = new tbl_users();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre),"MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "turik541129@gmail.com";
                WebMail.Password = "dilman99";
                WebMail.SmtpPort = 587;
                WebMail.Send(email, "Sonsuzbilgi.az İstifadəçi şifrəniz", "Şifrəniz:" + yenisifre);
                TempData["Remember"] = "Mailinizə şifrə göndərildi ...";
            }
            else
            {
                TempData["Remember"] = "Xəta baş verdi...";
            }
            return View();
        }

        public ActionResult IstifadeciOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IstifadeciOl(tbl_users istfd,string Sifre, string rdqadin, string rdkisi)
        {
            if (!string.IsNullOrEmpty(rdqadin))
                istfd.Cinsi = true;
            if (!string.IsNullOrEmpty(rdkisi))
                istfd.Cinsi = false;
            istfd.QeydiyyatTarixi = DateTime.Now;
            istfd.Yazar = false;
            istfd.Sifre = Crypto.Hash(Sifre,"MD5");
            istfd.Aktiv = true;
            istfd.QebulEdildi = true;
            db.tbl_users.Add(istfd);
            db.SaveChanges();
            return RedirectToAction("Giris");
        }

        public ActionResult MuellifOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MuellifOl(tbl_users istfd,string Sifre,string rdqadin,string rdkisi)
        {
            if (!string.IsNullOrEmpty(rdqadin))
                istfd.Cinsi = true;
            if (!string.IsNullOrEmpty(rdkisi))
                istfd.Cinsi = false;
            istfd.QeydiyyatTarixi = DateTime.Now;
            istfd.Yazar = true;
            istfd.Sifre = Crypto.Hash(Sifre,"MD5");
            istfd.QebulEdildi = false;
            istfd.Aktiv = true;
            db.tbl_users.Add(istfd);
            db.SaveChanges();

            tbl_rol yazar = db.tbl_rol.FirstOrDefault(x => x.RolAdi == "Yazar");
            tbl_userrol usrrol = new tbl_userrol();
            usrrol.RolId = yazar.RolId;
            usrrol.UserId = istfd.UserId;
            db.tbl_userrol.Add(usrrol);
            db.SaveChanges();

            return RedirectToAction("Giris");
        }


    }

    
}