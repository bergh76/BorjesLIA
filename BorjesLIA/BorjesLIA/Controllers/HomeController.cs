using BorjesLIA.Helper;
using BorjesLIA.Models;
using BorjesLIA.ViewModel;
using BorjesLIA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //StartModel asdf = new StartModel()
            //{
            //    UrlSlide = db.UrlModels.ToList()
            //}

            AddToSlider ATS = new AddToSlider();
            ContentSliderViewModel model = new ContentSliderViewModel()
            {
                SliderList = ATS.AddToListHelper()
                //SliderList = new List<Models.StartModel>()
            };   
            return View(model);

            //ContentSliderViewModel model = new ContentSliderViewModel()
            //{
            //    ImagesList = new System.IO.DirectoryInfo(Server.MapPath("~/Images/contentslider/")).GetFiles()
            //};
            //return View(model);

            //return View();
        }

        public ActionResult returnPartialView()
        {
            return View();
        }
    }
}