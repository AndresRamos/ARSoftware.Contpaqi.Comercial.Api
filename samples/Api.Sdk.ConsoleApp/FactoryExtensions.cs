using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Core.Domain.Common;

namespace Api.Sdk.ConsoleApp;

public class FactoryExtensions
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        return options;
    }
}
