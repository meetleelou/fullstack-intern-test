using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fullstack_intern_test.Models;
using System.Net.Http;
using Newtonsoft.Json;


namespace fullstack_intern_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<Controller> _logger;

        public WeatherForecastController(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                DoB = DateTime.Now.AddDays(index),
                FirstName = Summaries[rng.Next(Summaries.Length)],
                LastName = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            // return myownnameclass
        }
        
        [Route("test")]
        [HttpGet]
        public async Task<Result[]> GetTest()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://randomuser.me/api/?results=10");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            RandomUserMe randomUserMe = JsonConvert.DeserializeObject<RandomUserMe>(data);
            Result[] results = randomUserMe.results;
            _logger.LogCritical(results.Length.ToString());
            return results;
        }

        [Route("welcome")]
        // GET: http://localhost:5000/weatherforecast/welcome
        public IActionResult Welcome()
        {
            return View();
        }



    }
}
