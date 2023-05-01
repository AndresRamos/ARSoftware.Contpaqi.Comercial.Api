using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar almacenes.
/// </summary>
public sealed class
    BuscarAlmacenesRequest : ContpaqiRequest<BuscarAlmacenesRequestModel, BuscarAlmacenesRequestOptions, BuscarAlmacenesResponse>
{
    public BuscarAlmacenesRequest(BuscarAlmacenesRequestModel model, BuscarAlmacenesRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarAlmacenesRequest.
/// </summary>
public sealed class BuscarAlmacenesRequestModel
{
    /// <summary>
    ///     Parametro para buscar almacenes por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar almacenes por codigo.
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    ///     Parametro para buscar almacenes por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar almacenes por nombre:</para>
    ///         <code>SqlQuery = "CNOMBREALMACEN = 'nombre'"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admAlmacenes WHERE CNOMBREALMACEN = 'nombre'</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarAlmacenesRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarAlmacenesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarAlmacenesRequest.
/// </summary>
public sealed class BuscarAlmacenesResponse : ContpaqiResponse<BuscarAlmacenesResponseModel>
{
    public BuscarAlmacenesResponse(BuscarAlmacenesResponseModel model) : base(model)
    {
    }

    public static BuscarAlmacenesResponse CreateInstance(List<Almacen> almacenes)
    {
        return new BuscarAlmacenesResponse(new BuscarAlmacenesResponseModel(almacenes));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarAlmacenesResponse.
/// </summary>
public sealed class BuscarAlmacenesResponseModel
{
    public BuscarAlmacenesResponseModel(List<Almacen> almacenes)
    {
        Almacenes = almacenes;
    }

    public int NumeroRegistros => Almacenes.Count;

    /// <summary>
    ///     Lista de almacenes encontrados.
    /// </summary>
    public List<Almacen> Almacenes { get; set; }
}
