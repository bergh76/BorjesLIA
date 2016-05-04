using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Img
{
    public class Img
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Adress till sidan")]
        public string Url { get; set; }

        [Display(Name = "Namn på bilden")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        public int PlacingOrder { get; set; }

        public bool Active { get; set; }
    }
}