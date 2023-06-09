using Moq;
using System;
using System.Collections.Generic;
using WebApplication2.repository.Interfaces;

namespace TestProject1
{
    public class SomeFixture : IDisposable
    {
        public Mock<ISummaryRepository> SummaryRepositoryMock { get; private set; }
        public Mock<IWeatherRepository> WeatherRepositoryMock { get; private set; }

        public SomeFixture()
        {
            SetMocks();        
        }

        public void SetMocks()
        {
            SummaryRepositoryMock = new Mock<ISummaryRepository>();
            WeatherRepositoryMock = new Mock<IWeatherRepository>();
        }

        public void Dispose()
        {
            SummaryRepositoryMock = null;
            WeatherRepositoryMock = null;
        }
    }
}
