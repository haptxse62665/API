namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_AdminSendNotification", "FacultyID", "dbo.tbl_Faculty");
            DropForeignKey("dbo.tbl_UserSendNotification", "HostID", "dbo.tbl_Host");
            DropIndex("dbo.tbl_AdminSendNotification", new[] { "FacultyID" });
            DropIndex("dbo.tbl_UserSendNotification", new[] { "HostID" });
            CreateTable(
                "dbo.tbl_AdminNotificationFaculty",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FacultyID = c.Int(nullable: false),
                        AdminSendNotificationID = c.Int(nullable: false),
                        DateCreated = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateByUserID = c.String(maxLength: 128),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.String(maxLength: 128),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tbl_Faculty", t => t.FacultyID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_AdminSendNotification", t => t.AdminSendNotificationID, cascadeDelete: true)
                .Index(t => t.FacultyID)
                .Index(t => t.AdminSendNotificationID);
            
            CreateTable(
                "dbo.tbl_NotificationHost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HostID = c.Int(nullable: false),
                        UserSendNotificationID = c.Int(nullable: false),
                        DateCreated = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateByUserID = c.String(maxLength: 128),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.String(maxLength: 128),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tbl_Host", t => t.HostID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_UserSendNotification", t => t.UserSendNotificationID, cascadeDelete: true)
                .Index(t => t.HostID)
                .Index(t => t.UserSendNotificationID);
            
            DropColumn("dbo.tbl_AdminSendNotification", "FacultyID");
            DropColumn("dbo.tbl_UserSendNotification", "HostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_UserSendNotification", "HostID", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_AdminSendNotification", "FacultyID", c => c.Int(nullable: false));
            DropForeignKey("dbo.tbl_AdminNotificationFaculty", "AdminSendNotificationID", "dbo.tbl_AdminSendNotification");
            DropForeignKey("dbo.tbl_NotificationHost", "UserSendNotificationID", "dbo.tbl_UserSendNotification");
            DropForeignKey("dbo.tbl_NotificationHost", "HostID", "dbo.tbl_Host");
            DropForeignKey("dbo.tbl_AdminNotificationFaculty", "FacultyID", "dbo.tbl_Faculty");
            DropIndex("dbo.tbl_NotificationHost", new[] { "UserSendNotificationID" });
            DropIndex("dbo.tbl_NotificationHost", new[] { "HostID" });
            DropIndex("dbo.tbl_AdminNotificationFaculty", new[] { "AdminSendNotificationID" });
            DropIndex("dbo.tbl_AdminNotificationFaculty", new[] { "FacultyID" });
            DropTable("dbo.tbl_NotificationHost");
            DropTable("dbo.tbl_AdminNotificationFaculty");
            CreateIndex("dbo.tbl_UserSendNotification", "HostID");
            CreateIndex("dbo.tbl_AdminSendNotification", "FacultyID");
            AddForeignKey("dbo.tbl_UserSendNotification", "HostID", "dbo.tbl_Host", "ID", cascadeDelete: true);
            AddForeignKey("dbo.tbl_AdminSendNotification", "FacultyID", "dbo.tbl_Faculty", "ID", cascadeDelete: true);
        }
    }
}
