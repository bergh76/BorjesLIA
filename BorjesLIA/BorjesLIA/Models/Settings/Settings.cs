using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Settings
{
    public class Settings
    {
        public int ID { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Year { get; set; }

        [DataType(DataType.Text)]
        public int ChartType { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime LoggDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Användare")]
        public string User { get; set; }

        public Settings()
        {            
            string _user = HttpContext.Current.User.Identity.Name.ToString();
            string _loggDate = DateTime.Now.ToString();
            if (string.IsNullOrEmpty(_user))
            {
                User = "init";
                LoggDate = DateTime.Now;
            }
            else {
                User = _user;
                LoggDate = DateTime.Now;
            }
        }

    }
}