using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // Static name for Settings
        string settingsName = "Dieselpris Vecka";


        // GET: DieselWeekModels
        public ActionResult Index(DieselWeekViewModel dieselW)
        {
            dieselW = NewDieselWeekObject();
            return View(dieselW);
        }

        /// <summary>
        /// Creats a new DiesleWeekViewModel object, populates needed lists with data and returns the object
        /// </summary>
        /// <param name="dieselW"></param>
        /// <returns></returns>
        private DieselWeekViewModel NewDieselWeekObject()
        {
            DieselWeekViewModel dieselW;
            dieselW = new DieselWeekViewModel
            {
                AddWeekDiesel = new DieselWeekModel(),
                newWeekDieselList = db.DieselPriceWeek.OrderByDescending(x => x.Week),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return dieselW;
        }


        /// <summary>
        ///This ActionResult is not "in play" ie. no functionality connected to the View
        /// </summary>
        /// <param name="dieselWeekChart"></param>
        /// <returns></returns>
        //public ActionResult ShowView(DieselWeekViewModel dieselW)
        //{
        //    dieselW = new DieselWeekViewModel
        //    {
        //        AddWeekDiesel = new DieselWeekModel(),
        //        newWeekDieselList = db.DieselPriceWeek.OrderByDescending(x => x.Week),
        //        //populates list used for determain charttype from Entity Settings
        //        settings = db.Settings.Where(x => x.Name == settingsName)
        //    };
        //    return View(dieselW);
        //}


        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="dieselWeekChart"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> GetData(DieselWeekViewModel dieselWeekChart)
        {
            var data = await dieselWeekChart.GetWeekData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Instansiates an object for startpage
        /// </summary>
        /// <param name="dvm"></param>
        /// <returns></returns>
        public ActionResult _DieselWeekGraph(DieselWeekViewModel dvm)
        {
            dvm = new DieselWeekViewModel
            {
                newWeekDieselList = db.DieselPriceWeek.ToList().OrderByDescending(x => x.ID),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return View(dvm);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult _AddWeekDiesel(DieselWeekModel newDiesel)
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        using (var db = new ApplicationDbContext())
        //        {
        //            db.DieselPriceWeek.Add(newDiesel);
        //            db.SaveChanges();
        //            //newDiesel = db.DieselPriceWeek.ToList().OrderByDescending(x => x.Week);
        //            //var getNewChart = newEuro.GetData();
        //            //ModelState.Clear();
        //            //return PartialView("_WeekPriceDiesel", newDiesel);
        //            return PartialView("ShowView", newDiesel);
        //        }
        //    }
        //    else
        //    {
        //        return View(newDiesel);
        //    }
        //}

        /// <summary>
        /// Creats a new DiesleWeekViewModel object, populates needed lists with data and return a view with data
        /// </summary>
        /// <param name="newDiesel"></param>
        /// <returns></returns>
        public ActionResult _AddWeekDiesel(DieselWeekViewModel newDiesel)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    db.DieselPriceWeek.Add(newDiesel.AddWeekDiesel);
                    db.SaveChanges();
                    newDiesel.newWeekDieselList = db.DieselPriceWeek.ToList().OrderByDescending(x => x.Week);
                    newDiesel = NewDieselWeekObject();
                    return PartialView("ShowView", newDiesel);
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
