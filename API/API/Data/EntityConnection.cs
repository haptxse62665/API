namespace API.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;

    public partial class EntityConnection : IdentityDbContext<AspNetUser>
    {
        public EntityConnection()
            : base("name=EntityConnection")
        {
        }
        
        public virtual DbSet<tbl_Country> tbl_Country { get; set; }
        public virtual DbSet<tbl_DYC> tbl_DYC { get; set; }
        public virtual DbSet<tbl_Faculty> tbl_Faculty { get; set; }
        public virtual DbSet<tbl_Host> tbl_Host { get; set; }
        public virtual DbSet<tbl_StudentRespondNotification> tbl_StudentRespondNotification { get; set; }
        public virtual DbSet<tbl_UserSendNotification> tbl_UserSendNotification { get; set; }
        public virtual DbSet<tlb_Student> tlb_Student { get; set; }
        public static EntityConnection Create()
        {
            return new EntityConnection();
        }

        public virtual void Commit()
        {
            try
            {

                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AspNetUser>()
        //        .HasMany(e => e.tbl_DYC)
        //        .WithOptional(e => e.AspNetUser)
        //        .HasForeignKey(e => e.NetUsersID);

        //    modelBuilder.Entity<AspNetUser>()
        //        .HasMany(e => e.tbl_UserSendNotification)
        //        .WithOptional(e => e.AspNetUser)
        //        .HasForeignKey(e => e.NetUsersID);

        //    modelBuilder.Entity<AspNetUser>()
        //        .HasMany(e => e.tlb_Student)
        //        .WithOptional(e => e.AspNetUser)
        //        .HasForeignKey(e => e.NetUsersID);

        //    modelBuilder.Entity<tbl_Country>()
        //        .Property(e => e.CountryName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Country>()
        //        .Property(e => e.ImageURL)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Country>()
        //        .HasMany(e => e.tbl_Host)
        //        .WithOptional(e => e.tbl_Country)
        //        .HasForeignKey(e => e.tbl_Country_ID);

        //    modelBuilder.Entity<tbl_Faculty>()
        //        .Property(e => e.FacultyName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Faculty>()
        //        .HasMany(e => e.tbl_DYC)
        //        .WithRequired(e => e.tbl_Faculty)
        //        .HasForeignKey(e => e.FacultyId)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<tbl_Faculty>()
        //        .HasMany(e => e.tlb_Student)
        //        .WithRequired(e => e.tbl_Faculty)
        //        .HasForeignKey(e => e.FacultyId)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .Property(e => e.HostName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .Property(e => e.Location)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .Property(e => e.Email)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .Property(e => e.Image)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .Property(e => e.Type)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .HasMany(e => e.tbl_UserSendNotification)
        //        .WithRequired(e => e.tbl_Host)
        //        .HasForeignKey(e => e.HostID)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<tbl_Host>()
        //        .HasMany(e => e.tlb_Student)
        //        .WithRequired(e => e.tbl_Host)
        //        .HasForeignKey(e => e.HostID)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<tbl_StudentRespondNotification>()
        //        .Property(e => e.Coordinate)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_StudentRespondNotification>()
        //        .Property(e => e.Status)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_UserSendNotification>()
        //        .Property(e => e.TitleNotification)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_UserSendNotification>()
        //        .Property(e => e.ContentNotification)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_UserSendNotification>()
        //        .Property(e => e.LevelEmergency)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_UserSendNotification>()
        //        .Property(e => e.CreateByUserID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tbl_UserSendNotification>()
        //        .Property(e => e.UpdateByUserID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.NewPhoneNumber)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.Semester)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.TypeOfDY)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.ContactPerson)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.ContactNumber)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.NextOfKin)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.TopicHost)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .Property(e => e.TopicCountry)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<tlb_Student>()
        //        .HasMany(e => e.tbl_StudentRespondNotification)
        //        .WithOptional(e => e.tlb_Student)
        //        .HasForeignKey(e => e.NetUsersID);
        //}
    }
}
