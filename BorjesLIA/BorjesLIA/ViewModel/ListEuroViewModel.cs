using BorjesLIA.Models;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BorjesLIA.ViewModel
{
    public class ListEuroViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
                    var lDiesel = db.EuroExchangeModels.OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lDiesel);
                }
            }

        }
        public class EuroGrafModel
        {
            private ApplicationDbContext _db;
            public EuroGrafModel(ApplicationDbContext db)
            {
                _db = db;
            }
            public IEnumerable<LineChart> euroLineChartArray { get; set; }
            public class LineChart
            {
                public int yPrice { get; set; }
                public string xDate { get; set; }
            }


            public void PopulateRidesPerDayArray()
            {
                List<EuroExchangeModel> ds = new List<EuroExchangeModel>();
                euroLineChartArray = ds.GroupBy(x => x.Date.Day)
                    .OrderBy(x => x.Key)
                    .Select(groupObject =>
                    new LineChart
                    {
                        xDate = groupObject.Key.ToString(),
                    }).ToArray();
            }
        }
    }
}