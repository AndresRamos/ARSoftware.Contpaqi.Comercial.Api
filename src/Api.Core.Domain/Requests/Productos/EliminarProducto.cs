namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un producto.
/// </summary>
public sealed class EliminarProductoRequest : IContpaqiRequest<EliminarProductoRequestModel, EliminarProductoRequestOptions>
{
    public EliminarProductoRequestModel Model { get; set; } = new();
    public EliminarProductoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud EliminarProductoRequest.
/// </summary>
public sealed class EliminarProductoRequestModel
{
    /// <summary>
    ///     Codigo del producto a eliminar.
    /// </summary>
    public string CodigoProducto { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud EliminarProductoRequest.
/// </summary>
public sealed class EliminarProductoRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud EliminarProductoRequest.
/// </summary>
public sealed class EliminarProductoResponse : IContpaqiResponse<EliminarProductoResponseModel>
{
    public EliminarProductoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta EliminarProductoResponse.
/// </summary>
public sealed class EliminarProductoResponseModel
{
}
