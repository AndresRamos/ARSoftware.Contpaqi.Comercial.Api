using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class UnidadesMedidaFactory
{
    private static CrearUnidadMedidaRequest GetCrearUnidadMedidaRequest()
    {
        return new CrearUnidadMedidaRequest(new CrearUnidadMedidaRequestModel { UnidadMedida = GetModeloPrueba() },
            new CrearUnidadMedidaRequestOptions());
    }

    private static CrearUnidadMedidaResponse GetCrearUnidadMedidaResponse()
    {
        return new CrearUnidadMedidaResponse(new CrearUnidadMedidaResponseModel(GetModeloPrueba()));
    }

    private static ActualizarUnidadMedidaRequest GetActualizarUnidadMedidaRequest()
    {
        return new ActualizarUnidadMedidaRequest(
            new ActualizarUnidadMedidaRequestModel { Nombre = "PRUEBA", UnidadMedida = GetModeloPrueba() },
            new ActualizarUnidadMedidaRequestOptions());
    }

    private static ActualizarUnidadMedidaResponse GetActualizarUnidadMedidaResponse()
    {
        return new ActualizarUnidadMedidaResponse(new ActualizarUnidadMedidaResponseModel(GetModeloPrueba()));
    }

    private static BuscarUnidadesMedidaRequest GetBuscarUnidadesMedidaRequest()
    {
        return new BuscarUnidadesMedidaRequest(new BuscarUnidadesMedidaRequestModel(), new BuscarUnidadesMedidaRequestOptions());
    }

    private static BuscarUnidadesMedidaResponse GetBuscarUnidadesMedidaResponse()
    {
        return new BuscarUnidadesMedidaResponse(new BuscarUnidadesMedidaResponseModel(new List<UnidadMedida> { GetModeloPrueba() }));
    }

    public static UnidadMedida GetModeloPrueba()
    {
        return new UnidadMedida { Nombre = "PRUEBA", Abreviatura = "PZ", ClaveSat = "H87" };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(UnidadMedida)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearUnidadMedidaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearUnidadMedidaRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearUnidadMedidaResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearUnidadMedidaResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarUnidadMedidaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarUnidadMedidaRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarUnidadMedidaResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarUnidadMedidaResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarUnidadesMedidaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarUnidadesMedidaRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarUnidadesMedidaResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarUnidadesMedidaResponse(), options));
    }
}
