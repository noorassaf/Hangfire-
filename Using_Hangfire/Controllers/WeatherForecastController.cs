using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Using_Hangfire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // BackgroundJob.Enqueue(() => sendEmail("noor@test.com")); now

            //Console.WriteLine(DateTime.Now); 
            //BackgroundJob.Schedule(() => sendEmail("noor@test.com"),TimeSpan.FromMinutes(1));//after time

            RecurringJob.AddOrUpdate(() => sendEmail("noor@test.com"),Cron.Minutely);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        public void sendEmail(string Email)
        {
           Console.WriteLine($"email sent at{DateTime.Now}");
        }
    }
}