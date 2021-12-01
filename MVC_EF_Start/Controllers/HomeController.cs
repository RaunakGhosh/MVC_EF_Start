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



                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
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

        public IActionResult IndexWithLayout()
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