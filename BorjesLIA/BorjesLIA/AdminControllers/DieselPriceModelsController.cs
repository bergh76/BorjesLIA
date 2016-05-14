using System;
using System.Collections.Generic;
using System.Data;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.ViewModel;
using System.Threading.Tasks;
using BorjesLIA.Models.Diesel;

namespace BorjesLIA.AdminControllers
{
    [Authorize]
    public class DieselPriceModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DieselPriceModels
        [HttpGet]
        public ActionResult Index(DieselViewModel dieselV)
        {
            dieselV = new DieselViewModel
            {
                AddDiesel = new DieselPriceModel(),
                newDieselList = db.DieselPriceModels.ToList().OrderByDescending(x => x.Date)
            };
            return View(dieselV);
        }

        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(DieselViewModel dieselChartData)
        {
            var data = await dieselChartData.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddDiesel(DieselViewModel newDiesel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    db.DieselPriceModels.Add(newDiesel.AddDiesel);
                    db.SaveChanges();
                    newDiesel.newDieselList = db.DieselPriceModels.ToList().OrderByDescending(x => x.Date);
                    //var getNewChart = newEuro.GetData();
                    return PartialView("_DieselList", newDiesel);
                }
            }
            else
            {
                return View(newDiesel);
            }
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

        //// GET: DieselPriceModels/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DieselPriceModels/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(DieselPriceModel dieselPriceModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DieselPriceModels.Add(dieselPriceModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dieselPriceModel);
        //}

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
        public ActionResult Edit(DieselPriceModel dieselPriceModel)
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
