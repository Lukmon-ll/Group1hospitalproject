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

        /// <summary>
        /// Returns all Applications in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Applications in the database, including their associated jobs.
        /// </returns>
        /// <example>
        /// GET: api/ApplicationsData/ListApplications
        /// </example>

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

        /// <summary>
        /// Gather information about all applications related to a particular jobs id
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Jobs ID</param>
        // GET: api/ApplicationsData/ListApplicationforJobs/2
        [HttpGet]
        [ResponseType(typeof(ApplicationDto))]
        public IHttpActionResult ListApplicationforJobs(int id)
        {
            List<Application> Applications = db.Applications.Where(a => a.JobId == id).ToList();
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
            return Ok(ApplicationDtos);
        }

        /// <summary>
        /// Returns all Applications in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Applications in the system matching up to the Application ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Application</param>
        /// <example>
        /// GET: api/ApplicationsData/ApplicationsData/5
        /// </example>

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


        /// <summary>
        /// Updates a particular Application in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Application ID primary key</param>
        /// <param name="application">JSON FORM DATA of an Application</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ApplicationsData/UpdateApplication/5
        /// FORM DATA: Application JSON Object
        /// </example>

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
        /// <summary>
        /// Adds an application to the system
        /// </summary>
        /// <param name="application">JSON FORM DATA of an application</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: application ID, application Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ApplicationsData/AddApplication
        /// FORM DATA: application JSON Object
        /// </example>
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
        /// <summary>
        /// Deletes an Application from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Application</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ApplicationsData/DeleteApplication/5
        /// FORM DATA: (empty)
        /// </example>
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