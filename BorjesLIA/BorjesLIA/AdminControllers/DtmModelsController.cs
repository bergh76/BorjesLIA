using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Diesel;

namespace BorjesLIA.AdminControllers
{
    public class DtmModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DtmModels
        public ActionResult Index()
        {
            return View(db.DtmModels.ToList());
        }

        // GET: DtmModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DtmModel dtmModel = db.DtmModels.Find(id);
            if (dtmModel == null)
            {
                return HttpNotFound();
            }
            return View(dtmModel);
        }

        // GET: DtmModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DtmModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,dtmPrice,dateNewDtmPrice")] DtmModel dtmModel)
        {
            if (ModelState.IsValid)
            {
                db.DtmModels.Add(dtmModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dtmModel);
        }

        // GET: DtmModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DtmModel dtmModel = db.DtmModels.Find(id);
            if (dtmModel == null)
            {
                return HttpNotFound();
            }
            return View(dtmModel);
        }

        // POST: DtmModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,dtmPrice,dateNewDtmPrice")] DtmModel dtmModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dtmModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dtmModel);
        }

        // GET: DtmModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DtmModel dtmModel = db.DtmModels.Find(id);
            if (dtmModel == null)
            {
                return HttpNotFound();
            }
            return View(dtmModel);
        }

        // POST: DtmModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DtmModel dtmModel = db.DtmModels.Find(id);
            db.DtmModels.Remove(dtmModel);
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
