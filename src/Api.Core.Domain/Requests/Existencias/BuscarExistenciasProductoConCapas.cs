using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar las existencias de un producto por lote y/o pedimento.
/// </summary>
public sealed class BuscarExistenciasProductoConCapasRequest : ContpaqiRequest<BuscarExistenciasProductoConCapasRequestModel,
    BuscarExistenciasProductoConCapasRequestOptions, BuscarExistenciasProductoConCapasResponse>
{
    public BuscarExistenciasProductoConCapasRequest(BuscarExistenciasProductoConCapasRequestModel model,
        BuscarExistenciasProductoConCapasRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarExistenciasProductoConCapasRequest.
/// </summary>
public sealed class BuscarExistenciasProductoConCapasRequestModel
{
    /// <summary>
    ///     Código del producto.
    /// </summary>
    public string CodigoProducto { get; set; } = string.Empty;

    /// <summary>
    ///     Código del almacén.
    /// </summary>
    public string CodigoAlmacen { get; set; } = string.Empty;

    /// <summary>
    ///     Numero de pedimento.
    /// </summary>
    public string Pedimento { get; set; } = string.Empty;

    /// <summary>
    ///     Numero de lote.
    /// </summary>
    public string Lote { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud BuscarExistenciasProductoConCapasRequest.
/// </summary>
public sealed class BuscarExistenciasProductoConCapasRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud BuscarExistenciasProductoConCapasRequest.
/// </summary>
public sealed class BuscarExistenciasProductoConCapasResponse : ContpaqiResponse<BuscarExistenciasProductoConCapasResponseModel>
{
    public BuscarExistenciasProductoConCapasResponse(BuscarExistenciasProductoConCapasResponseModel model) : base(model)
    {
    }

    public static BuscarExistenciasProductoConCapasResponse CreateInstance(double existencias)
    {
        return new BuscarExistenciasProductoConCapasResponse(new BuscarExistenciasProductoConCapasResponseModel(existencias));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarExistenciasProductoConCapasResponse.
/// </summary>
public sealed class BuscarExistenciasProductoConCapasResponseModel
{
    public BuscarExistenciasProductoConCapasResponseModel(double existencias)
    {
        Existencias = existencias;
    }

    public double Existencias { get; set; }
}