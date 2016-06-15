using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models.Charts
{
    public enum ChartType
    {
        Linje = 1,
        Stapel,
        Kolumn,
        Area
    }
    public enum SortType
    {
        Datum = 1,
        Vecka,
        Kvartal,
        Månad
    }
}