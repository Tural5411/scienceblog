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
    public class tbl_usersController : Controller
    {
        private Model1 db = new Model1();

        // GET: AdminPanel/tbl_users
        public ActionResult Index()
        {
            var tbl_users = db.tbl_users.Include(t => t.tbl_sekil);
            return View(tbl_users.ToList());
        }

        // GET: AdminPanel/tbl_users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // GET: AdminPanel/tbl_users/Create
        public ActionResult Create()
        {
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil");
            return View();
        }

        // POST: AdminPanel/tbl_users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MuellifId,Adi,Soyadi,Mail,Tarixi,Nick,SekilId,Aktiv,Muellifmi,Haqqinda")] tbl_users tbl_users,HttpPostedFileBase Sekil)
        {
            if (ModelState.IsValid)
            {
                tbl_users.SekilId = SekilYukle(Sekil);
                tbl_users.MuellifId = Guid.NewGuid();
                tbl_users.Tarixi = DateTime.Now;
                db.tbl_users.Add(tbl_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_users.SekilId);
            return View(tbl_users);
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
            Bitmap boyukSek = new Bitmap(Photo, boyukWidth, boyukHeight);

            //balacaSek.Save("~/Upload/Sekiller/balaca/" + newName);
            //ortaSek.Save("~/Upload/Sekiller/orta/" + newName);
            //boyukSek.Save("~/Upload/Sekiller/boyuk/" + newName);

            tbl_users Istfd = (tbl_users)Session["Istifadeci"];
            tbl_sekil dbSekil = new tbl_sekil();
            dbSekil.Balacasekil = "/Upload/Sekiller/balaca/" + newName;
            dbSekil.Ortasekil = "/Upload/Sekiller/orta/" + newName;
            dbSekil.Boyuksekil = "/Upload/Sekiller/boyuk/" + newName;

            //dbSekil.ElaveEdenId = Istfd.MuellifId;

            db.tbl_sekil.Add(dbSekil);
            db.SaveChanges();
            return dbSekil.SekilId;

            throw new NotImplementedException();
        }

        // GET: AdminPanel/tbl_users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_users.SekilId);
            return View(tbl_users);
        }

        // POST: AdminPanel/tbl_users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MuellifId,Adi,Soyadi,Mail,Tarixi,Nick,SekilId,Aktiv,Muellifmi,Haqqinda")] tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SekilId = new SelectList(db.tbl_sekil, "SekilId", "Balacasekil", tbl_users.SekilId);
            return View(tbl_users);
        }

        // GET: AdminPanel/tbl_users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // POST: AdminPanel/tbl_users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tbl_users tbl_users = db.tbl_users.Find(id);
            db.tbl_users.Remove(tbl_users);
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
