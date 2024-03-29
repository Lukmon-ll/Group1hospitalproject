﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Group1hospitalproject.Migrations;
using Group1hospitalproject.Models;

namespace Group1hospitalproject.Controllers
{
    public class DoctorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DoctorData/ListDoctors
        [HttpGet]
        [ResponseType(typeof(DoctorDto))]
        public IHttpActionResult ListDoctors()
        {
            List<Doctor> Doctors =  db.Doctors.ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
            {
                DoctorID = a.DoctorID,
                DoctorName = a.DoctorName,
                DoctorDescription = a.DoctorDescription,
                DoctorEmail = a.DoctorEmail,
                DepartmentName = a.Department.DepartmentName
            }));
            return Ok(DoctorDtos);
        }

        [HttpGet]
        [ResponseType(typeof(DoctorDto))]
        public IHttpActionResult ListDoctorsForDepartment(int id)
        {
            List<Doctor> Doctors = db.Doctors.Where(a => a.DepartmentID == id).ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
            {
                DoctorID = a.DoctorID,
                DoctorName = a.DoctorName,
                DoctorDescription = a.DoctorDescription,
                DoctorEmail = a.DoctorEmail,
                DepartmentName = a.Department.DepartmentName
            }));
            return Ok(DoctorDtos);
        }
        //gather info on all doctors related to a spot id
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <param name="id">spot id</param>
        // GET: api/DoctorData/ListDoctors/1
        //[HttpGet]
        //[ResponseType(typeof(DoctorDto))]
        //public IEnumerable<DoctorDto> ListDoctorsForSpot(int id)
        //{
        //    List<Doctor> Doctors = db.Doctors.Where(a => a.ParkingSpotID == id).ToList();
        //    List<DoctorDto> DoctorDtos = new List<DoctorDto>();

        //    Doctors.ForEach(a => DoctorDtos.Add(new DoctorDto()
        //    {
        //        DoctorID = a.DoctorID,
        //        DoctorName = a.DoctorName,
        //        DoctorDescription = a.DoctorDescription,
        //        DoctorEmail = a.DoctorEmail,
        //        DepartmentName = a.Department.DepartmentName
        //    }));
        //    return DoctorDtos;
        //}

        // GET: api/DoctorData/FindDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpGet]
        public IHttpActionResult FindDoctor(int id)
        {
            Doctor Doctor = db.Doctors.Find(id);
            DoctorDto DoctorDto = new DoctorDto()
            {
                DoctorID = Doctor.DoctorID,
                DoctorName = Doctor.DoctorName,
                DoctorDescription = Doctor.DoctorDescription,
                DoctorEmail = Doctor.DoctorEmail,
                DepartmentName = Doctor.Department.DepartmentName
            };
            if (Doctor == null)
            {
                return NotFound();
            }

            return Ok(DoctorDto);
        }

        // POST: api/DoctorData/UpdateDoctor/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.DoctorID)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/DoctorData/AddDoctor
        [ResponseType(typeof(Doctor))]
        [HttpPost]
        public IHttpActionResult AddDoctor(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctor.DoctorID }, doctor);
        }

        // POST: api/DoctorData/DeleteDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpPost]
        public IHttpActionResult DeleteDoctor(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
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

        private bool DoctorExists(int id)
        {
            return db.Doctors.Count(e => e.DoctorID == id) > 0;
        }
    }
}