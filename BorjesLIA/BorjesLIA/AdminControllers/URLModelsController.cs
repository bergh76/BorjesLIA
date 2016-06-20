using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.URL;
using BorjesLIA.ViewModel;

namespace BorjesLIA.AdminControllers
{
    [Authorize]
    public class URLModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: URLModels
        [HttpGet]
        public ActionResult Index(URLViewModel urlV)
        {
            urlV = new URLViewModel
            {
                AddUrl = new URLModel(),
                newUrlList = db.UrlModels.ToList().OrderByDescending(x => x.ID)
            };
            return View(urlV);
        }

        // GET: URLModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            URLModel uRLModel = db.UrlModels.Find(id);
            if (uRLModel == null)
            {
                return HttpNotFound();
            }
            return View(uRLModel);
        }


        //[AllowAnonymous]
        ////Populates a list with data from database tabel EuroExchangeModel
        //public JsonResult GetData(URLViewModel urlx)
        //{
        //    var data = urlx.GetData();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
     
        public ActionResult _UrlList(URLViewModel urlV)
        {
            urlV = new URLViewModel
            {
                newUrlList = db.UrlModels.ToList().OrderByDescending(x => x.ID)
            };
            return View(urlV);
        }

        [ValidateAntiForgeryToken]
        public ActionResult _AddUrl(URLViewModel newUrl, FormCollection formCollection)
        {
           
            if (Request.IsAjaxRequest())
            {
                var user = HttpContext.User.Identity.Name;
                if (user == null)
                {
                    user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                }
                using (var db = new ApplicationDbContext())
                {
                    newUrl.AddUrl.dateUrl = Convert.ToDateTime(formCollection[1]);
                    newUrl.AddUrl.User = user;
                    newUrl.AddUrl.LoggDate = DateTime.Now;
                    newUrl.AddUrl.PlacingOrder = 0; //TODO: sätta deafult eller inmatning när man lägger till
                    newUrl.AddUrl.Active = true;
                    newUrl.AddUrl.Type = 2.1M;
                    db.UrlModels.Add(newUrl.AddUrl);
                    db.SaveChanges();
                    newUrl.newUrlList = db.UrlModels.ToList()
                        .OrderByDescending(x => x.ID);
                    return PartialView("_UrlList", newUrl);
                }
            }
            else
            {
                return View(newUrl);
            }
        }

        // GET: URLModels/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: URLModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,urlString,dateUrl")] URLModel uRLModel)
        {
            if (ModelState.IsValid)
            {
                db.UrlModels.Add(uRLModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uRLModel);
        }

        // GET: URLModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            URLModel uRLModel = db.UrlModels.Find(id);
            if (uRLModel == null)
            {
                return HttpNotFound();
            }
            return View(uRLModel);
        }

        // POST: URLModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, urlString, dateUrl, PlacingOrder, Type, Active, User, LoggDate, EditByUser")] URLModel uRLModel)
        {
            var editBy = HttpContext.User.Identity.Name;
            if (editBy == null)
            {
                editBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            if (ModelState.IsValid)
            {
                uRLModel.EditByUser = editBy;
                uRLModel.LoggDate = DateTime.Now;
                db.Entry(uRLModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uRLModel);
        }

        // GET: URLModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            URLModel uRLModel = db.UrlModels.Find(id);
            if (uRLModel == null)
            {
                return HttpNotFound();
            }
            return View(uRLModel);
        }

        // POST: URLModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            URLModel uRLModel = db.UrlModels.Find(id);
            db.UrlModels.Remove(uRLModel);
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
