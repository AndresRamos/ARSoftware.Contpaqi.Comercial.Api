using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un producto.
/// </summary>
public sealed class CrearProductoRequest : ApiRequestBase, IApiRequest<CrearProductoRequestModel, CrearProductoRequestOptions>
{
    public CrearProductoRequestModel Model { get; set; } = new();
    public CrearProductoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearProductoRequest.
/// </summary>
public sealed class CrearProductoRequestModel
{
    public Producto Producto { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearProductoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearProductoRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearProductoRequest.
/// </summary>
public sealed class CrearProductoResponse : ApiResponseBase, IApiResponse<CrearProductoResponseModel>
{
    public CrearProductoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CrearProductoResponse.
/// </summary>
public sealed class CrearProductoResponseModel
{
    public Producto Producto { get; set; } = new();
}
