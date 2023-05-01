using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar productos.
/// </summary>
public sealed class
    BuscarProductosRequest : ContpaqiRequest<BuscarProductosRequestModel, BuscarProductosRequestOptions, BuscarProductosResponse>
{
    public BuscarProductosRequest(BuscarProductosRequestModel model, BuscarProductosRequestOptions options) : base(model, options)
    {
    }
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

    /// <summary>
    ///     Parametro para buscar productos por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar productos por nombre:</para>
    ///         <code>SqlQuery = "CNOMBREPRODUCTO = 'nombre'"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admProductos WHERE CNOMBREPRODUCTO = 'nombre'</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
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
public sealed class BuscarProductosResponse : ContpaqiResponse<BuscarProductosResponseModel>
{
    public BuscarProductosResponse(BuscarProductosResponseModel model) : base(model)
    {
    }

    public static BuscarProductosResponse CreateInstance(List<Producto> productos)
    {
        return new BuscarProductosResponse(new BuscarProductosResponseModel(productos));
    }
}

public sealed class BuscarProductosResponseModel
{
    public BuscarProductosResponseModel(List<Producto> productos)
    {
        Productos = productos;
    }

    public int NumeroRegistros => Productos.Count;

    /// <summary>
    ///     Lista de productos encontrados.
    /// </summary>
    public List<Producto> Productos { get; set; }
}
