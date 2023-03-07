using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class EmpresaFactory
{
    public static BuscarEmpresasRequest BuscarTodo()
    {
        var request = new BuscarEmpresasRequest();

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarEmpresasRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));
    }
}
