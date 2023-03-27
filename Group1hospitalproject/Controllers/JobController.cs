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
    public class JobController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static JobController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/jobdata/");
        }
        // GET: Job/List
        public ActionResult List()
        {
            //objective: communicatie with our job data api to retrieve a list of jobs
            //curl https://localhost:44341/api/jobdata/ListJobs

   
            string url = "ListJobs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<JobDto> jobs = response.Content.ReadAsAsync<IEnumerable<JobDto>>().Result;
            Debug.WriteLine("Number of jobs received ");
            Debug.WriteLine(jobs.Count());

            return View(jobs);
        }

        // GET: Job/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicatie with our job data api to retrieve one job detail
            //curl https://localhost:44341/api/jobdata/FindJob/{id}


            string url = "FindJob/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            JobDto seletedjob = response.Content.ReadAsAsync<JobDto>().Result;


            return View(seletedjob);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Job/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        public ActionResult Create(Job job)
        {
            //curl -H "content-type:application/json" -d @job.json https://localhost:44341/api/jobdata/AddJob/
            string url = "AddJob";



            string jsonpayload = jss.Serialize(job);

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

        // GET: Job/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindJob/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            JobDto seletedjob = response.Content.ReadAsAsync<JobDto>().Result;

            return View(seletedjob);
        }

        // POST: Job/Update/5
        [HttpPost]
        public ActionResult Update(int id, Job job)
        {
            string url = "UpdateJob/" + id;

            string jsonpayload = jss.Serialize(job);

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

        // GET: Job/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindJob/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            JobDto seletedjob = response.Content.ReadAsAsync<JobDto>().Result;

            return View(seletedjob);
        }

        // POST: Job/DeleteJob/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeleteJob/" + id;

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
