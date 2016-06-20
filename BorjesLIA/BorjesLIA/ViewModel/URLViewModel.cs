using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models;
using BorjesLIA.Models.URL;

namespace BorjesLIA.ViewModel
{
    public class URLViewModel
    {
        public URLModel AddUrl{ get; set; }
        public IEnumerable<URLModel> newUrlList { get; set; }
    }
}