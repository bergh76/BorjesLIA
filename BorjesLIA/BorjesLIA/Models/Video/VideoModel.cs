﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Video
{
    public class VideoModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Namn på filmen")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Display(Name = "OrderIndex")]
        public int PlacingOrder { get; set; }

        [Display(Name = "Aktiv")]
        public bool Active { get; set; }

        [Display(Name = "Typ av video")]
        public int Type { get; set; }
    }
}