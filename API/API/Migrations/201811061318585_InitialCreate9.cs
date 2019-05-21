namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_UserSendNotification", "DateCreated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_UserSendNotification", "DateCreated", c => c.DateTime(storeType: "date"));
        }
    }
}
