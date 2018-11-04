using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DYCViewModel : UserViewModel
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string DYCID { get; set; }
    }
}