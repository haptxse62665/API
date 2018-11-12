namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_AdminSendNotification", "CreatedByUserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.tbl_AdminSendNotification", "UpdateByUserID", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_AdminSendNotification", "UpdateByUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.tbl_AdminSendNotification", "CreatedByUserID", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
