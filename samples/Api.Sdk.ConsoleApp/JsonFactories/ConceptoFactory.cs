using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ConceptoFactory
{
    public static BuscarConceptosRequest BuscarPorId()
    {
        var request = new BuscarConceptosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Id = 100;

        return request;
    }

    public static BuscarConceptosRequest BuscarPorCodigo()
    {
        var request = new BuscarConceptosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Codigo = "400";

        return request;
    }

    public static BuscarConceptosRequest BuscarPorSql()
    {
        var request = new BuscarConceptosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.SqlQuery = "CNOMBRECONCEPTO = 'nombre'";

        return request;
    }

    public static BuscarConceptosRequest BuscarTodo()
    {
        var request = new BuscarConceptosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));
    }
}
