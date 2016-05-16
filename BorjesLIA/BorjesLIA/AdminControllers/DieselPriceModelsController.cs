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
    [Authorize]
    public class DieselPriceModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DieselPriceModels
        public ActionResult Index()
        {
            return View(db.DieselPriceModels.ToList());
        }

        [AllowAnonymous]
        public JsonResult GetData()
        {
            var data = db.DieselPriceModels.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //ge en view
        public ActionResult DieselPriceGraph()
        {
            return View();
        }

        // GET: DieselPriceModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselPriceModel dieselPriceModel = db.DieselPriceModels.Find(id);
            if (dieselPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselPriceModel);
        }

        // GET: DieselPriceModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DieselPriceModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,dieselPrice,dateNewDieselPrice")] DieselPriceModel dieselPriceModel)
        {
            if (ModelState.IsValid)
            {
                db.DieselPriceModels.Add(dieselPriceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dieselPriceModel);
        }

        // GET: DieselPriceModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselPriceModel dieselPriceModel = db.DieselPriceModels.Find(id);
            if (dieselPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselPriceModel);
        }

        // POST: DieselPriceModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,dieselPrice,dateNewDieselPrice")] DieselPriceModel dieselPriceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieselPriceModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dieselPriceModel);
        }

        // GET: DieselPriceModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselPriceModel dieselPriceModel = db.DieselPriceModels.Find(id);
            if (dieselPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselPriceModel);
        }

        // POST: DieselPriceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DieselPriceModel dieselPriceModel = db.DieselPriceModels.Find(id);
            db.DieselPriceModels.Remove(dieselPriceModel);
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
