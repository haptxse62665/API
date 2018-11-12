using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class AdminSentNotifiViewModel
    {
        public string Title { get; set; }
        
        public string ContentRequest { get; set; }

        public string FacultyName { get; set; }
        
        public DateTime DateHazard { get; set; }

        public DateTime? DateCreate { get; set; }

    }
}