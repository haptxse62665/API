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
        }

        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string UsersId { get; set; }

        [Required]
        public string TitleNotification { get; set; }

        [Required]
        public string ContentNotification { get; set; }

        public int HostID { get; set; }

        public string LevelEmergency { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public string CreateByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        public string UpdateByUserID { get; set; }

        public bool Status { get; set; }

        [StringLength(128)]
        public string NetUsersID { get; set; }

        [ForeignKey ("NetUsersID")]
        public virtual AspNetUser AspNetUser { get; set; }

        [ForeignKey ("HostID")]
        public virtual tbl_Host tbl_Host { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_StudentResponse> tbl_StudentResponse { get; set; }
    }
}
