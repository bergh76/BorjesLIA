using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Video;
using BorjesLIA.Models.URL;
using BorjesLIA.Models.Img;

namespace BorjesLIA.ViewModels
{
    public class StartModelViewModel
    {
        public List<listViewModel> listVM { get; set; }
    }

    public class listViewModel
    {
        public string url { get; set; }
        public string name { get; set; }
        public int orderby { get; set; }

        public DateTime dateTime { get; set; }

    }
}