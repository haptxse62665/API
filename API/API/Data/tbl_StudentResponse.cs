namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using API.Data;

    public partial class tbl_StudentResponse
    {
        public int ID { get; set; }
        
        public int StudentID { get; set; }

        public int UserSendNotificationID { get; set; }

        public DateTime? TimeResponse { get; set; }

        public string ContentResponse { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public int? CreatedByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        public int? UpdateByUserID { get; set; }

        public bool? Status { get; set; }

        [ForeignKey("StudentID")]
        public virtual tlb_Student tlb_Student { get; set; }

        [ForeignKey("UserSendNotificationID")]
        public virtual tbl_UserSendNotification tbl_UserSendNotification { get; set; }
    }
}
