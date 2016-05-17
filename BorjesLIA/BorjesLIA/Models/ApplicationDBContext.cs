using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<URL.URLModel> UrlModels { get; set; }

        public System.Data.Entity.DbSet<Img.Img> Imgs { get; set; }

        public System.Data.Entity.DbSet<Diesel.DieselWeekModel> DieselPriceWeek { get; set; }

        public System.Data.Entity.DbSet<Diesel.DieselQuarterPriceModel> DieselPriceQuarter{ get; set; }

        public System.Data.Entity.DbSet<Diesel.DtmModel> DtmModels { get; set; }

        public System.Data.Entity.DbSet<Euro.EuroExchangeModel> EuroExchangeModels { get; set; }

        public System.Data.Entity.DbSet<Video.VideoModel> VideoModels { get; set; }

        public System.Data.Entity.DbSet<Charts.ChartModel> ChartTypeList { get; set; }

        //public System.Data.Entity.DbSet<BorjesLIA.Models.StartModel> StartModels { get; set; }
    }
}