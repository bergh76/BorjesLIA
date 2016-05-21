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
using BorjesLIA.Models.Settings;

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
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
                settings = db.Settings.Where(x => x.Name == "Eurokurs")
            };
            return View(euroV);
        }


        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the site
        /// </summary>
        /// <param name="newEuro"></param>
        /// <returns></returns>
        //public ActionResult ShowView(EuroViewModel euroV)
        //{
        //    euroV = new EuroViewModel
        //    {
        //        AddEuro = new EuroExchangeModel(),
        //        newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date)
        //    };
        //    return View(euroV);
        //}


        //[Authorize]


        [ValidateAntiForgeryToken]
        public ActionResult _AddEuro(EuroViewModel newEuro)
        {
            if (Request.IsAjaxRequest() && ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.EuroExchangeModels.Add(newEuro.AddEuro);
                    db.SaveChanges();
                    newEuro.newEuroList = db.EuroExchangeModels.ToList()
                        .OrderByDescending(x => x.Date);
                    ModelState.Clear();
                    return PartialView("ShowView", newEuro);
                }
            }
            return View(newEuro);
        }

        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetData(EuroViewModel eurox)
        {
            var data = await eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _EuroLineGraph(EuroViewModel euroV)
        {
            euroV = new EuroViewModel
            {                
                settings = db.Settings.Where(x => x.Name == "Eurokurs")
            };
            return View(euroV);
        }

        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the site
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


        /// <summary>
        ///This ActionResult is not "in play" ie. no functionality connected to the site
        /// or connected to the site
        /// </summary>
        /// <param name="newEuro"></param>
        /// <returns></returns>
        //public ActionResult _SubmitReload(EuroViewModel newEuro)
        //{
        //    //return RedirectToAction("Index", new EuroViewModel { });
        //    //return PartialView("_EuroList", newEuro);
        //    return View();
        //}


        public ActionResult Index_EuroSettings(EuroViewModel euroV)
        {
            euroV = new EuroViewModel
            {
                AddEuro = new EuroExchangeModel(),
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
                settings = db.Settings.Where(x => x.Name == "Eurokurs")
            };
            return View(euroV);
        }


        [HttpPost]
        public ActionResult SaveEuroSettings(Settings conf, FormCollection form)
        {
            //var dir = Path.Combine(Server.MapPath("~/Settings/Euro/EuroSettings.txt"));
            if (Request.IsAjaxRequest())
            {
                string name = form[1].ToString();
                conf.ID = db.Settings.Where(x => x.Name == name).Select(x => x.ID).FirstOrDefault();
                conf.Year = Convert.ToInt32(form[3]);
                conf.Name = form[1];
                db.Entry(conf).State = EntityState.Modified;
                db.SaveChanges();

                return PartialView("Index_EuroSettings");
            }
            return View(conf);
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
