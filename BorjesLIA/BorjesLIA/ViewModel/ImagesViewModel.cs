using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models;
using BorjesLIA.Models.Img;
using System.Threading.Tasks;

namespace BorjesLIA.ViewModel
{
    public class ImagesViewModel
    {
        public Img AddImage { get; set; }
        public IEnumerable<Img> newImageList { get; set; }


        public List<Img> GetData()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Imgs == null)
                {
                    return GetData();
                }
                else
                {
                    var lImages = db.Imgs.OrderBy(x => x.ID).ToList();
                    return lImages;
                }
            }

        }
    }
}