using System.Text.Json;
using Api.Core.Domain.Common;

namespace Api.Sdk.ConsoleApp;

public static class FactoryExtensions
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;
        //options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        return options;
    }
}
