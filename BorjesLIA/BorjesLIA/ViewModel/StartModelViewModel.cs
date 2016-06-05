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
    public class StartModelViewModel
    {
        //list which is used to represent object to show in the slider. 
        public List<listViewModel> listVM { get; set; }

        public class listViewModel
        {
            //possible to add fields to match the needs of values to show data in the slider, only restricted to the fields in each respective model.
            public string url { get; set; }
            public string name { get; set; }
            public int orderby { get; set; }
            public bool active { get; set; }
            public DateTime dateTime { get; set; }
            public int Duration { get; set; }
        }
    }
}