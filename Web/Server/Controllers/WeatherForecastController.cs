using KratosBlazorApp.Server.Extentions;
using KratosBlazorApp.Server.Services;
using KratosBlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace KratosBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IUserService userService, ILogger<WeatherForecastController> logger) : base(userService)
        {
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}