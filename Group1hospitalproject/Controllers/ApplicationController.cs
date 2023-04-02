using Group1hospitalproject.Models;
using Group1hospitalproject.Models.ViewModels;
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
    public class ApplicationController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ApplicationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }
        // GET: Application/List
        public ActionResult List()
        {
            //objective: communicatie with our application data api to retrieve a list of applications
            //curl https://localhost:44341/api/applicationdata/ListApplications

   
            string url = "applicationsdata/ListApplications";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<ApplicationDto> applications = response.Content.ReadAsAsync<IEnumerable<ApplicationDto>>().Result;
            Debug.WriteLine("Number of applications received ");
            Debug.WriteLine(applications.Count());


               return View(applications);
        }

        // GET: Application/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicatie with our application data api to retrieve one application detail
            //curl https://localhost:44341/api/applicationdata/FindApplication/{id}


            string url = "applicationsdata/FindApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            ApplicationDto seletedapplication = response.Content.ReadAsAsync<ApplicationDto>().Result;


            return View(seletedapplication);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Application/New
        public ActionResult New()
        {

            string url = "jobdata/ListJobs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<JobDto> JobsOptions = response.Content.ReadAsAsync<IEnumerable<JobDto>>().Result;

            return View(JobsOptions);
        }
         
        // POST: Application/Create
        [HttpPost]
        public ActionResult Create(Application application)
        {
            //curl -H "content-type:application/json" -d @application.json https://localhost:44341/api/applicationdata/AddApplication/
            string url = "applicationsdata/AddApplication";



            string jsonpayload = jss.Serialize(application);

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

        // GET: Application/Edit/5
        public ActionResult Edit(int id)
        {

            UpdateApplication ViewModel = new UpdateApplication();

            string url = "applicationsdata/FindApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ApplicationDto SelectedApplication = response.Content.ReadAsAsync<ApplicationDto>().Result;
            ViewModel.SelectedApplication = SelectedApplication;

            url = "jobdata/ListJobs/";
            response = client.GetAsync(url).Result;
            IEnumerable<JobDto> JobOptions = response.Content.ReadAsAsync<IEnumerable<JobDto>>().Result;
            ViewModel.JobOptions = JobOptions;

            return View(ViewModel);

        }

        // POST: Application/Update/5
        [HttpPost]
        public ActionResult Update(int id, Application application)
        {
            string url = "applicationsdata/UpdateApplication/" + id;

            string jsonpayload = jss.Serialize(application);

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

        // GET: Application/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "applicationsdata/FindApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ApplicationDto seletedapplication = response.Content.ReadAsAsync<ApplicationDto>().Result;

            return View(seletedapplication);
        }

        // POST: Application/DeleteApplication/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "applicationsdata/DeleteApplication/" + id;

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
