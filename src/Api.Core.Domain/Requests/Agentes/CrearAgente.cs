using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearAgenteRequest : ApiRequestBase, IApiRequest<CrearAgenteRequestModel, CrearAgenteRequestOptions>
{
    public CrearAgenteRequestModel Model { get; set; } = new();
    public CrearAgenteRequestOptions Options { get; set; } = new();
}

public sealed class CrearAgenteRequestModel
{
    public Agente Agente { get; set; } = new();
}

public sealed class CrearAgenteRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearAgenteResponse : ApiResponseBase, IApiResponse<CrearAgenteResponseModel>
{
    public CrearAgenteResponseModel Model { get; set; } = new();
}

public sealed class CrearAgenteResponseModel
{
    public Agente Agente { get; set; } = new();
}
