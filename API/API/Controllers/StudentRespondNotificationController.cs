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

namespace API.Controllers
{
    public class StudentRespondNotificationController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        // GET: api/StudentRespondNotification
        public IQueryable<tbl_StudentRespondNotification> Gettbl_StudentRespondNotification()
        {
            return db.tbl_StudentRespondNotification;
        }

        // GET: api/StudentRespondNotification/5
        [ResponseType(typeof(tbl_StudentRespondNotification))]
        public IHttpActionResult Gettbl_StudentRespondNotification(int id)
        {
            tbl_StudentRespondNotification tbl_StudentRespondNotification = db.tbl_StudentRespondNotification.Find(id);
            if (tbl_StudentRespondNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_StudentRespondNotification);
        }

        // PUT: api/StudentRespondNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_StudentRespondNotification(int id, tbl_StudentRespondNotification tbl_StudentRespondNotification)
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
        [ResponseType(typeof(tbl_StudentRespondNotification))]
        public IHttpActionResult Posttbl_StudentRespondNotification(tbl_StudentRespondNotification tbl_StudentRespondNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_StudentRespondNotification.Add(tbl_StudentRespondNotification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_StudentRespondNotification.ID }, tbl_StudentRespondNotification);
        }

        // DELETE: api/StudentRespondNotification/5
        [ResponseType(typeof(tbl_StudentRespondNotification))]
        public IHttpActionResult Deletetbl_StudentRespondNotification(int id)
        {
            tbl_StudentRespondNotification tbl_StudentRespondNotification = db.tbl_StudentRespondNotification.Find(id);
            if (tbl_StudentRespondNotification == null)
            {
                return NotFound();
            }

            db.tbl_StudentRespondNotification.Remove(tbl_StudentRespondNotification);
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
            return db.tbl_StudentRespondNotification.Count(e => e.ID == id) > 0;
        }
    }
}