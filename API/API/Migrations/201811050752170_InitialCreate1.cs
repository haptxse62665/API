namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_AdminSendNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        ContentRequest = c.String(nullable: false),
                        FacultyID = c.Int(nullable: false),
                        CreatedByUserID = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateDay = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdateByUserID = c.String(nullable: false, maxLength: 128),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Faculty", t => t.FacultyID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.FacultyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_AdminSendNotification", "FacultyID", "dbo.tbl_Faculty");
            DropForeignKey("dbo.tbl_AdminSendNotification", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.tbl_AdminSendNotification", new[] { "FacultyID" });
            DropIndex("dbo.tbl_AdminSendNotification", new[] { "UserID" });
            DropTable("dbo.tbl_AdminSendNotification");
        }
    }
}
