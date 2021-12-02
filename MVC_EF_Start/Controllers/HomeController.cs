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
namespace MVC_EF_Start.Controllers
{
  public class HomeController : Controller
  {
        HttpClient httpClient;

        static string BASE_URL = "https://data.cityofnewyork.us/resource/hv77-qnda.json";
        static string API_KEY = "WvRS9PwFThGICVdPfqUaW1RUsL6LekAtG33iyObg"; //Add your API key here inside ""

        public IActionResult Index(int id)
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


                string category = null;
                string year = null;
                string grade = null;
                string number_tested = null;
                String mean_score = null;
                String level_1_1 = null;
                String level_1_2 = null;
                String level_2_1 = null;
                String level_2_2 = null;
                String level_3_1 = null;
                String level_3_2 = null;
                String level_4_1 = null;
                String level_4_2 = null;
                String level_3_4_1 = null;
                String level_3_4_2 = null;
                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    foreach (var x in parksData)
                    {
                         category = x['category'];
                         year = x['year'];
                         grade = x['grade'];
                         number_tested = x['number_tested'];
                         mean_score = x['mean_scale_score'];
                         level_1_1 = x['level_1_1'];
                         level_1_2 = x['level_1_2'];
                         level_2_1 = x['level_2_1'];
                         level_2_2 = x['level_2_2'];
                         level_3_1 = x['level_3_1'];
                         level_3_2 = x['level_3_2'];
                         level_4_1 = x['level_4_1'];
                         level_4_2 = x['level_4_2'];
                         level_3_4_1 = x['level_3_4_1'];
                         level_3_4_2 = x['level_3_4_2'];
                    }


                    Exam exam1 = new Exam();
                    Participants participants1 = new Participants();
                    Category category1 = new Category();
             
                    exam1.mean_scale_score = mean_score;
                    exam1.number_tested = number_tested;
                    exam1.level_1_1 = level_1_1;
                    exam1.level_1_2 = level_1_2;
                    exam1.level_2_1 = level_2_1;
                    exam1.level_2_2 = level_2_2;
                    exam1.level_3_1 = level_3_1;
                    exam1.level_3_2 = level_3_2;
                    exam1.level_4_1 = level_4_1;
                    exam1.level_4_2 = level_4_2;
                    exam1.level_3_4_1 = level_3_4_1;
                    exam1.level_3_4_2 = level_3_4_2;

                    participants1.grade = grade;
                    participants1.year = year;

                    category1.category = category;


                }
                }


                return View();
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }
            return View();
        }

        public IActionResult CreateStudent()
        {
            return View();
        }

        public IActionResult UpdateStudent()
        {
            return View();
        }

        public IActionResult Contact()
        {
            GuestContact contact = new GuestContact();

            contact.Name = "Manish Agrawal";
            contact.Email = "magrawal@usf.edu";
            contact.Phone = "813-974-6716";


            /* alternate syntax to initialize object 
            GuestContact contact2 = new GuestContact
            {
              Name = "Manish Agrawal",
              Email = "magrawal@usf.edu",
              Phone = "813-974-6716"
            };
            */

            //ViewData["Message"] = "Your contact page.";

            return View(contact);
        }
        //public IActionResult Demo()
        //{
        //    Demo demo = new Demo();

        //    demo.name = "Clue";
        //    demo.age = "eleven";



        //    return View(demo);
        //}

        [HttpPost]
        public IActionResult Contact(GuestContact contact)
        {
            return View(contact);
        }

        /// <summary>
        /// Replicate the chart example in the JavaScript presentation
        /// 
        /// Typically LINQ and SQL return data as collections.
        /// Hence we start the example by creating collections representing the x-axis labels and the y-axis values
        /// However, chart.js expects data as a string, not as a collection.
        ///   Hence we join the elements in the collections into strings in the view model
        /// </summary>
        /// <returns>View that will display the chart</returns>
        public ViewResult DemoChart()
        {
            string[] ChartLabels = new string[] { "Africa", "Asia", "Europe", "Latin America", "North America" };
            int[] ChartData = new int[] { 2478, 5267, 734, 784, 433 };

            ChartModel Model = new ChartModel
            {
                ChartType = "bar",
                Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'")),
                Data = String.Join(",", ChartData.Select(d => d)),
                Title = "Predicted world population (millions) in 2050"
            };

            return View(Model);
        }
    }
}