using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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
        private readonly FolderSettings _folderSettings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<FolderSettings> options)
        {
            _logger = logger;
            _folderSettings = options.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var weatherForecastRecords = Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            _logger.LogInformation($"Path: {_folderSettings.Path}");
            string filename = $"weatherforecast_{DateTime.UtcNow.Ticks}.json";
            await using FileStream streamCreated = System.IO.File.Create($"{_folderSettings.Path}{filename}");
            await JsonSerializer.SerializeAsync(streamCreated, weatherForecastRecords);

            return Ok();
        }
    }
}