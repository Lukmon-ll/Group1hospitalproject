using Group1hospitalproject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Group1hospitalproject.Controllers
{
    public class ParkingCarController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ParkingCarController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/parkingspotdata/");
        }
        // GET: ParkingCar/List
        public ActionResult List()
        {
            //objective: communicatie with our parking car data api to retrieve a list of cars
            //curl https://localhost:44341/api/parkingcardata/ListParkingcars


            string url = "ListParkingCars";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<ParkingCarDto> parkingCars = response.Content.ReadAsAsync<IEnumerable<ParkingCarDto>>().Result;
            Debug.WriteLine("Number of cars received ");
            Debug.WriteLine(parkingCars.Count());

            return View(parkingCars);
        }

        // GET: ParkingCars/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicatie with our car data api to retrieve one car 
            //curl https://localhost:44341/api/parkingcardata/Findparkingcar/{id}


            string url = "FindParkingCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ParkingCarDto selectedparkingcar = response.Content.ReadAsAsync<ParkingCarDto>().Result;


            return View(selectedparkingcar);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: ParkingCar/New
        public ActionResult New()
        {
            return View();
        }

        // POST: ParkingCar/Create
        [HttpPost]
        public ActionResult Create(ParkingCar parkingCar)
        {
            //curl -H "content-type:application/json" -d @parkingcar.json https://localhost:44341/api/parkingcardata/AddParkingCar/
            string url = "AddParkingCar";



            string jsonpayload = jss.Serialize(parkingCar);

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

        // GET: ParkingCar/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindParkingCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ParkingCarDto selectedParkingCar = response.Content.ReadAsAsync<ParkingCarDto>().Result;

            return View(selectedParkingCar);
        }

        // POST: ParkingCar/Update/5
        [HttpPost]
        public ActionResult Update(int id, ParkingCar parkingCar)
        {
            string url = "UpdateParkingCar/" + id;

            string jsonpayload = jss.Serialize(parkingCar);

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

        // GET: ParkingCar/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindParkingCar/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ParkingCarDto selectedParkingCar = response.Content.ReadAsAsync<ParkingCarDto>().Result;

            return View(selectedParkingCar);
        }

        // POST: ParkingCar/DeleteParkingCar/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DeleteParkingCar/" + id;

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
