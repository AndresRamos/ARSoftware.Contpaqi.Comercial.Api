using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ConceptoFactory
{
    public static BuscarConceptosRequest BuscarPorId()
    {
        var request = new BuscarConceptosRequest();
        

        request.Model.Id = 100;

        return request;
    }

    public static BuscarConceptosRequest BuscarPorCodigo()
    {
        var request = new BuscarConceptosRequest();
        

        request.Model.Codigo = "400";

        return request;
    }

    public static BuscarConceptosRequest BuscarPorSql()
    {
        var request = new BuscarConceptosRequest();
        

        request.Model.SqlQuery = "CNOMBRECONCEPTO = 'nombre'";

        return request;
    }

    public static BuscarConceptosRequest BuscarTodo()
    {
        var request = new BuscarConceptosRequest();
        

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorId.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_PorSql.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_Todo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarTodo(), options));
    }
}
