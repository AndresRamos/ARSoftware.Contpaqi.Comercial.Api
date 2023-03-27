using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class AgenteFactory
{
    public const string Codigo = "AGE001";
    public const string Nombre = "Agente 1";

    public static CrearAgenteRequest Crear()
    {
        var request = new CrearAgenteRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Agente.Codigo = Codigo;
        request.Model.Agente.Nombre = Nombre;
        request.Model.Agente.Tipo = TipoAgente.VentasCobro;
        request.Model.Agente.DatosExtra = GetDatosExtra();

        return request;
    }

    public static ActualizarAgenteRequest Actualizar()
    {
        var request = new ActualizarAgenteRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.CodigoAgente = Codigo;
        request.Model.DatosAgente = GetDatosExtra();

        return request;
    }

    public static BuscarAgentesRequest BuscarPorId()
    {
        var request = new BuscarAgentesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Id = 100;

        return request;
    }

    public static BuscarAgentesRequest BuscarPorCodigo()
    {
        var request = new BuscarAgentesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Codigo = Codigo;

        return request;
    }

    public static BuscarAgentesRequest BuscarPorSql()
    {
        var request = new BuscarAgentesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.SqlQuery = "CTIPOAGENTE = 1";

        return request;
    }

    public static BuscarAgentesRequest BuscarTodo()
    {
        var request = new BuscarAgentesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        return request;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admAgentes.CFECHAALTAAGENTE), DateTime.Today.ToSdkFecha() },
            { nameof(admAgentes.CCOMISIONVENTAAGENTE), 5.ToString() },
            { nameof(admAgentes.CCOMISIONCOBROAGENTE), 10.ToString() }
        };
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAgenteRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAgenteRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_Sql.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));
    }
}
