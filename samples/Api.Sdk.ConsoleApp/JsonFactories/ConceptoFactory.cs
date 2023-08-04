using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ConceptoFactory
{
    public const string CodigoPrueba = "FACTURAPRUEBA";

    private static BuscarConceptosRequest GetBuscarConceptosRequest()
    {
        return new BuscarConceptosRequest(
            new BuscarConceptosRequestModel
            {
                Id = 1, Codigo = CodigoPrueba, SqlQuery = $"{nameof(admConceptos.CCODIGOCONCEPTO)} = 'PRUEBAFACTURA'"
            }, new BuscarConceptosRequestOptions());
    }

    private static BuscarConceptosResponse GetBuscarConceptosResponse()
    {
        return new BuscarConceptosResponse(new BuscarConceptosResponseModel(new List<ConceptoDocumento> { GetModeloPrueba() }));
    }

    private static ConceptoDocumento GetModeloPrueba()
    {
        return new ConceptoDocumento { Id = 1, Codigo = CodigoPrueba, Nombre = "FACTURA DE PRUEBAS", DatosExtra = GetDatosExtraPrueba() };
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admConceptos.CIDCONCEPTODOCUMENTO), "1" },
            { nameof(admConceptos.CCODIGOCONCEPTO), CodigoPrueba },
            { nameof(admConceptos.CNOMBRECONCEPTO), "FACTURA DE PRUEBAS" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(ConceptoDocumento)}.json"),
            JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarConceptosRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarConceptosResponse(), options));
    }
}
