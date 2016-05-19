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


        public List<URLModel> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Imgs == null)
                {
                    return GetData();
                }
                else
                {
                    var lURL = db.UrlModels.OrderBy(x => x.ID).ToList();
                    return lURL;
                }
            }

        }
    }
}