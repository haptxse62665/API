using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string NewPhoneNumber { get; set; }
        public int HostID { get; set; }
        public bool Arrival { get; set; }
        public string ContactNumber { get; set; }
        public string StudentID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public int FacultyID { get; set; }
        public string CountryName { get; set; }
        public string FacultyName { get; set; }
        public string HostName { get; set; }
    }
}