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
    //[Authorize]
    public class DtmModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Static name for Settings
        string settingsName = "Drivmedelstillägg";
        
        // GET: DtmModels
        public ActionResult Index(DMTViewModel dtmV)
        {
            dtmV = NewDTMObject();
            return View(dtmV);
        }

        /// <summary>
        /// Creats a new DMTViewModel object, populates needed lists with data and returns the object
        /// </summary>
        /// <returns></returns>
        private DMTViewModel NewDTMObject()
        {
            DMTViewModel dtmV;
            dtmV = new DMTViewModel
            {
                AddDtm = new DtmModel(),
                newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return dtmV;
        }

        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="dtmChart"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<JsonResult> GetData(DMTViewModel dtmChart)
        {
            //var data = await eurox.GetQuaerterData();
            var data = await dtmChart.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Instansiates an object for startpage
        /// </summary>
        /// <param name="dtmV"></param>
        /// <returns></returns>
        [AllowAnonymous]
         public ActionResult DtmLineGraph(DMTViewModel dtmV)
        {
            dtmV = new DMTViewModel
            {
                newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
            };
            return View(dtmV);
        }


        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the View
        /// </summary>
        /// <param name="dtmV"></param>
        /// <returns></returns>
        //public ActionResult ShowView(DMTViewModel dtmV)
        //{
        //    dtmV = new DMTViewModel
        //    {
        //        AddDtm = new DtmModel(),
        //        newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date),
        //        //populates list used for determain charttype from Entity Settings
        //        settings = db.Settings.Where(x => x.Name == settingsName)

        //    };
        //    return View(dtmV);
        //}

        /// <summary>
        /// Creats a new DMTViewModel object, populates needed lists with data and return a view with data
        /// </summary>
        /// <param name="newDTM"></param>
        /// <returns></returns>
        public ActionResult _AddNewDTM(DMTViewModel newDTM)
        {
            if (Request.IsAjaxRequest())
            {
                using (var db = new ApplicationDbContext())
                {
                    newDTM.AddDtm.Type = 1.2M;
                    db.DtmModels.Add(newDTM.AddDtm);
                    db.SaveChanges(); 
                    newDTM.newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date);
                    newDTM = NewDTMObject();
                    return PartialView("ShowView", newDTM);
                }
            }
            else
            {
                return View(newDTM);
            }
        }

       

        /// <summary>
        /// This ActionResult is not "in play" ie. no functionality connected to the View
        /// </summary>
        /// <param name="dtmV"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult _DTMList(DMTViewModel dtmV)
        {
            dtmV = new DMTViewModel
            {
                newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == settingsName)
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
