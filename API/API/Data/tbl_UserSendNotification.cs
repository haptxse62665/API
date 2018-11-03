namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UserSendNotification
    {
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
    }
}
