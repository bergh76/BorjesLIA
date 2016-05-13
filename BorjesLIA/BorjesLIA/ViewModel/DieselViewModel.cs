using BorjesLIA.Models;
using BorjesLIA.Models.Diesel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.ViewModel
{
    public class DieselViewModel
    {
        public DieselPriceModel AddDiesel { get; set; }
        public IEnumerable<DieselPriceModel> newDieselList { get; set; }


        public Task<List<DieselPriceModel>> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.DieselPriceModels == null)
                {
                    return GetData();
                }
                else
                {
                    var lDiesel = db.DieselPriceModels.OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lDiesel);
                }
            }
        }
    }
}