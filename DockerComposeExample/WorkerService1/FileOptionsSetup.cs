using Microsoft.Extensions.Options;
using WorkerService1;
using WorkerService1.Models;

public class FileOptionsSetup : IConfigureOptions<FileSettings>
{
    private const string fileOptions = "FileSettings";
    private readonly IConfiguration _configuration;

    public FileOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(FileSettings options)
    {
        _configuration.GetSection(fileOptions).Bind(options);
    }
}