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
                CodigoAlmacen = CodigoAlmacenPrueba, CodigoProducto = CodigoProductoPrueba, Fecha = DateTime.Today
            }, new BuscarExistenciasProductoRequestOptions());
    }

    private static BuscarExistenciasProductoResponse GetBuscarExistenciasProductoResponse()
    {
        return new BuscarExistenciasProductoResponse(new BuscarExistenciasProductoResponseModel(1));
    }

    private static BuscarExistenciasProductoConCaracteristicasRequest GetBuscarExistenciasProductoConCaracteristicasRequest()
    {
        return new BuscarExistenciasProductoConCaracteristicasRequest(
            new BuscarExistenciasProductoConCaracteristicasRequestModel
            {
                CodigoAlmacen = CodigoAlmacenPrueba,
                CodigoProducto = CodigoProductoPrueba,
                Fecha = DateTime.Today,
                AbreviaturaValorCaracteristica1 = "AB1",
                AbreviaturaValorCaracteristica2 = "AB2",
                AbreviaturaValorCaracteristica3 = "AB3"
            }, new BuscarExistenciasProductoConCaracteristicasRequestOptions());
    }

    private static BuscarExistenciasProductoConCaracteristicasResponse GetBuscarExistenciasProductoConCaracteristicasResponse()
    {
        return new BuscarExistenciasProductoConCaracteristicasResponse(new BuscarExistenciasProductoConCaracteristicasResponseModel(1));
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarExistenciasProductoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarExistenciasProductoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoConCaracteristicasRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarExistenciasProductoConCaracteristicasRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarExistenciasProductoConCaracteristicasResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarExistenciasProductoConCaracteristicasResponse(), options));
    }
}