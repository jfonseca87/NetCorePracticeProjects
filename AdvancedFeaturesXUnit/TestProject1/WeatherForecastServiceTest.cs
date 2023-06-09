using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.services;
using Xunit;

namespace TestProject1
{
    [Collection("SomeCollection")]
    public class WeatherForecastServiceTest
    {
        private SomeFixture _someFixture;

        public WeatherForecastServiceTest(SomeFixture someFixture)
        {
            _someFixture = someFixture;
        }

        [Fact]
        public void GetWeatherForecastSuccessfull()
        {
            _someFixture.SummaryRepositoryMock.Setup(x => x.GetSummaries()).Returns(new[] {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            });

            _someFixture.WeatherRepositoryMock.Setup(x => x.GetWeatherForecasts(It.IsAny<string[]>()))
                .Returns(new List<WeatherForecast> { new WeatherForecast(), new WeatherForecast() });

            var weatherService = new WeatherForecastService(_someFixture.SummaryRepositoryMock.Object, _someFixture.WeatherRepositoryMock.Object);
            var result = weatherService.GetWeatherForecast();

            Assert.NotNull(result);
            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void GetWeatherForecastHasNoSummariesReturnEmptyList()
        {
            _someFixture.SummaryRepositoryMock.Setup(x => x.GetSummaries()).Returns((string[])null);

            var weatherService = new WeatherForecastService(
                _someFixture.SummaryRepositoryMock.Object, 
                _someFixture.WeatherRepositoryMock.Object);
            var result = weatherService.GetWeatherForecast();

            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
