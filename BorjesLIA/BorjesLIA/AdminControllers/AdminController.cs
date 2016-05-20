using BorjesLIA.Models;
using BorjesLIA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.AdminControllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            ResultTilesPanelViewModels rtpv = new ResultTilesPanelViewModels();
            rtpv.returnTilePanelValues();
            return View(rtpv);
        }
    }
}