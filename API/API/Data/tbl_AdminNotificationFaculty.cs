namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_AdminNotificationFaculty
    {

        public int ID { get; set; }

        public int FacultyID { get; set; }

        public int AdminSendNotificationID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? DateCreated { get; set; }

        [StringLength(128)]
        public string CreateByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        [StringLength(128)]
        public string UpdateByUserID { get; set; }

        public bool Status { get; set; }

        [ForeignKey("FacultyID")]
        public virtual tbl_Faculty tbl_Faculty { get; set; }

        [ForeignKey("AdminSendNotificationID")]
        public virtual tbl_AdminSendNotification tbl_AdminSendNotification { get; set; }

    }
}
