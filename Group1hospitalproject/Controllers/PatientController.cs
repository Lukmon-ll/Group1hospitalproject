using Group1hospitalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Group1hospitalproject.Models.ViewModels;
using System.Diagnostics;



namespace Group1hospitalproject.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }

        // GET: Patient/List
        public ActionResult List()
        {
            //Objective: run a curl command to invoke the patient data web api to list the patients
            //https://localhost:44341/api/patientdata/listpatients

            string url = "patientdata/listpatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientsDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientsDto>>().Result;
            Debug.WriteLine(patients);

            return View(patients);

            
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            //api/PatientData/FindPatient/5
            DetailsPatient ViewModel = new DetailsPatient();

            string url = "PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientsDto SelectedPatient = response.Content.ReadAsAsync<PatientsDto>().Result;
            ViewModel.SelectedPatient = SelectedPatient;
            return View(ViewModel);
        }

        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patients patient)
        {
            // TODO: Add insert logic here
            //api/PatientData/AddPatient
            string url = "PatientData/AddPatient";
            string jsonpayload = jss.Serialize(patient);

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

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            //api/PatientData/FindPatient/5
            UpdatePatient ViewModel = new UpdatePatient();
            string url = "PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientsDto SelectedPatient = response.Content.ReadAsAsync<PatientsDto>().Result;
            ViewModel.SelectedPatient = SelectedPatient;

            return View(ViewModel);
        }

        // POST: Patient/Update/5
        [HttpPost]
        public ActionResult Update(int id, Patients patient)
        {
            //api/PatientData/UpdatePatient/5
            string url = "PatientData/UpdatePatient/" + id;

            string jsonpayload = jss.Serialize(patient);

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

        // GET: Patient/Delete/5
        public ActionResult DeleteC(int id)
        {
            //api/PatientData/DeletePatient/5
            string url = "PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientsDto selectedpatient = response.Content.ReadAsAsync<PatientsDto>().Result;

            return View(selectedpatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "PatientData/DeletePatient/" + id;

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
