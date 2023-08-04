using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un producto.
/// </summary>
public sealed class ActualizarProductoRequest : ContpaqiRequest<ActualizarProductoRequestModel, ActualizarProductoRequestOptions,
    ActualizarProductoResponse>
{
    public ActualizarProductoRequest(ActualizarProductoRequestModel model, ActualizarProductoRequestOptions options) : base(model, options)
    {
    }
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
public sealed class ActualizarProductoResponse : ContpaqiResponse<ActualizarProductoResponseModel>
{
    public ActualizarProductoResponse(ActualizarProductoResponseModel model) : base(model)
    {
    }

    public static ActualizarProductoResponse CreateInstance(Producto producto)
    {
        return new ActualizarProductoResponse(new ActualizarProductoResponseModel(producto));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarProductoResponse.
/// </summary>
public sealed class ActualizarProductoResponseModel
{
    public ActualizarProductoResponseModel(Producto producto)
    {
        Producto = producto;
    }

    public Producto Producto { get; set; }
}
