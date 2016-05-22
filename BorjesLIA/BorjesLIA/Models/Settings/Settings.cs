using BorjesLIA.Models.Charts;
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
        public int Year { get; set; }

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
            LoggDate = DateTime.Now;
            User = System.Security.Principal.WindowsIdentity.GetCurrent().ToString();
        }
    }
}