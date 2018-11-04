namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tbl_Country",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tbl_Host",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HostName = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        CountryID = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Image = c.String(),
                        Type = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tbl_Country", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.tbl_UserSendNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsersId = c.String(nullable: false, maxLength: 128),
                        TitleNotification = c.String(nullable: false),
                        ContentNotification = c.String(nullable: false),
                        HostID = c.Int(nullable: false),
                        LevelEmergency = c.String(),
                        DateCreated = c.DateTime(storeType: "date"),
                        CreateByUserID = c.String(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.String(),
                        Status = c.Boolean(nullable: false),
                        NetUsersID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.NetUsersID)
                .ForeignKey("dbo.tbl_Host", t => t.HostID, cascadeDelete: true)
                .Index(t => t.HostID)
                .Index(t => t.NetUsersID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        DateCreate = c.DateTime(),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(),
                        UpdateByUserID = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tbl_DYC",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacultyId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        NetUsersID = c.String(maxLength: 128),
                        DYCID = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NetUsersID)
                .ForeignKey("dbo.tbl_Faculty", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.NetUsersID);
            
            CreateTable(
                "dbo.tbl_Faculty",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FacultyName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tlb_Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacultyId = c.Int(nullable: false),
                        NewPhoneNumber = c.String(),
                        HostID = c.Int(nullable: false),
                        Semester = c.String(nullable: false),
                        TypeOfDY = c.String(nullable: false),
                        Arrival = c.Boolean(nullable: false),
                        TimeStart = c.DateTime(nullable: false, storeType: "date"),
                        TimeFinish = c.DateTime(nullable: false, storeType: "date"),
                        ContactPerson = c.String(),
                        ContactNumber = c.String(),
                        NextOfKin = c.String(),
                        KinNumber = c.Int(),
                        CreatedDay = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        NetUsersID = c.String(maxLength: 128),
                        StudentID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NetUsersID)
                .ForeignKey("dbo.tbl_Faculty", t => t.FacultyId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Host", t => t.HostID, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.HostID)
                .Index(t => t.NetUsersID);
            
            CreateTable(
                "dbo.tbl_StudentRespondNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        TimeRequest = c.DateTime(nullable: false, storeType: "date"),
                        Coordinate = c.String(),
                        DateCreated = c.DateTime(nullable: false, storeType: "date"),
                        CreatedByUserID = c.Int(),
                        UpdateDay = c.DateTime(storeType: "date"),
                        UpdateByUserID = c.Int(),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tlb_Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_UserSendNotification", "HostID", "dbo.tbl_Host");
            DropForeignKey("dbo.tbl_UserSendNotification", "NetUsersID", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_StudentRespondNotification", "StudentID", "dbo.tlb_Student");
            DropForeignKey("dbo.tlb_Student", "HostID", "dbo.tbl_Host");
            DropForeignKey("dbo.tlb_Student", "FacultyId", "dbo.tbl_Faculty");
            DropForeignKey("dbo.tlb_Student", "NetUsersID", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_DYC", "FacultyId", "dbo.tbl_Faculty");
            DropForeignKey("dbo.tbl_DYC", "NetUsersID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_Host", "CountryID", "dbo.tbl_Country");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.tbl_StudentRespondNotification", new[] { "StudentID" });
            DropIndex("dbo.tlb_Student", new[] { "NetUsersID" });
            DropIndex("dbo.tlb_Student", new[] { "HostID" });
            DropIndex("dbo.tlb_Student", new[] { "FacultyId" });
            DropIndex("dbo.tbl_DYC", new[] { "NetUsersID" });
            DropIndex("dbo.tbl_DYC", new[] { "FacultyId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.tbl_UserSendNotification", new[] { "NetUsersID" });
            DropIndex("dbo.tbl_UserSendNotification", new[] { "HostID" });
            DropIndex("dbo.tbl_Host", new[] { "CountryID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.tbl_StudentRespondNotification");
            DropTable("dbo.tlb_Student");
            DropTable("dbo.tbl_Faculty");
            DropTable("dbo.tbl_DYC");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tbl_UserSendNotification");
            DropTable("dbo.tbl_Host");
            DropTable("dbo.tbl_Country");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
