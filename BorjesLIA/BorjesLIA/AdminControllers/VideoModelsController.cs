using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BorjesLIA.Models;
using BorjesLIA.Models.Video;
using BorjesLIA.ViewModel;
using System.IO;

namespace BorjesLIA.AdminControllers
{
    [Authorize]
    public class VideoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VideoModels
        public ActionResult Index(VideoViewModel MVM)
        {
            MVM = new VideoViewModel
            {
                ListOfVideoModels = db.VideoModels.ToList()
            };

            return View(MVM);
        }
        private VideoViewModel returnNewObj()
        {
            VideoViewModel MVM;
            MVM = new VideoViewModel
            {
                ListOfVideoModels = db.VideoModels.ToList()
            };
            return MVM;
        }


        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, VideoModel VM, VideoViewModel modelObj)
        {
            //made change in webconfig to increase max upload size (1GB)  <httpRuntime targetFramework="4.5" maxRequestLength="1048576" />
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    if(VM.Url != null)
                    {

                        var video = new VideoModel();
                        video.Url = VM.Url;
                        video.Name = VM.Name;
                        video.PlacingOrder = 0;
                        video.Active = true;
                        video.Date = DateTime.Now;
                        video.Type = 2; //TODO: borde kanske vara enum. 2 för youtube
                        db.VideoModels.Add(video);
                        db.SaveChanges();

                    }
                    //ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".mp4" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/videos"), fileName);
                        file.SaveAs(path);
                        //ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";

                        var video = new VideoModel();
                        video.Url = fileName;
                        video.Name = VM.Name;
                        video.PlacingOrder = 0;
                        video.Active = true;
                        video.Date = DateTime.Now;
                        video.Type = 1; //TODO: borde kanske vara enum. 1 för mp4
                        db.VideoModels.Add(video);
                        db.SaveChanges();
                    }
                }
            }
            modelObj = returnNewObj();
            return View("Index", modelObj);
        }

        public ActionResult Preview(VideoViewModel MVM)
        {
            MVM = new VideoViewModel
            {
                ListOfVideoModels = db.VideoModels.ToList()
            };

            return View(MVM);
        }

        public ActionResult WatchPreview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }

            string fileName = videoModel.Url;
            string myPath = @"~\Content\videos\";
            string myFilePath = Path.Combine(myPath, fileName);

            return File(myFilePath, "Video/mp4");
        }

        [HttpPost]
        public ActionResult CallJsShowVideo(int videoID)
        {
            try
            {
                int myInt = videoID;

                VideoModel videoModel = db.VideoModels.Find(myInt);
                if (videoModel == null)
                {
                    return HttpNotFound();
                }
                //om internlänk
                if (videoModel.Type == 1) 
                {
                    string fileName = videoModel.Url;
                    string myPath = @"/Content/videos/";
                    string myFilePath = Path.Combine(myPath, fileName);
                    return Json(new { success = true, returnData = myFilePath });
                }
                //om externlänk
                else if(videoModel.Type == 2)
                {
                    string fileName = videoModel.Url;
                    return Json(new { success = true, returnData = fileName });
                }
                else
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Unable to show file.\nERRORINFO: " + ex.Message });
            }
        }

        // GET: VideoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // GET: VideoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // POST: VideoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Url,Name")] VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoModel);
        }

        // GET: VideoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // POST: VideoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoModel videoModel = db.VideoModels.Find(id);
            db.VideoModels.Remove(videoModel);
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
