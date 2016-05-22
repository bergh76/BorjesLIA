namespace BorjesLIA.Migrations
{
    using Models;
    using Models.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>

    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Seeds initial data for settings file
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {
            IList<Settings> intialSettings = new List<Settings>();

            intialSettings.Add(new Settings() { Name = "Dieselpris Vecka", ChartType = 1, Year = DateTime.Now.Year , LoggDate = DateTime.Now, User =""});
            intialSettings.Add(new Settings() { Name = "Dieselpris Kvartal", ChartType = 1, Year = DateTime.Now.Year, LoggDate = DateTime.Now, User = "" }); ;
            intialSettings.Add(new Settings() { Name = "Eurokurs", ChartType = 1, Year = DateTime.Now.Year, LoggDate = DateTime.Now, User = "" });
            intialSettings.Add(new Settings() { Name = "Drivmedelstillägg", ChartType = 1, Year = DateTime.Now.Year, LoggDate = DateTime.Now, User = "" });

            foreach (Settings std in intialSettings)
                context.Settings.Add(std);

            base.Seed(context);
        }

    }
}
