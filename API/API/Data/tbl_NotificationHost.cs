namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_NotificationHost
    {

        public int ID { get; set; }
       
        public int HostID { get; set; }

        public int UserSendNotificationID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? DateCreated { get; set; }

        [StringLength(128)]
        public string CreateByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        [StringLength(128)]
        public string UpdateByUserID { get; set; }

        public bool Status { get; set; }
        
        [ForeignKey ("HostID")]
        public virtual tbl_Host tbl_Host { get; set; }

        [ForeignKey ("UserSendNotificationID")]
        public virtual tbl_UserSendNotification tbl_UserSendNotification { get; set; }
        
    }
}
