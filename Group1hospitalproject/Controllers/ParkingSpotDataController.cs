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
    public class ParkingSpotDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ParkingSpotData/listparkingspots
        [HttpGet]
        public IEnumerable<ParkingSpotDto> ListParkingSpots()
        {
            List<ParkingSpot> parkingSpots = db.ParkingSpots.ToList();
            List<ParkingSpotDto> parkingSpotDtos = new List<ParkingSpotDto>();

            parkingSpots.ForEach(s => parkingSpotDtos.Add(new ParkingSpotDto()
            {
                ParkingSpotID = s.ParkingSpotID,
                SpotNumber = s.SpotNumber
            }));
            return parkingSpotDtos;
        }

        // GET: api/ParkingSpotData/findparkingspot/5
        [ResponseType(typeof(ParkingSpot))]
        [HttpGet]
        public IHttpActionResult FindParkingSpot(int id)
        {
            ParkingSpot parkingSpot = db.ParkingSpots.Find(id);
            ParkingSpotDto parkingSpotDto = new ParkingSpotDto()
            {
                ParkingSpotID = parkingSpot.ParkingSpotID,
                SpotNumber = parkingSpot.SpotNumber
            };
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return Ok(parkingSpotDto);
        }

        // Post: api/ParkingSpotData/updateparkingspot/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateParkingSpot(int id, ParkingSpot parkingSpot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkingSpot.ParkingSpotID)
            {
                return BadRequest();
            }

            db.Entry(parkingSpot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingSpotExists(id))
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

        // POST: api/ParkingSpotData/addparkingspot
        [ResponseType(typeof(ParkingSpot))]
        [HttpPost]
        public IHttpActionResult AddParkingSpot(ParkingSpot parkingSpot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkingSpots.Add(parkingSpot);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parkingSpot.ParkingSpotID }, parkingSpot);
        }

        //POST DELETE: api/ParkingSpotData/deleteparkingspot/5
        [ResponseType(typeof(ParkingSpot))]
        [HttpPost]
        public IHttpActionResult DeleteParkingSpot(int id)
        {
            ParkingSpot parkingSpot = db.ParkingSpots.Find(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            db.ParkingSpots.Remove(parkingSpot);
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

        private bool ParkingSpotExists(int id)
        {
            return db.ParkingSpots.Count(e => e.ParkingSpotID == id) > 0;
        }
    }
}