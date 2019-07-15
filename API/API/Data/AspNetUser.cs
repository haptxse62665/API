namespace API.Data
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class AspNetUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            tbl_DYC = new HashSet<tbl_DYC>();
            tbl_UserSendNotification = new HashSet<tbl_UserSendNotification>();
            tlb_Student = new HashSet<tlb_Student>();
            tbl_AdminSendNotification = new HashSet<tbl_AdminSendNotification>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public DateTime? DateCreate { get; set; }

        public int? CreatedByUserID { get; set; }

        public DateTime? UpdateDay { get; set; }

        public int? UpdateByUserID { get; set; }
        
        public bool? Status { get; set; }

     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DYC> tbl_DYC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UserSendNotification> tbl_UserSendNotification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tlb_Student> tlb_Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_AdminSendNotification> tbl_AdminSendNotification { get; set; }

    }
}
