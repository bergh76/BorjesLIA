using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Settings;
using BorjesLIA.ViewModel;
using BorjesLIA.Models.Euro;
using System.Threading.Tasks;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;

namespace BorjesLIA.Controllers
{
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string confEuroSettings = "Eurokurs";
        string confDtmSettings = "Drivmedelstillägg";
        // GET: Settings
        //public ActionResult Index(EuroViewModel euroV)
        //{
        //    euroV = new EuroViewModel
        //    {
        //        AddEuro = new EuroExchangeModel(),
        //        newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
        //        //populates list used for determain charttype from Entity Settings
        //        settings = db.Settings.Where(x => x.Name == confEuro)
        //    };
        //    return View(euroV);
        //}

        /// <summary>
        /// Creats a new EuroViewModel object, populates needed lists with data and return a view with data
        /// </summary>
        /// <param name="euroSettings"></param>
        /// <returns></returns>
        public ActionResult Index_EuroSettings(EuroViewModel euroSettings)
        {
            euroSettings = new EuroViewModel 
            {
                AddEuro = new EuroExchangeModel(),
                newEuroList = db.EuroExchangeModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == confEuroSettings)
            };
            return View(euroSettings);
        }

        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="eurox"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetEuroData(EuroViewModel eurox)
        {
            var data = await eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }     

        /// <summary>
        /// Creats a Settings object and collects data from inputform via FormCollection and saves new values to Settings Entity
        /// </summary>
        /// <param name="confEuro"></param>
        /// <param name="formEuro"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveEuroSettings(Settings confEuro, FormCollection formEuro)
        {
            // Saves new settings to Entity Settings
            if (Request.IsAjaxRequest() && ModelState.IsValid)
            {
                // populate values from Html-form
                string name = formEuro[1].ToString();
                confEuro.ID = db.Settings.Where(x => x.Name == confEuroSettings).Select(x => x.ID).FirstOrDefault();
                confEuro.Year = Convert.ToInt32(formEuro[2]);
                confEuro.Name = this.confEuroSettings;
                if (!string.IsNullOrEmpty(confEuro.Name) && confEuro.Year != 0)
                {
                    // saves data to Settings db Entity
                    db.Entry(confEuro).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("Index_EuroSettings", confEuro);                  
                }
               
            }
            return View(confEuro);
        }




        /// <summary>
        /// Creats a new EuroViewModel object, populates needed lists with data and return a view with data
        /// </summary>
        /// <param name="dtmSettings"></param>
        /// <returns></returns>
        public ActionResult Index_DtmSettings(DMTViewModel dtmSettings)
        {
            dtmSettings = new DMTViewModel
            {
                AddDtm = new DtmModel(),
                newDTMList = db.DtmModels.ToList().OrderByDescending(x => x.Date),
                //populates list used for determain charttype from Entity Settings
                settings = db.Settings.Where(x => x.Name == confDtmSettings)
            };
            return View(dtmSettings);
        }

        /// <summary>
        /// Populates the chart with data
        /// </summary>
        /// <param name="dtmData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //Populates a list with data from database tabel EuroExchangeModel
        public async Task<JsonResult> GetDTMData(DMTViewModel dtmData)
        {
            var data = await dtmData.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creats a Settings object and collects data from inputform via FormCollection and saves new values to Settings Entity
        /// </summary>
        /// <param name="confDtm"></param>
        /// <param name="formDtm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDtmSettings(Settings confDtm, FormCollection formDtm)
        {
            // Saves new settings to Entity Settings
            if (Request.IsAjaxRequest() && ModelState.IsValid)
            {
                // populate values from Html-form
                string name = formDtm[1].ToString();
                confDtm.ID = db.Settings.Where(x => x.Name == confDtmSettings).Select(x => x.ID).FirstOrDefault();
                confDtm.Year = Convert.ToInt32(formDtm[2]);
                confDtm.Name = this.confDtmSettings;
                if (!string.IsNullOrEmpty(confDtm.Name) && confDtm.Year != 0)
                {
                    // saves data to Settings db Entity
                    db.Entry(confDtm).State = EntityState.Modified;
                    db.SaveChanges();
                    
                    return PartialView("Index_DtmSettings", confDtm);
                }

            }
            return View(confDtm);
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
