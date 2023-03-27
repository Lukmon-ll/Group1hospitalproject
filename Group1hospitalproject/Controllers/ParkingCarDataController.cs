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
    public class ParkingCarDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ParkingCarData/listparkingcars
        [HttpGet]
        public IEnumerable<ParkingCarDto> ListParkingCars()
        {
            List<ParkingCar> parkingCars = db.ParkingCars.ToList();
            List<ParkingCarDto> parkingCarDtos = new List<ParkingCarDto>();

            parkingCars.ForEach(p => parkingCarDtos.Add(new ParkingCarDto()
            {
                ParkingCarID = p.ParkingCarID,
                LicencePlate = p.LicencePlate,
                DoctorName = p.Doctor.DoctorName
           
            }));
            return parkingCarDtos;

        }

        // GET: api/ParkingCarData/findparkingcar/5
        [ResponseType(typeof(ParkingCar))]
        [HttpGet]
        public IHttpActionResult FindParkingCar(int id)
        {
            ParkingCar parkingCar = db.ParkingCars.Find(id);
            ParkingCarDto parkingCarDto = new ParkingCarDto()
            {
                ParkingCarID = parkingCar.ParkingCarID,
                LicencePlate = parkingCar.LicencePlate,
                DoctorName = parkingCar.Doctor.DoctorName
            };

            if (parkingCar == null)
            {
                return NotFound();
            }

            return Ok(parkingCarDto);
        }

        // POST: api/ParkingCarData/updateparkingcar/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateParkingCar(int id, ParkingCar parkingCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkingCar.ParkingCarID)
            {
                return BadRequest();
            }

            db.Entry(parkingCar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingCarExists(id))
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

        // POST: api/ParkingCarData/addParkingcar
        [ResponseType(typeof(ParkingCar))]
        [HttpPost]
        public IHttpActionResult AddParkingCar(ParkingCar parkingCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkingCars.Add(parkingCar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parkingCar.ParkingCarID }, parkingCar);
        }

        // POST DELETE: api/ParkingCarData/deleteparkingcar/5
        [ResponseType(typeof(ParkingCar))]
        [HttpPost]
        public IHttpActionResult DeleteParkingCar(int id)
        {
            ParkingCar parkingCar = db.ParkingCars.Find(id);
            if (parkingCar == null)
            {
                return NotFound();
            }

            db.ParkingCars.Remove(parkingCar);
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

        private bool ParkingCarExists(int id)
        {
            return db.ParkingCars.Count(e => e.ParkingCarID == id) > 0;
        }
    }
}