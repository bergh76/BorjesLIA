using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
namespace BorjesLIA.ViewModel
{
    public class EuroViewModel
    {

        public EuroExchangeModel AddEuro { get; set; }
        public IEnumerable<EuroExchangeModel> newEuroList { get; set; }


        public Task<List<EuroExchangeModel>> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.EuroExchangeModels == null)
                {
                    return GetData();
                }
                else
                {
                    var lEuro = db.EuroExchangeModels.OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lEuro);
                }
            }
        }      
      
    }
}
