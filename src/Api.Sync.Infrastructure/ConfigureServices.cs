using System.Reflection;
using Api.Sync.Core.Application.Api.Interfaces;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using Api.Sync.Infrastructure.ContpaqiComercial;
using Api.Sync.Infrastructure.ContpaqiComercialApi;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Common.Mappings;
using ARSoftware.Contpaqi.Comercial.Sql;
using ARSoftware.Contpaqi.Comercial.Sql.Contexts;
using ARSoftware.Contpaqi.Comercial.Sql.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable UnusedMethodReturnValue.Local

namespace Api.Sync.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddAutoMapper(Assembly.GetAssembly(typeof(DtoToModelMappings)));

        serviceCollection.AddContpaqiComercialApiServices().AddContpaqiComercialServices(configuration);

        return serviceCollection;
    }

    private static IServiceCollection AddContpaqiComercialApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IContpaqiComercialApiService, ContpaqiComercialApiService>((serviceProvider, httpClient) =>
        {
            ApiSyncConfig apiSyncConfig = serviceProvider.GetRequiredService<IOptions<ApiSyncConfig>>().Value;
            ContpaqiComercialConfig contpaqiComercialConfig = serviceProvider.GetRequiredService<IOptions<ContpaqiComercialConfig>>().Value;
            httpClient.BaseAddress = new Uri(apiSyncConfig.BaseAddress);
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiSyncConfig.SubscriptionKey);
            httpClient.DefaultRequestHeaders.Add("x-Empresa-Rfc", contpaqiComercialConfig.Empresa.Parametros!.Rfc);
        });

        return serviceCollection;
    }

    private static IServiceCollection AddContpaqiComercialServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ContpaqiComercialGeneralesDbContext>(
            builder =>
            {
                builder.UseSqlServer(ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialGeneralesConnectionString(
                        configuration.GetConnectionString("Contpaqi")!))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddDbContext<ContpaqiComercialEmpresaDbContext>((provider, builder) =>
        {
            ContpaqiComercialConfig config = provider.GetRequiredService<IOptions<ContpaqiComercialConfig>>().Value;
            builder.UseSqlServer(ContpaqiComercialSqlConnectionStringFactory.CreateContpaqiComercialEmpresaConnectionString(
                    configuration.GetConnectionString("Contpaqi")!, config.Empresa.BaseDatos))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddContpaqiComercialSqlRepositories();

        serviceCollection.AddTransient<IAgenteRepository, AgenteRepository>();
        serviceCollection.AddTransient<IAlmacenRepository, AlmacenRepository>();
        serviceCollection.AddTransient<IClienteRepository, ClienteRepository>();
        serviceCollection.AddTransient<IConceptoRepository, ConceptoRepository>();
        serviceCollection.AddTransient<IDocumentoRepository, DocumentoRepository>();
        serviceCollection.AddTransient<IEmpresaRepository, EmpresaRepository>();
        serviceCollection.AddTransient<IExistenciasProductoRepository, ExistenciasProductoRepository>();
        serviceCollection.AddTransient<IFolioDigitalRepository, FolioDigitalRepository>();
        serviceCollection.AddTransient<IMovimientoRepository, MovimientoRepository>();
        serviceCollection.AddTransient<IProductoRepository, ProductoRepository>();
        serviceCollection.AddTransient<IDireccionRepository, DireccionRepository>();
        serviceCollection.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();

        return serviceCollection;
    }
}
