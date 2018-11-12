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
    public class FacultyController : ApiController
    {
        private EntityConnection db = new EntityConnection();



        //Get: GEt All list Faculty
        [Route("api/faculty/listAllFaculty")]
        [HttpGet]
        public List<FacultyViewModel> GetListFaculty()
        {
            var facultyList = db.tbl_Faculty.Where( p=> p.Status).ToList();
            List<FacultyViewModel> list = new List<FacultyViewModel>();
            foreach (var item in facultyList)
            {
                list.Add(new FacultyViewModel{ FacultyName = item.FacultyName, ID = item.ID});
            }
            return list;

        }



        // GET: api/Faculty
        public IQueryable<tbl_Faculty> Gettbl_Faculty()
        {
            return db.tbl_Faculty;
        }

        // GET: api/Faculty/5
        [ResponseType(typeof(tbl_Faculty))]
        public IHttpActionResult Gettbl_Faculty(int id)
        {
            tbl_Faculty tbl_Faculty = db.tbl_Faculty.Find(id);
            if (tbl_Faculty == null)
            {
                return NotFound();
            }

            return Ok(tbl_Faculty);
        }

        // PUT: api/Faculty/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Faculty(int id, tbl_Faculty tbl_Faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Faculty.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Faculty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_FacultyExists(id))
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

        // POST: api/Faculty
        [ResponseType(typeof(tbl_Faculty))]
        public IHttpActionResult Posttbl_Faculty(tbl_Faculty tbl_Faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Faculty.Add(tbl_Faculty);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Faculty.ID }, tbl_Faculty);
        }

        // DELETE: api/Faculty/5
        [ResponseType(typeof(tbl_Faculty))]
        public IHttpActionResult Deletetbl_Faculty(int id)
        {
            tbl_Faculty tbl_Faculty = db.tbl_Faculty.Find(id);
            if (tbl_Faculty == null)
            {
                return NotFound();
            }

            db.tbl_Faculty.Remove(tbl_Faculty);
            db.SaveChanges();

            return Ok(tbl_Faculty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_FacultyExists(int id)
        {
            return db.tbl_Faculty.Count(e => e.ID == id) > 0;
        }
    }
}