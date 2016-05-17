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
using System.Threading.Tasks;
using BorjesLIA.ViewModel;

namespace BorjesLIA.AdminControllers
{
    public class DieselWeekModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DieselWeekModels
        public ActionResult Index(DieselViewModel dieselW)
        {
            dieselW = new DieselViewModel
            {
                AddWeekDiesel = new DieselWeekModel(),
                newWeekDieselList = db.DieselPriceWeek.OrderByDescending(x => x.Week)
            };
            return View(dieselW);
        }      


        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(DieselViewModel dieselWeekChart)
        {
            var data = await dieselWeekChart.GetWeekData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddWeekDiesel(DieselViewModel newDiesel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    db.DieselPriceWeek.Add(newDiesel.AddWeekDiesel);
                    db.SaveChanges();
                    newDiesel.newWeekDieselList = db.DieselPriceWeek.ToList().OrderByDescending(x => x.Week);
                    //var getNewChart = newEuro.GetData();
                    return PartialView("_DieselWeekList", newDiesel);
                }
            }
            else
            {
                return View(newDiesel);
            }
        }
        

        // GET: DieselWeekModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselWeekModel dieselWeekModel = db.DieselPriceWeek.Find(id);
            if (dieselWeekModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselWeekModel);
        }

        // GET: DieselWeekModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DieselWeekModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Year,Week,DieselWeekValue,loggDate")] DieselWeekModel dieselWeekModel)
        {
            if (ModelState.IsValid)
            {
                db.DieselPriceWeek.Add(dieselWeekModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dieselWeekModel);
        }

        // GET: DieselWeekModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselWeekModel dieselWeekModel = db.DieselPriceWeek.Find(id);
            if (dieselWeekModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselWeekModel);
        }

        // POST: DieselWeekModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Year,Week,DieselWeekValue,loggDate")] DieselWeekModel dieselWeekModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieselWeekModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dieselWeekModel);
        }

        // GET: DieselWeekModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DieselWeekModel dieselWeekModel = db.DieselPriceWeek.Find(id);
            if (dieselWeekModel == null)
            {
                return HttpNotFound();
            }
            return View(dieselWeekModel);
        }

        // POST: DieselWeekModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DieselWeekModel dieselWeekModel = db.DieselPriceWeek.Find(id);
            db.DieselPriceWeek.Remove(dieselWeekModel);
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
