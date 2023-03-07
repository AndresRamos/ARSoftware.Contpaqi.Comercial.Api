using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ConceptoFactory
{
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

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarConceptosRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));
    }
}
