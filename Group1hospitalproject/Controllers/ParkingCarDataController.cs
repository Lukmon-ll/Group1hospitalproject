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

        /// <summary>
        /// returns list of cars in system
        /// </summary>
        /// <returns>
        /// all cars in the system
        /// </returns>
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

        /// <summary>
        /// returns list of cars in system for schedule id
        /// </summary>
        /// <returns>
        /// all cars related to a schedule
        /// <paramref name="id"/> id is for schedule
        /// </returns>
        // GET: api/ParkingCarData/listparkingcars/3
        //[HttpGet]
        //public IEnumerable<ParkingCarDto> Listcarsforschedule(int id)
        //{
        //    List<ParkingCar> parkingCars = db.ParkingCars.Where(p=>p.).ToList();
        //    List<ParkingCarDto> parkingCarDtos = new List<ParkingCarDto>();

        //    parkingCars.ForEach(p => parkingCarDtos.Add(new ParkingCarDto()
        //    {
        //        ParkingCarID = p.ParkingCarID,
        //        LicencePlate = p.LicencePlate,
        //        DoctorName = p.Doctor.DoctorName

        //    }));
        //    return parkingCarDtos;

        //}




        /// <summary>
        /// returns a single car
        /// </summary>
        /// <param name="id">car id</param>
        /// <returns>single instance of car in system</returns>
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
        /// <summary>
        /// updates a car in the system
        /// </summary>
        /// <param name="id">car id</param>
        /// <param name="parkingCar"></param>
        /// <returns>updated car</returns>
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
        /// <summary>
        /// add a car to the system
        /// </summary>
        /// <param name="parkingCar">car</param>
        /// <returns>new car to system</returns>
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
        /// <summary>
        /// removes car from system
        /// </summary>
        /// <param name="id">car id</param>
        /// <returns>deletes car, returns back to car list</returns>
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