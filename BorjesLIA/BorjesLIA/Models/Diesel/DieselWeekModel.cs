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
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "År")]
        public string Year { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Månad")]
        public string Month { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Vecka")]
        public int Week { get; set; }

        //[DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselWeekValue { get; set; }

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
        public decimal Type { get; set; }

        [Display(Name = "Aktiv")]
        public bool Active { get; set; }

 

        public DieselWeekModel()
        {
            var _user = HttpContext.Current.User.Identity.Name.ToString();
            Date = DateTime.Now;
            if (string.IsNullOrEmpty(_user) && string.IsNullOrEmpty(Year) && LoggDate == null)
            {
                //Quarter = Date
                User = "default";
                LoggDate = DateTime.Now;
            }
            else
            {
                LoggDate = DateTime.Now;
                User = _user;
            }
        }
    }
}