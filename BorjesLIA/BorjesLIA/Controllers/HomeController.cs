using BorjesLIA.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ContentSliderViewModel model = new ContentSliderViewModel()
            //{
            //    ImagesList = new System.IO.DirectoryInfo(Server.MapPath("~/Images/contentslider/")).GetFiles()
            //};

            //return View(model);

            return View();
        }
    }
}