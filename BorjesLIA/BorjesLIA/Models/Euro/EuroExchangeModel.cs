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

        [DataType(DataType.Text)]
        [Display(Name = "År")]
        public string Year { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        public EuroExchangeModel()
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