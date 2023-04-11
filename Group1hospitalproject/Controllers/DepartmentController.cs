using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Group1hospitalproject.Models;
using System.Web.Script.Serialization;
using Group1hospitalproject.Migrations;
using Group1hospitalproject.Models.ViewModels;

namespace Group1hospitalproject.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }
        // GET: Department/List
        public ActionResult List()
        {
            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> Department = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            return View(Department);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            DetailsDepartment ViewModel = new DetailsDepartment();
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            ViewModel.SelectedDepartment = SelectedDepartment;

            url = "doctordata/listdoctorsfordepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> RelatedDoctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            ViewModel.RelatedDoctors = RelatedDoctors;

            return View(ViewModel);
        }

        // GET: Department/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            string url = "departmentdata/adddepartment";
            string jsonpayload = jss.Serialize(department);


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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDepartment ViewModel = new UpdateDepartment();
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            ViewModel.SelectedDepartment = SelectedDepartment;

            return View(ViewModel);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Department department)
        {
            string url = "departmentdata/updatedepartment/" + id;
            string jsonpayload = jss.Serialize(department);


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

        // GET: Department/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            return View(SelectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "departmentdata/deletedepartment/" + id;


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
