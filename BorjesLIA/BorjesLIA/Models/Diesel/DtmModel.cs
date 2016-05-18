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

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselDTMValue { get; set; }

        [Display(Name = "ChartID")]
        public int DieselDTMChartID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        public DtmModel()
        {
            LoggDate = DateTime.Now;
        }
    }
}