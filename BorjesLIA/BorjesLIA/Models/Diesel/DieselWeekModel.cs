using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DieselWeekModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "År")]
        public int Year { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Vecka")]
        public string Week { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselWeekValue { get; set; }

        [Display(Name = "ChartID")]
        public int DieselWeekChartID { get; set; }


    }
}