namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_StudentResponse",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        UserSendNotificationID = c.Int(nullable: false),
                        TimeResponse = c.DateTime(),
                        ContentResponse = c.String(),
                        DateCreated = c.DateTime(storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tbl_UserSendNotification", t => t.UserSendNotificationID, cascadeDelete: true)
                .ForeignKey("dbo.tlb_Student", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.UserSendNotificationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_StudentResponse", "StudentID", "dbo.tlb_Student");
            DropForeignKey("dbo.tbl_StudentResponse", "UserSendNotificationID", "dbo.tbl_UserSendNotification");
            DropIndex("dbo.tbl_StudentResponse", new[] { "UserSendNotificationID" });
            DropIndex("dbo.tbl_StudentResponse", new[] { "StudentID" });
            DropTable("dbo.tbl_StudentResponse");
        }
    }
}
