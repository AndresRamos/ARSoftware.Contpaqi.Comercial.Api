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

    public static ActualizaDireccionClienteRequest GetActualizaDireccionClienteRequest()
    {
        return new ActualizaDireccionClienteRequest(
            new ActualizaDireccionClienteRequestModel { CodigoCliente = "PRUEBA", Direccion = GetModeloPrueba() },
            new ActualizaDireccionClienteRequestOptions());
    }

    public static ActualizaDireccionClienteResponse GetActualizaDireccionClienteResponse()
    {
        return ActualizaDireccionClienteResponse.CreateInstance(GetModeloPrueba());
    }

    public static BuscarDireccionClienteRequest GetBuscarDireccionClienteRequest()
    {
        return new BuscarDireccionClienteRequest(
            new BuscarDireccionClienteRequestModel { CodigoCliente = "PRUEBA", Tipo = TipoDireccion.Fiscal },
            new BuscarDireccionClienteRequestOptions());
    }

    public static BuscarDireccionClienteResponse GetBuscarDireccionClienteResponse()
    {
        return BuscarDireccionClienteResponse.CreateInstance(GetModeloPrueba());
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

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizaDireccionClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizaDireccionClienteRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizaDireccionClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizaDireccionClienteResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDireccionClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarDireccionClienteRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDireccionClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarDireccionClienteResponse(), options));
    }
}
