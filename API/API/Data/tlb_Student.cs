namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using API.Data;

    public partial class tlb_Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tlb_Student()
        {
            tbl_StudentRespondNotification = new HashSet<tbl_StudentRespondNotification>();
            tbl_StudentResponse = new HashSet<tbl_StudentResponse>();
        }

        [Key]
        public int Id { get; set; }

        public int FacultyId { get; set; }

        public string NewPhoneNumber { get; set; }

        public int HostID { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        public string TypeOfDY { get; set; }

        public bool Arrival { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimeStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimeFinish { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNumber { get; set; }

        public string NextOfKin { get; set; }

        public int? KinNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDay { get; set; }

        public int? CreatedByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        public int? UpdateByUserID { get; set; }

        public bool Status { get; set; }

        [StringLength(128)]
        public string NetUsersID { get; set; }

        [StringLength(128)]
        public string StudentID { get; set; }

        [ForeignKey ("NetUsersID")]
        public virtual AspNetUser AspNetUser { get; set; }

        [ForeignKey ("FacultyId")]
        public virtual tbl_Faculty tbl_Faculty { get; set; }

        [ForeignKey ("HostID")]
        public virtual tbl_Host tbl_Host { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_StudentRespondNotification> tbl_StudentRespondNotification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_StudentResponse> tbl_StudentResponse { get; set; }
    }
}
