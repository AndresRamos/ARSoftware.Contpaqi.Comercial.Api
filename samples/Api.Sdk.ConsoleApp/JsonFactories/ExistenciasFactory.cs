using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class ExistenciasFactory
{
    private const string CodigoAlmacenPrueba = "ALMACENPRUEBA";
    private const string CodigoProductoPrueba = "PRODUCTOPRUEBA";

    private static BuscarExistenciasProductoRequest GetBuscarExistenciasProductoRequest()
    {
        return new BuscarExistenciasProductoRequest(
            new BuscarExistenciasProductoRequestModel
            {
                CodigoAlmacen = CodigoAlmacenPrueba, CodigoProducto = CodigoProductoPrueba, Fecha = DateTime.Now
            }, new BuscarExistenciasProductoRequestOptions());
    }

    private static BuscarExistenciasProductoResponse GetBuscarExistenciasProductoResponse()
    {
        return new BuscarExistenciasProductoResponse(new BuscarExistenciasProductoResponseModel(1));
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarExistenciasProductoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarExistenciasProductoResponse(), options));
    }
}