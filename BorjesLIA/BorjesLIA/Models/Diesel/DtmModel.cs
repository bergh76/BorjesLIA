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

        [DataType(DataType.Text)]
        [Display(Name = "År")]
        public string Year { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Månad")]
        public string Month { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselDTMValue { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string EditByUser { get; set; }

        [Display(Name = "OrderIndex")]
        public int PlacingOrder { get; set; }

        [Display(Name = "Typ")]
        public int Type { get; set; }

        [Display(Name = "Aktiv")]
        public bool Active { get; set; }

    

        public DtmModel()
        {
            var _user = "default";
            if (!string.IsNullOrEmpty(User) || !string.IsNullOrEmpty(Year) || LoggDate != null)
            {
                LoggDate = DateTime.Now;
                User = HttpContext.Current.User.Identity.Name;
            }
            else
            {  //Quarter = Date
                User = _user;
                LoggDate = DateTime.Now;
            }
        }
    }
}