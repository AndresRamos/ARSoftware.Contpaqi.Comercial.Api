using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class EmpresaFactory
{
    private static BuscarEmpresasRequest BuscarTodo()
    {
        var request = new BuscarEmpresasRequest(new BuscarEmpresasRequestModel(), new BuscarEmpresasRequestOptions());

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarEmpresasRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));
    }
}
