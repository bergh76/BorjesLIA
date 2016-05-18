using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Img;
using System.IO;
using BorjesLIA.ViewModel;

namespace BorjesLIA.AdminControllers
{
    //[Authorize]
    public class ImgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Imgs
        public ActionResult Index(ImagesViewModel ImageV)
        {
            ImageV = new ImagesViewModel
            {
                AddImage = new Img(),
                newImageList = db.Imgs.ToList().OrderByDescending(x => x.ID)
            };
            return View(ImageV);
        }

        [AllowAnonymous]
        //Populates a list with data from database tabel ImagesViewModel
        public JsonResult GetData(ImagesViewModel imagex)
        {
            var data = db.Imgs.OrderBy(x => x.ID).ToList();
            //var data = imagex.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ImagesList(ImagesViewModel imageV)
        {
            imageV = new ImagesViewModel
            {
                newImageList = db.Imgs.ToList().OrderByDescending(x => x.Date)
            };
            return View(imageV);
        }

        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddNewImage(ImagesViewModel img, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var directorypath = Path.Combine(Server.MapPath("~/Images/ContentSlider/"));

                    var path = Path.Combine(Server.MapPath("~/Images/ContentSlider/"), fileName);
                    file.SaveAs(path);

                    img.AddImage.Url = fileName;
                    img.AddImage.Date = DateTime.Now;
                    img.AddImage.Active = true;
                    db.Imgs.Add(img.AddImage);
                    db.SaveChanges();

                    img.newImageList = db.Imgs.ToList().OrderByDescending(x => x.PlacingOrder);
                }
                //return PartialView("_ImagesList", img); //kör inte ajax
                return View("Index", img);
            }
            return View(img);
        }



        // GET: Imgs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }


        // POST: Imgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Imgs/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Img img, HttpPostedFileBase file)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (file != null)
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            var directorypath = Path.Combine(Server.MapPath("~/Images/ContentSlider/"));

        //            var path = Path.Combine(Server.MapPath("~/Images/ContentSlider/"), fileName);
        //            file.SaveAs(path);

        //            img.Url = fileName;
        //            img.Date = DateTime.Now;
        //            img.Active = true;

        //            db.Imgs.Add(img);
        //            db.SaveChanges();
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    return View(img);
        //}


        // GET: Imgs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        // POST: Imgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Url,Name,Date,PlacingOrder,Active")] Img img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }

        // GET: Imgs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Img img = db.Imgs.Find(id);
            if (img == null)
            {
                return HttpNotFound();
            }
            return View(img);
        }

        // POST: Imgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Img img = db.Imgs.Find(id);
            db.Imgs.Remove(img);
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
