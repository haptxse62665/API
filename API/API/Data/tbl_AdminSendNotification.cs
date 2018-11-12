namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_AdminSendNotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_AdminSendNotification()
        {
            tbl_AdminNotificationFaculty = new HashSet<tbl_AdminNotificationFaculty>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string NetUserID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ContentRequest { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime DateHazard { get; set; }
        

        [StringLength(128)]
        public string CreatedByUserID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? UpdateDay { get; set; }

      
        [StringLength(128)]
        public string UpdateByUserID { get; set; }

        public bool Status { get; set; }
        
        [ForeignKey ("NetUserID")]
        public virtual AspNetUser AspNetUser { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_AdminNotificationFaculty> tbl_AdminNotificationFaculty { get; set; }
    }
}
