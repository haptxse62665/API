namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_StudentRespondNotification", "CreatedByUserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.tbl_StudentRespondNotification", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_StudentRespondNotification", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_StudentRespondNotification", "CreatedByUserID", c => c.Int());
        }
    }
}
