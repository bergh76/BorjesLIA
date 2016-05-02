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

        [DataType(DataType.Currency)]
        [Display(Name = "Nytt DTMpris")]
        public decimal dtmPrice { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Nytt Datum")]
        public DateTime dateNewDtmPrice { get; set; }
    }
}