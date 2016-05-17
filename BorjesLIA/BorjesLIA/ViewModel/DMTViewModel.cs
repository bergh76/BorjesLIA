using BorjesLIA.Models;
using BorjesLIA.Models.Diesel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BorjesLIA.ViewModel
{
    public class DMTViewModel
    {

        public DtmModel AddDtm { get; set; }
        public IEnumerable<DtmModel> newDTMList { get; set; }
        public Task<List<DtmModel>> GetDtmData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.DtmModels == null)
                {
                    return GetDtmData();
                }
                else
                {
                    var lDtm = db.DtmModels.OrderBy(x => x.Month).ToList();
                    return Task.Run(() => lDtm);
                }
            }
        }
    }
}