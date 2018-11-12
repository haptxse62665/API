namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Host
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Host()
        {
            tbl_NotificationHost = new HashSet<tbl_NotificationHost>();
            tlb_Student = new HashSet<tlb_Student>();
        }

        public int ID { get; set; }

        [Required]
        public string HostName { get; set; }

        [Required]
        public string Location { get; set; }

        public int CountryID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public string Image { get; set; }

        [Required]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        public int? CreatedByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        public int? UpdateByUserID { get; set; }

        public bool Status { get; set; }

        [ForeignKey ("CountryID")]
        public virtual tbl_Country tbl_Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_NotificationHost> tbl_NotificationHost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tlb_Student> tlb_Student { get; set; }
    }
}
