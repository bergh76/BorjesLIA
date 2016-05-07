using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DieselPriceModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Dieselpris")]
        public decimal dieselPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime dateNewDieselPrice { get; set; }
    }
}