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
    public class CountryController : ApiController
    {
        private EntityConnection db = new EntityConnection();

        //GET: Get list all country and number of student arrival 
        [Route("api/country/listCountryAndArrivedStudent")]
        [HttpGet]
        public List<CountryViewModel> GetListCountry(int facultyID)
        {
            List<CountryViewModel> list = new List<CountryViewModel>();
            var countryList = db.tbl_Country.Where(p => p.Status).ToList();
            if(facultyID > 0)
            {
                foreach (var item in countryList)
                {
                    int numberStudentArrived = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID &&
                    p.FacultyId == facultyID && p.Arrival && p.Status).Count();
                    int totalStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID &&
                    p.FacultyId == facultyID && p.Status).Count();
                    if(totalStudent > 0)
                    list.Add(new CountryViewModel {CountryName = item.CountryName, ID = item.ID, NumberArrivalPerTotal = numberStudentArrived+"/"+ totalStudent, ImageURL= item.ImageURL});
                }
            }
            else
            {
                foreach (var item in countryList)
                {
                    int numberStudentArrived = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID  && p.Arrival && p.Status).Count();
                    int totalStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID && p.Status).Count();
                    if (totalStudent > 0)
                    list.Add(new CountryViewModel { CountryName = item.CountryName, ID = item.ID, NumberArrivalPerTotal = numberStudentArrived + "/" + totalStudent, ImageURL = item.ImageURL });
                }
            }
            return list;
           
        }


        // GET: api/Country
        public IQueryable<tbl_Country> Gettbl_Country()
        {
            return db.tbl_Country;
        }

        // GET: api/Country/5
        [ResponseType(typeof(tbl_Country))]
        public IHttpActionResult Gettbl_Country(int id)
        {
            tbl_Country tbl_Country = db.tbl_Country.Find(id);
            if (tbl_Country == null)
            {
                return NotFound();
            }

            return Ok(tbl_Country);
        }

        // PUT: api/Country/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Country(int id, tbl_Country tbl_Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Country.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CountryExists(id))
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

        // POST: api/Country
        [ResponseType(typeof(tbl_Country))]
        public IHttpActionResult Posttbl_Country(tbl_Country tbl_Country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Country.Add(tbl_Country);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Country.ID }, tbl_Country);
        }

        // DELETE: api/Country/5
        [ResponseType(typeof(tbl_Country))]
        public IHttpActionResult Deletetbl_Country(int id)
        {
            tbl_Country tbl_Country = db.tbl_Country.Find(id);
            if (tbl_Country == null)
            {
                return NotFound();
            }

            db.tbl_Country.Remove(tbl_Country);
            db.SaveChanges();

            return Ok(tbl_Country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CountryExists(int id)
        {
            return db.tbl_Country.Count(e => e.ID == id) > 0;
        }
    }
}