using BorjesLIA.Models;
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
        public Settings Settings { get; set; }
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public DieselWeekModel AddWeekDiesel { get; set; }
        public IEnumerable<DieselWeekModel> newWeekDieselList { get; set; }
        public Task<List<DieselWeekModel>> GetWeekData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.DieselPriceWeek == null)
                {
                    return GetWeekData();
                }
                else
                {
                    var lwDiesel = db.DieselPriceWeek.OrderBy(x => x.Week).ToList();
                    return Task.Run(() => lwDiesel);
                }
            }
        }
    }
    public class DieselQuarterViewModel
    {
        public Settings Settings { get; set; }
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public DieselQuarterPriceModel AddQuarterDiesel { get; set; }
        public IEnumerable<DieselQuarterPriceModel> newQuarterDieselList { get; set; }
        public Task<List<DieselQuarterPriceModel>> GetQuaerterData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.DieselPriceWeek == null)
                {
                    return GetQuaerterData();
                }
                else
                {
                    var lqDiesel = db.DieselPriceQuarter.OrderBy(x => x.Quarter).ToList();
                    return Task.Run(() => lqDiesel);
                }
            }
        }
    }
}