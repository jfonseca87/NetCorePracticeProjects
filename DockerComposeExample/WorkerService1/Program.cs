using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.ConfigureOptions<FileOptionsSetup>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
