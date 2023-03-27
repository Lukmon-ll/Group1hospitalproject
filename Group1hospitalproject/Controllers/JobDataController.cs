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

        // GET: api/JobData/ListJobs
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

        // GET: api/JobData/FindJob/5
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

        // POST: api/JobData/Update/5
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

        // POST: api/JobData/DeleteJob/5
        [ResponseType(typeof(Job))]
        [HttpPost]
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