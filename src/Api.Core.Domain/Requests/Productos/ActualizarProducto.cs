using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un producto.
/// </summary>
public sealed class ActualizarProductoRequest : ApiRequestBase,
    IApiRequest<ActualizarProductoRequestModel, ActualizarProductoRequestOptions>
{
    public ActualizarProductoRequestModel Model { get; set; } = new();
    public ActualizarProductoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud ActualizarProductoRequest.
/// </summary>
public sealed class ActualizarProductoRequestModel
{
    /// <summary>
    ///     Codigo del producto a actualizar.
    /// </summary>
    public string CodigoProducto { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosProducto { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarProductoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarProductoRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarProductoRequest.
/// </summary>
public sealed class ActualizarProductoResponse : ApiResponseBase, IApiResponse<ActualizarProductoResponseModel>
{
    public ActualizarProductoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta ActualizarProductoResponse.
/// </summary>
public sealed class ActualizarProductoResponseModel
{
    public Producto Producto { get; set; } = new();
}
