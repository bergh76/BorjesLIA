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
    public class DieselQuarterViewModel
    {
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public Quarters Quarters { get; set; }
        public SortType SortType { get; set; }
        public string ChartName { get; set; }
        public DieselQuarterPriceModel AddQuarterDiesel { get; set; }
        public IEnumerable<DieselQuarterPriceModel> newQuarterDieselList { get; set; }
        public string Year { get; set; }

        //public Task<List<DieselQuarterPriceModel>> GetQuarterData()
        //{
        //    ChartName = "Dieselpris Kvartal";
        //    using (var db = new ApplicationDbContext())
        //    {
        //        if (db.DieselPriceQuarter == null)
        //        {
        //            return GetQuarterData();
        //        }

        //        else
        //        {
        //            Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).Select(x => x.Year).FirstOrDefault();
        //            var lqDiesel = db.DieselPriceQuarter.Where(x => x.Year == Year).OrderBy(x => x.Year).ToList();
        //            return Task.Run(() => lqDiesel);
        //        }
        //    }

        //}

        public string Title { get; set; }
        public DieselQuarterViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                Title = "Kvartalspriser Diesel";
                DataTable = ConstrucDataTabel(db.DieselPriceQuarter.ToList().OrderBy(x => x.Quarter).ToArray());
            }
        }
        public GoogleVisualizationDataTable DataTable { get; set; }

        public GoogleVisualizationDataTable ConstrucDataTabel(DieselQuarterPriceModel[] data)
        {
            ChartName = "Dieselpris Kvartal";
            var dataTable = new GoogleVisualizationDataTable();
            var quarter = data.Select(x => x.Quarter).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            dataTable.AddColumn("Quarter", "string");
            using (var db = new ApplicationDbContext())
            {
                Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).OrderByDescending(x => x.Year).Select(x => x.Year).FirstOrDefault();
                if (Year != null)
                {
                    string[] values = Year.Split(',').Select(sValue => sValue.Trim()).ToArray();
                    foreach (string yItem in values)
                    {
                        dataTable.AddColumn(yItem.ToString(), "number");
                    }

                    foreach (var q in quarter)
                    {
                        var val = new List<object>(new[] { q.ToString() });
                        foreach (var year in values)
                        {
                            var result = data
                                .Where(x => x.Quarter == q && x.Year == year)
                                .Select(x => x.DieselQuarterValue)
                                .SingleOrDefault();
                            val.Add(result);
                        }
                        dataTable.AddRow(val);
                    }
                    return dataTable;
                }
                else
                {
                    return dataTable;
                }
            }
        }
    }
}