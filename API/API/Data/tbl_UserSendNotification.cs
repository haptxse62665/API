namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UserSendNotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_UserSendNotification()
        {
            tbl_StudentResponse = new HashSet<tbl_StudentResponse>();
            tbl_NotificationHost = new HashSet<tbl_NotificationHost>();
        }

        public int ID { get; set; }
        
        [Required]
        public string TitleNotification { get; set; }

        [Required]
        public string ContentNotification { get; set; }
        
        public string LevelEmergency { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateHazard  { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? DateCreated { get; set; }

        [StringLength(128)]
        public string CreateByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        [StringLength(128)]
        public string UpdateByUserID { get; set; }

        public bool Status { get; set; }

        [StringLength(128)]
        public string NetUsersID { get; set; }

        [ForeignKey ("NetUsersID")]
        public virtual AspNetUser AspNetUser { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_StudentResponse> tbl_StudentResponse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_NotificationHost> tbl_NotificationHost { get; set; }
    }
}
