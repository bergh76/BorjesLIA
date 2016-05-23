using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BorjesLIA.ViewModel
{
    public class DMTViewModel
    {
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public DtmModel AddDtm { get; set; }
        public IEnumerable<DtmModel> newDTMList { get; set; }

        public Task<List<DtmModel>> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.DtmModels == null)
                {
                    return GetData();
                }
                else
                {
                    var lDtm = db.DtmModels.OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lDtm);
                }
            }
        }
    }
}