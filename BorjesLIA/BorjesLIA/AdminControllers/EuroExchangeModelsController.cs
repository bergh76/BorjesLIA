using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using BorjesLIA.ViewModel;
using System.Threading.Tasks;
using System;
using System.Globalization;

namespace BorjesLIA.AdminControllers
{

    public class EuroExchangeModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        string settingsName = "Eurokurs";


        // GET: EuroExchangeModels
        public ActionResult Index(EuroViewModel euroObject)
        {
            euroObject = NewEuroObject();
            return View(euroObject);
        }

        /// <summary>
        /// Creats a new DiesleWeekViewModel object, populates needed lists with data and returns the object
        /// </summary>
        /// <returns></returns>
        private EuroViewModel NewEuroObject()
        {
            EuroViewModel euroV;
            euroV = new EuroViewModel
            {
                AddEuro = new EuroExchangeModel(),
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return euroV;
        }


        /// <summary>
        /// Creats a new EuroViewModel object, populates needed lists with data and return a view with data        /// </summary>
        /// <param name="newEuro"></param>
        /// <returns></returns>

        public ActionResult _AddEuro(EuroViewModel newEuro, FormCollection formCollection)
        {

            // Adds a new post to Entity EuroExchangeModel
            if (Request.IsAjaxRequest())
            {

                using (var db = new ApplicationDbContext())
                {
                    if (!string.IsNullOrEmpty(formCollection[1]))
                    {
                        var date = Convert.ToDateTime(formCollection[1]);
                        var year = date.Year.ToString();
                        // month name //
                        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                        DateTime date1 = date;
                        Calendar cal = dfi.Calendar;
                        var monthInt = cal.GetMonth(date1);
                        var month = dfi.GetMonthName(monthInt);
                        newEuro.AddEuro.Date = date;
                        newEuro.AddEuro.Year = year;
                        newEuro.AddEuro.Month = month;
                        if (db.EuroExchangeModels.Any(x => x.Year == year && x.Month == month) || db.EuroExchangeModels.ToList().Select(x => x.Date) == null)
                        {
                            // ToDo: return a errormessage to the view //
                            return View(newEuro);
                        }                        
                    }
                    else
                    {
                        // ToDo: return a errormessage to the view //
                        return View(newEuro);
                    }
                    var previousValue = db.EuroExchangeModels.FirstOrDefault();
                        if (previousValue != null)
                        {
                            newEuro.AddEuro.PlacingOrder = previousValue.PlacingOrder;
                            newEuro.AddEuro.Active = previousValue.Active;
                        }
                    
                    else
                    {
                        newEuro.AddEuro.PlacingOrder = 0;
                        newEuro.AddEuro.Active = true;
                    }
                    
                    newEuro.AddEuro.Type = 1.1M;
                    db.EuroExchangeModels.Add(newEuro.AddEuro);
                    db.SaveChanges();
                    newEuro = NewEuroObject();
                    return PartialView("ShowView", newEuro);
                }
            }
            return View(newEuro);
        }


        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="eurox"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        ////Populates a list with data from database tabel EuroExchangeModel
        //public async Task<JsonResult> GetData(EuroViewModel eurox)
        //{
        //    var data = await eurox.GetData();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult _EuroLineGraph(EuroViewModel euroGraph)
        {
            euroGraph =  new EuroViewModel
            {
                settings = db.Settings.Where(x => x.Name == "Eurokurs")
            };
            return View(euroGraph);
        }

        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the View
        /// </summary>
        /// <param name="euroV"></param>
        /// <returns></returns>
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult _AddNewEuro(EuroViewModel newEuro)
        //{
        //    if (Request.IsAjaxRequest() && ModelState.IsValid)
        //    {
        //        using (var db = new ApplicationDbContext())
        //        {
        //            db.EuroExchangeModels.Add(newEuro.AddEuro);
        //            db.SaveChanges();
        //            newEuro.newEuroList = db.EuroExchangeModels.ToList()
        //                .OrderByDescending(x => x.Date);
        //            return PartialView("_EuroList", newEuro);
        //        }
        //    }
        //    else
        //    {
        //        return View(newEuro);
        //    }
        //}


        //public ActionResult Index_EuroSettings(EuroViewModel euroSettings)
        //{
        //    euroSettings = new EuroViewModel
        //    {
        //        AddEuro = new EuroExchangeModel(),
        //        newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
        //        //populates list used for determain charttype from Entity Settings
        //        settings = db.Settings.Where(x => x.Name == settingsName)
        //    };
        //    return View(euroSettings);
        //}


        //[HttpPost]
        //public ActionResult SaveEuroSettings(Settings conf, FormCollection form)
        //{

        //    // Saves new settings to Entity Settings
        //    if (Request.IsAjaxRequest() && ModelState.IsValid)
        //    {
        //        string name = form[1].ToString();
        //        conf.ID = db.Settings.Where(x => x.Name == name).Select(x => x.ID).FirstOrDefault();
        //        conf.Year = Convert.ToInt32(form[3]);
        //        conf.Name = form[1];
        //        if (!string.IsNullOrEmpty(conf.Name) && conf.Year != 0)
        //        {
        //            db.Entry(conf).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return View("Index_EuroSettings", conf);
        //        }
        //    }
        //    return View(conf);
        //}



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
        public ActionResult Edit([Bind(Include = "ID,euroValue,Date")] EuroExchangeModel euroExchangeModel)
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
