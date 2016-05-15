using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DieselQuarterPriceModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "År")]
        public int Year { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Kvartal")]
        public string Quarter { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselQuarterValue { get; set; }

        public DateTime LoggDate { get; set; }
        
    }
}