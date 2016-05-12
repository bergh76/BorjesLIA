using BorjesLIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models.Img;

namespace BorjesLIA.Helper
{
    public class AddToSlider
    {
        Img someImgObj = new Img();
        
        public Img addValue(string someUrl)
        {
            someImgObj.Url = someUrl;
            return someImgObj;
        }
        
        public List<StartModel> AddToListHelper()
        {
            List<StartModel> list = new List<StartModel>();
            someImgObj = addValue("~/Images/contentslider/landscape-615429_960_720.jpg");

            list.Add(new StartModel { ImageSlide = someImgObj });
           
            
            return list;
        }
    }
}