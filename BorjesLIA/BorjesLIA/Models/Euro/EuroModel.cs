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
        [Display(Name = "Nytt Europris")]
        public decimal euroValue { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Nytt Datum")]
        public DateTime Date { get; set; }

    }
}