﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(int id)
        {
            Exam e2 = new Exam();
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
                        level_3_4_2 = i.level_3_4_2
                    };

                    dbcontext.Exams.Add(e1);

                   
                    ViewBag.e1 = e1;
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
                        category = i.category
      
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
            return View(e2);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Exam e)
        {
            if (ModelState.IsValid)
            {
                dbcontext.Add(e);
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(e);
        }

      

        //public IActionResult Contact()
        //{
        //    //GuestContact contact = new GuestContact();

        //    contact.Name = "Manish Agrawal";
        //    contact.Email = "magrawal@usf.edu";
        //    contact.Phone = "813-974-6716";


            /* alternate syntax to initialize object 
            GuestContact contact2 = new GuestContact
            {
              Name = "Manish Agrawal",
              Email = "magrawal@usf.edu",
              Phone = "813-974-6716"
            };
            */

            //ViewData["Message"] = "Your contact page.";

        //    return View(contact);
        //}
        //public IActionResult Demo()
        //{
        //    Demo demo = new Demo();

        //    demo.name = "Clue";
        //    demo.age = "eleven";



        //    return View(demo);
        //}

        //[HttpPost]
        //public IActionResult Contact(GuestContact contact)
        //{
        //    return View(contact);
        //}

        /// <summary>
        /// Replicate the chart example in the JavaScript presentation
        /// 
        /// Typically LINQ and SQL return data as collections.
        /// Hence we start the example by creating collections representing the x-axis labels and the y-axis values
        /// However, chart.js expects data as a string, not as a collection.
        ///   Hence we join the elements in the collections into strings in the view model
        /// </summary>
        /// <returns>View that will display the chart</returns>
        //public IActionResult Update(string cond)
        //{
            
        //    //fetch the records which match the given condition
        //    var level = dbcontext.Exams.Where(c => c.level_1_1 == cond).First();

        //    return View(level);
        //}
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getExamDetails = await dbcontext.Exams.FindAsync(id);
            return View(getExamDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Exam data)
        {

            if (ModelState.IsValid)
            {
                dbcontext.Update(data);
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(data);
        }

        public async Task<IActionResult> readData(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getExamDetails = await dbcontext.Exams.FindAsync(id);
            return View(getExamDetails);
        }
        public IActionResult Delete(string cond)
        {
            var exe = dbcontext.Exams.FirstOrDefault(x => x.number_tested == cond);
            if (exe != null)
            {
                dbcontext.Exams.Remove(exe);
                dbcontext.SaveChanges();
                TempData["shortMessage"] = "Deleted Successfully";
            }



            return RedirectToAction("mean", new { val = exe.mean_scale_score });
        }
        public ViewResult DemoChart()
        {
            string[] chartcols = { "#FFA07A", "#E9967A" };
            List<string> levels = new List<string>();
            levels.Add("level 1");levels.Add("Level 1 %");
            List<int> counts = new List<int>();
            try
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    var x = levels[i];
                    var lev = dbcontext.Exams.Where(c => c.level_1_1 == x).FirstOrDefault();
                    if (lev == null)
                        counts.Add(0);
                    else
                    {
                        var lev_end = dbcontext.Exams.Where(c => c.level_1_1 == lev.level_1_1).ToList();
                        counts.Add(lev_end.Count);
                    }
                }
            }
            catch (Exception el)
            {
                Console.WriteLine(el.Message);

            }
            ViewBag.levelcount = String.Join(",", counts.Select(d => d));
            ViewBag.level = String.Join(",", levels.Select(d => "'" + d + "'"));
            ViewBag.colors = String.Join(",", chartcols.Select(d => "'" + d + "'"));



            return View();



        }
    }
}