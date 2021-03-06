namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_StudentRespondNotification", "TimeRequest", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_StudentRespondNotification", "TimeRequest", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
