namespace BorjesLIA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BorjesLIA1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DieselQuarterPriceModels", "EditByUser", c => c.String());
            AddColumn("dbo.DieselWeekModels", "EditByUser", c => c.String());
            AddColumn("dbo.EuroExchangeModels", "EditByUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EuroExchangeModels", "EditByUser");
            DropColumn("dbo.DieselWeekModels", "EditByUser");
            DropColumn("dbo.DieselQuarterPriceModels", "EditByUser");
        }
    }
}
