using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Img;

namespace BorjesLIA.AdminControllers
{
    [Authorize]
    public class ImgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Imgs
        public ActionResult Index()
        {
            return View(db.Imgs.ToList());
        }

        // GET: Imgs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        // GET: Imgs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Imgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Url,Name,Date,PlacingOrder,Active")] Img img)
        {
            if (ModelState.IsValid)
            {
                db.Imgs.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(img);
        }

        // GET: Imgs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        // POST: Imgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Url,Name,Date,PlacingOrder,Active")] Img img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }

        // GET: Imgs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        // POST: Imgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Img img = db.Imgs.Find(id);
            db.Imgs.Remove(img);
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
