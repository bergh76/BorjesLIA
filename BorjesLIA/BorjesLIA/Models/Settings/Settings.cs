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


    }
}