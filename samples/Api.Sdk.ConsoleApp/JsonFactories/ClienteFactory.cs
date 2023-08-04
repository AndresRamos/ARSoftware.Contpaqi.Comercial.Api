using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums.CatalogosCfdi;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ClienteFactory
{
    public const string CodigoPrueba = "CLIENTEPRUEBA";

    private static CrearClienteRequest GetCrearClienteRequest()
    {
        return new CrearClienteRequest(new CrearClienteRequestModel { Cliente = GetModeloPrueba() }, new CrearClienteRequestOptions());
    }

    private static CrearClienteResponse GetCrearClienteResponse()
    {
        return new CrearClienteResponse(new CrearClienteResponseModel(GetModeloPrueba()));
    }

    private static ActualizarClienteRequest GetActualizarClienteRequest()
    {
        return new ActualizarClienteRequest(
            new ActualizarClienteRequestModel { CodigoCliente = CodigoPrueba, DatosCliente = GetDatosExtraPrueba() },
            new ActualizarClienteRequestOptions());
    }

    private static ActualizarClienteResponse GetActualizarClienteResponse()
    {
        return new ActualizarClienteResponse(new ActualizarClienteResponseModel(GetModeloPrueba()));
    }

    private static BuscarClientesRequest GetBuscarClientesRequest()
    {
        return new BuscarClientesRequest(
            new BuscarClientesRequestModel
            {
                Id = 1, Codigo = CodigoPrueba, SqlQuery = $"{nameof(admClientes.CRAZONSOCIAL)} = 'CLIENTE DE PRUEBAS'"
            }, new BuscarClientesRequestOptions());
    }

    private static BuscarClientesResponse GetBuscarClientesResponse()
    {
        return new BuscarClientesResponse(new BuscarClientesResponseModel(new List<ClienteProveedor> { GetModeloPrueba() }));
    }

    private static EliminarClienteRequest GetEliminarClienteRequest()
    {
        return new EliminarClienteRequest(new EliminarClienteRequestModel { CodigoCliente = CodigoPrueba },
            new EliminarClienteRequestOptions());
    }

    private static EliminarClienteResponse GetEliminarClienteResponse()
    {
        return new EliminarClienteResponse(new EliminarClienteResponseModel());
    }

    private static ClienteProveedor GetModeloPrueba()
    {
        return new ClienteProveedor
        {
            Codigo = CodigoPrueba,
            RazonSocial = "CLIENTE DE PRUEBAS",
            Rfc = "XAXX010101000",
            Tipo = TipoCliente.ClienteProveedor,
            UsoCfdi = UsoCfdiEnum.S01,
            RegimenFiscal = RegimenFiscalEnum._616,
            DireccionFiscal = new Direccion
            {
                Calle = "Pablo Villaseñor",
                NumeroExterior = "435",
                Colonia = "Ladrón de Guevara",
                Ciudad = "Guadalajara",
                Estado = "Jalisco",
                CodigoPostal = "44600",
                Pais = "México"
            },
            DatosExtra = GetDatosExtraPrueba()
        };
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admClientes.CEMAIL1), "email@cliente.com" },
            { nameof(admClientes.CTEXTOEXTRA1), "Texto Extra 1" },
            { nameof(admClientes.CTEXTOEXTRA2), "Texto Extra 2" },
            { nameof(admClientes.CTEXTOEXTRA3), "Texto Extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(ClienteProveedor)}.json"),
            JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearClienteRequest(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearClienteResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarClienteRequest(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarClienteResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarClientesRequest(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarClientesResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarClienteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetEliminarClienteRequest(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarClienteResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetEliminarClienteResponse(), options));
    }
}
