using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class AgenteFactory
{
    public const string CodigoPrueba = "AGENTEPRUEBA";

    private static CrearAgenteRequest GetCrearAgenteRequest()
    {
        return new CrearAgenteRequest(new CrearAgenteRequestModel { Agente = GetModeloPrueba() }, new CrearAgenteRequestOptions());
    }

    private static CrearAgenteResponse GetCrearAgenteResponse()
    {
        return new CrearAgenteResponse(new CrearAgenteResponseModel(GetModeloPrueba()));
    }

    private static ActualizarAgenteRequest GetActualizarAgenteRequest()
    {
        return new ActualizarAgenteRequest(
            new ActualizarAgenteRequestModel { CodigoAgente = CodigoPrueba, DatosAgente = GetDatosExtraPrueba() },
            new ActualizarAgenteRequestOptions());
    }

    private static ActualizarAgenteResponse GetActualizarAgenteResponse()
    {
        return new ActualizarAgenteResponse(new ActualizarAgenteResponseModel(GetModeloPrueba()));
    }

    private static BuscarAgentesRequest GetBuscarAgentesRequest()
    {
        return new BuscarAgentesRequest(
            new BuscarAgentesRequestModel
            {
                Id = 1, Codigo = CodigoPrueba, SqlQuery = $"{nameof(admAgentes.CNOMBREAGENTE)} = 'AGENTE DE PRUEBAS'"
            }, new BuscarAgentesRequestOptions());
    }

    private static BuscarAgentesResponse GetBuscarAgentesResponse()
    {
        return new BuscarAgentesResponse(new BuscarAgentesResponseModel(new List<Agente> { GetModeloPrueba() }));
    }

    private static Agente GetModeloPrueba()
    {
        return new Agente
        {
            Codigo = CodigoPrueba, Nombre = "AGENTE DE PRUEBAS", Tipo = TipoAgente.VentasCobro, DatosExtra = GetDatosExtraPrueba()
        };
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admAgentes.CCOMISIONVENTAAGENTE), "5" },
            { nameof(admAgentes.CCOMISIONCOBROAGENTE), "10" },
            { nameof(admAgentes.CTEXTOEXTRA1), "Texto extra 1" },
            { nameof(admAgentes.CTEXTOEXTRA2), "Texto extra 2" },
            { nameof(admAgentes.CTEXTOEXTRA3), "Texto extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Agente)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAgenteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearAgenteRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAgenteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearAgenteResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAgenteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarAgenteRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAgenteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarAgenteResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarAgentesRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarAgentesResponse(), options));
    }
}
