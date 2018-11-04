namespace API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<API.Data.EntityConnection>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "API.Data.EntityConnection";
        }

        protected override void Seed(API.Data.EntityConnection context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
