using System.Collections.Generic;
using WebApplication2.Models;

namespace WebApplication2.repository.Interfaces
{
    public interface IWeatherRepository
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts(string[] summaries);
    }
}
