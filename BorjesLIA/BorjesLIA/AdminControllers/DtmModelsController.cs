using System.Data;
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
    [Authorize]
    public class DtmModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DtmModels

        public ActionResult Index(DMTViewModel dtmV)
        {
            dtmV = new DMTViewModel
            {
                AddDtm = new DtmModel(),
                newDTMList = db.DtmModels.ToList()
                .OrderByDescending(x => x.Date)

            };
            return View(dtmV);
            //    return View(db.DtmModels.ToList());
        }

        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(DMTViewModel dtmChart)
        {
            var data = await dtmChart.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult _AddNewDTM(DMTViewModel newDTM)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    db.DtmModels.Add(newDTM.AddDtm);
                    db.SaveChanges(); // ToDO {"The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value.\r\nThe statement has been terminated."}
                    newDTM.newDTMList = db.DtmModels.ToList()
                        .OrderByDescending(x => x.Date);
                    return PartialView("_DtmList", newDTM);
                }
            }
            else
            {
                return View(newDTM);
            }
        }

        [AllowAnonymous]
        //ge en view
        public ActionResult _DtmGraphData(DMTViewModel dtmV)
        {
            dtmV = new DMTViewModel
            {
                newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date)
            };
            return View(dtmV);
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
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DtmModels/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Date,DieselDTMValue,DieselDTMChartID,LoggDate")] DtmModel dtmModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DtmModels.Add(dtmModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dtmModel);
        //}

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
        public ActionResult Edit([Bind(Include = "ID,Date,DieselDTMValue,DieselDTMChartID,LoggDate")] DtmModel dtmModel)
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
