using System.Text.Json;
using Api.Core.Domain.Common;
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

    public static CrearClienteRequest Crear()
    {
        var request = new CrearClienteRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Cliente.Tipo = TipoCliente.ClienteProveedor;
        request.Model.Cliente.Codigo = Codigo;
        request.Model.Cliente.RazonSocial = Nombre;
        request.Model.Cliente.Rfc = Rfc;
        request.Model.Cliente.UsoCfdi = UsoCfdi.S01;
        request.Model.Cliente.RegimenFiscal = RegimenFiscal._616;
        request.Model.Cliente.DireccionFiscal.Calle = "Pablo Villaseñor";
        request.Model.Cliente.DireccionFiscal.NumeroExterior = "435";
        request.Model.Cliente.DireccionFiscal.Colonia = "Ladrón de Guevara";
        request.Model.Cliente.DireccionFiscal.Ciudad = "Guadalajara";
        request.Model.Cliente.DireccionFiscal.Estado = "Jalisco";
        request.Model.Cliente.DireccionFiscal.CodigoPostal = "44600";
        request.Model.Cliente.DireccionFiscal.Pais = "México";

        request.Model.Cliente.DatosExtra = GetDatosExtra();

        return request;
    }

    public static ActualizarClienteRequest Actualizar()
    {
        var request = new ActualizarClienteRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.CodigoCliente = Codigo;
        request.Model.DatosCliente = GetDatosExtra();

        return request;
    }

    public static BuscarClientesRequest BuscarPorId()
    {
        var request = new BuscarClientesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Id = 100;

        return request;
    }

    public static BuscarClientesRequest BuscarPorCodigo()
    {
        var request = new BuscarClientesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Codigo = Codigo;

        return request;
    }

    public static BuscarClientesRequest BuscarTodo()
    {
        var request = new BuscarClientesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        return request;
    }

    public static EliminarClienteRequest Eliminar()
    {
        var request = new EliminarClienteRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.CodigoCliente = Codigo;

        return request;
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
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearClienteRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarClienteRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarClientesRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarClienteRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Eliminar(), options));
    }
}
