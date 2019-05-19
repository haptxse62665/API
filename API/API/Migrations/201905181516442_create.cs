namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.Boolean(nullable: false));
        }
    }
}
