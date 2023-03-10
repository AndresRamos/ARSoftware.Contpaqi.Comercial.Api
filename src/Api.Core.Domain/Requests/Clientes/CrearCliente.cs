using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearClienteRequest : ApiRequestBase, IApiRequest<CrearClienteRequestModel, CrearClienteRequestOptions>

{
    public CrearClienteRequestModel Model { get; set; } = new();
    public CrearClienteRequestOptions Options { get; set; } = new();
}

public sealed class CrearClienteRequestModel
{
    public Cliente Cliente { get; set; } = new();
}

public sealed class CrearClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearClienteResponse : ApiResponseBase, IApiResponse<CrearClienteResponseModel>
{
    public CrearClienteResponseModel Model { get; set; } = new();
}

public sealed class CrearClienteResponseModel
{
    public Cliente Cliente { get; set; } = new();
}
