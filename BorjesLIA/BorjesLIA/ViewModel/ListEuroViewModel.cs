using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Euro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorjesLIA.ViewModel
{
    public class EuroViewModel
    {

        public EuroExchangeModel AddEuro { get; set; }
        public IEnumerable<EuroExchangeModel> newEuroList { get; set; }
        public ChartModel ChartName { get; set; }
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
                    var lEuro = db.EuroExchangeModels.OrderBy(x => x.Date).ToList();
                   return Task.Run(() => lEuro);
                }
            }
            
        }      
    }
}
