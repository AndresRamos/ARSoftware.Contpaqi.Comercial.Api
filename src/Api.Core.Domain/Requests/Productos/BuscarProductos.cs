using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar productos.
/// </summary>
public sealed class BuscarProductosRequest : ApiRequestBase, IApiRequest<BuscarProductosRequestModel, BuscarProductosRequestOptions>
{
    public BuscarProductosRequestModel Model { get; set; } = new();
    public BuscarProductosRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarProductosRequest.
/// </summary>
public sealed class BuscarProductosRequestModel
{
    /// <summary>
    ///     Parametro para buscar productos por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar productos por codigo.
    /// </summary>
    public string? Codigo { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarProductosRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarProductosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarProductosRequest.
/// </summary>
public sealed class BuscarProductosResponse : ApiResponseBase, IApiResponse<BuscarProductosResponseModel>
{
    public BuscarProductosResponseModel Model { get; set; } = new();
}

public sealed class BuscarProductosResponseModel
{
    /// <summary>
    ///     Lista de productos encontrados.
    /// </summary>
    public List<Producto> Productos { get; set; } = new();
}
