using System;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        [Display(Name = "OrderIndex")]
        public int PlacingOrder { get; set; }

        [Display(Name = "Typ")]
        public decimal Type { get; set; }

        [Display(Name = "Aktiv")]
        public bool Active { get; set; }

      

        public EuroExchangeModel()
        {
            LoggDate = DateTime.Now;
            User = HttpContext.Current.User.Identity.Name;
        }
    }
}