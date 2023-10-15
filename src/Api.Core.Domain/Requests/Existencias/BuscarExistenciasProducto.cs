using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar existencias de un producto en un almacén en una fecha determinada.
/// </summary>
public sealed class BuscarExistenciasProductoRequest : ContpaqiRequest<BuscarExistenciasProductoRequestModel,
    BuscarExistenciasProductoRequestOptions, BuscarExistenciasProductoResponse>
{
    public BuscarExistenciasProductoRequest(BuscarExistenciasProductoRequestModel model, BuscarExistenciasProductoRequestOptions options) :
        base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarExistenciasProductoRequest.
/// </summary>
public sealed class BuscarExistenciasProductoRequestModel
{
    /// <summary>
    ///     Código del producto.
    /// </summary>
    public string CodigoProducto { get; set; }

    /// <summary>
    ///     Código del almacén.
    /// </summary>
    public string CodigoAlmacen { get; set; }

    /// <summary>
    ///     Fecha.
    /// </summary>
    public DateTime Fecha { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarExistenciasProductoRequest.
/// </summary>
public sealed class BuscarExistenciasProductoRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud BuscarExistenciasProductoRequest.
/// </summary>
public sealed class BuscarExistenciasProductoResponse : ContpaqiResponse<BuscarExistenciasProductoResponseModel>
{
    public BuscarExistenciasProductoResponse(BuscarExistenciasProductoResponseModel model) : base(model)
    {
    }

    public static BuscarExistenciasProductoResponse CreateInstance(double existencias)
    {
        return new BuscarExistenciasProductoResponse(new BuscarExistenciasProductoResponseModel(existencias));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarExistenciasProductoResponse.
/// </summary>
public sealed class BuscarExistenciasProductoResponseModel
{
    public BuscarExistenciasProductoResponseModel(double existencias)
    {
        Existencias = existencias;
    }

    public double Existencias { get; set; }
}