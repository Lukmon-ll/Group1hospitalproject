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
using System.Diagnostics;

namespace Group1hospitalproject.Controllers
{
    public class JobDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all jobs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all jobs in the database, including their associated Applications.
        /// </returns>
        /// <example>
        /// GET: api/JobData/ListJobs
        /// </example>
        [HttpGet]
        public IEnumerable<JobDto> ListJobs()
        {
            List<Job> Jobs = db.Jobs.ToList();
            List<JobDto> JobDtos = new List<JobDto>();

            Jobs.ForEach(a => JobDtos.Add(new JobDto()
            {
                JobId = a.JobId,
                Jobtype = a.Jobtype,
                JobTitle = a.JobTitle,
                Descriptions = a.Descriptions,
                DepartmentName = a.Department.DepartmentName
            }));
               return JobDtos;
        }

        /// <summary>
        /// Returns all jobs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A job in the system matching up to the job ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the jobs</param>
        /// <example>
        /// GET: api/JobData/FindJob/5
        /// </example>

        [ResponseType(typeof(Job))]
        [HttpGet]
        public IHttpActionResult FindJob(int id)
        {
            Job Job = db.Jobs.Find(id);
            JobDto JobDto = new JobDto()
            {
                JobId = Job.JobId,
                Jobtype = Job.Jobtype,
                JobTitle = Job.JobTitle,
                Descriptions = Job.Descriptions,
                DepartmentName = Job.Department.DepartmentName
            };
            if (Job == null)
            {
                return NotFound();
            }

            return Ok(JobDto);
        }
        /// <summary>
        /// Updates a particular Jobs in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Species ID primary key</param>
        /// <param name="job">JSON FORM DATA of an Job</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/JobData/Update/5
        /// FORM DATA: jobs JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobId)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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
        /// Adds an Job to the system
        /// </summary>
        /// <param name="job">JSON FORM DATA of a Job</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Job ID, Job Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/JobData/AddJob
        /// FORM DATA: Species JSON Object
        /// </example>

        // POST: api/JobData/AddJob
        [ResponseType(typeof(Job))]
        [HttpPost]
        public IHttpActionResult AddJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.JobId }, job);
        }

        /// <summary>
        /// Deletes a Job from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Job</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/JobData/DeleteJob/5
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Job))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.JobId == id) > 0;
        }
    }
}