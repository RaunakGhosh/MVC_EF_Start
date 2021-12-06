using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_EF_Start.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nancy.Json;
using MVC_EF_Start.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data.Entity;

namespace MVC_EF_Start.Controllers
{
  public class HomeController : Controller
  {
        public ApplicationDbContext dbcontext;
        public HomeController(ApplicationDbContext context)
        {
            dbcontext = context;
        }
        HttpClient httpClient;

        static string BASE_URL = "https://data.cityofnewyork.us/resource/hv77-qnda.json";
        static string API_KEY = "WvRS9PwFThGICVdPfqUaW1RUsL6LekAtG33iyObg"; //Add your API key here inside ""

        public IActionResult Index()
        {
            
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL;
            string parksData = "";

            //Parks parks = null;
            //Rootobject ro = new Rootobject();

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);
            //httpClient.BaseAddress = new Uri(BASE_URL);
            
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH)
                                                        .GetAwaiter().GetResult();
                //HttpResponseMessage response = httpClient.GetAsync(BASE_URL)
                //                                        .GetAwaiter().GetResult();
               

                
                
                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    //jArray = JArray.Parse(parksData);
                    
                    //output = JsonConvert.SerializeObject(parksData);
                }
                JArray jarray = JArray.Parse(parksData);
                List<Exam> examItems = ((JArray)jarray).Select(x => new Exam
                {
                    mean_scale_score = (string)x["mean_scale_score"],
                    number_tested = (string)x["number_tested"],
                    level_1_1 = (string)x["level_1_1"],
                    level_1_2 = (string)x["level_1_2"],
                    level_2_1 = (string)x["level_2_1"],
                    level_2_2 = (string)x["level_2_2"],
                    level_3_1 = (string)x["level_3_1"],
                    level_3_2 = (string)x["level_3_2"],
                    level_4_1 = (string)x["level_4_1"],
                    level_4_2 = (string)x["level_4_2"],
                    level_3_4_1 = (string)x["level_3_4_1"],
                    level_3_4_2 = (string)x["level_3_4_2"]
                }).ToList();

                List<Participants> partItems = ((JArray)jarray).Select(x => new Participants
                {
                    grade = (string)x["grade"],
                    year = (string)x["year"],
                   
                }).ToList();

                List<Category> catItems = ((JArray)jarray).Select(x => new Category
                {
                    category = (string)x["category"]


                }).ToList();
                
                    foreach(var i in examItems)
                {
                    Exam e1 = new Exam()
                    {
                        mean_scale_score = i.mean_scale_score,
                        number_tested = i.number_tested,
                        level_1_1 = i.level_1_1,
                        level_1_2 = i.level_1_2,
                        level_2_1 = i.level_2_1,
                        level_2_2 = i.level_2_2,
                        level_3_1 = i.level_3_1,
                        level_3_2 = i.level_3_2,
                        level_4_1 = i.level_4_1,
                        level_4_2 = i.level_4_2,
                        level_3_4_1 = i.level_3_4_1,
                        level_3_4_2 = i.level_3_4_2,
                        //cat_id = i.cat_id
                    };

                    dbcontext.Exams.Add(e1);
                    IEnumerable<Exam> x = new List<Exam>();
                    var test = (from name in dbcontext.Exams select name).ToList();
                    ViewBag.e1 = test;
                   
                }
                foreach (var i in partItems)
                {
                    Participants p1 = new Participants()
                    {
                        grade = i.grade,
                        year = i.year
                    };
                    dbcontext.Participants.Add(p1);
                }
                foreach (var i in catItems)
                {
                    Category c1 = new Category()
                    {
                        category = i.category,
                       
      
                    };
                    dbcontext.Category.Add(c1);
                }
                dbcontext.SaveChanges();

               
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }
            return View(ViewBag.e1);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Exam e)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                dbcontext.Add(e);
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
               
            
            return View();
        }

      
        public async Task<IActionResult> Update(int? id)
        {
            using (dbcontext)
            {
                var data = dbcontext.Exams.Where(x => x.examID == id).SingleOrDefault();
                return View(data);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(Exam data, int id)
        {

            using (dbcontext)
            {

                // Use of lambda expression to access
                // particular record from a database
                 var examData = dbcontext.Exams.FirstOrDefault(x => x.examID == id);

                // Checking if any such record exist 
                if (examData != null)
                {
                    examData.level_1_1 = data.level_1_1;
                    examData.level_1_2 = data.level_1_2;
                   
                    dbcontext.SaveChanges();

                    // It will redirect to 
                    // the Read method
                    return RedirectToAction("readData");
                }
                else
                    return View();
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //if (ModelState.IsValid)
            //{
            //    dbcontext.Update(data);
            //    await dbcontext.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            
        }

        public async Task<IActionResult> readData()
        {

            ViewBag.level1 =  dbcontext.Exams.Select(x => new { x.examID,x.mean_scale_score,x.number_tested, x.level_1_1, x.level_1_2,x.level_2_1 }).Take(20);

            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getExamDetails = await dbcontext.Exams.FindAsync(id);
            return View(getExamDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var getdet = await dbcontext.Exams.FindAsync(id);
            dbcontext.Exams.Remove(getdet);
            await dbcontext.SaveChangesAsync();
            return View(getdet);
        }
        public ViewResult DemoChart()
        {
            //var results = (from x in dbcontext.Participants

            //               select new
            //               {
            //                   grade = x.grade
            //               }).Take(5);



            //int[] label = new int[] { 1,2,3,4 };
            //List<int> labels = new List<int>(label);



            //List<string> ChartLabels = new List<string>();
            //ChartLabels = results.Select(p => p.grade).ToList();

            //ViewBag.Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'"));
            //ViewBag.Data = String.Join(",", labels.Select(d => d));


       
            return View();

        

    }
        // Enock start
        public IActionResult Profile()
        {

            return View();
        }
        // Enock end
    }
}