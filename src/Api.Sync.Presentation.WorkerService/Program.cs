using System.Reflection;
using Api.Sync.Core.Application;
using Api.Sync.Core.Application.ContpaqiComercial.Commands.IniciarSdk;
using Api.Sync.Infrastructure;
using Api.Sync.Presentation.WorkerService;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<ApiRequestHubClientFactory>();
        services.AddApplicationServices(hostContext.Configuration).AddInfrastructureServices(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

var mediator = host.Services.GetRequiredService<IMediator>();
var sdkSesionService = host.Services.GetRequiredService<IComercialSdkSesionService>();

await mediator.Send(new IniciarSdkCommand());

host.Run();

if (sdkSesionService.CanCerrarEmpresa)
{
    logger.LogInformation("Cerrando empresa.");
    sdkSesionService.CerrarEmpresa();
    logger.LogDebug("Empresa cerrada. {@ComercialSdkSesionService}", sdkSesionService);
}

if (sdkSesionService.CanTerminarSesion)
{
    logger.LogInformation("Terminando SDK.");
    sdkSesionService.TerminarSesionSdk();
    logger.LogDebug("SDK Terminado. {@ComercialSdkSesionService}", sdkSesionService);
}
