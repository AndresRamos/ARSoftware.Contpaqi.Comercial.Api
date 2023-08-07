using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class EmpresaFactory
{
    private static BuscarEmpresasRequest GetBuscarEmpresasRequest()
    {
        return new BuscarEmpresasRequest(new BuscarEmpresasRequestModel(), new BuscarEmpresasRequestOptions());
    }

    private static BuscarEmpresasResponse GetBuscarEmpresasResponse()
    {
        return new BuscarEmpresasResponse(new BuscarEmpresasResponseModel(new List<Empresa> { GetModeloPrueba() }));
    }

    private static Empresa GetModeloPrueba()
    {
        var empresa = new Empresa
        {
            Nombre = "Empresa de prueba",
            Ruta = @"C:\Compac\Empresas\adPRUEBAS_COMERCIAL",
            BaseDatos = "adPRUEBAS_COMERCIAL",
            Parametros = new Parametros
            {
                Rfc = "XAXX010101000",
                GuidAdd = "7e07b968-c30d-4809-b059-e809e4ef64e1",
                DatosExtra = new Dictionary<string, string>
                {
                    { nameof(admParametros.CIDEMPRESA), "1" },
                    { nameof(admParametros.CNOMBREEMPRESA), "Empresa de prueba" },
                    { nameof(admParametros.CRFCEMPRESA), "XAXX010101000" },
                    { nameof(admParametros.CGUIDDSL), "7e07b968-c30d-4809-b059-e809e4ef64e1" }
                }
            }
        };

        return empresa;
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Empresa)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarEmpresasRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarEmpresasRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarEmpresasResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarEmpresasResponse(), options));
    }
}
