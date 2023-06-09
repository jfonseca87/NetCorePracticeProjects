using System.Collections.Generic;
using WebApplication2.Models;
using WebApplication2.repository.Interfaces;

namespace WebApplication2.services
{
    public class WeatherForecastService
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastService(ISummaryRepository summaryRepository, IWeatherRepository weatherRepository)
        {
            _summaryRepository = summaryRepository;
            _weatherRepository = weatherRepository;
        }

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var summaries = _summaryRepository.GetSummaries();

            if (summaries == null) 
            {
                return new List<WeatherForecast>();
            }

            return _weatherRepository.GetWeatherForecasts(summaries);
        }
    }
}
