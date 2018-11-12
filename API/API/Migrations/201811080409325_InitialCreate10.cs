namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_AdminSendNotification", "DateHazard", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_AdminSendNotification", "DateHazard");
        }
    }
}
