using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models
{
    public class StartModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DieselPriceModel DieselSlide { get; set; }
        public DtmModel DmtSlide { get; set; }
        public GetEuroExchangeModel EuroSlide { get; set; }
        public UrlModel UrlSlide { get; set; }
        public VideoModel VideoSlide { get; set; }
    }
}