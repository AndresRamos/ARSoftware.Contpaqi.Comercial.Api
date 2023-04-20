using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar productos.
/// </summary>
public sealed class BuscarProductosRequest : IContpaqiRequest<BuscarProductosRequestModel, BuscarProductosRequestOptions>
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
public sealed class BuscarProductosResponse : IContpaqiResponse<BuscarProductosResponseModel>
{
    public BuscarProductosResponseModel Model { get; set; } = new();
}

public sealed class BuscarProductosResponseModel
{
    public int NumeroRegistros => Productos.Count;

    /// <summary>
    ///     Lista de productos encontrados.
    /// </summary>
    public List<Producto> Productos { get; set; } = new();
}
