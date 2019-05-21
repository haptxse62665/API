namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tbl_AdminSendNotification", name: "UserID", newName: "NetUserID");
            RenameIndex(table: "dbo.tbl_AdminSendNotification", name: "IX_UserID", newName: "IX_NetUserID");
            AlterColumn("dbo.tbl_StudentResponse", "DateCreated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.tbl_UserSendNotification", "UsersId");
            DropColumn("dbo.tbl_StudentResponse", "TimeResponse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_StudentResponse", "TimeResponse", c => c.DateTime());
            AddColumn("dbo.tbl_UserSendNotification", "UsersId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.tbl_StudentResponse", "DateCreated", c => c.DateTime(storeType: "date"));
            RenameIndex(table: "dbo.tbl_AdminSendNotification", name: "IX_NetUserID", newName: "IX_UserID");
            RenameColumn(table: "dbo.tbl_AdminSendNotification", name: "NetUserID", newName: "UserID");
        }
    }
}
