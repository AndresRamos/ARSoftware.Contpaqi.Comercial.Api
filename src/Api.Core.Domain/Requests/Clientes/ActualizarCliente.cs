using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class ActualizarClienteRequest : ApiRequestBase, IApiRequest<ActualizarClienteRequestModel, ActualizarClienteRequestOptions>
{
    public ActualizarClienteRequestModel Model { get; set; } = new();
    public ActualizarClienteRequestOptions Options { get; set; } = new();
}

public sealed class ActualizarClienteRequestModel
{
    public string CodigoCliente { get; set; } = string.Empty;
    public Dictionary<string, string> DatosCliente { get; set; } = new();
}

public sealed class ActualizarClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class ActualizarClienteResponse : ApiResponseBase, IApiResponse<ActualizarClienteResponseModel>
{
    public ActualizarClienteResponseModel Model { get; set; } = new();
}

public sealed class ActualizarClienteResponseModel
{
    public Cliente Cliente { get; set; } = new();
}
