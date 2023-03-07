using System.Text.Json;

namespace Api.Core.Domain.Common;

public static class JsonExtensions
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions(JsonSerializerDefaults.Web) { TypeInfoResolver = new PolymorphicTypeResolver() };
    }
}
