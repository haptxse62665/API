namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_UserSendNotification", "CreateByUserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.tbl_UserSendNotification", "UpdateByUserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.tbl_StudentResponse", "CreatedByUserID", c => c.String());
            AlterColumn("dbo.tbl_StudentResponse", "UpdateByUserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_StudentResponse", "UpdateByUserID", c => c.Int());
            AlterColumn("dbo.tbl_StudentResponse", "CreatedByUserID", c => c.Int());
            AlterColumn("dbo.tbl_UserSendNotification", "UpdateByUserID", c => c.String());
            AlterColumn("dbo.tbl_UserSendNotification", "CreateByUserID", c => c.String());
        }
    }
}
