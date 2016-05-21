using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorjesLIA.ViewModel
{
    public class EuroViewModel
    {
        public ChartType ChartType { get; set; }
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


        public Settings Settings { get; set; }

        public IEnumerable<Settings> settings { get; set; }


        //public Task<List<EuroExchangeModel>> getDataAsync()
        //{
        //    using (var db = new ApplicationDbContext())
        //    {
        //        if (db.DieselPriceWeek == null)
        //        {
        //            return getDataAsync();
        //        }
        //        else
        //        {
        //            var lEuro = db.EuroExchangeModels.OrderBy(x => x.Date).ToList();
        //            return Task.Run(() => lEuro);
        //        }
        //    }
        //}

    }
}
