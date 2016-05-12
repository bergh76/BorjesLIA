using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Video;
using BorjesLIA.Models.URL;
using BorjesLIA.Models.Img;
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
        public EuroExchangeModel EuroSlide { get; set; }
        public URLModel UrlSlide { get; set; }
        public VideoModel VideoSlide { get; set; }
        public BorjesLIA.Models.Img.Img ImageSlide { get; set; }
    }
}