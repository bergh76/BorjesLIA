using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Charts
{
    public class ChartModel
    {
        [Key]
        public int ID { get; set; }
        public string ChartName { get; set; }
        public int ChartID { get; set; }
    }
}