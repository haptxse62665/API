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
    public class DYCController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        // GET: api/DYC
        public IQueryable<tbl_DYC> Gettbl_DYC()
        {
            return db.tbl_DYC;
        }

        // GET: api/DYC/5
        [ResponseType(typeof(tbl_DYC))]
        public IHttpActionResult Gettbl_DYC(int id)
        {
            tbl_DYC tbl_DYC = db.tbl_DYC.Find(id);
            if (tbl_DYC == null)
            {
                return NotFound();
            }

            return Ok(tbl_DYC);
        }

        // PUT: api/DYC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_DYC(int id, tbl_DYC tbl_DYC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_DYC.Id)
            {
                return BadRequest();
            }

            db.Entry(tbl_DYC).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_DYCExists(id))
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

        // POST: api/DYC
        [ResponseType(typeof(tbl_DYC))]
        public IHttpActionResult Posttbl_DYC(tbl_DYC tbl_DYC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_DYC.Add(tbl_DYC);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_DYC.Id }, tbl_DYC);
        }

        // DELETE: api/DYC/5
        [ResponseType(typeof(tbl_DYC))]
        public IHttpActionResult Deletetbl_DYC(int id)
        {
            tbl_DYC tbl_DYC = db.tbl_DYC.Find(id);
            if (tbl_DYC == null)
            {
                return NotFound();
            }

            db.tbl_DYC.Remove(tbl_DYC);
            db.SaveChanges();

            return Ok(tbl_DYC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_DYCExists(int id)
        {
            return db.tbl_DYC.Count(e => e.Id == id) > 0;
        }
    }
}