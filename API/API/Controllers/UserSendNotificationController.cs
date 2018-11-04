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
    public class UserSendNotificationController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        //Get: Infor of notification from DYC/ADmin by host ID
        [Route("api/userSendNotification/inforByHostID")]
        [HttpGet]
        public List<UserSentNotificationViewModel> GetUser(int hostID)
        {
            List<UserSentNotificationViewModel> listNotification = new List<UserSentNotificationViewModel>();
            var notification = db.tbl_UserSendNotification.Where(p => p.HostID == hostID).ToList();
            foreach (var item in notification)
            {
                listNotification.Add( new UserSentNotificationViewModel { DateCreated = item.DateCreated,
                    ID =item.ID, LevelEmergency= item.LevelEmergency, Location= item.tbl_Host.Location,
                TitleNotification = item.TitleNotification});
            }
           
            return listNotification;
        }



    













        // GET: api/UserSendNotification
        public IQueryable<tbl_UserSendNotification> Gettbl_UserSendNotification()
        {
            return db.tbl_UserSendNotification;
        }

        // GET: api/UserSendNotification/5
        [ResponseType(typeof(tbl_UserSendNotification))]
        public IHttpActionResult Gettbl_UserSendNotification(int id)
        {
            tbl_UserSendNotification tbl_UserSendNotification = db.tbl_UserSendNotification.Find(id);
            if (tbl_UserSendNotification == null)
            {
                return NotFound();
            }

            return Ok(tbl_UserSendNotification);
        }

        // PUT: api/UserSendNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_UserSendNotification(int id, tbl_UserSendNotification tbl_UserSendNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_UserSendNotification.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_UserSendNotification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_UserSendNotificationExists(id))
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

        // POST: api/UserSendNotification
        [ResponseType(typeof(tbl_UserSendNotification))]
        public IHttpActionResult Posttbl_UserSendNotification(tbl_UserSendNotification tbl_UserSendNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_UserSendNotification.Add(tbl_UserSendNotification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_UserSendNotification.ID }, tbl_UserSendNotification);
        }

        // DELETE: api/UserSendNotification/5
        [ResponseType(typeof(tbl_UserSendNotification))]
        public IHttpActionResult Deletetbl_UserSendNotification(int id)
        {
            tbl_UserSendNotification tbl_UserSendNotification = db.tbl_UserSendNotification.Find(id);
            if (tbl_UserSendNotification == null)
            {
                return NotFound();
            }

            db.tbl_UserSendNotification.Remove(tbl_UserSendNotification);
            db.SaveChanges();

            return Ok(tbl_UserSendNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UserSendNotificationExists(int id)
        {
            return db.tbl_UserSendNotification.Count(e => e.ID == id) > 0;
        }
    }
}