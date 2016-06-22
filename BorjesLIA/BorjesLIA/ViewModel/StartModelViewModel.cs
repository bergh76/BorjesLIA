using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Video;
using BorjesLIA.Models.URL;
using BorjesLIA.Models.Img;
using BorjesLIA.ViewModel;
using BorjesLIA.Models;

namespace BorjesLIA.ViewModel
{
    //public enum SortSettings
    //{   
    //   Eurochart = 1,
    //   DieselQChart,
    //   DieselWChart,
    //   DMTChart,
    //   Webadress,
    //   Image,
    //   VideoMp4,
    //   VideoYoutube,
    //   VideVimeo,
    //   NotFound
    //}

    public class StartModelViewModel
    {
        //list which is used to represent object to show in the slider. 
        public List<listViewModel> listVM { get; set; }

        public class listViewModel
        {
            public int ID { get; set; }
            public string Url { get; set; }
            public string ObjectName { get; set; }
            public int PlacingOrder { get; set; }
            public bool Active { get; set; }
            public DateTime Date { get; set; }
            public int Duration { get; set; }
            public int Type { get; set; }
            public string TypeName { get; set; }
     
        }
    }
}