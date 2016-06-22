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
                    
                    newEuro.AddEuro.Type = 1;
                    db.EuroExchangeModels.Add(newEuro.AddEuro);
                    db.SaveChanges();
                    newEuro = NewEuroObject();
                    return PartialView("ShowView", newEuro);
                }
            }
            return View(newEuro);
        }

        public ActionResult _EuroLineGraph(EuroViewModel euroGraph)
        {
            euroGraph =  new EuroViewModel
            {
                settings = db.Settings.Where(x => x.Name == "Eurokurs")
            };
            return View(euroGraph);
        }

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
        public ActionResult Edit([Bind(Include = "ID, Year, Month, euroValue, Date, LoggDate, EditByUser")] EuroExchangeModel euroExchangeModel)
        {
            var editBy = HttpContext.User.Identity.Name;
            if (editBy == null)
            {
                editBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            if (ModelState.IsValid)
            {
                euroExchangeModel.EditByUser = editBy;
                euroExchangeModel.LoggDate = DateTime.Now;
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
