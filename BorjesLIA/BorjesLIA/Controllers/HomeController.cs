﻿
using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using BorjesLIA.ViewModel;
using BorjesLIA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BorjesLIA.ViewModels.StartModelViewModel;

namespace BorjesLIA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //TODO: testa att sidan fungerar även om det är null i databasen.

<<<<<<< HEAD
            var images = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();
            var exPageUrl = db.UrlModels.ToList();
            var euros = db.EuroExchangeModels.ToList();
            var dtm = db.DtmModels.ToList();
            var dieselPrice = db.DieselPriceModels.ToList();

            var model = new StartModelViewModel();
            model.listVM = new List<listViewModel>();

            foreach (var item in images)
=======
            if (db.UrlModels != null || db.EuroExchangeModels != null || db.Imgs != null)
>>>>>>> ver.1.0.3
            {
                var images = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();

<<<<<<< HEAD
                model.listVM.Add(listvm);
            }
            foreach (var item in exPageUrl)
            {
                var listvm = new listViewModel();
                listvm.url = item.urlString;

                model.listVM.Add(listvm);
            }

            if (euros != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
                model.listVM.Add(listvm);
            }
            if (dtm != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/DtmModels/DtmLineGraph/";
                model.listVM.Add(listvm);
            }
            if (dieselPrice != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/DieselPriceModels/DieselPriceGraph/";
                model.listVM.Add(listvm);
            }
            

            return View(model);
        }
=======
                var exPageUrl = db.UrlModels.ToList();
                var euros = db.EuroExchangeModels.ToList();
                var model = new StartModelViewModel();
                model.listVM = new List<listViewModel>();

                foreach (var item in exPageUrl)
                {
                    var listvm = new listViewModel();
                    listvm.url = item.urlString;
                    model.listVM.Add(listvm);
                }

                if (euros != null)
                {
                    
                    var listvm = new listViewModel();
                    listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
                    model.listVM.Add(listvm);
                }

                return View(model);
            }
            return View();
        }

    }
}

            /**
            //ContentSliderViewModel model = new ContentSliderViewModel()
            //{
            //    SliderList = db.StartModels.ToList()
            //};
            //return View(model);

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

        //public JsonResult GetData()
        //{
        //    EuroViewModel eurox = new EuroViewModel();
        //    var data = eurox.GetData();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult returnPartialView(dynamic element)
        //{
        //    var something = SH.returnPartialViewUrl(element);

        //    //return PartialView(@"~/Views/EuroExchangeModels/_EuroLineGraph.cshtml");
        //    return PartialView(something);
        //}
>>>>>>> ver.1.0.3
    }
}


