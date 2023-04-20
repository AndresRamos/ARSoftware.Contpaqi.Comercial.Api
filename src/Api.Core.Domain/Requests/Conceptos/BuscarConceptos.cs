using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar conceptos.
/// </summary>
public sealed class BuscarConceptosRequest : IContpaqiRequest<BuscarConceptosRequestModel, BuscarConceptosRequestOptions>
{
    public BuscarConceptosRequestModel Model { get; set; } = new();
    public BuscarConceptosRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosRequestModel
{
    /// <summary>
    ///     Parametro para buscar conceptos por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar conceptos por codigo.
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    ///     Parametro para buscar conceptos por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar conceptos por nombre:</para>
    ///         <code>SqlQuery = "CNOMBRECONCEPTO = 'nombre'"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admConceptos WHERE CNOMBRECONCEPTO = 'nombre'</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarConceptosRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarConceptosRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosResponse : IContpaqiResponse<BuscarConceptosResponseModel>
{
    public BuscarConceptosResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta BuscarConceptosRequest.
/// </summary>
public sealed class BuscarConceptosResponseModel
{
    public int NumeroRegistros => Conceptos.Count;

    /// <summary>
    ///     Lista de conceptos encontrados.
    /// </summary>
    public List<Concepto> Conceptos { get; set; } = new();
}
