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

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        public DieselWeekModel()
        {
            LoggDate = DateTime.Now;
            User = System.Security.Claims.ClaimsPrincipal.Current.Identity.Name.ToString();
        }
    }
}