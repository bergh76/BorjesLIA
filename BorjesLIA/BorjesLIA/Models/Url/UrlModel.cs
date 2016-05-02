using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models
{
    public class UrlModel
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Ny Url")]
        public string urlString { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Nytt Datum")]
        public DateTime dateUrl { get; set; }
    }
}