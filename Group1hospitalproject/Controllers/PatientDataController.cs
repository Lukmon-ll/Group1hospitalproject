using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using Group1hospitalproject.Models;



namespace Group1hospitalproject.Controllers
{
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientData/ListPatients
        [HttpGet]
        public IEnumerable<PatientsDto> ListPatients()
        {
            List<Patients> Patient = db.Patients.ToList();
            List<PatientsDto> PatientsDtos = new List<PatientsDto>();

            Patient.ForEach(a => PatientsDtos.Add(new PatientsDto()
            {
                PatientID = a.PatientID,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhoneNumber = a.PhoneNumber,
                Email = a.Email

            }));
            return PatientsDtos;
        }

        // GET: api/PatientData/FindPatient/5
        [HttpGet]
        [ResponseType(typeof(Patients))]
        public IHttpActionResult FindPatient(int id)
        {
            Patients Patient = db.Patients.Find(id);
            PatientsDto PatientDto = new PatientsDto()
            {
                PatientID = Patient.PatientID,
                FirstName = Patient.FirstName,
                LastName = Patient.LastName,
                PhoneNumber = Patient.PhoneNumber,
                Email = Patient.Email
            };
            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }

        // POST: api/PatientData/UpdatePatient/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePatient(int id, Patients patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patients.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patients).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientsExists(id))
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

        // POST: api/PatientData/AddPatient
        [HttpPost]
        [ResponseType(typeof(Patients))]
        public IHttpActionResult AddPatient(Patients patients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patients);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patients.PatientID }, patients);
        }

        // DELETE: api/PatientData/DeletePatient/5
        [HttpPost]
        [ResponseType(typeof(Patients))]
        public IHttpActionResult DeletePatient(int id)
        {
            Patients patients = db.Patients.Find(id);
            if (patients == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patients);
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

        private bool PatientsExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}