using BorjesLIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BorjesLIA.Models.Img;

namespace BorjesLIA.Helper
{
    public class SliderHelper
    {
      public string returnPartialViewUrl(dynamic s)
        {
            string[] whatISMyString = s;

            foreach (var item in whatISMyString)
            {
                var whatIsIt = item;
            }

            //retunerar en hårdkodad-string tills vidare, bara för att sidan ska snurra. 
            string stringUrl = "~/Views/EuroExchangeModels/_EuroLineGraph.cshtml";
            return stringUrl;
        }
    }
}