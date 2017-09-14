using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DbSet<URL.URLModel> UrlModels { get; set; }

        public DbSet<Img.Img> Imgs { get; set; }

        public DbSet<Diesel.DieselWeekModel> DieselPriceWeek { get; set; }

        public DbSet<Diesel.DieselQuarterPriceModel> DieselPriceQuarter { get; set; }

        public DbSet<Diesel.DtmModel> DtmModels { get; set; }

        public DbSet<Euro.EuroExchangeModel> EuroExchangeModels { get; set; }

        public DbSet<Video.VideoModel> VideoModels { get; set; }

        public DbSet<Settings.Settings> Settings { get; set; }



    }
}