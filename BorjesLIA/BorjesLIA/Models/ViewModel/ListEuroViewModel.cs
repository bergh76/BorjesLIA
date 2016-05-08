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