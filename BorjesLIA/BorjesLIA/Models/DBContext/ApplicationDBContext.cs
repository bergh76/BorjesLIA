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

        public System.Data.Entity.DbSet<BorjesLIA.Models.URL.URLModel> UrlModels { get; set; }

        public System.Data.Entity.DbSet<BorjesLIA.Models.Img.Img> Imgs { get; set; }

        public System.Data.Entity.DbSet<BorjesLIA.Models.Diesel.DieselPriceModel> DieselPriceModels { get; set; }

        public System.Data.Entity.DbSet<BorjesLIA.Models.Diesel.DtmModel> DtmModels { get; set; }

        public System.Data.Entity.DbSet<BorjesLIA.Models.Euro.EuroExchangeModel> EuroExchangeModels { get; set; }

        public System.Data.Entity.DbSet<BorjesLIA.Models.Video.VideoModel> VideoModels { get; set; }


    }
}