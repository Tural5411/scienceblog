using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SonsuzBlog.Models;

namespace SonsuzBlog.Areas.AdminPanel.Controllers
{
    public class tbl_postController : Controller
    {
        private Model1 db = new Model1();

        // GET: AdminPanel/tbl_post
        public ActionResult Index()
        {
            var tbl_post = db.tbl_post.Include(t => t.tbl_category).Include(t => t.tbl_users).Include(t => t.tbl_post_type).Include(t => t.tbl_sekil);
            return View(tbl_post.ToList());
        }

        // GET: AdminPanel/tbl_post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_post tbl_post = db.tbl_post.Find(id);
            if (tbl_post == null)
            {
                return HttpNotFound();
            }
            return View(tbl_post);
        }

        // GET: AdminPanel/tbl_post/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.tbl_category, "CategoryId", "Ad");
            ViewBag.MuellifId = new SelectList(db.tbl_users, "MuellifId", "Adi");
            ViewBag.PostTypeId = new SelectList(db.tbl_post_type, "PostTypeId", "Adi");
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil");
            return View();
        }

        // POST: AdminPanel/tbl_post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Baslig,Context,Tarixi,PostTypeId,CategoryId,MuellifId,SekilId,Baxis,Beyen,Aktiv")] tbl_post tbl_post, HttpPostedFileBase Sekil, string etiketler)
        {
            if (ModelState.IsValid)
            {
                if (tbl_post != null)
                {
                    tbl_users aktiv = Session["Istifadeci"] as tbl_users;
                    tbl_post.Tarixi = DateTime.Now;
                    tbl_post.PostTypeId = 1;
                    tbl_post.MuellifId = aktiv.MuellifId;
                    tbl_post.SekilId = SekilYukle(Sekil);
                    db.tbl_post.Add(tbl_post);
                    db.SaveChanges();

                    string[] etikets = etiketler.Split(',');
                    foreach (string etiket in etikets)
                    {
                        tbl_etiket etk = db.tbl_etiket.FirstOrDefault(x => x.Ad.ToLower() == etiket.ToLower().Trim());
                        if (etk != null)
                        {
                            etk = new tbl_etiket();
                            etk.Ad = etiket;
                            db.tbl_etiket.Add(etk);
                            db.SaveChanges();

                        }
                        tbl_post.tbl_etiket.Add(etk);
                        db.SaveChanges();
                    }

                }

                db.tbl_post.Add(tbl_post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.tbl_category, "CategoryId", "Ad", tbl_post.CategoryId);
            ViewBag.MuellifId = new SelectList(db.tbl_users, "MuellifId", "Adi", tbl_post.MuellifId);
            ViewBag.PostTypeId = new SelectList(db.tbl_post_type, "PostTypeId", "Adi", tbl_post.PostTypeId);
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_post.SekilId);
            return View(tbl_post);
        }

        private int SekilYukle(HttpPostedFileBase sekil)
        {
            int balacaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
            int balacaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);
            int ortaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
            int ortaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
            int boyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bow"]);
            int boyukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["boh"]);

            string newName = Path.GetFileNameWithoutExtension(sekil.FileName) + "" + Guid.NewGuid() + Path.GetExtension(sekil.FileName);
            Image Photo = Image.FromStream(sekil.InputStream);
            Bitmap balacaSek = new Bitmap(Photo, balacaWidth, balacaHeight);
            Bitmap ortaSek = new Bitmap(Photo, ortaWidth, ortaHeight);
            Bitmap boyukSek = new Bitmap(Photo);

            balacaSek.Save("~/Upload/Sekiller/balaca/" + newName);
            ortaSek.Save("~/Upload/Sekiller/orta/" + newName);
            boyukSek.Save("~/Upload/Sekiller/boyuk/" + newName);

            tbl_users Istfd = (tbl_users)Session["Istifadeci"];
            tbl_sekil dbSekil = new tbl_sekil();
            dbSekil.Balacasekil = "/Upload/Sekiller/balaca/" + newName;
            dbSekil.Ortasekil = "/Upload/Sekiller/orta/" + newName;
            dbSekil.Boyuksekil = "/Upload/Sekiller/boyuk/" + newName;

            dbSekil.ElaveEdenId = Istfd.MuellifId;

            db.tbl_sekil.Add(dbSekil);
            db.SaveChanges();
            return dbSekil.SekilId;

            throw new NotImplementedException();
        }

        // GET: AdminPanel/tbl_post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_post tbl_post = db.tbl_post.Find(id);
            if (tbl_post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.tbl_category, "CategoryId", "Ad", tbl_post.CategoryId);
            ViewBag.MuellifId = new SelectList(db.tbl_users, "MuellifId", "Adi", tbl_post.MuellifId);
            ViewBag.PostTypeId = new SelectList(db.tbl_post_type, "PostTypeId", "Adi", tbl_post.PostTypeId);
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_post.SekilId);
            return View(tbl_post);
        }

        // POST: AdminPanel/tbl_post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Baslig,Context,Tarixi,PostTypeId,CategoryId,MuellifId,SekilId,Baxis,Beyen,Aktiv")] tbl_post tbl_post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.tbl_category, "CategoryId", "Ad", tbl_post.CategoryId);
            ViewBag.MuellifId = new SelectList(db.tbl_users, "MuellifId", "Adi", tbl_post.MuellifId);
            ViewBag.PostTypeId = new SelectList(db.tbl_post_type, "PostTypeId", "Adi", tbl_post.PostTypeId);
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_post.SekilId);
            return View(tbl_post);
        }

        // GET: AdminPanel/tbl_post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_post tbl_post = db.tbl_post.Find(id);
            if (tbl_post == null)
            {
                return HttpNotFound();
            }
            return View(tbl_post);
        }

        // POST: AdminPanel/tbl_post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_post tbl_post = db.tbl_post.Find(id);
            db.tbl_post.Remove(tbl_post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
