namespace BorjesLIA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BorjesLIA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DtmModels", "EditByUser", c => c.String());
            AddColumn("dbo.Imgs", "LoggDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Imgs", "User", c => c.String());
            AddColumn("dbo.Imgs", "EditByUser", c => c.String());
            AddColumn("dbo.URLModels", "LoggDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.URLModels", "User", c => c.String());
            AddColumn("dbo.URLModels", "EditByUser", c => c.String());
            AddColumn("dbo.VideoModels", "LoggDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.VideoModels", "User", c => c.String());
            AddColumn("dbo.VideoModels", "EditByUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoModels", "EditByUser");
            DropColumn("dbo.VideoModels", "User");
            DropColumn("dbo.VideoModels", "LoggDate");
            DropColumn("dbo.URLModels", "EditByUser");
            DropColumn("dbo.URLModels", "User");
            DropColumn("dbo.URLModels", "LoggDate");
            DropColumn("dbo.Imgs", "EditByUser");
            DropColumn("dbo.Imgs", "User");
            DropColumn("dbo.Imgs", "LoggDate");
            DropColumn("dbo.DtmModels", "EditByUser");
        }
    }
}
