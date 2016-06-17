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
    public class DieselWeekViewModel
    {
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public DieselWeekModel AddWeekDiesel { get; set; }
        public IEnumerable<DieselWeekModel> newWeekDieselList { get; set; }
        public string Year { get; set; }
        public string ChartName { get; set; }
        //public Task<List<DieselWeekModel>> GetWeekData()
        //{
        //    ChartName = "Dieselpris Vecka";
        //    using (var db = new ApplicationDbContext())
        //    {
        //        if (db.DieselPriceWeek == null)
        //        {
        //            return GetWeekData();

        //        }
        //        else if (db.Settings.Where(x => x.Name == ChartName).Select(x => x.Year).FirstOrDefault() == "Alla")
        //        {
        //            var lwAllDiesel = db.DieselPriceWeek.ToList();
        //            return Task.Run(() => lwAllDiesel);
        //        }
        //        else
        //        {
        //            Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).Select(x => x.Year).FirstOrDefault();
        //            var lwDiesel = db.DieselPriceWeek.Where(x => x.Year == Year).OrderBy(x => x.Year).ToList();
        //            return Task.Run(() => lwDiesel);
        //        }
        //    }
        //}


        public string Title { get; set; }
        public GoogleVisualizationDataTable DataTable { get; set; }
        public DieselWeekViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                Title = "Veckopriser Diesel";
                DataTable = ConstrucDataTabel(db.DieselPriceWeek.ToList().OrderBy(x => x.Week).ToArray());
            }
        }

        public GoogleVisualizationDataTable ConstrucDataTabel(DieselWeekModel[] data)
        {
            ChartName = "Dieselpris Vecka";
            var dataTable = new GoogleVisualizationDataTable();
            var week = data.Select(x => x.Week).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            dataTable.AddColumn("Week", "string");
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

                    foreach (var w in week)
                    {
                        var val = new List<object>(new[] { w.ToString() });
                        foreach (var year in values)
                        {
                            var result = data
                                .Where(x => x.Week == w && x.Year == year)
                                .Select(x => Convert.ToDecimal(x.DieselWeekValue))
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


