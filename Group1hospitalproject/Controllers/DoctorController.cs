using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Group1hospitalproject.Migrations;
using Group1hospitalproject.Models;
using Group1hospitalproject.Models.ViewModels;

namespace Group1hospitalproject.Controllers
{
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DoctorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }
        // GET: Doctor/List
        public ActionResult List()
        {
            string url = "doctordata/listdoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DoctorDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            return View(doctors);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            DetailsDoctor ViewModel = new DetailsDoctor();
            string url = "doctordata/finddoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto SelectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            ViewModel.SelectedDoctor = SelectedDoctor;
            return View(ViewModel);
        }

        // GET: Doctor/New
        public ActionResult New()
        {
            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<DepartmentDto> DepartmentOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(DepartmentOptions);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {

            // TODO: Add insert logic here
                string url = "doctordata/adddoctor";
                string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDoctor ViewModel = new UpdateDoctor();
            string url = "doctordata/finddoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto SelectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            ViewModel.SelectedDoctor = SelectedDoctor;

            url = "departmentdata/listdepartments/";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> DepartmentOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.DepartmentOptions = DepartmentOptions;

            return View(ViewModel);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Doctor doctor)
        {
            string url = "doctordata/updatedoctor/" + id;

            string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "doctordata/finddoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto selecteddoctor = response.Content.ReadAsAsync<DoctorDto>().Result;

            return View(selecteddoctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "doctordata/deletedoctor/" + id;

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
