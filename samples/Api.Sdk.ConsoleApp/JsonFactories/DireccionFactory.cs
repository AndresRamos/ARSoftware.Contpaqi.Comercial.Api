using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class DireccionFactory
{
    public static CrearDireccionClienteRequest GetCrearDireccionClienteRequest()
    {
        return new CrearDireccionClienteRequest(
            new CrearDireccionClienteRequestModel { CodigoCliente = "PRUEBA", Direccion = GetModeloPrueba() },
            new CrearDireccionClienteRequestOptions());
    }

    public static CrearDireccionClienteResponse GetCrearDireccionClienteResponse()
    {
        return CrearDireccionClienteResponse.CreateInstance(GetModeloPrueba());
    }

    private static Direccion GetModeloPrueba()
    {
        return new Direccion
        {
            TipoCatalogo = TipoCatalogoDireccion.Clientes,
            Tipo = TipoDireccion.Fiscal,
            Calle = "Calle",
            NumeroExterior = "1",
            NumeroInterior = "1",
            Colonia = "Colonia",
            Ciudad = "Ciudad",
            Estado = "Estado",
            CodigoPostal = "123456",
            Pais = "Mexico"
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Direccion)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDireccionClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearDireccionClienteRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDireccionClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearDireccionClienteResponse(), options));
    }
}
