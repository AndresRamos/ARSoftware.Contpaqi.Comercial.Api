using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un producto.
/// </summary>
public sealed class CrearProductoRequest : ContpaqiRequest<CrearProductoRequestModel, CrearProductoRequestOptions, CrearProductoResponse>
{
    public CrearProductoRequest(CrearProductoRequestModel model, CrearProductoRequestOptions options) : base(model, options)
    {
    }
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
public sealed class CrearProductoResponse : ContpaqiResponse<CrearProductoResponseModel>
{
    public CrearProductoResponse(CrearProductoResponseModel model) : base(model)
    {
    }

    public static CrearProductoResponse CreateInstance(Producto producto)
    {
        return new CrearProductoResponse(new CrearProductoResponseModel(producto));
    }
}

/// <summary>
///     Modelo de la respuesta CrearProductoResponse.
/// </summary>
public sealed class CrearProductoResponseModel
{
    public CrearProductoResponseModel(Producto producto)
    {
        Producto = producto;
    }

    public Producto Producto { get; set; }
}
