using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BorjesLIA.ViewModel
{
    public class EuroViewModel
    {
        public ChartType ChartType { get; set; }
        public IEnumerable<Settings> settings { get; set; }
        public EuroExchangeModel AddEuro { get; set; }
        public IEnumerable<EuroExchangeModel> newEuroList { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public Task<List<EuroExchangeModel>> GetData()
        {
            Name = "Eurokurs";
            using (var db = new ApplicationDbContext())
            {

                if (db.EuroExchangeModels == null)
                {
                    return GetData();
                }

               
                else if (db.Settings.Where(x => x.Name == Name).Select(x => x.Year).FirstOrDefault() == "Alla")
                {
                    var lAllEuro = db.EuroExchangeModels.ToList();
                    return Task.Run(() => lAllEuro);
                }

                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.Name).OrderByDescending( x=> x.Year).Select(x => x.Year).FirstOrDefault();
                    var lEuro = db.EuroExchangeModels.Where(x => x.Date.Year.ToString() == Year).OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lEuro);
                }
            }
        }


        public string Title { get; set; }
        public string Subtitle { get; set; }
        public GoogleVisualizationDataTable DataTable { get; set; }
        public EuroViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                Title = "Euroindex";
                Subtitle = "År";
                DataTable = ConstrucDataTabel(db.EuroExchangeModels.ToList().OrderBy(x => x.Date).ToArray());
            }
        }

        public GoogleVisualizationDataTable ConstrucDataTabel(EuroExchangeModel[] data)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            
            var dataTable = new GoogleVisualizationDataTable();
            var month = data.Select(x => x.Date.Month).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            
            dataTable.AddColumn("Month", "string");
            /**
            // makes clusters of quarters
            //foreach (var q in quarters)
            //{
            //    dataTable.AddColumn(q.ToString(), "string");
            //}
            //foreach (var y in years)
            //{
            //    var val = new List<object>(new[] { y });
            //    foreach (var q in quarters)
            //    {
            //        var result = data
            //            .Where(x => x.Quarter == q && x.Year == y)
            //            .Select(x => x.DieselQuarterValue)
            //            .SingleOrDefault();
            //        val.Add(result);
            //    }
            //    dataTable.AddRow(val);
            //}
            // Makes clusters of years
            **/
            foreach (var yItem in years)
            {
                dataTable.AddColumn(yItem.ToString(), "number");
            }
            foreach (var m in month)
            {
                var strMonthName = mfi.GetMonthName(m).ToString();
                var val = new List<object>(new[] { strMonthName });
                foreach (var year in years)
                {
                    var result = data
                        .Where(x => x.Date.Month == m && x.Year == year)
                        .Select(x => x.euroValue)
                        .SingleOrDefault();
                    val.Add(result);
                }
                dataTable.AddRow(val);
            }
            return dataTable;
        }
    }
}
