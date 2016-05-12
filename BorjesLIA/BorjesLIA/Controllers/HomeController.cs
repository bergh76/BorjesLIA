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
            ContentSliderViewModel model = new ContentSliderViewModel()
            {
                SliderList = db.StartModels.ToList()
            };
            return View(model);

            //AddToSlider ATS = new AddToSlider();
            //ContentSliderViewModel model = new ContentSliderViewModel()
            //{
            //    SliderList = ATS.AddToListHelper()
            //    //SliderList = new List<Models.StartModel>()
            //};   
            //return View(model);

            //ContentSliderViewModel model = new ContentSliderViewModel()
            //{
            //    SliderList = new System.IO.DirectoryInfo(Server.MapPath("~/Images/contentslider/")).GetFiles()
            //};
            //return View(model);

            //return View();
        }

        public JsonResult GetData()
        {
            ListEuroViewModel eurox = new ListEuroViewModel();
            var data = eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult returnPartialView(dynamic element)
        {
            // TODO: hitta nåt sätt att sortera och veta vad som ska visas
            return PartialView(@"~/Views/EuroExchangeModels/_EuroLineGraph.cshtml");
         
        }
    }
}

/*
 [AllowAnonymous]
        public JsonResult GetData()
        {
            ListEuroViewModel eurox = new ListEuroViewModel();
            var data = eurox.GetData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //return partial view
        [HttpGet]
        [AllowAnonymous]
        public ActionResult returnPartialView()
        {
            return PartialView(@"~/Views/EuroExchangeModels/_EuroLineGraph.cshtml");
        }
*/
