using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DieselPriceModel
    {
        public int ID { get; set; }
        public decimal dieselPrice { get; set; }
        public DateTime dateNewDieselPrice { get; set; }
    }
}