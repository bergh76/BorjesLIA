using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
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
                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.Name).OrderByDescending( x=> x.Year).Select(x => x.Year).FirstOrDefault();
                    var lEuro = db.EuroExchangeModels.Where(x => x.Date.Year.ToString() == Year).OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lEuro);
                }
            }
        }
    }
}
