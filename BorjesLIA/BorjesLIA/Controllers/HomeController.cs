using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using BorjesLIA.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            foreach (var item in images)
            {
                if (item.Active == true)
                {
                    var listvm = new listViewModel();
                    listvm.name = item.Name;
                    listvm.url = "/Images/UploadedImg/" + item.Url;
                    listvm.orderby = item.PlacingOrder;
                    listvm.type = item.Type;
                    model.listVM.Add(listvm);
                }
            }
            foreach (var item in exPageUrl)
            {
                if (item.Active == true)
                {
                    var listvm = new listViewModel();
                    listvm.url = item.urlString;
                    listvm.orderby = item.PlacingOrder;
                    listvm.type = item.Type;
                    model.listVM.Add(listvm);
                }
            }
            foreach (var item in videos)
            {
                if (item.Active == true)
                {
                    var listvm = new listViewModel();
                    if (item.Type == 4.1m) //mp4
                    {
                        listvm.url = "/Content/videos/" + item.Url;
                        listvm.Duration = item.Duration;
                        listvm.orderby = item.PlacingOrder;
                        listvm.type = item.Type;
                        model.listVM.Add(listvm);
                    }
                    else if (item.Type == 4.2m) //youtube
                    {
                        listvm.url = item.Url;
                        listvm.Duration = item.Duration;
                        listvm.orderby = item.PlacingOrder;
                        listvm.type = item.Type;
                        model.listVM.Add(listvm);
                    }
                }
            }

            if (euros.Count != 0 || euros != null)
            {
               var checkActive = euros.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
                    listvm.orderby = euros.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.type = euros.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dtm.Count != 0 || dtm != null)
            {
                var checkActive = dtm.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.url = "/DtmModels/DtmLineGraph/";
                    listvm.orderby = dtm.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.type = dtm.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dieselPriceQuarter != null || dieselPriceQuarter != null)
            {
                var checkActive = dieselPriceQuarter.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.url = "/DieselQuarterPriceModels/_DieselQuarterGraph/";
                    listvm.orderby = dieselPriceQuarter.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.type = dieselPriceQuarter.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dieselPriceWeek != null || dieselPriceWeek != null)
            {
                var checkActive = dieselPriceWeek.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.url = "/DieselWeekModels/_DieselWeekGraph/";
                    listvm.orderby = dieselPriceWeek.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.type = dieselPriceWeek.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            //model.listVM = listOfViewModel;
            return View(model);
        }

         public ActionResult Order()
        {

            return View();
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