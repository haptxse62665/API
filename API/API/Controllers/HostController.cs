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
    public class HostController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        // GET: api/Host
        public IQueryable<tbl_Host> Gettbl_Host()
        {
            return db.tbl_Host;
        }

        // GET: api/Host/5
        [ResponseType(typeof(tbl_Host))]
        public IHttpActionResult Gettbl_Host(int id)
        {
            tbl_Host tbl_Host = db.tbl_Host.Find(id);
            if (tbl_Host == null)
            {
                return NotFound();
            }

            return Ok(tbl_Host);
        }

        // PUT: api/Host/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Host(int id, tbl_Host tbl_Host)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Host.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Host).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_HostExists(id))
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

        // POST: api/Host
        [ResponseType(typeof(tbl_Host))]
        public IHttpActionResult Posttbl_Host(tbl_Host tbl_Host)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Host.Add(tbl_Host);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Host.ID }, tbl_Host);
        }

        // DELETE: api/Host/5
        [ResponseType(typeof(tbl_Host))]
        public IHttpActionResult Deletetbl_Host(int id)
        {
            tbl_Host tbl_Host = db.tbl_Host.Find(id);
            if (tbl_Host == null)
            {
                return NotFound();
            }

            db.tbl_Host.Remove(tbl_Host);
            db.SaveChanges();

            return Ok(tbl_Host);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_HostExists(int id)
        {
            return db.tbl_Host.Count(e => e.ID == id) > 0;
        }
    }
}