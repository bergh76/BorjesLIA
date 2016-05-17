using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Euro;

using PagedList;
using BorjesLIA.ViewModel;

namespace BorjesLIA.AdminControllers
{
    //[Authorize]
    public class EuroExchangeModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EuroExchangeModels
        [HttpGet]
        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(db.EuroExchangeModels.ToList());
        }

        public ActionResult EuroListPagination(int pageNumber = 1, int pageSize = 10)
        {
            var euroList = db.EuroExchangeModels.ToList();
            PagedList<EuroExchangeModel> model = new PagedList<EuroExchangeModel>(euroList, pageNumber, pageSize);
            return View(model);
        }
        //Populates a list with data from database tabel EuroExchangeModel
        [AllowAnonymous]
        public JsonResult GetData()
        {
            ListEuroViewModel eurox = new ListEuroViewModel();
            var data = eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //ge en view
        public ActionResult _EuroLineGraph()
        {
            var someTestName = db.EuroExchangeModels.ToList();
            int name = someTestName.FirstOrDefault().ID;

            ViewBag.asdf = name;
        
            return View();
        }

        public ActionResult childController()
        {
            

            return View();
        }

        //get optional graph
        //public ActionResult _EuroLineGraph(int typOfChart)
        //{
        //    if (typOfChart == 1)
        //    {
        //        return PartialView(@"~/Views/Graph/GraphLine.cshtml");
        //    }
        //    else if (typOfChart == 2)
        //    {
        //        return PartialView(@"~/Views/Graph/GraphBar.cshtml");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //return partial view
        [HttpGet]
        [AllowAnonymous]
        public ActionResult returnPartialView()
        {
            return PartialView(@"~/Views/EuroExchangeModels/_EuroLineGraph.cshtml");
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

        // GET: EuroExchangeModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EuroExchangeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,euroValue,Date")] EuroExchangeModel euroExchangeModel)
        {
            if (ModelState.IsValid)
            {
                db.EuroExchangeModels.Add(euroExchangeModel);
                db.SaveChanges();
                return RedirectToAction("Index");
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
