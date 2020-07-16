using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using SonsuzBlog.Models;
using SonsuzBlog.ViewModel;

namespace SonsuzBlog.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        // GET: Home
        [Route("")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            PostVm VM = new PostVm();
            return View(VM);
        }
        public ActionResult AllPostList()
        {
            var postlar = db.tbl_post.OrderByDescending(x => x.PostId).Where(x => x.QebulEdildi == true).ToList();
            return View("PostList", postlar);
        }

        [Route("Elaqe")]
        public ActionResult Elaqe()
        {
            return View();
        }

        public ActionResult Navbar()
        {
            PostVm VM = new PostVm();

            VM.categories = db.tbl_category.ToList();
            VM.users = db.tbl_users.Where(x => x.Yazar == true && x.QebulEdildi == true).ToList();
            return PartialView(VM);
        }

        [HttpPost]
        public ActionResult Elaqe(string ad = null, string email = null, string baslig = null, string metn = null)
        {
            if (ad != null && email != null && metn != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "turik541129@gmail.com";
                WebMail.Password = "dilman99";
                WebMail.SmtpPort = 587;
                WebMail.Send("turik541129@gmail.com", baslig, email + "-" + metn);
                TempData["elaqe"] = "Mesajiniz ugurla gonderildi";
            }
            else
            {
                TempData["elaqe"] = "Xəta baş verdi ... Təkrar yoxlayın";

            }
            return View();
        }

        public ActionResult PostAxtar(string txtAxtar)
        {
            if (txtAxtar != null)
            {
                var data = db.tbl_post.Where(a => a.Baslig.Contains(txtAxtar) || a.Ozet.Contains(txtAxtar))
                .OrderByDescending(v => v.Tarixi).ToList();
                return View(data);
            }
            return View();
            
        }

        public ActionResult PostAxtarPartial()
        {
            return View();
        }

        public ActionResult HavaPraqnozu()
        {
            //string api = "85f4564614ea43a4588f7f6a2465d300";
            string baglanti = "api.openweathermap.org/data/2.5/weather?q={Baku}& appid ={85f4564614ea43a4588f7f6a2465d300}";

            
            XDocument Hava = XDocument.Load(baglanti);
            var istilik = Hava.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            var icon = Hava.Descendants("weather").ElementAt(0).Attribute("icon").Value;
            var durum = Hava.Descendants("weather").ElementAt(0).Attribute("value").Value;
            ViewBag.icon = "http://openweathermap.org/img/w/" + icon + ".png";
            ViewBag.istilik = istilik + " ºC";
            ViewBag.durum = durum;
            return View();
        }

        [Route("Qaydalar")]
        public ActionResult Qaydalar()
        {
            return View();
        }
        [Route("Haqqimizda")]
        public ActionResult Haqqimizda()
        {
            return View();
        }

        public ActionResult AllWidgets()
        {
            return View();
        }

    }
}