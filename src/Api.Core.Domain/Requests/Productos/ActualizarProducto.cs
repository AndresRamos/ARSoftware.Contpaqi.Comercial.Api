using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class ActualizarProductoRequest : ApiRequestBase,
    IApiRequest<ActualizarProductoRequestModel, ActualizarProductoRequestOptions>
{
    public ActualizarProductoRequestModel Model { get; set; } = new();
    public ActualizarProductoRequestOptions Options { get; set; } = new();
}

public sealed class ActualizarProductoRequestModel
{
    public string CodigoProducto { get; set; } = string.Empty;
    public Dictionary<string, string> DatosProducto { get; set; } = new();
}

public sealed class ActualizarProductoRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class ActualizarProductoResponse : ApiResponseBase, IApiResponse<ActualizarProductoResponseModel>
{
    public ActualizarProductoResponseModel Model { get; set; } = new();
}

public sealed class ActualizarProductoResponseModel
{
    public Producto Producto { get; set; } = new();
}
