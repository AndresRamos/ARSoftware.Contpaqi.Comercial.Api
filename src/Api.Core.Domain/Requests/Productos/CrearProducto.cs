using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearProductoRequest : ApiRequestBase, IApiRequest<CrearProductoRequestModel, CrearProductoRequestOptions>
{
    public CrearProductoRequestModel Model { get; set; } = new();
    public CrearProductoRequestOptions Options { get; set; } = new();
}

public sealed class CrearProductoRequestModel
{
    public Producto Producto { get; set; } = new();
}

public sealed class CrearProductoRequestOptions
{
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearProductoResponse : ApiResponseBase, IApiResponse<CrearProductoResponseModel>
{
    public CrearProductoResponseModel Model { get; set; } = new();
}

public sealed class CrearProductoResponseModel
{
    public Producto Producto { get; set; } = new();
}
