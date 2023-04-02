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
using Group1hospitalproject.Models;

namespace Group1hospitalproject.Controllers
{
    public class ApplicationsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ApplicationsData/ListApplications
        [HttpGet]
        public IEnumerable<ApplicationDto> ListApplications()
        {
            List<Application> Applications = db.Applications.ToList();
            List<ApplicationDto> ApplicationDtos = new List<ApplicationDto>();

            Applications.ForEach(a => ApplicationDtos.Add(new ApplicationDto()
            {
                ApplicationId = a.ApplicationId,
                Name = a.Name,
                Phone = a.Phone,
                Email = a.Email,
                JobId = a.Job.JobId,
                JobTitle = a.Job.JobTitle
            }));
            return ApplicationDtos;
        }
        
        
  
        // GET: api/ApplicationsData/FindApplication/5
        [ResponseType(typeof(Application))]
        [HttpGet]
        public IHttpActionResult FindApplication(int id)
        {
            Application Application = db.Applications.Find(id);
            ApplicationDto ApplicationDto = new ApplicationDto()
            {
                ApplicationId = Application.ApplicationId,
                Name = Application.Name,
                Phone = Application.Phone,
                Email = Application.Email,
                JobId = Application.JobId,
                JobTitle = Application.Job.JobTitle
            };
            if (Application == null)
            {
                return NotFound();
            }

            return Ok(ApplicationDto);
        }


        // POST: api/ApplicationsData/UpdateApplication/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateApplication(int id, Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.ApplicationId)
            {
                return BadRequest();
            }

            db.Entry(application).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/ApplicationsData/AddApplication
        [ResponseType(typeof(Application))]
        [HttpPost]
        public IHttpActionResult AddApplication(Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Applications.Add(application);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = application.ApplicationId }, application);
        }

        // POST: api/ApplicationsData/DeleteApplication/5
        [ResponseType(typeof(Application))]
        [HttpPost]
        public IHttpActionResult DeleteApplication(int id)
        {
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return NotFound();
            }

            db.Applications.Remove(application);
            db.SaveChanges();

            return Ok(application);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationExists(int id)
        {
            return db.Applications.Count(e => e.ApplicationId == id) > 0;
        }
    }
}