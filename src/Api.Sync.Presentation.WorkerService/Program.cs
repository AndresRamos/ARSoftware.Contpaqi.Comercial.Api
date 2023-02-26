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
        services.AddApplicationServices(hostContext.Configuration).AddInfrastructureServices(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
    })
    .Build();

var mediator = host.Services.GetRequiredService<IMediator>();
var sesionService = host.Services.GetRequiredService<IComercialSdkSesionService>();

await mediator.Send(new IniciarSdkCommand());

host.Run();

if (sesionService.CanCerrarEmpresa)
    sesionService.CerrarEmpresa();

if (sesionService.CanTerminarSesion)
    sesionService.TerminarSesionSdk();
