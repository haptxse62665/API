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
    public class AdminSendNotificationController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        [Route("api/userSendNotification/adminSentNotifiList")]
        [HttpGet]
        public List<AdminSentNotifiViewModel> GetAdminSentNotifiList(int facultyID)
        {
            List<AdminSentNotifiViewModel> result = new List<AdminSentNotifiViewModel>();
            if(facultyID == 0)
            {
                var listNotifi = db.tbl_AdminSendNotification.Where(p => p.Status).ToList();
                foreach (var item in listNotifi)
                {
                    if(db.tbl_AdminNotificationFaculty.Where(p => p.AdminSendNotificationID == item.ID && p.Status).Count() >1)
                    result.Add(new AdminSentNotifiViewModel {
                        ContentRequest = item.ContentRequest,
                        DateHazard = item.DateHazard,
                        FacultyName = "All faculty",
                        Title = item.Title,
                        DateCreate = item.DateCreated
                    });
                    else
                        result.Add(new AdminSentNotifiViewModel
                        {
                            ContentRequest = item.ContentRequest,
                            DateHazard = item.DateHazard,
                            FacultyName = db.tbl_AdminNotificationFaculty.Where(p => p.AdminSendNotificationID == item.ID && p.Status).FirstOrDefault().tbl_Faculty.FacultyName,
                            Title = item.Title,
                            DateCreate = item.DateCreated
                        });
                }
            }
            else
            {
                var list = db.tbl_AdminNotificationFaculty.Where(p => p.FacultyID == facultyID && p.Status).ToList();
                foreach (var item in list)
                {
                    result.Add(new AdminSentNotifiViewModel {
                        ContentRequest = db.tbl_AdminSendNotification.Find(item.AdminSendNotificationID).ContentRequest,
                        DateHazard = db.tbl_AdminSendNotification.Find(item.AdminSendNotificationID).DateHazard,
                        FacultyName = db.tbl_Faculty.Find(facultyID).FacultyName,
                        Title = db.tbl_AdminSendNotification.Find(item.AdminSendNotificationID).Title,
                        DateCreate = db.tbl_AdminSendNotification.Find(item.AdminSendNotificationID).DateCreated
                    });
                }
            }
            return result;
        }







            // GET: api/AdminSendNotification
            public IQueryable<tbl_AdminSendNotification> Gettbl_AdminSendNotification()
        {
            return db.tbl_AdminSendNotification;
        }

        // GET: api/AdminSendNotification/5
        [ResponseType(typeof(tbl_AdminSendNotification))]
        public IHttpActionResult Gettbl_AdminSendNotification(int id)
        {
            tbl_AdminSendNotification tbl_AdminSendNotification = db.tbl_AdminSendNotification.Find(id);
            if (tbl_AdminSendNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_AdminSendNotification);
        }

        // PUT: api/AdminSendNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_AdminSendNotification(int id, tbl_AdminSendNotification tbl_AdminSendNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_AdminSendNotification.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_AdminSendNotification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AdminSendNotificationExists(id))
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

        // POST: api/AdminSendNotification
        [ResponseType(typeof(tbl_AdminSendNotification))]
        public IHttpActionResult Posttbl_AdminSendNotification(tbl_AdminSendNotification tbl_AdminSendNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_AdminSendNotification.Add(tbl_AdminSendNotification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_AdminSendNotification.ID }, tbl_AdminSendNotification);
        }

        // DELETE: api/AdminSendNotification/5
        [ResponseType(typeof(tbl_AdminSendNotification))]
        public IHttpActionResult Deletetbl_AdminSendNotification(int id)
        {
            tbl_AdminSendNotification tbl_AdminSendNotification = db.tbl_AdminSendNotification.Find(id);
            if (tbl_AdminSendNotification == null)
            {
                return NotFound();
            }

            db.tbl_AdminSendNotification.Remove(tbl_AdminSendNotification);
            db.SaveChanges();

            return Ok(tbl_AdminSendNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AdminSendNotificationExists(int id)
        {
            return db.tbl_AdminSendNotification.Count(e => e.ID == id) > 0;
        }
    }
}