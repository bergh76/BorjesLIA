using BorjesLIA.Models;
using BorjesLIA.Models.Diesel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorjesLIA.ViewModel
{
    public class DieselViewModel
    {
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