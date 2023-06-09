using WorkerService1.Models;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FileSettings _fileSettings;

        public Worker(ILogger<Worker> logger, IOptions<FileSettings> fileSettings)
        {
            _logger = logger;
            _fileSettings = fileSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Thread.Sleep(3000);

                int filesProcessed = 0;
                string[] pendingFiles = Directory.GetFiles(_fileSettings.Path);
                if (!pendingFiles.Any())
                {
                    _logger.LogInformation("There are not new files to process");
                    continue;
                }

                foreach (var file in pendingFiles.ToList())
                {
                    using StreamReader jsonStream = new StreamReader(file);
                    string jsonStr = await jsonStream.ReadToEndAsync();
                    jsonStream.Close();
                    if (string.IsNullOrEmpty(jsonStr))
                    {
                        continue;
                    }

                    IEnumerable<WeatherForecast> forecastRecords = 
                        JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(jsonStr);
                    
                    // save data in the database
                    using var conn = new SqlConnection(_fileSettings.SqlConnection);
                    await conn.ExecuteAsync(@"INSERT INTO weatherforecast (Date, TemperatureC, TemperatureF, Summary)
                                              VALUES(@Date, @TemperatureC, @TemperatureF, @Summary)", forecastRecords);

                    // delete file after have been processed
                    File.Delete(file);
                    filesProcessed++;
                }

                _logger.LogInformation($"{filesProcessed} files were processed successfully!!");
            }
        }
    }
}