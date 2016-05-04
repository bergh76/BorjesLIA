using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models
{
    public class testModel
    {
        [Key]
        public int ID { get; set; }
        public string test { get; set; }
    }
}