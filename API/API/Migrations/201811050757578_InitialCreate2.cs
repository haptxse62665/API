namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_AdminSendNotification", "DateCreated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tbl_AdminSendNotification", "UpdateDay", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_AdminSendNotification", "UpdateDay", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tbl_AdminSendNotification", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
