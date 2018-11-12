using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class StudentResponseViewModel
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public DateTime? timeResponse { get; set; }
        public string contentResponse { get; set; }
        public string facultyName { get; set; }

    }
}