using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar las existencias de un producto con características en un almacén en una fecha determinada.
/// </summary>
public sealed class BuscarExistenciasProductoConCaracteristicasRequest : ContpaqiRequest<
    BuscarExistenciasProductoConCaracteristicasRequestModel, BuscarExistenciasProductoConCaracteristicasRequestOptions,
    BuscarExistenciasProductoConCaracteristicasResponse>
{
    public BuscarExistenciasProductoConCaracteristicasRequest(BuscarExistenciasProductoConCaracteristicasRequestModel model,
        BuscarExistenciasProductoConCaracteristicasRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarExistenciasProductoConCaracteristicasRequest.
/// </summary>
public sealed class BuscarExistenciasProductoConCaracteristicasRequestModel
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
    ///     Fecha.
    /// </summary>
    public DateTime Fecha { get; set; } = DateTime.Today;

    /// <summary>
    ///     Abreviatura del Valor de la Característica 1 del producto.
    /// </summary>
    public string AbreviaturaValorCaracteristica1 { get; set; } = string.Empty;

    /// <summary>
    ///     Abreviatura del Valor de la Característica 2 del producto.
    /// </summary>
    public string AbreviaturaValorCaracteristica2 { get; set; } = string.Empty;

    /// <summary>
    ///     Abreviatura del Valor de la Característica 3 del producto.
    /// </summary>
    public string AbreviaturaValorCaracteristica3 { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud BuscarExistenciasProductoConCaracteristicasRequest.
/// </summary>
public sealed class BuscarExistenciasProductoConCaracteristicasRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud BuscarExistenciasProductoConCaracteristicasRequest.
/// </summary>
public sealed class
    BuscarExistenciasProductoConCaracteristicasResponse : ContpaqiResponse<BuscarExistenciasProductoConCaracteristicasResponseModel>
{
    public BuscarExistenciasProductoConCaracteristicasResponse(BuscarExistenciasProductoConCaracteristicasResponseModel model) : base(model)
    {
    }

    public static BuscarExistenciasProductoConCaracteristicasResponse CreateInstance(double existencias)
    {
        return new BuscarExistenciasProductoConCaracteristicasResponse(
            new BuscarExistenciasProductoConCaracteristicasResponseModel(existencias));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarExistenciasProductoConCaracteristicasResponse.
/// </summary>
public sealed class BuscarExistenciasProductoConCaracteristicasResponseModel
{
    public BuscarExistenciasProductoConCaracteristicasResponseModel(double existencias)
    {
        Existencias = existencias;
    }

    public double Existencias { get; set; }
}