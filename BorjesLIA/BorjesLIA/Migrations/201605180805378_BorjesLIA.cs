namespace BorjesLIA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BorjesLIA : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StartModels", "DieselQuarterSlide_ID", "dbo.DieselQuarterPriceModels");
            DropForeignKey("dbo.StartModels", "DieselWeekSlide_ID", "dbo.DieselWeekModels");
            DropForeignKey("dbo.StartModels", "DmtSlide_ID", "dbo.DtmModels");
            DropForeignKey("dbo.StartModels", "EuroSlide_ID", "dbo.EuroExchangeModels");
            DropForeignKey("dbo.StartModels", "ImageSlide_ID", "dbo.Imgs");
            DropForeignKey("dbo.StartModels", "UrlSlide_ID", "dbo.URLModels");
            DropForeignKey("dbo.StartModels", "VideoSlide_ID", "dbo.VideoModels");
            DropIndex("dbo.StartModels", new[] { "DieselQuarterSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "DieselWeekSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "DmtSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "EuroSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "ImageSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "UrlSlide_ID" });
            DropIndex("dbo.StartModels", new[] { "VideoSlide_ID" });
            CreateTable(
                "dbo.ChartModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ChartName = c.String(),
                        ChartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DieselQuarterPriceModels", "DieselQuarterChartID", c => c.Int(nullable: false));
            AddColumn("dbo.DieselWeekModels", "DieselWeekChartID", c => c.Int(nullable: false));
            AddColumn("dbo.DtmModels", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.DtmModels", "DieselDTMChartID", c => c.Int(nullable: false));
            DropColumn("dbo.DieselQuarterPriceModels", "LoggDate");
            DropColumn("dbo.DieselWeekModels", "loggDate");
            DropColumn("dbo.DtmModels", "Year");
            DropColumn("dbo.DtmModels", "Month");
            DropTable("dbo.StartModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StartModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DieselQuarterSlide_ID = c.Int(),
                        DieselWeekSlide_ID = c.Int(),
                        DmtSlide_ID = c.Int(),
                        EuroSlide_ID = c.Int(),
                        ImageSlide_ID = c.Int(),
                        UrlSlide_ID = c.Int(),
                        VideoSlide_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DtmModels", "Month", c => c.String());
            AddColumn("dbo.DtmModels", "Year", c => c.DateTime(nullable: false));
            AddColumn("dbo.DieselWeekModels", "loggDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DieselQuarterPriceModels", "LoggDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.DtmModels", "DieselDTMChartID");
            DropColumn("dbo.DtmModels", "Date");
            DropColumn("dbo.DieselWeekModels", "DieselWeekChartID");
            DropColumn("dbo.DieselQuarterPriceModels", "DieselQuarterChartID");
            DropTable("dbo.ChartModels");
            CreateIndex("dbo.StartModels", "VideoSlide_ID");
            CreateIndex("dbo.StartModels", "UrlSlide_ID");
            CreateIndex("dbo.StartModels", "ImageSlide_ID");
            CreateIndex("dbo.StartModels", "EuroSlide_ID");
            CreateIndex("dbo.StartModels", "DmtSlide_ID");
            CreateIndex("dbo.StartModels", "DieselWeekSlide_ID");
            CreateIndex("dbo.StartModels", "DieselQuarterSlide_ID");
            AddForeignKey("dbo.StartModels", "VideoSlide_ID", "dbo.VideoModels", "ID");
            AddForeignKey("dbo.StartModels", "UrlSlide_ID", "dbo.URLModels", "ID");
            AddForeignKey("dbo.StartModels", "ImageSlide_ID", "dbo.Imgs", "ID");
            AddForeignKey("dbo.StartModels", "EuroSlide_ID", "dbo.EuroExchangeModels", "ID");
            AddForeignKey("dbo.StartModels", "DmtSlide_ID", "dbo.DtmModels", "ID");
            AddForeignKey("dbo.StartModels", "DieselWeekSlide_ID", "dbo.DieselWeekModels", "ID");
            AddForeignKey("dbo.StartModels", "DieselQuarterSlide_ID", "dbo.DieselQuarterPriceModels", "ID");
        }
    }
}
