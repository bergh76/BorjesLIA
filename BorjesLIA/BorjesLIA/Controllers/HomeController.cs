
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
            var euros = db.EuroExchangeModels.ToList();
            var dtm = db.DtmModels.ToList();
            var dieselPrice = db.DieselPriceQuarter.ToList();

            var model = new StartModelViewModel();
            model.listVM = new List<listViewModel>();

            foreach (var item in images)
            {
                var listvm = new listViewModel();
                var myImages = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();
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
                listvm.url = "/DtmModels/_DtmLineGraph/";
                model.listVM.Add(listvm);
            }
            //if (dieselPrice != null)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/DieselQuarterPriceModels/_QuarterPriceDiesel/";
            //    model.listVM.Add(listvm);
            //}
            
            return View(model);
        }
    }
}

         


