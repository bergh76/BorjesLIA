
using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using BorjesLIA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BorjesLIA.ViewModel.StartModelViewModel;

namespace BorjesLIA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var images = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();
            var exPageUrl = db.UrlModels.ToList();
            var videos = db.VideoModels.ToList();
            var euros = db.EuroExchangeModels.ToList();
            var dtm = db.DtmModels.ToList();
            var dieselPriceQuarter = db.DieselPriceQuarter.ToList();
            var dieselPriceWeek = db.DieselPriceWeek.ToList();

            var model = new StartModelViewModel();
            model.listVM = new List<listViewModel>();

            //foreach (var item in images)
            //{
            //    if (item.Active == true) 
            //    {
            //        var listvm = new listViewModel();
            //        listvm.name = item.Name;
            //        //listvm.url = "/Images/contentslider/" + item.Url;
            //        listvm.url = "/Images/UploadedImg/" + item.Url;
            //        listvm.orderby = item.PlacingOrder;

            //        model.listVM.Add(listvm);
            //    }
            //}
            //foreach (var item in exPageUrl)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = item.urlString;

            //    model.listVM.Add(listvm);
            //}
            foreach (var item in videos)
            {
                var listvm = new listViewModel();
                if(item.Type==1)
                {
                    listvm.url = "/Content/videos/" + item.Url;
                    listvm.Duration = item.Duration;
                    model.listVM.Add(listvm);
                }
                else if (item.Type == 2)
                {
                    listvm.url = item.Url;
                    listvm.Duration = item.Duration;
                    model.listVM.Add(listvm);
                }
            }

            //if (euros.Count != 0 || euros != null)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
            //    model.listVM.Add(listvm);
            //}
            //if (dtm.Count != 0 || dtm != null)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/DtmModels/DtmLineGraph/";
            //    model.listVM.Add(listvm);
            //}
            //if (dieselPriceQuarter != null || dieselPriceQuarter != null)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/DieselQuarterPriceModels/_DieselQuarterGraph/";
            //    model.listVM.Add(listvm);
            //}
            //if (dieselPriceWeek != null || dieselPriceWeek != null)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/DieselWeekModels/_DieselWeekGraph/";
            //    model.listVM.Add(listvm);
            //}

            return View(model);
        }
        public ActionResult GetSettings()
        {
            // Load settings to list
            // Then sett values from SettingsList
            // Render paget from listsettings 

            return View();
        }
    }
}