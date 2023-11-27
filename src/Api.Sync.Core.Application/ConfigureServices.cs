using System.Reflection;
using Api.Sync.Core.Application.Common.Behaviours;
using Api.Sync.Core.Application.Common.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Sync.Core.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        serviceCollection.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            serviceConfiguration.RegisterServicesFromAssemblyContaining<ApiRequest>();
            serviceConfiguration.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
            serviceConfiguration.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            serviceConfiguration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        serviceCollection.Configure<ApiSyncConfig>(configuration.GetSection(nameof(ApiSyncConfig)));
        serviceCollection.PostConfigure<ApiSyncConfig>(apiSyncConfig => { apiSyncConfig.CalculateShutdownDateTime(); });
        serviceCollection.Configure<ContpaqiComercialConfig>(configuration.GetSection(nameof(ContpaqiComercialConfig)));

        serviceCollection.AddContpaqiComercialSdkServices();

        return serviceCollection;
    }
}
