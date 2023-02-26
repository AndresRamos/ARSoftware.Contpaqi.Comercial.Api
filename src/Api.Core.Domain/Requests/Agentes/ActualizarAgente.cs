using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class ActualizarAgenteRequest : ApiRequestBase, IApiRequest<ActualizarAgenteRequestModel, ActualizarAgenteRequestOptions>
{
    public ActualizarAgenteRequestModel Model { get; set; } = new();
    public ActualizarAgenteRequestOptions Options { get; set; } = new();
}

public sealed class ActualizarAgenteRequestModel
{
    public string CodigoAgente { get; set; } = string.Empty;
    public Dictionary<string, string> DatosAgente { get; set; } = new();
}

public sealed class ActualizarAgenteRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class ActualizarAgenteResponse : ApiResponseBase, IApiResponse<ActualizarAgenteResponseModel>
{
    public ActualizarAgenteResponseModel Model { get; set; } = new();
}

public sealed class ActualizarAgenteResponseModel
{
    public Agente Agente { get; set; } = new();
}
