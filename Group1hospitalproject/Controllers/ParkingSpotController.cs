using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Group1hospitalproject.Models;
using Group1hospitalproject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace Group1hospitalproject.Controllers
{
    public class ParkingSpotController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ParkingSpotController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }

        // GET: ParkingSpot/list
        public ActionResult List()
        {   // communicate with parkingspotdata api to retreive parking spot list
            // curl /listparkingspots


            string url = "parkingspotdata/listparkingspots";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("the response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ParkingSpotDto> parkingSpots = response.Content.ReadAsAsync<IEnumerable<ParkingSpotDto>>().Result;
            //Debug.WriteLine("number of parkingspots received ");
            //Debug.WriteLine(parkingSpots.Count()); 

            return View(parkingSpots);
        }

        // GET: ParkingSpot/Details/5
        public ActionResult Details(int id)
        {
            // communicate with parkingspotdata api to retreive one parking spot
            // curl https://localhost:44341/api/parkingspotdata/findparkingspot/{id}

            DetailsSpot ViewModel = new DetailsSpot();

            string url = "parkingspotdata/findparkingspot/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("the response code is ");
            //Debug.WriteLine(response.StatusCode);

            ParkingSpotDto selectedParkingSpot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            //Debug.WriteLine("parking spot received ");
            //Debug.WriteLine(selectedParkingSpot.SpotNumber);

            ViewModel.selectedParkingSpot = selectedParkingSpot;

            // showcase doctors related to this parking spot
            // send a request to get info on doctors related to a parking spot id
            //url = "doctordata/listdoctorsforspot/" + id;
            //IEnumerable<DoctorDto> RelatedDoctors = ;
            //ViewModel.RelatedDoctors = RelatedDoctors;

            return View(selectedParkingSpot);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: ParkingSpot/New
        public ActionResult New()
        {
            return View();
        }

        // POST: ParkingSpot/Create
        [HttpPost]
        public ActionResult Create(ParkingSpot parkingSpot)
        {
            Debug.WriteLine("the input spot number is");
            Debug.WriteLine(parkingSpot.SpotNumber);
            // add a new parking spot into the system using the API
            //curl -H "Content-Type:application/json" -d @parkingspot.json https://localhost:44341/api/parkingspotdata/addparkingspot
            string url = "parkingspotdata/addparkingspot";


            string jsonpayload = jss.Serialize(parkingSpot);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");

            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: ParkingSpot/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "parkingspotdata/findparkingspot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingSpotDto selectedparkingspot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            return View(selectedparkingspot);
        }

        // POST: ParkingSpot/Edit/5
        [HttpPost]
        public ActionResult Update(int id, ParkingSpot parkingSpot)
        {

            string url = "parkingspotdata/updateparkingspot/" + id;
            string jsonpayload = jss.Serialize(parkingSpot);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: ParkingSpot/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "parkingspotdata/findparkingspot/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ParkingSpotDto selectedparkingspot = response.Content.ReadAsAsync<ParkingSpotDto>().Result;
            return View(selectedparkingspot);
        }

        // POST: ParkingSpot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "parkingspotdata/deleteparkingspot/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
