
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
            //TODO: testa att sidan fungerar även om det är null i databasen.

            var images = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();
            var exPageUrl = db.UrlModels.ToList();
            var euros = db.EuroExchangeModels.ToList();
            var dtm = db.DtmModels.ToList();
            var dieselPrice = db.DieselPriceModels.ToList();

            var model = new StartModelViewModel();
            model.listVM = new List<listViewModel>();

            foreach (var item in images)
            {
                var listvm = new listViewModel();
                listvm.name = item.Name;
                listvm.url = "/Images/contentslider/" + item.Url;
                listvm.orderby = item.PlacingOrder;

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
    }
}


