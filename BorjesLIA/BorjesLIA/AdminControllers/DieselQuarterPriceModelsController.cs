﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // Static name for Settings
        string settingsName = "Dieselpris Kvartal";


        // GET: DieselQuarterPriceModels
        public ActionResult Index(DieselQuarterViewModel dieselQ)
        {
            dieselQ = NewDieselQuarterObject();
            return View(dieselQ);
        }

        /// <summary>
        /// Creats a new DieselQuarterPriceModel object, populates needed lists with data and returns the object
        /// </summary>
        /// <param name="dieselQ"></param>
        /// <returns></returns>
        private DieselQuarterViewModel NewDieselQuarterObject()
        {
            DieselQuarterViewModel dieselQ;
            dieselQ = new DieselQuarterViewModel
            {
                AddQuarterDiesel = new DieselQuarterPriceModel(),
                newQuarterDieselList = db.DieselPriceQuarter.OrderByDescending(x => x.Quarter),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return dieselQ;
        }

        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the View
        /// </summary>
        /// <param name="dieselQ"></param>
        /// <returns></returns>
        //public ActionResult ShowView(DieselQuarterViewModel dieselQ)
        //{
        //    dieselQ = new DieselQuarterViewModel
        //    {
        //        AddQuarterDiesel = new DieselQuarterPriceModel(),
        //        newQuarterDieselList = db.DieselPriceQuarter.OrderByDescending(x => x.Quarter),
        //        //populates list used for determain charttype from Entity Settings
        //        settings = db.Settings.Where(x => x.Name == settingsName)
        //    };
        //    return View(dieselQ);
        //}


        public ActionResult _QuarterPriceDiesel(DieselQuarterViewModel dqpData)
        {
            if(dqpData != null) { 
            dqpData = new DieselQuarterViewModel
            {
                newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.Year),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            }
            return View(dqpData);
        }

        /// <summary>
        /// Instansiates an object for startpage
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public ActionResult _DieselQuarterGraph(DieselQuarterViewModel dvm)
        {
            dvm = new DieselQuarterViewModel
            {
                newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.ID),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return View(dvm);
        }

        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="dieselQuarterChart"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(DieselQuarterViewModel dieselQuarterChart)
        {
            var data = await dieselQuarterChart.GetQuarterData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Creats a new DieselQuarterViewModel object, populates needed lists with data and return a view with data
        /// </summary>
        /// </summary>
        /// <param name="newQDiesel"></param>
        /// <returns></returns>
        public ActionResult _AddQuarterDiesel(DieselQuarterViewModel newQDiesel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    var previousValue = db.DieselPriceQuarter.FirstOrDefault();
                    newQDiesel.AddQuarterDiesel.PlacingOrder = previousValue.PlacingOrder;
                    newQDiesel.AddQuarterDiesel.Active = previousValue.Active;
                    newQDiesel.AddQuarterDiesel.Type = 1.3M;
                    db.DieselPriceQuarter.Add(newQDiesel.AddQuarterDiesel);
                    db.SaveChanges();
                    newQDiesel.newQuarterDieselList = db.DieselPriceQuarter.ToList().OrderByDescending(x => x.Quarter);
                    newQDiesel = NewDieselQuarterObject();
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
