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

        //Get: statistic list host and reponse of student in that host of notifi
        [Route("api/userSendNotification/listStudentByNotificationIDandHost")]
        [HttpGet]
        public List<StudentResponseViewModel> getStudentByNotificationIDandHost(int notificationID, int facultyID, int hostID)
        {
            List<StudentResponseViewModel> result = new List<StudentResponseViewModel>();
            if(facultyID == 0)
            {
                var listStudentResponse = db.tbl_StudentResponse.Where(
                    p => p.Status && p.UserSendNotificationID == notificationID && p.tlb_Student.HostID == hostID).ToList();
                foreach (var item in listStudentResponse)
                {
                    result.Add(new StudentResponseViewModel {
                    contentResponse = item.ContentResponse,
                    facultyName = db.tlb_Student.Find(item.StudentID).tbl_Faculty.FacultyName,
                    fullName = db.tlb_Student.Find(item.StudentID).AspNetUser.FullName,
                    timeResponse = item.DateCreated,
                    userName = db.tlb_Student.Find(item.StudentID).AspNetUser.UserName
                    });
                }
            }
            else
            {
                var listStudentResponse = db.tbl_StudentResponse.Where(
                    p => p.Status && p.UserSendNotificationID == notificationID && 
                    p.tlb_Student.HostID == hostID && p.tlb_Student.FacultyId == facultyID).ToList();
                foreach (var item in listStudentResponse)
                {
                    result.Add(new StudentResponseViewModel
                    {
                        contentResponse = item.ContentResponse,
                        facultyName = db.tbl_Faculty.Find(facultyID).FacultyName,
                        fullName = db.tlb_Student.Find(item.StudentID).AspNetUser.FullName,
                        timeResponse = item.DateCreated,
                        userName = db.tlb_Student.Find(item.StudentID).AspNetUser.UserName
                    });
                }
            }
            

            return result;
        }

        //Get: statistic list host and reponse of student in that host of notifi
        [Route("api/userSendNotification/listHostAndCount")]
        [HttpGet]
        public List<ResponseViewModel> GetHostByNotifi(int notificationID, int facultyID)
        {
            List<ResponseViewModel> result = new List<ResponseViewModel>();
            if (facultyID == 0)
            {
                var listHost = db.tbl_NotificationHost.Where(p => p.UserSendNotificationID == notificationID).ToList();
                foreach (var item in listHost)
                {
                    result.Add(new ResponseViewModel
                    {
                        HostID = item.HostID,
                        NotificationID = notificationID,
                        HostName = item.tbl_Host.HostName,
                        CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Not Ok") && p.Status).Count(),
                        CountNumberOK = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Ok") && p.Status).Count(),
                        CountWaiting = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Waiting") && p.Status).Count(),
                        Total = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.UserSendNotificationID == notificationID && p.Status).Count(),
                    });
                }
            }
            else
            {
                var listHost = db.tbl_NotificationHost.Where(p => p.UserSendNotificationID == notificationID).ToList();
                foreach (var item in listHost)
                {
                    result.Add(new ResponseViewModel
                    {
                        HostID = item.HostID,
                        NotificationID = notificationID,
                        HostName = item.tbl_Host.HostName,
                        CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.tlb_Student.FacultyId == facultyID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Not Ok") && p.Status).Count(),
                        CountNumberOK = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.tlb_Student.FacultyId == facultyID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Ok") && p.Status).Count(),
                        CountWaiting = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.tlb_Student.FacultyId == facultyID && p.UserSendNotificationID == notificationID && p.ContentResponse.Equals("Waiting") && p.Status).Count(),
                        Total = db.tbl_StudentResponse.Where(p => p.tlb_Student.HostID == item.HostID && p.tlb_Student.FacultyId == facultyID && p.UserSendNotificationID == notificationID && p.Status).Count(),
                    });
                }
            }
            return result;
        }

        //Get: statistic notification and reponse of student 
        [Route("api/userSendNotification/listNotificationAndCount")]
        [HttpGet]
        public List<ResponseViewModel> GetNotifiList(int facultyID)
        {
            List<ResponseViewModel> result = new List<ResponseViewModel>();
            var notifications = db.tbl_UserSendNotification.Where(p => p.Status).ToList();
            if (facultyID == 0)
            {
                foreach (var item in notifications)
                {
                    if(db.tbl_NotificationHost.Where(p=> p.UserSendNotificationID == item.ID).GroupBy(p=>p.tbl_Host.tbl_Country).Count() > 1)
                    {
                        result.Add(new ResponseViewModel
                        {
                            NotifiTitle = item.TitleNotification,
                            NotificationID = item.ID,
                            CountryName = "All country",
                            CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Not OK")).Count(),
                            CountNumberOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("OK")).Count(),
                            CountWaiting = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Waiting")).Count(),
                            Total = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID).Count(),
                        });
                    }
                    else
                    {
                        result.Add(new ResponseViewModel
                        {
                            NotifiTitle = item.TitleNotification,
                            NotificationID = item.ID,
                            CountryName = db.tbl_NotificationHost.Where(p => p.UserSendNotificationID == item.ID).FirstOrDefault().tbl_Host.tbl_Country.CountryName,
                            CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Not OK")).Count(),
                            CountNumberOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("OK")).Count(),
                            CountWaiting = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Waiting")).Count(),
                            Total = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID).Count(),
                        });
                    }
                    
                } 
            }
            else
            {
                foreach (var item in notifications)
                {
                    if (db.tbl_DYC.Where(p => p.NetUsersID.Equals(item.NetUsersID)).FirstOrDefault().FacultyId == facultyID)
                    {
                        if (db.tbl_NotificationHost.Where(p => p.UserSendNotificationID == item.ID).GroupBy(p => p.tbl_Host.tbl_Country).Count() > 1)
                        {
                            result.Add(new ResponseViewModel
                            {
                                NotifiTitle = item.TitleNotification,
                                NotificationID = item.ID,
                                CountryName = "All country",
                                CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Not OK")).Count(),
                                CountNumberOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("OK")).Count(),
                                CountWaiting = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Waiting")).Count(),
                                Total = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID).Count(),
                            });
                        }
                        else
                        {
                            result.Add(new ResponseViewModel
                            {
                                NotifiTitle = item.TitleNotification,
                                NotificationID = item.ID,
                                CountryName = db.tbl_NotificationHost.Where(p => p.UserSendNotificationID == item.ID).FirstOrDefault().tbl_Host.tbl_Country.CountryName,
                                CountNumberNotOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Not OK")).Count(),
                                CountNumberOK = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("OK")).Count(),
                                CountWaiting = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID && p.ContentResponse.Equals("Waiting")).Count(),
                                Total = db.tbl_StudentResponse.Where(p => p.UserSendNotificationID == item.ID).Count(),
                            });
                        }

                    }
                }
            }
            return result;
        }

        //Get: Infor of notification from DYC/ADmin by host ID
        [Route("api/userSendNotification/inforByHostID")]
        [HttpGet]
        public List<UserSentNotificationViewModel> GetNotificationByHostID(int hostID)
        {
            List<UserSentNotificationViewModel> listNotification = new List<UserSentNotificationViewModel>();
            var notifications = db.tbl_NotificationHost.Where(p => p.HostID == hostID && p.Status);
            foreach (var item in notifications)
            {
                listNotification.Add(new UserSentNotificationViewModel
                {
                    DateCreated = item.tbl_UserSendNotification.DateCreated,
                    ID = item.UserSendNotificationID,
                    LevelEmergency = item.tbl_UserSendNotification.LevelEmergency,
                    Location = item.tbl_Host.Location,
                    TitleNotification = item.tbl_UserSendNotification.TitleNotification
                });
            }
            return listNotification;
        }

        //GET : Insert  notification DYC/admin send to student, facultyID here is which faculty will sent to, faculty = 0 mean sent to all country 
        [Route("api/userSendNotification/insertAdminSentNotoficationToDYC")]
        [HttpPost]
        public IHttpActionResult InsertAdminSentNotoficationToDYC(string contentNotification, string titleNotification,
            DateTime dateHazard, string UserID, int facultyID)
        {
            tbl_AdminSendNotification notifi = new tbl_AdminSendNotification();
            if (facultyID != 0)
            {
                notifi = new tbl_AdminSendNotification
                {
                    ContentRequest = contentNotification,
                    CreatedByUserID = UserID,
                    DateCreated = DateTime.Now,
                    Title = titleNotification,
                    Status = true,
                    NetUserID = UserID,
                    DateHazard = dateHazard
                };
                db.tbl_AdminSendNotification.Add(notifi);
                db.SaveChanges();
                db.tbl_AdminNotificationFaculty.Add(new tbl_AdminNotificationFaculty {
                    Status = true,
                    FacultyID = facultyID,
                    AdminSendNotificationID = notifi.ID});
                db.SaveChanges();
            }
            else
            {
                var faculty = db.tbl_Faculty.ToList();
                notifi = new tbl_AdminSendNotification
                {
                    ContentRequest = contentNotification,
                    CreatedByUserID = UserID,
                    DateCreated = DateTime.Now,
                    Title = titleNotification,
                    Status = true,
                    NetUserID = UserID,
                    DateHazard = dateHazard
                };
                db.tbl_AdminSendNotification.Add(notifi);
                db.SaveChanges();
                foreach (var item in faculty)
                {
                    db.tbl_AdminNotificationFaculty.Add(new tbl_AdminNotificationFaculty
                    {
                        Status = true,
                        FacultyID = item.ID,
                        AdminSendNotificationID = notifi.ID
                    });
                }
                db.SaveChanges();
            }
            db.SaveChanges();
            return Ok();
        }

        //GET : Insert  notification DYC/admin send to student 
        [Route("api/userSendNotification/insertUserSentNotofication")]
        [HttpPost]
        public IHttpActionResult InsertUserSentNotofication(string contentNotification, string titleNotification,
            DateTime dateHazard, string levelEmergency, string UserID, string country, string host, int facultyID)
        {
            host = host.Replace("_", "&");
            tbl_UserSendNotification notifi = new tbl_UserSendNotification();
            if(facultyID == 0)
            {
                if (country.Equals("All"))
                {
                    notifi = new tbl_UserSendNotification {
                        CreateByUserID = UserID,
                        ContentNotification = contentNotification,
                        LevelEmergency = levelEmergency,
                        DateHazard = dateHazard,
                        DateCreated = DateTime.Now,
                        Status = true,
                        NetUsersID = UserID,
                        TitleNotification = titleNotification
                    };
                    db.tbl_UserSendNotification.Add(notifi);
                    db.SaveChanges();
                    var hosts = db.tbl_Host.ToList();
                    foreach (var item in hosts)
                    {
                        if(db.tlb_Student.Where(p=> p.HostID == item.ID).Count() >0)
                        {
                            db.tbl_NotificationHost.Add(new tbl_NotificationHost {
                                HostID = item.ID,
                                Status = true,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                    }
                    db.SaveChanges();
                    var studentList = db.tlb_Student.Where(p => p.Status).ToList();
                    foreach (var item in studentList)
                    {
                        db.tbl_StudentResponse.Add(new tbl_StudentResponse {
                            Status = true,
                            ContentResponse = "Waiting",
                            CreatedByUserID = UserID,
                            DateCreated = DateTime.Now,
                            StudentID = item.Id,
                            UserSendNotificationID = notifi.ID
                        });
                    }
                    db.SaveChanges();
                }
                else
                {
                    if (host.Equals("All"))
                    {
                        notifi = new tbl_UserSendNotification
                        {
                            CreateByUserID = UserID,
                            ContentNotification = contentNotification,
                            LevelEmergency = levelEmergency,
                            DateHazard = dateHazard,
                            DateCreated = DateTime.Now,
                            Status = true,
                            NetUsersID = UserID,
                            TitleNotification = titleNotification
                        };
                        db.tbl_UserSendNotification.Add(notifi);
                        db.SaveChanges();
                        var hosts = db.tbl_Host.Where(p => p.tbl_Country.CountryName.Equals(country)).ToList();
                        foreach (var item in hosts)
                        {
                            if(db.tlb_Student.Where(p => p.HostID == item.ID).Count() > 0)
                            {
                                db.tbl_NotificationHost.Add( new tbl_NotificationHost {
                                    Status = true,
                                    HostID = item.ID,
                                    UserSendNotificationID = notifi.ID
                                });
                            }
                        }
                        db.SaveChanges();
                        var studentList = db.tlb_Student.Where(p=> p.tbl_Host.tbl_Country.CountryName.Equals(country) && p.Status);
                        foreach (var item in studentList)
                        {
                            db.tbl_StudentResponse.Add(new tbl_StudentResponse
                            {
                                Status = true,
                                ContentResponse = "Waiting",
                                CreatedByUserID = UserID,
                                DateCreated = DateTime.Now,
                                StudentID = item.Id,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        notifi = new tbl_UserSendNotification
                        {
                            CreateByUserID = UserID,
                            ContentNotification = contentNotification,
                            LevelEmergency = levelEmergency,
                            DateHazard = dateHazard,
                            DateCreated = DateTime.Now,
                            Status = true,
                            NetUsersID = UserID,
                            TitleNotification = titleNotification
                        };
                        db.tbl_UserSendNotification.Add(notifi);
                        db.SaveChanges();
                        db.tbl_NotificationHost.Add(new tbl_NotificationHost
                        {
                            HostID = db.tbl_Host.Where(p => p.HostName.Equals(host) && p.tbl_Country.CountryName.Equals(country)).FirstOrDefault().ID,
                            UserSendNotificationID = notifi.ID,
                            Status = true
                        });
                        db.SaveChanges();
                        var studentList = db.tlb_Student.Where(p => p.tbl_Host.HostName.Equals(host) && p.tbl_Host.tbl_Country.CountryName.Equals(country) && p.Status).ToList();
                        foreach (var item in studentList)
                        {
                            db.tbl_StudentResponse.Add(new tbl_StudentResponse
                            {
                                Status = true,
                                ContentResponse = "Waiting",
                                CreatedByUserID = UserID,
                                DateCreated = DateTime.Now,
                                StudentID = item.Id,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                if (country.Equals("All"))
                {
                    notifi = new tbl_UserSendNotification
                    {
                        CreateByUserID = UserID,
                        ContentNotification = contentNotification,
                        LevelEmergency = levelEmergency,
                        DateHazard = dateHazard,
                        DateCreated = DateTime.Now,
                        Status = true,
                        NetUsersID = UserID,
                        TitleNotification = titleNotification
                    };
                    db.tbl_UserSendNotification.Add(notifi);
                    db.SaveChanges();
                    var hosts = db.tbl_Host.ToList();
                    foreach (var item in hosts)
                    {
                        if (db.tlb_Student.Where(p => p.HostID == item.ID && p.FacultyId == facultyID).Count() > 0)
                        {
                            db.tbl_NotificationHost.Add(new tbl_NotificationHost
                            {
                                HostID = item.ID,
                                Status = true,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                    }
                    db.SaveChanges();
                    var studentList = db.tlb_Student.Where(p => p.tbl_Faculty.ID == facultyID && p.Status).ToList();
                    foreach (var item in studentList)
                    {
                        db.tbl_StudentResponse.Add(new tbl_StudentResponse {
                            Status = true,
                            ContentResponse = "Waiting",
                            CreatedByUserID = UserID,
                            DateCreated = DateTime.Now,
                            StudentID = item.Id,
                            UserSendNotificationID = notifi.ID
                        });
                    }
                    db.SaveChanges();
                }
                else
                {
                    if (host.Equals("All"))
                    {
                        notifi = new tbl_UserSendNotification
                        {
                            CreateByUserID = UserID,
                            ContentNotification = contentNotification,
                            LevelEmergency = levelEmergency,
                            DateHazard = dateHazard,
                            DateCreated = DateTime.Now,
                            Status = true,
                            NetUsersID = UserID,
                            TitleNotification = titleNotification
                        };
                        db.tbl_UserSendNotification.Add(notifi);
                        db.SaveChanges();
                        var hosts = db.tbl_Host.Where(p => p.tbl_Country.CountryName.Equals(country)).ToList();
                        foreach (var item in hosts)
                        {
                            if (db.tlb_Student.Where(p => p.HostID == item.ID && p.FacultyId == facultyID).Count() > 0)
                            {
                                db.tbl_NotificationHost.Add(new tbl_NotificationHost
                                {
                                    Status = true,
                                    HostID = item.ID,
                                    UserSendNotificationID = notifi.ID
                                });
                            }
                        }
                        var studentList = db.tlb_Student.Where(p =>p.tbl_Host.tbl_Country.CountryName.Equals(country) && p.tbl_Faculty.ID == facultyID && p.Status).ToList();
                        foreach (var item in studentList)
                        {
                            db.tbl_StudentResponse.Add(new tbl_StudentResponse
                            {
                                Status = true,
                                ContentResponse = "Waiting",
                                CreatedByUserID = UserID,
                                DateCreated = DateTime.Now,
                                StudentID = item.Id,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        notifi = new tbl_UserSendNotification
                        {
                            CreateByUserID = UserID,
                            ContentNotification = contentNotification,
                            LevelEmergency = levelEmergency,
                            DateHazard = dateHazard,
                            DateCreated = DateTime.Now,
                            Status = true,
                            NetUsersID = UserID,
                            TitleNotification = titleNotification
                        };
                        db.tbl_UserSendNotification.Add(notifi);
                        db.SaveChanges();
                        db.tbl_NotificationHost.Add(new tbl_NotificationHost
                        {
                            HostID = db.tbl_Host.Where(p => p.HostName.Equals(host) && p.tbl_Country.CountryName.Equals(country)).FirstOrDefault().ID,
                            UserSendNotificationID = notifi.ID,
                            Status = true
                        });
                        db.SaveChanges();
                        var studentList = db.tlb_Student.Where(p =>p.tbl_Host.HostName.Equals(host) && p.tbl_Host.tbl_Country.CountryName.Equals(country) && p.tbl_Faculty.ID == facultyID && p.Status).ToList();
                        foreach (var item in studentList)
                        {
                            db.tbl_StudentResponse.Add(new tbl_StudentResponse
                            {
                                Status = true,
                                ContentResponse = "Waiting",
                                CreatedByUserID = UserID,
                                DateCreated = DateTime.Now,
                                StudentID = item.Id,
                                UserSendNotificationID = notifi.ID
                            });
                        }
                        db.SaveChanges();
                    }
                }
            }
            db.SaveChanges();
            return Ok();
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