using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models
{
    public class StartModel
    {

        public List<DieselPriceModel> dieselPriceList {get; set;}
        public List<DtmModel> dtmPriceList { get; set; }
        public List<EuroExchangeModel> euroExchangeList { get; set; }
        public List<UrlModel> urlList { get; set; }

    }
}