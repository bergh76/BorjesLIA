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
using System.Web.Helpers;
using System.Threading;

namespace BorjesLIA.AdminControllers
{
    //[Authorize]
    public class ImgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private const string TempFolder = "/Temp";
        private const string MapTempFolder = "~" + TempFolder;
        public string ActionString = String.Empty;
        public string AvatarPath = "/Images/UploadedImg/";
        private readonly string[] _imageFileExtensions = { ".jpg", ".png", ".gif", ".jpeg" };

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

        //borrowed code

        [AllowAnonymous]
        public ActionResult MyImages()
        {
            var model = new ImagesViewModel();

            return View(model);
        }


        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ShowView(int? ourId, IEnumerable<HttpPostedFileBase> files, int avatarW = 1000, int avatarH = 1000)
        {
            if (files == null || !files.Any()) return Json(new { success = false, errorMessage = "No file uploaded." });
            var file = files.FirstOrDefault();  // get ONE only
            if (file == null || !IsImage(file)) return Json(new { success = false, errorMessage = "File is of wrong format." });
            if (file.ContentLength <= 0) return Json(new { success = false, errorMessage = "File cannot be zero length." });
            var webPath = GetTempSavedFilePath(file, avatarW, avatarH);
            return Json(new { success = true, fileName = webPath.Replace("/", "\\") }); // success
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SaveSingleImage(string t, string l, string h, string w, string fileName, int avatarW, int avatarH, int placingOrder, string imgName)
        {
            TempData["singleupload"] = "nope";

            try
            {
                // Calculate dimensions
                var top = Convert.ToInt32(t.Replace("-", "").Replace("px", ""));
                var left = Convert.ToInt32(l.Replace("-", "").Replace("px", ""));
                var height = Convert.ToInt32(h.Replace("-", "").Replace("px", ""));
                var width = Convert.ToInt32(w.Replace("-", "").Replace("px", ""));

                // Get file from temporary folder
                var fn = Path.Combine(Server.MapPath(MapTempFolder), Path.GetFileName(fileName));
                // ...get image and resize it, ...
                var img = new WebImage(fn);

                img.Resize(width, height);
                // ... crop the part the user selected, ...
                img.Crop(top, left, img.Height - top - avatarH, img.Width - left - avatarW);
                // ... delete the temporary file,...
                System.IO.File.Delete(fn);
                // ... and save the new one.
                var filename2 = Path.GetFileName(fn);
                //var user = User.Identity.Name;

                //AvatarPath = AvatarPath + String.Format("/UploadedImg/{0}/", user);
                //AvatarPath = AvatarPath + String.Format("/UploadedImg/{0}/", user);
                var newFileName = Path.Combine(AvatarPath, Path.GetFileName(fn));
                var newFileLocation = HttpContext.Server.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.Save(newFileLocation);
                //TODO: fält som ska vara med
                var image = new Img();
                image.Name = imgName; 
                image.Url = filename2;
                image.Date = DateTime.Now;
                image.Active = true;
                image.PlacingOrder = placingOrder; 

               
                db.Imgs.Add(image);
                db.SaveChanges();

                TempData["singleupload"] = "success";

                return Json(new { success = true, avatarFileLocation = newFileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Unable to upload file.\nERRORINFO: " + ex.Message });
            }
        }

        [AllowAnonymous]
        public ActionResult SingleImage()
        {
            return View();
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file == null) return false;
            return file.ContentType.Contains("image") ||
                _imageFileExtensions.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        private string GetTempSavedFilePath(HttpPostedFileBase file, int w, int h)
        {
            // Define destination
            var serverPath = HttpContext.Server.MapPath(TempFolder);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            // Generate unique file name
            var fileName = Path.GetFileName(file.FileName);
            fileName = SaveTemporaryAvatarFileImage(file, serverPath, fileName, w, h);

            // Clean up old files after every save
            CleanUpTempFolder(1);
            return Path.Combine(TempFolder, fileName);
        }

        private static string SaveTemporaryAvatarFileImage(HttpPostedFileBase file, string serverPath, string fileName, int w, int h)
        {
            var img = new WebImage(file.InputStream);

            double ratio = img.Height / (double)img.Width;

            var fullFileName = Path.Combine(serverPath, fileName);
            Thread.Sleep(100);
            if (System.IO.File.Exists(fullFileName))
            {
                System.IO.File.Delete(fullFileName);
            }
            Thread.Sleep(100);
            img.Save(fullFileName);
            return Path.GetFileName(img.FileName);
        }

        private void CleanUpTempFolder(int hoursOld)
        {
            try
            {
                var currentUtcNow = DateTime.UtcNow;
                var serverPath = HttpContext.Server.MapPath("/Temp");
                if (!Directory.Exists(serverPath)) return;
                var fileEntries = Directory.GetFiles(serverPath);
                foreach (var fileEntry in fileEntries)
                {
                    var fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                    var res = currentUtcNow - fileCreationTime;
                    if (res.TotalHours > hoursOld)
                    {
                        System.IO.File.Delete(fileEntry);
                    }
                }
            }
            catch
            {
                // Deliberately empty.
            }
        }

        //end borrowed code


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
