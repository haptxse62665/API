using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Data;
using API.Models;

namespace API.Controllers
{
    public class StudentEmergencyController : ApiController
    {
        private EntityConnection db = new EntityConnection();



        //GET : Create Student sent notification
        [Route("api/student/createStudentSentNotofication")]
        [HttpPost]
        public IHttpActionResult CreateStudentSentNotofication(int studentID, string Coordinate)
        {
            string userID = db.tlb_Student.Find(studentID).AspNetUser.Id;
            tbl_StudentEmergency notifi = new tbl_StudentEmergency
            {
                Coordinate = Coordinate,
                StudentID = studentID,
                CreatedByUserID = userID,
                Status = true,
                DateCreated = DateTime.Now,
                TimeRequest = DateTime.Now
            };
            db.tbl_StudentEmergency.Add(notifi);
            db.SaveChanges();
            return Ok();
        }
        //GET : Get list student emergency by faculty

        [Route("api/studentResponse/studentEmergency")]
        [HttpGet]
        public List<StudentEmergencyViewModel> GetListStudentEmergency(int facultyID)
        {
            List<StudentEmergencyViewModel> list = new List<StudentEmergencyViewModel>();
            if (facultyID > 0)
            {
                var studentEmergencyList = db.tbl_StudentEmergency.Where(p => p.tlb_Student.FacultyId == facultyID && p.Status).ToList();
                foreach (var item in studentEmergencyList)
                {
                    list.Add(new StudentEmergencyViewModel
                    {
                        Coordinate = item.Coordinate,
                        FullName = item.tlb_Student.AspNetUser.FullName,
                        PhoneNumber = item.tlb_Student.NewPhoneNumber,
                        StudentID = item.StudentID,
                        ID = item.ID,
                        DateCreate = item.TimeRequest
                    });
                }
                return list;
            }
            else
            {
                var studentEmergencyList = db.tbl_StudentEmergency.Where(p => p.Status).ToList();
                foreach (var item in studentEmergencyList)
                {
                    list.Add(new StudentEmergencyViewModel
                    {
                        Coordinate = item.Coordinate,
                        FullName = item.tlb_Student.AspNetUser.FullName,
                        PhoneNumber = item.tlb_Student.NewPhoneNumber,
                        StudentID = item.StudentID,
                        UserName = item.tlb_Student.AspNetUser.UserName,
                        ID = item.ID,
                        DateCreate = item.TimeRequest
                    });
                }
                return list;
            }

        }

        //GET : Delete emergency case 
        [Route("api/student/remmoveStudentEmergency")]
        [HttpPost]
        public IHttpActionResult RemmoveStudentEmergency(int studentEmergencyID)
        {
            db.tbl_StudentEmergency.Find(studentEmergencyID).Status = false;
            db.SaveChanges();
            return Ok();
        }
        
        // GET: api/StudentRespondNotification
        public IQueryable<tbl_StudentEmergency> Gettbl_StudentRespondNotification()
        {
            return db.tbl_StudentEmergency;
        }

        // GET: api/StudentRespondNotification/5
        [ResponseType(typeof(tbl_StudentEmergency))]
        public IHttpActionResult Gettbl_StudentRespondNotification(int id)
        {
            tbl_StudentEmergency tbl_StudentRespondNotification = db.tbl_StudentEmergency.Find(id);
            if (tbl_StudentRespondNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_StudentRespondNotification);
        }

        // PUT: api/StudentRespondNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_StudentRespondNotification(int id, tbl_StudentEmergency tbl_StudentRespondNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_StudentRespondNotification.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_StudentRespondNotification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_StudentRespondNotificationExists(id))
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

        // POST: api/StudentRespondNotification
        [ResponseType(typeof(tbl_StudentEmergency))]
        public IHttpActionResult Posttbl_StudentRespondNotification(tbl_StudentEmergency tbl_StudentRespondNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_StudentEmergency.Add(tbl_StudentRespondNotification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_StudentRespondNotification.ID }, tbl_StudentRespondNotification);
        }

        // DELETE: api/StudentRespondNotification/5
        [ResponseType(typeof(tbl_StudentEmergency))]
        public IHttpActionResult Deletetbl_StudentRespondNotification(int id)
        {
            tbl_StudentEmergency tbl_StudentRespondNotification = db.tbl_StudentEmergency.Find(id);
            if (tbl_StudentRespondNotification == null)
            {
                return NotFound();
            }

            db.tbl_StudentEmergency.Remove(tbl_StudentRespondNotification);
            db.SaveChanges();

            return Ok(tbl_StudentRespondNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_StudentRespondNotificationExists(int id)
        {
            return db.tbl_StudentEmergency.Count(e => e.ID == id) > 0;
        }
    }
}