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
using BorjesLIA.ViewModel;
using System.Threading.Tasks;

namespace BorjesLIA.AdminControllers
{
    public class DieselQuarterPriceModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DieselQuarterPriceModels
        public ActionResult Index(DieselViewModel dieselQ)
        {
            dieselQ = new DieselViewModel
            {
                AddQuarterDiesel = new DieselQuarterPriceModel(),
                newQuarterDieselList = db.DieselPriceQuarter.OrderByDescending(x => x.Quarter)
            };
            return View(dieselQ);
        }
        public ActionResult ShowView(DieselViewModel dieselQ)
        {
            dieselQ = new DieselViewModel
            {
                AddQuarterDiesel = new DieselQuarterPriceModel(),
                newQuarterDieselList = db.DieselPriceQuarter.OrderByDescending(x => x.Quarter)
            };
            return View(dieselQ);
        }
        public ActionResult _QuarterPriceDiesel(DieselViewModel dqpData)
        {
            dqpData = new DieselViewModel
            {
                newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.Year)
            };
            return View(dqpData);
        }
        public ActionResult _DieselQuarterGraph(DieselViewModel dvm)
        {
            dvm = new DieselViewModel
            {
                newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.ID)
            };
            return View(dvm);
        }
        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(DieselViewModel dieselQuarterChart)
        {
            var data = await dieselQuarterChart.GetQuaerterData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddQuarterDiesel(DieselViewModel newQDiesel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    db.DieselPriceQuarter.Add(newQDiesel.AddQuarterDiesel);
                    db.SaveChanges();
                    newQDiesel.newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.Quarter);
                    //var getNewChart = newEuro.GetData();
                    //return PartialView("QuarterPriceDiesel", newQDiesel);
                    return PartialView("ShowView", newQDiesel);
                }
            }
            else
            {
                return View(newQDiesel);
            }
        }


        // GET: DieselQuarterPriceModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselQuarterPriceModel dieselQuarterPriceModel = db.DieselPriceQuarter.Find(id);
            if (dieselQuarterPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselQuarterPriceModel);
        }

        // GET: DieselQuarterPriceModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DieselQuarterPriceModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Year,Quarter,DieselWeekValue,LoggDate")] DieselQuarterPriceModel dieselQuarterPriceModel)
        {
            if (ModelState.IsValid)
            {
                db.DieselPriceQuarter.Add(dieselQuarterPriceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dieselQuarterPriceModel);
        }

        // GET: DieselQuarterPriceModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselQuarterPriceModel dieselQuarterPriceModel = db.DieselPriceQuarter.Find(id);
            if (dieselQuarterPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselQuarterPriceModel);
        }

        // POST: DieselQuarterPriceModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Year,Quarter,DieselWeekValue,LoggDate")] DieselQuarterPriceModel dieselQuarterPriceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieselQuarterPriceModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dieselQuarterPriceModel);
        }

        // GET: DieselQuarterPriceModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselQuarterPriceModel dieselQuarterPriceModel = db.DieselPriceQuarter.Find(id);
            if (dieselQuarterPriceModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselQuarterPriceModel);
        }

        // POST: DieselQuarterPriceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DieselQuarterPriceModel dieselQuarterPriceModel = db.DieselPriceQuarter.Find(id);
            db.DieselPriceQuarter.Remove(dieselQuarterPriceModel);
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
