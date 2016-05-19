using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using BorjesLIA.ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using BorjesLIA.Models.Charts;

namespace BorjesLIA.AdminControllers
{
    
    public class EuroExchangeModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EuroExchangeModels
        [HttpGet]
        public ActionResult Index(EuroViewModel euroV)
        {
            euroV = new EuroViewModel
            {
                AddEuro = new EuroExchangeModel(),
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date)
            };
            return View(euroV);
        }

        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public JsonResult GetData(EuroViewModel eurox)
        {
            var data = eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

         public ActionResult _EuroLineGraph(EuroViewModel euroV)
        {
            euroV = new EuroViewModel
            {
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date)
            };
            return View(euroV);
        }     

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult _AddNewEuro(EuroViewModel newEuro)
        {
            if (Request.IsAjaxRequest() && ModelState.IsValid) 
            {
                using (var db = new ApplicationDbContext())
                {
                    db.EuroExchangeModels.Add(newEuro.AddEuro);
                    db.SaveChanges();
                    newEuro.newEuroList = db.EuroExchangeModels.ToList()
                        .OrderByDescending(x => x.Date);
                    return PartialView("_EuroList", newEuro);
                }
            }
            else
            {
                return View(newEuro);
            }
        }

        public ActionResult _SubmitReload(EuroViewModel newEuro)
        {
            //return RedirectToAction("Index", new EuroViewModel { });
            //return PartialView("_EuroList", newEuro);
            return View();
        }


        // GET: EuroExchangeModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EuroExchangeModel euroExchangeModel = db.EuroExchangeModels.Find(id);
            if (euroExchangeModel == null)
            {
                return HttpNotFound();
            }
            return View(euroExchangeModel);
        }
       

        // GET: EuroExchangeModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EuroExchangeModel euroExchangeModel = db.EuroExchangeModels.Find(id);
            if (euroExchangeModel == null)
            {
                return HttpNotFound();
            }
            return View(euroExchangeModel);
        }

        // POST: EuroExchangeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,euroValue,Date,EuroChartID")] EuroExchangeModel euroExchangeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(euroExchangeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(euroExchangeModel);
        }

        // GET: EuroExchangeModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EuroExchangeModel euroExchangeModel = db.EuroExchangeModels.Find(id);
            if (euroExchangeModel == null)
            {
                return HttpNotFound();
            }
            return View(euroExchangeModel);
        }

        // POST: EuroExchangeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EuroExchangeModel euroExchangeModel = db.EuroExchangeModels.Find(id);
            db.EuroExchangeModels.Remove(euroExchangeModel);
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
