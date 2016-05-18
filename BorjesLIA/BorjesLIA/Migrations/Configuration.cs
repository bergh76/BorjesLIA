namespace BorjesLIA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>

    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
           // AutomaticMigrationDataLossAllowed = true;
        }
    }
}
