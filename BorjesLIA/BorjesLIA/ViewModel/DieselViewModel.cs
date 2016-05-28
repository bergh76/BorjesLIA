﻿using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Settings;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public Task<List<DieselWeekModel>> GetWeekData()
        {
            Name = "Dieselpris Vecka";
            using (var db = new ApplicationDbContext())
            {
                if (db.DieselPriceWeek == null)
                {
                    return GetWeekData();
                  
                }
                else if (db.Settings.Where(x => x.Name == Name).Select(x => x.Year).FirstOrDefault() == "Alla")
                {
                    var lwAllDiesel = db.DieselPriceWeek.ToList();
                    return Task.Run(() => lwAllDiesel);
                }
                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.Name).Select(x => x.Year).FirstOrDefault();
                    var lwDiesel = db.DieselPriceWeek.Where(x=> x.Year == Year).OrderBy(x => x.Year).ToList();
                    return Task.Run(() => lwDiesel);
                }
            }
        }
    }
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
                else if (db.Settings.Where(x => x.Name == Name).Select(x => x.Year).FirstOrDefault() == "Alla")
                {
                    var lqAllDiesel = db.DieselPriceQuarter.ToList();
                    return Task.Run(() => lqAllDiesel);
                }
                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.Name).Select(x => x.Year).FirstOrDefault();
                    var lqDiesel = db.DieselPriceQuarter.Where(x => x.Year == Year).OrderBy(x => x.Year).ToList();
                    return Task.Run(() => lqDiesel);
                }
            }
        }
    }
}