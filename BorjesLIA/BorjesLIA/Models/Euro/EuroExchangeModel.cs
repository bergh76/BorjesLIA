using BorjesLIA.Models.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Euro
{
    public class EuroExchangeModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Europris")]
        public decimal euroValue { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        //public int ChartType { get; set; }
        public int EuroChartID { get; set; }
        public ChartModel ChartID { get; set;}
    }
}