using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.ViewModel
{
    public class ListEuroViewModel
    {
       
        public List<EuroExchangeModel> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.EuroExchangeModels == null)
                {
                    return GetData();
                }
                else
                {
                    var lDiesel = db.EuroExchangeModels.OrderBy(x => x.Date).ToList();
                    return lDiesel;
                }
            }

        }
    }
}