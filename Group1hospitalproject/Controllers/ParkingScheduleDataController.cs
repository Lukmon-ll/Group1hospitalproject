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
using Group1hospitalproject.Models.ViewModels;

namespace Group1hospitalproject.Controllers
{
    public class ParkingScheduleDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// list parking schedules
        /// </summary>
        /// <returns>list of parking schedules</returns>
        // GET: api/ParkingScheduleData/listparkingschedules
        [HttpGet]
        public IEnumerable<ParkingScheduleDto> ListParkingSchedules()
        {
            List<ParkingSchedule> parkingSchedules = db.ParkingSchedules.ToList();
            List<ParkingScheduleDto> parkingScheduleDtos = new List<ParkingScheduleDto>();

            parkingSchedules.ForEach(s => parkingScheduleDtos.Add(new ParkingScheduleDto()
            {
                ParkingScheduleID = s.ParkingScheduleID,
                SpotNumber = s.ParkingSpot.SpotNumber,
                LicencePlate = s.ParkingCar.LicencePlate,
                DateTimeIn = s.DateTimeIn,
                DateTimeOut = s.DateTimeOut

            }));
            return parkingScheduleDtos;
        }

        /// <summary>
        /// list parking schedules for particular car
        /// </summary>
        /// <param name="id">id is for car</param>
        /// <returns>list of parking schedules for car</returns>
        // GET: api/ParkingScheduleData/listparkingschedules/id
        [HttpGet]
        public IEnumerable<ParkingScheduleDto> ListSchedulesForCar(int id)
        {
            List<ParkingSchedule> parkingSchedules = db.ParkingSchedules.Where(s=>s.ParkingCarID==id).ToList();
            List<ParkingScheduleDto> parkingScheduleDtos = new List<ParkingScheduleDto>();

            parkingSchedules.ForEach(s => parkingScheduleDtos.Add(new ParkingScheduleDto()
            {
                ParkingScheduleID = s.ParkingScheduleID,
                SpotNumber = s.ParkingSpot.SpotNumber,
                LicencePlate = s.ParkingCar.LicencePlate,
                DateTimeIn = s.DateTimeIn,
                DateTimeOut = s.DateTimeOut

            }));
            return parkingScheduleDtos;
        }



        /// <summary>
        /// particular schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ParkingScheduleData/findparkingschedule/5
        [ResponseType(typeof(ParkingSchedule))]
        [HttpGet]
        public IHttpActionResult FindParkingSchedule(int id)
        {
            ParkingSchedule parkingSchedule = db.ParkingSchedules.Find(id);
            ParkingScheduleDto parkingScheduleDto = new ParkingScheduleDto()
            {
                ParkingScheduleID = parkingSchedule.ParkingScheduleID,
                SpotNumber = parkingSchedule.ParkingSpot.SpotNumber,
                LicencePlate = parkingSchedule.ParkingCar.LicencePlate,
                DateTimeIn = parkingSchedule.DateTimeIn,
                DateTimeOut = parkingSchedule.DateTimeOut
            };

            if (parkingSchedule == null)
            {
                return NotFound();
            }

            return Ok(parkingScheduleDto);
        }

        /// <summary>
        /// update/edit a schedule
        /// </summary>
        /// <param name="id">scheduleid</param>
        /// <param name="parkingSchedule"></param>
        /// <returns>updated schedule</returns>
        // POST: api/ParkingScheduleData/updateparkingschedule/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateParkingSchedule(int id, ParkingSchedule parkingSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkingSchedule.ParkingScheduleID)
            {
                return BadRequest();
            }

            db.Entry(parkingSchedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingScheduleExists(id))
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
        /// adds new schedule
        /// </summary>
        /// <param name="parkingSchedule"></param>
        /// <returns>list of schedules/bookings with new one added</returns>
        // POST: api/ParkingScheduleData/addparkingschedule
        [ResponseType(typeof(ParkingSchedule))]
        [HttpPost]
        public IHttpActionResult AddParkingSchedule(ParkingSchedule parkingSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkingSchedules.Add(parkingSchedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parkingSchedule.ParkingScheduleID }, parkingSchedule);
        }
        /// <summary>
        /// deletes schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list with deleted schedule gone</returns>
        // DELETE: api/ParkingScheduleData/deleteparkingschedule/5
        [ResponseType(typeof(ParkingSchedule))]
        [HttpPost]
        public IHttpActionResult DeleteParkingSchedule(int id)
        {
            ParkingSchedule parkingSchedule = db.ParkingSchedules.Find(id);
            if (parkingSchedule == null)
            {
                return NotFound();
            }

            db.ParkingSchedules.Remove(parkingSchedule);
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

        private bool ParkingScheduleExists(int id)
        {
            return db.ParkingSchedules.Count(e => e.ParkingScheduleID == id) > 0;
        }
    }
}