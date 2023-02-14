using System.Reflection;
using Api.Sync.Core.Application;
using Api.Sync.Infrastructure;
using Api.Sync.Presentation.WorkerService;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddApplicationServices(hostContext.Configuration).AddInfrastructureServices(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
    })
    .Build();

host.Run();
