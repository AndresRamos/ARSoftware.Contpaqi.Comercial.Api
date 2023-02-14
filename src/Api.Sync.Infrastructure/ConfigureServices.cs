using System.Reflection;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Core.Application.ContpaqiComercialApi.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial;
using Api.Sync.Infrastructure.ContpaqiComercialApi;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.Sync.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddContpaqiComercialApiServices().AddContpaqiComercialServices(configuration);

        return serviceCollection;
    }

    private static IServiceCollection AddContpaqiComercialApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IContpaqiComercialApiService, ContpaqiComercialApiService>((serviceProvider, httpClient) =>
        {
            ApiSyncConfig apiSyncConfig = serviceProvider.GetRequiredService<IOptions<ApiSyncConfig>>().Value;
            httpClient.BaseAddress = new Uri(apiSyncConfig.BaseAddress);
        });

        return serviceCollection;
    }

    private static IServiceCollection AddContpaqiComercialServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ContpaqiComercialGeneralesDbContext>((provider, builder) =>
            {
                builder.UseSqlServer(ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialGeneralesConnectionString(
                    configuration.GetConnectionString("Contpaqi")));
            },
            ServiceLifetime.Transient,
            ServiceLifetime.Transient);

        serviceCollection.AddDbContext<ContpaqiComercialEmpresaDbContext>((provider, builder) =>
            {
                ContpaqiComercialConfig config = provider.GetRequiredService<IOptions<ContpaqiComercialConfig>>().Value;
                builder.UseSqlServer(ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
                    configuration.GetConnectionString("Contpaqi"),
                    config.Empresa.BaseDatos));
            },
            ServiceLifetime.Transient,
            ServiceLifetime.Transient);

        serviceCollection.AddTransient<IEmpresaRepository, EmpresaRepository>();
        serviceCollection.AddTransient<IDocumentoRepository, DocumentoRepository>();
        serviceCollection.AddTransient<IClienteRepository, ClienteRepository>();

        return serviceCollection;
    }
}
