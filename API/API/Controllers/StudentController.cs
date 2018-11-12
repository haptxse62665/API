using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using API.Data;
using API.Models;
using Microsoft.AspNet.Identity.Owin;

namespace API.Controllers
{
    public class StudentController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        // GET: api/Student
        public IQueryable<tlb_Student> Gettlb_Student()
        {
            return db.tlb_Student;
        }
        //GET : GET lisst student arrival 
        [Route("api/DYCAndAdmin/listStudentArrivalOrNot")]
        [HttpGet]
        public List<StudentViewModel> GetListStudentArrivalOrNot(int facultyId, string countryName)
        {
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            if (facultyId > 0)
            {
                var students = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.CountryName == countryName && p.FacultyId == facultyId && p.Status).ToList();
                foreach (var item in students)
                {
                    studentList.Add(new StudentViewModel
                    {
                        FullName = item.AspNetUser.FullName,
                        Arrival = item.Arrival,
                        UserName = item.AspNetUser.UserName
                    });
                }
                return studentList;
            }
            else
            {
                var students = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.CountryName == countryName && p.Status).ToList();
                foreach (var item in students)
                {
                    studentList.Add(new StudentViewModel
                    {
                        FullName = item.AspNetUser.FullName,
                        Arrival = item.Arrival,
                        UserName = item.AspNetUser.UserName
                    });
                }
                return studentList;
            }
        }
        //Get: List Host and munber of student
        [Route("api/DYCAndAdmin/listHStudentInHost")]
        [HttpGet]
        public List<StudentViewModel> GetListStudent(int facultyId, int hostID)
        {
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            if (facultyId > 0)
            {
                var students = db.tlb_Student.Where(p => p.HostID == hostID && p.FacultyId == facultyId && p.Status).ToList();
                foreach (var item in students)
                {
                    studentList.Add(new StudentViewModel
                    {
                        StudentID = item.StudentID,
                        FullName = item.AspNetUser.FullName,
                        FacultyName = item.tbl_Faculty.FacultyName,
                        UserName = item.AspNetUser.UserName
                    });
                }
                return studentList;
            }
            else
            {
                var students = db.tlb_Student.Where(p => p.HostID == hostID && p.Status).ToList();
                foreach (var item in students)
                {
                    studentList.Add(new StudentViewModel
                    {
                        StudentID = item.StudentID,
                        FullName = item.AspNetUser.FullName,
                        FacultyName = item.tbl_Faculty.FacultyName,
                        UserName = item.AspNetUser.UserName
                    });
                }
                return studentList;
            }
        }
        //GET: student infor by user name of tbl_User
        [Route("api/student/info")]
        [HttpGet]
        public StudentViewModel GetStudentInfor(string username)
        {
            var _userManager = Request.GetOwinContext().GetUserManager<AspNetUserManager>();
            var user = _userManager.FindByEmailAsync(username).Result;
            var student = db.tlb_Student.Where(p => p.NetUsersID == user.Id && p.Status).FirstOrDefault();
            return new StudentViewModel
            {
                Id = student.Id,
                NewPhoneNumber = student.NewPhoneNumber,
                HostID = student.HostID,
                Arrival = student.Arrival,
                ContactNumber = student.ContactNumber,
                StudentID = student.StudentID,
                FullName = student.AspNetUser.FullName,
                Email = student.AspNetUser.Email,
                CountryName = student.tbl_Host.tbl_Country.CountryName,    
                CountryID = student.tbl_Host.CountryID,
                FacultyID = student.FacultyId,
                FacultyName = student.tbl_Faculty.FacultyName,
                HostName = student.tbl_Host.HostName
            };
        }


        //GET : List country and number of arival student  

        [Route("api/student/updateArrival")]
        [HttpPost]
        public IHttpActionResult UpdateArrival(int id)
        {
            db.tlb_Student.Find(id).Arrival = true;
            db.SaveChanges();
            return Ok();
        }

        //Parth : Update new phone number of DY student 
        [Route("api/student/updateNewPhoneNumber")]
        [HttpPost]
        public IHttpActionResult UpdateNewPhoneNumber(int id, string phoneNumber)
        {
           db.tlb_Student.Find(id).NewPhoneNumber = phoneNumber;
                db.SaveChanges();
            return Ok();
        }

        // GET: api/Student/5
        [ResponseType(typeof(tlb_Student))]
        public IHttpActionResult Gettlb_Student(int id)
        {
            tlb_Student tlb_Student = db.tlb_Student.Find(id);
            if (tlb_Student == null)
            {
                return NotFound();
            }

            return Ok(tlb_Student);
        }

        // PUT: api/Student/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttlb_Student(int id, tlb_Student tlb_Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlb_Student.Id)
            {
                return BadRequest();
            }

            db.Entry(tlb_Student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tlb_StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Student
        [ResponseType(typeof(tlb_Student))]
        public IHttpActionResult Posttlb_Student(tlb_Student tlb_Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tlb_Student.Add(tlb_Student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tlb_Student.Id }, tlb_Student);
        }

        // DELETE: api/Student/5
        [ResponseType(typeof(tlb_Student))]
        public IHttpActionResult Deletetlb_Student(int id)
        {
            tlb_Student tlb_Student = db.tlb_Student.Find(id);
            if (tlb_Student == null)
            {
                return NotFound();
            }

            db.tlb_Student.Remove(tlb_Student);
            db.SaveChanges();

            return Ok(tlb_Student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tlb_StudentExists(int id)
        {
            return db.tlb_Student.Count(e => e.Id == id) > 0;
        }
    }
}