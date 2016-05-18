
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
                if (item.Active == true) //TODO: inte testad om den fungerar som tänkt.
                {
                    var listvm = new listViewModel();
                    listvm.name = item.Name;
                    listvm.url = "/Images/contentslider/" + item.Url;
                    listvm.orderby = item.PlacingOrder;

                    model.listVM.Add(listvm);
                }
            }
            foreach (var item in exPageUrl)
            {
                var listvm = new listViewModel();
                listvm.url = item.urlString;

                model.listVM.Add(listvm);
            }

            if (euros.Count != 0)
            {
                var listvm = new listViewModel();
                listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
                model.listVM.Add(listvm);
            }
            //if (dtm.Count != 0)
            //{
            //    var listvm = new listViewModel();
            //    listvm.url = "/DtmModels/DtmLineGraph/";
            //    model.listVM.Add(listvm);
            //}
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




