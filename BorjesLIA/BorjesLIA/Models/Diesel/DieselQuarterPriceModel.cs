using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Diesel
{
    public class DieselQuarterPriceModel
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
        [Display(Name = "Kvartal")]
        public string Quarter { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pris")]
        public decimal DieselQuarterValue { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        public DieselQuarterPriceModel()
        {
            var _user = "default";
            var _year = DateTime.Now.Year.ToString();
            Date = DateTime.Now;
            if (!string.IsNullOrEmpty(User) || !string.IsNullOrEmpty(Year) || LoggDate != null)
            {
                LoggDate = DateTime.Now;
                User = HttpContext.Current.User.Identity.Name;
                Year = Date.Year.ToString();
            }
            else
            {  //Quarter = Date
                User = _user;
                Year = _year;
                LoggDate = DateTime.Now;
            }
        }
    }
}