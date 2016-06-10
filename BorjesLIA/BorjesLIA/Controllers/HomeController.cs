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
                    //listOfViewModel.Add(listvm);
                }
            }
            foreach (var item in exPageUrl)
            {
                var listvm = new listViewModel();
                listvm.url = item.urlString;
                listvm.orderby = item.PlacingOrder;
                listvm.type = item.Type;
                model.listVM.Add(listvm);
                //listOfViewModel.Add(listvm);
            }
            foreach (var item in videos)
            {
                var listvm = new listViewModel();
                if (item.Type == 4.1m) //mp4
                {
                    listvm.url = "/Content/videos/" + item.Url;
                    listvm.Duration = item.Duration;
                    listvm.orderby = item.PlacingOrder;
                    listvm.type = item.Type;
                    model.listVM.Add(listvm);
                    //listOfViewModel.Add(listvm);
                }
                else if (item.Type == 4.2m) //youtube
                {
                    listvm.url = item.Url;
                    listvm.Duration = item.Duration;
                    listvm.orderby = item.PlacingOrder;
                    listvm.type = item.Type;
                    model.listVM.Add(listvm);
                    //listOfViewModel.Add(listvm);
                }
            }

            if (euros.Count != 0 || euros != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/EuroExchangeModels/_EuroLineGraph/";
                listvm.orderby = euros.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = euros.Select(x => x.Type).FirstOrDefault();
                model.listVM.Add(listvm);
                //listOfViewModel.Add(listvm);
            }
            if (dtm.Count != 0 || dtm != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/DtmModels/DtmLineGraph/";
                listvm.orderby = dtm.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dtm.Select(x => x.Type).FirstOrDefault();
                model.listVM.Add(listvm);
                //listOfViewModel.Add(listvm);
            }
            if (dieselPriceQuarter != null || dieselPriceQuarter != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/DieselQuarterPriceModels/_DieselQuarterGraph/";
                listvm.orderby = dieselPriceQuarter.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dieselPriceQuarter.Select(x => x.Type).FirstOrDefault();
                model.listVM.Add(listvm);
                //listOfViewModel.Add(listvm);
            }
            if (dieselPriceWeek != null || dieselPriceWeek != null)
            {
                var listvm = new listViewModel();
                listvm.url = "/DieselWeekModels/_DieselWeekGraph/";
                listvm.orderby = dieselPriceWeek.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dieselPriceWeek.Select(x => x.Type).FirstOrDefault();
                model.listVM.Add(listvm);
                //listOfViewModel.Add(listvm);
            }
            //model.listVM = listOfViewModel;
            return View(model);
        }

        public ActionResult Order()
        {

            var images = db.Imgs.OrderByDescending(x => x.PlacingOrder).ToList();
            var exPageUrl = db.UrlModels.ToList();
            var videos = db.VideoModels.ToList();
            var euros = db.EuroExchangeModels.ToList();
            var dtm = db.DtmModels.ToList();
            var dieselPriceQuarter = db.DieselPriceQuarter.ToList();
            var dieselPriceWeek = db.DieselPriceWeek.ToList();
            //int tempOrderPlacingID = 1;
            var model = new StartModelViewModel();
            model.listVM = new List<listViewModel>();


            Dictionary<decimal, string> dictionary =
        new Dictionary<decimal, string>();

            dictionary.Add(1.1M, "Eurochart");
            dictionary.Add(1.2M, "DTM");
            dictionary.Add(1.3M, "Dieselchart quarter");
            dictionary.Add(1.4M, "Dieselchart week");
            dictionary.Add(2.1M, "Webbadress");
            dictionary.Add(3.1M, "Bild");
            dictionary.Add(4.1M, "Video mp4");
            dictionary.Add(4.2M, "Video YouTube");
            dictionary.Add(0.0M, "Typ hittades inte");


            foreach (var item in images)
            {
                var listvm = new listViewModel();
                listvm.name = String.Format("{0} - {1}", dictionary[item.Type], item.Name);
                listvm.orderby = item.PlacingOrder;
                listvm.type = item.Type;
                listvm.active = item.Active;
                listvm.ID = item.ID;
                //listvm.OrderPlacingID = tempOrderPlacingID;

                ////item.PlacingOrder = 99;
                //item.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);


                //var userFromDb = db.Users.Where(u => u.UserID == user.UserID).First();
                //user.Password = person.Password;
                //user.VerifyPassword = person.VerifyPassword;

                //db.Entry(person).CurrentValues.SetValues(user);
                //db.SaveChanges();
            }
            foreach (var item in exPageUrl)
            {
                var listvm = new listViewModel();
                listvm.name = String.Format("{0} - {1}", dictionary[item.Type], item.urlString);
                listvm.orderby = item.PlacingOrder;
                listvm.type = item.Type;
                listvm.active = item.Active;
                listvm.ID = item.ID;
                //listvm.OrderPlacingID = tempOrderPlacingID;

                //item.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);

            }
            foreach (var item in videos)
            {
                var listvm = new listViewModel();
                listvm.name = String.Format("{0} - {1}", dictionary[item.Type], item.Name);
                listvm.orderby = item.PlacingOrder;
                listvm.Duration = item.Duration;
                listvm.type = item.Type;
                listvm.active = item.Active;
                listvm.ID = item.ID;
                //listvm.OrderPlacingID = tempOrderPlacingID;

                //item.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);

                //db.VideoModels.Add(video);
                //db.SaveChanges();
            }

            if (euros.Count != 0 || euros != null)
            {
                var listvm = new listViewModel();
                listvm.name = dictionary[euros.Select(x => x.Type).FirstOrDefault()];
                listvm.orderby = euros.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = euros.Select(x => x.Type).FirstOrDefault();
                listvm.active = euros.Select(x => x.Active).FirstOrDefault();
                listvm.ID = euros.Select(x => x.ID).FirstOrDefault();
                //listvm.OrderPlacingID = tempOrderPlacingID;

                //var changeOrderPlacing = db.EuroExchangeModels.FirstOrDefault();
                //changeOrderPlacing.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);

            }
            if (dtm.Count != 0 || dtm != null)
            {
                var listvm = new listViewModel();
                listvm.name = dictionary[dtm.Select(x => x.Type).FirstOrDefault()];
                listvm.orderby = dtm.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dtm.Select(x => x.Type).FirstOrDefault();
                listvm.active = dtm.Select(x => x.Active).FirstOrDefault();
                listvm.ID = dtm.Select(x => x.ID).FirstOrDefault();
                //listvm.OrderPlacingID = tempOrderPlacingID;

                //var changeOrderPlacing = db.DtmModels.FirstOrDefault();
                //changeOrderPlacing.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);

            }
            if (dieselPriceQuarter != null || dieselPriceQuarter != null)
            {
                var listvm = new listViewModel();
                listvm.name = dictionary[dieselPriceQuarter.Select(x => x.Type).FirstOrDefault()];
                listvm.orderby = dieselPriceQuarter.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dieselPriceQuarter.Select(x => x.Type).FirstOrDefault();
                listvm.active = dieselPriceQuarter.Select(x => x.Active).FirstOrDefault();
                listvm.ID = dieselPriceQuarter.Select(x => x.ID).FirstOrDefault();
                //listvm.OrderPlacingID = tempOrderPlacingID;

                //var changeOrderPlacing = db.DieselPriceQuarter.FirstOrDefault();
                //changeOrderPlacing.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();

                //tempOrderPlacingID += 1;
                model.listVM.Add(listvm);

            }
            if (dieselPriceWeek != null || dieselPriceWeek != null)
            {
                var listvm = new listViewModel();
                listvm.name = dictionary[dieselPriceWeek.Select(x => x.Type).FirstOrDefault()];
                listvm.orderby = dieselPriceWeek.Select(x => x.PlacingOrder).FirstOrDefault();
                listvm.type = dieselPriceWeek.Select(x => x.Type).FirstOrDefault();
                listvm.active = dieselPriceWeek.Select(x => x.Active).FirstOrDefault();
                listvm.ID = dieselPriceWeek.Select(x => x.ID).FirstOrDefault();

                //listvm.OrderPlacingID = tempOrderPlacingID;
                //var changeOrderPlacing = db.DieselPriceWeek.FirstOrDefault();
                //changeOrderPlacing.OrderPlacingID = tempOrderPlacingID;
                //db.SaveChanges();
                //tempOrderPlacingID += 1;

                model.listVM.Add(listvm);

            }

            return View(model);

        }

        //SetOrderPlacing
        [HttpPost]
        public ActionResult SetOrderPlacing(StartModelViewModel listaMVM)
        {
            List<listViewModel> TempListOrderList = new List<listViewModel>();

            if (ModelState.IsValid)
            {
                foreach (var item in listaMVM.listVM)
                {
                    //var formOrderPlacingID = item.OrderPlacingID;
                    var formOrderBy = item.orderby;
                    var formType = item.type;
                    var formId = item.ID;
                    var formActive = item.active;

                    TempListOrderList.Add(item);
                }
            }

            foreach (var item in TempListOrderList)
            {
                if (item.type == 1.1m) //eurochart
                {
                    var changeOrderPlacing = db.EuroExchangeModels.FirstOrDefault();
                  if(changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                   if(changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 1.2m) //dtm
                {
                    var changeOrderPlacing = db.DtmModels.FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 1.3m) //quarter
                {
                    var changeOrderPlacing = db.DieselPriceQuarter.FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 1.4m) //week
                {
                    var changeOrderPlacing = db.DieselPriceWeek.FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 2.1m) //url
                {
                    var changeOrderPlacing = db.UrlModels.Where(x => x.ID == item.ID).FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 3.1m) //img 
                {
                    var changeOrderPlacing = db.Imgs.Where(x => x.ID == item.ID).FirstOrDefault();
                    var asdf = item.orderby;
                    var asdfasdf = item.active;
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 4.1m) //mp4
                {
                    var changeOrderPlacing = db.VideoModels.Where(x => x.ID == item.ID).FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                else if (item.type == 4.2m) //youtube
                {
                    var changeOrderPlacing = db.VideoModels.Where(x => x.ID == item.ID).FirstOrDefault();
                    if (changeOrderPlacing.PlacingOrder != item.orderby)
                    {
                        changeOrderPlacing.PlacingOrder = item.orderby;
                        db.SaveChanges();
                    }
                    if (changeOrderPlacing.Active != item.active)
                    {
                        changeOrderPlacing.Active = item.active;
                        db.SaveChanges();
                    }
                }
                
            }

            return RedirectToAction("Order");
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