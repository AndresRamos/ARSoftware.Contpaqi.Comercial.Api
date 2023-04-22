using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ClienteFactory
{
    public const string Codigo = "CTE001";
    public const string Nombre = "Cliente 1";
    public const string Rfc = "XAXX010101000";

    private static CrearClienteRequest Crear()
    {
        var request = new CrearClienteRequest();

        request.Model.Cliente = CrearClientePrueba();

        return request;
    }

    private static ActualizarClienteRequest Actualizar()
    {
        var request = new ActualizarClienteRequest();

        request.Model.CodigoCliente = Codigo;
        request.Model.DatosCliente = GetDatosExtra();

        return request;
    }

    private static BuscarClientesRequest BuscarPorId()
    {
        var request = new BuscarClientesRequest();

        request.Model.Id = 100;

        return request;
    }

    private static BuscarClientesRequest BuscarPorCodigo()
    {
        var request = new BuscarClientesRequest();

        request.Model.Codigo = Codigo;

        return request;
    }

    private static BuscarClientesRequest BuscarPorSql()
    {
        var request = new BuscarClientesRequest();

        request.Model.SqlQuery = "CRAZONSOCIAL = 'razonSocial'";

        return request;
    }

    private static BuscarClientesRequest BuscarTodo()
    {
        var request = new BuscarClientesRequest();

        return request;
    }

    private static EliminarClienteRequest Eliminar()
    {
        var request = new EliminarClienteRequest();

        request.Model.CodigoCliente = Codigo;

        return request;
    }

    public static Cliente CrearClientePrueba()
    {
        var cliente = new Cliente
        {
            Tipo = TipoCliente.ClienteProveedor,
            Codigo = Codigo,
            RazonSocial = Nombre,
            Rfc = Rfc,
            UsoCfdi = UsoCfdi.S01,
            RegimenFiscal = RegimenFiscal._616,
            DireccionFiscal =
            {
                Calle = "Pablo Villaseñor",
                NumeroExterior = "435",
                Colonia = "Ladrón de Guevara",
                Ciudad = "Guadalajara",
                Estado = "Jalisco",
                CodigoPostal = "44600",
                Pais = "México"
            },
            DatosExtra = GetDatosExtra()
        };

        return cliente;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admClientes.CFECHAALTA), DateTime.Today.ToSdkFecha() },
            { nameof(admClientes.CRAZONSOCIAL), Nombre },
            { nameof(admClientes.CRFC), Rfc },
            { nameof(admClientes.CTEXTOEXTRA1), "Texto Extra" }
        };
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearClienteRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarClienteRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_PorId.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_PorSql.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_Todo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarClienteRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Eliminar(), options));
    }
}
