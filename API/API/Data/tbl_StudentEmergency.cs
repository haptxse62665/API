namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_StudentEmergency
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        [Column(TypeName = "Datetime2")]
        public DateTime TimeRequest { get; set; }

        public string Coordinate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        [StringLength(128)]
        public string CreatedByUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDay { get; set; }

        public int? UpdateByUserID { get; set; }
        
        public bool Status { get; set; }

        [ForeignKey ("StudentID")]
        public virtual tlb_Student tlb_Student { get; set; }
    }
}
