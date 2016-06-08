using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BorjesLIA.ViewModel
{
    //public class DieselWeekViewModel
    //{
    //    public IEnumerable<Settings> settings { get; set; }
    //    public ChartType ChartType { get; set; }
    //    public DieselWeekModel AddWeekDiesel { get; set; }
    //    public IEnumerable<DieselWeekModel> newWeekDieselList { get; set; }
    //    public string Year { get; set; }
    //    public string Name { get; set; }
    //    public Task<List<DieselWeekModel>> GetWeekData()
    //    {
    //        Name = "Dieselpris Vecka";
    //        using (var db = new ApplicationDbContext())
    //        {
    //            if (db.DieselPriceWeek == null)
    //            {
    //                return GetWeekData();

    //            }
    //            else if (db.Settings.Where(x => x.Name == Name).Select(x => x.Year).FirstOrDefault() == "Alla")
    //            {
    //                var lwAllDiesel = db.DieselPriceWeek.ToList();
    //                return Task.Run(() => lwAllDiesel);
    //            }
    //            else
    //            {
    //                Year = db.Settings.ToList().Where(x => x.Name == this.Name).Select(x => x.Year).FirstOrDefault();
    //                var lwDiesel = db.DieselPriceWeek.Where(x => x.Year == Year).OrderBy(x => x.Year).ToList();
    //                return Task.Run(() => lwDiesel);
    //            }
    //        }
    //    }
    //}



    public class DieselQuarterViewModel
    {
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public DieselQuarterPriceModel AddQuarterDiesel { get; set; }
        public IEnumerable<DieselQuarterPriceModel> newQuarterDieselList { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public Task<List<DieselQuarterPriceModel>> GetQuarterData()

        {
            Name = "Dieselpris Kvartal";
            using (var db = new ApplicationDbContext())
            {
                if (db.DieselPriceQuarter == null)
                {
                    return GetQuarterData();
                }

                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.Name).Select(x => x.Year).FirstOrDefault();
                    var lqDiesel = db.DieselPriceQuarter.Where(x => x.Year == Year).OrderBy(x => x.Year).ToList();
                    return Task.Run(() => lqDiesel);
                }
            }

        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DieselQuarterViewModel()
        {
             using (var db = new ApplicationDbContext())
            {
                Title = "Kvartalspriser Diesel";
                Subtitle = "År";
                DataTable = ConstrucDataTabel(db.DieselPriceQuarter.ToList().OrderBy(x => x.Quarter).ToArray());
            }
        }
        public GoogleVisualizationDataTable DataTable { get; set; }

        public GoogleVisualizationDataTable ConstrucDataTabel(DieselQuarterPriceModel[] data)
        {
           
            var dataTable = new GoogleVisualizationDataTable();
            var quarters = data.Select(x => x.Quarter).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            dataTable.AddColumn("Quarter", "string");
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
            foreach (var year in years)
            {
                dataTable.AddColumn(year.ToString(), "number");
            }
            foreach (var quarter in quarters)
            {
                var val = new List<object>(new[] { quarter });
                foreach (var year in years)
                {
                    var result = data
                        .Where(x => x.Quarter == quarter && x.Year == year)
                        .Select(x => x.DieselQuarterValue)
                        .SingleOrDefault();
                    val.Add(result);
                }
                dataTable.AddRow(val);
            }           
            return dataTable;
        }

    }
}