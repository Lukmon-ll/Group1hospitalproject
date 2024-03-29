﻿using Group1hospitalproject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Group1hospitalproject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace Group1hospitalproject.Controllers
{
    public class ParkingScheduleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ParkingScheduleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }
        /// <summary>
        /// lists scheduled parking list
        /// </summary>
        /// <returns>list of parking shcedule list</returns>
        // GET: ParkingSchedule/List
        public ActionResult List()
        {
            //objective: communicate with the parking schedule data api to retrieve a list of parking schedules
            //curl https://localhost:44341/api/parkingscheduledata/ListParkingschedules


            string url = "parkingscheduledata/ListParkingSchedules";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<ParkingScheduleDto> parkingSchedules = response.Content.ReadAsAsync<IEnumerable<ParkingScheduleDto>>().Result;
            Debug.WriteLine("Number of schedules received ");
            Debug.WriteLine(parkingSchedules.Count());

            return View(parkingSchedules);
        }
        /// <summary>
        /// returns a single instance of a parking sched by id
        /// </summary>
        /// <param name="id">schedule id</param>
        /// <returns>returns a parking schedule by id</returns>
        // GET: ParkingSchedules/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicatie with our car data api to retrieve one car 
            //curl https://localhost:44341/api/parkingscheduledata/FindparkingSchedule/{id}

            DetailsSchedule ViewModel = new DetailsSchedule();

            string url = "parkingscheduledata/FindParkingSchedule/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ParkingScheduleDto selectedparkingschedule = response.Content.ReadAsAsync<ParkingScheduleDto>().Result;


            return View(selectedparkingschedule);
        }

        public ActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// creates new parking schedule booking
        /// </summary>
        /// <returns>new parking booking</returns>
        // GET: ParkingSchedule/New
        public ActionResult New()
        {
            CreateSchedule ViewModel = new CreateSchedule();

            // able to select spots for new schedule
            string url = "parkingspotdata/listparkingspots";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ParkingSpotDto> SpotOptions = response.Content.ReadAsAsync<IEnumerable<ParkingSpotDto>>().Result;

            ViewModel.SpotOptions = SpotOptions;

            //able to select cars for new schedule

            url = "parkingcardata/listparkingcars";
            response = client.GetAsync(url).Result;
            IEnumerable<ParkingCarDto> CarOptions = response.Content.ReadAsAsync<IEnumerable<ParkingCarDto>>().Result;

            ViewModel.CarOptions = CarOptions;


            return View(ViewModel);
        
        }

        // POST: ParkingCar/Create
        /// <summary>
        /// new parking schedule
        /// </summary>
        /// <param name="parkingSchedule"></param>
        /// <returns>new parking shcedule</returns>
        [HttpPost]
        public ActionResult Create(ParkingSchedule parkingSchedule)
        {
            //curl -H "content-type:application/json" -d @parkingschedule.json https://localhost:44341/api/parkingscheduledata/AddParkingSchedule/
            string url = "parkingscheduledata/AddParkingSchedule";


            string jsonpayload = jss.Serialize(parkingSchedule);

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
        /// <summary>
        /// updates parking schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns>updated parking schedule</returns>
        // GET: ParkingSchedule/Edit/5
        public ActionResult Edit(int id)

        {

            UpdateSchedule ViewModel = new UpdateSchedule();

            string url = "parkingscheduledata/FindParkingSchedule/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ParkingScheduleDto selectedParkingSchedule = response.Content.ReadAsAsync<ParkingScheduleDto>().Result;

            ViewModel.SelectedSchedule =  selectedParkingSchedule;

            //include all spots for a schedule

            url = "parkingspotdata/listparkingspots/";
            response = client.GetAsync(url).Result;
            IEnumerable<ParkingSpotDto> SpotOptions = response.Content.ReadAsAsync<IEnumerable<ParkingSpotDto>>().Result;

            ViewModel.SpotOptions = SpotOptions;


            // include all cars for a schedule

            url = "parkingcardata/listparkingcars/";
            response = client.GetAsync(url).Result;
            IEnumerable<ParkingCarDto> CarOptions = response.Content.ReadAsAsync<IEnumerable<ParkingCarDto>>().Result;

            ViewModel.CarOptions = CarOptions;

            return View(ViewModel);
        }
        /// <summary>
        /// updated parking schedule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingSchedule"></param>
        /// <returns>updated parking schedule</returns>
        // POST: ParkingSchedule/Update/5
        [HttpPost]
        public ActionResult Update(int id, ParkingSchedule parkingSchedule)
        {
            string url = "parkingscheduledata/UpdateParkingSchedule/" + id;

            string jsonpayload = jss.Serialize(parkingSchedule);

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
        /// <summary>
        /// confirms if user wants to delete schedule by id
        /// </summary>
        /// <param name="id">schedule id</param>
        /// <returns>to schedule list, deletes schedule</returns>
        // GET: ParkingSchedule/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "parkingscheduledata/FindParkingSchedule/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ParkingScheduleDto selectedParkingSchedule = response.Content.ReadAsAsync<ParkingScheduleDto>().Result;

            return View(selectedParkingSchedule);
        }
        /// <summary>
        /// delete schedule by id
        /// </summary>
        /// <param name="id">schedule id</param>
        /// <returns>to schedule list, deletes schedule</returns>
        // GET: ParkingSchedule/DeleteConfirm/5
        // POST: ParkingSchedule/DeleteParkingSchedule/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "parkingscheduledata/DeleteParkingSchedule/" + id;

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
