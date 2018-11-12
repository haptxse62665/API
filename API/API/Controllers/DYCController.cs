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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace API.Controllers
{
    public class DYCController : ApiController
    {
        private EntityConnection db = new EntityConnection();


        //Get: DYC infor by user id
        [Route("api/dyc/inforByUserName")]
        [HttpGet]
        public UserViewModel GetUserInforByUserName(string username)
        {
            var _userManager = Request.GetOwinContext().GetUserManager<AspNetUserManager>();
            var user = _userManager.FindByEmailAsync(username).Result;
            string rolename = _userManager.GetRoles(user.Id).FirstOrDefault();
            if (rolename.Equals("DYC"))
            {
                var dyc = db.tbl_DYC.Where(p => p.NetUsersID == user.Id && p.Status).FirstOrDefault();
                return new DYCViewModel
                {
                    DYCID = dyc.DYCID,
                    Email = user.Email,
                    FacultyId = dyc.FacultyId,
                    FullName = user.FullName,
                    Id = dyc.Id, 
                    PhoneNumber = user.PhoneNumber,
                    RoleName = rolename,
                    UserID = user.Id
                };
            }
            else
            {
                if (rolename.Equals("Admin"))
                {
                    return new UserViewModel
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        RoleName = rolename,
                        UserID = user.Id,
                        UserName = user.UserName
                    };
                }
            }
            return null;
            
        }



        //Get: List country and munber of student
        [Route("api/DYCAndAdmin/listCountryAndNumberStudent")]
        [HttpGet]
        public List<CountryViewModel> GetListCountry(int facultyId)
        {
            List<CountryViewModel> countryList = new List<CountryViewModel>();
            var countrys = db.tbl_Country.ToList();
            if (facultyId > 0)
            {
                foreach (var item in countrys)
                {
                    int numberStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID && p.FacultyId == facultyId && p.Status).Count();
                    if(numberStudent > 0)
                    {
                        countryList.Add(new CountryViewModel { ID = item.ID, CountryName = item.CountryName, NumberOfStudent = numberStudent, ImageURL = item.ImageURL});
                    }
                }
                return countryList;
            }
            else
            {
                foreach (var item in countrys)
                {
                    int numberStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == item.ID && p.Status).Count();
                    if (numberStudent > 0)
                    {
                        countryList.Add(new CountryViewModel { ID = item.ID, CountryName = item.CountryName, NumberOfStudent = numberStudent, ImageURL = item.ImageURL });
                    }
                }
                return countryList;
            }
            
        }


        //Get: List Host and munber of student
        [Route("api/DYCAndAdmin/listHostAndNumberStudent")]
        [HttpGet]
        public List<HostViewModel> GetListHost(int facultyId, int countryId)
        {
            List<HostViewModel> hostList = new List<HostViewModel>();
            var hosts = db.tbl_Host.ToList();
            if (facultyId > 0)
            {
                foreach (var item in hosts)
                {
                    int numberStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == countryId && p.FacultyId == facultyId && p.HostID == item.ID && p.Status).Count();
                    if (numberStudent > 0)
                    {
                        hostList.Add(new HostViewModel { HostID = item.ID, NumberOfStudent = numberStudent, HostName = item.HostName });
                    }
                }
                return hostList;
            }
            else
            {
                foreach (var item in hosts)
                {
                    int numberStudent = db.tlb_Student.Where(p => p.tbl_Host.tbl_Country.ID == countryId && p.HostID == item.ID && p.Status).Count();
                    if (numberStudent > 0)
                    {
                        hostList.Add(new HostViewModel { HostID = item.ID, NumberOfStudent = numberStudent, HostName = item.HostName });
                    }
                }
                return hostList;
            }

        }


      







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