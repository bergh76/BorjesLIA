using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DtmModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "År")]
        public DateTime Year { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Månad")]
        public string Month { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselDTMValue { get; set; }

        public DateTime LoggDate { get; set; }
    }
}