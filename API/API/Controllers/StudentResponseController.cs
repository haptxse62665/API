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
    public class StudentResponseController : ApiController
    {
        private EntityConnection db = new EntityConnection();


        //Get: Infor of notification from DYC/ADmin by host ID and studentID and student response != waitting 
        [Route("api/userSendNotification/inforByStudentID")]
        [HttpGet]
        public List<UserSentNotificationViewModel> GetNotificationByHostID(int studentID)
        {
            List<UserSentNotificationViewModel> listNotification = new List<UserSentNotificationViewModel>();
            var notification = db.tbl_StudentResponse.Where(p => p.StudentID == studentID && p.ContentResponse.Equals("Waiting")).ToList();
            foreach (var item in notification)
            {
                listNotification.Add(new UserSentNotificationViewModel
                {
                    DateCreated = item.TimeResponse,
                    ID = item.ID,
                    LevelEmergency = item.tbl_UserSendNotification.LevelEmergency,
                    Location = item.tbl_UserSendNotification.tbl_Host.Location,
                    TitleNotification = item.tbl_UserSendNotification.TitleNotification
                });
            }

            return listNotification;
        }


        //GET : UPDATE Student response notification from DYC 

        [Route("api/student/updateStudentResponseStatus")]
        [HttpGet]
        //id input is ID of StudentResponse table
        public IHttpActionResult UpdateStudentResponseStatus(int id, string status)
        {
            db.tbl_StudentResponse.Find(id).ContentResponse = status;
            db.SaveChanges();
            return Ok();
        }


        //GET : Create Student sent notification

        [Route("api/student/createStudentSentNotofication")]
        [HttpGet]
        public IHttpActionResult CreateStudentSentNotofication(int studentID, string Coordinate)
        {
            string userID = db.tlb_Student.Find(studentID).AspNetUser.Id;
            tbl_StudentRespondNotification notifi = new tbl_StudentRespondNotification { Coordinate = Coordinate, StudentID = studentID,
                CreatedByUserID = userID, Status = true, DateCreated = DateTime.Now, TimeRequest = DateTime.Now};
            db.tbl_StudentRespondNotification.Add(notifi);
            db.SaveChanges();
            return Ok();
        }



        // GET: api/StudentResponse
        public IQueryable<tbl_StudentResponse> Gettbl_StudentResponse()
        {
            return db.tbl_StudentResponse;
        }

        // GET: api/StudentResponse/5
        [ResponseType(typeof(tbl_StudentResponse))]
        public IHttpActionResult Gettbl_StudentResponse(int id)
        {
            tbl_StudentResponse tbl_StudentResponse = db.tbl_StudentResponse.Find(id);
            if (tbl_StudentResponse == null)
            {
                return NotFound();
            }

            return Ok(tbl_StudentResponse);
        }

        // PUT: api/StudentResponse/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_StudentResponse(int id, tbl_StudentResponse tbl_StudentResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_StudentResponse.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_StudentResponse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_StudentResponseExists(id))
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

        // POST: api/StudentResponse
        [ResponseType(typeof(tbl_StudentResponse))]
        public IHttpActionResult Posttbl_StudentResponse(tbl_StudentResponse tbl_StudentResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_StudentResponse.Add(tbl_StudentResponse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_StudentResponse.ID }, tbl_StudentResponse);
        }

        // DELETE: api/StudentResponse/5
        [ResponseType(typeof(tbl_StudentResponse))]
        public IHttpActionResult Deletetbl_StudentResponse(int id)
        {
            tbl_StudentResponse tbl_StudentResponse = db.tbl_StudentResponse.Find(id);
            if (tbl_StudentResponse == null)
            {
                return NotFound();
            }

            db.tbl_StudentResponse.Remove(tbl_StudentResponse);
            db.SaveChanges();

            return Ok(tbl_StudentResponse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_StudentResponseExists(int id)
        {
            return db.tbl_StudentResponse.Count(e => e.ID == id) > 0;
        }
    }
}