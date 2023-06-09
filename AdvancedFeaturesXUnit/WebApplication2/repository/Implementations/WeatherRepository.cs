using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.repository.Interfaces;

namespace WebApplication2.repository.Implementations
{
    public class WeatherRepository : IWeatherRepository
    {
        public IEnumerable<WeatherForecast> GetWeatherForecasts(string[] summaries)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            }).ToList();
        }
    }
}
