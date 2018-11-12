namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_StudentRespondNotification", newName: "tbl_StudentEmergency");
            AddColumn("dbo.tbl_UserSendNotification", "DateHazard", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_UserSendNotification", "DateHazard");
            RenameTable(name: "dbo.tbl_StudentEmergency", newName: "tbl_StudentRespondNotification");
        }
    }
}
