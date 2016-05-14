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
        public decimal dieselValue { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd mm:HH:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime Date {
            get; set;
        }
    }
}