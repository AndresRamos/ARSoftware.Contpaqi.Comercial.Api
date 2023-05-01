using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class AgenteFactory
{
    public const string Codigo = "AGE001";
    public const string Nombre = "Agente 1";

    private static CrearAgenteRequest Crear()
    {
        var request = new CrearAgenteRequest(new CrearAgenteRequestModel(), new CrearAgenteRequestOptions());

        request.Model.Agente.Codigo = Codigo;
        request.Model.Agente.Nombre = Nombre;
        request.Model.Agente.Tipo = TipoAgente.VentasCobro;
        request.Model.Agente.DatosExtra = GetDatosExtra();

        return request;
    }

    private static ActualizarAgenteRequest Actualizar()
    {
        var request = new ActualizarAgenteRequest(new ActualizarAgenteRequestModel(), new ActualizarAgenteRequestOptions());

        request.Model.CodigoAgente = Codigo;
        request.Model.DatosAgente = GetDatosExtra();

        return request;
    }

    private static BuscarAgentesRequest BuscarPorId()
    {
        var request = new BuscarAgentesRequest(new BuscarAgentesRequestModel(), new BuscarAgentesRequestOptions());

        request.Model.Id = 100;

        return request;
    }

    private static BuscarAgentesRequest BuscarPorCodigo()
    {
        var request = new BuscarAgentesRequest(new BuscarAgentesRequestModel(), new BuscarAgentesRequestOptions());

        request.Model.Codigo = Codigo;

        return request;
    }

    private static BuscarAgentesRequest BuscarPorSql()
    {
        var request = new BuscarAgentesRequest(new BuscarAgentesRequestModel(), new BuscarAgentesRequestOptions());

        request.Model.SqlQuery = "CTIPOAGENTE = 1";

        return request;
    }

    private static BuscarAgentesRequest BuscarTodo()
    {
        var request = new BuscarAgentesRequest(new BuscarAgentesRequestModel(), new BuscarAgentesRequestOptions());

        return request;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admAgentes.CCOMISIONVENTAAGENTE), 5.ToString() }, { nameof(admAgentes.CCOMISIONCOBROAGENTE), 10.ToString() }
        };
    }

    public static Agente CrearAgentePrueba()
    {
        return new Agente { Codigo = Codigo, Nombre = Nombre, Tipo = TipoAgente.VentasCobro, DatosExtra = GetDatosExtra() };
    }

    public static Agente CrearAgenteDatosMinimos()
    {
        return new Agente { Codigo = Codigo };
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAgenteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAgenteRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_Sql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAgentesRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));
    }
}
