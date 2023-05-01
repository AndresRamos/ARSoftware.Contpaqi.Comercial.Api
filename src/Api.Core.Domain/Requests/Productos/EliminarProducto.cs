using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un producto.
/// </summary>
public sealed class
    EliminarProductoRequest : ContpaqiRequest<EliminarProductoRequestModel, EliminarProductoRequestOptions, EliminarProductoResponse>
{
    public EliminarProductoRequest(EliminarProductoRequestModel model, EliminarProductoRequestOptions options) : base(model, options)
    {
    }
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
public sealed class EliminarProductoResponse : ContpaqiResponse<EliminarProductoResponseModel>
{
    public EliminarProductoResponse(EliminarProductoResponseModel model) : base(model)
    {
    }

    public static EliminarProductoResponse CreateInstance()
    {
        return new EliminarProductoResponse(new EliminarProductoResponseModel());
    }
}

/// <summary>
///     Modelo de la respuesta EliminarProductoResponse.
/// </summary>
public sealed class EliminarProductoResponseModel
{
}
