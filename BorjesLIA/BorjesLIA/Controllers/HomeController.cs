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
                    listvm.ObjectName = item.Name;
                    listvm.Url = "/Images/UploadedImg/" + item.Url;
                    listvm.PlacingOrder = item.PlacingOrder;
                    listvm.Type = item.Type;
                    model.listVM.Add(listvm);
                }
            }
            foreach (var item in exPageUrl)
            {
                if (item.Active == true)
                {
                    var listvm = new listViewModel();
                    listvm.Url = item.urlString;
                    listvm.PlacingOrder = item.PlacingOrder;
                    listvm.Type = item.Type;
                    model.listVM.Add(listvm);
                }
            }
            foreach (var item in videos)
            {
                if (item.Active == true)
                {
                    var listvm = new listViewModel();
                    if (item.Type == 7) //mp4
                    {
                        listvm.Url = "/Content/videos/" + item.Url;
                        listvm.Duration = item.Duration;
                        listvm.PlacingOrder = item.PlacingOrder;
                        listvm.Type = item.Type;
                        model.listVM.Add(listvm);
                    }
                    else if (item.Type == 8) //youtube
                    {
                        listvm.Url = item.Url;
                        listvm.Duration = item.Duration;
                        listvm.PlacingOrder = item.PlacingOrder;
                        listvm.Type = item.Type;
                        model.listVM.Add(listvm);
                    }
                    else if (item.Type == 9) //vimeo
                    {
                        listvm.Url = item.Url;
                        listvm.Duration = item.Duration;
                        listvm.PlacingOrder = item.PlacingOrder;
                        listvm.Type = item.Type;
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
                    listvm.Url = "/EuroExchangeModels/_EuroLineGraph/";
                    listvm.PlacingOrder = euros.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.Type = euros.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dtm.Count != 0 || dtm != null)
            {
                var checkActive = dtm.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.Url = "/DtmModels/DtmLineGraph/";
                    listvm.PlacingOrder = dtm.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.Type = dtm.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dieselPriceQuarter != null || dieselPriceQuarter != null)
            {
                var checkActive = dieselPriceQuarter.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.Url = "/DieselQuarterPriceModels/_DieselQuarterGraph/";
                    listvm.PlacingOrder = dieselPriceQuarter.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.Type = dieselPriceQuarter.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
            if (dieselPriceWeek != null || dieselPriceWeek != null)
            {
                var checkActive = dieselPriceWeek.Select(x => x.Active).FirstOrDefault();
                if (checkActive == true)
                {
                    var listvm = new listViewModel();
                    listvm.Url = "/DieselWeekModels/_DieselWeekGraph/";
                    listvm.PlacingOrder = dieselPriceWeek.Select(x => x.PlacingOrder).FirstOrDefault();
                    listvm.Type = dieselPriceWeek.Select(x => x.Type).FirstOrDefault();
                    model.listVM.Add(listvm);
                }
            }
       
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